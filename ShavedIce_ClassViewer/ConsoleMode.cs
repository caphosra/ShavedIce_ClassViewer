using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ShavedIce_ClassViewer
{
    static class ConsoleMode
    {
        public static void ConsoleMain()
        {
            if (Program.TypeName == null)
            {
                var types = Program.AssetFile.GetTypes();

                if (Program.FileName == null)
                {
                    foreach (var type in types)
                    {
                        InstanceWriter.WriteToConsole(type);
                    }
                }
                else if (Program.HTMLMode)
                {
                    using (StreamWriter sw = new StreamWriter(Program.FileName))
                    {
                        sw.Write("<!DOCTYPE html><html><body>");
                        foreach (var type in types)
                        {
                            InstanceWriter.WriteToHTMLFile(sw, type);
                        }
                        sw.Write("</body></html>");
                    }
                    System.Diagnostics.Process.Start(Program.FileName);
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(Program.FileName))
                    {
                        foreach (var type in types)
                        {
                            InstanceWriter.WriteToTextFile(sw, type);
                        }
                    }
                }

            }
            else
            {
                Type type = null;
                try
                {
                    type = Program.AssetFile.GetType(Program.TypeName);
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR 1001 : CAN'T LOAD " + Program.TypeName);
                }

                if (Program.FileName == null)
                {
                    InstanceWriter.WriteToConsole(type);
                }
                else if (Program.HTMLMode)
                {
                    using (StreamWriter sw = new StreamWriter(Program.FileName))
                    {
                        sw.Write("<!DOCTYPE html><html><body>");
                        InstanceWriter.WriteToHTMLFile(sw, type);
                        sw.Write("</body></html>");
                    }
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(Program.FileName))
                    {
                        InstanceWriter.WriteToTextFile(sw, type);
                    }
                }
            }
        }
    }

    public class ConsoleChangeColor : IDisposable
    {
        public ConsoleChangeColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        public void Dispose()
        {
            Console.ResetColor();
        }
    }
}
