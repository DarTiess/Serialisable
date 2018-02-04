using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLib;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
//b.	Создать новый проект (тип — консольное приложение) с именем «SerializConsolApp».
//Добавить ссылку на библиотеку «ClassLib».
namespace SerializConsolApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //c.В функции Main() данного проекта создать коллекцию (на базе обобщенного класса List<T> ) 
//и добавить в коллекцию 4–5 объектов класса «РС». 
            List<PC> Computers = new List<PC>();
            Computers.Add(new PC("Acer", "ACD25417458", "i5", 500, 4));
            Computers.Add(new PC("Lenovo", "NB4155NH4", "i7", 1000, 16));
            Computers.Add(new PC("HP", "JLP01482000", "i5", 1000, 8));
            Computers.Add(new PC("Samsug", "FDS2541782", "i3", 500, 8));
            foreach(var i in Computers)
            {
                i.Show();
            }
            Console.WriteLine();

            

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\t\tСистемой обнаружена сетевая атака,\n\t\t необходимо выключить ВСЕ компьютеры!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine("Нажите y - YES, для немедленного выключения;\n n - NO, для сохранения данных и выключения позднее");
                string answer;
                answer = Console.ReadLine();
                if ((answer == "y") || (answer == "Y"))
                {
                foreach (var i in Computers)
                {
                    i.SwitchOff(answer);
                }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Или перезагрузить комппьютер");
                    Console.ForegroundColor = ConsoleColor.White;
                    answer = Console.ReadLine();
                    if ((answer == "y") || (answer == "Y"))
                {
                    foreach (var i in Computers)
                    {
                        i.Reboot(answer);
                    }
                    }
                    else
                    {
                    foreach (var i in Computers)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ваш сеанс был прерван Системным Адинистратором.");
                        Console.ForegroundColor = ConsoleColor.White;
                        i.SwitchOff(answer);
                    }

                }
            }

           // d.Произвести сериализацию коллекции в бинарный файл с именем «listSerial.txt»в каталоге на диске D.
//В случае наличия аналогичного файла в каталоге старый файл перезаписать новым файлом
//и вывести об этом уведомление.

            BinaryFormatter formatter = new BinaryFormatter();
            using(FileStream fs=new FileStream(@"E:\listSerial.txt", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, Computers);
                Console.WriteLine("Объект сериализован успешно");
            }

          

        }
    }
}
