using System;
using CustomGenerics.Structures;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using API.Models_;
 


namespace StringCompressor
{
    class Program
    { 
        static void Main(string[] args)
        {
            Huffman<HuffmanChar> Huff = new Huffman<HuffmanChar>();
            try
            {
            //Insert:
                Console.WriteLine("Escribe el string a comprimir");
                string text = Console.ReadLine();
                
                Console.WriteLine("Se ha guardado el string con éxito para comprimir");
                string CompressedText = Huff.CompressText(text);
                Console.WriteLine("El resultado de la compresión es el siguiente:");
                Console.WriteLine(CompressedText);
                Console.WriteLine("¿Desea descomprimirlo? | Presione 'Y'. De lo contrario, presione cualquier otra tecla.");
                if (Console.ReadKey().Key == ConsoleKey.Y)
                {
                    Console.Clear();
                    string originaltext = Huff.DecompressText(CompressedText);
                    Console.WriteLine("El resultado de la descompresión es el siguiente:");
                    Console.WriteLine(originaltext);
                    Console.ReadLine();
                }
                Console.WriteLine("Feliz día!");
                Console.ReadLine();

            }
            catch
            {
                Console.WriteLine("Por favor ingrese un string válido.");
                Console.ReadLine();
                Console.Clear();
                //goto Insert;

            }

        }
    }
}
