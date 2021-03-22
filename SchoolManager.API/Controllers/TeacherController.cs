using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManager.Application.Methods.Teacher.Commands.CreateTeacher;
using SchoolManager.Application.Methods.Teacher.Commands.DeleteTeacher;
using SchoolManager.Application.Methods.Teacher.Commands.UpdateTeacher;
using SchoolManager.Application.Methods.Teacher.Queries.GetTeacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManager.API.Controllers
{
    [Route("api/classes/{classId}/teacher")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeacherController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetTeacher(int classId)
        {
            return Ok(await _mediator.Send(new GetTeacherQuery { ClassId = classId }));
        }
        [HttpPost]
        public async Task<IActionResult> CreateTeacher(int classId, [FromBody] CreateTeacherCommand command)
        {
            command.ClassId = classId;
            await _mediator.Send(command);
            return Created($"api/classes/{classId}/teacher", null);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(int classId)
        {
            await _mediator.Send(new DeleteTeacherCommand { ClassId = classId});
            return NoContent();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateStudent(int classId, [FromBody] UpdateTeacherCommand command)
        {
            command.ClassId = classId;
            await _mediator.Send(command);
            return Ok();
        }

    }
}
