namespace OnlineStoreWeb.Areas.Common.Controllers;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

[Area("common")]
public class ErrorController : Controller
{
    private readonly ILogger<ErrorController> _logger;

    public ErrorController(ILogger<ErrorController> logger)
    {
        _logger = logger;
    }
    
    [Route("error/404")]
    public IActionResult HandlePageNotFound()
    {
        return View("404");
    }

    [Route("error/500")]
    public IActionResult HandleServerError()
    {
        var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
        Exception? exception = exceptionHandlerPathFeature?.Error;

        if (exception != null)
        {
            _logger.LogError(exception, "500 Error occurred. Path: {Path}", exceptionHandlerPathFeature?.Path);
        }
        else
        {
            _logger.LogError("500 Error occurred. Path: {Path}", HttpContext.Request.Path);
        }

        return View("500");
    }

    [Route("error/{statusCode}")]
    public IActionResult HandleError(int statusCode)
    {
        _logger.LogWarning("Error occurred with status code {StatusCode}. Path: {Path}", statusCode, HttpContext.Request.Path);
        return View("error");
    }
}
