using AppAwm.DAL;
using AppAwm.Models;
using AppAwm.Models.Enum;
using AppAwm.Respostas;
using AppAwm.Services.Interface;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;

namespace AppAwm.Services
{
    public class AnexoService: IAnexo<AnexoAnswer>
    {

        public AnexoService() { }

        private GenericRepositoryValidation.GenericRepositoryExceptionStatus status;

        public AnexoAnswer Save(Anexo anexo)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Anexo>(db, out status);

                int ret = contexto.Create(anexo);

                return ret > 0 ? AnexoAnswer.DeSucesso() : AnexoAnswer.DeErro();
            }
            catch
            {
                return AnexoAnswer.DeErro();
            }
        }

        public AnexoAnswer List(Expression<Func<Anexo, bool>> predicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Anexo>(db, out status);

                List<Anexo> list = contexto.GetAll(predicate).ToList();

                return list.Count > 0 ? AnexoAnswer.DeSucesso(list) : AnexoAnswer.DeErro("Nenhum regisro encontrado");
            }
            catch
            {
                return AnexoAnswer.DeErro("Ocorreu um erro na execução da consulta");
            }
        }

        public AnexoAnswer Remove(Anexo anexo)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Anexo>(db, out status);

                int resposta =  contexto.Delete(anexo);

                return resposta > 0 ? AnexoAnswer.DeSucesso("Anexo removido com sucesso!") : AnexoAnswer.DeErro("Ocorreu algum erro ao tentar remover o item.");
            }
            catch
            {
                return AnexoAnswer.DeErro("Ocorreu um erro na execução");
            }
        }

       

        public AnexoAnswer UpdateStatus(int id, EnumStatusDocs statusDocs, string usuario, string? message = null)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Anexo>(db, out status);

                if(string.IsNullOrWhiteSpace(message))
                    message = null;

                int resposta = 0;

                if (statusDocs is EnumStatusDocs.Rejeitado or EnumStatusDocs.Resalva)
                {
                    resposta = db.Anexos.Where(w => w.Cd_Anexo == id)
                    .ExecuteUpdate(ax
                    => ax.SetProperty(sp
                    => sp.Status, statusDocs).SetProperty(sp
                    => sp.Cd_UsuarioAnalista, string.IsNullOrEmpty(usuario) ? null : usuario).SetProperty(sp
                    => sp.Dt_Validade_Documento, DateTime.Now.AddDays(2)).SetProperty(sp
                    => (statusDocs == EnumStatusDocs.Rejeitado ? sp.MotivoRejeicao : sp.MotivoResalva), message));
                }
                else
                {

                    resposta = db.Anexos.Where(w => w.Cd_Anexo == id)
                    .ExecuteUpdate(ax
                    => ax.SetProperty(sp
                    => sp.Status, statusDocs).SetProperty(sp
                    => sp.Cd_UsuarioAnalista, string.IsNullOrEmpty(usuario) ? null : usuario));
                }

                return resposta > 0 ? AnexoAnswer.DeSucesso("Status Atualizado com sucesso!") : AnexoAnswer.DeErro("Ocorreu algum erro ao tentar alterar o status do documento.");
            }
            catch
            {
                return AnexoAnswer.DeErro("Ocorreu um erro na execução");
            }
        }

        public void Notify()
        {
            static IConnection cnn()
            {
                ConnectionFactory factory = new ConnectionFactory();
                factory.UserName = "guest";
                factory.Password = "guest";
                factory.HostName = "localhost";
                factory.RequestedChannelMax = 10;

                IConnection conn = factory.CreateConnection();

                return conn;
            }

            using IConnection conexao = cnn();
            using var canal = conexao.CreateModel();
            canal.ConfirmSelect();

            IBasicProperties props = canal.CreateBasicProperties();
            props.ContentType = "text/plain";
            props.DeliveryMode = 2;

            canal.ExchangeDeclare("changeMq", ExchangeType.Fanout);

            var corpo = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(1));

            var queueName = canal.QueueDeclare(queue: "operacao", false, false, false, null).QueueName;
            canal.QueueBind(queue: queueName, exchange: "changeMq", routingKey: string.Empty);

            canal.BasicPublish(exchange: "changeMq", routingKey: "", basicProperties: props, body: corpo);

            canal.WaitForConfirmsOrDie(TimeSpan.FromSeconds(2));
        }

        public HistoricoExecucao? GetLastHistoricoExecucao()
        {
            using DbCon db = new();
            using var contexto = new RepositoryGeneric<HistoricoExecucao>(db, out status);

            return contexto.GetAll(g => g.Dt_Execucao > DateTime.Now.Date).OrderBy(o => o.Id).LastOrDefault();
        }

        public int InsereProximaExecucao(HistoricoExecucao historicoExecucao)
        {
            using DbCon db = new();
            using var contexto = new RepositoryGeneric<HistoricoExecucao>(db, out status);

            return contexto.Create(historicoExecucao);
        }
    }
}
