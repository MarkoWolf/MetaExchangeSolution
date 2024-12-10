using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    [Route("")]
    public IActionResult GetHealth()
    {
        return Ok(new
        {
            status = "Healthy",
            timestamp = DateTime.UtcNow
        });
    }
    
    [HttpGet]
    [Route("details")]
    public IActionResult GetDetailedHealth()
    {
       
        var dbConnectionHealthy = true; 
        var cacheHealthy = true;

        return Ok(new
        {
            status = "Healthy",
            dependencies = new
            {
                database = dbConnectionHealthy ? "Healthy" : "Unhealthy",
                cache = cacheHealthy ? "Healthy" : "Unhealthy"
            },
            timestamp = DateTime.UtcNow,
            server = Environment.MachineName,
            uptime = GetUptime()
        });
    }

    private string GetUptime()
    {
        using (var process = Process.GetCurrentProcess())
        {
            var uptime = DateTime.UtcNow - process.StartTime.ToUniversalTime();
            return uptime.ToString("c"); // c = Standardformat für Zeitspanne
        }
    }
}