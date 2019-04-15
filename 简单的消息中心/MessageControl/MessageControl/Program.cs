using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MessageControl
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            //GenericCacheTest.Show();

            //foreach (int i in GetIndex())
            //{

            //}
            //List<Student>
            Console.ReadKey();
        }

        private static List<int> GetIndex()
        {
            Console.WriteLine("aa" + "\r\n");
            return new List<int>() { 1, 2, 3 };
        }
    }
}
