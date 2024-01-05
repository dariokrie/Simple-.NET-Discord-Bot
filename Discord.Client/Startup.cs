using Discord.Client.Interfaces;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace Discord.Client
{
    public class Startup : IStartup
    {
        private DiscordSocketClient _client;

        public async Task StartAsync()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;

            await _client.LoginAsync(TokenType.Bot, "<discord bot token>");
            await _client.StartAsync();

            await _client.SetStatusAsync(UserStatus.DoNotDisturb);
            await _client.SetGameAsync("<optional: status message>");

            _client.Ready += Client_Ready;
            _client.SlashCommandExecuted += SlashCommandHandler;

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task Client_Ready()
        {
            var guild = _client.GetGuild(/*add guildId as parameter here*/);
            var guildCommand = new SlashCommandBuilder();
            guildCommand.WithName("ping");
            guildCommand.WithDescription("Simple Ping Command");

            try
            {
                await guild.CreateApplicationCommandAsync(guildCommand.Build());
            }

            catch (HttpException exception)
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
