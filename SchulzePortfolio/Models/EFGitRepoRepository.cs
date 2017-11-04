using System;
using System.Linq;

namespace SchulzePortfolio.Models
{
    public class EFContactRepository : IGitRepoRepository
    {
        SchulzePortfolioDbContext db;

        public EFContactRepository(SchulzePortfolioDbContext connection = null)
        {
            if (connection == null)
            {
                this.db = new SchulzePortfolioDbContext();
            }
            else
            {
                this.db = connection;
            }
        }

        public IQueryable<GitRepo> GitRepos
        {
            get { return db.GitRepos; }
        }

        public GitRepo Edit(GitRepo gitRepo)
        {
            db.Contacts.Add();
            db.SaveChanges();
            return gitRepo;
        }

        public void Remove(GitRepo gitRepo)
        {
            db.GitRepo.Remove(gitRepo);
            db.SaveChanges();
        }

        public GitRepo Save(GitRepo gitRepo)
        {
            db.Contacts.Add(gitRepo);
            db.SaveChanges();
            return gitRepo;
        }
    }
}