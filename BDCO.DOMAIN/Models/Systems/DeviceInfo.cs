using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDCO.Domain.Models
{
    public class DeviceInfo
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RecordId { get; set; }
        public int? UserId { get; set; }
        public string DeviceUniqueId { get; set; }
        public string DeviceId { get; set; }
    }
}
