using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace task11
{
    class Program
    {
        //
        const int queuelength = 2;
        const int blocksize = 50000;
        const int maxelements = blocksize*queuelength;
        static bool endofdata = false;
            //
        static Object lockObject = new object();
        static ManualResetEvent resetEvent = new ManualResetEvent(false);

        static Queue<object> buffer = new Queue<object>();
        static void Main(string[] args)
        {
            
            Assembly dal = Assembly.LoadFile(Path.Combine(Environment.CurrentDirectory, "DAL.dll"));
            Type person = dal.GetType("DAL.Person");
            Type personproviderType = dal.GetType("DAL.PersonsProvider");
            Object personprovider = Activator.CreateInstance(personproviderType);
            MethodInfo getpersonsMethodInfo = personproviderType.GetMethod("GetPersons");

           
            
            Thread readThread = new Thread(() => readMethod(personprovider, getpersonsMethodInfo));
            Thread writeThread = new Thread(writeMethod);
            serialwrite(personprovider, getpersonsMethodInfo);
            readThread.Start();
            writeThread.Start();
            Console.ReadLine();
        }

        static void readMethod(object o, MethodInfo readMethodInfo)
        {
            //
            int j = 1;
            object[] tmp;
            while (!endofdata)
            {
                tmp = (object[]) readMethodInfo.Invoke(o, new object[] {j, blocksize});
                if (tmp.Length == 0)
                {
                    endofdata = true;
                }
                else
                {
                    bool tobuf = false;
                    while (!tobuf)
                    {
                        if (buffer.Count < queuelength)
                        {
                           lock (lockObject)
                            {
                                buffer.Enqueue(tmp);
                                resetEvent.Reset();
                                tobuf = true;
                            }
                        }
                        else
                        {
                            resetEvent.WaitOne();
                        }
                    }
                    j += blocksize;
                }
            }
        }
        static void writeMethod(object obj)
        {
            FileStream fileStream = File.Create(Path.Combine(Environment.CurrentDirectory, "persons.txt"));
            StreamWriter sw = new StreamWriter(fileStream);
            object[] block;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (!endofdata || (buffer.Count > 0))
            {
                
                    if (buffer.Count > 0)
                    {
                        lock (lockObject)
                        {
                            block = (object[]) buffer.Dequeue();
                            resetEvent.Set();
                        }

                        for (int i = 0; i < block.Length; i++)
                        {
                            //Console.WriteLine(block[i].ToString());
                            sw.WriteLine(block[i].ToString());
                        }
                    }
                
                Thread.Sleep(1);
            }

            stopwatch.Stop();
            Console.WriteLine($"Parallel write took {stopwatch.Elapsed.Seconds} seconds");
            sw.Flush();
            sw.Close();
            fileStream.Close();
        }

        static void serialwrite(object o, MethodInfo readMethodInfo)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int j = 1;
            object[] tmp;
            FileStream fileStream = File.Create(Path.Combine(Environment.CurrentDirectory, "persons2.txt"));
            StreamWriter sw = new StreamWriter(fileStream);
            
            while (!endofdata)
            {
                tmp = (object[])readMethodInfo.Invoke(o, new object[] { j, blocksize });
                if (tmp.Length == 0)
                {
                    endofdata = true;
                }
                else
                {
                    //
                    for (int i = 0; i < tmp.Length; i++)
                    {
                        sw.WriteLine(tmp[i].ToString());
                    }
                }
                j += blocksize;
            }
            stopwatch.Stop();
            Console.WriteLine($"Serial write took {stopwatch.Elapsed.TotalSeconds} seconds");
            sw.Flush();
            sw.Close();
            fileStream.Close();
            endofdata = false;
        }
    }
}
