using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Aggregates.Common
{
    public class QuickSearch
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SearchId { get; set; }
        public string Logo { get; set; }
        public string Title { get; set; }
        public string GroupTitle { get; set; }
        public string SearchUrl { get; set; }
    }
    public class SearchView
    {
        public string GroupTitle { get; set; }
        public List<QuickSearch> QuickSearches { get; set; }
    }
    public class QuickSearchRequest{
        public string Query { get; set; }
        public List<QuickSearch> QuickSearches { get; set; }
    }
}
