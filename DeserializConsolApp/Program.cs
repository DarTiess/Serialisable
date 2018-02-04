using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLib;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DeserializConsolApp
{
    class Program
    {
        /*

e.	Создать новый проект (тип — консольное приложение) с именем «DeserializConsolApp». 
Добавить ссылку на библиотеку «ClassLib».

f.	В функции Main() произвести десериализацию коллекции, 
созданной в проекте с именем «SerializConsolApp» и вывести на экран.*/
        static void Main(string[] args)
        {

            BinaryFormatter formatter = new BinaryFormatter();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Дессериализаци файла listSerial.txt");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            using (FileStream fs = new FileStream(@"E:\listSerial.txt", FileMode.OpenOrCreate))
            {
                List<PC> deserComputers = (List<PC>)formatter.Deserialize(fs);
                foreach (PC p in deserComputers)
                {
                    Console.WriteLine("Модель :{0}, SN: {1}, {2}, HDD: {3}, RAM: {4}, Класс: {5}", p.Model, p.SN, p.Proccesor, p.HDDisk, p.RAMemory, p.Class);
                }
            }
        }
    }
}
