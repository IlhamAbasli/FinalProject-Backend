﻿using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.News
{
    public class NewsEditDto
    {
        public string NewsTitle { get; set; }
        public string NewsContent1 { get; set; }
        public string NewsContent2 { get; set; }
        public string NewsContent3 { get; set; }
        public List<IFormFile> NewImages { get; set; }
        public ICollection<NewsImage> NewsImages {  get; set; }
    }
}
