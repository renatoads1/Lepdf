using System;
using System.Collections.Generic;
using System.Text;

namespace LendoPdf
{
    public class Publicacao
    {
        public int Id { get; set; }
        public string Trt { get; set; }
        public string DataJornal { get; set; }
        public string DataPublicacao { get; set; }
        public string PublicacaoText { get; set; }

        public Publicacao()
        {
        }

        public Publicacao(string trt, string dataJornal, string dataPublicacao, string publicacaoText)
        {
            Trt = trt;
            DataJornal = dataJornal;
            DataPublicacao = dataPublicacao;
            PublicacaoText = publicacaoText;
        }

        public Publicacao(int id, string trt, string dataJornal, string dataPublicacao, string publicacaoText)
        {
            Id = id;
            Trt = trt;
            DataJornal = dataJornal;
            DataPublicacao = dataPublicacao;
            PublicacaoText = publicacaoText;
        }
    }
}
