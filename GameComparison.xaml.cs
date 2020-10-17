using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.ExceptionServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;

namespace Tim_reader_v2
{
    /// <summary>
    /// Interaction logic for GameComparison.xaml
    /// </summary>
    public partial class GameComparison : Window
    {
        public GameComparison()
        {
            InitializeComponent();
        }
        //Logs
        public void IRCLog(string message)
        {
            txtIRC.Text += message + Environment.NewLine;
        }
        public void GCLog(string message)
        {
            txtGC.Text += message + Environment.NewLine;
        }
        
        //Labels
        public void GCLabel(string message)
        {
            lblGC.Text += message + Environment.NewLine;
        }
        //functions 
        public Rounders Specialroundcollection(MetaAndTXTObjects Game1,MetaAndTXTObjects Game2)
        {

            Rounders rounders = new Rounders();

            int lastfoundg1 = 0;
            int lastfoundg2 = 0;
            int InvaildGame1rounders = 0;
            int InvaildGame2rounders = 0;

            foreach (Round roundg1 in Game1.Rounds)
            {
                if (roundg1.IsSpecialRound == true)
                {

                    if (lastfoundg1 != 0)
                    {
                        if ((roundg1.Number - lastfoundg1) % 4 == 0)
                        {
                            rounders.G1FourRounders += 1;
                        }
                        else if ((roundg1.Number - lastfoundg1) % 5 == 0)
                        {
                            rounders.G1FiveRounders += 1;

                        }
                        else
                        {
                          
                            InvaildGame1rounders += 1;
                        }



                    }

                    rounders.game1specialrounds.Add(roundg1.Number.ToString());
                    lastfoundg1 = (int)roundg1.Number;
                }
                if (roundg1.Number == Game2.CurrentRound.Number)
                {
                    break;
                }
            }
            foreach (Round roundg2 in Game2.Rounds)
            {
                    if (roundg2.IsSpecialRound == true)
                    {

                        if (lastfoundg2 != 0)
                        {
                            if ((roundg2.Number - lastfoundg2) % 4 == 0)
                            {
                                rounders.G2FourRounders += 1;
                            }
                            else if ((roundg2.Number - lastfoundg2) % 5 == 0)
                            {
                                rounders.G2FiveRounders += 1;

                            }
                            else
                            {
                                
                                InvaildGame2rounders += 1;
                             
                            }

                        }

                        rounders.game2specialrounds.Add(roundg2.Number.ToString());
                        lastfoundg2 = (int)roundg2.Number;
                    }
                if (roundg2.Number == Game1.CurrentRound.Number)
                {
                    break;
                }
            }
            if(InvaildGame1rounders != 0)
            {
                Utilities.MessageBox($" The {Game1.CurrentRound.Number.ToString()} game has invaild rounders");
            }
            if (InvaildGame2rounders != 0)
            {
                Utilities.MessageBox($" The {Game2.CurrentRound.Number.ToString()} game has invaild rounders");
            }
            return rounders;
        }
        public void SpecialRoundLog(Rounders rounders, MetaAndTXTObjects Game1, MetaAndTXTObjects Game2)
        {
             
            

            GCLog(Utilities.SpecialRoundType(Game1.LevelName));
            if (Game1.CurrentRound.Number > Game2.CurrentRound.Number)
            {
                GCLog($"Round: {Game1.CurrentRound.Number} Game up until Round: {Game2.CurrentRound.Number}");
                GCLog(string.Join(", ", rounders.game1specialrounds));
            }
            else
            {
                GCLog($"Round: {Game1.CurrentRound.Number} Game");
                GCLog(string.Join(", ", rounders.game1specialrounds));
            }


            if (Game2.CurrentRound.Number > Game1.CurrentRound.Number)
            {
                GCLog($"Round: {Game2.CurrentRound.Number} Game up until Round: {Game1.CurrentRound.Number}");
                GCLog(string.Join(", ", rounders.game2specialrounds));
            }
            else
            {
                GCLog($"Round: {Game2.CurrentRound.Number} Game");
                GCLog(string.Join(", ", rounders.game1specialrounds));
            };
            GCLog($"Four Rounders: {rounders.G1FourRounders} | {rounders.G2FourRounders}");
            GCLog($"Five Rounders: {rounders.G1FiveRounders} | {rounders.G2FiveRounders}");

        }
        public void IDC(MetaAndTXTObjects Game1, MetaAndTXTObjects Game2)
        {
            //game1 sph and roundtime list grabing
            List<decimal> sphg1 = new List<decimal>();
            List<decimal> roundtimeinming1 = new List<decimal>();
            
            
            foreach (Round currentround in Game1.Rounds)
            {

                //call to zombie count function and convert to hordes 

                double Zombies = Utilities.CalculateZombieCount((uint)Game1.PlayerCount, currentround.Number);

                decimal Hordes = (decimal)(Zombies / 24);


                decimal sph;
                decimal roundtime = Utilities.Time(currentround.EndTimestamp, currentround.StartTimestamp);
                roundtime /= 60000;

                //calculate sph
                
                    sph = roundtime / Hordes;
                    sph *= 60;
                    sph = Math.Round(sph, 2);
                    sphg1.Add(sph);
                    roundtimeinming1.Add(roundtime);
                
                if (currentround.Number == Game2.CurrentRound.Number)
                {
                    break;
                }

            }
            //game2 sph and roundtime list grabing
            List<decimal> sphg2 = new List<decimal>();
            List<decimal> roundtimeinming2 = new List<decimal>();
            foreach (Round currentround in Game2.Rounds)
            {

                //call to zombie count function and convert to hordes 

                double Zombies = Utilities.CalculateZombieCount((uint)Game2.PlayerCount, currentround.Number);

                decimal Hordes = (decimal)(Zombies / 24);


                decimal sph;
                decimal roundtime = Utilities.Time(currentround.EndTimestamp, currentround.StartTimestamp);
                roundtime /= 60000;

                //calculate sph
                
                    sph = roundtime / Hordes;
                    sph *= 60;
                    sph = Math.Round(sph, 2);
                    sphg2.Add(sph);
                    roundtimeinming2.Add(roundtime);
                
                if (currentround.Number == Game1.CurrentRound.Number)
                {
                    break;
                }

            }
            
            
            if(Game1.CurrentRound.Number > Game2.CurrentRound.Number)
            {
                int minrounds = Math.Min(Game1.Rounds.Count(), Game2.Rounds.Count());
                for (int i = 0; i < minrounds; i++)
                {
                    if (!Game1.Rounds[i].IsSpecialRound && !Game2.Rounds[i].IsSpecialRound)
                    {
                        
                        decimal sphdelta = Math.Round(sphg1[(int)(Game1.Rounds[i].Number - 1)] - sphg2[(int)(Game1.Rounds[i].Number - 1)], 2);
                        decimal roundtimedelta = Math.Round(roundtimeinming1[(int)(Game1.Rounds[i].Number - 1)] - roundtimeinming2[(int)(Game1.Rounds[i].Number - 1)], 2);
                        if (Math.Abs(roundtimedelta) >= 1)
                        {
                            IRCLog($"Round: {Game1.Rounds[i].Number} | SphΔ: {sphdelta} | TimeΔ: {roundtimedelta} (Min's) ");
                        }
                        else if (Math.Abs(roundtimedelta) < 1)
                        {
                            roundtimedelta *= 60;
                            IRCLog($"Round: {Game1.Rounds[i].Number} | SphΔ: {sphdelta} | TimeΔ: {roundtimedelta} (sec's) ");
                        }


                    }
                    else
                    {

                        decimal roundtimedelta = Math.Round(roundtimeinming1[(int)(Game1.Rounds[i].Number - 1)] - roundtimeinming2[(int)(Game1.Rounds[i].Number - 1)], 2);
                        if (Math.Abs(roundtimedelta) >= 1)
                        {
                            IRCLog($"Round: {Game1.Rounds[i].Number} | TimeΔ: {roundtimedelta} (Min's) ");
                        }
                        else if (Math.Abs(roundtimedelta) < 1)
                        {
                            roundtimedelta *= 60;
                            IRCLog($"Round: {Game1.Rounds[i].Number} | TimeΔ: {roundtimedelta} (sec's) ");
                        }
                    }
                }
            }
            else if (Game2.CurrentRound.Number > Game1.CurrentRound.Number || Game2.CurrentRound.Number == Game1.CurrentRound.Number)
            {
                int minrounds = Math.Min(Game1.Rounds.Count(), Game2.Rounds.Count());
                for (int i = 0; i < minrounds; i++)
                {
                     if (!Game1.Rounds[i].IsSpecialRound && !Game2.Rounds[i].IsSpecialRound)
                        {
                            decimal sphdelta = Math.Round(sphg2[(int)(Game1.Rounds[i].Number - 1)] - sphg1[(int)(Game1.Rounds[i].Number - 1)], 2);
                            decimal roundtimedelta = Math.Round(roundtimeinming2[(int)(Game1.Rounds[i].Number - 1)] - roundtimeinming1[(int)(Game1.Rounds[i].Number - 1)], 2);
                            if (Math.Abs(roundtimedelta) >= 1)
                            {
                                IRCLog($"Round: {Game1.Rounds[i].Number} | TimeΔ: {roundtimedelta} (Min's) | SphΔ: {sphdelta} ");
                            }
                            else if (Math.Abs(roundtimedelta) < 1)
                            {
                                roundtimedelta *= 60;
                                IRCLog($"Round: {Game1.Rounds[i].Number} | TimeΔ: {roundtimedelta} (sec's) | SphΔ: {sphdelta} ");
                            }


                     }
                        else
                        {

                            decimal roundtimedelta = Math.Round(roundtimeinming2[(int)(Game1.Rounds[i].Number - 1)] - roundtimeinming1[(int)(Game1.Rounds[i].Number - 1)], 2);
                            if (Math.Abs(roundtimedelta) >= 1)
                            {
                                IRCLog($"Round: {Game1.Rounds[i].Number} | TimeΔ: {roundtimedelta} (Min's) ");
                            }
                            else if (Math.Abs(roundtimedelta) < 1)
                            {
                                roundtimedelta *= 60;
                                IRCLog($"Round: {Game1.Rounds[i].Number} | TimeΔ: {roundtimedelta} (sec's) ");
                            }
                        }
                    
                }
            }
            
        }

