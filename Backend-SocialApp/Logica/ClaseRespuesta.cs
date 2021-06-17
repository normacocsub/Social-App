using System.Collections.Generic;
using Entity;

namespace Logica
{
    public class ClaseRespuesta<T>
    {
        public ClaseRespuesta(T objeto)
        {
            Error = false;
            Objeto = objeto;
        }

        public ClaseRespuesta(List<T> lista)
        {
            Error = false;
            Lista = lista;
        }

        public ClaseRespuesta(string mensaje)
        {
            Error = true;
            Mensaje = mensaje;
        }
        public bool Error { get; set; }
        public string Mensaje { get; set; }
        public T Objeto { get; set; }
        public List<T> Lista { get; set; }
    }
}