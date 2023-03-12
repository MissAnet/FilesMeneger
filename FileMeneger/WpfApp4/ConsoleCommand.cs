using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace WpfApp4
{

    internal class ConsoleCommand
    {
        [DllImport("Kernel32")]
        public static extern void AllocConsole();

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("Kernel32")]
        public static extern void FreeConsole();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);


        string _command;
        string path_ ="";
        string select_element;
        string path;
        bool isFile;
        MainCommand command = new MainCommand();
        MainWindow main;

        public void HideShowConsole(int SW_PAR,MainWindow mainWindow)
        {
            ShowWindow(GetConsoleWindow(), SW_PAR);
            main = mainWindow;
        }
        //load
        public void Loading()
        {
            if (path_ == "")
            {
                string[] disks = Environment.GetLogicalDrives();
                for (int i = 0; i < disks.Length; i++)
                {
                    Console.WriteLine(disks[i].ToString());
                }
            }
            else
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path_);

                DirectoryInfo[] directories = directoryInfo.GetDirectories();
                FileInfo[] files = directoryInfo.GetFiles();

                Console.WriteLine("======================================================");
                for (int i = 0; i < files.Length; i++)
                {
                    Console.WriteLine(files[i].ToString());
                }
                for (int i = 0; i < directories.Length; i++)
                {
                    Console.WriteLine(directories[i].ToString());
                }
                Console.WriteLine("======================================================");
            }
        }
        //OpenConsole
        public void OpenConsole()
        {
            try
            {
                AllocConsole();

                Console.ForegroundColor = ConsoleColor.Green;
                Loading();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Command:\r\n            Delete:path\r\n            Create:path\r\n            Open:path\r\n            Copy:path\r\n            Rename:path\r\n            Paste:path\r\n            Info:path\r\n            Archivate:path");
                Console.WriteLine("Write exit for close console");
                string console_context = Console.ReadLine();

                if (console_context.Contains(":") || console_context == "exit")
                {
                    if (console_context == "exit")
                        HideShowConsole(0, main);
                    else
                    {
                        _command = console_context.Substring(0, console_context.IndexOf(":"));
                        path_ = console_context.Substring(console_context.IndexOf(":") + 1);

                        if (_command != "" && path_ != "" && (!AreYouDisk(path_) || _command == "Open"))
                        {
                            switch (_command)
                            {
                                case "Delete":
                                    Delete();
                                    break;

                                case "Create":
                                    Create();
                                    break;

                                case "Open":
                                    Open();
                                    break;

                                case "Copy":
                                    Copy();
                                    break;

                                case "Rename":
                                    Rename();
                                    break;

                                case "Paste":
                                    Paste();
                                    break;

                                case "Archivate":
                                    Archive();
                                    break;
                                case "Info":
                                    Info();
                                    break;
                                default:
                                    OpenConsole();
                                    break;
                            }
                        }
                        else
                        {
                            OpenConsole();
                        }
                    }
                }
                else
                {
                    if (console_context == "Света нет")
                    {
                        Console.WriteLine("Create By Anastasia 'Annet'");
                        OpenConsole();
                    }
                    else
                    {
                        Console.WriteLine("Неправельно введена команда , укажите как в примере");
                        OpenConsole();
                        path_ = "";
                        _command = "";
                    }
                }
            }
            catch { MessageBox.Show("Ошибка при запуске консоли."); }
            //Console.WriteLine(_command);
            //Console.WriteLine(path_);
        }

        //open file/directory
        public void Open()
        {
            try
            {
                if (path_.EndsWith("\\"))
                    path_ = path_.Substring(0, path_.Length - 1) + "/";
                //Console.WriteLine(path_);
                path = path_.Substring(0, path_.LastIndexOf("/") + 1);
                select_element = path_.Substring(path_.LastIndexOf("/") + 1);

                if (path_.Contains("."))
                {
                    if (File.Exists(path_))
                    {
                        command.openFileDirectory(path, select_element);
                    }
                    else
                        Console.WriteLine("Файла не существует");
                }
                else
                {
                    if (Directory.Exists(path_))
                    {
                        main.loadFileDir(path_);
                    }
                    else
                    {
                        Console.WriteLine("Папки не существует");
                    }
                }
                OpenConsole();
            }
            catch 
            { 
                Console.WriteLine("Не удалось открыть файл"); 
                OpenConsole(); }
        }

        public bool AreYouDisk(string paths)
        {
            string[] disks = Environment.GetLogicalDrives();

            for (int i = 0; i < disks.Length; i++)
            {
                if (paths == disks[i])
                    return true;
            }
            return false;
        }
        //create
        public void Create()
        {
            try
            {
                if (path_.Contains("."))
                    isFile = true;
                else isFile = false;
                if (isFile)
                {
                    if (File.Exists(path_))
                    {
                        MessageBox.Show("Файл с таким именем уже суещствует");
                    }
                    else
                    {
                        using (FileStream fs = File.Create(path_));
                        Console.WriteLine("Файл " + path_ +" создан");
                    }
                }
                else
                {
                    if (Directory.Exists(path_))
                    {
                        MessageBox.Show("Папка с таким именем уже суещствует");
                    }
                    else
                    {
                        Directory.CreateDirectory(path_);
                        Console.WriteLine("Папка " + path_ + " создана");
                    }
                }
                OpenConsole();
            }
            catch
            { 
                MessageBox.Show("Невозможно создать файл , попробуйте в другом месте или с другим названием/расширением");
                OpenConsole();
            }
        }

        //delete
        public void Delete()
        {
            if (path_.EndsWith("/") || path_.EndsWith("\\"))
                path_ = path_.Substring(0, path_.Length - 1);
            //Console.WriteLine(path_);
            path = path_.Substring(0, path_.LastIndexOf("/") + 1);
            select_element = path_.Substring(path_.LastIndexOf("/") + 1);
            try
            {
                Console.WriteLine("Файл/Папка " + command.Delete(path, select_element)+select_element + " был/а удален/а");
                OpenConsole();
            }
            catch { Console.WriteLine("Не удалось удалить , попробуйте другой документ"); OpenConsole(); }
        }

        //COpy
        public void Copy()
        {
            try 
            {
                if (path_.EndsWith("\\"))
                    path_ = path_.Substring(0, path_.Length - 1) + "/";
               // Console.WriteLine(path_);
                path = path_.Substring(0, path_.LastIndexOf("/") + 1);
                select_element = path_.Substring(path_.LastIndexOf("/") + 1);

                command.MetodCopy(path,select_element);
                Console.WriteLine("Скопировано");
                OpenConsole();
            }
            catch { Console.WriteLine("Не удалось скопировать файл/папку"); OpenConsole(); }
        }

        //Paste
        public void Paste()
        {
            try
            {
                command.MetodPaste(path_);
                Console.WriteLine("Перемещено");
                OpenConsole();
            }
            catch { Console.WriteLine("Не удалось перенести файл/папку"); OpenConsole(); }
        }

        //Rename
        public void Rename()
        {
            try 
            {
                FileAttributes fileAttributes = File.GetAttributes(path_);

                if ((fileAttributes & FileAttributes.Directory) == FileAttributes.Directory)
                    isFile = false;
                else isFile = true;

                string extension = "";
                string name = "";
                string new_name = "";
                bool normali = true;
                
                path = path_.Substring(0, path_.LastIndexOf("/") + 1);
                select_element = path_.Substring(path_.LastIndexOf("/") + 1);

                if (isFile)
                {
                    extension = System.IO.Path.GetExtension(path_ + select_element);
                    name = select_element.Substring(0, select_element.LastIndexOf("."));
                    path = path_.Substring(0,path_.LastIndexOf("/") + 1);
                }
                else
                {
                    name = select_element;
                }
                
                if (extension != "")
                {
                    while (normali)
                    {
                    Console.Write("Укажите название нового файла: ");
                    new_name = Console.ReadLine();
                        if(!File.Exists(path+new_name+extension))
                            normali = false;
                        else
                            Console.WriteLine("Файл с таким именем уже существует");
                    }
                        File.Move(path + name + extension, path + new_name + extension);
                        Console.WriteLine("Переименовано");
                        OpenConsole();
                }
                else
                {
                    while (normali)
                    {
                        Console.Write("Укажите название новой папки: ");
                        new_name = Console.ReadLine();
                        if (!Directory.Exists(path + new_name))
                            normali = false;
                        else
                            Console.WriteLine("Папка с таким именем уже существует");
                    }
                        Directory.Move(path + new_name, path + new_name);
                        Console.WriteLine("Переименовано");
                        OpenConsole();
                }
            }
            catch 
            {
                Console.WriteLine("Не удалось переименовать файл/папку");
                OpenConsole(); }
        }
        //archive
        public void Archive()
        {
            path = path_.Substring(0, path_.LastIndexOf("/") + 1);
            select_element = path_.Substring(path_.LastIndexOf("/") + 1);
            string arch_path = path + "Новая Папка/";
            try
            {
                command.Archive(path_, arch_path, select_element);
                Console.WriteLine("Архив создан");
                OpenConsole();
            }
            catch
            {
                Console.WriteLine("Не удалось создать архив");
                OpenConsole();
            }
        }

        //info
        public void Info()
        {
            path = path_.Substring(0, path_.LastIndexOf("/") + 1);
            select_element = path_.Substring(path_.LastIndexOf("/") + 1);

            try
            {
                FileAttributes fileAttributes = File.GetAttributes(path_);

                if (select_element != null & (fileAttributes & FileAttributes.Directory) != FileAttributes.Directory)
                {
                    FileInfo file = new FileInfo(path_);
                    Console.WriteLine("Название: "+ file.Name);
                    Console.WriteLine("Полное название: " + (file.FullName).ToString());
                    Console.WriteLine("Размер: "+(file.Length / 1024).ToString());
                    Console.WriteLine("Дата создания: "+ (file.CreationTime).ToString());
                    Console.WriteLine("Последняя дата редактирования: "+(file.LastWriteTime).ToString());
                }
                OpenConsole();
            }
            catch
            {
                OpenConsole();
            }
        }
    }
}

