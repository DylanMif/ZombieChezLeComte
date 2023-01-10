using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ZombieChezLeComte
{
    /// <summary>
    /// Classe pour gérer la sauvegarde des données
    /// Ce jeu retient deux choses :
    /// -un int contenant le numero de la nuit à laquelle s'est arrêté le joueur (1, 2, 3, 4 ou 5)
    /// -un int (valant 0 ou 1 pour false ou true) retenant si le joueur à fini la nuit 5 au moins une fois
    /// </summary>
    public class DataSaver
    {
        /// <summary>
        /// Méthode permettant de sauvegarde un numéro de nuit
        /// </summary>
        /// <param name="nightNumber">le numéro de nuit à sauvegarder </param>
        public static void SaveNight(int nightNumber)
        {
            if(nightNumber < 0 || nightNumber > 5)
            {
                throw new ArgumentException("Le numéros de la nuit doit être compris entre 0 et 5");
            }
            string a = DataSaver.LoadEnd().ToString();
            StreamWriter sw = new StreamWriter("data.txt");
            sw.WriteLine(nightNumber.ToString() + "," + a);
            sw.Close();
        }
        /// <summary>
        /// Méthode pour sauvegarder si le joueur à fini la nuit 5 ou non
        /// </summary>
        /// <param name="end">0 pour false ou 1 pour true</param>
        /// <exception cref="Exception"></exception>
        public static void SaveEnd(int end)
        {
            if(end != 0 && end != 1)
            {
                throw new Exception("Le paramètre end doit valoir soit 0 (pour false) soit 1 (pour true)");
            }
            string a = DataSaver.LoadNight().ToString();
            StreamWriter sw = new StreamWriter("data.txt");
            sw.WriteLine(a + "," + end.ToString());
            sw.Close();
        }

        /// <summary>
        /// Renvoie le numéros de nuit sauvegardé
        /// </summary>
        /// <returns>Le numéros de nuit sauvegarder</returns>
        public static int LoadNight()
        {
            StreamReader sr = new StreamReader("data.txt");
            String text = sr.ReadLine();
            sr.Close();
            if(text is null)
            {
                return 1;
            }
            return int.Parse(text.Split(",")[0]);
        }

        /// <summary>
        /// Renvoie si le joueur à fini au moins une fois la nuit 5
        /// </summary>
        /// <returns>1 pour true ou 0 pour false</returns>
        public static int LoadEnd()
        {
            StreamReader sr = new StreamReader("data.txt");
            String text = sr.ReadLine();
            sr.Close();
            if (text is null)
            {
                return 0;
            }
            return int.Parse(text.Split(",")[1]);
        }
    }
}
