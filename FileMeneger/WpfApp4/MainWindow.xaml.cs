using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Reflection;
using System.IO.Packaging;
using System.IO.Pipes;
using System.Runtime.InteropServices;

namespace WpfApp4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string path_ = "";
        MainCommand command = new MainCommand();
        ConsoleCommand console = new ConsoleCommand();

        public MainWindow()
        {
            InitializeComponent();
            console.HideShowConsole(0,this);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadFileDir(path_);
        }

        public void loadFileDir(string path)
        {
            path_ = path;
            DirectoryInfo directoryInfo;
            try
            {
                listV_Main.Items.Clear();
                textBox_path.Text = path;

                if (path == "")
                {
                    string[] disks = Environment.GetLogicalDrives();
                    for (int i = 0; i < disks.Length; i++)
                    {
                        listV_Main.Items.Add(disks[i]);
                    }
                }
                else
                {
                    directoryInfo = new DirectoryInfo(path);

                    DirectoryInfo[] directories = directoryInfo.GetDirectories();
                    FileInfo[] files = directoryInfo.GetFiles();


                    for (int i = 0; i < files.Length; i++)
                    {
                        listV_Main.Items.Add(files[i].Name);
                    }
                    for (int i = 0; i < directories.Length; i++)
                    {
                        listV_Main.Items.Add(directories[i].Name);
                    }
                }
            }
            catch 
            {
                MessageBox.Show("Не удалось загрузить" , "Ошибка");
            }
        }

        //doubleclick / open
        private void listV_Main_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (listV_Main.SelectedItem != null)
                {
                    path_ = command.openFileDirectory(path_, listV_Main.SelectedItem.ToString());
                    loadFileDir(path_);
                }
            }
            catch { loadFileDir(path_); }
        }

        //goback
        private void btn_GoBack_Click(object sender, RoutedEventArgs e)
        {
            path_ = command.goBack(path_);
            loadFileDir(path_);
        }

        //hot-keyboard
        private void listV_Main_KeyDown(object sender, KeyEventArgs e)
        {
            //ctrl + c
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.C)
            {
                MetodCopy();
            }
            //ctrl + v
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.V)
            {
                MetodPaste();
            }
        }

        //delete
        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (listV_Main.SelectedItem != null && path_ != "")
                loadFileDir(command.Delete(path_,listV_Main.SelectedItem.ToString()));
        }

        //create
        private void btn_Create_Click(object sender, RoutedEventArgs e)
        {
            if (path_ != "")
            {
                CreateWindow createWindow = new CreateWindow(this);
                createWindow.ShowDialog();
            }
        }

        //info
        private void btn_ShowInfo_Click(object sender, RoutedEventArgs e)
        {
            if (listV_Main.SelectedItem != null && path_ != "")
                command.ShowInfo(path_, listV_Main.SelectedItem.ToString());
        }

        //copy
        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            MetodCopy();
        }

        //paste
        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            MetodPaste();
        }

        //методы
        public void MetodCopy()
        {
            try
            {
                if (listV_Main.SelectedItem != null && path_ != "")
                    command.MetodCopy(path_, listV_Main.SelectedItem.ToString());
            }
            catch { }
        }
        public void MetodPaste()
        {
            try
            {
                if (path_ != "")
                    loadFileDir(command.MetodPaste(path_));
            }
            catch { }
        }

        //архив
        private void btn_Archiving_Click(object sender, RoutedEventArgs e)
        {
            if (listV_Main.SelectedItem != null && path_ != "")
            {
                string path = path_ + listV_Main.SelectedItem.ToString();
                string arch_path  = path_ + "Новая Папка/";
                try
                {
                    command.Archive(path,arch_path,listV_Main.SelectedItem.ToString());
                    loadFileDir(path_);
                }
                catch { }
            }
        }

        //rename
        private void rename_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (listV_Main.SelectedItem != null && path_ != "")
                    command.Rename(path_, listV_Main.SelectedItem.ToString(),this);
            }
            catch
            {
                loadFileDir(path_);
            }
        }

        //console

        private void btn_Console_Click(object sender, RoutedEventArgs e)
        {
            console.HideShowConsole(5,this);
            console.OpenConsole();
        }
    }
}