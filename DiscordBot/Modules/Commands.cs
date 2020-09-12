using Discord.Commands;
using System;
using System.Threading.Tasks;
using Discord;
using DiscordBot.Services;
using System.Diagnostics;

namespace DiscordBot.Modules
{
    // for commands to be available, and have the Context passed to them, we must inherit ModuleBase
    public class Commands : ModuleBase
    {
        private readonly AudioService service;

        // Remember to add an instance of the AudioService
        // to your IServiceCollection when you initialize your bot
        public Commands(AudioService _service)
        {
            service = _service;
        }

        [Command("hello")]
        public async Task HelloCommand()
        {
            var user = Context.User;
            await ReplyAsync($"Hello {user.Mention}!");
        }

        #region Math

        [Command("add")]
        public async Task Add(int num1, int num2)
        {
            // We can also access the channel from the Command Context.
            await Context.Channel.SendMessageAsync($"{num1}+{num2} = {num1 + num2}");
        }

        [Command("minus")]
        public async Task Minus(int num1, int num2)
        {
            // We can also access the channel from the Command Context.
            await Context.Channel.SendMessageAsync($"{num1}-{num2} = {num1 - num2}");
        }

        [Command("mul")]
        public async Task Multiply(int num1, int num2)
        {
            // We can also access the channel from the Command Context.
            await Context.Channel.SendMessageAsync($"{num1}*{num2} = {num1 * num2}");
        }

        [Command("divide")]
        public async Task Divide(int num1, int num2)
        {
            // We can also access the channel from the Command Context.
            await Context.Channel.SendMessageAsync($"{num1}/{num2} = {num1 / num2}");
        }

        #endregion Math

        #region Roll

        [Command("roll")]
        public async Task Roll(string str1)
        {
            var random = new Random();
            var user = Context.User;

            await ReplyAsync($"There is {random.Next(0, 100)}% that {user.Mention} is {str1}");
        }

        [Command("roll")]
        public async Task Roll(string str1, string str2)
        {
            var random = new Random();
            var user = Context.User;

            await ReplyAsync($"There is {random.Next(0, 100)}% that {user.Mention} {str1} {str2}");
        }

        [Command("roll")]
        public async Task Roll(string str1, string str2, string str3)
        {
            var random = new Random();
            var user = Context.User;

            await ReplyAsync($"There is {random.Next(0, 100)}% that {user.Mention} {str1} {str2} {str3}");
        }

        [Command("roll")]
        public async Task Roll(string str1, string str2, string str3, string str4)
        {
            var random = new Random();
            var user = Context.User;

            await ReplyAsync($"There is {random.Next(0, 100)}% that {user.Mention} {str1} {str2} {str3} {str4}");
        }

        #endregion Roll

        #region Audio Commands

        [Command("join", RunMode = RunMode.Async)]
        public async Task Join()
        {
            // Check if user is in a channel
            if ((Context.User as IVoiceState).VoiceChannel != null)
            {
                await service.JoinAudio(Context.Guild, (Context.User as IVoiceState).VoiceChannel);
            }
            else
            {
                await ReplyAsync($"User not in a voice channel");
            }
        }

        // Remember to add preconditions to the commands
        [Command("leave", RunMode = RunMode.Async)]
        public async Task Leave()
        {
            await service.LeaveAudio(Context.Guild);
        }

        [Command("stop", RunMode = RunMode.Async)]
        public async Task Stop()
        {
            await Leave();
            await Join();
            await ReplyAsync($"Stopped playing.");
        }

        [Command("play", RunMode = RunMode.Async)]
        public async Task Play([Remainder] string song)
        {
            test();
            await service.SendAudioAsync(Context.Guild, Context.Channel, song);
        }

        #endregion Audio Commands

        private Process test()
        {
            return Process.Start(new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                Arguments = $"/C youtube-dl.exe -e",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = false
            }
            );
        }
    }
}