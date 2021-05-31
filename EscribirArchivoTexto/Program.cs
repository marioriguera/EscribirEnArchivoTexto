using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EscribirArchivoTexto
{
    /// <summary>
    /// Objeto de ejemplo que representa aquí una supuesta Reserva
    /// </summary>
    class Reserva
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Moneda { get; set; }
        public double Valor { get; set; }
    }
    class Program
    {     
        /// <summary>
        /// Ejemplo de un objeto que se va a guardar en la Base de Datos.
        /// En el caso de TaoUniverse se sustituiría el valor que retorna esta funcion por el objeto que se intenta guardar en la base de datos.
        /// </summary>
        /// <returns></returns>
        private static Reserva LlenarReserva()
        {
            Random r = new Random();
            var numeroRandom = r.Next(1, 100);

            return new Reserva { Nombre = "Nombre"+ numeroRandom , Apellidos = "Apellidos"+ numeroRandom , Direccion = "Direccion"+numeroRandom , Moneda = "Moneda"+numeroRandom , Valor = 50};
        }
        /// <summary>
        /// Debido a que este ejemplo es un proyecto de consola esta función lo que hace es ejecutar la consola y tres veces la funcion ImprimirDatosLog() que es
        /// donde está el ejemplo de impresion en un archivo de texto. En TaoUniverse esta función no existiría
        /// ya que donde se ejecutaria la impresión en el archivo de texto.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            await ImprimirDatosLog(LlenarReserva(),"Funcion AgregarReserva()");
            await ImprimirDatosLog(LlenarReserva(),"Funcion ActualizarReserva()");
            await ImprimirDatosLog(LlenarReserva(),"Funcion GestionarReserva()");

            Console.WriteLine("Completado");

            Console.ReadLine();
        }

        /// <summary>
        /// Esta funcion representaría cualquiera que tiene una comunicación con la base de datos, que se encuentra en la capa de TaoUniverse (Visual), la cual tenga una acción a ejecutar.
        /// Funcionaría tanto para la implementación de Javier como para el uso de EntityFramework.
        /// Por ejemplo FacturarAgenciaAsync está la llamada a la función Facturar(), dentro de la misma se crea la factura, entonces antes de enviar la factura a la base de datos implementamos la escritura
        /// en el archivo de texto para recoger sus valores (Paso # 1). Es importante también en el catch de la función agregar la impresión de los datos de la posible excepción que surja. (Mensaje, InnerException y StackTrace)
        /// </summary>
        /// <param name="reserva"> Esto sería el ejemplo de cualquier ejemplo que desean imprimir </param>
        /// <param name="funcion"> Sería el nomrbre de la Función identificada </param>
        /// Los parametros de esta función de ejmplo no existirian tampoco en el TaoUniverse ya que el parametro funcion seria un string con el nombre exacto de la funcion en la que nos encontramos y reserva
        /// sería el objeto que deseamos manipular.
        /// <returns></returns>
        public static async Task ImprimirDatosLog(Reserva reserva, string funcion)
        {
            //Paso # 1
            //EL archivo se creará en la raiz de la implementación.
            using StreamWriter file = new StreamWriter("LogsDeDatos.txt", append: true);
            await file.WriteLineAsync("//----------------------------------------------------------");            
            await file.WriteLineAsync(funcion + " [" + DateTime.Now+"]");            
            await file.WriteLineAsync("La reserva es: ");    
            await file.WriteLineAsync("Nombre: "+reserva.Nombre+" , "+ "Apellidos: " + reserva.Apellidos + " , "+ "Direccion: "+reserva.Direccion + " , "+ "Moneda: "+reserva.Moneda + " , "+"Valor: "+reserva.Valor);
            
            try
            {
                /*

                    Codigo de las funciones

                */

                if (reserva.Valor == 50)
                {
                    /*
                    Se lanza una excepcion para demostrar que en el archivo de texto se pueden imprimir las excepciones, esto sería en este ejemmplo
                    ya que las excepciones vendrán desde la base de datos o generadas por la propia aplicación.
                    */
                    throw new Exception("Esto es una excepcion que ocurre despues que tengo los datos en el archivo. ");
                }

            }catch(Exception ex)
            {
                //Paso # 2
                await file.WriteLineAsync(ex.Message);
                if (ex.InnerException != null) await file.WriteLineAsync(ex.InnerException.Message); else await file.WriteLineAsync("No hay InnerException");
                if (ex.InnerException != null) await file.WriteLineAsync(ex.InnerException.StackTrace); else await file.WriteLineAsync("No hay StackTrace");
            }
        }
    }
}
