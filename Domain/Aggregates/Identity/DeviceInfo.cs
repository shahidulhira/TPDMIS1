using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Aggregates.Identity
{
    public class DeviceInfo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DeviceInfoId { get; set; }
        public int? UserId { get; set; }
        public string DeviceUniqueId { get; set; }
        public string DeviceId { get; set; }
    }
}
