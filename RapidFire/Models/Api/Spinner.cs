using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RapidFireLib.Models.Api
{
    public class SpinnerRequest
    {
        public string ModelName { get; set; }
        public string DisplayText { get; set; }
        public string ValueText { get; set; }
        public string Where { get; set; }
        public string Sql { get; set; }
    }
    public class SpinnerValue
    {
        public string DisplayText { get; set; }
        public string ValueText { get; set; }
    }
    public class ForApiResponse
    {
        public int UserId { get; set; }
        public int PageSize { get; set; }
        public int PageNo { get; set; }

    }
}
