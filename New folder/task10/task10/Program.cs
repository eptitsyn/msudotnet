using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace task10
{
    class Program
    {

        [Serializable] public class Person
        {
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string Patronymic { get; set; }
            public string Phone_number { get; set; }
            public string Address { get; set; }
            public override string ToString()
            {
                return $"Person Name: \"{Firstname} {Patronymic} {Lastname}\" Phone: \"{Phone_number}\" Address: \"{Address}\"";
            }
        }

        private static void SavePersons(List<Person> Base, string filename)
        {

            using (FileStream stream = new FileStream(filename, FileMode.Create)) 
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, Base);
            }
        }



        private static List<Person> LoadPersons(string filename)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(filename, FileMode.Open))
            {
               // if (serializer.CanDeserialize(stream))
                {
                    return (List<Person>)binaryFormatter.Deserialize(stream);
                }
                //else
                //{
                //    throw new Exception();
                //}

            }
        }

        private static List<Person> dosearch(List<Person> Base, string Fieldname, string searchvalue)
        {
            List<Person> result = new List<Person>();
            MethodInfo mi = typeof(Person).GetMethod($"get_{Fieldname}");
            result = Base.FindAll(person => mi.Invoke(person, null).ToString().Contains(searchvalue));
            return result;
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
            do 
            {
                Console.WriteLine("Search person by:");
                Console.WriteLine("1 - Firstname");
                Console.WriteLine("2 - Lastname");
                Console.WriteLine("3 - Patronymic");
                Console.WriteLine("4 - Phone");
                Console.WriteLine("5 - Address");
                choose = Console.ReadKey().KeyChar;
                switch (choose)
                {
                    case '1': { fieldname = "Firstname"; break; }
                    case '2': { fieldname = "Lastname"; break; }
                    case '3': { fieldname = "Patronymic"; break; }
                    case '4': { fieldname = "Phone_number"; break; }
                    case '5': { fieldname = "Address"; break; }
                }
            } while (choose  < '1' || choose > '5' );
            Console.Write("Enter value to search:");
            value = Console.ReadLine();
            List<Person> result = dosearch(Base, fieldname, value);
            Console.WriteLine("Result ==========");
            foreach (Person person in result)
            {
                Console.WriteLine(person.ToString());
            }
            Console.WriteLine("End of Result ==========");
        }
        static void Main(string[] args)
        {
            List<Person> Base;

            try
            {
                Base = LoadPersons("persons.bin");
            }
            catch (Exception)
            {
                Base = new List<Person>();
            }

            int c;

            do
            {
                Console.WriteLine("1 - add person");
                Console.WriteLine("2 - search person");
                Console.WriteLine("3 - print all");

                Console.WriteLine("0 - quit");
                c = Console.ReadKey().KeyChar;
                switch (c)
                {
                    case '1':
                    {
                        AddPerson(ref Base);
                        break;
                    }
                    case '2':
                    {
                        SearchPerson(ref Base);
                        break;
                    }
                    case '3':
                        {
                            foreach (Person person in Base)
                            {
                                Console.WriteLine(person.ToString());
                            }
                            break;
                        }
                }
            } while (c != '0');
            SavePersons(Base, "persons.bin");
        }


    }
}
