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
        public void Delete(string s, out int count)
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
                if (n.GetDirectories().Count() != 0)
                {
                    foreach (DirectoryInfo k in dirInf.GetDirectories())
                    {
                        int tempCount = count;
                        Delete(k.FullName, out tempCount);
                        count+=tempCount;
                    }
                }
                Directory.Delete(n.FullName);
                count++;
            }
        }
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

        public void FindDir(int count, DirectoryInfo dir, out int quantity)
        {
            quantity = 0;
            foreach (DirectoryInfo dirN in dir.GetDirectories())
            {
                if (dirN.Exists)
                {
                    if(dirN.GetDirectories().Count() != 0 && count > 0)
                    {
                        foreach (DirectoryInfo dirNext in dir.GetDirectories())
                        {
                            int temp = quantity;
                            Console.WriteLine(dirNext.Name);
                            quantity++;
                            count--;
                            FindDir(count, dir, out temp);
                        }
                        quantity++;
                    }                                    
                }
                else Console.WriteLine("{0} dir hasnt subdirs", dirN.Name);
            }
        }

        static void Main(string[] args)
        {
            bool neflag;
            bool flag;
            string dirName = @"tests";
            string subDirName = @"tests\newtest";
            DirectoryInfo path = new DirectoryInfo(dirName);
            DirectoryInfo subPath = new DirectoryInfo(subDirName);
            Program p = new Program();
            int quant = 1;

            foreach (string s in args)
            {
                neflag = true;
                flag = true;
                if (flag && neflag) Console.WriteLine("lab_1_architechture_shepeta_andrey_pz_16_2");        
                if (s ==  "/?")
                {
                    neflag = false;
                    Console.WriteLine("/create - create example files and directories");
                    Console.WriteLine("/findd - find directory");
                    Console.WriteLine("/findf - find file");
                    Console.WriteLine("/some - to delete some files from directory");
                    Console.WriteLine("/all - delete all files from directory");
                    Console.WriteLine("/allf - delete all files from directory and subdirectories");
                    Console.WriteLine("/exit - end the programm...");
                }
                else if (s == "/create")
                {
                    try
                    {
                        do
                        {
                            Console.WriteLine("Type a quantity of creating elements (both of them) : ");
                            quant = Convert.ToInt32(Console.ReadLine());
                            if (quant >= 1 && quant <= 10) p.CreateDefault(dirName, subDirName, @"texttext", quant);
                            else Console.WriteLine("Quantity must be in range from 1 to 10!");
                        } while (!(quant >= 1 && quant <= 10));
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message + "\n");
                    }
                }
                else if (s == "/findd")
                {
                    string findDir = "";
                    Console.WriteLine("Type name of the directory");
                    try
                    {
                        Int32 quantity = 0;
                        findDir = Console.ReadLine();
                        Console.WriteLine("Quantity of recursions : ");
                        Int32 count = Convert.ToInt32(Console.ReadLine());                     
                        DirectoryInfo fDir = new DirectoryInfo(findDir);
                        p.FindDir(count, fDir, out quantity);
                        Console.WriteLine("Directory {0} has {1} subdirectories\nQuantity of recursions : {2}", fDir.Name, quantity, count);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message + "\n");
                    }
                }
                else if (s == "/allf")
                {
                    try
                    {
                        FileAttributes fAtr = File.GetAttributes(path.FullName);
                        Console.WriteLine(fAtr);
                        int count = 0;
                        Console.WriteLine("All files and directories from \"tests\" successfuly deleted!");
                        p.Delete(dirName, out count);
                        Console.WriteLine(@"Quantity of deleted files and directories from \tests\ : {0}", count);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message + "\n");
                    }
                }
                else
                {
                    Console.WriteLine("Unknown command! Try again");
                }
            }         
        }
    }
}
