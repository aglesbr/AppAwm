using AppAwm.Models;
using AppAwm.Models.Enum;
using AppAwm.Respostas;
using AppAwm.Services;
using AppAwm.Services.Interface;
using AppAwm.Util;

namespace AppAwm.Worker
{
    public class MonitorarValidadoDocumento : BackgroundService
    {
        private readonly IAnexo<AnexoAnswer> servicoAnexo;
        private readonly IUsuario<UsuarioAnswer> servicoUsuario;
        private readonly IColaborador<ColaboradorAnswer> servicoFuncionario;
        public MonitorarValidadoDocumento()
        {
            servicoAnexo = new AnexoService();
            servicoUsuario = new UsuarioService();
            servicoFuncionario = new ColaboradorService();
        }


        protected void ChecarDatadcoumento()
        {
            string mensagem = string.Empty;

            try
            {
                mensagem += "<html><body><h3><center>HDDOC - AVISO DE VALIDADE DE DOCUMENTO</center><hr/></h3><br/><p>Olá {0}<br/></p>";
                mensagem += "<p>O sistema identificou documentos com o prazo de validade proximo do vencimento.</p>";
                mensagem += "<div style='border:1px solid black; padding:10px; border-radius: 5px;'><br>DOCUMENTO:<br><ul>{1}</ul><br>FUNCIONARIO: {2}<br>EMPRESA: {3}";
                mensagem += "</div><p>Resposta automático!<br/>Favor não responder este e-mail!</p></body></html>";

                var execucao = servicoAnexo.GetLastHistoricoExecucao() ?? new HistoricoExecucao { Dt_Execucao = DateTime.Now.Date };

                var date = (execucao?.Dt_Execucao.Month == DateTime.Now.Date.Month);

                if (date)
                {
                    servicoAnexo.InsereProximaExecucao(new HistoricoExecucao { Dt_Execucao = execucao!.Dt_Execucao.AddMonths(1) });

                    var lista = servicoFuncionario.List(x => x.Status && x.Anexos!.Count > 0);

                    var users = servicoUsuario.List(y => y.Status);
                    string docs = string.Empty;

                    if (lista.Success)
                    {
                        lista.Colaboradores.ForEach(x =>
                        {
                            if (x.Empresa != null)
                            {
                                x.Foto = null;
                            }
                        });

                        foreach (var item in lista.Colaboradores)
                        {
                            var vencimentos = item.Anexos!.Where(i => (i.Dt_Validade_Documento - DateTime.Now.Date).TotalDays > 1 && (i.Dt_Validade_Documento - DateTime.Now.Date).TotalDays < 30 && i.Status == EnumStatusDocs.Aprovado).ToList();

                            if (vencimentos.Count > 0)
                            {
                                vencimentos.ForEach(i => { docs += "<li>" + i.Nome + " - validade: " + i.Dt_Validade_Documento.ToShortDateString() + "</li>"; });

                                Usuario user = users.Usuarios.FirstOrDefault(f => f.Cd_Usuario == vencimentos[0].Id_UsuarioCriacao)!;
                                string msg = string.Format(mensagem, user.Nome, docs, item.Nome, item.Empresa!.Nome);

                                Utility.EnviarEmail(msg, user);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                mensagem += "<html><body><h3><center>HDDOC - ERRO NO MONITORAMENTO DE DOCUMENTOS</center><hr/></h3><br/>";
                mensagem += "<p>O sistema identificou no monitoramento do prazo de validade dos documentos.</p>";
                mensagem += $"<div style='border:1px solid black; padding:10px; border-radius: 5px;'><br>ERRO: {ex.Message} ";
                mensagem += "</div><p>Resposta automático!<br/>Favor não responder este e-mail!</p></body></html>";

                Utility.EnviarEmail(mensagem, new Usuario { Email = "hagles@hddoc.com.br", Nome = "Herbert Agles" }, "ERRO DE MONITORAMENTO");
            }

        }


        protected void UpdateStatusDocumentoValidadeReslva()
        {
            string mensagem = string.Empty;

            try
            {
                AnexoAnswer? anexoAnswer = null;
                string motivo = "Rejeitado por falta de ajuste no prazo da resalva";
                AnexoAnswer resposta = servicoAnexo.List(l => l.Status == EnumStatusDocs.Resalva);

                if (resposta.Success)
                {
                    foreach (var item in resposta.Anexos)
                    {
                        DateTime dateValidate = (item.Status == EnumStatusDocs.Resalva ? item.Dt_Criacao.Date.AddDays(1) : item.Dt_Validade_Documento);

                        if ((dateValidate - DateTime.Now.Date).TotalDays <= 0)
                        {
                            servicoAnexo.UpdateStatus(item.Cd_Anexo, item.Status == EnumStatusDocs.Aprovado ? EnumStatusDocs.Expirado : EnumStatusDocs.Rejeitado, "Sistema HDDOC", item.Status == EnumStatusDocs.Resalva ? motivo : null);
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                mensagem += "<html><body><h3><center>HDDOC - ERRO NO MONITORAMENTO DE DOCUMENTOS COM RESALVAS</center><hr/></h3><br/>";
                mensagem += "<p>O sistema identificou no monitoramento de documentos aprovados com RESALVAS</p>";
                mensagem += $"<div style='border:1px solid black; padding:10px; border-radius: 5px;'><br>ERRO: {ex.Message} ";
                mensagem += "</div><p>Resposta automático!<br/>Favor não responder este e-mail!</p></body></html>";

                Utility.EnviarEmail(mensagem, new Usuario { Email = "hagles@hddoc.com.br", Nome = "Herbert Agles" }, "ERRO DE MONITORAMENTO COM RESALVAS");
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var timer = new PeriodicTimer(TimeSpan.FromDays(1));

            while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
            {
                ChecarDatadcoumento();
                UpdateStatusDocumentoValidadeReslva();
            }
        }
    }
}
