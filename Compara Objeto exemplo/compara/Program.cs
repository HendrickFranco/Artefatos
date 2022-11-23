using Artefatos.ClassesExemplos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Artefatos
{
    public class Program
    {


        static void Main(string[] args)
        {
            MetodosTops metodosTops = new MetodosTops();
            Console.WriteLine("Inicio - > Compara dois objetos e retorna as alterações em uma lista de tuple.");

            #region Compara dois objetos e retorna as alterações em uma lista de tuple.
            //cria os objetos e setas os valores que serão comparados
            ClasseTeste obj1 = new ClasseTeste(1, "João", "exemplo de descricao", new ClasseTipoTeste(1, "Tipo1"), new TelefoneTeste(999999, 41));
            ClasseTeste obj2 = new ClasseTeste(1, "João Alterado", "exemplo de descricao Alterada", new ClasseTipoTeste(2, "Tipo1 alterado"), new TelefoneTeste(98888888, 42));
            //Informa as propriedades que devem ser comparadas. 
            var propriedades = (new string[] {nameof(ClasseTeste.Codigo),
                                              nameof(ClasseTeste.Nome),
                                              nameof(ClasseTeste.Descricao),
                                              nameof(ClasseTeste.TipoTeste)
                                              //,nameof(ClasseTeste.Telefone)
                                              }).ToList();

            var diferencas = metodosTops.ComparaDoisObjetos(obj1, obj2, propriedades);
            StringBuilder mensagemExemploAlteracao = new StringBuilder();
            mensagemExemploAlteracao.Append("Alteração das propriedades: \r\n");

            foreach (var diferenca in diferencas)
            {
                if (mensagemExemploAlteracao.Length > "Alteração das propriedades: \r\n".Length)
                {
                    mensagemExemploAlteracao.Append(", \r\n");
                }
                //se a propriedade tiver data annotation 'description' usa a description como nome da propriedade, sen usa o proprio nome da pripriedade 'NameOF'.
                // nesse exemplo apenas a proprieadade descrição possui data annotation.
                mensagemExemploAlteracao.Append(String.IsNullOrEmpty(diferenca.Item3) ? diferenca.Item1 : diferenca.Item3);
                mensagemExemploAlteracao.Append(": de ");
                mensagemExemploAlteracao.Append("'");
                mensagemExemploAlteracao.Append(diferenca.Item2);
                mensagemExemploAlteracao.Append("'");
                mensagemExemploAlteracao.Append(" para ");
                mensagemExemploAlteracao.Append("'");
                mensagemExemploAlteracao.Append(diferenca.Item4);
                mensagemExemploAlteracao.Append("'");
            }
            mensagemExemploAlteracao.Append(".");
            Console.WriteLine("------------------------exemplo mensagem alteração-------------------------");
            Console.WriteLine(mensagemExemploAlteracao);
            #endregion

            Console.WriteLine("------------------------outro exemplo mensagem alteração-------------------------");
            string retorno = "";
            var x = metodosTops.ComparaDoisObjetos(obj1, obj2, propriedades);
            var auxDescricao = "";
            Type myType = typeof(ClasseTeste);
            Type tipoPropriedade;

            for(var i = 0; i <= x.Count - 1; i++)
            {
                tipoPropriedade = myType.GetProperty(x[i].Item1).PropertyType;
                
                if (!tipoPropriedade.IsPrimitive && tipoPropriedade.IsClass && tipoPropriedade != typeof(string))
                {
                    if (!string.IsNullOrEmpty(auxDescricao) && auxDescricao != x[i].Item1)
                    {
                        retorno += "}";
                        auxDescricao = x[i].Item1;
                    }
                    retorno += " " + (string.IsNullOrEmpty(x[i].Item3) ? x[i].Item1 : x[i].Item3) + ": {\r\n";
                }
                else
                {
                    switch (x[i].Item5)
                    {
                        case "":
                            retorno += $" {(x[i].Item1)}: de '";
                            //verifica se tem valor anterior
                            retorno += (x[i].Item2 != "" && x[i].Item2 != "0" ? x[i].Item2 : "Não Informado") + "' para '";
                            retorno += (x[i].Item4 != "" && x[i].Item4 != "0" ? x[i].Item4 : "Não Informado") + "', \r\n";
                            break;
                        default:
                            retorno += $"   {(x[i].Item1)}: de '";
                            //verifica se tem valor anterior
                            retorno += (x[i].Item2 != "" && x[i].Item2 != "0" ? x[i].Item2 : "Não Informado") + "' para '";
                            retorno += (x[i].Item4 != "" && x[i].Item4 != "0" ? x[i].Item4 : "Não Informado") + "', \r\n";
                            break;
                    }
                }

                if(i + 1 > x.Count - 1 && !string.IsNullOrEmpty(x[i].Item5))
                {
                    retorno += " }";
                }
            }
            //foreach (var item in x)
            //{


            //    var tipoPropriedade = myType.GetProperty(item.Item1).PropertyType;
            //    if (!tipoPropriedade.IsPrimitive && tipoPropriedade.IsClass && tipoPropriedade != typeof(string))
            //    {
            //        if (!string.IsNullOrEmpty(auxDescricao) && auxDescricao != item.Item1)
            //        {
            //            retorno += "}";
            //            auxDescricao = item.Item1;
            //        }
            //        retorno += " " + (string.IsNullOrEmpty(item.Item3) ? item.Item1 : item.Item3) + ": {\r\n";
            //    }
            //    else
            //    {
            //        switch (item.Item5)
            //        {
            //            case "":
            //                retorno += $" {(item.Item1)}: de '";
            //                //verifica se tem valor anterior
            //                retorno += (item.Item2 != "" && item.Item2 != "0" ? item.Item2 : "Não Informado") + "' para '";
            //                retorno += (item.Item4 != "" && item.Item4 != "0" ? item.Item4 : "Não Informado") + "', \r\n";
            //                break;                            
            //            default:
            //                retorno += $"   {(item.Item1)}: de '";
            //                //verifica se tem valor anterior
            //                retorno += (item.Item2 != "" && item.Item2 != "0" ? item.Item2 : "Não Informado") + "' para '";
            //                retorno += (item.Item4 != "" && item.Item4 != "0" ? item.Item4 : "Não Informado") + "', \r\n";
            //                break;
            //        }
            //    }
            //}
            retorno += !String.IsNullOrWhiteSpace(retorno) ? "" : "";

            Console.Write(retorno);
            Console.ReadKey();
        }
    }


}
