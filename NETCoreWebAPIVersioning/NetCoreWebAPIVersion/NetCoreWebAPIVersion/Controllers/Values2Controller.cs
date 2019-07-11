using Microsoft.AspNetCore.Mvc;

namespace NetCoreWebAPIVersion.ControllersV2
{
    [ApiController]
    [ApiVersion("0.1", Deprecated =true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class Values3Controller : ControllerBase
    {
        [HttpGet]
        public string Get(ApiVersion apiVersion) => $"Controller = {GetType().Name}\nVersion = {apiVersion}";
    }

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public string Get(ApiVersion apiVersion) => $"Controller = {GetType().Name}\nVersion = {apiVersion}";
    }

    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/values")]
    public class Values2Controller : ControllerBase
    {
        [HttpGet]
        public string Get(ApiVersion apiVersion) => $"Controller = {GetType().Name}\nVersion = {apiVersion}";
    }

    [ApiVersionNeutral]
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        public string Get() => "OK";
    }
}