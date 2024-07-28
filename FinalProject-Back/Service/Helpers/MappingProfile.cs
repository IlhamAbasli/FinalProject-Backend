using AutoMapper;
using Domain.Entities;
using Service.DTOs.Genre;
using Service.DTOs.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<News, NewsDto>();
            CreateMap<NewsEditDto, News>();
            CreateMap<News,NewsDetailDto>();

            CreateMap<GenreCreateDto, Genre>();
            CreateMap<Genre,GenreDto>();
            CreateMap<GenreEditDto, Genre>();
        }

    }
}
