#pragma warning disable 168

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;

namespace ShavedIce_ClassViewer
{
    static class Program
    {
        public static Assembly LoadedAssembly { get; private set; }

        public static bool VisualMode { get; private set; }
        public static bool HTMLMode { get; private set; }

        public static string FileName { get; private set; }
        public static string TypeName { get; private set; }

        static int Main(string[] args)
        {
            VisualMode = false;
            HTMLMode = false;
            FileName = "";
            TypeName = null;

            if (args.Length == 0)
            {
                bool can_not_exit = true;

                while (can_not_exit)
                {
                    Console.Write("ShavedIce_ClassViewer "  + FileName + "$ ");
                    string[] cmd = Console.ReadLine().SplitDoubleQuotation();

                    Console.WriteLine(cmd.ToStringAll(","));

                    if (cmd.Length == 0)
                    {
                        continue;
                    }

                    switch(cmd[0])
                    {
                        case "writeconsole":
                        case "print":
                            {
                                foreach (var type in LoadedAssembly.GetTypes())
                                {
                                    InstanceWriter.WriteToConsole(type);
                                }
                            }
                            break;
                        case "writefile":
                            {
                                if (cmd.Length == 2)
                                {
                                    using (StreamWriter sw = new StreamWriter(cmd[1]))
                                    {
                                        foreach (var type in LoadedAssembly.GetTypes())
                                        {
                                            InstanceWriter.WriteToTextFile(sw, type);
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("ARG COUNT IS WRONG");
                                }
                            }
                            break;
                        case "writehtml":
                            {
                                if (cmd.Length == 2)
                                {
                                    StringBuilder sb = new StringBuilder();

                                    foreach (var type in LoadedAssembly.GetTypes())
                                    {
                                        sb.AppendLine(InstanceWriter.GetHTMLText(type));
                                    }

                                    InstanceWriter.WriteHTML(cmd[1], LoadedAssembly.FullName, sb.ToString());

                                    System.Diagnostics.Process.Start(cmd[1]);
                                }
                                else
                                {
                                    Console.WriteLine("ARG COUNT IS WRONG");
                                }
                            }
                            break;
                        case "load":
                            {
                                if(cmd.Length == 2)
                                {
                                    try
                                    {
                                        LoadedAssembly = Assembly.LoadFile(cmd[1]);
                                    }
                                    catch(Exception e)
                                    {
                                        Console.WriteLine("CAN'T LOAD FILE\r\n" + e.ToString());
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("ARG COUNT IS WRONG");
                                }
                            }
                            break;
                        case "exit":
                            {
                                can_not_exit = false;
                            }
                            break;
                        default:
                            continue;
                    }
                }

                return 0;
            }

            try
            {
                LoadedAssembly = Assembly.LoadFrom(args[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR 0002 : CAN'T LOAD ASSET FILE");
                return -1;
            }

            for (int i = 1; i < args.Length; i++)
            {
                switch (args[i])
                {
                    // Visual Mode
                    case "-v":
                        {
                            VisualMode = true;
                        }
                        break;
                    // Output File
                    case "-f":
                        {
                            i++;
                            if (i == args.Length)
                            {
                                Console.WriteLine("ERROR 0003 : FILE OUTPUT COMMAND IS WRONG");
                                return -1;
                            }
                            else
                            {
                                FileName = args[i];
                            }
                        }
                        break;
                    // Type Name
                    case "-t":
                        {
                            i++;
                            if (i == args.Length)
                            {
                                Console.WriteLine("ERROR 0004 : TYPE COMMAND IS WRONG");
                                return -1;
                            }
                            else
                            {
                                TypeName = args[i];
                            }
                        }
                        break;
                    // HTML
                    case "-html":
                        {
                            HTMLMode = true;
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("ERROR 0005 : ARGS AREN'T EXIST");
                        }
                        return -1;

                }
            }

            if (VisualMode)
            {
                Application.Run(new VisualMode());
            }
            else
            {
                ConsoleMode.ConsoleMain();
            }

            return 0;
        }
    }

    static class StringEx
    {
        public static string[] SplitDoubleQuotation(this string s, bool ignoreDoubleQuotation = true)
        {
            List<string> list = new List<string>();

            bool inDoubleQuotation = false;

            StringBuilder sb = new StringBuilder();

            foreach(char c in s)
            {
                if((!inDoubleQuotation) && c == ' ')
                {
                    list.Add(sb.ToString());
                    sb.Clear();
                }
                else if(c == '\"')
                {
                    inDoubleQuotation = !inDoubleQuotation;
                    if(!ignoreDoubleQuotation) sb.Append(c);
                }
                else
                {
                    sb.Append(c);
                }
            }

            list.Add(sb.ToString());

            return list.ToArray();
        }
    }
}
