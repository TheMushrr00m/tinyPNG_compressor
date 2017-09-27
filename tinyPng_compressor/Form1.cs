using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tinyPng_compressor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Task<int> ExecuteCompression(string _File)
        {
            // Launch powershell
            System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo(
                "cmd", 
                "/c node ../../assets/index.js " + _File
            );

            // Se redirecciona la salida del método a un Stream de datos
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;

            // Ejecuta el proceso en el background
            procStartInfo.CreateNoWindow = true;

            // Inicia la compresión
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo = procStartInfo;
            proc.Start();

            // Obtiene la salida por consola
            string result = proc.StandardOutput.ReadToEnd();

            // Devuelve un valor con una expresión lambda
            return Task<int>.Factory.StartNew(() => 1);
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            // Muestra el dialogo para elegir el directorio
            DialogResult result = browserDialog.ShowDialog();

            if(result == DialogResult.OK)
            {
                lbStatus.Text = "Procesando...";
                string[] files =
                    Directory.GetFiles(browserDialog.SelectedPath);

                for(int i = 0; i < files.Length; i++)
                {
                    var task = ExecuteCompression(files[i]);
                    task.Start();

                    var task_result = await task;
                    lbStatus.Text = "Archivos comprimidos: " + (i + 1) + "/" + files.Length;
                }
            }
        }
    }
}
