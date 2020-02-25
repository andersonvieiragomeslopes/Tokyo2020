using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tokyo2020.WebApi.Models
{
    public class PagingParameterModel
    {
        const int maxPageSize = 20;

        public int pageNumber { get; set; } = 1;

        public int _pageSize { get; set; } = 10;
        public string TotalPage { get; set; }
        // public string QuerySearch { get; set; }

        public int pageSize {

            get { return _pageSize; }
            set {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}