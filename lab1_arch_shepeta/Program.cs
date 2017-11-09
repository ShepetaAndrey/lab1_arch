using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace lab1_arch_shepeta
{
    class Program
    {

        /////////////////DELETING TESTS FOLDERS AND FILES/////////////////////
        public void DeleteSubj(string s, out int count)
        {
            count = 0;
            DirectoryInfo dirInf = new DirectoryInfo(s);
            foreach (FileInfo n in dirInf.GetFiles())
            {
                File.Delete(n.FullName);
                count++;
            }
            foreach (DirectoryInfo n in dirInf.GetDirectories())
            {
                foreach (DirectoryInfo k in dirInf.GetDirectories())
                {
                    int tempCount = count;
                    DeleteSubj(k.FullName, out tempCount);
                    count += tempCount;
                }
                Directory.Delete(n.FullName);
                count++;
            }
        }
        //////////////CREATING TEST FOLDERS AND FILES//////////////////////
        public void CreateDefault(string sd, string subd, string NameOfFile, int quantity)
        {
            DirectoryInfo dirInf = new DirectoryInfo(sd);
            string myPath = @"C:\Users\ZenBook\Documents\Visual Studio 2015\Projects\lab1_arch_shepeta\lab1_arch_shepeta\bin\Debug\let";
            if(!Directory.Exists(myPath))
            {
                Directory.CreateDirectory(myPath + "\\" + "NewFolder");
                Console.WriteLine("Created new directory : \\" + myPath + "\\" + "NewFolder");
                Directory.CreateDirectory(myPath + "\\" + "NewFolder" + "\\" + "NewFolder1");
                Console.WriteLine("Created new directory : \\" + myPath + "\\" + "NewFolder" + "\\" + "NewFolder1");
                Directory.CreateDirectory(myPath + "\\" + "NewFolder" + "\\" + "NewFolder1" + "\\" + "test");
                Console.WriteLine("Created new directory : \\" + myPath + "\\" + "NewFolder" + "\\" + "NewFolder1" + "\\" + "test");
            }
            string fullPath = Path.Combine(dirInf.FullName + "\\..");
            string FileFullPath = dirInf.FullName + @"\" + NameOfFile;
            if (!dirInf.Exists)
            {
                Console.WriteLine("Created new directory : \\" + dirInf.Name);
                dirInf.Create();
            }
            for(int i = 0; i < quantity; i++)
            {
                dirInf.CreateSubdirectory(subd + i.ToString());
                Console.WriteLine("Подпапка {0} была создана в каталоге :\n{1}", subd + i.ToString(), fullPath);
            }
            for(int j = 0; j < quantity; j++)
            {
                if (!File.Exists(FileFullPath + j.ToString() + ".txt"))
                {
                    File.Create(FileFullPath + j.ToString() + ".txt");
                    Console.WriteLine("Файл : {0} был создан в каталоге :\n{1}", NameOfFile + j.ToString() + ".txt", FileFullPath + ".txt");
                }
            }
        }
        //////////////FINDING DIRECTORIES///////////////
        public string FindDir(string dir, string s)
        {
            string found_str = "";
            DirectoryInfo lala = new DirectoryInfo(dir);
            foreach(DirectoryInfo d in lala.GetDirectories())
            {
                if (d.Name.ToUpper() == s.ToUpper())
                {
                    return d.FullName;
                }
                if (d.GetDirectories().Count() != 0)
                {
                    return FindDir(d.FullName, s);
                }
            }
            return found_str;
        }

        static void Main(string[] args)
        {
            string foundSTR = ""; //получает искомую папку
            string dirName = @"tests"; //создаем тестовые файлы в этой папке
            string subDirName = @"helloworld\newtest"; 
            int quant = 0;
            Program p = new Program();

            foreach (string s in args)
            {       
                if(s != "/?" && s != "/allf" && s != "/findd" && s != "create")
                {
                    try
                    {
                        string ddd = "tests";           //Папка, в которой производится поиск пользовательской папки    
                        foundSTR = p.FindDir(ddd, s);  //Результат поиска
                        if (foundSTR == "")
                        {
                            foundSTR = "Diroctory didnt found";
                            Console.WriteLine("Catch-path = {0}", foundSTR);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                if (s ==  "/?")
                {
                    Console.WriteLine("  /create - create example files and directories");
                    Console.WriteLine("  /findd - find directory");
                    Console.WriteLine("  /allf - delete all files from directory and subdirectories");
                }
                else if (s == "/create")
                {
                    try
                    {
                        do
                        {
                            Console.WriteLine("Type a quantity of creating elements (both of them) : ");
                            quant = Convert.ToInt32(Console.ReadLine());
                            if (quant >= 1 && quant <= 10) p.CreateDefault(dirName, subDirName, @"textexample", quant);
                            else Console.WriteLine("Quantity must be in range from 1 to 10!");
                        } while (!(quant >= 1 && quant <= 10));
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message + "\n");
                    }
                }
                //else if (s == "/findd")
                //{
                //    try
                //    {
                //        Int32 quantity = 0;

                //        string findDir = "";
                //        Console.WriteLine("Type name of the directory");    
                //        findDir = Console.ReadLine();        
                                                          
                //        DirectoryInfo fDir = new DirectoryInfo(findDir);
                        
                //        string q = p.FindDir(fDir);
                //        Console.WriteLine("Directory {0} has {1} subdirectories\n", fDir.Name, q);
                //    }
                //    catch(Exception e)
                //    {
                //        Console.WriteLine(e);
                //    }
                //}
                else if (s == "/allf")
                {
                    try
                    {
                        if (Directory.Exists(foundSTR))
                        {                         
                            int count = 0;
                            p.DeleteSubj(foundSTR, out count);
                            DirectoryInfo haha = new DirectoryInfo(foundSTR);
                            Console.WriteLine("All files and directories from \"{0}\" successfuly deleted!", haha.Name);
                            Console.WriteLine(@"Quantity of deleted files and directories from \{0}\ : {1}", haha.Name, count);
                        }
                        else
                        {
                            Console.WriteLine("Directory {0} doesnt exist", foundSTR);
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                else if (s != "/allf" || s != "findd" || s!="/create")
                {
                    
                }
                else
                {
                    Console.WriteLine("Unknown command! Try again");
                }
                
            }         
        }
    }
}