using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace WpfApp4
{
    internal class MainCommand
    {
        //for copy+paste
        bool isFile = false;
        string path_copy = "";
        string name_copy = "";

        //open
        public string openFileDirectory(string path_, string select_element)
        {
            //path_ = path_
            //select_element = (listV_main);

            FileAttributes fileAttributes;
            string path = "";
            if ((select_element).EndsWith(@"\"))
                path = (select_element).Substring(0, (select_element).LastIndexOf(@"\")) + "/";

            if (path != "")
                fileAttributes = File.GetAttributes(path);
            else
                fileAttributes = File.GetAttributes(path_ + select_element);

            if ((fileAttributes & FileAttributes.Directory) == FileAttributes.Directory)
            {
                if ((select_element).EndsWith(@"\"))
                    path_ = path_ + (select_element).Substring(0, (select_element).LastIndexOf(@"\")) + "/";
                else
                    path_ = path_ + select_element + "/";
                return path_;
            }
            else
            {
                try
                {
                    Process.Start(path_ + select_element); return path_;
                }
                catch
                {
                    MessageBox.Show("Не удалось открыть файл.", "Ошибка");
                    return path_;
                }
            }
        }
        //goBack
        public string goBack(string path_)
        {
            if (path_.EndsWith("/"))
                path_ = path_.Remove(path_.Length - 1);
            if (path_.EndsWith(":"))
                path_ = "";
            try
            {
                path_ = path_.Substring(0, path_.LastIndexOf("/"));
                path_ += "/";
                return path_;
            }
            catch
            {
                return "";
            }
        }

        //copy
        public void MetodCopy(string path_ , string select_element)
        {
            if (path_ != "")
            {
                FileAttributes fileAttributes = File.GetAttributes(path_ + select_element);

                if ((fileAttributes & FileAttributes.Directory) == FileAttributes.Directory)
                    isFile = false;
                else isFile = true;

                path_copy = path_;
                name_copy = select_element;

                //буфер
                System.Collections.Specialized.StringCollection collection = new System.Collections.Specialized.StringCollection();
                collection.Add(path_ + select_element);
                Clipboard.SetFileDropList(collection);
            }
            else
            {
            }
        }

        //paste
        public string MetodPaste(string path_)
        {
            if (path_ != "")
            {
                try
                {
                    if (isFile == true)
                        File.Move(path_copy + "/" + name_copy, path_ + "/" + name_copy);
                    else
                        Directory.Move(path_copy + "/" + name_copy, path_ + "/" + name_copy);
                    return path_;
                }
                catch { return path_; };
            }
            else return "";
        }
        //rename
        public void Rename(string path_,string select_element, MainWindow main)
        {
                if (path_ != "")
                {
                    FileAttributes fileAttributes = File.GetAttributes(path_ + select_element);

                    if ((fileAttributes & FileAttributes.Directory) == FileAttributes.Directory)
                        isFile = false;
                    else isFile = true;
                    string extension = "";
                    string name = "";
                    if (isFile)
                    {
                        extension = System.IO.Path.GetExtension(path_ + select_element);
                        name = select_element.Substring(0, select_element.LastIndexOf("."));
                    }
                    else
                        name = select_element;

                    Rename rename = new Rename(name, path_, extension, main);
                    rename.Show();
                }
        }
        //archive
        public void Archive(string path, string arch_path, string select_element)
        {
            FileAttributes fileAttributes = File.GetAttributes(path);

            Directory.CreateDirectory(arch_path);

            if ((fileAttributes & FileAttributes.Directory) == FileAttributes.Directory)
            {
                Directory.Move(path, arch_path + select_element);
                ZipFile.CreateFromDirectory(arch_path + select_element, path + ".zip");
            }
            else
            {
                File.Move(path, arch_path + select_element);
                path = path.Substring(0, path.LastIndexOf("."));
                ZipFile.CreateFromDirectory(arch_path, path + ".zip");
            }
            Directory.Delete(arch_path, true);
        }
        //Info
        public void ShowInfo(string path_,string select_element)
        {
            FileAttributes fileAttributes = File.GetAttributes(path_ + select_element);

            if (select_element != null & (fileAttributes & FileAttributes.Directory) != FileAttributes.Directory)
            {
                FileInfo file = new FileInfo(path_ + select_element);
                Info info = new Info(file);
                info.Show();
            }
        }

        //delete
        public string Delete(string path_,string select_element)
        {
            FileAttributes fileAttributes = File.GetAttributes(path_ + select_element);
            if ((fileAttributes & FileAttributes.Directory) == FileAttributes.Directory)
                Directory.Delete(path_ + select_element, true);
            else
                File.Delete(path_ + select_element);
            return path_;
        }
    }
}