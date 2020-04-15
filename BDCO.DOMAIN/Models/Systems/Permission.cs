using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDCO.Domain.Aggregates
{
    [Table("AppPermission")]
    public class AppPermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public int AppResourceId { get; set; }
        public bool ExecuteInsert { get; set; }
        public bool ExecuteRead { get; set; }
        public bool ExecuteUpdate { get; set; }
        public bool ExecuteDelete { get; set; }
    }

    [Table("AppResource")]
    public class AppResource
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength]
        public string RelativePath { get; set; }
        public bool? Active { get; set; }
        [MaxLength]
        public string DeployedPath { get; set; }
    }
}
