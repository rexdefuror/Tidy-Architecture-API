using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetLore.Application.Read.TaskLists;
using NetLore.Application.Write.TaskLists;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetLore.Api.Controllers
{
    /// <summary>
    /// Task list controller.
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [Route("api/list")]
    [ApiController]
    public class TaskListsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Creates a new instance of <see cref="TaskListsController"/>.
        /// </summary>
        /// <param name="mediator">The request mediator.</param>
        public TaskListsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets all tasks.
        /// </summary>
        /// <returns>A list of tasks <see cref="Domain.Models.Task"/></returns>
        [HttpGet]
        [SwaggerResponse(200, "Successfully retrieved data.", typeof(IEnumerable<Domain.Models.TaskList>))]
        [SwaggerResponse(204, "Request successful, no data found.")]
        [SwaggerResponse(400, "Invalid request or data.", typeof(IEnumerable<ValidationFailure>))]
        public async Task<IEnumerable<Domain.Models.TaskList>> Get()
        {
            return await _mediator.Send(new FindAllTaskListsRequest());
        }

        /// <summary>
        /// Gets the specific task list by identifier.
        /// </summary>
        /// <param name="id">The task list identifier.</param>
        /// <returns>A specific task list <see cref="Domain.Models.TaskList"/></returns>
        [HttpGet("{id}")]
        [SwaggerResponse(200, "Successfully retrieved data.", typeof(Domain.Models.TaskList))]
        [SwaggerResponse(204, "Request successful, no data found.")]
        [SwaggerResponse(400, "Invalid request or data.", typeof(IEnumerable<ValidationFailure>))]
        public async Task<Domain.Models.TaskList> Get(int id)
        {
            return await _mediator.Send(new FindTaskListByIdRequest(id));
        }

        /// <summary>
        /// Creates a new task list.
        /// </summary>
        /// <param name="model">A task model sent through request body.</param>
        /// <returns>This is a command it does not send a modeled response.</returns>
        [HttpPost]
        [SwaggerResponse(200, "Successfully saved data.", typeof(Domain.Models.TaskList))]
        [SwaggerResponse(400, "Invalid request or data.", typeof(IEnumerable<ValidationFailure>))]
        public async Task Create([FromBody] Domain.Models.TaskList model)
        {
            await _mediator.Send(new SaveTaskListRequest(model));
        }

        /// <summary>
        /// Updates the specified task list.
        /// </summary>
        /// <param name="id">The task list identifier.</param>
        /// <param name="model">A task list model to be updated to.</param>
        /// <returns>This is a command it does not send a modeled response.</returns>
        [HttpPut]
        [SwaggerResponse(200, "Successfully saved data.", typeof(Domain.Models.TaskList))]
        [SwaggerResponse(400, "Invalid request or data.", typeof(IEnumerable<ValidationFailure>))]
        [SwaggerResponse(404, "Entity not found.", typeof(IEnumerable<ValidationFailure>))]
        public async Task Update(int id, [FromBody] Domain.Models.TaskList model)
        {
            await _mediator.Send(new UpdateTaskListRequest(id, model));
        }

        /// <summary>
        /// Removes the specified task list.
        /// </summary>
        /// <param name="id">The task list identifier.</param>
        /// <returns>This is a command it does not send a modeled response.</returns>
        [HttpDelete("{id}")]
        [SwaggerResponse(200, "Successfully saved data.", typeof(Domain.Models.TaskList))]
        [SwaggerResponse(400, "Invalid request or data.", typeof(IEnumerable<ValidationFailure>))]
        [SwaggerResponse(404, "Entity not found.", typeof(IEnumerable<ValidationFailure>))]
        public async Task Remove(int id)
        {
            await _mediator.Send(new DeleteTaskListRequest(id));
        }
    }
}