using AppDocManager.Models;
using System;
using System.IO;
using System.Windows.Forms;

namespace AppDocManager
{
    public partial class UI03FrmPdf : Form
    {
        private readonly Anexo ObjAnexo;
        public UI03FrmPdf()
        {
            InitializeComponent();
        }

        public UI03FrmPdf(Anexo anexo)
        {
            ObjAnexo = anexo;
            InitializeComponent();
        }

        private void UI03FrmPdf_Load(object sender, EventArgs e)
        {
            if (ObjAnexo.Arquivo.Length == 0)
            {
                MemoryStream msErro = new MemoryStream(Properties.Resources.DocErro);
                pdfViewer1.LoadFromStream(msErro);
                return;
            }

            MemoryStream ms = new MemoryStream(ObjAnexo.Arquivo) ;
            pdfViewer1.LoadFromStream(ms);
        }
    }
}
