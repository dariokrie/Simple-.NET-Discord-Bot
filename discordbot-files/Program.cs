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

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}