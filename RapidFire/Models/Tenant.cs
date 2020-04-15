using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidFireLib.Models
{
    [Table("Tenants")]
    public class Tenant
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string TenantName { get; set; }
        public string SubdomainPrefix { get; set; }
        public bool HasDatabase { get; set; }
        public string DatabaseConnectionString { get; set; }
        public bool HasDefaultSchema { get; set; }
        public string SchemaName { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedOn { get; set; }
        public bool IsPendingUpdate { get; set; }
        public string UpdateSQL { get; set; }
    }
    
}
