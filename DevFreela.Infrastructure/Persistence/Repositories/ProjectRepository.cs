using Dapper;
using DevFreela.Core.DTOs;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public ProjectRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return  await _dbContext.Projects.ToListAsync();
        }

        public async Task<Project> GetDetailsByIdAsync(int id)
        {
            var project = await _dbContext.Projects
               .Include(p => p.Client)
               .Include(p => p.Freelancer)
               .SingleOrDefaultAsync(p => p.Id == id);

            return project;
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Project project)
        {
            await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var project = await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == id);

            project.Cancel();
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {

            await _dbContext.SaveChangesAsync();
        }

        public async Task StartAsync(Project project)
        {
            await _dbContext.SaveChangesAsync();

            /*using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "UPDATE Project SET Status = @status, StartedAt = @startedat WHERE Id = @id";

                await sqlConnection.ExecuteAsync(script, new { status = project.Status, startedat = project.StartedAt, id = project.Id});
            }*/
        }

        public async Task FinishAsync(Project project)
        {
            await _dbContext.SaveChangesAsync();

            /*using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "UPDATE Project SET Status = @status, finishedat = @finishedat WHERE Id = @id";

                await sqlConnection.ExecuteAsync(script, new { status = project.Status, finishedat = project.FinishedAt, id = project.Id });
            }*/
        }

        public async Task AddCommentAsync(ProjectComment comment)
        {
            await _dbContext.ProjetComments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
        }
    }
}
