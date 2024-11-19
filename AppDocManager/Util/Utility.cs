using AppDocManager.Models;
using AppDocManager.Models.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppAwm.Util
{
    public static class Utility
    {
        /// <summary>
        /// Servidor de dominio ClARO para o o sistema Adm Master
        /// </summary>
        // public static readonly string ServerDomain = Environment.MachineName;


        public static Usuario Usuario { get; set; }

        public static string Criptografar(string _input)
        {
            //byte[] inputBytes = Encoding.ASCII.GetBytes(_input);
            byte[] hash = MD5.Create(_input).Hash;
            var sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("X2"));

            return sb.ToString();
        }

        public static async Task<WhatsAppAnswer> SendWhatsAppAsync(Anexo anexo)
        {
            try
            {
                string strTo = Regex.Replace(Usuario.Telefone, @"[^\d]", string.Empty);
                WhatsAppCommand command = new WhatsAppCommand
                {
                    messaging_product = "whatsapp",
                    to = $"55{Regex.Replace(Usuario.Telefone, @"[^\d]", string.Empty)}",
                    type = "template",
                    template = new Template
                    {
                        name = "em_analise",
                        language = new Language 
                            { 
                                code = "pt_BR" 
                            },
                        components = new List<Component>
                        {
                            new Component 
                                { 
                                    type = "body",
                                    parameters = new List<Parameter>
                                    {
                                        new Parameter { type = "text", text = Usuario.Nome },
                                        new Parameter { type = "text", text = anexo.Nome },
                                        new Parameter { type = "text", text = anexo.Funcionario.Nome }
                                    }
                            }
                        }
                    }
                };

                //var client = new HttpClient();
                //var request = new HttpRequestMessage(HttpMethod.Post, "https://graph.facebook.com/v21.0/483361804856418/messages");
                //request.Headers.TryAddWithoutValidation("Authorization", "Bearer EAASwX8xLXIIBO79dZBYjDmPrYgefiV3fruxVdrEnTM2dAPZAzgBGEKFquM1kXs2tHHhVJyVGYGGPEvTVkR7UU73rUXZBwJZAgAidkXMxMhnwNmM7ckbPmA4KqTtMIH726OSI2ZBl9cjAnMk8m0NIWGTOZAORsFn7lxEMQPSSzEOCtukwPK3KEtap2V2QvP50Mg8OkZB8IBgTCTHBUbA022jxL6MgRrWnQZDZD");
                //var content = new StringContent("{\r\n\t\"messaging_product\": \"whatsapp\",\r\n\t\"to\": \"55" + strTo + "\",\r\n\t\"type\": \"template\",\r\n\t\"template\": {\r\n\t\t\"name\": \"em_analise\",\r\n\t\t\"language\": {\r\n\t\t\t\"code\": \"pt_BR\"\r\n\t\t},\r\n\t\t\"components\": [\r\n\t\t\t{\r\n\t\t\t\t\"type\": \"body\",\r\n\t\t\t\t\"parameters\": [\r\n\t\t\t\t\t{\r\n\t\t\t\t\t\t\"type\": \"text\",\r\n\t\t\t\t\t\t\"text\": \"" + Usuario.Nome + "\"\r\n\t\t\t\t\t},\r\n\t\t\t\t\t{\r\n\t\t\t\t\t\t\"type\": \"text\",\r\n\t\t\t\t\t\t\"text\": \"" + anexo.Nome + "\"\r\n\t\t\t\t\t},\r\n\t\t\t{\r\n\t\t\t\t\t\t\"type\": \"text\",\r\n\t\t\t\t\t\t\"text\": \"" + anexo.Funcionario.Nome + "\"\r\n\t\t\t\t\t}\r\n\t\t\t\t]\r\n\t\t\t}\r\n\t\t]\r\n\t}\r\n}", null, "application/json");
                //request.Content = content;
                //var response = await client.SendAsync(request);
                //response.EnsureSuccessStatusCode();
                //Console.WriteLine(await response.Content.ReadAsStringAsync());

                string token = ConfigurationManager.AppSettings["tokenWhats"];
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://graph.facebook.com/v21.0/483361804856418/messages");
                request.Headers.Add("Authorization", "Bearer " + token);
                string msgBocy = System.Text.Json.JsonSerializer.Serialize(command);
                var content = new StringContent(msgBocy, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string stringJason = await response.Content.ReadAsStringAsync();
                
                WhatsAppAnswer answer = JsonConvert.DeserializeObject<WhatsAppAnswer>(stringJason);

                return null;
            }
            catch (Exception ex)
            {
                return new WhatsAppAnswer() { MessagingProduct = ex.Message };
            }
        }

        public static void EnviarEmail(Rejeito rejeito)
        {
            try
            {
                NetworkCredential login;
                SmtpClient client;
                MailMessage msg;

                string statusDoc = (rejeito.Anexo.Status ==  EnumStatusDocs.Rejeitado ? "REJEITOU" : rejeito.Anexo.Status == EnumStatusDocs.Aprovado ? "APROVOU" : "APROVOU COM RESALVA");

                string mensagem = string.Empty;
                mensagem += $"<html><body><h3><center>HDdoc - Gestão de documentação técnica</center><hr/></h3><br/><p>Olá {rejeito.Usuario.Nome},<br/></p>";
                mensagem += $"<p>A gestão de documentação técnica, {statusDoc} o seguinte item:</p>";
                mensagem += $"<div style='border:1px solid black; padding:10px; border-radius:5px;'>";
                mensagem += $"<p><b>DOCUMENTO:</b> {rejeito.Anexo.Nome}</br>";
                mensagem += $"<b>TIPO:</b> {ListaTipoAnexo.Find(f => f.Key == rejeito.Anexo.TipoAnexo).Value}</br>";

                if (rejeito.Anexo.Status.HasFlag(EnumStatusDocs.Rejeitado & EnumStatusDocs.Resalva))
                    mensagem += $"<b>MOTIVO:</b> {rejeito.Anexo.Descricao}</br>";

                mensagem += $"<b>FUNCIONÁRIO:</b> {rejeito.Funcionario.Nome} - <b>Telefone:</b> {rejeito.Funcionario.Telefone}</br>";
                mensagem += $"<b>EMPRESA:</b> {rejeito.Empresa.Nome}</p>";
                mensagem += $"</div><p>Favor não responder este e-mail!</p></body></html>";

                login = new NetworkCredential("agles.developer", "hoswoqxeghfohswj");
                client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = login;
                msg = new MailMessage { From = new MailAddress("agles.developer@gmail.com", "Sistema - HDdoc", Encoding.Default), };
                msg.To.Add(new MailAddress(rejeito.Usuario.Email, rejeito.Usuario.Nome, Encoding.ASCII));
                msg.Subject = $"HDdoc - Documentação";
                msg.Body = mensagem;
                msg.BodyEncoding = Encoding.Default;
                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.Normal;
                msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                client.Send(msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<KeyValuePair<string, string>> ListaTipoAnexo
        {
            get
            {
                var item = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("0", " ****** TODOS ******"),
                    new KeyValuePair<string, string>("0004", "CONTRATO INDIVIDUAL DE TRABALHO"),
                    new KeyValuePair<string, string>("0005", "CTPS DIGITAL COMPLETA/E-SOCIAL ATUALIZADO"),
                    new KeyValuePair<string, string>("0007", "FICHA DE EPI"),
                    new KeyValuePair<string, string>("0036", "FICHA DE REGISTRO COM FOTO(FRENTE/VERSO)"),
                    new KeyValuePair<string, string>("0035", "FOTO 3X4 DIGITAL COLORIDA"),
                    new KeyValuePair<string, string>("0006", "OSS (NR01)"),
                    new KeyValuePair<string, string>("0016", "NR 10 BASICO BT"),
                    new KeyValuePair<string, string>("0026", "NR 35 TRABALHADOR"),
                    new KeyValuePair<string, string>("0034", "NR 18 CONSTRUÇÃO")
                };

                return item;
            }

        }

        public static List<KeyValuePair<int, string>> ListaStatus
        { 
            get
            {
                var item = new List<KeyValuePair<int, string>>
                {
                    new KeyValuePair<int, string>(0, " *** TODOS ***"),
                    new KeyValuePair<int, string>(1, " Enviado"),
                    new KeyValuePair<int, string>(2, " Em Analise"),
                    new KeyValuePair<int, string>(3, " Aprovado"),
                    new KeyValuePair<int, string>(4, " Rejeitado"),
                    new KeyValuePair<int, string>(5, " Aprovado com Resalva"),
                    new KeyValuePair<int, string>(6, " Expirado")
                };

                return item;
            }
        }
    }
}
