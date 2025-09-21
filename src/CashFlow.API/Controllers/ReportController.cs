﻿using CashFlow.Application.UseCases.Expenses.Reports.Excel;
using CashFlow.Application.UseCases.Expenses.Reports.Pdf;
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
    public async Task<IActionResult> GetExcel(
        [FromServices] IGenerateExpensesReportExcelUseCase useCase,
        [FromHeader] DateOnly month)
    {
        var file = await useCase.Execute(month);

        if (file.Length == 0)
            return NoContent();

        return File(file, MediaTypeNames.Application.Octet, "report.xlsx");
    }

    [HttpGet("pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetPdf(
       [FromServices] IGenerateExpensesReportPdfUseCase useCase,
       [FromQuery] DateOnly month)
    {
        var file = await useCase.Execute(month);

        if (file.Length == 0)
            return NoContent();

        return File(file, MediaTypeNames.Application.Pdf, "report.pdf");
    }
}
