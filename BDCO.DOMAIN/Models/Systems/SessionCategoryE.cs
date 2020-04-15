using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDCO.Domain.Aggregates.Systems
{
    [Table("SessionCategoryE")]
    public class SessionCategoryE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RecordId { get; set; }
        public string TypeId { get; set; }
        public string TypeName { get; set; }
        public string TypeNameEn { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryNameEn { get; set; }
    }

}
