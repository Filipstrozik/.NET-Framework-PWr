using Lab9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab9.ArticlesContext
{
    public interface IArticlesContext
    {
        List<Article> GetArticles();
        Article GetArticle(int id);
        void AddArticle(Article article);
        void RemoveArticle(int id);

        void UpdateArticle(Article article);

    }
}
