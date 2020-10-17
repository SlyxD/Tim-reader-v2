using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Windows;

namespace Tim_reader_v2
{
    
    public static class Utilities
    {
        public static void ShowMessageBox(string error)
        {
            MessageBox.Show(error);
        }

        public static string FormatTime(decimal EndTimestamp, decimal StartTimestamp)
        {
            decimal Time = (EndTimestamp - StartTimestamp) / (decimal)60000;

            if (Time > 60)
            {
                Time /= 60;
                int hours = (int)Math.Truncate(Time);
                decimal mins = (Time - hours) * 60;
                int minswhole = (int)Math.Truncate(mins);
                decimal secs = (mins - minswhole) * 60;
                int secswhole = (int)Math.Truncate(secs);
                decimal milsecs = (secs - secswhole);
                milsecs = Math.Round(milsecs, 0);
                return hours + ":" + minswhole.ToString("D2") + ":" + secswhole.ToString("D2") + "." + milsecs;

            }

            else
            {
                decimal mins = Time;
                int minswhole = (int)Math.Truncate(mins);
                decimal secs = (mins - minswhole) * 60;
                int secswhole = (int)Math.Truncate(secs);
                decimal milsecs = (secs - secswhole);
                milsecs = Math.Round(milsecs, 0);
                return minswhole.ToString("D2") + ":" + secswhole.ToString("D2") + "." + milsecs;
            }

        }
        public static decimal Time(decimal EndTimestamp, decimal StartTimestamp)
        {
            var roundtime = EndTimestamp - StartTimestamp;
            return roundtime;
        }
        public static long GetSetUpRound(MetaAndTXTObjects info)
        {
            foreach (Round currentround in info.Rounds)
            {
                if (info.SetUpTimestamp < currentround.StartTimestamp)
                {

                    return (currentround.Number - 1);
                }

            }
            return 0;
        }
        public static double CalculateZombieCount(UInt32 PlayerCount, long Round)
        {
            switch (PlayerCount)
            {
                case 1:
                    if (Round < 20)
                    {
                        double[] c = { 6, 8, 13, 18, 24, 27, 28, 28, 29, 33, 34, 36, 39, 41, 44, 47, 50, 53, 56 };
                        return c[Round - 1];
                    }
                    return Math.Round(0.09 * Round * Round - 0.0029 * Round + 23.958);
                case 2:
                    if (Round < 20)
                    {
                        double[] c = { 7, 9, 15, 21, 27, 31, 32, 33, 34, 42, 45, 49, 54, 59, 64, 70, 76, 82, 89 };
                        return c[Round - 1];
                    }
                    return Math.Round(0.1882 * Round * Round - 0.4313 * Round + 29.212);
                case 3:
                    if (Round < 20)
                    {
                        double[] c = { 11, 14, 23, 32, 41, 47, 48, 50, 51, 62, 68, 74, 81, 89, 97, 105, 114, 123, 133 };
                        return c[Round - 1];
                    }
                    return Math.Round(0.2637 * Round * Round + 0.1802 * Round + 35.015);
                case 4:
                    if (Round < 20)
                    {
                        double[] c = { 14, 18, 30, 42, 54, 62, 64, 66, 68, 83, 91, 99, 108, 118, 129, 140, 152, 164, 178 };
                        return c[Round - 1];
                    }
                    return Math.Round(0.35714 * Round * Round - 0.0714 * Round + 50.4286);
                default:
                    return 0;
            }
        }
        public static string MapNameConv(string levelcodename)
        {
            string MapName = "";
            if (levelcodename == "zombie_cod5_prototype")
            {
                MapName = "Nacht Der Untoten";
            }
            else if (levelcodename == "zombie_cod5_asylum")
            {
                MapName = "verrückt";
            }
            else if (levelcodename == "zombie_coast")
            {
                MapName = "Call Of The Dead";
            }
            else if (levelcodename == "zombie_temple")
            {
                MapName = "Shangri-La";
            }
            else if (levelcodename == "zombie_moon")
            {
                MapName = "Moon";
            }
            else if (levelcodename == "zombie_cod5_sumpf")
            {
                MapName = "Shi No Numa";
            }
            else if (levelcodename == "zombie_cod5_factory")
            {
                MapName = "Der Riese";
            }
            else if (levelcodename == "zombie_theater")
            {
                MapName = "Kino Der Toten";
            }
            else if (levelcodename == "zombie_pentagon")
            {
                MapName = "Five";
            }
            else if (levelcodename == "zombie_cosmodrome")
            {
                MapName = "Ascension";
            }
            return MapName;
        }
        public static string SpecialRoundType(string levelcodename)
        {
            string SpecialRoundName = "";

            if (levelcodename == "zombie_cod5_prototype" || levelcodename == "zombie_cod5_asylum" || levelcodename == "zombie_coast" || levelcodename == "zombie_temple" || levelcodename == "zombie_paris")
            {
                SpecialRoundName = "no special rounds";
            }
            else if (levelcodename == "zombie_cod5_sumpf" || levelcodename == "zombie_cod5_factory" || levelcodename == "zombie_theater")
            {
                SpecialRoundName = "Dog Rounds:";
            }
            else if (levelcodename == "zombie_pentagon")
            {
                SpecialRoundName = "Doctor/Scientist Rounds:";
            }
            else if (levelcodename == "zombie_cosmodrome")
            {
                SpecialRoundName = "Monkey/Chino Rounds;";
            }
            return SpecialRoundName;
        }
    }
}
