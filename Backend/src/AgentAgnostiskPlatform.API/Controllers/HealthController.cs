using Microsoft.AspNetCore.Mvc;
using AgentAgnostiskPlatform.API.Controllers.Shared;

namespace AgentAgnostiskPlatform.API.Controllers;

/// <summary>
///    API Health Check Handling
/// </summary>
public class HealthController : AppControllerBase
{
    /// <summary>
    ///     API Health Check
    /// </summary>
    [HttpGet, Route("")]
    [ProducesResponseType(typeof(string), 200)]
    public async Task<IActionResult> GetHealthAsync()
    {
        return await Task.FromResult(Ok("API is healthy"));
    }
}