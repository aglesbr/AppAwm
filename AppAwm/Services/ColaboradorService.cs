using AppAwm.DAL;
using AppAwm.Models;
using AppAwm.Models.Enum;
using AppAwm.Respostas;
using AppAwm.Services.Interface;
using AppAwm.Util;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Globalization;
using System.Linq.Expressions;

namespace AppAwm.Services
{
    public class ColaboradorService : IColaborador<ColaboradorAnswer>
    {
        private GenericRepositoryValidation.GenericRepositoryExceptionStatus status;

        public bool Check(Expression<Func<Colaborador, bool>> predicate)
        {
            using DbCon db = new();
            using var contexto = new RepositoryGeneric<Colaborador>(db, out status);

            if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                return contexto.GetItem(predicate) is not null;

            return false;
        }

        public ColaboradorAnswer Get(Expression<Func<Colaborador, bool>> predicate, Usuario? usuario)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Colaborador>(db, out status);
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    Colaborador? funcionario = contexto.GetAll(predicate).FirstOrDefault();

                    if (funcionario != null)
                    {
                        var contextoCargo = new RepositoryGeneric<Cargo>(db, out status);
                        funcionario.Cargo = contextoCargo.GetItem(s => s.Cd_Cargo == funcionario.Cd_Cargo);

                    }
                    // consome lista de empresas
                    using var contextoEmpresa = new RepositoryGeneric<Empresa>(db, out status);
                    List<SelectListItem> empresas = [.. contextoEmpresa.GetAll(p =>
                     (usuario!.Perfil == EnumPerfil.Administrador ? p.Cd_Empresa > 0 : p.Cd_Empresa == usuario.Cd_Empresa)
                     && p.Status
                    ).
                    Select(p => new SelectListItem { Value = p.Cd_Empresa.ToString(), Text = p.Nome })];


                    return ColaboradorAnswer.Bind(funcionario, empresas);
                }

