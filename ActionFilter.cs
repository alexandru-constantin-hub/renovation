using Microsoft.AspNetCore.Mvc.Filters;

namespace Renovation
{
    public class ActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // our code before action executes
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // our code after action executes
        }
    }
}
