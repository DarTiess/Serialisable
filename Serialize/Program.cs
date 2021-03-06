﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Soap;

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
        public Book Update(Book obj)
        {
            Console.Write("Новое название : ");
            obj.name = Console.ReadLine();
            Console.Write("Новая цена : ");
            obj.price = Int32.Parse(Console.ReadLine());
            Console.Write("Новый автор : ");
            obj.author = Console.ReadLine();
            Console.Write("ГОД : ");
            obj.year = Int32.Parse(Console.ReadLine());
            return obj;

        }
        public Book AddBook()
        {
            Book obj = new Book();
            Console.Write("Новое название : ");
            obj.name = Console.ReadLine();
            Console.Write("Новая цена : ");
            obj.price = Int32.Parse(Console.ReadLine());
            Console.Write("Новый автор : ");
            obj.author = Console.ReadLine();
            Console.Write("ГОД : ");
            obj.year = Int32.Parse(Console.ReadLine());
            return obj;
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
            Console.WriteLine("Имя : " + FirstName + "\n\tФамилия : " + LastName + "\n\tНомер телефона : " + Number + "\n\tГод рождения : " + BirthYear);
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
                Console.WriteLine();
                foreach (Book i in books)
                {
                    Console.WriteLine("Название: {0}\nЦена: {1}\nАвтор: {2} \nГод издания: {3}\n", i.name,i.price,i.author,i.year);

                }
            }
            #endregion

            //3.Придумать пользовательский атрибут (самостоятельно).
            //Данный атрибут должен выполнять прикладную задачу(а не демонстрационную).
            //Проверить его функциональность.

            int choise;
            Console.WriteLine("Выберите 1 - для редактирования Книги\n\t2 - для добавления Книги в список\n\t3 - для удаления Книги из списка");
            choise = Int32.Parse(Console.ReadLine());
            
            Book upbook = new Book();
           
            switch (choise)
            {
                case 1:
                    Console.WriteLine("Введите порядковый номер книги");
                    choise = Int32.Parse(Console.ReadLine()) - 1;
                    books[choise] = upbook.Update(books[choise]);
                    Console.WriteLine("Информация о Книге была оредакированна");
                    break;
                case 2:
                    books.Add(upbook.AddBook());
                    Console.WriteLine("Книга добавлена в список");
                    break;
                case 3:
                    Console.WriteLine("Введите порядковый номер книги");
                    choise = Int32.Parse(Console.ReadLine()) - 1;
                    books.RemoveAt(choise);
                    Console.WriteLine("Книга удалена");
                    break;

            }
            Console.WriteLine();
            foreach (Book i in books)
            {
                Console.WriteLine("Название: {0}\nЦена: {1}\nАвтор: {2} \nГод издания: {3}\n", i.name, i.price, i.author, i.year);

            }
            using (FileStream fs = new FileStream("Books.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, books);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Список сохранен в Books.dat файл");
                Console.ForegroundColor = ConsoleColor.White;

            }
           
            Console.WriteLine();
            
       

            //4.Из csv файла (имя; фамилия; телефон; год рождения) прочитать записи в коллекцию.
            //Каждая запись коллекции должна иметь тип Person.
            //Сериализовать коллекцию с помощью класса SoapFormatter и сохранить в файл.
            #region
            string path = @"E:\GitHub\Serialize\Serialize\Serialize\bin\Debug\Person.csv";

            List<Person> people = new List<Person>();
            List<string> lines = new List<string>();
            string[] lineR;
           
            using (StreamReader rd = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                while ((line = rd.ReadLine()) != null)
                {
                   lines.Add(line);
                    Console.WriteLine(line);
                }
            
            }
            Console.WriteLine();
           for(int i = 0; i < lines.Count; i++)
            {
                lineR = lines[i].Split(';');
                 people.Add(new Person() { FirstName = lineR[0], LastName = lineR[1], Number = lineR[2], BirthYear = lineR[3] });
                
            }

            Console.WriteLine();
            foreach (Person i in people)
            {
                i.Print();
            }
            Console.WriteLine();

            SoapFormatter soapformatter = new SoapFormatter();
            using(FileStream fs=new FileStream("Persons.soap", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, people);
                Console.WriteLine("Сериализация завершена");
            }
            Console.WriteLine();
            #endregion

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
