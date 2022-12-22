using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Threading.Tasks;
using Discord.Net;
using Newtonsoft.Json;

namespace discordbot
{
    internal class Program
    {
        private DiscordSocketClient _client;

        static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            _client.Log += Log;

            await _client.LoginAsync(TokenType.Bot, "ENTER YOUR DISCORD BOT TOKEN HERE");
            await _client.StartAsync();

            await _client.SetStatusAsync(UserStatus.DoNotDisturb);      // CAN BE SET TO ONLINE, IDLE, DND 
            await _client.SetGameAsync("ENTER STATUS MESSAGE HERE");

            _client.Ready += Client_Ready;
            _client.SlashCommandExecuted += SlashCommandHandler;


            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task Client_Ready()
        {
            // guildId = Rightclick on server icon and click "Copy ID"
            var guild = _client.GetGuild(guildId);   // replace guildId with your servers guildId
            var guildCommand = new SlashCommandBuilder();
            guildCommand.WithName("ping");

            guildCommand.WithDescription("Simple Ping Command");

            try
            {
                await guild.CreateApplicationCommandAsync(guildCommand.Build());
            }
            catch (ApplicationCommandException exception)
            {
                var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);
                Console.WriteLine(json);
            }
        }

        private async Task SlashCommandHandler(SocketSlashCommand command)
        {
            await command.RespondAsync($"You executed {command.Data.Name}");
        }
    }
}