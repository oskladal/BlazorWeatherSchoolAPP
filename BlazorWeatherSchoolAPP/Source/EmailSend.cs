using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Radzen;


namespace BlazorWeatherSchoolAPP.Source
{

    // struktura ukládaných dat z formuláře
    public class DataTable
    {
        public string Email { get; set; }
        public string TextField { get; set; }
    }
    public class EmailSend
    {
        public string Message { get; set; }
        private IConfiguration config;
        
        public EmailSend(IConfiguration Config)
        {
            config = Config;
           
        }

        public void SendEmail(DataTable form)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    // Nastavení adresy odesílatele
                    mail.From = new MailAddress(config["Email"]);

                    // Nastavení adresy příjemce
                    mail.To.Add("kopeckyd@vscht.cz");

                    // Nastavení předmětu zprávy
                    mail.Subject = "Dotaz WeatherAplikace VŠCHT";

                    // Nastavení obsahu zprávy
                    mail.Body = form.TextField + " Z emailu: " + form.Email;

                    // Odeslání zprávy
                    using (SmtpClient smtpClient = new SmtpClient(config["SMTP"], 25)) //587
                    {
                        //smtpClient.Credentials = new System.Net.NetworkCredential(config["Email"], config["EmailKey"]);
                        smtpClient.EnableSsl = true;
                        smtpClient.Send(mail);
                        Message = "Email byl odeslán";

                    }
                }
            }
            catch (Exception ex)
            {
                Message = "Email se nepodařilo odeslat";
                Console.WriteLine(ex);

            }
        }
    }
}

