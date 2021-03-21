using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManager.Application.Classes.Commands.CreateClass;
using SchoolManager.Application.Classes.Commands.DeleteClass;
using SchoolManager.Application.Classes.Queries.GetAllClasses;
using SchoolManager.Application.Classes.Queries.GetClassById;
using SchoolManager.Domain.Enums;
using System;
using System.Collections.Generic;
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

        [HttpGet]
        public async Task<IActionResult> GetAllClasses([FromQuery]string type)
        {
            return Ok(await _mediator.Send(new GetAllClassesQuery()));
        }

        [HttpGet("{classId}")]
        public async Task<IActionResult> GetClassById([FromRoute] int classId)
        {
            return Ok(await _mediator.Send(new GetClassByIdQuery() { ClassId = classId }));
        }

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



    }
}
