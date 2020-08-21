using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALICORP.Entidades;
using ALICORP.Logicas;

namespace ALICORP.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ActualizarVerificacion();
        }
        private static void BuscarVerificacion()
        {
            VerificacionLogica verificacionLogica = new VerificacionLogica();

            Verificacion entidad = verificacionLogica.Buscar(1,true);

            Console.WriteLine(entidad.Id);
            Console.WriteLine(entidad.Nombre);
            Console.WriteLine(entidad.Activo);
            Console.ReadKey();
        }


        private static void ListarVerificacion()
        {
            VerificacionLogica verificacionLogica = new VerificacionLogica();
            verificacionLogica.Listar();

        }

        private static void ActualizarVerificacion()
        {

            VerificacionLogica verificacionLogica = new VerificacionLogica();
            Verificacion verificacion = verificacionLogica.Buscar(1, true);

            verificacion.Nombre = "Verificación modificada";

            int i = 0;
            foreach (var item in verificacion.Categorias)
            {
                item.Descripcion = string.Format("Categoria Nº {0} modificado", ++i);
            }

            //    Verificacion verificacion = new Verificacion();
            //    verificacion.Id = 1;
            //    verificacion.Activo = true;
            //    verificacion.Nombre = "Verificacion 1";

            //    Categoria categoria = new Categoria();
            //    categoria.Id = 1;
            //    categoria.VerificacionId = 1;
            //    categoria.Descripcion = "ASPECTOS GENERALES1";
            //    categoria.Orden = 1;
            //    categoria.Eliminado = true;

            //    Categoria categoria1 = new Categoria();
            //    categoria1.Id = 2;
            //    categoria1.VerificacionId = 2;
            //    categoria1.Descripcion = "ASPECTOS GENERALES2";
            //    categoria1.Orden = 1;
            //    categoria1.Eliminado = true;

            //    Categoria categoria2 = new Categoria();
            //    categoria2.Id = 3;
            //    categoria2.VerificacionId = 3;
            //    categoria2.Descripcion = "ASPECTOS GENERALES3";
            //    categoria2.Orden = 1;
            //    categoria2.Eliminado = true;

            //    Pregunta pregunta = new Pregunta();
            //    pregunta.Id = 1;
            //    pregunta.CategoriaId= 1;
            //    pregunta.Orden = 1;
            //    pregunta.Titulo = " Como te llamas1";
            //    pregunta.Descripcion = "";
            //    pregunta.Eliminado = true;

            //    Pregunta pregunta1 = new Pregunta();
            //    pregunta1.Id = 2;
            //    pregunta1.CategoriaId = 2;
            //    pregunta1.Orden = 1;
            //    pregunta1.Titulo = "Tons que mami?";
            //    pregunta1.Descripcion = "";
            //    pregunta1.Eliminado = true;

            //    Pregunta pregunta2 = new Pregunta();
            //    pregunta2.Id = 3;
            //    pregunta2.CategoriaId = 3;
            //    pregunta2.Orden = 1;
            //    pregunta2.Titulo = "Algo fast bellakita";
            //    pregunta2.Descripcion = "";
            //    pregunta2.Eliminado = true;

            //    Respuesta respuesta = new Respuesta();
            //    respuesta.PreguntaId = 1;
            //    respuesta.Descripcion = "Tu dirasssssss";
            //    respuesta.Valor = 1;

            //    Respuesta respuesta1 = new Respuesta();
            //    respuesta1.PreguntaId = 2;
            //    respuesta1.Descripcion = "Tu diras";
            //    respuesta1.Valor = 1;

            //    Respuesta respuesta2 = new Respuesta();
            //    respuesta2.PreguntaId = 3;
            //    respuesta2.Descripcion = "Tu diras";
            //    respuesta2.Valor = 1;

            //    pregunta.Respuestas = new List<Respuesta>() { respuesta, respuesta1, respuesta2 };
            //    categoria.Preguntas = new List<Pregunta>() { pregunta,pregunta1, pregunta2};
            //    verificacion.Categorias = new List<Categoria> { categoria, categoria1, categoria2};

            verificacionLogica.Actualizar(verificacion);
        }

        private static void GuardarVerificacion()
        {
            Verificacion verificacion = new Verificacion();
            verificacion.Activo = true;
            verificacion.Nombre = "Verificacion 1";

            Categoria categoria = new Categoria();
            categoria.Descripcion = "ASPECTOS GENERALES";
            categoria.Orden = 1;
            categoria.Eliminado = false;

            Categoria categoria2 = new Categoria();
            categoria2.Descripcion = "ASPECTOS GENERALES 2";
            categoria2.Orden = 2;
            categoria2.Eliminado = false;

            Pregunta pregunta = new Pregunta();
            pregunta.Orden = 1;
            pregunta.Titulo = " Como te llamas";
            pregunta.Descripcion = "";
            pregunta.Eliminado = false;


            Pregunta pregunta2 = new Pregunta();
            pregunta2.Orden = 2;
            pregunta2.Titulo = " Como te llamas 2";
            pregunta2.Descripcion = "";
            pregunta2.Eliminado = false;

            Respuesta respuesta = new Respuesta();
            respuesta.Descripcion = "Si";
            respuesta.Valor = 1;

            Respuesta respuesta2 = new Respuesta();
            respuesta2.Descripcion = "Si";
            respuesta2.Valor = 2;

            Respuesta respuesta3 = new Respuesta();
            respuesta3.Descripcion = "Si";
            respuesta3.Valor = 4;

            pregunta.Respuestas = new List<Respuesta>() { respuesta, respuesta2, respuesta2 };
            categoria.Preguntas = new List<Pregunta>() { pregunta, pregunta2 };
            verificacion.Categorias = new List<Categoria> { categoria, categoria2 };


            VerificacionLogica verificacionLogica = new VerificacionLogica();
            verificacionLogica.Guardar(verificacion);
        }
    }
}