        public void TimetoΔ(MetaAndTXTObjects Game1,MetaAndTXTObjects Game2)
        {
            //game1 data collection

            int minrounds = Math.Min(Game1.Rounds.Count(), Game2.Rounds.Count());
            List<decimal> g1splits = new List<decimal>();
            List<long> SplitRounds = new List<long>();
            for (int i = 0; i < minrounds; i++)
            {
                if (Game1.Rounds[i].Number % 10 == 0 && Game1.Rounds[i].Number >= 50 || Game1.Rounds[i].Number == 30)
                {
                    SplitRounds.Add(Game1.Rounds[i].Number);
                    g1splits.Add(Utilities.Time(Game1.Rounds[i].StartTimestamp, Game1.StartTimestamp));

                }
            }
            //game2 data collection
            List<decimal> g2splits = new List<decimal>();
            for (int i = 0; i < minrounds; i++)
            {
                if (Game2.Rounds[i].Number % 10 == 0 && Game1.Rounds[i].Number >= 50 || Game1.Rounds[i].Number == 30)
                {

                    g2splits.Add(Utilities.Time(Game2.Rounds[i].StartTimestamp, Game2.StartTimestamp));

                }
            }
            decimal TimeinHrs;
            decimal TimeinMins;
            decimal TimeinSecs;
            List<decimal> TimetoΔ = new List<decimal>();
            List<TimeToDeltaInfo>  DeltaList = new List<TimeToDeltaInfo>();
            for (int i = 0; i < g1splits.Count(); i++)
            {
                TimetoΔ.Add(g1splits[i] - g2splits[i]);
                if (Math.Abs(TimetoΔ[i]) >= 3600000)
                {
                    TimeinHrs = (TimetoΔ[i]) / (decimal)3600000;
                    GCLog($"{SplitRounds[i]} : {Math.Round(TimeinHrs,2)} Hours");
                }
                else if (Math.Abs(TimetoΔ[i]) >= 60000)
                {
                    TimeinMins = (TimetoΔ[i]) / (decimal)60000;
                    GCLog($"{SplitRounds[i]} : {Math.Round(TimeinMins, 2)} Minutes");
                }
                else if (Math.Abs(TimetoΔ[i]) >= 1000)
                {
                    TimeinSecs = (TimetoΔ[i]) / (decimal)1000;
                    GCLog($"{SplitRounds[i]} : {Math.Round(TimeinSecs, 2)} Seconds");
                }

            }


        }

