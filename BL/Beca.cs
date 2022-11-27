using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Beca
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DpascualAlfaSolucionesContext context = new DL.DpascualAlfaSolucionesContext())
                {
                    var becas = (from becaLinq in context.Becas
                                   select becaLinq).ToList();

                    if (becas != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var objBeca in becas)
                        {
                            ML.Beca beca = new ML.Beca();
                            beca.IdBeca = objBeca.IdBeca;
                            beca.Tipo = objBeca.Tipo;

                            result.Objects.Add(beca);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }
            return result;
        }
    }
}
