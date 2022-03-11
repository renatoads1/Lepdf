using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using LendoPdf.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace LendoPdf
{
    class Program
    {
        static void Main(string[] args)
        {
            //usado para criar o banco via EF
            //using (var context = new ApplicationDbContext())
            //{
            //    context.Database.EnsureCreated();
            //}
           
            
            //faz o download do arquivo
            for (int z = 1; z < 25; z++) {

                
                DirectoryInfo diretorio = new DirectoryInfo(@"C:\PDF\");
                StringBuilder sb = new StringBuilder();
                string caminho = "";
                string dataDaDisponibilizacao = "";
                string txtTratado = "";
                var context = new ApplicationDbContext();
                List<Publicacao> publicacoes = new List<Publicacao>();
                FazDownload fd = new FazDownload();
                
                //inicio
                caminho = fd.FazdownloadTrt(z);// download e pega o caminho do arquivo 
                //lendo o pdf 
                using (PdfReader reader = new PdfReader(caminho))
                {
                    for (int pageNo = 1; pageNo <= reader.NumberOfPages; pageNo++)
                    {
                        ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                        string text = PdfTextExtractor.GetTextFromPage(reader, pageNo, strategy);
                        text = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(text)));
                        sb.Append(text);
                    }
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

                            if (itemz.Contains("disponibilização:") || (a > 0 && a < 7))
                            {
                                dataDaDisponibilizacao += itemz;
                                a++;
                                //de 0 a 6
                            }
                        }
                        i++;
                    }
                    else
                    {
                        i++;
                        //txtTratado += "\n\n--->"+dataDaDisponibilizacao + "\n#####\nProcesso Nº " + item;
                        Console.WriteLine(i.ToString() + " de " + explosao.Length.ToString());

                    }
                    //coloca as publicações em uma lista para depois inserir no banco
                    Publicacao pub = new Publicacao();
                    pub.Trt = z.ToString();
                    pub.DataJornal = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                    pub.DataPublicacao = dataDaDisponibilizacao;
                    pub.PublicacaoText = "Processo Nº " + item;
                    publicacoes.Add(pub);

                }
                //salvar lista de publicacoes no banco aqui
                int c = 0;
                foreach (var itemc in publicacoes)
                {
                    context.Add(itemc);
                    context.SaveChanges();
                    Console.WriteLine($"Gravando db {c}");
                    c++;
                }

                //StreamWriter sw2 = new StreamWriter(caminho+".txt", true, Encoding.UTF8);
                //try
                //{
                //    //Write out the numbers 1 to 10 on the same line.
                //    sw2.Write(txtTratado);
                //    //close the file
                //    sw2.Close();
                //}
                //catch (Exception e)
                //{
                //    Console.WriteLine("Exception: " + e.Message);
                //    Console.ReadLine();
                //}
                //finally
                //{
                //    sw2.Close();
                //}
                //deleta arquivo pdf
                File.Delete(caminho);
                Console.Clear();
            }

            Environment.Exit(0);
        }
    }
}
