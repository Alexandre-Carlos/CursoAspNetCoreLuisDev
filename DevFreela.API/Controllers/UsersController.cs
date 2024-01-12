

using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.LoginUser;
using DevFreela.Application.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{

    [Route("api/users")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        //private readonly IUserService _userService;

        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
            //_userService = userService;
        }



        //api/users/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            //var user = _userService.GetById(id);

            var getUserQuery = new GetUserQuery(id);

            var user = await _mediator.Send(getUserQuery);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        //api/users
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            // var id = _userService.Create(inputModel);

            /* Criado filter para validação
            if (!ModelState.IsValid)
            {
                var messages = ModelState
                    .SelectMany(ms => ms.Value.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(messages);
            }
            */
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = id }, command);
        }

        //api/users/login
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var loginUserViewModel = await _mediator.Send(command);

            if (loginUserViewModel == null)
                return BadRequest();

            return Ok(loginUserViewModel);
        }


    }
}
