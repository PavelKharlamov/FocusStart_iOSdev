using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FocusStartTest_iOSdev
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("АВТОМОБИЛЬНЫЙ СПРАВОЧНИК 1.0 \n" +
                              "ГЛАВНОЕ МЕНЮ: \n" +
                              "1 - Редактировать информацию об автомобилях \n" +
                              "2 - Добавить новый автомобиль \n" +
                              "3 - Удалить автомобиль \n" +
                              "4 - Выход \n");

            string command = "";

            try
            {
                Console.WriteLine("Информация об автомобилях:");

                using (StreamReader carName = new StreamReader("carName.txt"))
                {
                    String autoName = "";
                    String autoInfo = "";
                    int count = File.ReadAllLines("carName.txt").Length;

                    using (StreamReader carInfo = new StreamReader("carInfo.txt"))
                    {
                        for (int n = 1; n <= count; n++)
                        {
                                autoName = carName.ReadLine();
                                autoInfo = carInfo.ReadLine();
                                Console.WriteLine(n + ". " + autoName + ": " + autoInfo);
                        }
                    }                  
                }

                Console.WriteLine();
                Console.WriteLine("Выберите действие (1-4):");
                command = Console.ReadLine();

                if (command == "1") // Редактирование информации выбранного автомобиля
                {
                    Console.WriteLine("Введите номер автомобиля");
                    command = Console.ReadLine();
                    string[] linesCarInfo = File.ReadAllLines("carInfo.txt");
                    string lineCarInfo = linesCarInfo[Convert.ToInt32(command) - 1];
                    Console.WriteLine(lineCarInfo);

                    Console.WriteLine("Введите новую информацию для выбранного автомобиля");
                    string newInfo = Console.ReadLine();

                    string str = string.Empty;
                    using (StreamReader reader = File.OpenText("carInfo.txt"))
                    {
                        str = reader.ReadToEnd();
                    }
                    str = str.Replace(lineCarInfo, newInfo);

                    using (StreamWriter file = new StreamWriter("carInfo.txt"))
                    {
                        file.Write(str);
                    }

                    Console.WriteLine("Запись успешно отредактирована. Возвращение в Главное меню. Нажмите любую клавишу");
                    Console.ReadKey();
                    Process.Start(Assembly.GetExecutingAssembly().Location);
                    Environment.Exit(0);
                }

                else if (command == "2") // Добавление нового автомобиля 
                {
                    Console.WriteLine("Введите название нового автомобиля");
                    string newAutoName = Console.ReadLine();

                    StreamWriter writerName = new StreamWriter("carName.txt", true);
                    writerName.WriteLine(newAutoName);
                    writerName.Close();

                    Console.WriteLine("Введите информацию о новом автомобиле");
                    string newAutoInfo = Console.ReadLine();

                    StreamWriter writerInfo = new StreamWriter("carInfo.txt", true);
                    writerInfo.WriteLine(newAutoInfo);
                    writerInfo.Close();

                    Console.WriteLine("Новая запись успешно добавлена. Переход в главное меню. Нажмите любую клавишу");
                    Console.ReadKey();

                    Process.Start(Assembly.GetExecutingAssembly().Location);
                    Environment.Exit(0);
                }

                else if (command == "3") // Удаление записи
                {
                    Console.WriteLine("Введите номер автомобиля");
                    command = Console.ReadLine();
                    int line_to_delete = Convert.ToInt32(command) - 1;
                    string[] linesCarName = File.ReadAllLines("carName.txt");
                    string[] linesCarInfo = File.ReadAllLines("carInfo.txt");
                    string lineCarName = linesCarName[line_to_delete];
                    string lineCarInfo = linesCarInfo[line_to_delete];
                    Console.WriteLine(lineCarName + ": " + lineCarInfo);

                    Console.WriteLine("Удалить выбранную запись? (y/n)");
                    command = Console.ReadLine();

                    if (command == "y")
                    {
                        string tempFile = Path.GetTempFileName();

                        using (var sr = new StreamReader("carName.txt"))
                        using (var sw = new StreamWriter(tempFile))
                        {
                            string line;

                            while ((line = sr.ReadLine()) != null)
                            {
                                if (line != lineCarName)
                                    sw.WriteLine(line);
                            }
                        }

                        File.Delete("carName.txt");
                        File.Move(tempFile, "carName.txt");

                        using (var sr = new StreamReader("carInfo.txt"))
                        using (var sw = new StreamWriter(tempFile))
                        {
                            string line;

                            while ((line = sr.ReadLine()) != null)
                            {
                                if (line != lineCarInfo)
                                    sw.WriteLine(line);
                            }
                        }

                        File.Delete("carInfo.txt");
                        File.Move(tempFile, "carInfo.txt");

                        Console.WriteLine("Запись успешно удалена. Возвращение в Главное меню. Нажмите любую клавишу");
                        Console.ReadKey();
                        Process.Start(Assembly.GetExecutingAssembly().Location);
                        Environment.Exit(0);
                    }

                    else if (command == "n") // Нет
                    {
                        Process.Start(Assembly.GetExecutingAssembly().Location);
                        Environment.Exit(0);
                    }

                    else // Ошибка. Перезапуск. Главное меню
                    {
                        Console.WriteLine("Некорректно. Переход в главное меню");
                        Console.ReadKey();
                        Process.Start(Assembly.GetExecutingAssembly().Location);
                        Environment.Exit(0);
                    }
                }

                else if (command == "4") // Выход
                {
                    Console.WriteLine("Выйти? (y/n)");
                    string exitAnswer = Console.ReadLine();
                    if (exitAnswer == "y") // Да
                    {
                        Environment.Exit(0);
                    }

                    else if (exitAnswer == "n") // Нет
                    {
                        Process.Start(Assembly.GetExecutingAssembly().Location);
                        Environment.Exit(0);
                    }

                    else // Ошибка. Перезапуск. Главное меню
                    {
                        Console.WriteLine("Некорректно. Переход в главное меню");
                        Console.ReadKey();
                        Process.Start(Assembly.GetExecutingAssembly().Location);
                        Environment.Exit(0);
                    }
                }

            }

            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }            
        }
    }
}
