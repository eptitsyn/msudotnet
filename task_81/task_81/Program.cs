using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace task_81
{
    class Program
    {
        static void Main(string[] args)
        {

            Regex phoneRegex= new Regex(@"^\s*\""?(\+[0-9]{1,4}\s?)?(\([0-9]{1,4}\))?\s?[0-9,\-]{0,12}\""\s*$");
            Regex nameRegex = new Regex(@"^\s*\""?[A-Za-z1-9\s]*\""\s*$");
            //email regex from http://emailregex.com/
            Regex emailRegex = new Regex(@"^\s*\""?[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\""\s*$");

            StreamReader streamReader = null;
            try
            {
            if (args.Length > 0)
                streamReader = new StreamReader(args[0]);
            else
                streamReader = new StreamReader(Path.Combine(Environment.CurrentDirectory, "test.txt"));
            
                string line = streamReader.ReadLine();
                string[] splittedString = line.Split(',');
                int[] columns = new int[10];
                
                //запоминаем типы колонок
                for (int i = 0; i < splittedString.Length; i++)
                {
                    switch (splittedString[i].Replace(" ", string.Empty))
                    {
                        case "email": //1
                            columns[i] = 2;
                            //
                            break;
                        case "name": //0
                            //
                            columns[i] = 1;
                            break;
                        case "phone": //2
                            //
                            columns[i] = 3;
                            break;
                    }
                }

                int employees = 0;
                int incorrect_phone = 0;
                int incorrect_email = 0;


                while ((line = streamReader.ReadLine()) != null)
                {
                    ++employees;

                    splittedString = line.Split(',');
                    for (int i = 0; i < splittedString.Length; i++)
                    {
                        switch (columns[i])
                        {
                            case 2: //email
                                if (!emailRegex.IsMatch(splittedString[i])) ++incorrect_email;
                                break;
                            case 1: //name

                                break;
                            case 3: //phone
                                if (!phoneRegex.IsMatch(splittedString[i])) ++incorrect_phone;
                                break;
                        }
                    }
                }
                Console.WriteLine($"Total employees:{employees}");
                Console.WriteLine($"Incorrect emails:{incorrect_email}");
                Console.WriteLine($"Incorrect phones:{incorrect_phone}");
            }
            finally
            {
                streamReader?.Close();
            }


            Console.ReadLine();
        }
    }
}
