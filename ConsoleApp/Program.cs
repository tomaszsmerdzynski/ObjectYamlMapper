using ObjectYamlMapper.Serialization;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Serializer yamlSerializer = new();

            Person person = new()
            {
                Firstname = "Jan",
                Lastname = "Kowalski",
                Age = 30,
                Projects = new List<string> { "projekt1", "projekt2", "projekt3", }
            };

            string folderPath = @"C:\Users\Tomek\source\repos\ObjectYamlMapper\ConsoleApp\Files";
            string filePath = Path.Combine(folderPath, "test.yaml");

            Console.WriteLine("Starting serialization");
            string serializedObject = yamlSerializer.Serialize(person);
            Console.WriteLine("Serialized object: ");
            Console.WriteLine(serializedObject);
        }
    }

    internal class Person
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }
        public List<string> Projects { get; set; }

    }
}
