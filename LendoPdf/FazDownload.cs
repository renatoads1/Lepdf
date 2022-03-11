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
    public class FazDownload
    {
        public string FazdownloadTrt(int trt) {
            //Marca o diretório a ser listado
            DirectoryInfo diretorio = new DirectoryInfo(@"C:\PDF\");
            string retorno = "";
            //variaveis de robo webdrive
            string url = @"https://dejt.jt.jus.br/dejt/f/n/diariocon";
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--incognito");
            options.AddUserProfilePreference("download.default_directory", @"C:\PDF\");
            ChromeDriver driver = new ChromeDriver(options);
            //robô do selenium webdriver
            try
            {
                driver.Navigate().GoToUrl(url);//abre navegador
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro:"+ex.Message);
                driver.Close();
                driver.Quit();
                Environment.Exit(0);
            }
            
            driver.Manage().Window.Maximize();//navegador maximizado
            //select Juridico
            var inputData = driver.FindElement(By.Name("corpo:formulario:tipoCaderno"));
            var selectObject = new SelectElement(inputData);
            selectObject.SelectByIndex(2);
            //proximo select tribunal
            var inputData2 = driver.FindElement(By.Name("corpo:formulario:tribunal"));
            var selectObject2 = new SelectElement(inputData2);
            selectObject2.SelectByIndex(trt);//tribunáis sempre do 1 áo 24
            //click buton  ''
            var buttonData = driver.FindElement(By.Name("corpo:formulario:botaoAcaoPesquisar"));
            buttonData.Click();

            //lick download
            var btnDownload = driver.FindElement(By.XPath("//*[@class='bt af_commandButton']"));
            btnDownload.Click();
            //fechar navegador
            Thread.Sleep(15000);
            driver.Close();
            driver.Quit();
            

            //Executa função GetFile(Lista os arquivos desejados de acordo com o parametro)
            FileInfo[] Arquivos = diretorio.GetFiles("*.*");
            //Começamos a listar os arquivos
            foreach (FileInfo fileinfo in Arquivos)
            {
                if (fileinfo.Name.Contains(".pdf"))
                {
                    retorno = @"C:\PDF\" + fileinfo.Name;
                }
            }
            return retorno;
        }
    }
}
