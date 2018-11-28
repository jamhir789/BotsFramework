using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotManagerAcount.Controllers;
using BotManagerAcount.Forms;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;


namespace BotManagerAcount.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }
        private static IDialog<ContactForm> CrearFormulario()
        {
            return Chain.From(() => FormDialog.FromForm(ContactForm.CrearFormulario));
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            List<Contacto> listaContactos = new List<Contacto>();
            listaContactos.Add(new Contacto { Identificador = "default-user", Nombre = "erick", Apellido = "medina", Telefono = "8476", Correo = "erick.medina@itspanuco.edu.mx" });
            listaContactos.Add(new Contacto { Identificador = "123", Nombre = "angela", Apellido = "paniño", Telefono = "986", Correo = "angela.patiño@itspanuco.edu.mx" });


            var activity = await result as Activity;

            // calculate something for us to return
           // int length = (activity.Text ?? string.Empty).Length;

            string mensajeRecibido = activity.Text.ToLower();

            string id = activity.From.Id;

            Contacto contactoElegido = (from e in listaContactos where e.Identificador == id select e).Single<Contacto>();

            await context.PostAsync(String.Format("hola {0}",contactoElegido.Nombre ));

            //switch (mensajeRecibido)
            //{
            //    case "hola":
            //        await context.PostAsync($".");
            //        break;

            //    case "crear perfil":
            //        context.Forward(new ContactForm, null, null);
            //        //await Conversation.SendAsync(activity, CrearFormulario);
            //        break;
            //    default:
            //        await context.PostAsync($"lo siento no tengo respuesta para eso");
            //        break;

            //}
            // return our reply to the user
            //await context.PostAsync($"Enviaste  {activity.Text} que tiene {length} caracteres");


            context.Wait(MessageReceivedAsync);
        }
    }
}