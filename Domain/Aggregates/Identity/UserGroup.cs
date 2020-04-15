using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Aggregates
{
    [Table("AspNetRole")]
    public class AspNetRole:IdentityRole
    {
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int? UserId { get; set; }
        public DateTime EntryDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModificationDate { get; set; }

    }
    public class AspNetRoleView : IdentityRole
    {
        public string Description { get; set; }
        public string IsActive { get; set; }
        public string EntryDate { get; set; }
        public long? TotalRecord { get; set; }
        public string FirstLetter { get; set; }
        public int? TotalMember { get; set; }
        public int? UIAccess { get; set; }
        public string Rating { get; set; }
        public string LastAcessOn { get; set; }
    }
    public class GroupSubView
    {
        public string Name { get; set; }
    }
}
