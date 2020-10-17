using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
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

namespace Tim_reader_v2
{
    /// <summary>
    /// Interaction logic for GSwindow.xaml
    /// </summary>


    public partial class GSwindow : Window
    {

        public GSwindow()
        {
            InitializeComponent();
        }



        //Logs
        public void GameInfoLog(string message)
        {
            txtLogGameInfo.Text += message + Environment.NewLine;
        }
        public void TimesLog(string message)
        {
            txtLogTimes.Text += message + Environment.NewLine;
        }

        public void RoundInfoLog(string message)
        {
            txtLogRoundInfo.Text += message + Environment.NewLine;
        }
        public void TradeLog(string message)
        {
            txtLogTradeInfo.Text += message + Environment.NewLine;
        }
        //labels
        public void GameLabelLog(string message)
        {

            txtblockGameLabel.Text += message + Environment.NewLine;
        }
        public void GameLabelRoundLog(string message)
        {

            txtblockGameRound.Text += message + Environment.NewLine;
        }
        public void GameLabelTimes(string message)
        {

            txtblockGameTimes.Text += message + Environment.NewLine;
        }
        public void GameLabelRoundInfo(string message)
        {

            txtblockRoundInfo.Text += message + Environment.NewLine;
        }
        //parent functions
        public void gamelabel(MetaAndTXTObjects Info)
        {

            if (Info.PlayerCount == 1)
            {
                string MapName = Utilities.MapNameConv(Info.LevelName);


                GameLabelLog(MapName + " | Solo | ");
                GameLabelRoundLog(" Round: " + Info.CurrentRound.Number);

            }
            else if (Info.PlayerCount > 1)
            {
                string MapName = Utilities.MapNameConv(Info.LevelName);
                GameLabelLog(MapName + " | " + Info.PlayerCount + " Player | ");
                GameLabelRoundLog(" Round: " + Info.CurrentRound.Number);

                GameLabelLog(" ");

            }
        }
        public void SpecialRounds(MetaAndTXTObjects Info)
        {
            List<string> specialrounds = new List<string>();
            string SpecialRoundName = Utilities.SpecialRoundType(Info.LevelName);
            if (SpecialRoundName != "no special rounds")
            {
                int Fourrounders = 0, Fiverounders = 0, lastfound = 0;
                foreach (Round round in Info.Rounds)
                {



                    if (round.IsSpecialRound == true)
                    {

                        if (lastfound != 0)
                        {
                            if ((round.Number - lastfound) % 4 == 0)
                            {
                                Fourrounders += 1;
                            }
                            else if ((round.Number - lastfound) % 5 == 0)
                            {
                                Fiverounders += 1;

                            }
                            else
                            {
                                GameInfoLog($"Not A Vaild Rounder... MODDING or Bennie sucks at coding !?! ");
                            }



                        }

                        specialrounds.Add(round.Number.ToString());
                        lastfound = (int)round.Number;
                    }
                }

                GameInfoLog(SpecialRoundName + " " + string.Join(", ", specialrounds));
                GameInfoLog($"Four rounders: {Fourrounders}");
                GameInfoLog($"Five rounders: {Fiverounders}");

            }
        }
        public void GameEndLog(MetaAndTXTObjects Info)
        {

            if (Info.EndTimestamp > Info.StartTimestamp)
            {
                RoundInfoLog("Game ended on round: " + Info.CurrentRound.Number + " at " + Utilities.FormatTime(Info.EndTimestamp, Info.StartTimestamp));
            }
            else
            { 
                RoundInfoLog("Bennie is bad at coding so the End Time Stamp was not logged");
            }
        }
        public void PlayerInfo(MetaAndTXTObjects Info)
        {
            foreach (Player currentplayer in Info.Players)
            {
                List<string> Downedrounds = new List<string>();
                long lastfoundDowns = 0;
                GameInfoLog(currentplayer.Name);
                GameInfoLog($"Downs: {currentplayer.Downs}");
                foreach (Round currentround in Info.Rounds)
                {
                    try
                    {
                        PlayerStat player = currentround.PlayerStats.AsEnumerable().SingleOrDefault((p) => p.Slot == currentplayer.Slot);
                        if (player.Downs == lastfoundDowns + 1)
                        {
                            Downedrounds.Add(currentround.Number.ToString());
                            lastfoundDowns = player.Downs;
                        }
                        else if ((player.Downs - lastfoundDowns) > 1)
                        {
                            Downedrounds.Add($" {currentround.Number} ({player.Downs - lastfoundDowns})");
                            lastfoundDowns = player.Downs;
                        }
                        else if (lastfoundDowns == player.Downs)
                        {
                            lastfoundDowns = player.Downs;
                        }
                    }
                    catch
                    {
                        continue;
                    }

                }
                GameInfoLog(string.Join(",", Downedrounds));
            }
        }
        public void SetupInfo(MetaAndTXTObjects Info)
        {
            long SetUpRound = Utilities.GetSetUpRound(Info);
            if (SetUpRound != 0)
            {
                GameInfoLog("Setup Info:");
                GameInfoLog("Round: " + SetUpRound);
                GameInfoLog("");
            }
        }
        public void TimeTo(MetaAndTXTObjects Info)
        {
            foreach (Round currentround in Info.Rounds)
            {
                if (currentround.Number % 10 == 0 && currentround.Number >= 50 || currentround.Number == 30)
                {

                    TimesLog($" {currentround.Number} - {Utilities.FormatTime(currentround.StartTimestamp, Info.StartTimestamp)}");

                }
            }
            TimesLog(" ");
        }
        public void IRS(MetaAndTXTObjects Info)
        {
            foreach (Round currentround in Info.Rounds)
            {



                //call to zombie count function and convert to hordes 

                double Zombies = Utilities.CalculateZombieCount((uint)Info.PlayerCount, currentround.Number);

                decimal Hordes = (decimal)(Zombies / 24);


                decimal sph = 0;
                decimal roundtime = Utilities.Time(currentround.EndTimestamp, currentround.StartTimestamp);
                roundtime /= 60000;




                //calculate sph
                if (currentround.IsSpecialRound == false)
                {
                    sph = roundtime / Hordes;
                    sph *= 60;
                    sph = Math.Round(sph, 2);
                }

                // special round true 
                if (roundtime > 1 && currentround.IsSpecialRound)
                {
                    roundtime = Math.Round(roundtime, 2);
                    RoundInfoLog("round: " + currentround.Number + " in " + roundtime + "Min's" + " " + (currentround.IsSpecialRound ? "special round" : ""));
                }
                else if (roundtime < 1 && currentround.IsSpecialRound)
                {
                    roundtime *= 60;
                    roundtime = Math.Round(roundtime, 2);
                    RoundInfoLog("round: " + currentround.Number + " in " + roundtime + "sec's" + " " + (currentround.IsSpecialRound ? "special round" : ""));
                }

                //round time Log < 1 min
                if (roundtime < 1 && !currentround.IsSpecialRound && currentround.TrapHits > 0)
                {
                    roundtime *= 60;
                    roundtime = Math.Round(roundtime, 2);
                    RoundInfoLog("round: " + currentround.Number + " in " + roundtime + " Sec's" + " " + "| Round sph: " + sph + "|" + " Trap hits:" + currentround.TrapHits + "|");
                }
                else if (roundtime < 1 && !currentround.IsSpecialRound && currentround.TrapHits == 0)
                {
                    roundtime *= 60;
                    roundtime = Math.Round(roundtime, 2);
                    RoundInfoLog("round: " + currentround.Number + " in " + roundtime + " Sec's" + " " + "| Round sph: " + sph + "|");

                }

                //round time Log > 1 min 
                else if (roundtime > 1 && !currentround.IsSpecialRound && currentround.TrapHits > 0)
                {
                    roundtime = Math.Round(roundtime, 2);
                    RoundInfoLog("round: " + currentround.Number + " in " + roundtime + " Min's" + " " + "| Round sph: " + sph + "|" + " Trap hits:" + currentround.TrapHits + "|");
                }

                else if (roundtime > 1 && !currentround.IsSpecialRound && currentround.TrapHits == 0)
                {
                    roundtime = Math.Round(roundtime, 2);
                    RoundInfoLog("round: " + currentround.Number + " in " + roundtime + " Min's" + " " + "| Round sph: " + sph + "|");
                }


            }




            RoundInfoLog("Round: " + Info.CurrentRound.Number.ToString() + " Started at " + Utilities.FormatTime(Info.CurrentRound.StartTimestamp, Info.StartTimestamp));
            GameEndLog(Info);
        }
        //Back Button
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
        public void TimMeta(MetaAndTXTObjects MetaInfo)
        {
            // game label
            gamelabel(MetaInfo);

            
            if ((MetaInfo.PlayerCount == 1))
            {
                PlayerInfo(MetaInfo);
            }
            else if (MetaInfo.PlayerCount > 1)
            {

                GameInfoLog(" ");
                GameInfoLog("Players and Player Stats:");

                PlayerInfo(MetaInfo);

            }
            GameInfoLog(" ");
            GameInfoLog("Game Stats:");

            //GameInfoLog(" ");
            //GameInfoLog("Game Stats:");
            /// special rounds stuff
            SpecialRounds(MetaInfo);

            GameInfoLog($"BoxHits: {MetaInfo.BoxHits}");
            GameInfoLog("");
            ///Set up Infomation 

            SetupInfo(MetaInfo);


            ///missed instas log
            if(MetaInfo.CurrentRound.Number >= 163)
            {
                List<string> MissedInstas = new List<string>();
                foreach (Round currentround in MetaInfo.Rounds)
                {
                    if (currentround.MissedInsta == true)
                    {
                        MissedInstas.Add(currentround.Number.ToString());
                    }
                }
                if (MissedInstas.Count != 0)
                {
                    GameInfoLog($"Missed Instas:{MissedInstas.Count} ({string.Join(",", MissedInstas)}) ");
                    GameInfoLog(" ");
                }
            }
            
            List<long> Tradetimes = new List<long>();
            List<long> Boxhits = new List<long>();
            List<long> Traphits = new List<long>();

            foreach (Trade Tradeinfo in MetaInfo.Trades)
            {
                Tradetimes.Add(Tradeinfo.Time);
                Traphits.Add(Tradeinfo.TrapHits);
                Boxhits.Add(Tradeinfo.BoxHits);

            }
            if (Tradetimes.Count != 0)
            {
                if (Tradetimes.Count != 0 && Boxhits.Count != 0)
                {
                    TradeLog($"Trade count: {Tradetimes.Count}");
                    TradeLog($"Trade Average: {Math.Round((Tradetimes.Average() / 60000), 2)}");
                    TradeLog($"Box Hit Average: {Math.Round(Boxhits.Average(), 2)}");
                    TradeLog($"Trap Hit Average: {Math.Round(Traphits.Average(), 2)}");
                    if (Math.Round(Traphits.Average(), 2) != 0)
                    {
                        TradeLog($"Boxhits per traps: {Math.Round((Math.Round(Boxhits.Average(), 2) / Math.Round(Traphits.Average(), 2)), 2)}");
                    }
                    TradeLog(" ");
                }
                int tradenumber = 0;
                foreach (Trade trade in MetaInfo.Trades)
                {
                    tradenumber += 1;
                    TradeLog($"Trade: {tradenumber}");
                    if (trade.StartRoundNumber != trade.EndRoundNumber)
                    {
                        TradeLog($"Rounds: {trade.StartRoundNumber} - {trade.EndRoundNumber}");
                    }
                    else
                    {
                        TradeLog($"Round: {trade.StartRoundNumber}");
                    }
                    TradeLog($"Trade Length: {Utilities.FormatTime(trade.EndTimestamp, trade.StartTimestamp)}");
                    TradeLog($"Boxhits: {trade.BoxHits}");
                    TradeLog($"Traphits: {trade.TrapHits}");
                    TradeLog(" ");
                }
                if (MetaInfo.CurrentTrade != null)
                {
                    TradeLog($"Current Trade:");
                    TradeLog($"{MetaInfo.CurrentTrade.StartRoundNumber}- {MetaInfo.CurrentRound.Number}");

                    decimal Time = MetaInfo.CurrentTrade.StartTimestamp / (decimal)60000;

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
                        TradeLog($"Trade Started at: {hours + ":" + minswhole.ToString("D2") + ":" + secswhole.ToString("D2") + "." + milsecs}");

                    }

                    else
                    {
                        decimal mins = Time;
                        int minswhole = (int)Math.Truncate(mins);
                        decimal secs = (mins - minswhole) * 60;
                        int secswhole = (int)Math.Truncate(secs);
                        decimal milsecs = (secs - secswhole);
                        milsecs = Math.Round(milsecs, 0);
                        TradeLog($"Trade Started at: {minswhole.ToString("D2") + ":" + secswhole.ToString("D2") + "." + milsecs}");
                    }

                    TradeLog($"Boxhits: {MetaInfo.CurrentTrade.BoxHits}");

                    TradeLog($"Traphits: {MetaInfo.CurrentTrade.TrapHits}");
                }
            }
            else
            {
                TradeLog("No trades logged");
            }



