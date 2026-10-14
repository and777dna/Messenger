using Microsoft.AspNetCore.Mvc.Filters;

namespace Messenger.Controllers.Filters;

public class IdempotencyFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if(context.Filters.Contains())
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        throw new NotImplementedException();
    }
}