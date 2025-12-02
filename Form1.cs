using FullAtuSeletor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ImportFullUpdater
{
    public partial class Form1 : Form
    {
        Arquivo arquivo = new Arquivo();

        public Form1()
        {
            InitializeComponent();
            var dados = arquivo.Ler();
            CaminhoImportFull.Text = dados.enderecoDestino.ToString();
        }

        private void BtnFolder_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd1 = new OpenFileDialog();
            //define as propriedades do controle OpenFileDialog
            ofd1.Multiselect = false;
            ofd1.Title = "Selecionar Arquivo";
            ofd1.InitialDirectory = @"C:\dados\txt";
            //filtra para exibir todos os arquivos
            ofd1.Filter = "All files (*.exe)|*.exe";
            ofd1.CheckFileExists = true;
            ofd1.CheckPathExists = true;
            ofd1.FilterIndex = 1;
            ofd1.RestoreDirectory = true;
            ofd1.ReadOnlyChecked = true;
            ofd1.ShowReadOnly = true;
            DialogResult dr = ofd1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                CaminhoImportFull.Text = ofd1.FileName;
            }
            if (!string.IsNullOrEmpty(CaminhoImportFull.Text))
            {
                btnAtualizar.Enabled = true;
            }
            arquivo.Gravar(new Arquivo { enderecoDestino = CaminhoImportFull.Text });
        }
    
        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(lblArquivo.Text))
                {
                    var dados = arquivo.Ler();
                    // Verifica se o arquivo de origem existe
                    //if (!File.Exists(dados.enderecoOrigem))
                    //{
                    //    MessageBox.Show("Arquivo de origem não encontrado!", "Erro");
                    //    return;
                    //}

                    // Verifica se o caminho de destino é válido e termina com .exe
                    if (string.IsNullOrEmpty(CaminhoImportFull.Text) || !CaminhoImportFull.Text.EndsWith(".exe"))
                    {
                        MessageBox.Show("O caminho de destino não é válido. Certifique-se de que é um arquivo .exe.", "Erro");
                        return;
                    }

                    // Realiza a cópia do arquivo
                    File.Copy($@"{dados.enderecoOrigem}/PLSQLImportFull.exe", CaminhoImportFull.Text, true);
                    File.Copy($@"{dados.enderecoOrigem}/version.txt", CaminhoImportFull.Text.Replace("PLSQLImportFull.exe", "version.txt"), true);
                    string caminhoExe = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PLSQLImportFull.exe");
                    Process.Start(caminhoExe);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro");
            }

        }
    }
}
