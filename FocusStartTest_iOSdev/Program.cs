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
                              "МЕНЮ: \n" +
                              "1 - Просмотреть информацию об автомобилях \n" +
                              "2 - Добавить новый автомобиль \n" +
                              "3 - Удалить автомобиль \n" +
                              "4 - Редактировать информацию об автомобиле \n" +
                              "5 - Выход \n");

            string command = Console.ReadLine();

            try
            {
                if (command == "1")
                {
                    Console.WriteLine("Информация об автомобилях:");

                    using (StreamReader carName = new StreamReader("carName.txt"))
                    {
                        String autoName = "";
                        String autoInfo = "";
                        int count = System.IO.File.ReadAllLines("carName.txt").Length;

                        using (StreamReader carInfo = new StreamReader("carInfo.txt"))
                        {
                            for (int n = 1; n <= count; n++)
                            {
                                autoName = carName.ReadLine();
                                autoInfo = carInfo.ReadLine();
                                Console.WriteLine(n + ". " + autoName + ": " + autoInfo);
                            }
                        }
                        Console.WriteLine();
                        Console.WriteLine("Редактировать информацию об автомобиле или выйти в главное меню? (r/e)");
                        command = Console.ReadLine();

                        if (command == "r")
                        {
                            Console.WriteLine("Напишите номер автомобиля");
                            command = Console.ReadLine();


                        }

                        else if (command == "e")
                        {
                            Process.Start(Assembly.GetExecutingAssembly().Location);
                            Environment.Exit(0);
                        }

                        else
                        {
                            Console.WriteLine("Ошибка. Приложение закрывается. Нажмите любую клавишу");
                            Console.ReadKey();
                            Environment.Exit(0);
                        }
                    }
                }

                else if (command == "2")
                {
                    Console.WriteLine("ДЕЙСТВИЕ ДВА");
                }

                else if (command == "3")
                {
                    Console.WriteLine("ДЕЙСТВИЕ ТРИ");
                }

                else if (command == "4")
                {
                    Console.WriteLine("ДЕЙСТВИЕ ЧЕТЫРЕ");
                }

                else if (command == "5") // Выход
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
                        Console.WriteLine("Некорректно");
                        Console.ReadKey();
                        Process.Start(Assembly.GetExecutingAssembly().Location);
                        Environment.Exit(0);
                    }
                }

                else
                {
                    Console.WriteLine("ОШИБКА");
                }

            }

            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
               
        }
    }
}
