using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace Tim_reader_v2
{
    /// <summary>
    /// Interaction logic for Dwindow.xaml
    /// </summary>
    public partial class Dwindow : Window
    {
        //function parents
        //config logic
        public Config GetConfig()
        {
            Config cfg;
            if (!File.Exists("cfg.txt"))
            {
                cfg = new Config();
                File.WriteAllText("cfg.txt", JsonConvert.SerializeObject(new Config()));
            }
            else
            {
                cfg = JsonConvert.DeserializeObject<Config>(File.ReadAllText("cfg.txt"));
            }
            return cfg;
        }
        public void FileDirectoryLogic(Config cfg)
        {
            while (string.IsNullOrEmpty(cfg.Gamefolder) || !Directory.Exists(cfg.Gamefolder))
            {
                FilePromptor(cfg);
            }
            File.WriteAllText("cfg.txt", JsonConvert.SerializeObject(cfg));
        }

        public void FilePromptor(Config cfg)
        {
            using (var fbd = new FolderBrowserDialog())
            {

                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    cfg.Gamefolder = fbd.SelectedPath;
                }

            }
        }
        public void lstDialog(string Gamefolder)
        {
            string[] files = Directory.GetFiles(Gamefolder);
            foreach (string file in files)
            {
                if (Path.GetExtension(file) == ".timmeta")
                {
                    try
                    {
                        MetaAndTXTObjects MetaInfo = JsonConvert.DeserializeObject<MetaAndTXTObjects>(File.ReadAllText(file));
                        lstGames.Items.Add(MetaInfo);

                    }
                    catch
                    {
                        lstGames.Items.Add("Invaild File, tampered or corrupted");
                        continue;
                    }

                }
                else if (Path.GetExtension(file) == ".txt")
                {
                    try
                    {
                        MetaAndTXTObjects TxtInfo = JsonConvert.DeserializeObject<MetaAndTXTObjects>(File.ReadAllText(file));
                        lstGames.Items.Add(TxtInfo);

                    }
                    catch
                    {
                        lstGames.Items.Add("Invaild File, tampered or corrupted");
                        continue;
                    }


                }
            }
        }
        

        public Dwindow()
        {
            InitializeComponent();
        }
        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Config cfg = GetConfig();
            FileDirectoryLogic(cfg);
            lstDialog(cfg.Gamefolder);
        }
        public void FileDirectory_Click(object sender, RoutedEventArgs e)
        {
            Config cfg = GetConfig();
            lstGames.Items.Clear();
            cfg.Gamefolder = null;
            FileDirectoryLogic(cfg);
            lstDialog(cfg.Gamefolder);
        }
        private void ReadFile_Click(object sender, RoutedEventArgs e)
        {
            if (lstGames.SelectedItems.Count == 1)
            {
                if (lstGames.SelectedItem == null)
                {
                    return;
                }
                GSwindow window = new GSwindow();

                if (lstGames.SelectedItem is MetaAndTXTObjects MetaInfo)
                {
                    if (MetaInfo.MetadataVersion != null)
                    {
                        window.TimMeta((MetaAndTXTObjects)lstGames.SelectedItem);
                    }
                    else
                    {
                        window.TimSaved((MetaAndTXTObjects)lstGames.SelectedItem);
                    }
                }
                window.ShowDialog();
            }
            else if (lstGames.SelectedItems.Count > 1)
            {
                System.Windows.Forms.MessageBox.Show("More than 1 game selected");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Please select an item");
            }

        }
        public void GameComparison_Click(object sender, RoutedEventArgs e)
         {
            GameComparison GCwindow = new GameComparison();
           
            if (lstGames.SelectedItems.Count > 2)
            {
                System.Windows.Forms.MessageBox.Show("More than 2 games selected");
            }
            else if (lstGames.SelectedItems.Count == 1 )
            {
                System.Windows.Forms.MessageBox.Show("Only one game is selected");
            }
            else
            {
                    GCwindow.TimComparison((MetaAndTXTObjects)lstGames.SelectedItems[0], (MetaAndTXTObjects)lstGames.SelectedItems[1]);

                //{
                //    Utilities.MessageBox("Invaild File/Files Selected");
                //}
            }
            
        }
        
       

        
    }
}
