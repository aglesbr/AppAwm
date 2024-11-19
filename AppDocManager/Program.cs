using System;
using System.Windows.Forms;

namespace AppDocManager
{
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        /// 

        //private static void ConfigureServices(ServiceCollection services)
        //{
        //    //services.AddScoped<IPaciente, PacienteConsumer>();
        //   // services.AddScoped<Form1>();
        //}


        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UI04FrmLogin());
            //Application.Run(new UI01FrmMain());
        }
    }
}
