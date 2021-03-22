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

        /// <summary>
        ///     Returns teacher from class with given id.
        /// </summary>
        /// 
        /// <param name="classId"></param>
        /// <response code="200">Success</response>
        /// <response code="404">Class not found</response>
        /// <response code="500">Server Error</response>
        [HttpGet]
        public async Task<IActionResult> GetTeacher(int classId)
        {
            return Ok(await _mediator.Send(new GetTeacherQuery { ClassId = classId }));
        }

        /// <summary>
        ///     Creates teacher in class with given id.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="classId"></param>
        /// <remarks>
        ///   Sample request:
        ///
        ///     POST /api/classes/{classId}/teacher
        ///     {
        ///         "firstName": "Magnus",
        ///         "lastName: "Carlsen",
        ///         "pesel: "012345678910",
        ///         "dateOfBirth": "1980-10-02",
        ///         "phoneNumber": "501505101 "        
        ///     }
        ///     
        /// ClassID is not required
        /// </remarks>
        /// <response code="201">Success created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Class not found</response>
        /// <response code="500">Server Error</response>
        [HttpPost]
        public async Task<IActionResult> CreateTeacher(int classId, [FromBody] CreateTeacherCommand command)
        {
            command.ClassId = classId;
            await _mediator.Send(command);
            return Created($"api/classes/{classId}/teacher", null);
        }

        /// <summary>
        ///     Deletes teacher in class with given id.
        /// </summary>
        /// <param name="classId"></param>
        /// <response code="204">Success deleted</response>
        /// <response code="404">Class or teacher not found</response>
        /// <response code="500">Server Error</response>
        [HttpDelete]
        public async Task<IActionResult> DeleteTeacher(int classId)
        {
            await _mediator.Send(new DeleteTeacherCommand { ClassId = classId});
            return NoContent();
        }

        /// <summary>
        ///     Updates teacher in class with given id.
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
        /// <response code="200">Success updated</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Class or teacher not found</response>
        /// <response code="500">Server Error</response>
        [HttpPut]
        public async Task<IActionResult> UpdateTeacher(int classId, [FromBody] UpdateTeacherCommand command)
        {
            command.ClassId = classId;
            await _mediator.Send(command);
            return Ok();
        }

    }
}
