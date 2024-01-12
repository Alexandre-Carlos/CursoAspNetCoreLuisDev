using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteCommand;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GeAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]

    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMediator _mediator;

        public ProjectsController(IProjectService projectService, IMediator mediator)
        {
            _projectService = projectService;
            _mediator = mediator;
        }

        //api/projects?query='net core'
        [HttpGet]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> Get(string query)
        {
            //var projects = _projectService.GetAll(query);

            var getAllProjectQuery = new GetAllProjectsQuery(query);

            var projects = await _mediator.Send(getAllProjectQuery);

            return Ok(projects);
        }

        //api/projects/599
        [HttpGet("{id}")]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> GetById(int id)
        {
            //var project = _projectService.GetById(id);

            var command = new GetProjectsByIdQuery(id);

            var project = await _mediator.Send(command);

            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpPost]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
        {
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

            //var id = _projectService.Create(inputModel);

            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = id }, command);
        }

        //api/projects/2
        [HttpPut("{id}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectCommand command)
        {
            if (command.Description.Length > 200)
            {
                return BadRequest();
            }

            //_projectService.Update(inputModel);

            await _mediator.Send(command);

            return NoContent();
        }

        //api/projects/3 DELETE
        [HttpDelete("{id}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Delete(int id)
        {
            //_projectService.Delete(id);

            var command = new DeleteProjectCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }

        //api/projets/1/comments POST
        [HttpPost("{id}/comments")]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> PostComment(int id, [FromBody] CreateCommentCommand command)
        {
            //_projectService.CreateComment(inputModel);

            await _mediator.Send(command);

            return NoContent();
        }

        //api/projects/1/sart
        [HttpPut("{id}/start")]
        [Authorize(Roles = "client")]
        public IActionResult Start(int id)
        {
            //_projectService.Start(id);  

            var command = new StartProjectCommand(id);
            _mediator.Send(command);

            return NoContent();
        }

        //api/projects/1/finish
        [HttpPut("{id}/finish")]
        [Authorize(Roles = "client")]
        public IActionResult Finish(int id)
        {
            _projectService.Finish(id);

            var command = new FinishProjectCommand(id);
            _mediator.Send(command);

            return NoContent();
        }
    }
}
