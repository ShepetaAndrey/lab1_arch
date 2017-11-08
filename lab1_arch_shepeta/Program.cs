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
        //static int temp;
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
            DirectoryInfo lala = new DirectoryInfo(dir);
            foreach(DirectoryInfo d in lala.GetDirectories())
            {
                if(d.Name == s)
                {
                    return d.FullName;
                }
                Console.WriteLine(d.Name);
                if (d.GetDirectories().Count() != 0)
                {
                    FindDir(d.FullName, s);
                }
            }
            return "No matches found";
        }
        


        static void Main(string[] args)
        {
            string dirName = @"tests";
            string subDirName = @"helloworld\newtest";
            DirectoryInfo path = new DirectoryInfo(dirName);
            DirectoryInfo subPath = new DirectoryInfo(subDirName);
            Program p = new Program();
            int quant = 0;

            foreach (string s in args)
            {       
                if (s ==  "/?")
                {
                    Console.WriteLine("  /create - create example files and directories");
                    Console.WriteLine("  /findd - find directory");
                    Console.WriteLine("  /findf - find file");
                    Console.WriteLine("  /some - to delete some files from directory");
                    Console.WriteLine("  /all - delete all files from directory");
                    Console.WriteLine("  /allf - delete all files from directory and subdirectories");
                    Console.WriteLine("  /exit - end the programm...");
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
                        int count = 0;
                        Console.WriteLine("All files and directories from \"tests\" successfuly deleted!");
                        p.DeleteSubj(dirName, out count);
                        Console.WriteLine(@"Quantity of deleted files and directories from \tests\ : {0}", count);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message + "\n");
                    }
                }
                else if (s != "/allf" || s != "findd" || s!="/create")
                {
                    try
                    {
                        string dirNew = @"\let";
                        string catch_path = "dada";
                        catch_path = p.FindDir(dirNew, s);
                        Console.WriteLine("FullName of {0} is {1}", s, catch_path);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    //else
                    //{
                    //    Console.WriteLine("Directory wasnt found!");
                    //}
                }
                else
                {
                    Console.WriteLine("Unknown command! Try again");
                }
            }         
        }
    }
}