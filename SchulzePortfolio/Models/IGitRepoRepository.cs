using System.Linq;

namespace SchulzePortfolio.Models
{
    public interface IGitRepoRepository
    {
        IQueryable<GitRepo> GitRepo { get; }
        GitRepo Save(GitRepo gitRepo);
        GitRepo Edit(GitRepo gitRepo);
        void Remove(GitRepo gitRepo);
    }
}