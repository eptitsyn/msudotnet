using System;
using System.IO;


namespace task_82
{
    class Program
    {
        static void Main(string[] args)
        {
            string logpath = Path.Combine(Environment.CurrentDirectory, "task82.log");
            if (args.Length > 1) logpath = args[2];

            StreamWriter logfile = new StreamWriter(logpath);
           
            FileSystemWatcher watcher = new FileSystemWatcher();
            if (args.Length > 0)
                watcher.Path = args[1];
            else
                watcher.Path = Environment.CurrentDirectory;//@"d:\tmp\";
            watcher.Filter = "*.cs";
            watcher.Changed += (s, e) =>
            {
                Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + $" {e.Name}  {e.ChangeType}");
                logfile.WriteLine(DateTime.Now.ToString("HH:mm:ss") + $" {e.Name}  {e.ChangeType}");
                logfile.Flush();
            };
            watcher.Created += (s, e) =>
            {
                Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + $" {e.Name}  {e.ChangeType}");
                logfile.WriteLine(DateTime.Now.ToString("HH:mm:ss") + $" {e.Name}  {e.ChangeType}");
                logfile.Flush();
            };
            watcher.Deleted += (s, e) =>
            {
                Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + $" {e.Name}  {e.ChangeType}");
                logfile.WriteLine(DateTime.Now.ToString("HH:mm:ss") + $" {e.Name}  {e.ChangeType}");
                logfile.Flush();
            };

            watcher.EnableRaisingEvents = true;
            Console.ReadKey();
            logfile.Close();

        }
    }
}
