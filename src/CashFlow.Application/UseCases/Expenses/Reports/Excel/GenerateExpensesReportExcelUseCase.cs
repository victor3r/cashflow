using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Expenses.Reports.Excel;
public class GenerateExpensesReportExcelUseCase(IExpensesReadOnlyRepository repository) : IGenerateExpensesReportExcelUseCase
{
    private readonly IExpensesReadOnlyRepository _repository = repository;
    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await _repository.FilterByMonth(month);

        if (expenses.Count == 0)
        {
            return [];
        }

        using var workbook = new XLWorkbook();

        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Arial";

        var worksheet = workbook.Worksheets.Add(month.ToString("Y"));

        InsertHeader(worksheet);

        using var file = new MemoryStream();
        workbook.SaveAs(file);

        return file.ToArray();
    }

    private static void InsertHeader(IXLWorksheet worksheet)
    {
        var currentRow = 1;

        worksheet.Cell(currentRow, 1).Value = ResourceReportGenerationMessages.TITLE;
        worksheet.Cell(currentRow, 2).Value = ResourceReportGenerationMessages.DATE;
        worksheet.Cell(currentRow, 3).Value = ResourceReportGenerationMessages.PAYMENT_TYPE;
        worksheet.Cell(currentRow, 4).Value = ResourceReportGenerationMessages.AMOUNT;
        worksheet.Cell(currentRow, 5).Value = ResourceReportGenerationMessages.DESCRIPTION;

        var headerRange = worksheet.Range(currentRow, 1, currentRow, 5);

        headerRange.Style.Font.Bold = true;
        headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#f5c2b6");
        headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        worksheet.Cell(currentRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
    }
}
