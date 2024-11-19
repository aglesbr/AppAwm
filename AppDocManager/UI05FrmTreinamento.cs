using AppDocManager.Models;
using AppDocManager.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Resources.ResXFileRef;

namespace AppDocManager
{
    public partial class UI05FrmTreinamento : UI00FrmTemplate
    {
        private int _id = 0;
        public UI05FrmTreinamento(int id)
        {
            _id = id;
            InitializeComponent();
        }
        
        private void Salvar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("teste");
        }

        private async void UI05FrmTreinamento_Load(object sender, EventArgs e)
        {
            try
            {
                UI01FrmMain.salvar.Click += Salvar_Click;
                var response = ServiceAwm.Get($"Operacao/GetTreinamentos");
                var resposta = response.Result.EnsureSuccessStatusCode();

                if (resposta.IsSuccessStatusCode)
                {
                    var carga = await resposta.Content.ReadAsStringAsync();
                    List<Treinamento> treinamentos = JsonConvert.DeserializeObject<List<Treinamento>>(carga);

                    TreeViewTreinamento.Nodes.Clear();

                    TreeNode nodeRoot = new TreeNode("Aptidões de Treinamentos");

                    foreach (var item in treinamentos)
                    {
                        var node = new TreeNode { Text = item.Nome, Tag = item };

                        foreach (var habilidade in item.Habilidades)
                        {
                            node.Nodes.Add(new TreeNode { Text = habilidade.Nome, Tag = habilidade });
                        }
                        nodeRoot.Nodes.Add(node);
                    }

                    TreeViewTreinamento.Nodes.Add(nodeRoot);
                    TreeViewTreinamento.Nodes[0].Expand();


                    response = ServiceAwm.Get($"Operacao/GetColaborador/{_id}");
                    resposta = response.Result.EnsureSuccessStatusCode();

                    if (resposta.IsSuccessStatusCode)
                    {
                        var strFuncionario = await resposta.Content.ReadAsStringAsync();
                        Funcionario funcionario = JsonConvert.DeserializeObject<Funcionario>(strFuncionario);

                        if (funcionario.Foto != null)
                        {
                            MemoryStream ms = new MemoryStream(funcionario.Foto);
                            pictureFoto.Image = Image.FromStream(ms);
                        }

                        txtNome.Text = funcionario.Nome;
                        txtCargo.Text = funcionario.Cargo.Nome;
                        txtEmpresa.Text = funcionario.Empresa.Nome;
                        txtDescricao.Text = funcionario.Observacao;
                        materialChkEstrangeiro.Checked = funcionario.Nacionalidade;
                        materialChkPcd.Checked = funcionario.Pcd;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao tentar carregar as informações,\nERRO: " + ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void TreeViewTreinamento_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level > 0)
            {
                TreeViewTreinamento.AfterCheck -= TreeViewTreinamento_AfterCheck;

                if (e.Node.Level == 1)
                {
                    e.Node.Nodes.OfType<TreeNode>().ToList().ForEach(t => t.Checked = e.Node.Checked);
                }

                if (e.Node.Level == 2)
                {
                    var item = e.Node.Parent.Nodes.OfType<TreeNode>().FirstOrDefault(x => x.Checked);
                    e.Node.Parent.Checked = item != null;
                }

                TreeViewTreinamento.AfterCheck += TreeViewTreinamento_AfterCheck;

            }
        }
    }
}
