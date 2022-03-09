using System;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Xml.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace first_program
{
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
    class Program
    {
        static async Task Main(string[] args)
        {
            char choice_2;
            Console.WriteLine("Лабораторная работа №1");
            Console.WriteLine("Выполнила: Головина Ирина\n");
            do
            {
                Console.WriteLine("================================================================");
                Console.WriteLine("=  Нажмите на цифру - 1 - чтобы показать информацию о дисках   =");
                Console.WriteLine("=  Нажмите на цифру - 2 - чтобы выполнить действия с файлами   =");
                Console.WriteLine("=  Нажмите на цифру - 3 - чтобы сжать файл                     =");
                Console.WriteLine("=  Нажмите на цирфу - 4 - чтобы продемонстрировать XML формат  =");
                Console.WriteLine("=  Нажмите на цифру - 5 - чтобы продемонстрировать JSON формат =");
                Console.WriteLine("=  Нажмите на цифру - 0 - чтобы завершить выполнение программы =");
                Console.WriteLine("================================================================");
                Console.Write("Какую команду выполнить? Нажмите на цифру! ");
                int x = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("========================================\n");
                switch (x)
                {
                    //информация о дисках
                    case 1:
                        {
                            Console.WriteLine("Вы нажали на цифру - 1 - !");
                            DriveInfo[] drivers = DriveInfo.GetDrives();
                            foreach (DriveInfo drive in drivers)
                            {
                                Console.WriteLine($"Имя: {drive.Name}");
                                Console.WriteLine($"Тип: {drive.DriveType}");
                                if (drive.IsReady)
                                {
                                    Console.WriteLine($"Дисковое пространство - {drive.TotalSize}");
                                    Console.WriteLine($"Свободное место - {drive.TotalFreeSpace}");
                                    Console.WriteLine($"Путь - {drive.VolumeLabel}");
                                }
                                Console.WriteLine();
                            }
                            break;
                        }
                    //выполняем манипуляции над файлами
                    case 2:
                        {
                            Console.WriteLine("Вы нажали на цифру - 2 - !");
                            string path = @"/Users/alina/Desktop/практика1/";
                            Console.WriteLine("Напишите текст, чтобы записать его в файл:");
                            string text = Console.ReadLine();
                            // записываем в файл
                            using (FileStream fstream = new FileStream($"{path}text.txt", FileMode.OpenOrCreate))
                            {
                                //строку переводим в байты
                                byte[] array = System.Text.Encoding.Default.GetBytes(text);
                                // записываем массив байтов в файл
                                fstream.Write(array, 0, array.Length);
                                Console.WriteLine("Текст записан в файл!");
                            }
                            // чтение из файла
                            using (FileStream fstream = File.OpenRead($"{path}text.txt"))
                            {
                                // строку переводим в байты
                                byte[] array = new byte[fstream.Length];
                                // считываем данные
                                fstream.Read(array, 0, array.Length);
                                // декодируем байты в строку
                                string textFromFile = System.Text.Encoding.Default.GetString(array);
                                Console.WriteLine($"Текст из файла - {textFromFile}");
                            }
                            Console.WriteLine("Хотите удалить файл? Нажмите - Y - если да, - N - если нет: ");
                            switch (Console.ReadLine())
                            {
                                case "y":
                                    if (File.Exists($"{path}text.txt"))
                                    {
                                        File.Delete($"{path}text.txt");
                                        Console.WriteLine("Файл удален!");
                                    }
                                    else Console.WriteLine("Такого файла не существует!");
                                    break;
                                case "n":
                                    break;
                                default:
                                    Console.WriteLine("Введено некорректное значение; исправьте, пожалуйста, ошибку!");
                                    break;
                            }
                            Console.WriteLine();
                            break;
                        }
                    //сжимаем файл
                    case 3:
                        {
                            Console.WriteLine("Вы нажали на цифру - 3 - !");
                            string SourceFile = @"/Users/alina/Desktop/практика1/cat.txt";// исходный файл
                            string CompressedFile = @"/Users/alina/Desktop/практика1/book.gz";// сжатый файл
                            string TargetFile = @"/Users/alina/Desktop/практика1/cat_new.txt";// восстановленный файл
                                                                                                    // записываем текст в наш файл
                            string message = "Я - студентка МИРЭА";
                            await File.WriteAllTextAsync(SourceFile, message);
                            // сжимаем файл
                            Compress(SourceFile, CompressedFile);
                            // читаем из сжатого файла
                            Decompress(CompressedFile, TargetFile);
                            Console.WriteLine("Хотите удалить файлы? Нажмите - Y - если да, - N - если нет: ");
                            switch (Console.ReadLine())
                            {
                                case "y":
                                    if ((File.Exists(SourceFile) &&
                                         File.Exists(CompressedFile) && File.Exists(TargetFile)) == true)
                                    {
                                        File.Delete(SourceFile);
                                        File.Delete(CompressedFile);
                                        File.Delete(TargetFile);
                                        Console.WriteLine("Файлы удалены!");
                                    }
                                    else Console.WriteLine("Ошибка!\n Пожалйста, проверьте их наличие!");
                                    break;
                                case "n":
                                    break;
                                default:
                                    Console.WriteLine("Введено некорректное значение; исправьте, пожалуйста, ошибку!");
                                    break;
                            }
                            Console.WriteLine();
                            break;
                        }
                    // XML
                    case 4:
                        {
                            Console.WriteLine("Вы нажали на цифру - 4 - !");
                            XmlDocument xDoc = new XmlDocument();
                            XDocument xdoc = new XDocument();
                            Console.Write("Сколько USER'ов хотите создать?: ");
                            int count = Convert.ToInt32(Console.ReadLine());
                            XElement list = new XElement("users");
                            for (int i = 1; i <= count; i++)
                            {
                                XElement User = new XElement("user");
                                Console.Write("Введите имя USER'а: ");
                                XAttribute UserName = new XAttribute("name", Console.ReadLine());
                                Console.WriteLine();
                                Console.Write("Введите возраст USER'а: ");
                                XElement UserAge = new XElement("age", Convert.ToInt32(Console.ReadLine()));
                                Console.WriteLine();
                                Console.Write("Введите название компании USER'а: ");
                                XElement UserCompany = new XElement("company", Console.ReadLine());
                                Console.WriteLine();
                                User.Add(UserName);
                                User.Add(UserAge);
                                User.Add(UserCompany);
                                list.Add(User);
                            }
                            xdoc.Add(list);
                            xdoc.Save(@"/Users/alina/Desktop/практика1/users.xml");

                            Console.Write("Прочитать XML файл? Нажмите - Y - если да, - N - если нет: ");
                            switch (Console.ReadLine())
                            {
                                case "y":
                                    Console.WriteLine();
                                    xDoc.Load(@"/Users/alina/Desktop/практика1/users.xml");
                                    XmlElement xRoot = xDoc.DocumentElement;
                                    foreach (XmlNode xnode in xRoot)
                                    {
                                        if (xnode.Attributes.Count > 0)
                                        {
                                            XmlNode attr = xnode.Attributes.GetNamedItem("name");
                                            if (attr != null)
                                                Console.WriteLine($"Имя: {attr.Value}");
                                        }
                                        foreach (XmlNode childnode in xnode.ChildNodes)
                                        {
                                            if (childnode.Name == "age")
                                                Console.WriteLine($"Возраст: {childnode.InnerText}");
                                            if (childnode.Name == "company")
                                                Console.WriteLine($"Компания: {childnode.InnerText}");
                                        }
                                    }
                                    Console.WriteLine();
                                    break;
                                case "n":
                                    break;
                                default:
                                    Console.WriteLine("Введено некорректное значение; исправьте, пожалуйста, ошибку!");
                                    break;
                            }
                            Console.Write("Удалить созданный xml файл? y/n: ");
                            switch (Console.ReadLine())
                            {
                                case "y":
                                    FileInfo xmlfilecheck = new FileInfo(@"/Users/alina/Desktop/практика1/users.xml");
                                    if (xmlfilecheck.Exists)
                                    {
                                        xmlfilecheck.Delete();
                                        Console.WriteLine("Файл успешно удален!");
                                    }
                                    else Console.WriteLine("Такого файла не существует!");
                                    break;
                                case "n":
                                    break;
                                default:
                                    Console.WriteLine("Введено некорректное значение; исправьте, пожалуйста, ошибку!");
                                    break;
                            }
                            Console.WriteLine();
                            break;
                        }
                    case 5:
                        /*FIFTH - JSON*/
                        {
                            Console.WriteLine("Вы нажали на цифру - 5 - !");
                            // сохранение данных
                            using (FileStream fs = new FileStream(@"/Users/alina/Desktop/практика1/user.json", FileMode.OpenOrCreate))
                            {
                                Person p = new Person() { Name = "Alina", Age = 19 };
                                await JsonSerializer.SerializeAsync<Person>(fs, p);
                                Console.WriteLine("Информация в файл была записана заранее - она сохранена!");
                            }
                            // чтение данных
                            using (FileStream fs = new FileStream(@"/Users/alina/Desktop/практика1/user.json", FileMode.OpenOrCreate))
                            {
                                Person restoredPerson = await JsonSerializer.DeserializeAsync<Person>(fs);
                                Console.WriteLine($"Имя: {restoredPerson.Name}  Возраст: {restoredPerson.Age}");
                            }
                            Console.Write("Хотите удалить файл? Нажмите - Y - если да, - N - если нет: ");
                            switch (Console.ReadLine())
                            {
                                case "y":
                                    File.Delete(@"/Users/alina/Desktop/практика1/user.json");
                                    Console.WriteLine("\nФайл успешно удален!");
                                    break;
                                case "n":
                                    break;
                            }
                            break;
                        }
                    default:
                        Console.WriteLine("\nОШИБКА ВВОДА!");
                        break;
                }
                Console.WriteLine("================================");
                Console.Write("\nХотите продолжить работу? Нажмите - Y - если да, - N - если нет:");
                choice_2 = Convert.ToChar(Console.ReadLine());
                Console.Write('\n');
            } while (choice_2 != 'n');
        }
        public static void Compress(string sourceFile, string compressedFile)
        {
            // поток для чтения исходного файла
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
            {
                // поток для записи сжатого файла
                using (FileStream targetStream = File.Create(compressedFile))
                {
                    // поток архивации
                    using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                    {
                        sourceStream.CopyTo(compressionStream); // копируем байты из одного потока в другой
                        Console.WriteLine("Файла {0} был сжат завершено. Его первоначальный размер: {1}  размер после сжатия: {2}.",
                            sourceFile, sourceStream.Length.ToString(), targetStream.Length.ToString());
                    }
                }
            }
        }
        public static void Decompress(string compressedFile, string targetFile)
        {
            // поток для чтения из сжатого файла
            using (FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
            {
                // поток для записи восстановленного файла
                using (FileStream targetStream = File.Create(targetFile))
                {
                    // поток разархивации
                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(targetStream);
                        Console.WriteLine("Восстановлен файл: {0}", targetFile);
                    }
                }
            }
        }
    }
}

