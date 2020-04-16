using System;
using System.IO;
using TransPerfect.Services;

namespace TransPerfect
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Please insert the path from the file to read QR:");
            string path = Console.ReadLine();

            while (File.Exists(path))
            {
                Console.WriteLine("The file don't exists.");
                Console.WriteLine("Please insert the path from the file to read QR:");
                path = Console.ReadLine();
            }

            Console.Write(new ApiClientGoQR().ReadQR(File.ReadAllBytes(path)));

        }
    }
}