            //time to functions
            TimeTo(MetaInfo);

            //Indualvidual Round Stats
            IRS(MetaInfo);



        }
        public void TimSaved(MetaAndTXTObjects TxtInfo)
        {

            // Map Name 

            gamelabel(TxtInfo);
            //game info

            GameInfoLog(" ");

            GameInfoLog("Game Stats:");
            GameInfoLog($"solo lives used: {TxtInfo.SoloLivesGiven}");
            ///Set up Infomation 

            SetupInfo(TxtInfo);
            // special rounds stuff
            SpecialRounds(TxtInfo);
            GameInfoLog($"BoxHits: {TxtInfo.BoxHits}");
            GameInfoLog("");
            if (TxtInfo.CurrentRound.Number >= 163)
            {
                GameInfoLog("Pro developer bennie missed a bug in TIM that make missed insta kill rounds not viewable with lifespan of the TXT filetype.");
            }

            //trades


            List<long> Tradetimes = new List<long>();
            List<long> Boxhits = new List<long>();

            foreach (Trade Tradeinfo in TxtInfo.Trades)
            {
                Tradetimes.Add(Tradeinfo.Time);

                Boxhits.Add(Tradeinfo.BoxHits);

            }
            if (Tradetimes.Count != 0)
            {

                if (Tradetimes.Count() != 0 && Boxhits.Count() != 0)
                {
                    TradeLog($"Trade count: {Tradetimes.Count}");
                    TradeLog($"Trade Average: {Math.Round((Tradetimes.Average() / 60000), 2)}");
                    TradeLog($"Box Hit Average: {Math.Round(Boxhits.Average(), 2)}");
                    TradeLog(" ");
                }
                int tradenumber = 0;
                foreach (Trade trade in TxtInfo.Trades)
                {
                    tradenumber += 1;
                    TradeLog($"Trade: {tradenumber}");
                    if (trade.StartRoundNumber != trade.EndRoundNumber)
                    {
                        TradeLog($"Rounds: {trade.StartRoundNumber} - {trade.EndRoundNumber}");
                    }
                    else
                    {
                        TradeLog($"Round: {trade.StartRoundNumber}");
                    }

                    TradeLog($"Trade Length: {Utilities.FormatTime(trade.EndTimestamp, trade.StartTimestamp)}");
                    TradeLog($"Boxhits: {trade.BoxHits}");
                    TradeLog(" ");
                }
                if (TxtInfo.CurrentTrade != null)
                {
                    TradeLog($"Current Trade:");
                    TradeLog($"{TxtInfo.CurrentTrade.StartRoundNumber}- {TxtInfo.CurrentRound.Number}");

                    decimal Time = TxtInfo.CurrentTrade.StartTimestamp / (decimal)60000;

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
                        TradeLog($"Trade Started at: {hours + ":" + minswhole.ToString("D2") + ":" + secswhole.ToString("D2") + "." + milsecs}");

                    }

                    else
                    {
                        decimal mins = Time;
                        int minswhole = (int)Math.Truncate(mins);
                        decimal secs = (mins - minswhole) * 60;
                        int secswhole = (int)Math.Truncate(secs);
                        decimal milsecs = (secs - secswhole);
                        milsecs = Math.Round(milsecs, 0);
                        TradeLog($"Trade Started at: {minswhole.ToString("D2") + ":" + secswhole.ToString("D2") + "." + milsecs}");
                    }

                    TradeLog($"Boxhits: {TxtInfo.CurrentTrade.BoxHits}");
                }
            }
            else
            {
                TradeLog("No trades logged");
            }
            //time to

            TimeTo(TxtInfo);


            //Individual round info

            IRS(TxtInfo);



        }
    }
}
