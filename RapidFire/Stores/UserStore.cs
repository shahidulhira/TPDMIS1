//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using RapidFireLib.Lib.Core;
//using RapidFireLib.Models.IdentityModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading;
//using System.Threading.Tasks;

//public class UserStore<TUser> :
//        IUserClaimStore<TUser>,
//        IUserLoginStore<TUser>,
//        IUserPasswordStore<TUser>,
//        IUserSecurityStampStore<TUser>,
//        IUserEmailStore<TUser>,
//        IUserPhoneNumberStore<TUser>,
//        IQueryableUserStore<TUser>,
//        IUserTwoFactorStore<TUser>,
//        IUserLockoutStore<TUser>,
//        IUserTwoFactorRecoveryCodeStore<TUser>,
//        IProtectedUserStore<TUser>

//        where TUser : RFNetUser
//{

//    RapidFire rf;
//    public UserStore(ref IConfig config)
//    {
//        rf = new RapidFire(config);
//    }


//    public IQueryable<TUser> Users => (IQueryable<TUser>)rf.Db.GetAll(new RFNetUser(), "");
//    private readonly ILookupNormalizer _normalizer;
//    //public async Task SetTokenAsync(TUser user, string loginProvider, string name, string value, CancellationToken cancellationToken)
//    //{
//    //    cancellationToken.ThrowIfCancellationRequested();

//    //    if (user.Tokens == null)
//    //    {
//    //        user.Tokens = new List<IdentityUserToken<string>>();
//    //    }

//    //    IdentityUserToken<string> token = user.Tokens.FirstOrDefault(x => x.LoginProvider == loginProvider && x.Name == name);

//    //    if (token == null)
//    //    {
//    //        token = new IdentityUserToken<string> { LoginProvider = loginProvider, Name = name, Value = value };
//    //        user.Tokens.Add(token);
//    //    }
//    //    else
//    //    {
//    //        token.Value = value;
//    //    }

//    //    await Task.Run(()=>rf.Db.Save(user));
//    //}

//    //public Task RemoveTokenAsync(TUser user, string loginProvider, string name, CancellationToken cancellationToken)
//    //{
//    //    cancellationToken.ThrowIfCancellationRequested();

//    //    if (user?.Tokens == null)
//    //    {
//    //        return Task.CompletedTask;
//    //    }

//    //    user.Tokens.RemoveAll(x => x.LoginProvider == loginProvider && x.Name == name);

//    //    return Task.Run(() => rf.Db.Save(user));
//    //}

//    //public Task<string> GetTokenAsync(TUser user, string loginProvider, string name, CancellationToken cancellationToken)
//    //{
//    //    cancellationToken.ThrowIfCancellationRequested();

//    //    return Task.FromResult(user?.Tokens?.FirstOrDefault(x => x.LoginProvider == loginProvider && x.Name == name)?.Value);
//    //}

//    //public async Task<string> GetAuthenticatorKeyAsync(TUser user, CancellationToken cancellationToken)
//    //{
//    //    cancellationToken.ThrowIfCancellationRequested();

//    //    return (await Task.Run(()=> (TUser)rf.Db.GetById(user,user.Id)))?.AuthenticatorKey ?? user.AuthenticatorKey;
//    //}

//    //public Task SetAuthenticatorKeyAsync(TUser user, string key, CancellationToken cancellationToken)
//    //{
//    //    cancellationToken.ThrowIfCancellationRequested();

//    //    user.AuthenticatorKey = key;
//    //    return Task.Run(() => rf.Db.Save(user));
//    //}

//    public async Task<IdentityResult> CreateAsync(TUser user, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();
//        TUser u = Users.FirstOrDefault(x => x.UserName == user.UserName);
//        if (u != null)
//        {
//            return IdentityResult.Failed(new IdentityError { Code = "Username already in use" });
//        }
//        await Task.Run(() => rf.Db.Save(user));
//        if (user.Email != null)
//        {
//            await SetEmailAsync(user, user.Email, cancellationToken);
//        }
//        await Task.Run(() => rf.Db.Save(user));
//        return IdentityResult.Success;
//    }

