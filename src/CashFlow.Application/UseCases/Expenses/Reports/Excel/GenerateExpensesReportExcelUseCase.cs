using CashFlow.Domain.Reports;
using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Expenses.Reports.Excel;
public class GenerateExpensesReportExcelUseCase : IGenerateExpensesReportExcelUseCase
{
    public async Task<byte[]> Execute(DateOnly month)
    {
        using var workbook = new XLWorkbook();

        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Arial";

        var worksheet = workbook.Worksheets.Add(month.ToString("Y"));

        InsertHeader(worksheet);
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
        headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        headerRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
    }
}
