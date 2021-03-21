using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SchoolManager.Application.Common.Exceptions;
using System;
using System.Collections.Generic;

namespace SchoolManager.API.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public ApiExceptionFilterAttribute()
        {
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                {typeof(NotFoundException), HandleNotFoundException },
                {typeof(ValidationException), HandleValidationException }
            };
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);
            base.OnException(context); 
        }

        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();

            if(_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            HandleUnknownException(context);
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            context.Result = new ObjectResult(context.Exception.Message)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            context.Result = new NotFoundObjectResult(context.Exception.Message);
            context.ExceptionHandled = true;
        }

        private void HandleValidationException(ExceptionContext context)
        {
            context.Result = new BadRequestObjectResult(context.Exception.Message);
            context.ExceptionHandled = true;
        }

    }
}
