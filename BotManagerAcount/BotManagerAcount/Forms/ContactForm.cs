using Microsoft.Bot.Builder.Dialogs;//para retornar un dialogo hacia el messages se ocupa esta libreria
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotManagerAcount.Forms
{
    [Serializable]
    public class ContactForm
    {
        [Prompt("Cual es tu nombre?")]
        public string Nombre;

        [Prompt("cual es tu apellido?")]
        public string Apellido;

        [Prompt("Cual es tu edad?")]
        public string Edad;

        [Prompt("Cual eres hombre o mujer?")]
        public string Genero;

        public static IForm<ContactForm> CrearFormulario()
        {
            return new FormBuilder<ContactForm>()
                .Message("Hola, vamos a comenzar a conocer tus datos")
                .OnCompletion(async (context, ContactForm) =>
                {
                    await context.PostAsync("tu perfil esta completo.");
                })
            .Build();



        }

    }

 

    [Serializable]
    public enum Genero
    {
        Hombre = 0,
        Mujer = 1
    };

}