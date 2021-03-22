using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManager.Application.Classes.Commands.CreateClass;
using SchoolManager.Application.Classes.Commands.DeleteClass;
using SchoolManager.Application.Classes.Commands.UpdateClass;
using SchoolManager.Application.Classes.Queries.GetAllClasses;
using SchoolManager.Application.Classes.Queries.GetClassById;
using System.Threading.Tasks;

namespace SchoolManager.API.Controllers
{
    [Route("api/classes")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClassesController(IMediator mediator)
        {
            _mediator = mediator;
        }

       
        /// <summary>
        /// Returns all classes.
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="500">Server Error</response>
        [HttpGet]
        public async Task<IActionResult> GetAllClasses()
        {
            return Ok(await _mediator.Send(new GetAllClassesQuery()));
        }

        /// <summary>
        ///     Returns class with given id.
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="404">Class not found</response>
        /// <response code="500">Server Error</response>
        [HttpGet("{classId}")]
        public async Task<IActionResult> GetClassById([FromRoute] int classId)
        {
            return Ok(await _mediator.Send(new GetClassByIdQuery() { ClassId = classId }));
        }


        /// <summary>
        ///     Creates class.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/classes/
        ///     {
        ///         "Name": "1aT",
        ///         "Type": 2
        ///     }
        ///     
        /// Type:
        /// 1 - Liceum
        /// 2 - Technikum
        /// </remarks>
        /// <response code="201">Success created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Server Error</response>
        [HttpPost]
        public async Task<IActionResult> CreateClass([FromBody] CreateClassCommand command)
        {
            var classId = await _mediator.Send(command);
            return Created($"api/classes/{classId}", null);
        }

        /// <summary>
        /// Deletes class with given id.
        /// </summary>
        /// <response code="204">Success deleted</response>
        /// <response code="404">Class not found</response>
        /// <response code="500">Server error</response>
        [HttpDelete("{classId}")]
        public async Task<IActionResult> DeleteClass(int classId)
        {
            await _mediator.Send(new DeleteClassCommand { ClassId = classId });
            return NoContent();
        }

        /// <summary>
        ///     Updates class with given id.
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="command"></param>
        /// <remarks>
        ///   Sample request:
        ///
        ///     POST /api/classes/{classId}
        ///     {
        ///         "Name": "1aT",
        ///         "Type": 2
        ///     }
        ///     
        /// Type:
        /// 1 - Liceum
        /// 2 - Technikum
        /// </remarks>
        /// <response code="200">Success updated</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Class not found</response>
        /// <response code="500">Server Error</response>
        [HttpPut("{classId}")]
        public async Task<IActionResult> UpdateClass(int classId, UpdateClassCommand command)
        {
            await _mediator.Send(new UpdateClassCommand() { ClassId = classId, Name = command.Name, Type = command.Type});
            return Ok();
        }




    }
}
