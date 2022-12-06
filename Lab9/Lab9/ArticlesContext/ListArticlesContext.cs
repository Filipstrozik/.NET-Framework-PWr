using Lab9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab9.ArticlesContext
{
    public class ListArticlesContext : IArticlesContext
    {


        List<Article> articlesList = new List<Article>() {
            new Article(0, "Football List", 55.99, new DateTime(2033,3,22), Category.Sport)
            };


        public void AddArticle(Article article)  
        {
            int nextId;
            if (articlesList.Count() == 0)
            {
                nextId = 0;
            } else
            {
                nextId = articlesList.Max(a => a.Id) + 1;
            }
            article.Id = nextId;
            articlesList.Add(article);
        }

        public Article GetArticle(int id)
        {
            return articlesList.FirstOrDefault(a => a.Id == id);
        }

        public List<Article> GetArticles()
        {
            return articlesList;
        }

        public void RemoveArticle(int id)
        {
            Article articleToRemove = articlesList.FirstOrDefault(a => a.Id == id);
            if (articleToRemove != null)
                articlesList.Remove(articleToRemove);
        }

        public void UpdateArticle(Article article)
        {
            Article articleToUpdate = articlesList.FirstOrDefault(a => a.Id == article.Id);
            articlesList = articlesList.Select(a => (a.Id == article.Id) ? article : a).ToList();
        }
    }
}
