#pragma warning disable 168

using System;
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
        public static Assembly AssetFile { get; private set; }

        public static bool VisualMode { get; private set; }
        public static bool HTMLMode { get; private set; }

        public static string FileName { get; private set; }
        public static string TypeName { get; private set; }

        static int Main(string[] args)
        {
            VisualMode = false;
            HTMLMode = false;
            FileName = null;
            TypeName = null;

            if (args.Length == 0)
            {
                Console.WriteLine("ERROR 0001 : ARGS ARE NULL");
                return -1;
            }

            try
            {
                AssetFile = Assembly.LoadFrom(args[0]);
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
}
