using Lab9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab9.ArticlesContext
{
    public class MapArticlesContext : IArticlesContext
    {

        Dictionary<int, Article> map = new Dictionary<int, Article>()
        {
            {0, new Article(0, "Football Dictionary", 55.99, new DateTime(2033,3,22), Category.Sport) }
        };

        public void AddArticle(Article article)
        {

            int nextId;
            if (map.Count() == 0)
            {
                nextId = 0;
            }
            else
            {
                nextId = map.Keys.Max(k => k) + 1;
            }
            article.Id = nextId;
            map.Add(nextId, article);
        }

        public Article GetArticle(int id)
        {
            return map.GetValueOrDefault(id);
        }

        public List<Article> GetArticles()
        {
            return map.Values.ToList();
        }

        public void RemoveArticle(int id)
        {
            Article articleToRemove = map.GetValueOrDefault(id);
            if (articleToRemove != null)
                map.Remove(articleToRemove.Id);
        }

        public void UpdateArticle(Article article)
        {
            Article articleToUpdate = map.GetValueOrDefault(article.Id);
            map[article.Id] = article;
        }
    }
}
