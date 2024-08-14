using Application.Articles.Commands.Create;
using Application.Articles.Commands.Update;
using AutoMapper;
using Core.Articles;
using Infrastructure.Articles.DTOs;

namespace Application.Articles.Mappings
{
    public class ArticleMapping : Profile
    {
        public ArticleMapping()
        {
            CreateMap<Article, ArticleDto>().ReverseMap();
            CreateMap<CreateArticleCommand, Article>();
            CreateMap<UpdateArticleCommand, Article>();
        }
    }
}