                return ColaboradorAnswer.DeFalha();
            }
            catch (Exception ex)
            {
                return ColaboradorAnswer.DeErro(ex.Message);
            }
        }

        public ColaboradorAnswer List(Expression<Func<Colaborador, bool>> predicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Colaborador>(db, out status);
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    List<Colaborador> funcionarios = [.. contexto.GetAll(predicate)
                        .Include(emp => emp.Empresa).OrderBy(o => o.Nome)
                        .Include(o => o.VinculoObras)
                        .Include(a => a.Anexos)
                        .Select(ss => new Colaborador
                        {
                             Anexos = (ICollection<Anexo>)ss.Anexos.Select(anx =>
                             new Anexo {
                                 Cd_Anexo = anx.Cd_Anexo, TipoAnexo = anx.TipoAnexo, Status = anx.Status, Cd_Empresa_Id = anx.Cd_Empresa_Id, Cd_Funcionario_Id = anx.Cd_Funcionario_Id,
                                 MotivoRejeicao = anx.MotivoRejeicao, MotivoResalva = anx.MotivoResalva, Nome = anx.Nome, Dt_Validade_Documento = anx.Dt_Validade_Documento,
                                 Cd_UsuarioCriacao = anx.Cd_UsuarioCriacao, Id_UsuarioCriacao = anx.Id_UsuarioCriacao
                             }),
                             VinculoObras = ss.VinculoObras,
                             Empresa = ss.Empresa,
                             Cd_Funcionario = ss.Cd_Funcionario,
                             Documento = ss.Documento,
                             Integrado = ss.Integrado,
                             Cd_UsuarioCriacao = ss.Cd_UsuarioCriacao,
                             Status = ss.Status,
                             Nascimento = ss.Nascimento,
                             Escolaridade = ss.Escolaridade,
                             Id_UsuarioCriacao = ss.Id_UsuarioCriacao,
                             Foto = ss.Foto,
                             Estrangeiro = ss.Estrangeiro,
                             Pcd = ss.Pcd,
                             Sexo = ss.Sexo,
                             TipoContrato = ss.TipoContrato,
                             Id_Empresa = ss.Id_Empresa,
                             Cd_Cargo = ss.Cd_Cargo,
                             Nome = ss.Nome,
                             Telefone = ss.Telefone,
                        })];

                    funcionarios.ForEach(f =>
                    {
                        f.TotalDocumentoParaVencer = f.Anexos!.Count(w =>
                            Enumerable.Range(1, 27).Contains(w.TipoAnexo)
                            && w.Status == EnumStatusDocs.Aprovado 
                            && (w.Dt_Validade_Documento - DateTime.Now.Date).TotalDays > 1 && (w.Dt_Validade_Documento - DateTime.Now.Date).TotalDays < 30);
                    });

                    return funcionarios.Count > 0 ? ColaboradorAnswer.DeSucesso(funcionarios) : ColaboradorAnswer.DeErro("Nenhum registro fui localizado");
                }

                return ColaboradorAnswer.DeFalha();
            }
            catch (Exception ex)
            {
                return ColaboradorAnswer.DeErro(ex.Message);
            }
        }

        public ColaboradorAnswer Save(Colaborador funcionario, EnumAcao acao)
        {
            using DbCon db = new();
            using var contexto = new RepositoryGeneric<Colaborador>(db, out status);
            try
            {
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    if (acao == EnumAcao.Editar)
                    {
                        var obj = contexto.GetItem(x => x.Cd_Funcionario == funcionario.Cd_Funcionario) ?? new();
                        funcionario.Foto = obj.Foto!;
                    }
                    int ret = acao == EnumAcao.Criar ? contexto.Create(funcionario) : contexto.Edit(funcionario);

                    return ret > 0 ? ColaboradorAnswer.DeSucesso(acao) : ColaboradorAnswer.DeErro("Ocorreu um erro ao tentar registar a empresa"); ;
                }

                return ColaboradorAnswer.DeFalha();
            }
            catch (Exception ex)
            {
                return ColaboradorAnswer.DeErro(ex.Message);
            }
        }

        public List<Cargo> GetCargos(string nome)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Cargo>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    return [.. contexto.GetAll(g => g.Nome!.ToLower().StartsWith(nome.ToLower())).Take(50)];
                }

                return [];

            }
            catch
            {
                throw;
            }
        }

        public List<Empresa> GetEmpresas(Expression<Func<Empresa, bool>> predicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Empresa>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    return [.. contexto.GetAll(predicate)];
                }

                return [];

            }
            catch
            {
                throw;
            }
        }

        public int UpdateFoto(Colaborador funcionario)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Colaborador>(db, out status);

                var resposta = db.Funcionarios.Where(w => w.Cd_Funcionario == funcionario.Cd_Funcionario)
                .ExecuteUpdate(ax => ax.SetProperty(sp => sp.Foto, funcionario.Foto));

                return resposta;
            }
            catch
            {
                return 0;
            }
        }

        public Cracha? GetCracha(int? id = 0)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Colaborador>(db, out status);


                var resposta = db.Funcionarios.Where(w => w.Cd_Funcionario == id)
                    .Include(emp => emp.Empresa)
                    .Include(axo => axo.Anexos)
                    .Select(s => new
                    {
                        s.Nome,
                        s.Documento,
                        s.Foto,
                        s.Cd_Cargo,
                        s.Empresa,
                        Anexo = s.Anexos.Where(w => w.Status == EnumStatusDocs.Aprovado)
                        .ToList()
                    }).FirstOrDefault();


                if (resposta != null)
                {
                    var cargo = db.Cargos.SingleOrDefault(s => s.Cd_Cargo == resposta.Cd_Cargo);

                    Cracha cracha = new() 
                    { 
                        Nome = resposta.Nome, 
                        Documento = resposta.Documento, 
                        Empresa = resposta.Empresa.Nome, 
                        Foto = resposta.Foto, 
                        Cargo = cargo.Nome, 
                        QrCode = null,
                        AnexosTreinamentos = [.. resposta.Anexo.Select(s => new Anexo 
                        { 
                                Dt_Validade_Documento = s.Dt_Validade_Documento,  
                                Nome = Utility.DocumentacaoComplementarWorker.SingleOrDefault(f => f.Cd_Documentaco_Complementar == s.TipoAnexo).Nome 
                        })
                        .OrderBy(o => o.Nome)],
                    };

                    return cracha;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public int UpdateCliente(bool isAdd)
        {
            using DbCon db = new();
            using var contexto = new RepositoryGeneric<Cliente>(db, out status);
            try
            {
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    if (isAdd)
                        Utility.Cliente.PlanoVidasAtivadas++;
                    else
                        Utility.Cliente.PlanoVidasAtivadas--;

                    int ret = contexto.Edit(Utility.Cliente);

                    return ret;
                }

                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public ColaboradorAnswer ImportarColaboradore(MemoryStream stream, Usuario? usuario, int cd_empresa)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Colaborador>(db, out status);

                Colaborador? colaborador = null;
                List<Colaborador> list = [];

                string[] header = ["COLABORADOR", "SEXO", "CODIGO_CARGO", "CPF", "TELEFONE", "DATA_NASCIMENTO", "DATA_ADMISSAO"];

                List<Colaborador> checkList = db.Funcionarios.Where(w => w.Id_Empresa == cd_empresa).ToList();

                Empresa? empresa = GetEmpresas(emp => emp.Status && emp.Cd_Empresa == cd_empresa).FirstOrDefault();

                if (empresa == null)
                    return ColaboradorAnswer.DeErro($"A empresa com o cnpj: {empresa!.Cnpj}, ainda não está cadastrada no sistema.");

                IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);

                DataTable ds = reader.AsDataSet().Tables["ListaColaborador"]!;

                if (ds == null)
                    return ColaboradorAnswer.DeErro("O arquivo enviado para importação não é válido");

                var t = ds.Rows[0];
                foreach (var coluna in t.ItemArray)
                {
                    if (!header.Contains(coluna.ToString()))
                        return ColaboradorAnswer.DeErro("O arquivo enviado para importação não é válido");
                }

                ds.Rows.RemoveAt(0);
                foreach (DataRow dr in ds.Rows)
                {
                    colaborador = new()
                    {
                        Id_Empresa = cd_empresa,
                        Id_UsuarioCriacao = usuario.Cd_Usuario,
                        Escolaridade = 2,
                        Status = true,
                        Cd_UsuarioCriacao = usuario.Nome,
                        Integrado = false,
                        TipoContrato = 1,
                        Dt_Criacao = DateTime.Now.Date,
                        Nome = dr[0].ToString(),
                        Sexo = dr[1].ToString(),
                        Cd_Cargo = Convert.ToInt32(dr[2]),
                        Documento = dr[3].ToString(),
                        Telefone = dr[4].ToString(),
                        Nascimento = DateTime.ParseExact(dr[5].ToString().Replace('.', '/'), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        Dt_Admissao = DateTime.ParseExact(dr[6].ToString().Replace('.', '/'), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    };

                    if (checkList.Any(n => n.Documento == colaborador.Documento)) continue;

                    list.Add(colaborador);
                }

                int retorno = contexto.BulkInsert(list);

                return retorno > 0 ? ColaboradorAnswer.DeSucesso(EnumAcao.Criar) : ColaboradorAnswer.DeErro("Todos os colaboradores da planilha<br/>já foram cadastrados anteriormente.");
            }
            catch (Exception ex)
            {
                return ColaboradorAnswer.DeErro("Ocorreu um erro na importação,<br/> verifique se dos os campos estão preenchido conforme o modelo da planilha fornecida<br/>ERRO " + ex.Message);
            }
        }

        public bool CheckClienteVidasDisponivel()
        {
            using DbCon db = new();
            using var contexto = new RepositoryGeneric<Cliente>(db, out status);

            var cliente = contexto.GetItem(c => c.Cd_Cliente == Utility.Cliente!.Cd_Cliente);

            return Utility.Cliente!.PlanoVidasAtivadas < cliente!.PlanoVidas;
        }
    }
}
