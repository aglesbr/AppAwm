﻿using AppAwm.Models;
using AppAwm.Models.Enum;
using AppAwm.Respostas;
using AppAwm.Services;
using AppAwm.Services.Interface;
using AppAwm.Util;
using System.Text.RegularExpressions;

namespace AppAwm.Worker
{
    public class MonitorarValidadoDocumento : BackgroundService
    {
        private readonly IAnexo<AnexoAnswer> servicoAnexo;
        private readonly IUsuario<UsuarioAnswer> servicoUsuario;
        private readonly IColaborador<ColaboradorAnswer> servicoFuncionario;
        private readonly IDocumentacaoComplementar<DocumentacaoComplementarAnswer> servicoDocumentacao;
        private readonly ILogException<LogExceptionAnswer> servicoLogException;
        private readonly IEmpresa<EmpresaAnswer> servicoEmpresa;

        public MonitorarValidadoDocumento()
        {
            servicoAnexo = new AnexoService();
            servicoUsuario = new UsuarioService();
            servicoFuncionario = new ColaboradorService();
            servicoDocumentacao = new DocumentacaoComplementarService();
            servicoLogException = new LogExceptionService();
            servicoEmpresa = new EmpresaService();
        }


        /// <summary>
        /// checa a validade dos documentos 1 vez por mês e envia um e-mail para o usuário
        /// </summary>
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
                            var vencimentos = item.Anexos!.Where(i => Enumerable.Range(1, 27).Contains(i.TipoAnexo)
                            && (i.Dt_Validade_Documento - DateTime.Now.Date).TotalDays > 1
                            && (i.Dt_Validade_Documento - DateTime.Now.Date).TotalDays < 30
                            && i.Status == EnumStatusDocs.Aprovado).ToList();

                            if (vencimentos.Count > 0)
                            {
                                vencimentos.ForEach(i => { docs += "<li>" + i.Nome + " - validade: " + i.Dt_Validade_Documento.ToString("dd/MM/yyyy") + "</li>"; });

                                Usuario user = users.Usuarios.FirstOrDefault(f => f.Cd_Usuario == vencimentos[0].Id_UsuarioCriacao)!;
                                string msg = string.Format(mensagem, user.Nome, docs, item.Nome, item.Empresa!.Nome);

                                Utility.EnviarEmail(msg, user);
                                docs = string.Empty;
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

                Utility.EnviarEmail(mensagem, new Usuario { Email = "agles.net@msn.com", Nome = "Herbert Agles" }, "ERRO DE MONITORAMENTO");

                servicoLogException.Save(new LogException
                {
                    Error = $"{ex.Message} - ERRO NO MONITORAMENTO DE DOCUMENTOS",
                    Metodo = "ChecarDatadcoumento()",
                    OrigemTrace = "MonitorarValidadoDocumento"
                });
            }

        }

