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
                    StringBuilder sb = new StringBuilder();

                    foreach (var type in types)
                    {
                        sb.AppendLine(InstanceWriter.GetHTMLText(type));
                    }

                    WriteHTML(Program.FileName, Program.AssetFile.FullName, sb.ToString());

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
                var type = Program.AssetFile.GetType(Program.TypeName);
                if (type == null)
                {
                    Console.WriteLine("ERROR 1001 : CAN'T LOAD " + Program.TypeName);
                    return;
                }

                if (Program.FileName == null)
                {
                    InstanceWriter.WriteToConsole(type);
                }
                else if (Program.HTMLMode)
                {
                    WriteHTML(Program.FileName, type.Name, InstanceWriter.GetHTMLText(type));

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

        static void WriteHTML(string path, string title, string text)
        {
            string template = "";
            template = File.ReadAllText(
                Path.GetDirectoryName(
                    System.Reflection.Assembly.GetExecutingAssembly().Location 
                    ) +
                    "\\Template.html");

            template = template.Replace("$$title$$", title);
            template = template.Replace("$$text$$", text);
            template = template.Replace("$$cssPath$$", 
                Path.GetDirectoryName(
                        System.Reflection.Assembly.GetExecutingAssembly().Location
                    ) +
                    "\\ShavedIce_HTML_Style.css");

            File.WriteAllText(Program.FileName, template);
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
