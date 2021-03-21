using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManager.Application.Students.Commands.CreateStudent;
using SchoolManager.Application.Students.Queries.GetAllStudents;
using SchoolManager.Application.Students.Queries.GetStudentById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManager.API.Controllers
{
    [Route("api/classes/{classId}/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllStudents(int classId)
        {
            return Ok(await _mediator.Send(new GetAllStudentsQuery{ ClassId = classId }));
        }
        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetStudentById(int classId, int studentId)
        {
            return Ok(await _mediator.Send(new GetStudentByIdQuery { ClassId = classId, StudentId = studentId }));
        }
        [HttpPost]
        public async Task<IActionResult> CreateStudent(int classId,[FromBody] CreateStudentCommand command)
        {
            var studentId = await _mediator.Send(command);

            return Created($"api/classes/{classId}/students/{studentId}", null);

        }
    }
}