        public void TimComparison(MetaAndTXTObjects Game1, MetaAndTXTObjects Game2)
        {
            //Checks
            if (Game1 == null && Game2 == null)
            {
               Utilities.MessageBox("Game/Games are not vaild");
                return;
            } 
            
           if(Game1.PlayerCount != Game2.PlayerCount || Game1.LevelName != Game2.LevelName)
           {
                Utilities.MessageBox("Game comparisons can only be done with games of the same map and same player count");
                return;
           }

           //Gamelabel
            string Game1MapName = Utilities.MapNameConv(Game1.LevelName);
            string Game2MapName = Utilities.MapNameConv(Game2.LevelName);
            if(Game1.PlayerCount == 1)
            {
                GCLabel($"{Game1MapName} | Round: {Game1.CurrentRound.Number} vs. Round: {Game2.CurrentRound.Number} | Solo ");
            }
            else
            {
                GCLabel($"{Game1MapName} | Round: {Game1.CurrentRound.Number} vs. Round: {Game2.CurrentRound.Number} | {Game1.PlayerCount} Player ");
            }

            if (Game1.Fh != null && Game2.Fh != null)
            {
                // header
                GCLog($" Round: {Game1.CurrentRound.Number} | Round: {Game2.CurrentRound.Number}");
                GCLog("");
                //special round  stuff
                if (Utilities.SpecialRoundType(Game1.LevelName) != "no special rounds")
                {
                    Rounders rounders = Specialroundcollection(Game1, Game2);
                    SpecialRoundLog(rounders, Game1, Game2);
                }
                //Indualvidual round comparison
                IRCLog($" (Round: {Game1.CurrentRound.Number} stats)  - (Round: {Game2.CurrentRound.Number} stats)");
                IDC(Game1, Game2);
                TimetoΔ(Game1, Game2);
            }
            else
            {
                // header
                GCLog($" Round: {Game1.CurrentRound.Number} | Round: {Game2.CurrentRound.Number}");
                GCLog("");
                //special round  stuff
                if(Utilities.SpecialRoundType(Game1.LevelName) != "no special rounds")
                {
                    Rounders rounders = Specialroundcollection(Game1, Game2);
                    SpecialRoundLog(rounders, Game1, Game2);
                }
                IRCLog($" (Round: {Game1.CurrentRound.Number} stats)  - (Round: {Game2.CurrentRound.Number} stats)");
                IDC(Game1, Game2);
                TimetoΔ(Game1, Game2);
            }


            this.Show();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
