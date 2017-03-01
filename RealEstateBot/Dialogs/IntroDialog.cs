namespace ArchimedesBot.Dialogs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.Internals.Fibers;
    using Microsoft.Bot.Connector;
    using Search.Models;
    using Search.Services;

    [Serializable]
    public class IntroDialog : IDialog<object>
    {
        private ISearchClient searchClient;

        public IntroDialog(ISearchClient searchClient)
        {
            SetField.NotNull(out this.searchClient, nameof(searchClient), searchClient);
        }

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(this.StartSearchDialog);
            return Task.CompletedTask;
        }

        public Task StartSearchDialog(IDialogContext context, IAwaitable<IMessageActivity> input)
        {
            context.Call(new ArchimedesSearchDialog(this.searchClient), this.Done);
            return Task.CompletedTask;
        }

        public async Task Done(IDialogContext context, IAwaitable<IList<SearchHit>> input)
        {
            var selection = await input;

            if (selection != null && selection.Any())
            {
                string list = string.Join("\n\n", selection.Select(s => $"* {s.Output} ({s.Key})"));
                await context.PostAsync($"Eureka! For future reference, you asked these questions:\n\n{list}");
            }

            context.Done<object>(null);
        }
    }
}
