using DevFreela.Core.DTOs;
using DevFreela.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevFreela.Core.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync();
        Task<Project> GetDetailsByIdAsync(int id);

        Task<Project> GetByIdAsync(int id);
        Task AddAsync(Project project);
        Task SaveChangesAsync();
        Task DeleteAsync(int id);
        Task StartAsync(Project project);
        Task FinishAsync(Project project);
        Task AddCommentAsync(ProjectComment comment);
    }
}
