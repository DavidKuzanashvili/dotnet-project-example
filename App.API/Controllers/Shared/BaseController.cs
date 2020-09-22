using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace App.API.Controllers.Shared
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected string LangCode => HttpContext.Request.Headers["Accept-Language"];

        protected ILogger<BaseController> Logger =>
            HttpContext.RequestServices.GetService<ILogger<BaseController>>();
    }
}
