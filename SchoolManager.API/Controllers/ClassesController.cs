using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManager.Application.Classes.Commands.CreateClass;
using SchoolManager.Application.Classes.Commands.DeleteClass;
using SchoolManager.Application.Classes.Commands.UpdateClass;
using SchoolManager.Application.Classes.Queries.GetAllClasses;
using SchoolManager.Application.Classes.Queries.GetClassById;
using SchoolManager.Domain.Enums;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

       
        [SwaggerOperation(Summary = "Returns all classes"]
        [SwaggerResponse(200,"Success")]
        [SwaggerResponse(500,"Server Error")]
        [HttpGet]
        public async Task<IActionResult> GetAllClasses()
        {
            return Ok(await _mediator.Send(new GetAllClassesQuery()));
        }

        [SwaggerOperation(Summary = "Returns class with given id")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Server Error")]
        [HttpGet("{classId}")]
        public async Task<IActionResult> GetClassById([FromRoute] int classId)
        {
            return Ok(await _mediator.Send(new GetClassByIdQuery() { ClassId = classId }));
        }

        [SwaggerOperation(Summary = "Creates a class")]
        [SwaggerResponse(201, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Server Error")]
        [SwaggerRequestBody(Description = "Name: 1aT", Required = true)]

        [HttpPost]
        public async Task<IActionResult> CreateClass([FromBody] CreateClassCommand command)
        {
            var classId = await _mediator.Send(command);
            return Created($"api/classes/{classId}", null);
        }
        [HttpDelete("{classId}")]
        public async Task<IActionResult> DeleteClass(int classId)
        {
            await _mediator.Send(new DeleteClassCommand { ClassId = classId });
            return NoContent();
        }

        [HttpPut("{classId}")]
        public async Task<IActionResult> UpdateClass(int classId, UpdateClassCommand command)
        {
            await _mediator.Send(new UpdateClassCommand() { ClassId = classId, Name = command.Name, Type = command.Type});
            return Ok();
        }




    }
}
