using DevFreela.Application.Queries.GeAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllProjectsCommandHandlerTests
    {
        [Fact]
        public async Task ThreeProjectsExist_Execute_ReturnThreeProjectViewModels()
        {
            //Arrange
            var projects = new List<Project>
            {
                new Project("Nome do teste 1", "Descricao de teste 1", 1, 2, 10000),
                new Project("Nome do teste 2", "Descricao de teste 2", 1, 2, 20000),
                new Project("Nome do teste 3", "Descricao de teste 3", 1, 2, 30000)
            };

            var projectRepositorMock = new Mock<IProjectRepository>();
            projectRepositorMock.Setup(pr => pr.GetAllAsync().Result).Returns(projects);

            var getAllProjectsQuery = new GetAllProjectsQuery("");
            var getAllProjectQueryHandler = new GetAllProjectsQueryHandler(projectRepositorMock.Object);

            //Act
            var projectViewModelList = await getAllProjectQueryHandler.Handle(getAllProjectsQuery, new CancellationToken());

            //Assert
            Assert.NotNull(projectViewModelList);
            Assert.NotEmpty(projectViewModelList);
            Assert.Equal(projects.Count, projectViewModelList.Count);

            projectRepositorMock.Verify(pr => pr.GetAllAsync(), Times.Once);
        }
    }
}
