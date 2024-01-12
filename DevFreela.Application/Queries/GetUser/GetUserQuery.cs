using DevFreela.Application.ViewModels;
using DevFreela.Core.DTOs;
using MediatR;

namespace DevFreela.Application.Queries.GetUser
{
    public class GetUserQuery : IRequest<UserDTO>
    {
        public GetUserQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
