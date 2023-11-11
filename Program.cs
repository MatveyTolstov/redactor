using System.Data.Common;
using System.IO;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace Редактор
{
    internal class Program
    {
        static void Main()
        {
            List<Figure> figures = new List<Figure>();

            Console.WriteLine("Введите путь файла(и его расширение)");
            string f = Console.ReadLine();

            if (f.EndsWith(".txt"))
            {
                Console.Clear();
                Console.WriteLine("Нажмите S, чтобы сохранить в файл");
                string[] lines = File.ReadAllLines(f);

                for (int i = 0; i < lines.Length; i++)
                {
                    Console.WriteLine(lines[i]);
                }
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.S)
                {
                    Console.Clear();
                    Console.WriteLine("Введите путь файла , который хотите сохранить (и его расширение)");
                    string road = Console.ReadLine();
                    if (road.EndsWith(".json"))
                    {
                        Console.Clear() ;
                        Console.WriteLine("Файл успешно сохранен!");
                        lines = File.ReadAllLines(f);

                        for (int i = 0; i < lines.Length; i += 3)
                        {
                            Figure figure = new Figure();
                            {
                                figure.name = lines[i];
                                figure.lenth = lines[i + 1];
                                figure.width = lines[i + 2];

                            }
                            figures.Add(figure);
                        }
                        string json = JsonConvert.SerializeObject(figures);
                        File.WriteAllText(road, json);
                    }
                    else if (road.EndsWith(".xml"))
                    {
                        Console.Clear();
                        Console.WriteLine("Файл успешно сохранен!");
                        lines = File.ReadAllLines(f);


                        for (int i = 0; i < lines.Length; i += 3)
                        {
                            Figure figure = new Figure();
                            {
                                figure.name = lines[i];
                                figure.lenth = lines[i + 1];
                                figure.width = lines[i + 2];
                            };
                            figures.Add(figure);
                        }

                        XmlSerializer xml = new XmlSerializer(typeof(List<Figure>));
                        using (FileStream fs = new FileStream(road, FileMode.Create))
                        {
                            xml.Serialize(fs, figures);
                        }
                    }
                }


            }
            else if (f.EndsWith(".json"))
            {
                Console.Clear();
                Console.WriteLine("Нажмите S, чтобы сохранить в файл");



                string json = File.ReadAllText(f);
                figures = JsonConvert.DeserializeObject<List<Figure>>(json);
                foreach (var figure in figures)
                {
                    Console.WriteLine(figure.name);
                    Console.WriteLine(figure.lenth);
                    Console.WriteLine(figure.width);
                }



                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.S)
                {
                    Console.Clear();
                    Console.WriteLine("Введите путь файла , который хотите сохранить (и его расширение)");
                    string road = Console.ReadLine();
                    if (road.EndsWith(".txt"))
                    {
                        Console.Clear();
                        Console.WriteLine("Файл успешно сохранен!");
                        string txt = File.ReadAllText(f);
                        figures = JsonConvert.DeserializeObject<List<Figure>>(txt);

                        Console.WriteLine("Файл успешно сохранен!");
                        foreach (var figure in figures)
                        {
                            File.AppendAllText(road, figure.name + "\n");
                            File.AppendAllText(road, figure.lenth + "\n");
                            File.AppendAllText(road, figure.width + "\n");
                        }
                    }

                    else if (road.EndsWith(".xml"))
                    {
                        Console.Clear();
                        Console.WriteLine("Файл успешно сохранен!");
                        XmlSerializer serializer = new XmlSerializer(typeof(List<Figure>));
                        using (FileStream fs = new FileStream(road, FileMode.OpenOrCreate))
                        {
                            serializer.Serialize(fs, figures);
                        }
                    }
                }



            }
            else if (f.EndsWith(".xml"))
            {
                Console.Clear();
                Console.WriteLine("Нажмите S, чтобы сохранить в файл");
                XmlSerializer serializer = new XmlSerializer(typeof(List<Figure>));
                using (FileStream fs = new FileStream(f, FileMode.Open))
                {
                    figures = (List<Figure>)serializer.Deserialize(fs);
                    foreach (var figure in figures)
                    {
                        Console.WriteLine(figure.name);
                        Console.WriteLine(figure.lenth);
                        Console.WriteLine(figure.width);
                    }
                }
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.S)
                {
                    Console.Clear();
                    Console.WriteLine("Введите путь файла , который хотите сохранить (и его расширение)");
                    string road = Console.ReadLine();
                    if (road.EndsWith(".txt"))
                    {
                        Console.Clear();
                        Console.WriteLine("Файл успешно сохранен!");
                        XmlSerializer xml = new XmlSerializer(typeof(List<Figure>));
                        using (FileStream fs = new FileStream(f, FileMode.Open))
                        {
                            figures = (List<Figure>)xml.Deserialize(fs);
                        }

                        foreach (var figure in figures)
                        {
                            File.AppendAllText(road, figure.name + "\n");
                            File.AppendAllText(road, figure.lenth + "\n");
                            File.AppendAllText(road, figure.width + "\n");
                        }
                    }
                    else if (road.EndsWith(".json"))
                    {
                        Console.Clear();
                        Console.WriteLine("Файл успешно сохранен!");
                        XmlSerializer xml = new XmlSerializer(typeof(List<Figure>));
                        using (FileStream fs = new FileStream(f, FileMode.Open))
                        {
                            figures = (List<Figure>)xml.Deserialize(fs);
                        }
                        string json = JsonConvert.SerializeObject(figures);
                        File.WriteAllText(road, json);
                    }
                }
            }
        }
    }
}