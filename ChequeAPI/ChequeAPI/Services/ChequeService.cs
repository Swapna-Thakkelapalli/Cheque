using ChequeAPI.Helpers;
using ChequeAPI.Models;

using iTextSharp.text;
using iTextSharp.text.pdf;

using Microsoft.Extensions.Configuration;

using SelectPdf;

using System;
using System.Drawing;
using System.IO;

namespace ChequeAPI.Services
{
    public class ChequeService : IChequeService
    {
        private readonly IConfiguration _configuration;

        public ChequeService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public byte[] GenerateCheque(ChequeDTO cheque)
        {
            var section = _configuration.GetSection(Constants.CurrConvConfiguration);
            string apiKey = section.GetValue<string>(Constants.CurrConvApikey);
            string apiUrl = section.GetValue<string>(Constants.CurrConvUrl);
            var response = HttpHelper.CallAPI<string>(apiUrl, (int)Enums.APICall.Get, $"q={cheque.Currency}_GBP&compact=ultra&apiKey={apiKey}");
            var convertedAmount = Math.Round(cheque.Amount * (Convert.ToDouble(response.Split(":")[1].Replace("}", ""))),2);
            string AmountInWords = Utility.AmountToWords(convertedAmount);
            string currentDirectory = Directory.GetCurrentDirectory();
            string fullPath = Path.Combine(currentDirectory, Constants.Assets, Constants.ChequeHtml);
            string chequeHtml = File.ReadAllText(fullPath);
            chequeHtml = chequeHtml.Replace(Constants.PayeeName, cheque.PayeeName).Replace(Constants.ServerUrl, currentDirectory+"/"+Constants.Assets);
            chequeHtml = chequeHtml.Replace(Constants.Amount, convertedAmount.ToString()).Replace(Constants.Date,cheque.Date).Replace(Constants.AmountInWords, AmountInWords);
            byte[] result = GetCertificateByteArray(chequeHtml);
            byte[] finalByteArray = ConcatAndAddContent(result);
            return finalByteArray;
        }
        private byte[] GetCertificateByteArray(string htmlString)
        {
            HtmlToPdf converter = new HtmlToPdf();
            SelectPdf.PdfDocument doc = new SelectPdf.PdfDocument();
            converter.Options.PdfPageSize = PdfPageSize.Custom;
            converter.Options.PdfPageCustomSize = new SizeF(700, 260);
            converter.Options.MarginLeft = 0;
            converter.Options.MarginRight = 0;
            converter.Options.MarginTop = 0;
            converter.Options.MarginBottom = 0;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            doc = converter.ConvertHtmlString(htmlString);
            byte[] sample = doc.Save();
            doc.Close();
            return sample;
        }
        private static byte[] ConcatAndAddContent(byte[] chequedoc)
        {
            byte[] pdf;
            using (MemoryStream ms = new MemoryStream())
            {
                Document doc = new Document();
                PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                var pgSize = new iTextSharp.text.Rectangle(700, 260);
                doc.SetPageSize(pgSize);
                doc.Open();
                PdfContentByte cb = writer.DirectContent;
                PdfImportedPage page;
                PdfReader reader;
                reader = new PdfReader(chequedoc);
                int pages = reader.NumberOfPages;
                // loop over document pages
                for (int i = 1; i <= pages; i++)
                {
                    doc.NewPage();
                    page = writer.GetImportedPage(reader, i);
                    cb.AddTemplate(page, 0, 0);
                }
                doc.Close();
                pdf = ms.GetBuffer();
                ms.Flush();
                ms.Dispose();
            }
            return pdf;
        }
    }
}
