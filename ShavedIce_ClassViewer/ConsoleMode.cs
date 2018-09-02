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
                var types = Program.LoadedAssembly.GetTypes();

                if (Program.FileName == "")
                {
                    foreach (var type in types)
                    {
                        InstanceWriter.WriteToConsole(type);
                    }
                }
                else if (Program.HTMLMode)
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (var type in types)
                    {
                        sb.AppendLine(InstanceWriter.GetHTMLText(type));
                    }

                    InstanceWriter.WriteHTML(Program.FileName, Program.LoadedAssembly.FullName, sb.ToString());

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
                var type = Program.LoadedAssembly.GetType(Program.TypeName);
                if (type == null)
                {
                    Console.WriteLine("ERROR 1001 : CAN'T LOAD " + Program.TypeName);
                    return;
                }

                if (Program.FileName == "")
                {
                    InstanceWriter.WriteToConsole(type);
                }
                else if (Program.HTMLMode)
                {
                    InstanceWriter.WriteHTML(Program.FileName, type.Name, InstanceWriter.GetHTMLText(type));

                    System.Diagnostics.Process.Start(Program.FileName);
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
