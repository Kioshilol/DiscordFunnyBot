using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace DiscordFunnyBot.Commands
{
    public class FunCommands : BaseCommandModule
    {
        [Command("hello"), Description("say hello")]
        public async Task Ping(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Yo guys");
        }
    }
}
