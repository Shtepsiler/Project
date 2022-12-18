using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;

namespace Project
{
    internal class Database
    {
        int MyId=0;
       static SqlConnection sqlConnection = new SqlConnection(@"Data Source=LAPTOP-2CTM7RH1\SHTEPSILL;Initial Catalog=accessories;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        static string connectionstring = @"Data Source=LAPTOP-2CTM7RH1\SHTEPSILL;Initial Catalog=accessories;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public bool TryConnect()
        {
            bool chek = false;
            using (SqlConnection sqlConnection = new SqlConnection(@"Data Source=LAPTOP-2CTM7RH1\SHTEPSILL;Initial Catalog=accessories;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                sqlConnection.Open();
                chek = true;
                return chek;
            }

        }

        public int Connect()
        {
            Console.OutputEncoding = UTF8Encoding.UTF8;
            Console.WriteLine("увійти(0)/зареєструватися(1)");
            int v = int.Parse(Console.ReadLine());
            do
            {

                switch (v)
                {
                    case 0:
                        {
                            Console.Clear();
                            Console.WriteLine("Введіть email");
                            string email = Console.ReadLine();
                            Console.WriteLine("Введіть пароль");
                            string password = Console.ReadLine();

                            sqlConnection.Open();
                            var userlist = sqlConnection.Query<Models.User>("select * from [User]");
                            sqlConnection.Close();
                            foreach (var user in userlist)
                            {
                                if (user.Email == email)
                                {
                                    bool isright = false;
                                    do
                                    {
                                        if (user.Password == password)
                                        {
                                            Console.WriteLine("Ви ввійшли успішно");
                                            isright = true;
                                            MyId = user.Usr_Id;
                                            return MyId;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Ви не ввійшли, введіть правильний пароль");
                                            password = Console.ReadLine();

                                        }
                                    }
                                    while (!isright);
                                }
                                else
                                {
                                    Console.WriteLine("Користпувача не знайдено, зареєструвати?(1-так 0-ні)");
                                    if(int.Parse(Console.ReadLine()) == 1)
                                    v = 1;
                                    else
                                        v = 0;
                                    break;
                                }
                            }

                            break;
                        }
                    case 1:
                        {

                            Console.WriteLine("Процесс реєстрації:");
                            Console.WriteLine("Введіть ім'я");
                            string name = Console.ReadLine();
                            Console.WriteLine("Введіть прізвище");
                            string surname = Console.ReadLine();
                            Console.WriteLine("Введіть email");
                            string email = Console.ReadLine();
                            string pass;
                            bool isright = false;
                            do
                            {
                                Console.WriteLine("Ведіть пароль");
                                pass = Console.ReadLine();
                                Console.WriteLine("Повторіть пароль");
                                string passr = Console.ReadLine();
                                if (pass == passr)
                                    isright = true;
                            } while (!isright);

                            sqlConnection.Open();
                            var userlist = sqlConnection.Query<Models.User>("insert into [User] values(@name,@surname,@email,@pass)", new
                            {
                                @name = name,
                                @surname = surname,
                                @email = email,
                                @pass = pass
                            });
                            Console.WriteLine("Нового користувача зареєстровано");

                            sqlConnection.Close();
                            break;
                        }
                }
            }
            while (MyId == 0);



            return MyId;
        }

        public void AddToBusket()
        {
            List<Models.Computer> computers = GetComputer();

            int n = 0;
            foreach (var computer in computers)
            {
                Console.WriteLine(++n);
                Console.WriteLine(GetProcessorsByID(computer.P_Id));
                Console.WriteLine(GetMotherboardByID(computer.M_Id));

                Console.WriteLine(GetGraphic_CardByID(computer.GC_Id));

                Console.WriteLine(GetHard_DriveByID(computer.H_Id));

                Console.WriteLine(GetSsdByID(computer.S_Id));

                Console.WriteLine(GetPSUByID(computer.PSU_Id));

                Console.WriteLine(GetRAMByID(computer.R_Id));
                Console.WriteLine();
            }
            Console.WriteLine("Бажаєте додати в улюблені?(1-так 0-ні)");
            if (int.Parse(Console.ReadLine()) == 1)
            {
                Console.WriteLine("Оберіть номер збірки");
                int numb = int.Parse(Console.ReadLine());
                var comp = computers[numb];
                using (sqlConnection)
                {
                    try
                    {
                        try
                        {
                            sqlConnection.Query("insert into Computer([M_Id],[P_Id],[GC_Id],[R_Id],[PSU_Id],[S_Id],[H_Id],[GPrice]) values(@m,@p,@gc,@r,@psu,@s,@h,@price)", new
                            {
                                @m = comp.M_Id,
                                @p = comp.P_Id,
                                @gc = comp.GC_Id,
                                @r = comp.R_Id,
                                @psu = comp.PSU_Id,
                                @s = comp.S_Id,
                                @h = comp.H_Id,
                                @price = comp.GPrice
                            });
                        }
                        catch
                        {

                        }

                        int c_id = sqlConnection.QueryFirstOrDefault<int>("select Id from Computer where [M_Id]=@m and [P_Id]=@p and [GC_Id]=@gc and [R_Id]= @r and [PSU_Id]=@psu and [S_Id]=@s and [H_Id]=@h and [GPrice]=@price", new
                        {
                            @m = comp.M_Id,
                            @p = comp.P_Id,
                            @gc = comp.GC_Id,
                            @r = comp.R_Id,
                            @psu = comp.PSU_Id,
                            @s = comp.S_Id,
                            @h = comp.H_Id,
                            @price = comp.GPrice
                        });
                        try
                        {
                            sqlConnection.Query("insert into User_Computers([Usr_Id],[Comp_Id]) values(@u_id,@c_id)", new { @u_id = MyId, @c_id = c_id });
                            Console.WriteLine("Збірка успішно додана");
                        }
                        catch
                        {
                            Console.WriteLine("Збірка вже була додана або щось пішло не так");
                        }




                            }
                    catch
                    {

                    }
                }
            }
            else
            { 

            }

        }

        public List<Models.Computer> GetComputer()
        {
            Console.WriteLine("Введіть ціну комп'ютера");
            int price = int.Parse(Console.ReadLine());
            using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
            {
                var list = sqlConnection.Query<Models.Computer>("GetComputer @price", new { @price = price }).ToList();
                return list;
            }

        }

        public List<Models.Computer> GetFavorite()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
            {
                var list = sqlConnection.Query<Models.Computer>("select Computer.[Id], [M_Id],[P_Id],[GC_Id],[R_Id],[PSU_Id],[S_Id],[H_Id],[GPrice] from Computer left join User_Computers on Computer.[Id]=User_Computers.[Comp_Id] where User_Computers.[Usr_Id]=@id  ", new {@id=MyId}).ToList();
               

                foreach (var computer in list)
                {
                    Console.WriteLine(computer.Id);
                    Console.WriteLine(GetProcessorsByID(computer.P_Id));
                    Console.WriteLine(GetMotherboardByID(computer.M_Id));

                    Console.WriteLine(GetGraphic_CardByID(computer.GC_Id));

                    Console.WriteLine(GetHard_DriveByID(computer.H_Id));

                    Console.WriteLine(GetSsdByID(computer.S_Id));

                    Console.WriteLine(GetPSUByID(computer.PSU_Id));

                    Console.WriteLine(GetRAMByID(computer.R_Id));
                    Console.WriteLine();
                } 
                return list;
            }

        }



        public List<Models.Accessories.Processor> GetProcessors()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
            {
                var list = sqlConnection.Query<Models.Accessories.Processor>("select [P_Id],[Brand],[Model],[Generation],[Socket],[Power],[Price] from [Processor] ").ToList();
                return list;
            }
           
        }
        public List<Models.Accessories.Motherboard> GetMotherboard()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
            {
                var list = sqlConnection.Query<Models.Accessories.Motherboard>("select [M_Id],[Brand],[Model],[Socket],[Graphic_Interface],[Ram_type],[Price],[Power] from [Motherboard] ").ToList();
                return list;
            }

        }
        public List<Models.Accessories.Graphic_Card> GetGraphic_Card()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
            {
                var list = sqlConnection.Query<Models.Accessories.Graphic_Card>("select [GC_Id],[Brand],[Model],[Interface],[Price],[Power] from [Graphic_Card] ").ToList();
                return list;
            }
            

        }
        public List<Models.Accessories.Hard_Drive> GetHard_Drive()
        {
        using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
        {
            var list = sqlConnection.Query<Models.Accessories.Hard_Drive>("select [H_Id],[Brand],[Model],[Size],[Interface],[Price] from [Hard_Drive] ").ToList();
            return list;
        }

        }
        public List<Models.Accessories.Ssd> GetSsd()
        {
        using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
        {
            var list = sqlConnection.Query<Models.Accessories.Ssd>("select [S_Id],[Brand],[Model],[Size],[Interface],[Price] from [Ssd] ").ToList();
            return list;
        }

        }
        public List<Models.Accessories.PSU> GetPSU()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
            {
                var list = sqlConnection.Query<Models.Accessories.PSU>("select [PSU_Id],[Brand],[Model],[Power],[Price] from [PSU] ").ToList();
                return list;
            }

        }
        public List<Models.Accessories.RAM> GetRAM()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
            {
                var list = sqlConnection.Query<Models.Accessories.RAM>("select [R_Id],[Brand],[Model],[Capacity],[Type],[Price] from [RAM] ").ToList();
                return list;
            }

        }

        public Models.Accessories.Processor GetProcessorsByID(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
            {
                var list = sqlConnection.QueryFirstOrDefault<Models.Accessories.Processor>("select [P_Id],[Brand],[Model],[Generation],[Socket],[Power],[Price] from [Processor] where P_Id = @id ", new { @id = id });
                return list;
            }

        }
        public Models.Accessories.Motherboard GetMotherboardByID(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
            {
                var list = sqlConnection.QueryFirstOrDefault<Models.Accessories.Motherboard>("select [M_Id],[Brand],[Model],[Socket],[Graphic_Interface],[Ram_type],[Price],[Power] from [Motherboard] where M_Id = @id ", new { @id = id });
                return list;
            }

        }
        public Models.Accessories.Graphic_Card GetGraphic_CardByID(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
            {
                var list = sqlConnection.QueryFirstOrDefault<Models.Accessories.Graphic_Card>("select [GC_Id],[Brand],[Model],[Interface],[Price],[Power] from [Graphic_Card]  where GC_Id = @id", new { @id = id });
                return list;
            }

        }
        public Models.Accessories.Hard_Drive GetHard_DriveByID(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
            {
                var list = sqlConnection.QueryFirstOrDefault<Models.Accessories.Hard_Drive>("select [H_Id],[Brand],[Model],[Size],[Interface],[Price] from [Hard_Drive]  where H_Id = @id", new { @id = id });
                return list;
            }

        }
        public Models.Accessories.Ssd GetSsdByID(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
            {
                var list = sqlConnection.QueryFirstOrDefault<Models.Accessories.Ssd>("select [S_Id],[Brand],[Model],[Size],[Interface],[Price] from [Ssd]  where S_Id = @id", new { @id = id });
                return list;
            }

        }
        public Models.Accessories.PSU GetPSUByID(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
            {
                var list = sqlConnection.QueryFirstOrDefault<Models.Accessories.PSU>("select [PSU_Id],[Brand],[Model],[Power],[Price] from [PSU]  where PSU_Id = @id", new { @id = id });
                return list;
            }

        }
        public Models.Accessories.RAM GetRAMByID(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
            {
                var list = sqlConnection.QueryFirstOrDefault<Models.Accessories.RAM>("select [R_Id],[Brand],[Model],[Capacity],[Type],[Price] from [RAM] where R_Id = @id ", new { @id = id });
                return list;
            }

        }



    }
}