//    public async Task<IdentityResult> DeleteAsync(TUser user, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        await Task.Run(() => rf.Db.Delete(user));
//        return IdentityResult.Success;
//    }

//    public Task<TUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();
//        return Task.Run(() => Users.FirstOrDefault(x => x.UserId == Convert.ToInt32(userId)));
//    }

//    public Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();
//        return Task.Run(()=>Users.FirstOrDefault(x=>x.NormalizedUserName == normalizedUserName.ToUpper()));
//    }

//    public async Task<IdentityResult> UpdateAsync(TUser user, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();
//        await SetEmailAsync(user, user.Email, cancellationToken);
//        await Task.Run(()=> rf.Db.Save(user));
//        return IdentityResult.Success;
//    }

//    public Task AddClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        if (user.Claims == null)
//        {
//            user.Claims = new List<IdentityUserClaim<string>>();
//        }

//        user.Claims.AddRange(claims.Select(claim => new IdentityUserClaim<string>()
//        {
//            ClaimType = claim.Type,
//            ClaimValue = claim.Value
//        }));

//        return Task.Run(() => rf.Db.Save(user.Claims));
//    }

//    public Task ReplaceClaimAsync(TUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        user?.Claims?.RemoveAll(x => x.ClaimType == claim.Type);

//        user?.Claims?.Add(new IdentityUserClaim<string>()
//        {
//            ClaimType = newClaim.Type,
//            ClaimValue = newClaim.Value
//        });

//        return Task.Run(() => rf.Db.Save(user.Claims));
//    }

//    public Task RemoveClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        foreach (Claim claim in claims)
//        {
//            user?.Claims?.RemoveAll(x => x.ClaimType == claim.Type);
//        }

//        return Task.Run(() => rf.Db.Save(user.Claims));
//    }

//    public async Task<IList<TUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();
//        //var claimedUsers = Users.Include(x => x.Claims).Where(x=>x.Claims.Where(y=>y.ClaimType.Where(p=>p == Convert.To)));
//        var users = new List<TUser>();
//        return (await Task.Run(()=>users)).ToList();
//    }

//    public Task<string> GetNormalizedUserNameAsync(TUser user, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        return Task.FromResult(user.NormalizedUserName);
//    }

//    public Task<string> GetUserIdAsync(TUser user, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        return Task.FromResult(user?.Id);
//    }

//    public Task<string> GetUserNameAsync(TUser user, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        return Task.FromResult(user.UserName);
//    }

//    public async Task<IList<Claim>> GetClaimsAsync(TUser user, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        TUser dbUser = Users.FirstOrDefault(x=>x.UserId == user.UserId);
//        return dbUser?.Claims?.Select(x => new Claim(x.ClaimType, x.ClaimValue))?.ToList() ?? new List<Claim>();
//    }

//    public Task SetNormalizedUserNameAsync(TUser user, string normalizedName, CancellationToken cancellationToken)
//    {
//        user.NormalizedUserName = normalizedName;
//        return Task.Run(() => rf.Db.Save(user));
//    }

//    public Task SetUserNameAsync(TUser user, string userName, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        user.UserName = userName;
//        return Task.Run(() => rf.Db.Save(user));
//    }

//    void IDisposable.Dispose()
//    {
//    }

//    public async Task<string> GetEmailAsync(TUser user, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        return (await Task.Run(()=> Users.FirstOrDefault(x => x.UserId == user.UserId)))?.Email ?? user.Email;
//    }

//    public async Task<bool> GetEmailConfirmedAsync(TUser user, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        return (await Task.Run(() => Users.FirstOrDefault(x => x.UserId == user.UserId)))?.EmailConfirmed ?? user.EmailConfirmed;
//    }

//    public async Task<TUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        return await Task.Run(() => Users.FirstOrDefault(x => x.NormalizedEmail == normalizedEmail));
//    }