        /// <summary>
        /// checar a validade do documento com RESALVA e atualizar o status do documento para REJEITADO caso o prazo tenha expirado
        /// </summary>
        protected void UpdateStatusDocumentoValidadeReslva()
        {

            try
            {
                string motivo = "Rejeitado por falta de ajuste no prazo da resalva";
                AnexoAnswer resposta = servicoAnexo.List(l => l.Status == EnumStatusDocs.Resalva);

                if (resposta.Success)
                {
                    foreach (var item in resposta.Anexos)
                    {
                        if ((item.Dt_Criacao.Date - DateTime.Now.Date).TotalDays < 0)
                        {
                            servicoAnexo.UpdateStatus(item.Cd_Anexo, EnumStatusDocs.Rejeitado, "Sistema HDDOC", item.Status == EnumStatusDocs.Resalva ? motivo : null);
                        }
                    }
                }

                resposta = servicoAnexo.List(w => Enumerable.Range(1, 39).Contains(w.TipoAnexo) && w.Status == EnumStatusDocs.Aprovado);

                int totalDays = 0;

                if (resposta.Success)
                {
                    foreach (var item in resposta.Anexos)
                    {
                        totalDays = (item.Dt_Validade_Documento - DateTime.Now.Date).Days;
                        if (0 <= totalDays && totalDays <= 1)
                        {
                            servicoAnexo.UpdateStatus(item.Cd_Anexo, totalDays == 1 ? EnumStatusDocs.Resalva : EnumStatusDocs.Expirado, "Sistema HDDOC", "documento previsto para o vencimento em 24 horas");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string mensagem = string.Empty;
                mensagem += "<html><body><h3><center>HDDOC - ERRO NO MONITORAMENTO DE DOCUMENTOS COM RESALVAS</center><hr/></h3><br/>";
                mensagem += "<p>O sistema identificou no monitoramento de documentos aprovados com RESALVAS</p>";
                mensagem += $"<div style='border:1px solid black; padding:10px; border-radius: 5px;'><br>ERRO: {ex.Message} ";
                mensagem += "</div><p>Resposta automático!<br/>Favor não responder este e-mail!</p></body></html>";

                Utility.EnviarEmail(mensagem, new Usuario { Email = "agles.net@msn.com", Nome = "Herbert Agles" }, "ERRO DE MONITORAMENTO COM RESALVAS");

                servicoLogException.Save(new LogException
                {
                    Error = $"{ex.Message} - ERRO NO MONITORAMENTO DE DOCUMENTOS COM RESALVAS",
                    Metodo = "UpdateStatusDocumentoValidadeReslva()",
                    OrigemTrace = "MonitorarValidadoDocumento"
                });
            }
        }

        /// <summary>
        /// checar a validade do documento APROVADO ou REPROVADO e atualizar o status do documento para EXPIRADO caso o prazo tenha expirado
        /// </summary>
        protected void ChecarValidadoDocumento()
        {
            string mensagem = string.Empty;
            try
            {
                Usuario? usuario = null;
                AnexoAnswer resposta = servicoAnexo.List(l => Enumerable.Range(1, 39).Contains(l.TipoAnexo) 
                && l.Status == EnumStatusDocs.Aprovado || l.Status == EnumStatusDocs.Rejeitado || l.Status == EnumStatusDocs.Enviado);

                if (resposta.Success)
                {
                    foreach (var item in resposta.Anexos)
                    {
                        if ((item.Dt_Validade_Documento - DateTime.Now.Date).TotalDays < 0)
                        {
                            servicoAnexo.UpdateStatus(item.Cd_Anexo, EnumStatusDocs.Expirado, "Sistema HDDOC", "documento vencido");

                            ColaboradorAnswer respostaColaborador = servicoFuncionario.List(g => g.Cd_Funcionario == item.Cd_Funcionario_Id!);

                            /// resgata o colaborador que esta com a documentação vencida
                            if (respostaColaborador.Success)
                            {
                                Colaborador colaborador = respostaColaborador.Colaboradores.FirstOrDefault()!;

                                UsuarioAnswer respostaUsuario = servicoUsuario.List(g => g.Cd_Usuario == colaborador!.Id_UsuarioCriacao);
                                usuario = respostaUsuario.Usuarios.FirstOrDefault();

                                colaborador.Integrado = false;

                                respostaColaborador = servicoFuncionario.Save(colaborador, EnumAcao.Editar);

                                if (respostaColaborador.Success)
                                {
                                    var tipoDocumento = Utility.DocumentacaoComplementarWorker.FirstOrDefault(f => f.Cd_Documentaco_Complementar == item.TipoAnexo)!;

                                    mensagem = "<html><body><h3><center>HDDOC - DOCUMENTAÇÃO EXPIRADA</center><hr/></h3><br/>"
                                    + $"<p>O sistema identificou EXPIRAÇÃO DE DOCUMENTO </p>"
                                    + $"<div style='border:1px solid black; padding:10px; border-radius: 5px;'><br>TIPO DO DOCUMENTO: {tipoDocumento.Nome}<br/>" 
                                    + $"NOME DO DOCUMENTO: {item.Nome} - VALIDADE: {item.Dt_Validade_Documento:dd/MM/yyyy}<br>" 
                                    + $"FUNCIONÁRIO: {colaborador.Nome} - CPF: {Regex.Replace(colaborador.Documento!, @"(\d{3})(\d{3})(\d{3})(\d{2})", @"$1.$2.$3-$3")}"
                                    + "</div><p>Resposta automático!<br/>Favor não responder este e-mail!</p></body></html>";
                                }
                            }
                            else
                            {
                                /// resgata o usuario resposavel pela documentação vencida da empresa
                                
                                EmpresaAnswer empresaAnswer = servicoEmpresa.List(g => g.Cd_Empresa == item.Cd_Empresa_Id!);
                                Empresa empresa = empresaAnswer.Empresas.FirstOrDefault()!;

                                usuario = servicoUsuario.List(g => g.Cd_Usuario == item.Id_UsuarioCriacao).Usuarios.FirstOrDefault();

                                var tipoDocumento = Utility.DocumentacaoComplementarWorker.FirstOrDefault(f => f.Cd_Documentaco_Complementar == item.TipoAnexo)!;

                                mensagem = "<html><body><h3><center>HDDOC - DOCUMENTAÇÃO EXPIRADA</center><hr/></h3><br/>"
                                    + $"<p>O sistema identificou EXPIRAÇÃO DE DOCUMENTO </p>"
                                    + $"<div style='border:1px solid black; padding:10px; border-radius: 5px;'>" 
                                    + $"<br>TIPO DO DOCUMENTO: {tipoDocumento.Nome}<br/>NOME DO DOCUMENTO: {item.Nome} - VALIDADE: {item.Dt_Validade_Documento:dd/MM/yyyy}"
                                    + $"<br>EMPRESA: {empresa.Nome} - CNPJ: {Regex.Replace(empresa.Cnpj!, @"(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})", @"$1.$2.$3/$4-$5")}"
                                    + $"</div><p>Resposta automático!<br/>Favor não responder este e-mail!</p></body></html>";

                            }


                            Utility.EnviarEmail(mensagem, new Usuario
                            {
                                Email = usuario!.Email,
                                Nome = usuario.Nome,
                            });


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mensagem =  string.Empty;
                mensagem += "<html><body><h3><center>HDDOC - MONITORAMENTO DE DOCUMENTOS(EXPIRADO)</center><hr/></h3><br/>";
                mensagem += "<p>O sistema identificou no monitoramento de EXPIRAÇÃO</p>";
                mensagem += $"<div style='border:1px solid black; padding:10px; border-radius: 5px;'><br>ERRO: {ex.Message} ";
                mensagem += "</div><p>Resposta automático!<br/>Favor não responder este e-mail!</p></body></html>";
                Utility.EnviarEmail(mensagem, new Usuario
                {
                    Email = "agles.net@msn.com",
                    Nome = "Herbert Agles"
                }, "DOCUMENTAÇÃO EXPIRADA - ERRO");

                servicoLogException.Save(new LogException
                {
                    Error = $"{ex.Message} - DOCUMENTAÇÃO EXPIRADA",
                    Metodo = "ChecarValidadoDocumentoAprovador()",
                    OrigemTrace = "MonitorarValidadoDocumento"
                });
            }
        }

        /// <summary>
        /// Remove os documentos expirados a mais de 30 dias da base de dados
        /// </summary>
        protected void RemoveDocumentosExpirados()
        {
            try
            {
                AnexoAnswer resposta = servicoAnexo.List(w => Enumerable.Range(1, 39).Contains(w.TipoAnexo) && w.Status == EnumStatusDocs.Expirado);

                int totalDays = 0;

                if (resposta.Success)
                {
                    foreach (var item in resposta.Anexos)
                    {
                        totalDays = (item.Dt_Validade_Documento - DateTime.Now.Date).Days;

                        if (totalDays < -30)
                        {
                            servicoAnexo.Remove(item);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                servicoLogException.Save(new LogException
                {
                    Error = ex.Message,
                    Metodo = "RemoveDocumentosExpirados()",
                    OrigemTrace = "MonitorarValidadoDocumento"
                });
            }
        }


        protected void CarregarDocumentosComplementares()
        {
            try
            {
                var documentos = servicoDocumentacao.Get(l => l.Status);
                if (documentos.Success)
                {
                    Utility.DocumentacaoComplementarWorker = documentos.DocumentacaoComplementares;
                }
            }
            catch (Exception ex)
            {
                servicoLogException.Save(new LogException
                {
                    Error = $"{ex.Message} - CARREGAR DOCUMENTO COMPLEMENTARRES CARGA INICIAL",
                    Metodo = "CarregarDocumentosComplementares()",
                    OrigemTrace = "MonitorarValidadoDocumento"
                });
            }
        }

        /// <summary>
        /// Principal Método que executa o serviço em segundo plano para monitorar a validade dos documentos complementares.
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            CarregarDocumentosComplementares();
            var timer = new PeriodicTimer(TimeSpan.FromDays(1));

            do
            {
                ChecarDatadcoumento();
                UpdateStatusDocumentoValidadeReslva();
                ChecarValidadoDocumento();
                RemoveDocumentosExpirados();
            }
            while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken));
        }
    }
}
