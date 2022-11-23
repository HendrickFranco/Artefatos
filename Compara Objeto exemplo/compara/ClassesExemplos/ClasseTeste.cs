using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artefatos.ClassesExemplos
{
    public class ClasseTeste
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        [Description("Descrição (unica propriedade com data annotation)")]
        public string Descricao { get; set; }

        public ClasseTipoTeste TipoTeste { get; set; }
        public TelefoneTeste Telefone { get; set; }

        public ClasseTeste(int codigo, string nome, string descricao, ClasseTipoTeste tipoTeste, TelefoneTeste telefone)
        {
            this.Codigo = codigo;
            this.Nome = nome;
            this.Descricao = descricao;
            this.TipoTeste = tipoTeste;
            this.Telefone = telefone;
        }
    }

    public class ClasseTipoTeste
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }

        public ClasseTipoTeste(int codigo, string descricao)
        {
            this.Codigo = codigo;
            this.Descricao = descricao;
        }
    }
    public class TelefoneTeste
    {
        public int Numero { get; set; }
        public int DD { get; set; }


        public TelefoneTeste(int numero, int dd)
        {
            this.Numero = numero;
            this.DD = dd;
        }
    }
}
