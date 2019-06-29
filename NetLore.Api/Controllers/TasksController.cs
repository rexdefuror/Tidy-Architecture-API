using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetLore.Application.Read.Tasks;
using NetLore.Application.Write.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetLore.Api.Controllers
{
    /// <summary>
    /// Tasks controller.
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Creates a new instance of <see cref="TasksController"/>.
        /// </summary>
        /// <param name="mediator">The request mediator.</param>
        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets all tasks.
        /// </summary>
        /// <returns>A list of tasks <see cref="Domain.Models.Task"/></returns>
        [HttpGet]
        [SwaggerResponse(200, "Successfully retrieved data.", typeof(IEnumerable<Domain.Models.Task>))]
        [SwaggerResponse(204, "Request successful, no data found.")]
        [SwaggerResponse(400, "Invalid request or data.", typeof(IEnumerable<ValidationFailure>))]
        public async Task<IEnumerable<Domain.Models.Task>> Get()
        {
            return await _mediator.Send(new FindAllTasksRequest());
        }

        /// <summary>
        /// Gets the specific task by identifier.
        /// </summary>
        /// <param name="id">The task identifier.</param>
        /// <returns>A specific task <see cref="Domain.Models.Task"/></returns>
        [HttpGet("{id}")]
        [SwaggerResponse(200, "Successfully retrieved data.", typeof(Domain.Models.Task))]
        [SwaggerResponse(204, "Request successful, no data found.")]
        [SwaggerResponse(400, "Invalid request or data.", typeof(IEnumerable<ValidationFailure>))]
        public async Task<Domain.Models.Task> Get(int id)
        {
            return await _mediator.Send(new FindTaskByIdRequest(id));
        }

        /// <summary>
        /// Creates a new task.
        /// </summary>
        /// <param name="model">A task model sent through request body.</param>
        /// <returns>This is a command it does not send a modeled response.</returns>
        [HttpPost]
        [SwaggerResponse(200, "Successfully saved data.", typeof(Domain.Models.Task))]
        [SwaggerResponse(400, "Invalid request or data.", typeof(IEnumerable<ValidationFailure>))]
        public async Task Create([FromBody] Domain.Models.Task model)
        {
            await _mediator.Send(new SaveTaskRequest(model));
        }

        /// <summary>
        /// Updates the specified task.
        /// </summary>
        /// <param name="id">The task identifier.</param>
        /// <param name="model">A task model to be updated to.</param>
        /// <returns>This is a command it does not send a modeled response.</returns>
        [HttpPut]
        [SwaggerResponse(200, "Successfully saved data.", typeof(Domain.Models.Task))]
        [SwaggerResponse(400, "Invalid request or data.", typeof(IEnumerable<ValidationFailure>))]
        [SwaggerResponse(404, "Entity not found.", typeof(IEnumerable<ValidationFailure>))]
        public async Task Update(int id, [FromBody] Domain.Models.Task model)
        {
            await _mediator.Send(new UpdateTaskRequest(id, model));
        }

        /// <summary>
        /// Removes the specified task.
        /// </summary>
        /// <param name="id">The task identifier.</param>
        /// <returns>This is a command it does not send a modeled response.</returns>
        [HttpDelete("{id}")]
        [SwaggerResponse(200, "Successfully saved data.", typeof(Domain.Models.Task))]
        [SwaggerResponse(400, "Invalid request or data.", typeof(IEnumerable<ValidationFailure>))]
        [SwaggerResponse(404, "Entity not found.", typeof(IEnumerable<ValidationFailure>))]
        public async Task Remove(int id)
        {
            await _mediator.Send(new DeleteTaskRequest(id));
        }
    }
}