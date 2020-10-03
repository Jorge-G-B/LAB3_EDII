using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
//using API.Models;
 


namespace StringCompressor
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Escribe el string a comprimir");
                string code = Console.ReadLine();
                Console.WriteLine("Se ha guardado el string con éxito para comprimir");
                //bool.HasMoreValues = true;
                //
            }
            catch 
            {
                Console.WriteLine("Por favor ingrese un string válido.");
                Console.ReadLine();
                Console.Clear();
                throw;
            }              
                
        }
    }
}
