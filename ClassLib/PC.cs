using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib
{[Serializable]
   public class PC
    {
        /*6.	Создать библиотеку классов с именем «ClassLib». 

a.	В библиотеке «ClassLib» создать класс с именем «РС», 
описывающий компьютер. Данный класс должен включать:  
i.	3–4 поля (марка, серийный номер и т.д.),  
ii.	свойства (к каждому полю),  
iii.	конструкторы (по умолчанию, с параметрами),  
iv.	методы, моделирующие функционирование ПЭВМ (включение/выключение, перегрузку). */
        string model;
        string Serie;
        string processorType;
        int HDD;
        int RAM;
        string TypeOfClass;
        public string Model { get { return model; } set { model = value; } }
        public string SN { get { return Serie; }set { Serie = value; } }
        public string Proccesor { get { return processorType; } set {processorType = value; } }
        public int HDDisk { get { return HDD; } set { HDD = value; } }
        public int RAMemory { get { return RAM; }set { RAM = value; } }
        public string Class {
            get
            {
                if ((Proccesor == "i7")&&(RAMemory>4))
                {
                    TypeOfClass = "Gamer Class";
                }
                else if ((Proccesor == "i5")&&(RAMemory<=8))
                {
                    TypeOfClass = "Buisness Class";
                }

                else
                {
                    TypeOfClass = "Home Class/Education Class";
                }
                return TypeOfClass;
                        }
        }

        public PC() { }
        public PC(string model,string Serie,string processorType,int HDD,int RAM)
        {
           this.model=model;
            this.Serie=Serie;
            this.processorType=processorType;
            this.HDD=HDD;
            this.RAM=RAM;
            TypeOfClass=Class;
        }

        public void Show()
        {
            Console.Write(model + "   " + Serie + "   " + processorType + "   " + HDD + "GB  " + RAM + "GB  ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Class);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void SwitchOff(string str)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Компьютер " + model + " выключен\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void TurnOnn(string str)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Компьютер " + model + " включен\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Reboot(string str)
        {
            SwitchOff("y");
            TurnOnn("y");
        
            Console.WriteLine("Компьютер " + model + " перезагружен\n ");
           
        }


    }
}
