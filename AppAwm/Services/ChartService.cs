using AppAwm.DAL;
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
                using var contexto = new RepositoryGeneric<Anexo>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    listAnexos = [.. contexto.GetAll(predicate)];

                    chartRetorno.TotalDocAnalise = listAnexos!.Count(c => c.Status == Models.Enum.EnumStatusDocs.EmAnalise);
                    chartRetorno.TotalDocAprovado = listAnexos!.Count(c => c.Status == Models.Enum.EnumStatusDocs.Aprovado);

                    if (origem == 1)
                    {
                        if (listAnexos != null)
                        {
                            funcionarioAnswer = servicoColaborador.List(f => f.Status
                                && (usuario.Perfil != Models.Enum.EnumPerfil.Administrador ? f.Id_Empresa == listAnexos!.FirstOrDefault()!.Cd_Empresa_Id : f.Id_Empresa > 0)
                                && (usuario.Perfil != Models.Enum.EnumPerfil.Administrador ? f.Id_UsuarioCriacao == usuario.Cd_Usuario : f.Id_Empresa > 0)
                            );

                            chartRetorno.TotalSemDoc = funcionarioAnswer.Colaboradore.Count(s => s.Anexos!.Count == 0);
                        }
                    }
                    else
                    {
                        if (listAnexos != null)
                        {
                            empresaAnswer = servicoEmpresa.ListChart(emp => emp.Status
                            && (usuario.Perfil != Models.Enum.EnumPerfil.Administrador ? emp.Cd_Empresa == listAnexos!.FirstOrDefault()!.Cd_Empresa_Id : emp.Cd_Empresa > 0)
                            );
                        }

                        if (usuario.Perfil == Models.Enum.EnumPerfil.Administrador)
                            chartRetorno.TotalSemDoc = empresaAnswer.Empresas.Count(s => s.Anexos.Count == 0);
                        else
                        {
                            var empAnexo = empresaAnswer.Empresas[0].Anexos.Where(w => w.Cd_Funcionario_Id == null).Select(s => s.TipoAnexo).Distinct().ToList();

                            chartRetorno.TotalSemDoc = Utility.DocumentacaoComplementares.Where(w => w.Status && w.Origem == 2).Select(s => s.Cd_Documentaco_Complementar).Except(empAnexo).Count();
                        }
                    }

                    return ChartAnswer.DeSucesso(chartRetorno);
                }

                funcionarioAnswer = servicoColaborador.List(f => f.Status && f.Id_Empresa == listAnexos!.FirstOrDefault()!.Cd_Empresa_Id);
                chartRetorno.TotalSemDoc = funcionarioAnswer.Colaboradore.Count(s => s.Anexos!.Count == 0);

                return ChartAnswer.DeSucesso(chartRetorno);
            }
            catch (Exception ex)
            {
                return ChartAnswer.DeErro(ex.Message);
            }
        }
    }
}
