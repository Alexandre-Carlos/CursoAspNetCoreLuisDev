using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetProjectById
{
    public class GetProjectsByIdQueryHandler : IRequestHandler<GetProjectsByIdQuery, ProjectDetailsViewModel>
    {
        private readonly IProjectRepository _projectRepository;

        public GetProjectsByIdQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ProjectDetailsViewModel> Handle(GetProjectsByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);

            if (project == null) return null;

            var projectDetailsViewModel = new ProjectDetailsViewModel(
                    project.Id,
                    project.Title,
                    project.Description,
                    project.TotalCost,
                    project.StartedAt,
                    project.FinishedAt,
                    project.Client.FullName,
                    project.Freelancer.FullName
                );
            return projectDetailsViewModel;
        }
    }
}
