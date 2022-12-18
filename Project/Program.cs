using System;

namespace Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();
            if (database.TryConnect())
            {
                database.Connect();
                int comand=0;
                    
                do
                {
                  
                   Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("Оберіть команду:");
                    Console.WriteLine("0 - вийти з програми");
                    Console.WriteLine("1 - отримати список процесорів");
                    Console.WriteLine("2 - отримати список материнських плат");
                    Console.WriteLine("3 - отримати список графічних крат");
                    Console.WriteLine("4 - оримати список оперативної пам'яті");
                    Console.WriteLine("5 - отримати список Ssd-дисків");
                    Console.WriteLine("6 - отримати список Hdd-дисків");
                    Console.WriteLine("7 - отримати список блоків живлення");
                    Console.WriteLine("8 - отримати список комп'ютерів за ціною");
                    Console.WriteLine("9 - отримати список вибраних комп'ютерів");


                    try
                    {
                        comand = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        comand = int.Parse(Console.ReadLine());
                    }
                    switch (comand)
                    {
                        case 1:
                            {
                                var list1 = database.GetProcessors();
                                foreach (var cpu in list1)
                                    Console.WriteLine(cpu);
                                Console.WriteLine();
                                break;
                            }
                        case 2:
                            {
                                var list2 = database.GetMotherboard();
                                foreach (var mb in list2)
                                    Console.WriteLine(mb);
                                Console.WriteLine();

                                break;
                            }
                        case 3:
                            {
                                var list3 = database.GetGraphic_Card();
                                foreach (var gc in list3)
                                    Console.WriteLine(gc);
                                Console.WriteLine();

                                break;
                            }
                        case 4:
                            {
                                var list4 = database.GetRAM();
                                foreach (var ram in list4)
                                    Console.WriteLine(ram);
                                Console.WriteLine();

                                break;
                            }
                        case 5:
                            {
                                var list5 = database.GetSsd();
                                foreach (var ssd in list5)
                                    Console.WriteLine(ssd);
                                Console.WriteLine();

                                break;
                            }
                        case 6:
                            {
                                var list6 = database.GetHard_Drive();
                                foreach (var hdd in list6)
                                    Console.WriteLine(hdd);
                                Console.WriteLine();

                                break;
                            }
                        case 7:
                            {
                                var list7 = database.GetPSU();
                                foreach (var psu in list7)
                                    Console.WriteLine(psu);
                                Console.WriteLine();

                                break;
                            }
                        case 8: 
                            {
                                database.AddToBusket();
                                break; 
                            }
                        case 9:
                            {
                                database.GetFavorite();
                                break;
                            }
                            }
                   






                } while (comand!=0);
            }

        }
    }
}
