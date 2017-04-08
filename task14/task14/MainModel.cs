using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace task14
{
    class MainModel
    {
        public string text { get; set; }
        public string Filepath { get; set; }

        public void New()
        {
            text = null;
        }

        public void Open()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                text = File.ReadAllText(openFileDialog.FileName);
                Filepath = openFileDialog.FileName;
            }
        }

        public void Save()
        {
            if (string.IsNullOrEmpty(Filepath))
            {
                SaveAs();
            }
            else
            {
                SaveToFile(text, Filepath);
            }
        }

        public void SaveAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File|*.txt";
            saveFileDialog.Title = "Save text file as..";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                SaveToFile(text, saveFileDialog.FileName);
                Filepath = saveFileDialog.FileName;
            }
        }

        private void SaveToFile(string text, string path)
        {
            StreamWriter sw = new StreamWriter(path);
            sw.Write(text);
            sw.Close();
        }
    }
}