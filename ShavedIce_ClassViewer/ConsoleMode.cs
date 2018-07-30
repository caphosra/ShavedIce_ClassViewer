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
                        WriteHTMLHeader(sw, Program.AssetFile.FullName);
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
                    using (StreamWriter sw = new StreamWriter(Program.FileName))
                    {
                        WriteHTMLHeader(sw, type.Name);
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

        static void WriteHTMLHeader(StreamWriter sw, string title)
        {
            sw.WriteLine("<!DOCTYPE html>");
            sw.WriteLine("<html>");
            sw.WriteLine(" <head>");
            sw.WriteLine("  <title>" + title + "</title>");
            sw.WriteLine("  <script type=\"text/javascript\" src=\"http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js\"></script>");
            sw.WriteLine("  <script type=\"text/javascript\">");
            sw.WriteLine("   function goToClass()");
            sw.WriteLine("   {");
            sw.WriteLine("    id = '#' + document.forms.classFinder.textBox.value;");
            sw.WriteLine("    if($(id).length)");
            sw.WriteLine("    {");
            sw.WriteLine("     speed = 100;");
            sw.WriteLine("     var position = $(id).offset().top;");
            sw.WriteLine("     $(\"html, body\").animate({ scrollTop: position }, speed, \"swing\");");
            sw.WriteLine("    }");
            sw.WriteLine("    else");
            sw.WriteLine("    {");
            sw.WriteLine("     window.alert(id + \" isn't exist\");");
            sw.WriteLine("    }");
            sw.WriteLine("   }");
            sw.WriteLine("  </script>");
            sw.WriteLine(" </head>");
            sw.WriteLine(" <body>");
            sw.WriteLine("  <form id=\"classFinder\" action=\"\">");
            sw.WriteLine("   <input id=\"textBox\" type=\"text\" value = \"\"/>");
            sw.WriteLine("   <input type=\"button\" value=\"GO\" onClick=\"goToClass();\"/>");
            sw.WriteLine("  </form>");
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
