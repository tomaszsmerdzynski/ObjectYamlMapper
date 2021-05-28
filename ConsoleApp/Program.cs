using System;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            // zmienic
            string folderPath = @"C:\Users\Admin\source\repos\ObjectYamlMapper\ConsoleApp\Files";
            string filePath = Path.Combine(folderPath, "test.yaml");
        }
    }
}