//    public async Task<string> GetNormalizedEmailAsync(TUser user, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        return (await Task.Run(() => Users.FirstOrDefault(x => x.UserId == user.UserId)))?.NormalizedEmail ?? user.NormalizedEmail;
//    }

//    public Task SetEmailConfirmedAsync(TUser user, bool confirmed, CancellationToken cancellationToken)
//    {
//        user.EmailConfirmed = confirmed;
//        return Task.Run(() => rf.Db.Save(user));
//    }

//    public Task SetNormalizedEmailAsync(TUser user, string normalizedEmail, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        user.NormalizedEmail = normalizedEmail;
//        return Task.Run(() => rf.Db.Save(user));
//    }

//    public async Task SetEmailAsync(TUser user, string email, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        await SetNormalizedEmailAsync(user, _normalizer.Normalize(user.Email), cancellationToken);
//        user.Email = email;

//        await Task.Run(() => rf.Db.Save(user));
//    }

//    public async Task<int> GetAccessFailedCountAsync(TUser user, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        return (await Task.Run(()=> Users.FirstOrDefault(x => x.UserId == user.UserId)))?.AccessFailedCount ?? user.AccessFailedCount;
//    }

//    public async Task<bool> GetLockoutEnabledAsync(TUser user, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        return (await Task.Run(() => Users.FirstOrDefault(x => x.UserId == user.UserId)))?.LockoutEnabled ?? user.LockoutEnabled;
//    }

//    public async Task<int> IncrementAccessFailedCountAsync(TUser user, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        user.AccessFailedCount++;
//        await Task.Run(() => rf.Db.Save(user));
//        return user.AccessFailedCount;
//    }

//    public Task ResetAccessFailedCountAsync(TUser user, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        user.AccessFailedCount = 0;
//        return Task.Run(() => rf.Db.Save(user));
//    }

//    public async Task<DateTimeOffset?> GetLockoutEndDateAsync(TUser user, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        return (await Task.Run(() => Users.FirstOrDefault(x => x.UserId == user.UserId)))?.LockoutEnd ?? user.LockoutEnd;
//    }

//    public Task SetLockoutEndDateAsync(TUser user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        user.LockoutEnd = lockoutEnd;
//        return Task.Run(() => rf.Db.Save(user));
//    }

//    public Task SetLockoutEnabledAsync(TUser user, bool enabled, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        user.LockoutEnabled = enabled;
//        return Task.Run(() => rf.Db.Save(user));
//    }

//    public Task AddLoginAsync(TUser user, UserLoginInfo login, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        if (user.Logins == null)
//        {
//            user.Logins = new List<IdentityUserLogin<string>>();
//        }

//        user.Logins.Add(new IdentityUserLogin<string>
//        {
//            UserId = user.Id,
//            LoginProvider = login.LoginProvider,
//            ProviderDisplayName = login.ProviderDisplayName,
//            ProviderKey = login.ProviderKey
//        });

//        return Task.Run(() => rf.Db.Save(user));
//    }

//    public async Task RemoveLoginAsync(TUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        TUser dbUser = await Task.Run(() => Users.FirstOrDefault(x => x.UserId == user.UserId));
//        user.Logins.RemoveAll(x => x.LoginProvider == loginProvider && x.ProviderKey == providerKey);
//        dbUser.Logins.RemoveAll(x => x.LoginProvider == loginProvider && x.ProviderKey == providerKey);

//        await Task.Run(() => rf.Db.Save(user));
//    }

//    public async Task<TUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();
//        var loginProvierUser = (IQueryable<IdentityUserLogin<string>>)rf.Db.GetAll(new IdentityUserLogin<string>(),"");
//        var userId = loginProvierUser.Where(x => x.LoginProvider == loginProvider && x.ProviderKey == providerKey).FirstOrDefault().UserId;
//        return await Task.Run(()=> Users.FirstOrDefault(x => x.Id == userId));
//    }

//    public async Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        TUser dbUser = await Task.Run(() => Users.FirstOrDefault(x => x.UserId == user.UserId));
//        return dbUser?.Logins?.Select(x => new UserLoginInfo(x.LoginProvider, x.ProviderKey, x.ProviderDisplayName))?.ToList() ?? new List<UserLoginInfo>();
//    }

