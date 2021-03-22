using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManager.Application.Students.Commands.CreateStudent;
using SchoolManager.Application.Students.Commands.DeleteStudent;
using SchoolManager.Application.Students.Commands.UpdateStudent;
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
        /// <summary>
        ///     Returns all students from class with given id
        /// </summary>
        /// <param name="classId"></param>
        /// <response code="200">Success</response>
        /// <response code="404">Class not found</response>
        /// <response code="500">Server Error</response>
        [HttpGet]
        public async Task<IActionResult> GetAllStudents(int classId)
        {
            return Ok(await _mediator.Send(new GetAllStudentsQuery{ ClassId = classId }));
        }

        /// <summary>
        ///     Returns student with given studentId from class with given classId
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="studentId"></param>
        /// <response code="200">Success</response>
        /// <response code="404">Class or student not found</response>
        /// <response code="500">Server Error</response>
        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetStudentById(int classId, int studentId)
        {
            return Ok(await _mediator.Send(new GetStudentByIdQuery { ClassId = classId, StudentId = studentId }));
        }

        /// <summary>
        ///     Creates student in class with given id.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="classId"></param>
        /// <remarks>
        ///   Sample request:
        ///
        ///     POST /api/classes/{classId}/students
        ///     {
        ///         "firstName": "Bobby",
        ///         "lastName: "Fischer",
        ///         "pesel: "012345678910",
        ///         "dateOfBirth": "1966-10-02",
        ///         "phoneNumber": "501505101         
        ///     }
        ///     
        /// ClassID is not required
        /// </remarks>
        /// <response code="201">Success created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Class not found</response>
        /// <response code="500">Server Error</response>
        [HttpPost]
        public async Task<IActionResult> CreateStudent(int classId,[FromBody] CreateStudentCommand command)
        {
            command.ClassId = classId;
            var studentId = await _mediator.Send(command);
            return Created($"api/classes/{classId}/students/{studentId}", null);
        }

        /// <summary>
        ///     Deletes student with given studentId in class with given classId.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="classId"></param>
        /// <response code="204">Success deleted</response>
        /// <response code="404">Class or student not found</response>
        /// <response code="500">Server Error</response>
        [HttpDelete("{studentId}")]
        public async Task<IActionResult> DeleteStudent(int classId, int studentId)
        {
            await _mediator.Send(new DeleteStudentCommand { ClassId = classId, StudentId = studentId});
            return NoContent();
        }

        /// <summary>
        ///     Updates student with given studentId in class with given classId.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="classId"></param>
        /// <param name="command"></param>
        /// <remarks>
        ///   Sample request:
        ///
        ///     POST /api/classes/{classId}/students/{studentId}
        ///     {
        ///         "firstName": "Bobby",
        ///         "lastName: "Fischer",
        ///         "pesel: "012345678910",
        ///         "dateOfBirth": "1966-10-02",
        ///         "phoneNumber": "501505101         
        ///     }
        ///     
        /// ClassID is not required
        /// </remarks>
        /// <response code="200">Success Updated</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Class or student not found</response>
        /// <response code="500">Server Error</response>
        [HttpPut("{studentId}")]
        public async Task<IActionResult> UpdateStudent(int classId, int studentId, [FromBody]UpdateStudentCommand command)
        {
            command.ClassId = classId;
            command.StudentId = studentId;
            await _mediator.Send(command);
            return Ok();
        }

    }
}
