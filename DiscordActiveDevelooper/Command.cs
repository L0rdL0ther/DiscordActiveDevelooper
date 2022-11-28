using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace DiscordActiveDevelooper
{
    public class Command : BaseCommandModule
    {
        [Command("komut")]
        public async Task mmw(CommandContext ctx, [RemainingText] string ss)
        {
            if (ss == null)
            {
               await ctx.Channel.SendMessageAsync("Wronge usage *komut {hello or anything}");
            }
            await ctx.Channel.SendMessageAsync(ss);

        }

    }
}
