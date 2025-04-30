using ECommerce.API.Attributes;
using ECommerce.API.Helper;
using ECommerce.Application.Interfaces;
using ECommerce.Shared.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace ECommerce.API.Filters
{
    public class ExecutionFilter : IActionFilter, IAsyncActionFilter, IExceptionFilter
    {
        private readonly ILogger<ExecutionFilter> _logger;
        private readonly IDBService _dbService;

        public ExecutionFilter(ILogger<ExecutionFilter> logger, IDBService dbService)
        {
            _logger = logger;
            _dbService = dbService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            _logger.LogInformation($"[LOG] Starting execution of '{actionName}'.");

            // 1. Check for null parameters
            foreach (var param in context.ActionArguments)
            {
                if (param.Value == null)
                {
                    context.Result = new BadRequestObjectResult(new ResponseStructure
                    {
                        Success = false,
                        Error = $"Parameter '{param.Key}' cannot be null."
                    });

                    _logger.LogWarning($"[WARN] Validation failed for parameter '{param.Key}'.");
                    return;
                }
            }

            // 2. Check for ModelState errors
            if (!context.ModelState.IsValid)
            {
                var error = context.ModelState
                    .Where(ms => ms.Value!.Errors.Any())
                    .Select(ms => ms.Value?.Errors.First().ErrorMessage)
                    .FirstOrDefault();

                context.Result = new BadRequestObjectResult(new ResponseStructure
                {
                    Success = false,
                    Error = error ?? "Validation failed."
                });

                _logger.LogWarning($"[WARN] ModelState validation failed: {error}");
            }
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var isGetMethod = context.HttpContext.Request.Method == HttpMethods.Get;
            var dbBypass = context.ActionDescriptor is ControllerActionDescriptor cad && cad.MethodInfo.IsDefined(typeof(BypassDbTransaction), inherit: true);

            if (!isGetMethod || !dbBypass)
            {
                await using (_dbService.Begin())
                {
                    var resultContext = await next();
                }
            }
            else
            {
                await next();
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;

            if (context.Exception == null)
            {
                _logger.LogInformation($"[LOG] Action '{actionName}' completed successfully.");
            }
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var actionName = context.ActionDescriptor.DisplayName;

            var allErrorMessages = new StringBuilder();

            while (exception != null)
            {
                allErrorMessages.AppendLine(exception.Message);
                exception = exception.InnerException;
            }

            string finalErrorMessage = allErrorMessages.ToString();

            _logger.LogError($"[ERROR] Exception in action '{actionName}': {finalErrorMessage}");

            var response = new ResponseStructure
            {
                Success = false,
                Error = finalErrorMessage
            };

            context.Result = new JsonResult(response)
            {
                StatusCode = Convert.ToInt32(ExceptionHelper.GetStatusCode(context.Exception))
            };

            context.ExceptionHandled = true;
        }
    }
}