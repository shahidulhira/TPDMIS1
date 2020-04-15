using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RapidFireLib.Models.IdentityModels
{
    public class RFNetUser : IdentityUser
    {
        public RFNetUser()
        {
            //Claims = new List<IdentityUserClaim<string>>();
            //Logins = new List<IdentityUserLogin<string>>();
            //Tokens = new List<IdentityUserToken<string>>();
            //RecoveryCodes = new List<TwoFactorRecoveryCode>();
        }
        [Key]
        public override string Id { get; set; }
        public override string UserName { get; set; }
        public override string NormalizedUserName { get; set; }
        public override string Email { get; set; }
        public override string NormalizedEmail { get; set; }
        public override bool EmailConfirmed { get; set; }
        public override string PasswordHash { get; set; }
        public string Password { get; set; }
        public override string SecurityStamp { get; set; }
        public override string ConcurrencyStamp { get; set; }
        public override string PhoneNumber { get; set; }
        public override bool PhoneNumberConfirmed { get; set; }
        public override bool TwoFactorEnabled { get; set; }
        public override DateTimeOffset? LockoutEnd { get; set; }
        public override bool LockoutEnabled { get; set; }
        public override int AccessFailedCount { get; set; }
        //public string AuthenticatorKey { get; set; }

        //Custom Property Added
        public int? UserId { get; set; }
        //public string DesignationId { get; set; }
        //public int? UserGroupId { get; set; }
        //public string FullName { get; set; }

        //Navigation Properties Start
        //public virtual List<IdentityUserClaim<string>> Claims { get; set; }
        //public virtual List<IdentityUserLogin<string>> Logins { get; set; }
        //public virtual List<IdentityUserToken<string>> Tokens { get; set; }
        //public virtual List<TwoFactorRecoveryCode> RecoveryCodes { get; set; }
        //Navigation Properties End
    }
    public class TwoFactorRecoveryCode
    {
        public string Code { get; set; }

        public bool Redeemed { get; set; }
    }
}
