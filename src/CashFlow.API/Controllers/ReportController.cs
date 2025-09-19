using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CashFlow.API.Controllers;

[Route("[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    [HttpGet("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel()
    {
        var file = new byte[1];

        if (file.Length == 0)
            return NoContent();

        return File(file, MediaTypeNames.Application.Octet, "report.xlsx");
    }
}
