using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using AdaptiveCards;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Bot3.Dialogs
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

            Activity replyToConversation = activity.CreateReply("Should go to conversation");
            replyToConversation.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            replyToConversation.Attachments = new List<Attachment>();

            AdaptiveCard cad = new AdaptiveCard()
            {
                Body = new List<CardElement>()
                {
                    new Container()
                    {
                       Speak ="<s>Hola!</s><s>Esto es una tarjeta adaptable</s>",
                        Items = new List<CardElement>()
                        {
                            new ColumnSet()
                            {

                                Columns = new List<Column>()
                                {
                                    new Column ()
                                    {
                                        Size = ColumnSize.Auto,
                                        Items = new List<CardElement>()
                                        {
                                            new Image()
                                            {
                                                Url ="https://st.depositphotos.com/2777531/4760/v/450/depositphotos_47607033-stock-illustration-geek-nerd-boy.jpg",
                                                Size = ImageSize.Medium,
                                                Style = ImageStyle.Person
                                            }
                                        }
                                    },

                                    new Column()
                                    {
                                        Size = ColumnSize.Stretch,
                                        Items = new List<CardElement>()
                                        {
                                            new TextBlock()
                                            {
                                              Text="Hola",
                                              Weight =TextWeight.Bolder,
                                              IsSubtle=true
                                            },
                                             new TextBlock()
                                            {
                                              Text="Esto es una tarjeta adaptable",
                                              Wrap=true
                                            }
                                        }

                                    },







                                }

                            }
                        }
                    }
                },


                //boton

                Actions = new List<ActionBase>()
              {
                      new ShowCardAction()
                      {
                       Title ="Formulario",
                       Speak ="<s>formulario</s>"
                    //Card = GetFormulario()
                      },
                }

            };
            

            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = cad

            };

            var reply = context.MakeMessage();
            reply.Attachments.Add(attachment);

            await context.PostAsync(reply, CancellationToken.None);




            context.Wait(MessageReceivedAsync);

        }

        private static AdaptiveCard Getformulario()
        {

            return new AdaptiveCard()
            {
                Body = new List<CardElement>()
                {
                    new TextBlock()
                    {
                        Text="formulario tarjeta adaptable",
                        Speak="<s>demo ingresar datos</<>",
                        Weight=TextWeight.Bolder,
                        Size=TextSize.Large
                    },
                    new TextBlock(){Text="ingresa tu nombre"},
                    new TextInput()
                    {
                        Id="Nombre",
                        Speak="<s>Ingresa tu nombre completo</s>",
                        Placeholder="Nombre,Apellidos",
                        Style=TextInputStyle.Text
                    },
                    new TextBlock(){Text="fecha de nacimiento"},
                    new DateInput()
                    {
                        Id="fecha nacimiento",
                        Speak="<s>Cuando naciste</s>"
                    },
                    new TextBlock(){Text="Ingresa tu numero de telefono" },
                    new NumberInput()
                    {
                        Id="telefono",
                        Speak="<s>Numero de telefono</s>"
                    }


                },

                Actions = new List<ActionBase>()
                {
                    new SubmitAction()
                    {
                        Title="Guardar",
                        Speak="<s>guardar</s>"
                    }
                }
            };
        }







    }
}




//// calculate something for us to return
//int length = (activity.Text ?? string.Empty).Length;

//// return our reply to the user
//await context.PostAsync($"You sent {activity.Text} which was {length} characters");