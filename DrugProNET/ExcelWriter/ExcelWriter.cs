using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace DrugProNET
{
    public class ExcelWriter
    {
        private const string TEMP_PATH = "Temp";
        private const string DEFAULT_FILE_NAME = "spreadsheet.xlsx";

        public static MemoryStream CreateAsStream(List<string> header, List<List<string>> data)
        {
            return new MemoryStream(CreateExcelPackage(header, data).GetAsByteArray());
        }

        public static void CreateInTemp(List<string> header, List<List<string>> data, string fileName = DEFAULT_FILE_NAME)
        {
            ExcelPackage excelPackage = CreateExcelPackage(header, data);

            string path = HttpContext.Current.Server.MapPath(TEMP_PATH + "/" + fileName);
            FileInfo file = new FileInfo(path);

            excelPackage.SaveAs(file);
        }

        private static ExcelPackage CreateExcelPackage(List<string> header, List<List<string>> data)
        {
            ExcelPackage excel = new ExcelPackage();
            excel.Workbook.Worksheets.Add("A");
            ExcelWorksheet worksheet = excel.Workbook.Worksheets["A"];

            if (header != null)
            {
                AddHeader(worksheet, header);
            }

            AddData(worksheet, data);

            // Must be called AFTER filling with data
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            return excel;
        }

        private static void AddHeader(ExcelWorksheet worksheet, List<string> header)
        {
            List<string[]> row = new List<string[]>();
            row.Add(header.ToArray());

            string range = "A1:" + Char.ConvertFromUtf32(row[0].Length + 64) + "1";
            worksheet.Cells[range].LoadFromArrays(row);

            // Style header to make it easier to distinguish from data
            worksheet.Cells[range].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[range].Style.Fill.BackgroundColor.SetColor(Color.Gray);
            worksheet.Cells[range].Style.Font.Bold = true;
            worksheet.Cells[range].Style.Font.Color.SetColor(Color.White);
        }

        private static void AddData(ExcelWorksheet worksheet, List<List<string>> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < data[i].Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1].LoadFromText(data[i][j]);
                }
            }
        }
    }
}