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

            Book book = new()
            {
                Title = "Lord of the Rings",
                Author = new()
                {
                    Firstname = "J.R.R.",
                    Lastname = "Tolkien"
                },
                Genre = Genre.Fantasy,
                ReleaseDate = new DateTime(1954, 7, 29).Date,
                Countries = new List<string> { "England", "Poland", "USA" },
            };

            string folderPath = @"C:\Users\Tomek\source\repos\ObjectYamlMapper\ConsoleApp\Files";
            string filePath = Path.Combine(folderPath, "test.yaml");

            string serializedObject = yamlSerializer.Serialize(book);
            Console.WriteLine("Serialized object: ");
            Console.WriteLine(serializedObject);

            if (!File.Exists(filePath))
            {
                var file = File.Create(filePath);
                file.Close();
            }
            File.WriteAllText(filePath, serializedObject);
        }
    }

    internal class Book
    {
        public string Title { get; set; }
        public Author Author { get; set; }
        public Genre Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<string> Countries { get; set; }
    }

    internal class Author
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }

    internal enum Genre
    {
        Action,
        Adventure,
        Fantasy,
        Historical,
        Horror,
        ScienceFiction
    }
}
