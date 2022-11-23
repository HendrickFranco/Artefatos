using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Artefatos
{
    public class MetodosTops
    {

        /// <summary>
        ///  Compara dois objetos do mesmo tipo e retorna se houve alteração nas propriedades informadas no parametro 'considerar'.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="obj"> objeto com os valores que estão no banco de dados</param>
        /// <param name="objAlterado"> objeto com os valores que estão na tela</param>
        /// <param name="considerar"> propriedades que serão verificadas se houve alteração.</param>
        /// <returns></returns>
        public List<Tuple<string, string, string, string, string>> ComparaDoisObjetos<A, B>(A obj, B objAlterado, List<string> considerar = null)
        {
            PropertyInfo aux;
            string vlr1;
            string vlr2;
            object[] atributos;
            string nome;
            object attr;
            string descricaoProp;
            List<Tuple<string, string, string, string, string>> lst = new List<Tuple<string, string, string, string, string>>();
            foreach (var item in obj.GetType().GetProperties())
            {
                descricaoProp = "";
                if (considerar != null && considerar.Count > 0 && considerar.Any(x => x.Equals(item.Name)) || considerar == null)
                {
                    aux = obj.GetType().GetProperties().Where(x => x.Name == item.Name).FirstOrDefault();
                    if (aux != null)
                    {
                        nome = item.Name;
                        atributos = item.GetCustomAttributes(true);
                        if (atributos != null && atributos.Count() > 0)
                        {
                            attr = atributos.Where(x => x.ToString().Equals("System.ComponentModel.DescriptionAttribute")).FirstOrDefault();
                            if (attr != null)
                            {
                                descricaoProp = ((DescriptionAttribute)attr).Description;
                            }
                        }
                        //Verficia se  a propriedade é  primitiva
                        if (!aux.PropertyType.IsPrimitive && aux.PropertyType.IsClass && !(aux.PropertyType == typeof(string)))
                        {
                            var auxObjAlterado = aux.GetValue(objAlterado);
                            var auxObj = item.GetValue(obj);
                            var teste = this.ComparaDoisObjetos(auxObj, auxObjAlterado, null);
                            string propPai = nome;
                            if(teste.Count > 0)
                            {
                                //para propriedades pais, no caso de classes, o tuple retorna todos os valores como o nome da classe exceto o item 3 que é o data notation.
                                lst.Add(new Tuple<string, string, string, string, string>(nome, nome, descricaoProp.ToString(), nome, ""));
                            }
                            foreach (var auxTeste in teste)
                            {
                                lst.Add(new Tuple<string, string, string, string, string>(auxTeste.Item1, auxTeste.Item2, descricaoProp, auxTeste.Item4, propPai)); ;
                            }
                        }
                        else
                        {
                            vlr1 = aux.GetValue(objAlterado)?.ToString() ?? "";
                            vlr2 = item.GetValue(obj)?.ToString() ?? "";
                            if (vlr1 != vlr2)
                            {
                                lst.Add(new Tuple<string, string, string, string, string>(nome, (aux.GetValue(obj) != null ? aux.GetValue(obj).ToString() : " "), descricaoProp.ToString(), vlr1, ""));
                            }
                        }
                    }
                }
                else
                {
                    continue;
                }
            }
            return lst;
        }
    }
 
}
