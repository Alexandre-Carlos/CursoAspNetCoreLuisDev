using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace DevFreela.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public UserService(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public int Create(NewUserInputModel inputModel)
        {
            var user = new User(inputModel.FullName, inputModel.Email, inputModel.BirthDate, inputModel.Password, inputModel.Role);

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user.Id;
        }

        public UserDetailsViewModel GetById(int id)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == id);

            if (user == null)
            {
                return null;
            }

            var userDetailsViewModel = new UserDetailsViewModel(user.Id, user.FullName, user.Email, user.BirthDate);

            return userDetailsViewModel;
        }
    }
}
