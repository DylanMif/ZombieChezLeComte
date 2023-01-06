using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ZombieChezLeComte
{
    public class DataSaver
    {
        public static void SaveNight(int nightNumber)
        {
            StreamWriter sw = new StreamWriter("data.txt");
            sw.WriteLine(nightNumber.ToString());
            sw.Close();
        }

        public static int LoadNight()
        {
            StreamReader sr = new StreamReader("data.txt");
            String text = sr.ReadLine();
            sr.Close();
            if(text is null)
            {
                return 0;
            }
            return int.Parse(text);
        }
    }
}
