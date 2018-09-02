using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace ShavedIce_ClassViewer
{
    public static class InstanceWriter
    {
        internal static List<InstanceOrder> Output(Type type)
        {
            var list = new List<InstanceOrder>();

            list.Add(type.Name, 0);
            {
                list.Add("fullname : " + type.FullName, 1);

                foreach (var method in
                    type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
                {
                    list.Add(
                        (method.IsPublic ? "public " : "") +
                        (method.IsPrivate ? "private " : "") +
                        (method.IsStatic ? "static " : "") +
                        method.ReturnParameter.ParameterType.Name + " " + 
                        method.Name +
                        "(" + method.GetParameters().ToStringAll((param) =>
                            {
                                return param.ParameterType.Name + " " + param.Name;
                            }, ",") + 
                        ")", 1);
                }

                foreach (var field in
                    type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
                {
                    list.Add(
                        (field.IsPublic ? "public " : "") +
                        (field.IsPrivate ? "private " : "") +
                        (field.IsStatic ? "static " : "") +
                        field.FieldType + " " +
                        field.Name, 1);
                }
            }

            return list;
        }

        public static void WriteToConsole(Type type)
        {
            var list = Output(type);
            foreach (var order in list)
            {
                if (order.indent == 0)
                {
                    using (new ConsoleChangeColor(ConsoleColor.Green))
                        Console.WriteLine(order.ToString());
                }
                else Console.WriteLine(order.ToString());
            }
        }

        public static void WriteToTextFile(StreamWriter sw, Type type)
        {
            var list = Output(type);
            foreach (var order in list)
            {
                sw.WriteLine(order.ToString());
            }
        }

        [Obsolete]
        public static void WriteToHTMLFile(StreamWriter sw, Type type)
        {
            var list = Output(type);
            foreach (var order in list)
            {
                sw.WriteLine(order.ToStringHTML());
            }
        }

        public static string GetHTMLText(Type type)
        {
            var list = Output(type);
            StringBuilder sb = new StringBuilder();
            foreach (var order in list)
            {
                sb.AppendLine(order.ToStringHTML());
            }
            return sb.ToString();
        }

        public static void WriteHTML(string path, string title, string text)
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

    class InstanceOrder
    {
        public string text;
        public int indent;

        public InstanceOrder(string text, int indent)
        {
            this.text = text;
            this.indent = indent;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < indent; i++)
            {
                sb.Append("\t");
            }
            sb.Append(text);
            return sb.ToString();
        }
        
        public string ToStringHTML()
        {
            StringBuilder sb = new StringBuilder();

            string text = this.text;
            text = text.Replace("<", "&lt;");
            text = text.Replace(">", "&gt;");

            if(indent == 0)
            {
                sb.Append("<h2 id=\"" + text + "\">" + text + "</h2>");
            }
            else
            {
                sb.Append("<p>" + text + "</p>");
            }
            
            return sb.ToString();
        }
    }

    static class ArrayEx
    {
        public static string ToStringAll<T>(this T[] data, Func<T, string> func, string separeter = "")
        {
            string s = "";
            for(int i = 0; i < data.Length; i++)
            {
                s += (func(data[i]));
                if(i < data.Length - 1)
                {
                    s += separeter;
                }
            }
            return s;
        }

        public static string ToStringAll(this string[] str, string separeter = "")
        {
            string s = "";
            for (int i = 0; i < str.Length; i++)
            {
                s += str[i];
                if (i < str.Length - 1)
                {
                    s += separeter;
                }
            }
            return s;
        }

        public static void Add(this List<InstanceOrder> instanceOrders, string text, int indent)
        {
            instanceOrders.Add(new InstanceOrder(text, indent));
        }
    }
}
