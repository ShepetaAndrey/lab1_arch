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
        public void Delete(string s)
        {
            DirectoryInfo dirInf = new DirectoryInfo(s);
            foreach (FileInfo n in dirInf.GetFiles())
            {
                File.Delete(n.FullName);
            }
            foreach (DirectoryInfo n in dirInf.GetDirectories())
            {
                if (n.GetDirectories().Count() != 0)
                {
                    foreach (DirectoryInfo k in dirInf.GetDirectories())
                    {
                        Delete(k.FullName);
                    }
                }
                Directory.Delete(n.FullName);
            }
        }
        public void CreateDefault(string sd, string subd, string NameOfFile, int quantity)
        {
            DirectoryInfo dirInf = new DirectoryInfo(sd);
            string fullPath = Path.Combine(dirInf.FullName + "\\..");
            string FileFullPath = dirInf.FullName + @"\" + NameOfFile;
            if (!dirInf.Exists)
            {
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
        static void Main(string[] args)
        {
            Console.WriteLine("lab_1_architechture_shepeta_andrey_pz_16_2");
            string dirName = @"tests";
            string subDirName = @"newtest\1";
            string s = "";
            int quant = 1;
            Program p = new Program();
            do
            {
                if (s == "")
                {
                    Console.WriteLine("What we gonna do?...............\n");
                    Console.WriteLine("Input \"/?\" to work with programm : ");
                }
                else
                {
                    Console.WriteLine("Unknown command! Try again");
                }
                s = Console.ReadLine();
                if (s == @"/?")
                {
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
                    do
                    {
                        Console.WriteLine("Type a quantity of creating elements (both of them) : ");
                        quant = Convert.ToInt32(Console.ReadLine());
                        if (quant >= 1 && quant <= 10)
                            p.CreateDefault(dirName, subDirName, @"texttext", quant);
                        else
                        {
                            Console.WriteLine("Quantity must be in range from 1 to 10!");
                        }
                    } while (!(quant >= 1 && quant <= 10));
                }
                else if (s == "/findd")
                {
                    Console.WriteLine("Type name of the directory (for example : another_test0)");
                    string dd = Console.ReadLine();
                    DirectoryInfo findd = new DirectoryInfo(dirName + "\\" + dd);
                    Console.WriteLine(findd.FullName);
                }
            } while (s != "/exit");           
            Console.Read();
        }
    }
}
