using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    internal class Models
    {
        public class User
        {
            public int Usr_Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
        public class Computer {
            public int Id { get; set; }
            public int M_Id { get; set; }
            public int P_Id { get; set; }
            public int GC_Id { get; set; }
            public int R_Id { get; set; }
            public int PSU_Id { get; set; }
            public int S_Id { get; set; }
            public int H_Id { get; set; }
            public int GPrice { get; set; }
        }
        public class Busket
        {
            public int Id { get; set; }
            public int Usr_Id { get; set; }
            public int Comp_Id { get; set; }

        }
        public class Accessories
        {
            public class Processor
            {
                public int P_Id { get; set; }
                public string Brand { get; set; }
                public string Model { get; set; }
                public int Generation { get; set; }
                public string Socket { get; set; }
                public string Graphic_Interface { get; set; }
                public int Power { get; set; }
                public int Price { get; set; }

                public override string ToString()
                {
                    return (this.P_Id + " " + this.Brand + " " + this.Model + " " + this.Generation + " " + this.Socket + " "+this.Graphic_Interface +" "+ this.Power + " " + this.Price);
                }
            }
            public class Motherboard
            {
                public int M_Id { get; set; }
                public string Brand { get; set; }
                public string Model { get; set; }
                public string Socket { get; set; }
                public string Ram_type { get; set; }
                public int Power { get; set; }
                public int Price { get; set; }
                
                public override string ToString()
                {
                    return (this.M_Id + " " + this.Brand + " " + this.Model + " " + this.Socket + " " + this.Ram_type + " " + this.Power + " " + this.Price);
                }
            }
            public class Graphic_Card
            {
                public int GC_Id { get; set; }
                public string Brand { get; set; }
                public string Series { get; set; }
                public string Model { get; set; }
                public int Price { get; set; }
                public int Power { get; set; }
                public override string ToString()
                {
                    return (this.GC_Id + " " + this.Brand + " " + this.Series + " " + this.Model + " " + this.Power + " " + this.Price);
                }
            }
            public class Hard_Drive
            {
                public int H_Id { get; set; }
                public string Brand { get; set; }
                public string Model { get; set; }
                public int Size { get; set; }
                public string Interface { get; set; }
                public int Price { get; set; }
                public override string ToString()
                {
                    return (this.H_Id + " " + this.Brand + " " + this.Model + " " + this.Size + " " + this.Interface + " " + this.Price);
                }
            }
            public class Ssd
            {
                public int S_Id { get; set; }
                public string Brand { get; set; }
                public string Model { get; set; }
                public string Size { get; set; }
                public string Interface { get; set; }
                public int Price { get; set; }
                public override string ToString()
                {
                    return (this.S_Id + " " + this.Brand + " " + this.Model + " " + this.Size + " " + this.Interface + " " + this.Price);
                }
            }              
            public class PSU
            {
                public int PSU_Id { get; set; }
                public string Brand { get; set; }
                public string Model { get; set; }
                public int Power { get; set; }
                public int Price { get; set; }
                public override string ToString()
                {
                    return (this.PSU_Id + " " + this.Brand + " " + this.Model + " " + this.Power + " " + this.Price);
                }
            }

            public class RAM
            {
                public int R_Id { get; set; }
                public string Brand { get; set; }
                public string Model { get; set; }
                public int Capacity { get; set; }
                public string Type { get; set; }
                public int Price { get; set; }
                public override string ToString()
                {
                    return (this.R_Id + " " + this.Brand + " " + this.Model + " " + this.Capacity + " " + this.Type + " " + this.Price);
                }
            }
        }
    }
}
