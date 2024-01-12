using DevFreela.Application.ViewModels;
using MediatR;
using System.Collections.Generic;

namespace DevFreela.Application.Queries.GeAllProjects
{
    public class GetAllProjectsQuery : IRequest<List<ProjectViewModel>>
    {
        public GetAllProjectsQuery(string query)
        {
            Query = query;
        }
        public string Query { get; private set; }

    }
}
