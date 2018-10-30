using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DrugProNET
{
    public partial class ExcelWriterTestPage : System.Web.UI.Page
    {
        static Random r = new Random();

        protected void DownloadButton_Click(object sender, EventArgs e)
        {
            string[] d = { "ID", "First Name", "Last Name", "DOB" };
            List<string> header = new List<String>(d);

            List<List<string>> data = new List<List<string>>();
            for (int i = 0; i < r.Next(100, 200); i++)
            {
                List<string> row = new List<string>();
                for (long j = 0; j < header.Count; j++)
                {
                    row.Add((r.NextDouble() * 1000000).ToString());
                }
                data.Add(row);
            }

            ExcelWriter.CreateInTemp(header, data);

            Response.Clear();
            Response.ContentType = "application/force-download";
            Response.AddHeader("content-disposition", "attachment; filename=file.xlsx");
            Response.BinaryWrite(ExcelWriter.CreateAsStream(header, data).ToArray());
            Response.End();
        }
    }
}