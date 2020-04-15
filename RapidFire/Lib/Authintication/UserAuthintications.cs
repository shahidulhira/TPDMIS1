using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Novell.Directory.Ldap;
using RapidFireLib.Lib.Core;
using RapidFireLib.Lib.Extension;
using RapidFireLib.Models.IdentityModels;
using System;
using System.Collections;
using System.DirectoryServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RapidFireLib.Lib.Authintication
{
    public enum LoginType
    {
        DbLogin,
        ADLogin,
        ProviderLogin
    }
    public class UserAuthintications
    {
        Db Db = null;
        IConfig configuration;
        public UserAuthintications(IConfig config)
        {
            Db = new Db(config);
        }
        //PasswordHasher passwordHasher = new PasswordHasher();
        //Configuration configuration = new Configuration();
        //public UserAuthintications(ref Configuration _configuration)
        //{
        //    configuration = _configuration;
        //}
        public User Authenticate(object model, string username, string password, bool isHashBased, LoginType loginType, DbContext context = null)
        {
            RFNetUser user = new RFNetUser();
            if (LoginType.DbLogin == loginType)
            {
                user = GetUserByUserName(model, username);
                if (user == null) return null;
                bool isVerified;
                var passwordHasher = new PasswordHasher<RFNetUser>(new OptionsWrapper<PasswordHasherOptions>(
                new PasswordHasherOptions()
                {
                    CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2
                }));
                if (isHashBased)
                    isVerified = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Success;
                else
                    isVerified = user.Password == password;
                if (isVerified)
                    return GetResonseUser(user, GetToken(user));
            }
            else if (LoginType.ADLogin == loginType)
            {
                if (LoginLdap(username, password))
                    user = GetUserByUserName(model, username);
                if (user != null)
                    return GetResonseUser(user, GetToken(user));
            }
            return null;

        }
        private RFNetUser GetUserByUserName(object model, string username)
        {
            var modelCasted = (RFNetUser)model;
            object rec = new object();
            Func<RFNetUser, bool> func = x => x.UserName == username;
            rec = Db.GetAll(model, func);
            bool isExists = (int)rec.GetPropertyValue("Count") != 0;
            if (!isExists)
                return null;
            var user = (RFNetUser)(((IList)rec)[0]);
            if (user == null)
                return null;
            return user;
        }
        private User GetResonseUser(RFNetUser user, string token)
        {
            var responseUser = new User();
            responseUser.UserId = user.UserId ?? 0;
            responseUser.Token = token;
            responseUser.Email = user.Email;
            responseUser.Username = user.UserName;
            return responseUser;
        }
        private string GetToken(RFNetUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var expireAt = DateTime.MaxValue;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name, user.UserName.ToString()),
                        new Claim("UserId", user.UserId.ToString()),
                }),
                //Expires = expireAt,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(@"MIGpAgEAAiEAuMmqfAzvVKJpIieaQkfC8BlZACwoOZssBCc/HIphNXcCAwEAAQIg
                                            Givly4ABfZkrDr1RKcYEI8Oyi9IoYes6eiO2fU1ALIECEQDe3gSNIlRk7Y8isu+Y
                                            qS1hAhEA1EJmx1b6rhjMxd4r2SG51wIQVylfgE7/0KU0CK8Qk5T+oQIQOI1cft3g
                                            ukPnQwy3mAlRTwIQDeu1TMQl74QOdaI3YZ5voA==")), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool LoginLdap(String username, String pwd)
        {
            String domainAndUsername = "" + @"\" + username;
            DirectoryEntry entry = new DirectoryEntry("LDAP://" + "", domainAndUsername, pwd);
            try
            {
                //Bind to the native AdsObject to force authentication.            
                Object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();
                if (null == result) { return false; }
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("Error authenticating user. " + ex.Message);
            }
            return true;
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(@"MIGpAgEAAiEAuMmqfAzvVKJpIieaQkfC8BlZACwoOZssBCc/HIphNXcCAwEAAQIg
                                            Givly4ABfZkrDr1RKcYEI8Oyi9IoYes6eiO2fU1ALIECEQDe3gSNIlRk7Y8isu+Y
                                            qS1hAhEA1EJmx1b6rhjMxd4r2SG51wIQVylfgE7/0KU0CK8Qk5T+oQIQOI1cft3g
                                            ukPnQwy3mAlRTwIQDeu1TMQl74QOdaI3YZ5voA==")),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
        private string RefreshToken(string username, TimeSpan timeSpan, string jwtKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Expiration, DateTime.UtcNow.Add(timeSpan).ToString()),
                }),
                Expires = DateTime.UtcNow.Add(timeSpan).AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey)), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = tokenHandler.WriteToken(token);
            return refreshToken;
        }
        public bool VerifyRefreshToken(string token, string refreshToken)
        {
            var tokenPrincipal = GetPrincipalFromExpiredToken(token);
            var refreshTokenPrincipal = GetPrincipalFromExpiredToken(refreshToken);
            return tokenPrincipal.Identity.Name == refreshTokenPrincipal.Identity.Name;
        }
    }
    public class User
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public string DeviceId { get; set; }
        public string GeoType { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string Organization { get; set; }
        public string StaffID { get; set; }
        public string PhoneNumber { get; set; }
        public string CreateDate { get; set; }
        public string EditDate { get; set; }
    }
}
