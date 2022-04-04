using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity.Extensions;

namespace FormulaBot.Commands
{
    public class TriviaCommands : BaseCommandModule
    {
        private readonly Dictionary<string, string> _questions = new()
        {
            {"Who was the world champion in 2007?", "Kimi Raikkonen"},
            {"Who has the current record for most pole positions?", "Lewis Hamilton"},
            {"Which constructor won the constructors championship in 1997?", "Williams"},
            {"Who was the first World Drivers' Champion?", "Giuseppe Farina"},
            {"Which circuit did Pierre Gasly have his first win?", "Monza"},
            {"Who won the 2005 French Grand Prix?", "Fernando Alonso"},
            {"Which engine supplier was Mclaren with in 2012?", "Mercedes"}
        };

        [Command("trivia")]
        public async Task GetTriviaQuestion(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();
            var rand = new Random();

            var question = _questions.ElementAt(rand.Next(-1, _questions.Count)).Key;
            var answer = _questions[question];

            await ctx.Channel.SendMessageAsync(question).ConfigureAwait(false);

            var userAnswer = await interactivity.WaitForMessageAsync(x => x.Author.Username == ctx.User.Username).ConfigureAwait(false);

            if (string.Equals(userAnswer.Result.Content, answer, StringComparison.CurrentCultureIgnoreCase))
            {
                await ctx.Channel.SendMessageAsync("Yay, well done! You are Correct!!").ConfigureAwait(false);
            }
            else if (userAnswer.Result.Content.Substring(0, 1) == "!")
            {
                await ctx.Channel.SendMessageAsync("This is a command! This answer is wrong!");
            }
            else
            {
                await ctx.Channel.SendMessageAsync("Oh No! You are wrong!!").ConfigureAwait(false);
            }
        }
    }
}
