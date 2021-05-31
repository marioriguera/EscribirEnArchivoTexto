using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EscribirArchivoTexto
{
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

        private static Reserva LlenarReserva()
        {
            Random r = new Random();
            var numeroRandom = r.Next(1, 100);

            return new Reserva { Nombre = "Nombre"+ numeroRandom , Apellidos = "Apellidos"+ numeroRandom , Direccion = "Direccion"+numeroRandom , Moneda = "Moneda"+numeroRandom , Valor = 50};
        }
        static async Task Main(string[] args)
        {
            await ImprimirDatosLog(LlenarReserva(),"Funcion AgregarReserva()");
            await ImprimirDatosLog(LlenarReserva(),"Funcion ActualizarReserva()");
            await ImprimirDatosLog(LlenarReserva(),"Funcion AgregarReserva()");

            Console.WriteLine("Completado");

            Console.ReadLine();
        }

        public static async Task ImprimirDatosLog(Reserva reserva, string funcion)
        {
            using StreamWriter file = new StreamWriter("LogsDeDatos.txt", append: true);
            await file.WriteLineAsync("//----------------------------------------------------------");            
            await file.WriteLineAsync(funcion + " [" + DateTime.Now+"]");            
            await file.WriteLineAsync("La reserva es: ");    
            await file.WriteLineAsync("Nombre: "+reserva.Nombre+" , "+ "Apellidos: " + reserva.Apellidos + " , "+ "Direccion: "+reserva.Direccion + " , "+ "Moneda: "+reserva.Moneda + " , "+"Valor: "+reserva.Valor);
            try
            {
                if(reserva.Valor == 50)
                {
                    throw new Exception("Esto es una excepcion que ocurre despues que tengo los datos en el archivo. ");
                }

            }catch(Exception ex)
            {
                await file.WriteLineAsync(ex.Message);
            }
        }
    }
}
