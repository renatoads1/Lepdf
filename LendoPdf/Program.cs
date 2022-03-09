using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.IO;
using System.Text;

namespace LendoPdf
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataDaDisponibilizacao = "";
            string txtTratado = "";
            StringBuilder sb = new StringBuilder();
            string caminho = @"C:\PDF\Diario_3427__8_3_2022.pdf";
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
                //StreamWriter sw = new StreamWriter(@"C:\PDF\Diario_3427__8_3_2022.txt", true, Encoding.UTF8);
                //try
                //{
                //    //Write out the numbers 1 to 10 on the same line.
                //    sw.Write(sb);
                //    //close the file
                //    sw.Close();
                //}
                //catch (Exception e)
                //{
                //    Console.WriteLine("Exception: " + e.Message);
                //    Console.ReadLine();
                //}
                //finally
                //{
                //    sw.Close();
                //}
            }
            // 2 pegar linha por linha do txt separando em blocos

            var textCompleto = sb.ToString();
            var explosao = textCompleto.Split("Processo Nº");
            int i = 0;
            int a = 0;
            foreach (var item in explosao)
            {
                if (i == 0)
                {
                    //trata o cabeçálio
                    var cabecalioArr = item.Split(" ");
                    foreach (var itemz in cabecalioArr)
                    {
                        
                        if (itemz.Contains("disponibilização:") || (a > 0 && a < 7)) {
                            dataDaDisponibilizacao += itemz;
                            a++;
                            //de 0 a 6
                        }
                    }
                    i++;
                }
                else {
                    i++;
                    txtTratado += dataDaDisponibilizacao + "\n#####\nProcesso Nº " + item;
                    Console.WriteLine(i.ToString() + " de " + explosao.Length.ToString());

                }

            }
            StreamWriter sw2 = new StreamWriter(@"C:\PDF\DiarioSeparado.txt", true, Encoding.UTF8);
            try
            {
                //Write out the numbers 1 to 10 on the same line.
                sw2.Write(txtTratado);
                //close the file
                sw2.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                Console.ReadLine();
            }
            finally
            {
                sw2.Close();
            }


        }
    }
}
