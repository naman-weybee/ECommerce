using ECommerce.Shared.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace ECommerce.API.Filters
{
    public class ExecutionFilter : IActionFilter, IExceptionFilter
    {
        private readonly ILogger<ExecutionFilter> _logger;

        public ExecutionFilter(ILogger<ExecutionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            _logger.LogInformation($"[LOG] Starting execution of '{actionName}'.");

            foreach (var param in context.ActionArguments)
            {
                if (param.Value == null)
                {
                    context.Result = new BadRequestObjectResult(new ResponseStructure
                    {
                        success = false,
                        error = $"Parameter '{param.Key}' cannot be null."
                    });

                    _logger.LogWarning($"[WARN] Validation failed for parameter '{param.Key}'.");
                    return;
                }
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
                success = false,
                error = finalErrorMessage
            };

            context.Result = new JsonResult(response)
            {
                StatusCode = 500
            };

            context.ExceptionHandled = true;
        }
    }
}