using AppAwm.DAL;
using AppAwm.DAL.Repository;
using AppAwm.Models;
using AppAwm.Respostas;
using AppAwm.Services.Interface;
using AppAwm.Util;
using System.Linq.Expressions;

namespace AppAwm.Services
{
    public class ChartService : IChart<ChartAnswer>
    {
        private readonly IColaborador<ColaboradorAnswer> servicoColaborador;
        private readonly IEmpresa<EmpresaAnswer> servicoEmpresa;

        public ChartService(IColaborador<ColaboradorAnswer> _servicoColaborador, IEmpresa<EmpresaAnswer> _servicoEmpresa)
        {
            servicoColaborador = _servicoColaborador;
            servicoEmpresa = _servicoEmpresa;
        }

        private GenericRepositoryValidation.GenericRepositoryExceptionStatus status;

        public ChartAnswer Get(Expression<Func<Anexo, bool>>? predicate, Usuario usuario, int origem)
        {
            try
            {
                Chart chartRetorno = new();
                ColaboradorAnswer? funcionarioAnswer = null;
                EmpresaAnswer? empresaAnswer = null;

                List<Anexo>? listAnexos = null;

                using DbCon db = new();
                using var contexto = new RepositoryAnexo(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    listAnexos = [.. contexto.GetAll(predicate!)];

                    if (listAnexos.Count > 0)
                    {
                        chartRetorno.TotalDocAnalise = listAnexos!.Count(c => c.Status == Models.Enum.EnumStatusDocs.EmAnalise);
                        chartRetorno.TotalDocAprovado = listAnexos!.Count(c => c.Status == Models.Enum.EnumStatusDocs.Aprovado);
                        chartRetorno.TotalDocEnviado = listAnexos!.Count(c => c.Status == Models.Enum.EnumStatusDocs.Enviado);
                    }
                    else
                    {
                        chartRetorno.TotalDocAnalise = 0;
                        chartRetorno.TotalDocAprovado = 0;
                        return ChartAnswer.DeSucesso(chartRetorno);
                    }

                    if (origem == 1)
                    {
                        funcionarioAnswer = servicoColaborador.List(f => f.Status
                            && (usuario.Perfil != Models.Enum.EnumPerfil.Administrador ? f.Id_Empresa == usuario.Cd_Empresa : f.Id_Empresa > 0)
                           // && (usuario.Perfil != Models.Enum.EnumPerfil.Administrador ? f.Id_UsuarioCriacao == usuario.Cd_Usuario : f.Id_Empresa > 0)
                        );

                        chartRetorno.TotalSemDoc = funcionarioAnswer.Colaboradores.Count(s => s.Anexos!.Count == 0);
                    }
                    else
                    {
                        empresaAnswer = servicoEmpresa.ListChart(emp => emp.Status
                        && (usuario.Perfil == Models.Enum.EnumPerfil.Administrador ? emp.Cd_Empresa > 0 : usuario.IsMaster ?  emp.Cd_Cliente_Id == usuario.Cliente.Cd_Cliente: emp.Cd_Empresa == usuario.Empresa.Cd_Empresa)
                        );

                        if (empresaAnswer.Empresas.Count == 0)
                            return ChartAnswer.DeSucesso(chartRetorno);

                        var empresaSemDocs = empresaAnswer.Empresas.Select(s => new Anexo { Cd_Empresa_Id = s.Cd_Empresa }).ExceptBy(listAnexos.Select(ss => ss.Cd_Empresa_Id), Sa1 => Sa1.Cd_Empresa_Id).ToList();

                        if (usuario.Perfil == Models.Enum.EnumPerfil.Administrador)
                            chartRetorno.TotalSemDoc = empresaSemDocs.Count;
                        else
                        {
                            if (empresaSemDocs.Count == 0)
                            {
                                chartRetorno.TotalSemDoc = 0;
                            }
                            else
                            {
                                var empAnexo = empresaAnswer.Empresas[0].Anexos.Where(w => w.Cd_Funcionario_Id == null).Select(s => s.TipoAnexo).Distinct().ToList();

                                chartRetorno.TotalSemDoc = Utility.DocumentacaoComplementares.Where(w => w.Status && w.Origem == 2).Select(s => s.Cd_Documentaco_Complementar).Except(empAnexo).Count();
                            }
                        }
                    }

                    return ChartAnswer.DeSucesso(chartRetorno);
                }

                funcionarioAnswer = servicoColaborador.List(f => f.Status && f.Id_Empresa == listAnexos!.FirstOrDefault()!.Cd_Empresa_Id);
                chartRetorno.TotalSemDoc = funcionarioAnswer.Colaboradores.Count(s => s.Anexos!.Count == 0);

                return ChartAnswer.DeSucesso(chartRetorno);
            }
            catch (Exception ex)
            {
                return ChartAnswer.DeErro(ex.Message);
            }
        }
    }
}
