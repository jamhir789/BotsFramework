using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Bot2.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            var reply = activity.CreateReply();

            reply.Attachments = new List<Attachment>();

            reply.Attachments.Add(new Attachment
            {
                Name = "Contro prueba",
                Content = "probando mi bot",
                ContentUrl = "https://i0.wp.com/codigoespagueti.com/wp-content/uploads/2017/06/deadpool.jpg?resize=1080%2C600&quality=100&ssl=1",
                ContentType = "image/jpg"

            });

            await context.PostAsync(reply);
            await context.PostAsync($"Haci que has vuelto Erick ");


            context.Wait(MessageReceivedAsync);
        }
    }
}




//// calculate something for us to return
//int length = (activity.Text ?? string.Empty).Length;

//// return our reply to the user
//await context.PostAsync($"You sent {activity.Text} which was {length} characters");