using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BotSettings
{
    public partial class BotSettings : Form
    {
        public static DataStorage<List<Settings>> SettingsDataStorage { get; private set; }
        public static List<Settings> settingData;
        public string token;
        public int Timer;
        public int firsTimer;
        public int currentTimer;
        public bool programIsWorking;
        public BotSettings()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            File.Open("Save.json", FileMode.OpenOrCreate).Close();
            File.WriteAllText("Save.json", "");
            settingData = SettingsDataStorage.Read();
            if (settingData == null)
                settingData = new List<Settings>();

            #region DataSave

            var data = new Settings()
            {
                Token = token,
                Time = Timer
            };

            settingData.Add(data);
            SettingsDataStorage.Save(settingData);

            #endregion

            foreach (var VARIABLE in settingData)
            {
                currentTimer = VARIABLE.Time;
                firsTimer = VARIABLE.Time;
            }
            programIsWorking = true;
            Start.Enabled = false;
            button1.Enabled = true;
            timer1.Start();
            Process.Start(Directory.GetCurrentDirectory()+@"\"+ "DiscordActiveDevelooper.exe");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var process in Process.GetProcessesByName("DiscordActiveDevelooper"))
                {
                    process.Kill();
                }
            }
            catch (Exception exception)
            {
            }
            timer1.Stop();
            programIsWorking = false;
            button1.Enabled = false;
            Start.Enabled = true;
        }
        private void BotSettings_Load(object sender, EventArgs e)
        {
            #region ProcessKill

            try
            {
                foreach (var process in Process.GetProcessesByName("DiscordActiveDevelooper.exe"))
                {
                    process.Kill();
                }
            }
            catch (Exception exception)
            {
            }

            #endregion

            #region CreateFile

            if (!File.Exists("Save.json"))
            {
                File.Open("Save.json", FileMode.OpenOrCreate).Close();
            }

            #endregion

            #region FileCrateRead

            File.Open("Save.json", FileMode.OpenOrCreate).Close();
            SettingsDataStorage = new DataStorage<List<Settings>>(Directory.GetCurrentDirectory(), "Save.json");
            settingData = SettingsDataStorage.Read();
            if (settingData == null)
                settingData = new List<Settings>();

            #endregion

            timer1.Interval = 1000;


            foreach (var dat in settingData)
            {
                try
                {
                    textBox1.Text = dat.Token;
                    textBox2.Text = dat.Time.ToString();
                }
                catch (Exception exception)
                {

                }
            }

        }

        public class Settings
        {
            public string Token { get; set; }
            public int Time { get; set; }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            currentTimer--;

            NextRestart.Text = "NextRestart " + currentTimer;

            if (currentTimer <= 10)
            {
                #region ProcessKill

                try
                {
                    foreach (var process in Process.GetProcessesByName("DiscordActiveDevelooper"))
                    {
                        process.Kill();
                    }
                }
                catch (Exception exception)
                {
                }

                #endregion
            }

            if (currentTimer <= 0)
            {
                Process.Start("DiscordActiveDevelooper.exe");
                currentTimer = firsTimer;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            token = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Timer = Convert.ToInt32(textBox2.Text);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.gg/42HJZjTdjB");
        }
    }
}
