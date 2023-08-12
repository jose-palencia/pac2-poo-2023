using MailKit.Net.Smtp;
using MimeKit;
using Portafolio.Models;

namespace Portafolio.Servicios
{
    public class ServicioMailTrap: IServicioEmail
    {
        public async Task Enviar(ContactoViewModel modelo) 
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(modelo.Nombre, modelo.Email));
            email.To.Add(new MailboxAddress("Jose Palencia", "jilopez@unah.edu.hn"));

            email.Subject = $"Contacto de: {modelo.Nombre}";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            { 
                Text = $"<p>{ modelo.Mensaje }</p>"
            };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("sandbox.smtp.mailtrap.io", 587, false);
                smtp.Authenticate("2216848d262d49", "2352d3ad83443d");

                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
        }
    }
}
