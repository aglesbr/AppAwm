using AppAwm.DAL;
using AppAwm.DAL.Repository;
using AppAwm.Models;
using AppAwm.Models.Enum;
using AppAwm.Respostas;
using AppAwm.Services.Interface;
using AppAwm.Util;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;

namespace AppAwm.Services
{
    public class AnexoService : IAnexo<AnexoAnswer>
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
            catch (Exception ex)
            {
                return AnexoAnswer.DeErro(ex.Message);
            }
        }

        public AnexoAnswer List(Expression<Func<Anexo, bool>> predicate, bool hasDocumnt = false)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryAnexo(db, out status);

                List<Anexo> list = [.. contexto.GetAll(predicate, hasDocumnt)];

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

                int resposta = contexto.Delete(anexo);

                return resposta > 0 ? AnexoAnswer.DeSucesso("Anexo removido com sucesso!") : AnexoAnswer.DeErro("Ocorreu algum erro ao tentar remover o item.");
            }
            catch
            {
                return AnexoAnswer.DeErro("Ocorreu um erro na execução");
            }
        }



        public AnexoAnswer UpdateStatus(int id, EnumStatusDocs statusDocs, string usuario, string? message = null, bool isRevoga = false)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Anexo>(db, out status);

                if (string.IsNullOrWhiteSpace(message))
                    message = null;

                int resposta = 0;

                if (isRevoga)
                {
                    resposta = db.Anexos.Where(w => w.Cd_Anexo == id)
                    .ExecuteUpdate(ax
                    => ax.SetProperty(sp
                    => sp.Status, statusDocs).SetProperty(sp
                    => sp.Cd_UsuarioAnalista, string.IsNullOrEmpty(usuario) ? null : usuario).SetProperty(sp
                    => sp.Dt_Validade_Documento, DateTime.Now.AddDays(2)).SetProperty(sp
                    => sp.MotivoRejeicao, message).SetProperty(sp
                    => sp.MotivoResalva, message));

                    return resposta > 0 ? AnexoAnswer.DeSucesso("Status Atualizado com sucesso!") : AnexoAnswer.DeErro("Ocorreu algum erro ao tentar alterar o status do documento.");
                }

                if (statusDocs is EnumStatusDocs.Rejeitado or EnumStatusDocs.Resalva)
                {
                    resposta = db.Anexos.Where(w => w.Cd_Anexo == id)
                    .ExecuteUpdate(ax
                    => ax.SetProperty(sp
                    => sp.Status, statusDocs).SetProperty(sp
                    => sp.Cd_UsuarioAnalista, string.IsNullOrEmpty(usuario) ? null : usuario).SetProperty(sp
                    => sp.Dt_Validade_Documento, DateTime.Now.AddDays(2)).SetProperty(sp
                    => (statusDocs == EnumStatusDocs.Rejeitado ? sp.MotivoRejeicao : sp.MotivoResalva), isRevoga ? null : message));
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
            try
            {
                static IConnection cnn()
                {
                    ConnectionFactory factory = new()
                    {
                        UserName = "guest",
                        Password = "guest",
                        HostName = "localhost",
                        Port = 5672,
                        RequestedChannelMax = 20
                    };

                    IConnection conn = factory.CreateConnection();

                    return conn;
                }

                using IConnection conexao = cnn();
                using var canal = conexao.CreateModel();
                canal.ConfirmSelect();

                IBasicProperties props = canal.CreateBasicProperties();
                props.ContentType = "text/plain";
                props.DeliveryMode = 2;

                canal.ExchangeDeclare("ChangeHDdoc", ExchangeType.Direct);

                var corpo = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(1));

                var queueName = canal.QueueDeclare(queue: Utility.Cliente!.CanalMq, false, false, false, null).QueueName;
                canal.QueueBind(queue: queueName, exchange: "ChangeHDdoc", routingKey: "docs");

                canal.BasicPublish(exchange: "ChangeHDdoc", routingKey: "docs", basicProperties: props, body: corpo);

                canal.WaitForConfirmsOrDie(TimeSpan.FromSeconds(2));

                conexao.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
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

        public List<DocumentacaoComplementar> DocumentacaoComplementar(int cd_Cargo, int? cd_empresa)
        {
            using DbCon db = new();
            using var contexto = new RepositoryGeneric<DocumentacaoComplementar>(db, out status);

            var filter = db.DocumentacaoCargos.Where(s => s.Cd_Cargo_Id == cd_Cargo).ToList();
            filter.RemoveAll(r => r.Cd_Empresa_Id != null && r.Cd_Empresa_Id != cd_empresa);

            List<int> list = filter.Select(ss => ss.Cd_Documento_Id).ToList();

            if (list.Count == 0)
                return [];

            var documentos = contexto.GetAll(s => list.Contains(Convert.ToInt32(s.Cd_DocumentoComplementar_Id!)) && s.Status);

            return [.. documentos];
        }

        public List<DocumentacaoComplementar> DocumentacaoComplementar(Expression<Func<DocumentacaoComplementar, bool>> predicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<DocumentacaoComplementar>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    List<DocumentacaoComplementar> list = [.. contexto.GetAll(predicate)];

                    if (list.Count == 0)
                        return [];

                    return list;
                }
                return [];
            }
            catch
            {
                return [];
            }
        }
    }
}
