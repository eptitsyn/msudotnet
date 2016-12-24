using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace task10
{
    class Program
    {
        class Person
        {
            string Firstname;
            string Lastname;
            string Patronymic;
            string Phone_number;
            string Address;
        }
        static void Main(string[] args)
        {
            //LoadBase
            List<Person> Base;
        }

        private List<Person> search(List<Person> Base, string Fieldname, string searchvalue)
        {
            List<Person> resultList = new List<Person>();
            foreach (Person recordPerson in Base)
            {
                FieldInfo fi = typeof (Person).GetField(Fieldname);
                if (fi.GetValue(recordPerson).ToString().Contains(searchvalue))
                {
                    resultList.Add(recordPerson);
                }
            }
            return resultList;
        }
    }
}
