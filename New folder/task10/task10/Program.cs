using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace task10
{
    class Program
    {

        [Serializable] class Person
        {
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string Patronymic { get; set; }
            public string Phone_number { get; set; }
            public string Address { get; set; }
        }

        private static void SavePersons(List<Person> Base, string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Person>));            
            using (FileStream stream = new FileStream(filename, FileMode.Create)) 
            {
                serializer.Serialize(stream, Base);
            }
        }

        private static List<Person> LoadPersons(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Person>));
            using (FileStream stream = new FileStream(filename, FileMode.Open))
            {
               // if (serializer.CanDeserialize(stream))
                {
                    return (List<Person>)serializer.Deserialize(stream);
                }
                //else
                //{
                //    throw new Exception();
                //}

            }
        }

        private static List<Person> dosearch(List<Person> Base, string Fieldname, string searchvalue)
        {
            FieldInfo fi = typeof(Person).GetField(Fieldname);
            return Base.FindAll(person => fi.GetValue(person).ToString().Contains(searchvalue));
        }

        private static void AddPerson(ref List<Person> Base)
        {
            Person person = new Person();
            Console.Write("Add Person. Name:");
            person.Firstname = Console.ReadLine();
            Console.Write("Add Person. Lastame:");
            person.Lastname = Console.ReadLine();
            Console.Write("Add Person. Patronymic:");
            person.Patronymic = Console.ReadLine();
            Console.Write("Add Person. Phone:");
            person.Phone_number = Console.ReadLine();
            Console.Write("Add Person. Address:");
            person.Address = Console.ReadLine();

            Base.Add(person);
        }

        private static void SearchPerson(ref List<Person> Base)
        {
            
            int choose;
            string fieldname = "", value;
            while ((choose = Console.ReadKey().KeyChar) >= '1' && choose <= 5 )
            {
                Console.WriteLine("Searchperson by:");
                Console.WriteLine("1 - Firstname");
                Console.WriteLine("2 - Lastname");
                Console.WriteLine("3 - Patronymic");
                Console.WriteLine("4 - Phone");
                Console.WriteLine("5 - Address");
                switch (choose)
                {
                    case '1': { fieldname = "Firstname"; break; }
                    case '2': { fieldname = "Lastname"; break; }
                    case '3': { fieldname = "Patronymic"; break; }
                    case '4': { fieldname = "Phone_number"; break; }
                    case '5': { fieldname = "Address"; break; }
                }
            }
            Console.Write("Value:");
            value = Console.ReadLine();
            List<Person> result = dosearch(Base, fieldname, value);
            Console.WriteLine("Result");
            foreach (Person person in result)
            {
                Console.WriteLine(person.ToString());
            }
        }
        static void Main(string[] args)
        {
            List<Person> Base;
            //LoadBase
            try
            {
                Base = LoadPersons("perxons.xml");
            }
            catch (Exception)
            {
                Base = new List<Person>();
            }

            int c;
            while ((c = Console.ReadKey().KeyChar) != '0')
            {
                Console.Clear();
                Console.WriteLine("1 - add person");
                Console.WriteLine("2 - search person");
                Console.WriteLine("0 - quit");
                switch (c)
                {
                    case '1': { AddPerson(ref Base); break;}
                    case '2': { SearchPerson(ref Base); break; }
                }
            }
            SavePersons(Base, "persons.xml");
        }


    }
}
