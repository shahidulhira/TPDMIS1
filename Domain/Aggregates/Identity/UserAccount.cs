using RapidFireLib.Models.IdentityModels;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Aggregates.Identity
{
    [Table("AspNetUsers")]
    public class AspNetUser : RFNetUser
    {
        //public string GeoType { get; set; }
       // public string Password { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string Organization { get; set; }
        public string StaffID { get; set; }
       // public string PhoneNumber { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? EditDate { get; set; }


    }

    /// <summary>
    /// Login Request Model
    /// </summary>
    public class LoginUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string DeviceUniqueId { get; set; }
    }
}
