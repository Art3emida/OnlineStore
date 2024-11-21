namespace OnlineStoreWeb.Configuration;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineStore.Application.Exceptions.Common;

public class ControllerExceptionFilter: IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is NotFoundException)
        {
            context.Result = new NotFoundResult();
            context.ExceptionHandled = true;
        }
    }
}
