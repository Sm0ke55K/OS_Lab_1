using System;
using System.IO;
using System.IO.Compression;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace HelloApp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }
        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Вывести информацию о логических дисках");
            Console.WriteLine("2) Работа с файлами");
            Console.WriteLine("3) Работа с форматом JSON");
            Console.WriteLine("4) Работа с форматом XML");
            Console.WriteLine("5) Создание zip архива и операции с ним");
            Console.WriteLine("6) Exit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    InfoDisck();
                    return true;
                case "2":
                    WorkingWithFiles();
                    return true;
                case "3":
                    JSONAsync();
                    return true;
                case "4":
                    XML();
                    return true;
                case "5":
                    ZIP();
                    return true;
                case "6":
                    return false;
                default:
                    return true;
            }
        }
        private static void InfoDisck()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            Console.Clear();

            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine($"{drive.Name}");
                if (drive.IsReady)
                {
                    Console.WriteLine($"Объем диска: {drive.TotalSize}");
                    Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace}");
                    Console.WriteLine($"Метка: {drive.VolumeLabel}");
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Press <Enter> to continue...");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
        }
        private static int WorkingWithFiles()
        {
        Found:
            Console.Clear();
            Console.WriteLine($"***Работа с файлами***\n");
            Console.WriteLine($"Choose an option:");
            Console.WriteLine($"1) Создать файл:");
            Console.WriteLine($"2) Записать в файл строку, введённую пользователем:");
            Console.WriteLine($"3) Прочитать файл в консоль:");
            Console.WriteLine($"4) Удалить файл:");
            Console.WriteLine($"5) Exit to the menu");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine($"Введите путь где будет создан файл и его имя:");
                    string path = Console.ReadLine();
                    using (FileStream fs = File.Create(path))
                        Console.WriteLine("Файл создан");
                    Console.WriteLine($"Press <Enter> to continue...");
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                    goto Found;

                case "2":
                    Console.Clear();
                    Console.WriteLine($"Введите путь файла для записи строки:");
                    string path2 = Console.ReadLine();
                    Console.WriteLine("Введите строку для записи в файл:");
                    string text = Console.ReadLine();

                    using (FileStream fstream = new FileStream(path2, FileMode.OpenOrCreate))
                    {
                        byte[] array = System.Text.Encoding.Default.GetBytes(text);
                        fstream.Write(array, 0, array.Length);
                        Console.WriteLine("Текст записан в файл");
                        Console.WriteLine($"Press <Enter> to continue...");
                        while (Console.ReadKey().Key != ConsoleKey.Enter) { }

                    }
                    goto Found;

                case "3":
                    Console.Clear();
                    Console.WriteLine($"Введите путь файла:");
                    string path3 = Console.ReadLine();
                    using (FileStream fstream = File.OpenRead(path3))
                    {
                        byte[] array = new byte[fstream.Length];
                        fstream.Read(array, 0, array.Length);
                        string textFromFile = System.Text.Encoding.Default.GetString(array);
                        Console.WriteLine($"\nТекст из файла: {textFromFile}\n");
                        Console.WriteLine($"Press <Enter> to continue...");
                        while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                        goto Found;
                    }
                case "4":
                    Console.Clear();
                    Console.WriteLine($"Введите путь файла:");
                    string path4 = Console.ReadLine();
                    FileInfo fileInf = new FileInfo(path4);
                    if (fileInf.Exists)
                    {
                        File.Delete(path4);
                    }
                    Console.WriteLine($"Подтвердите удаление файла");
                    Console.WriteLine($"\nФайл удалён...");
                    Console.WriteLine($"Press <Enter> to continue...");
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                    goto Found;

                case "5":
                    return 0;
            }

            Console.WriteLine($"Press <Enter> to continue...");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            return 0;
        }

        
    class Student
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public int Year { get; set; }
        public string Group { get; set; }
    }
        static async Task<int> JSONAsync()
        {
        Found:
            Console.Clear();
            Console.WriteLine($"***Работа с файлами формата JSON***\n");
            Console.WriteLine($"Choose an option");
            Console.WriteLine($"1) Создать файл JSON");
            Console.WriteLine($"2) Выполнить сериализацию объекта");
            Console.WriteLine($"3) Прочитать файл в консоль");
            Console.WriteLine($"4) Удалить файл");
            Console.WriteLine($"5) Exit to the menu");
            Console.Write("\r\nSelect an option: ");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine($"Введите путь где будет создан файл:");
                    string pathh = Console.ReadLine();
                    processJsonFile(pathh);
                    static void processJsonFile(string pathh)
                    {
                        Console.WriteLine("3.Работа с форматом JSON ");
                        FileInfo fileJSON = new FileInfo(pathh);
                        try
                        {
                            using (FileStream fStream = File.Create(pathh))
                            {
                                Console.WriteLine($"\tФайл, создан по пути: {pathh}");
                                //если файл создан, получить информацию о файле
                                if (fileJSON.Exists)
                                {
                                    Console.WriteLine("\tИмя файла: {0}", fileJSON.Name);
                                    Console.WriteLine("\tВремя создания: {0}", fileJSON.CreationTime);
                                    Console.WriteLine("\tРазмер: {0}", fileJSON.Length);
                                    Console.WriteLine();
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        try
                        {
                            //запись данных
                            using (StreamWriter sw = new StreamWriter(pathh, true, System.Text.Encoding.Default))
                            {
                                Student student = new Student();
                                Console.Write("\tВведите имя студента: ");
                                student.Name = Console.ReadLine();
                                Console.Write("\tВведите фамилию студента: ");
                                student.SurName = Console.ReadLine();
                                Console.Write("\tВведите группу студента: ");
                                student.Group = Console.ReadLine();
                                while (true)
                                {
                                    Console.Write("\tВведите год поступления студента: ");
                                    string year = Console.ReadLine();
                                    if (int.TryParse(year, out int number))
                                    {
                                        student.Year = number;
                                        break;
                                    }
                                    Console.Write("Вы ввели не число, введите число еще раз: ");
                                }
                                sw.WriteLine(JsonSerializer.Serialize<Student>(student));
                            }
                            //чтение данных
                            using (StreamReader sr = new StreamReader(pathh))
                            {
                                Console.Write("\tИнформация из файла:\n ");
                                Student restoredStudent = JsonSerializer.Deserialize<Student>(sr.ReadToEnd());
                                Console.WriteLine($"\t\tName: {restoredStudent.Name}\n\t\tSurname: {restoredStudent.SurName}");
                                Console.WriteLine($"\t\tGroup: {restoredStudent.Group}\n\t\tYear: {restoredStudent.Year}");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    Console.WriteLine($"Press <Enter> to continue...");
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                    goto Found;

                case "2":
                    Console.Clear();
                    Console.WriteLine($"Введите путь файла для записи строки:");
                    string path2 = Console.ReadLine();
                    using (StreamWriter sw = new StreamWriter(path2, true, System.Text.Encoding.Default))
                    {
                        Student student = new Student();
                        Console.Write("\tВведите имя студента: ");
                        student.Name = Console.ReadLine();
                        Console.Write("\tВведите фамилию студента: ");
                        student.SurName = Console.ReadLine();
                        Console.Write("\tВведите группу студента: ");
                        student.Group = Console.ReadLine();
                        while (true)
                        {
                            Console.Write("\tВведите год поступления студента: ");
                            string year = Console.ReadLine();
                            if (int.TryParse(year, out int number))
                            {
                                student.Year = number;
                                break;
                            }
                            Console.Write("Вы ввели не число, введите число еще раз: ");
                        }
                        sw.WriteLine(JsonSerializer.Serialize<Student>(student));
                    }
                    Console.WriteLine("Введите строку для записи в файл:");
                    string text = Console.ReadLine();
                    goto Found;

                case "3":
                    Console.Clear();
                    Console.WriteLine($"Введите путь файла:");
                    string path3 = Console.ReadLine();
                    using (StreamReader sr = new StreamReader(path3))
                    {
                        Console.Write("\tИнформация из файла:\n ");
                        Student restoredStudent = JsonSerializer.Deserialize<Student>(sr.ReadToEnd());
                        Console.WriteLine($"\t\tName: {restoredStudent.Name}\n\t\tSurname: {restoredStudent.SurName}");
                        Console.WriteLine($"\t\tGroup: {restoredStudent.Group}\n\t\tYear: {restoredStudent.Year}");
                    }
                    Console.ReadLine();
                    goto Found;

                case "4":
                    Console.Clear();
                    Console.WriteLine($"Введите путь файла:");
                    string path4 = Console.ReadLine();
                    FileInfo fileJSON = new FileInfo(path4);
                    Console.Write("Хотите удалить файл? (1/0): ");
                    int sigh = int.Parse(Console.ReadLine());
                    if (sigh == 1)
                    {
                        if (fileJSON.Exists)
                        {
                            fileJSON.Delete();
                            Console.WriteLine($"\tФайл по пути {path4} удален.");
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\tФайл по пути {path4} не удален.");
                    }
                    Console.WriteLine($"Подтвердите удаление файла");
                    Console.WriteLine($"\nФайл удалён...");
                    Console.WriteLine($"Press <Enter> to continue...");
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                    goto Found;

                case "5":
                    return 0;
            }
            Console.WriteLine($"Press <Enter> to continue...");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            return 0;
        }
    private static int XML()
        {
        Found2:
            Console.Clear();
            Console.WriteLine($"***Работа с форматом XML***\n");
            Console.WriteLine($"1) Создать файл формате XML");
            Console.WriteLine($"2) Записать в файл новые данные из консоли");
            Console.WriteLine($"3) Прочитать файл в консоль");
            Console.WriteLine($"4) Удалить файл");
            Console.WriteLine($"5) Exit to the menu");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    EditXmlFile();
                    goto Found2;

                case "2":
                    CreateXml();
                    goto Found2;

                case "3":
                    Console.WriteLine("\nВведите путь расположения документа: ");
                    string pachxml1 = Console.ReadLine();
                    XDocument xdoc1 = XDocument.Load(pachxml1);
                    Console.Clear();
                    Console.WriteLine(xdoc1);
                    Console.WriteLine($"Press <Enter> to continue...");
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                    goto Found2;

                case "4":
                    Console.Clear();
                    Console.WriteLine($"Введите путь файла:");
                    string path5 = Console.ReadLine();
                    FileInfo fileInf = new FileInfo(path5);
                    if (fileInf.Exists)
                    {
                        File.Delete(path5);
                    }
                    Console.WriteLine($"Подтвердите удаление файла");
                    Console.WriteLine($"\nФайл удалён...");
                    Console.WriteLine($"Press <Enter> to continue...");
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                    goto Found2;

                case "5":
                    return 0;
            }
            Console.WriteLine($"Press <Enter> to continue...");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            return 0;

        }

        private static void EditXmlFile()
        {
            XDocument xdoc = new XDocument(); //корневой элемент
            XElement MainElement = new XElement("people"); //первый элемент
            XElement Element = new XElement("person");
            XAttribute NameAttr = new XAttribute("name", "exampleName");
            XElement CompanyElem = new XElement("company", "exampleCompany");
            XElement AgeElem = new XElement("age", "exampleAGE");

            Element.Add(NameAttr);
            Element.Add(CompanyElem);
            Element.Add(AgeElem);

            MainElement.Add(Element);
            xdoc.Add(MainElement);
            Console.WriteLine("\nВведите путь создания документа: ");
            string pachxml = Console.ReadLine();
            xdoc.Save(pachxml);
            Console.WriteLine("Data saved");
            Console.WriteLine($"Press <Enter> to continue...");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
        }
        private static void CreateXml()
        {
            Console.WriteLine("\nВведите путь документа: ");
            string pachxml1 = Console.ReadLine();
            XDocument xdoc = XDocument.Load(pachxml1);
            XElement root = xdoc.Element("Elements");
            Console.WriteLine("Введите название элемента:\n>>");
            string Element1 = Console.ReadLine();
            Console.WriteLine("Введите название атрибута 1:\n>>");
            string Attribute1 = Console.ReadLine();
            Console.WriteLine("Введите значение атрибута 1:\n>>");
            string AttributeValue1 = Console.ReadLine();
            Console.WriteLine("Введите название атрибута 2:\n>>");
            string Attribute2 = Console.ReadLine();
            Console.WriteLine("Введите значение элемента:\n>>");
            string AttributeValue2 = Console.ReadLine();
            Console.WriteLine("Введите название атрибута 3:\n>>");
            string Attribute3 = Console.ReadLine();
            Console.WriteLine("Введите значение атрибута 3:\n>>");
            string AttributeValue3 = Console.ReadLine();
            root.Add(new XElement(Element1,
                                    new XAttribute(Attribute1, AttributeValue2),
                                    new XElement(Attribute2, AttributeValue2),
                                    new XElement(Attribute3, AttributeValue2)));
            xdoc.Save(pachxml1);
            // выводим xml-документ на консоль
            Console.WriteLine(xdoc);
            Console.WriteLine($"Press <Enter> to continue...");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }

        }
        private static int ZIP()
        {
        Found3:
            Console.Clear();
            Console.WriteLine($"***Создание zip архива и операции с ним***\n");
            Console.WriteLine($"1) Создать архив в форматер zip");
            Console.WriteLine($"2) Добавить файл, выбранный пользователем, в архив");
            Console.WriteLine($"3) Разархивировать файл и вывести данные о нем");
            Console.WriteLine($"4) Удалить файл и архив");
            Console.WriteLine($"5) Exit to the menu");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Введите путь для нового архива (без расширения):\n");
                    string ArchivePath = Console.ReadLine(); //путь архива
                    Console.WriteLine("Введите имя архива:\n");
                    string Archivename = Console.ReadLine(); // имя архива
                    string ArchiveDir = ArchivePath + Archivename;
                    string Arch = ArchivePath + Archivename + ".zip";
                    FileInfo fileInf = new FileInfo(Arch);

                    if (!fileInf.Exists)
                    {

                        if (!System.IO.Directory.Exists(Arch))
                        {
                            System.IO.Directory.CreateDirectory(ArchiveDir);
                            ZipFile.CreateFromDirectory(ArchiveDir, Arch);
                            Console.WriteLine("Архив *" + Archivename + "* создан.\n");
                        }
                    }

                    Console.WriteLine($"Press <Enter> to continue...");
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                    goto Found3;

                case "2":
                    Console.WriteLine("\nВведите ПУТЬ файла который нужно добавить в архив:");
                    string FilePath = Console.ReadLine(); //путь файла

                    string Dobavl = FilePath + ".zip";
                    ZipFile.CreateFromDirectory(FilePath, Dobavl);  // D:\test   D:\test.zip
                    Console.WriteLine("Файл добавлен в архив.\n");
                    Console.WriteLine($"Press <Enter> to continue...");
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                    goto Found3;
                case "3":

                    Console.WriteLine("\nВведите ПУТЬ архива(без расширения):");
                    string ArhNAME = Console.ReadLine(); //имя архива
                    string ArhPath = ArhNAME + ".zip";
                    string ArhNAME1 = ArhNAME + "_open";
                    ZipFile.ExtractToDirectory(ArhPath, ArhNAME1);  //   D:\test.zip   D:\test
                    Console.WriteLine("Архив *" + ArhPath + "* разархивирован.\n");
                    Console.WriteLine("Список файлов в каталоге:");
                    string[] files = Directory.GetFiles(ArhNAME);
                    foreach (string s in files)
                    {
                        Console.WriteLine(s);
                    }
                    Console.WriteLine("\nСписок каталогов в каталоге:");
                    string[] files1 = Directory.GetDirectories(ArhNAME);
                    foreach (string s in files)
                    {
                        Console.WriteLine(s);
                    }
                    Console.WriteLine($"\nPress <Enter> to continue...");
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { }

                    goto Found3;
                case "4":
                NO:
                    Console.WriteLine($"Введите путь архива который нужно удалить:");
                    string path4 = Console.ReadLine();
                    FileInfo fileInf1 = new FileInfo(path4);
                    if (fileInf1.Exists)
                    {
                        Console.WriteLine($"Подтвердите удаление файла Y/N");
                        string YN = Console.ReadLine();
                        if ((YN == "Y") || (YN == "y"))
                        {
                            File.Delete(path4);
                            Console.WriteLine($"\nФайл удалён...");
                            Console.WriteLine($"Press <Enter> to continue...");
                            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                        }
                        else
                        {
                            Console.WriteLine($"Отмена!");
                            Console.WriteLine($"Press <Enter> to continue...");
                            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                            goto NO;
                        }
                    }
                    goto Found3;

                case "5":
                    return 0;
            }

            Console.WriteLine($"Press <Enter> to continue...");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            return 0;
        }
    }
}