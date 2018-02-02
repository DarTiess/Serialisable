using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Serialize
{
    // 1.	Объявить в консольном приложении класс Book 
    // с полями: название, стоимость, автор, год. 
    [Serializable]
    public class Book
    {
        public string name { get; set; }
        public int price { get; set; }
        public string author { get; set; }
        public int year { get; set; }
        
       public void Print()
        {
            Console.WriteLine("Наименование произведения: " + name + "\n\tцена: " + price + "\n\tавтор: " + author + "\n\tгод издания: " + year);
        }
    }
    [Serializable]
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Number { get; set; }
        public string BirthYear { get; set; }
        public void Print()
        {
            Console.WriteLine("Имя : " + FirstName + " Фамилия : " + LastName + " Номер телефона : " + Number + " Год рождения : " + BirthYear);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Создать коллекцию элементов Book и заполнить тестовыми данными. 
            //С помощью класса BinaryFormatter сохранить состояние приложения в двоичный файл.
            #region
            List<Book> books = new List<Book>();
            books.Add(new Book() { name = "Oliver Twist", price = 150, author = "Charles Dickens", year =1839 });
            books.Add(new Book() { name = "The Adventures of Tom Sawyer", price = 230, author = " Mark Twain", year = 1876 });
            books.Add(new Book() { name = "The Picture of Dorian Gray", price = 180, author = "Oscar Wilde", year = 1890 });
            books.Add(new Book() { name = "The Headless Horseman", price = 215, author = "Mayne Reid", year = 1866 });
            foreach(Book i in books)
            {
                i.Print();
            }
            Console.WriteLine();
            
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("Books.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, books);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Сериализация удалась!");
                Console.ForegroundColor = ConsoleColor.White;

            }
            #endregion
            //2.	На основании задания 1 восстановить состояние приложения из двоичного файла.
            #region
            Console.WriteLine("Десериализация...");
            using(FileStream fs=new FileStream("Books.dat", FileMode.OpenOrCreate))
            {
                List<Book> newbooks = (List<Book>)formatter.Deserialize(fs);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Десериализация прошла успешно");
                Console.ForegroundColor = ConsoleColor.White;
                foreach (Book i in books)
                {
                    Console.WriteLine("Название: {0}\nЦена: {1}\nАвтор: {2} \nГод издания: {3}\n", i.name,i.price,i.author,i.year);

                }
            }
            #endregion

            //3.Придумать пользовательский атрибут (самостоятельно).
            //Данный атрибут должен выполнять прикладную задачу(а не демонстрационную).
            //Проверить его функциональность.


            //            //4.Из csv файла (имя; фамилия; телефон; год рождения) прочитать записи в коллекцию.
            //            //Каждая запись коллекции должна иметь тип Person.
            //            //Сериализовать коллекцию с помощью класса SoapFormatter и сохранить в файл.
            //            string path = @"E:\GitHub\Serialize\Serialize\Serialize\bin\Debug\Person.csv";

            //            List<Person> people = new List<Person>();
            //            string line;
            //            List<string> peopleline = new List<string>();
            //            using (StreamReader rd=new StreamReader(path, System.Text.Encoding.Default))
            //            {

            //                while ((line = rd.ReadLine()) != null)
            //                {



            //                    Console.WriteLine(line);
            //                }


            //for(int i = 0; i <rd.ReadToEnd().Length; i++)
            //                    {
            //                peopleline.Add(line);

            //            } }
            //            foreach (var i in peopleline)
            //            {
            //                Console.WriteLine(i);
            //            }


            //            //foreach (Person i in people)
            //            //{
            //            //    i.Print();
            //            //}


            //5.	Самостоятельно рассмотреть библиотеку Newtonsoft.Json и сериализовать коллекцию в json файл.
            #region
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Сериализация в JSON\n");
            Console.ForegroundColor = ConsoleColor.White;

            DataContractJsonSerializer jsonformatter = new DataContractJsonSerializer(typeof(List<Book>));
            using(FileStream fs=new FileStream("Booking.json", FileMode.OpenOrCreate))
            {
                jsonformatter.WriteObject(fs, books);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Сериализация прошла успешно!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Второй  json способ..");
            Console.ForegroundColor = ConsoleColor.White;
            var json = JsonConvert.SerializeObject(books);
            Console.WriteLine(json);
            StreamWriter jsonfile = new StreamWriter("Bookig2.json");
            jsonfile.WriteLine(json);
            jsonfile.Close();
            Console.WriteLine();
            #endregion


            /*

6.	Создать библиотеку классов с именем «ClassLib». 

a.	В библиотеке «ClassLib» создать класс с именем «РС», 
описывающий компьютер. Данный класс должен включать:  
i.	3–4 поля (марка, серийный номер и т.д.),  
ii.	свойства (к каждому полю),  
iii.	конструкторы (по умолчанию, с параметрами),  
iv.	методы, моделирующие функционирование ПЭВМ (включение/выключение, перегрузку). 

b.	Создать новый проект (тип — консольное приложение) с именем «SerializConsolApp».
Добавить ссылку на библиотеку «ClassLib».

c.	В функции Main() данного проекта создать коллекцию (на базе обобщенного класса List<T> ) 
и добавить в коллекцию 4–5 объектов класса «РС». 

d.	Произвести сериализацию коллекции в бинарный файл с именем «listSerial.txt»в каталоге на диске D.
В случае наличия аналогичного файла в каталоге старый файл перезаписать новым файлом
и вывести об этом уведомление. 

e.	Создать новый проект (тип — консольное приложение) с именем «DeserializConsolApp». 
Добавить ссылку на библиотеку «ClassLib».

f.	В функции Main() произвести десериализацию коллекции, 
созданной в проекте с именем «SerializConsolApp» и вывести на экран.

*/
        }
    }
}
