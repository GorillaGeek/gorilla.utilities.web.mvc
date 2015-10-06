using Gorilla.Utilities.Bags;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gorilla.Utilities.Web.Mvc.Models
{
    public class DataTableRequest
    {
        public int draw { get; set; }

        public int start { get; set; }

        public int length { get; set; }

        public Dictionary<string, string> search { get; set; }

        public List<Dictionary<string, string>> order { get; set; }

        public List<Dictionary<string, string>> columns { get; set; }

        public PaginationSettings ToSettings()
        {

            if (!(this.order != null && this.order.Any() && this.columns != null && this.columns.Any()))
            {
                throw new Exception("Invalid Data Table request");
            }

            var orderFirst = this.order.First();
            var orderColumn = this.columns[int.Parse(orderFirst["column"])]["data"];
            var orderDirection = orderFirst["dir"] != "asc" ? PaginationSettings.enSortOrder.Descending : PaginationSettings.enSortOrder.Ascending;

            var settings = new PaginationSettings(orderColumn)
            {
                Take = this.length,
                Skip = this.start,
                OrderColumn = orderColumn,
                OrderDirection = orderDirection
            };

            if (this.search != null && this.search.Any())
            {
                settings.Search = this.search.Keys.Contains("value") ? this.search["value"] : string.Empty;
            }

            return settings;
        }

        public DataTableResponse<T> ToResponse<T>(PagedResult<T> result)
        {

            return new DataTableResponse<T>(this.draw, result.Data)
            {
                recordsTotal = result.TotalRecords
            };

        }
    }
}
