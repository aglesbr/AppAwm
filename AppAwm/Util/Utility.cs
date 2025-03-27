using AppAwm.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AppAwm.Util
{
    public static class Utility
    {
        /// <summary>
        /// Servidor de dominio ClARO para o o sistema Adm Master
        /// </summary>
        // public static readonly string ServerDomain = Environment.MachineName;

        public static Cliente? Cliente { get; set; }

        public static readonly string Secret = "eyJSb2xlIjoiQWRtaW4iLCJJc3N1ZX0=";
        public static readonly string KeyApi = "a99341da-d1df-41b2-9d82-3d0825572151-b95e1887-caad-4f3a-bc27-e390c4b7b7a3";
        public static readonly string UrlApi = "https://api.cnpja.com/office/{0}";

        public static string GenerateToken(Usuario userCurrency)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var key = Encoding.ASCII.GetBytes(Secret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                    [
                        new(ClaimTypes.Name, userCurrency.Nome!.ToString()),
                        new(ClaimTypes.Email, userCurrency.Email!.ToString()),
                        new(ClaimTypes.Role, userCurrency.Perfil!.ToString())
                    ]),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };


                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string Criptografar(string _input)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(_input);
            byte[] hash = MD5.HashData(inputBytes);
            StringBuilder sb = new();

            for (int i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("X2"));

            return sb.ToString();
        }

        public static void EnviarEmail(bool isNovoUsuario, Usuario usuario, string? linkGddoc = null)
        {
            try
            {
                NetworkCredential login;
                SmtpClient client;
                MailMessage msg;

                string perfil = usuario.Perfil == Models.Enum.EnumPerfil.Terceiro ? "Terceiro" : usuario.Perfil == Models.Enum.EnumPerfil.Analista ? "Analista" : "Funcionário";

                string mensagem = string.Empty;
                mensagem += $"<html><body><h3><center>HDDOC - {(isNovoUsuario ? "NOVO USUÁRIO" : " SOLICITAÇÃO DE SENHA")}</center><hr/></h3><br/><p>Olá {usuario.Nome}<br/></p>" +
                    $"<p>Foi gerada uma senha temporária de acesso!<br>Será solicitado a alteração da senha temporária para uma senha de sua preferência.</p>" +
                    $"<div style='border:1px solid black; padding:10px; border-radius: 5px;'>Perfil de acesso: <b>{perfil}</b><br/>Usuário:<b> {usuario.Documento}</b><br/>Senha: <b>$123Master</b><br/>";
                if (!string.IsNullOrWhiteSpace(linkGddoc))
                    mensagem += $"<br><b>Click no link para acessar o sistema HDDOC</b>: <a href='{linkGddoc}'>{linkGddoc}</a>";

                mensagem += $"</div><p>Resposta automático!<br/>Favor não responder este e-mail!</p></body></html>";

                //login = new NetworkCredential("agles.developer", "hoswoqxeghfohswj");
                login = new NetworkCredential("842bbe001@smtp-brevo.com", "S5tF0jdYDT8Kq4Lm");

                client = new SmtpClient("smtp-relay.brevo.com", 587);
                client.EnableSsl = true;

                client.Credentials = login;
                msg = new MailMessage { From = new MailAddress("conferencia@hddoc.com.br", "Sistema - HDDOC", Encoding.Default), };
                msg.To.Add(new MailAddress(usuario.Email!, usuario.Nome, Encoding.ASCII));
                msg.Subject = $"HDDOC - {(isNovoUsuario ? "Bem Vindo(a)" : "Nova Senha")}";
                msg.Body = mensagem;
                msg.BodyEncoding = Encoding.Default;
                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.Normal;
                msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;


                //client.SendMailAsync(msg);
                client.Send(msg);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void EnviarEmail(string message, Usuario usuario, string? assunto = null)
        {
            try
            {
                NetworkCredential login;
                SmtpClient client;
                MailMessage msg;

                //login = new NetworkCredential("agles.developer", "hoswoqxeghfohswj");
                //client = new SmtpClient("smtp.gmail.com", 587);

                login = new NetworkCredential("842bbe001@smtp-brevo.com", "S5tF0jdYDT8Kq4Lm");
                client = new SmtpClient("smtp-relay.brevo.com", 587);
                client.EnableSsl = true;
                client.Credentials = login;
                msg = new MailMessage { From = new MailAddress("conferencia@hddoc.com.br", "Sistema - HDDOC", Encoding.Default), };
                msg.To.Add(new MailAddress(usuario.Email!, usuario.Nome, Encoding.ASCII));
                msg.Subject = assunto ?? $"HDDOC - Vencimento de Documento";
                msg.Body = message;
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


        //public static async ValueTask<Endereco?> GetCepAsync(string cep)
        //{
        //    var client = new HttpClient();
        //    var request = new HttpRequestMessage(HttpMethod.Get, $"https://viacep.com.br/ws/{cep}/json/");
        //    var response = await client.SendAsync(request);

        //    if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
        //    {
        //        cep = response.Content.ReadAsStringAsync().Result;

        //        Endereco? endereco = JsonConvert.DeserializeObject<Endereco>(cep);

        //        return endereco ?? new();
        //    }

        //    return null;
        //}


        public static List<DocumentacaoComplementar> DocumentacaoComplementares { get; set; } = [];

        public static List<DocumentacaoComplementar> DocumentacaoComplementarWorker { get; set; } = [];

        public static List<KeyValuePair<int, string>> TipoStatus
        {
            get => [
                new(1, "Enviado para analise, orange-text"),
                new(2, "Em analise, blue-text"),
                new(3, "Aprovado, green-text" ),
                new(4, "Reprovado, red-text"),
                new(5, "Aprovado com resalva, green-text"),
                new(6, "Expirado, black-text"),
                ];
        }

        public static List<KeyValuePair<string, string>> Conexao
        {
            get => [
                new("MySql", "WAConnectionMySql"),
                new("MsSql", "WAConnectionMsSql"),
                ];
        }
    }
}
