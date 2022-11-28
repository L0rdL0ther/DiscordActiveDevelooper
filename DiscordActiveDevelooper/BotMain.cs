using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;

namespace DiscordActiveDevelooper
{
    public class BotMain
    {
        public static DataStorage<List<Settings>> SettingsDataStorage { get; private set; }
        public static List<Settings> settingData;
        static void Main(string[] args)
        {
            try
            {
                trippleasyncTask().GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadLine();
            }
        }
        public static string Token;
        static async Task trippleasyncTask()
        {

            #region GetToken

           

            File.Open("Save.json", FileMode.OpenOrCreate).Close();
            SettingsDataStorage = new DataStorage<List<Settings>>(Directory.GetCurrentDirectory(), "Save.json");
            settingData = SettingsDataStorage.Read();
            if (settingData == null)
                settingData = new List<Settings>();

            foreach (var VARIABLE in settingData)
            {
                Token = VARIABLE.Token;
            }

            #endregion

            #region BotConfig

            try
            {
                var discord = new DiscordClient(new DiscordConfiguration()
                {
                    Token =Token,
                    TokenType = TokenType.Bot,
                    Intents = DiscordIntents.AllUnprivileged,


                });
                var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
                {
                    StringPrefixes = new[] { "*" }
                });
                commands.RegisterCommands<Command>();
                await discord.ConnectAsync();
                Console.WriteLine("Made By LordLother");
                Console.WriteLine("<Lord></Lother>#3735 My Discord");
                Console.WriteLine("My Discord Server : https://discord.gg/42HJZjTdjB");
                await Task.Delay(-1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            #endregion


           

        }
    }
}
