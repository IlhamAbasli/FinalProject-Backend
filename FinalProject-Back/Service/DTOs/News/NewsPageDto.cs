using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.News
{
    public class NewsPageDto
    {
        public List<NewsDetailDto> News { get; set; }
        public int PageCount { get; set; }
    }
}
