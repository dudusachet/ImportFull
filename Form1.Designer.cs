namespace ImportFullUpdater
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblArquivo = new System.Windows.Forms.Label();
            this.btnAtualizar = new System.Windows.Forms.Button();
            this.BtnFolder = new System.Windows.Forms.Button();
            this.CaminhoImportFull = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblArquivo
            // 
            this.lblArquivo.AutoSize = true;
            this.lblArquivo.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArquivo.Location = new System.Drawing.Point(23, 32);
            this.lblArquivo.Name = "lblArquivo";
            this.lblArquivo.Size = new System.Drawing.Size(111, 14);
            this.lblArquivo.TabIndex = 0;
            this.lblArquivo.Text = "Selecione o arquivo:";
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAtualizar.ForeColor = System.Drawing.Color.Black;
            this.btnAtualizar.Location = new System.Drawing.Point(113, 83);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(149, 23);
            this.btnAtualizar.TabIndex = 1;
            this.btnAtualizar.Text = "Atualizar";
            this.btnAtualizar.UseVisualStyleBackColor = true;
            this.btnAtualizar.Click += new System.EventHandler(this.btnAtualizar_Click);
            // 
            // BtnFolder
            // 
            this.BtnFolder.AllowDrop = true;
            this.BtnFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BtnFolder.Location = new System.Drawing.Point(356, 54);
            this.BtnFolder.Name = "BtnFolder";
            this.BtnFolder.Size = new System.Drawing.Size(42, 23);
            this.BtnFolder.TabIndex = 2;
            this.BtnFolder.Text = "...";
            this.BtnFolder.UseVisualStyleBackColor = true;
            this.BtnFolder.Click += new System.EventHandler(this.BtnFolder_Click);
            // 
            // CaminhoImportFull
            // 
            this.CaminhoImportFull.Location = new System.Drawing.Point(25, 54);
            this.CaminhoImportFull.Name = "CaminhoImportFull";
            this.CaminhoImportFull.Size = new System.Drawing.Size(325, 20);
            this.CaminhoImportFull.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 131);
            this.Controls.Add(this.CaminhoImportFull);
            this.Controls.Add(this.BtnFolder);
            this.Controls.Add(this.btnAtualizar);
            this.Controls.Add(this.lblArquivo);
            this.Name = "Form1";
            this.Text = "ImportFull Updater";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblArquivo;
        private System.Windows.Forms.Button btnAtualizar;
        private System.Windows.Forms.Button BtnFolder;
        private System.Windows.Forms.TextBox CaminhoImportFull;
    }
}

