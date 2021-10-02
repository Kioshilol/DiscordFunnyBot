using DiscordFunnyBot.Commands;
using DiscordFunnyBot.Models;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFunnyBot
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }


        public async Task RunAsync()
        {
            var json = string.Empty;

            using(var fs = File.OpenRead(@"C:\Users\p.khmylko\source\repos\DiscordFunnyBot\DiscordFunnyBot\DiscordFunnyBot\config.json"))
            {
                using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                {
                    json = await sr.ReadToEndAsync();
                }
            }

            var configModel = JsonConvert.DeserializeObject<ConfigModel>(json);
            Client = new DiscordClient(GetInitialConfiguration(configModel));
            Client.Ready += OnClientReady;
            Client.UseInteractivity(GetInteractivityConfiguration(configModel));
            Commands = Client.UseCommandsNext(GetCommandsConfiguration(configModel));
            Commands.RegisterCommands<FunCommands>();
            await Client.ConnectAsync();

            await Task.Delay(-1);
        }

        private CommandsNextConfiguration GetCommandsConfiguration(ConfigModel configModel)
        {
            var commandConfig = new CommandsNextConfiguration();

            commandConfig.StringPrefixes = new string[]
            {
                configModel.Prefix
            };

            commandConfig.EnableMentionPrefix = true;
            commandConfig.EnableDms = false;
            commandConfig.DmHelp = true;
            return commandConfig;
        }

        private DiscordConfiguration GetInitialConfiguration(ConfigModel configModel)
        {
            var config = new DiscordConfiguration()
            {
                Token = configModel.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug
            };

            return config;
        }

        private InteractivityConfiguration GetInteractivityConfiguration(ConfigModel configModel)
        {
            var config = new InteractivityConfiguration()
            {
            };

            return config;
        }

        private Task OnClientReady(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}
