using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace LendoPdf
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            string caminho = @"C:\PDF\Diario_3422__25_2_2022.pdf";
            using (PdfReader reader = new PdfReader(caminho))
            {
                for (int pageNo = 1; pageNo <= reader.NumberOfPages; pageNo++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string text = PdfTextExtractor.GetTextFromPage(reader, pageNo, strategy);
                    text = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(text)));
                    sb.Append(text);
                }
                // 1 preciso escrever este texto em arquivo com append txt
                //Open the File
                StreamWriter sw = new StreamWriter(@"C:\PDF\Diario_3422__25_2_2022.txt", true, Encoding.UTF8);
                try
                {
                    //Write out the numbers 1 to 10 on the same line.
                    sw.Write(sb);
                    //close the file
                    sw.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                    Console.ReadLine();
                }
                finally
                {
                    sw.Close();
                }
            }
            // 2 pegar linha por linha do txt separando em blocos
            
        }
    }
}