//    public Task<string> GetPasswordHashAsync(TUser user, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        return Task.FromResult(user.PasswordHash);
//    }

//    public async Task<bool> HasPasswordAsync(TUser user, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        return (await Task.Run(() => Users.FirstOrDefault(x => x.UserId == user.UserId)))?.PasswordHash != null;
//    }

//    public Task SetPasswordHashAsync(TUser user, string passwordHash, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        user.PasswordHash = passwordHash;
//        return Task.Run(() => rf.Db.Save(user));
//    }

//    public async Task<string> GetPhoneNumberAsync(TUser user, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        return (await Task.Run(() => Users.FirstOrDefault(x => x.UserId == user.UserId)))?.PhoneNumber;
//    }

//    public async Task<bool> GetPhoneNumberConfirmedAsync(TUser user, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        return (await Task.Run(() => Users.FirstOrDefault(x => x.UserId == user.UserId)))?.PhoneNumberConfirmed ?? false;
//    }

//    public Task SetPhoneNumberAsync(TUser user, string phoneNumber, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        user.PhoneNumber = phoneNumber;
//        return Task.Run(() => rf.Db.Save(user));
//    }

//    public Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        user.PhoneNumberConfirmed = confirmed;
//        return Task.Run(() => rf.Db.Save(user));
//    }

//    public Task AddToRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
//    {
//        //if (user.Roles == null)
//        //{
//        //    user.Roles = new List<string>();
//        //}

//        //user.Roles.Add(roleName);

//        return Task.Run(() => rf.Db.Save(user));
//    }

//    public Task RemoveFromRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        //user.Roles.Remove(roleName);

//        return Task.Run(() => rf.Db.Save(user));
//    }

    

//    public async Task<bool> IsInRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        TUser dbUser = await Task.Run(() => Users.FirstOrDefault(x => x.UserId == user.UserId));
//        //return dbUser?.Roles.Contains(roleName) ?? false;
//        return false;
//    }

//    public Task<string> GetSecurityStampAsync(TUser user, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        return Task.FromResult(user.SecurityStamp);
//    }

//    public Task SetSecurityStampAsync(TUser user, string stamp, CancellationToken cancellationToken)
//    {
//        user.SecurityStamp = stamp;
//        return Task.Run(() => rf.Db.Save(user));
//    }

//    public Task ReplaceCodesAsync(TUser user, IEnumerable<string> recoveryCodes, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        user.RecoveryCodes = recoveryCodes.Select(x => new TwoFactorRecoveryCode { Code = x, Redeemed = false })
//            .ToList();

//        return Task.Run(() => rf.Db.Save(user));
//    }

//    public async Task<bool> RedeemCodeAsync(TUser user, string code, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        TUser dbUser = await Task.Run(() => Users.FirstOrDefault(x => x.UserId == user.UserId));
//        if (dbUser == null)
//        {
//            return false;
//        }

//        TwoFactorRecoveryCode c = user.RecoveryCodes.FirstOrDefault(x => x.Code == code);

//        if (c == null || c.Redeemed)
//        {
//            return false;
//        }

//        c.Redeemed = true;

//        await Task.Run(() => rf.Db.Save(user));

//        return true;
//    }

//    public async Task<int> CountCodesAsync(TUser user, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        TUser dbUser = await Task.Run(() => Users.FirstOrDefault(x => x.UserId == user.UserId));
//        return dbUser?.RecoveryCodes.Count ?? 0;
//    }

//    public async Task<bool> GetTwoFactorEnabledAsync(TUser user, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        return (await Task.Run(() => Users.FirstOrDefault(x => x.UserId == user.UserId)))?.TwoFactorEnabled ?? user.TwoFactorEnabled;
//    }

//    public Task SetTwoFactorEnabledAsync(TUser user, bool enabled, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        user.TwoFactorEnabled = enabled;
//        return Task.Run(() => rf.Db.Save(user));
//    }
//}
