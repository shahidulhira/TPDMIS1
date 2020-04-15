using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using BDCO.Domain.Result;
using BDCO.Core.Command;
using BDCO.Domain.RequestModels;
using BDCO.Domain.Aggregates;
using System.Collections.Generic;
using BDCO.Domain.Response;
using BDCO.Domain.Models;

namespace BDCO.Domain.Identity
{
    [Table("AspNetUsers")]
    public class AspNetUsers : AggregateRoot
    {
        [Key]
        public string Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        public string Email { get; set; }
        public string NGOID { get; set; }
        //public string DistCode { get; set; }
        //public string UpazilaCode { get; set; }
        //public string UniCode { get; set; }
        //public string VillageCode { get; set; }
        public bool EmailConfirmed { get; set; }
        public string OldPassword { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string Organization { get; set; }
        public string StaffID { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public int? UserType { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? EditDate { get; set; }
        public string EditBy { get; set; }
        public string GeoType { get; set; }
        public string LanguageName { get; set; }
        [NotMapped]
        public bool LoginStatus { get; set; }
        [NotMapped]
        public string SuggestedUserId { get; set; }

        private UnitOfWork _unitOfWork { get; set; } = new UnitOfWork();

        public AspNetUsers()
        {

        }


        public LoginResponseResult Login(UsersLogin userLogin)
        {
            LoginResponseResult result = new LoginResponseResult();
            string sql = String.Format(@"SELECT [UserID],[UserName],[OldPassword] as [Password],UserType,GeoType,[FullName],[Designation],[Organization] ,StaffID,[PhoneNumber]
                                            ,[Email] , convert(nvarchar(12), cast([CreateDate]  as date),103)   CreateDate,
											ISNULL( convert (nvarchar(10), cast(EditDate as date),103),'')EditDate
                                            FROM [AspNetUsers] where IsActive=1 AND UserName='{0}' AND OldPassword ='{1}' ", userLogin.UserName, userLogin.Password);
            result.UserInfo = _unitOfWork.context.Database.SqlQuery<LoginUserInfo>(sql).FirstOrDefault();
            if (result.UserInfo != null)
            {
                if (userLogin.DeviceUniqueId != null)
                {
                    var exist = _unitOfWork.GenericRepositories<DeviceInfo>().FindBy(x => x.DeviceUniqueId == userLogin.DeviceUniqueId && x.UserId == result.UserInfo.UserID).FirstOrDefault();
                    if (exist == null)
                    {
                        DeviceInfo deviceInfo = new DeviceInfo()
                        {
                            DeviceUniqueId = userLogin.DeviceUniqueId,
                            UserId = result.UserInfo.UserID
                        };
                        _unitOfWork.GenericRepositories<DeviceInfo>().Insert(deviceInfo);
                        _unitOfWork.SaveChange();

                        sql = $@"SELECT RecordId,DeviceUniqueId,REPLACE(STR(RecordId, 5), SPACE(1), '0') DeviceId,UserId FROM DeviceInfo WHERE DeviceUniqueId='{userLogin.DeviceUniqueId}' AND UserId='{result.UserInfo.UserID}'";
                        var device = _unitOfWork.GenericRepositories<DeviceInfo>().GetRecordSet(sql).FirstOrDefault();
                        _unitOfWork.GenericRepositories<DeviceInfo>().Update(device);
                        _unitOfWork.SaveChange();
                        result.UserInfo.DeviceId = device.DeviceId;
                    }
                    else
                    {
                        result.UserInfo.DeviceId = exist.DeviceId;
                    }
                }

                

                
            }
            else
                return result = null;

            return result;

        }

        public List<AspNetUsers> GetUserList(AspNetUsers user)
        {
            return _unitOfWork.GenericRepositories<AspNetUsers>().GetAll().ToList();
        }
        public SuggestedUserIdResponse GetSuggestedUserId(SuggestedUserIdRM suggestedUserIdRM)
        {
            string sql = "";

            SuggestedUserIdResponse result = new SuggestedUserIdResponse();

            sql = string.Format(@"Select Top 1 Cast(UserId as nvarchar(max)) From AspNetUsers  Where len(UserId) <> '5' ORDER BY UserId DESC");
            result.SuggestedUserId = _unitOfWork.RawSqlQuery<SuggestedUserIdResponse>(sql).ToString();

            return result;
        }

        public CommandResult SaveUser(UserRM command)
        {
            string result = "";

            try
            {
                //int? userExist = 0;
                string sqluser = string.Format(@"SELECT TOP 1 CAST(UserId as NVARCHAR(MAX)) as UserId FROM AspNetUsers  Where UserName = '{0}' ", command.objUser.PhoneNumber);
                var userExist = _unitOfWork.GenericRepositories<AspNetUsers>().FindBy(x => x.UserName == command.objUser.PhoneNumber).FirstOrDefault();

                if (userExist == null && command.objUser.UserID == 0)
                {
                    if (command.objUser.UserID == 0)
                    {
                        string userid = string.Format(@"Select Top 1 Cast(UserId as nvarchar(max)) From AspNetUsers  Where len(UserId) <> '5' ORDER BY UserId DESC");
                        string ipname = string.Format(@"Select Name From ObservationalOrganization Where ID = {0}", command.objUser.Organization);
                        var newuserid = _unitOfWork.RawSqlQuery<string>(userid).FirstOrDefault();
                        //command.objUser.UserID = Convert.ToInt32(newuserid) + 1;
                        command.objUser.OldPassword = "12345";
                        command.objUser.Id = Guid.NewGuid().ToString();
                        command.objUser.UserName = command.objUser.PhoneNumber;
                        command.objUser.PasswordHash = Hash(command.objUser.OldPassword);
                        //command.objUser.PasswordHash = "AGFHk67PwvkjEGGoOTToIyMpqzAaWyi0u+duhcnImkR/feAb5pzFBgQLwKAg7U8b0A==";
                        command.objUser.EmailConfirmed = false;
                        command.objUser.PhoneNumberConfirmed = false;
                        command.objUser.TwoFactorEnabled = false;
                        command.objUser.LockoutEnabled = true;
                        command.objUser.AccessFailedCount = 0;
                        command.objUser.Organization = _unitOfWork.RawSqlQuery<string>(ipname).FirstOrDefault();
                        command.objUser.LanguageName = "en";
                        //command.objSuchanaUser.SecurityStamp = new AspNetUsers
                        //{
                        //    PasswordHash = Hash(command.objSuchanaUser.OldPassword),
                        //    Email = command.objSuchanaUser.Email,
                        //    UserName = command.objSuchanaUser.PhoneNumber,
                        //    SecurityStamp = Guid.NewGuid().ToString()
                        //}.ToString();

                        command.objUser.CreateBy = command.UserId + "";
                        command.objUser.CreateDate = DateTime.Now;
                        command.objUser.SecurityStamp = "540d247b-b097-4783-81d3-18bbe60e04bc";

                        _unitOfWork.GenericRepositories<AspNetUsers>().Insert(command.objUser);
                        result = _unitOfWork.SaveChange();
                        LogEventStore(command.objUser.Id.ToString(), "AspNetUsers", "Save");
                    }

                    return new CommandResult()
                    {
                        Status = 400,
                        Message = result != "" ? result : "Record Save successfully."
                    };
                }
                else if (userExist != null && command.objUser.UserID != 0)
                {
                    string ipname = string.Format(@"Select Name From ObservationalOrganization Where ID = {0}", command.objUser.Organization);
                    var user = _unitOfWork.GenericRepositories<AspNetUsers>().GetAll().Where(x => x.UserID == command.objUser.UserID).FirstOrDefault();

                    user.FullName = command.objUser.FullName;
                    user.Email = command.objUser.Email;
                    user.PhoneNumber = command.objUser.PhoneNumber;
                    user.UserName = command.objUser.PhoneNumber;
                    user.Organization = _unitOfWork.RawSqlQuery<string>(ipname).FirstOrDefault();
                    user.Designation = command.objUser.Designation;
                    user.StaffID = command.objUser.StaffID;
                    user.IsActive = command.objUser.IsActive;
                    user.UserType = command.objUser.UserType;
                    user.GeoType = command.objUser.GeoType;
                    user.EditBy = command.UserId + "";
                    user.EditDate = DateTime.Now;
                    _unitOfWork.GenericRepositories<AspNetUsers>().Update(user);
                    result = _unitOfWork.SaveChange();
                    LogEventStore(command.objUser.UserID.ToString(), "AspNetUsers", "Update");


                    return new CommandResult()
                    {
                        Status = 200,
                        Message = result != "" ? result : "Record Save successfully."
                    };
                }
                else
                {
                    return new CommandResult()
                    {
                        Status = 111,
                        Message = result != "" ? result : "User already Exist!"
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Hash(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            var hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }

    }
    public class CheckUserIdIsExist
    {
        public int UserId { get; set; }
    }
    public class LoginUserInfo
    {
        public int? UserID { get; set; }
        public string GeoType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string Organization { get; set; }
        public string StaffID { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string CreateDate { get; set; }
        public string EditDate { get; set; }
        public string DeviceId { get; set; }



    }
}
