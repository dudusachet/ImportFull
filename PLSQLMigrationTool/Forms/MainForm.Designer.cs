using System.Drawing;
using System.Windows.Forms;

namespace PLSQLImportFull.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabMaintenance = new System.Windows.Forms.TabPage();
            this.btnRunMaintenance = new System.Windows.Forms.Button();
            this.grpMaintenanceActions = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.chkDisableAllTriggers = new System.Windows.Forms.CheckBox();
            this.chkDisableFKAndCheck = new System.Windows.Forms.CheckBox();
            this.chkPurgeRecycleBin = new System.Windows.Forms.CheckBox();
            this.chkResetSequences = new System.Windows.Forms.CheckBox();
            this.chkGatherStats = new System.Windows.Forms.CheckBox();
            this.rtbImportLog = new System.Windows.Forms.RichTextBox();
            this.lblLog = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabConnection = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.btnLoadConfig = new System.Windows.Forms.Button();
            this.btnPasteString = new System.Windows.Forms.Button();
            this.lblConnectionStatus = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.lblUserId = new System.Windows.Forms.Label();
            this.txtServiceName = new System.Windows.Forms.TextBox();
            this.lblServiceName = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.lblHost = new System.Windows.Forms.Label();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabTruncate = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.checkedListTables = new System.Windows.Forms.CheckedListBox();
            this.btnDeselectAllTables = new System.Windows.Forms.Button();
            this.btnSelectAllTables = new System.Windows.Forms.Button();
            this.btnRefreshTables = new System.Windows.Forms.Button();
            this.btnTruncate = new System.Windows.Forms.Button();
            this.btnSortTruncateName = new System.Windows.Forms.Button();
            this.btnSortTruncateRows = new System.Windows.Forms.Button();
            this.tabImport = new System.Windows.Forms.TabPage();
            this.lstImportFiles = new System.Windows.Forms.ListBox();
            this.btnSelectImportFiles = new System.Windows.Forms.Button();
            this.btnRunImport = new System.Windows.Forms.Button();
            this.btnClearImportList = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tabRestore = new System.Windows.Forms.TabPage();
            this.lblRestoreInfo = new System.Windows.Forms.Label();
            this.btnRunRestore = new System.Windows.Forms.Button();
            this.grpRestoreActions = new System.Windows.Forms.GroupBox();
            this.chkEnableAllTriggers = new System.Windows.Forms.CheckBox();
            this.chkEnableFKAndCheck = new System.Windows.Forms.CheckBox();
            this.tabTriggers = new System.Windows.Forms.TabPage();
            this.btnDisableAllTriggers = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.checkedListTriggers = new System.Windows.Forms.CheckedListBox();
            this.btnDeselectAllTriggers = new System.Windows.Forms.Button();
            this.btnSelectAllTriggers = new System.Windows.Forms.Button();
            this.btnRefreshTriggers = new System.Windows.Forms.Button();
            this.btnDisableTriggers = new System.Windows.Forms.Button();
            this.btnEnableTriggers = new System.Windows.Forms.Button();
            this.tabConstraints = new System.Windows.Forms.TabPage();
            this.btnDisableFKandCheck = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.checkedListConstraints = new System.Windows.Forms.CheckedListBox();
            this.btnDeselectAllConstraints = new System.Windows.Forms.Button();
            this.btnSelectAllConstraints = new System.Windows.Forms.Button();
            this.btnRefreshConstraints = new System.Windows.Forms.Button();
            this.btnDisableConstraints = new System.Windows.Forms.Button();
            this.btnEnableConstraints = new System.Windows.Forms.Button();
            this.tabEnableConstraints = new System.Windows.Forms.TabPage();
            this.pnlConstraintFilter = new System.Windows.Forms.Panel();
            this.lblFilter = new System.Windows.Forms.Label();
            this.chkForeign = new System.Windows.Forms.CheckBox();
            this.chkPrimary = new System.Windows.Forms.CheckBox();
            this.chkUnique = new System.Windows.Forms.CheckBox();
            this.chkCheck = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkedListEnableConstraints = new System.Windows.Forms.CheckedListBox();
            this.btnDeselectAllEnableConstraints = new System.Windows.Forms.Button();
            this.btnSelectAllEnableConstraints = new System.Windows.Forms.Button();
            this.btnRefreshEnableConstraints = new System.Windows.Forms.Button();
            this.btnEnableSelectedConstraints = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabMaintenance.SuspendLayout();
            this.grpMaintenanceActions.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabConnection.SuspendLayout();
            this.tabTruncate.SuspendLayout();
            this.tabImport.SuspendLayout();
            this.tabRestore.SuspendLayout();
            this.grpRestoreActions.SuspendLayout();
            this.tabConstraints.SuspendLayout();
            this.tabEnableConstraints.SuspendLayout();
            this.pnlConstraintFilter.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMaintenance
            // 
            this.tabMaintenance.Controls.Add(this.btnRunMaintenance);
            this.tabMaintenance.Controls.Add(this.grpMaintenanceActions);
            this.tabMaintenance.Location = new System.Drawing.Point(4, 22);
            this.tabMaintenance.Name = "tabMaintenance";
            this.tabMaintenance.Padding = new System.Windows.Forms.Padding(3);
            this.tabMaintenance.Size = new System.Drawing.Size(750, 438);
            this.tabMaintenance.TabIndex = 1;
            this.tabMaintenance.Text = "2 - Controle / Manutenção";
            this.tabMaintenance.UseVisualStyleBackColor = true;
            // 
            // btnRunMaintenance
            // 
            this.btnRunMaintenance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRunMaintenance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRunMaintenance.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnRunMaintenance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnRunMaintenance.ForeColor = System.Drawing.Color.Black;
            this.btnRunMaintenance.Location = new System.Drawing.Point(20, 260);
            this.btnRunMaintenance.Name = "btnRunMaintenance";
            this.btnRunMaintenance.Size = new System.Drawing.Size(710, 50);
            this.btnRunMaintenance.TabIndex = 1;
            this.btnRunMaintenance.Text = "EXECUTAR SELECIONADOS";
            this.btnRunMaintenance.UseVisualStyleBackColor = true;
            this.btnRunMaintenance.Click += new System.EventHandler(this.btnRunMaintenance_Click);
            // 
            // grpMaintenanceActions
            // 
            this.grpMaintenanceActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMaintenanceActions.Controls.Add(this.checkBox2);
            this.grpMaintenanceActions.Controls.Add(this.chkDisableAllTriggers);
            this.grpMaintenanceActions.Controls.Add(this.chkDisableFKAndCheck);
            this.grpMaintenanceActions.Controls.Add(this.chkPurgeRecycleBin);
            this.grpMaintenanceActions.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpMaintenanceActions.Location = new System.Drawing.Point(20, 38);
            this.grpMaintenanceActions.Name = "grpMaintenanceActions";
            this.grpMaintenanceActions.Size = new System.Drawing.Size(710, 217);
            this.grpMaintenanceActions.TabIndex = 0;
            this.grpMaintenanceActions.TabStop = false;
            this.grpMaintenanceActions.Text = "Selecione as ações a executar:";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox2.Location = new System.Drawing.Point(30, 140);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(265, 20);
            this.checkBox2.TabIndex = 5;
            this.checkBox2.Text = "Gerar Estatísticas (Gather Schema Stats)";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // chkDisableAllTriggers
            // 
            this.chkDisableAllTriggers.AutoSize = true;
            this.chkDisableAllTriggers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkDisableAllTriggers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDisableAllTriggers.Location = new System.Drawing.Point(30, 35);
            this.chkDisableAllTriggers.Name = "chkDisableAllTriggers";
            this.chkDisableAllTriggers.Size = new System.Drawing.Size(210, 20);
            this.chkDisableAllTriggers.TabIndex = 0;
            this.chkDisableAllTriggers.Text = "Desabilitar TODAS as Triggers";
            this.chkDisableAllTriggers.UseVisualStyleBackColor = true;
            // 
            // chkDisableFKAndCheck
            // 
            this.chkDisableFKAndCheck.AutoSize = true;
            this.chkDisableFKAndCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkDisableFKAndCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDisableFKAndCheck.Location = new System.Drawing.Point(30, 70);
            this.chkDisableFKAndCheck.Name = "chkDisableFKAndCheck";
            this.chkDisableFKAndCheck.Size = new System.Drawing.Size(292, 20);
            this.chkDisableFKAndCheck.TabIndex = 1;
            this.chkDisableFKAndCheck.Text = "Desabilitar Constraints (Foreign Key e Check)";
            this.chkDisableFKAndCheck.UseVisualStyleBackColor = true;
            // 
            // chkPurgeRecycleBin
            // 
            this.chkPurgeRecycleBin.AutoSize = true;
            this.chkPurgeRecycleBin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkPurgeRecycleBin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPurgeRecycleBin.Location = new System.Drawing.Point(30, 105);
            this.chkPurgeRecycleBin.Name = "chkPurgeRecycleBin";
            this.chkPurgeRecycleBin.Size = new System.Drawing.Size(252, 20);
            this.chkPurgeRecycleBin.TabIndex = 2;
            this.chkPurgeRecycleBin.Text = "Limpar Lixeira (PURGE RECYCLEBIN)";
            this.chkPurgeRecycleBin.UseVisualStyleBackColor = true;
            // 
            // chkResetSequences
            // 
            this.chkResetSequences.AutoSize = true;
            this.chkResetSequences.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkResetSequences.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkResetSequences.Location = new System.Drawing.Point(30, 140);
            this.chkResetSequences.Name = "chkResetSequences";
            this.chkResetSequences.Size = new System.Drawing.Size(334, 20);
            this.chkResetSequences.TabIndex = 4;
            this.chkResetSequences.Text = "Resetar Sequences (prc_wms_util_reset_sequence)";
            this.chkResetSequences.UseVisualStyleBackColor = true;
            // 
            // chkGatherStats
            // 
            this.chkGatherStats.AutoSize = true;
            this.chkGatherStats.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkGatherStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGatherStats.Location = new System.Drawing.Point(30, 105);
            this.chkGatherStats.Name = "chkGatherStats";
            this.chkGatherStats.Size = new System.Drawing.Size(265, 20);
            this.chkGatherStats.TabIndex = 3;
            this.chkGatherStats.Text = "Gerar Estatísticas (Gather Schema Stats)";
            this.chkGatherStats.UseVisualStyleBackColor = true;
            // 
            // rtbImportLog
            // 
            this.rtbImportLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbImportLog.BackColor = System.Drawing.Color.Black;
            this.rtbImportLog.Font = new System.Drawing.Font("Consolas", 9F);
            this.rtbImportLog.Location = new System.Drawing.Point(8, 199);
            this.rtbImportLog.Name = "rtbImportLog";
            this.rtbImportLog.ReadOnly = true;
            this.rtbImportLog.Size = new System.Drawing.Size(729, 186);
            this.rtbImportLog.TabIndex = 11;
            this.rtbImportLog.Text = "";
            // 
            // lblLog
            // 
            this.lblLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLog.AutoSize = true;
            this.lblLog.Location = new System.Drawing.Point(9, 183);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(94, 13);
            this.lblLog.TabIndex = 10;
            this.lblLog.Text = "Log de Execução:";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabConnection);
            this.tabControl.Controls.Add(this.tabMaintenance);
            this.tabControl.Controls.Add(this.tabTruncate);
            this.tabControl.Controls.Add(this.tabImport);
            this.tabControl.Controls.Add(this.tabRestore);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(758, 464);
            this.tabControl.TabIndex = 0;
            // 
            // tabConnection
            // 
            this.tabConnection.Controls.Add(this.button1);
            this.tabConnection.Controls.Add(this.btnLoadConfig);
            this.tabConnection.Controls.Add(this.btnPasteString);
            this.tabConnection.Controls.Add(this.lblConnectionStatus);
            this.tabConnection.Controls.Add(this.btnDisconnect);
            this.tabConnection.Controls.Add(this.btnConnect);
            this.tabConnection.Controls.Add(this.txtPassword);
            this.tabConnection.Controls.Add(this.lblPassword);
            this.tabConnection.Controls.Add(this.txtUserId);
            this.tabConnection.Controls.Add(this.lblUserId);
            this.tabConnection.Controls.Add(this.txtServiceName);
            this.tabConnection.Controls.Add(this.lblServiceName);
            this.tabConnection.Controls.Add(this.txtPort);
            this.tabConnection.Controls.Add(this.lblPort);
            this.tabConnection.Controls.Add(this.txtHost);
            this.tabConnection.Controls.Add(this.lblHost);
            this.tabConnection.Controls.Add(this.txtConnectionString);
            this.tabConnection.Controls.Add(this.label1);
            this.tabConnection.Location = new System.Drawing.Point(4, 22);
            this.tabConnection.Name = "tabConnection";
            this.tabConnection.Padding = new System.Windows.Forms.Padding(3);
            this.tabConnection.Size = new System.Drawing.Size(750, 438);
            this.tabConnection.TabIndex = 0;
            this.tabConnection.Text = "1 - Conexão";
            this.tabConnection.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(598, 406);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 26);
            this.button1.TabIndex = 12;
            this.button1.Text = "Buscar Atualizção";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.validatorbtn);
            // 
            // btnLoadConfig
            // 
            this.btnLoadConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadConfig.Location = new System.Drawing.Point(637, 76);
            this.btnLoadConfig.Name = "btnLoadConfig";
            this.btnLoadConfig.Size = new System.Drawing.Size(101, 35);
            this.btnLoadConfig.TabIndex = 3;
            this.btnLoadConfig.Text = "Carregar .Config";
            this.btnLoadConfig.UseVisualStyleBackColor = true;
            this.btnLoadConfig.Click += new System.EventHandler(this.btnLoadConfig_Click);
            // 
            // btnPasteString
            // 
            this.btnPasteString.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPasteString.Location = new System.Drawing.Point(637, 35);
            this.btnPasteString.Name = "btnPasteString";
            this.btnPasteString.Size = new System.Drawing.Size(101, 35);
            this.btnPasteString.TabIndex = 2;
            this.btnPasteString.Text = "Colar String";
            this.btnPasteString.UseVisualStyleBackColor = true;
            this.btnPasteString.Click += new System.EventHandler(this.btnPasteString_Click);
            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.AutoSize = true;
            this.lblConnectionStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblConnectionStatus.Location = new System.Drawing.Point(9, 199);
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Padding = new System.Windows.Forms.Padding(5);
            this.lblConnectionStatus.Size = new System.Drawing.Size(137, 25);
            this.lblConnectionStatus.TabIndex = 5;
            this.lblConnectionStatus.Text = "Status: Desconectado";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(157, 156);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(139, 26);
            this.btnDisconnect.TabIndex = 11;
            this.btnDisconnect.Text = "Desconectar";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(12, 156);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(139, 26);
            this.btnConnect.TabIndex = 10;
            this.btnConnect.Text = "Conectar";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Location = new System.Drawing.Point(496, 165);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(241, 20);
            this.txtPassword.TabIndex = 8;
            this.txtPassword.Text = "r22sp15";
            // 
            // lblPassword
            // 
            this.lblPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(496, 147);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(41, 13);
            this.lblPassword.TabIndex = 0;
            this.lblPassword.Text = "Senha:";
            // 
            // txtUserId
            // 
            this.txtUserId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserId.Location = new System.Drawing.Point(496, 113);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(241, 20);
            this.txtUserId.TabIndex = 7;
            this.txtUserId.Text = "r22sp15";
            // 
            // lblUserId
            // 
            this.lblUserId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserId.AutoSize = true;
            this.lblUserId.Location = new System.Drawing.Point(496, 95);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(46, 13);
            this.lblUserId.TabIndex = 0;
            this.lblUserId.Text = "Usuário:";
            // 
            // txtServiceName
            // 
            this.txtServiceName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServiceName.Location = new System.Drawing.Point(249, 113);
            this.txtServiceName.Name = "txtServiceName";
            this.txtServiceName.Size = new System.Drawing.Size(241, 20);
            this.txtServiceName.TabIndex = 6;
            this.txtServiceName.Text = "XE";
            this.txtServiceName.TextChanged += new System.EventHandler(this.txtServiceName_TextChanged);
            // 
            // lblServiceName
            // 
            this.lblServiceName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblServiceName.AutoSize = true;
            this.lblServiceName.Location = new System.Drawing.Point(249, 95);
            this.lblServiceName.Name = "lblServiceName";
            this.lblServiceName.Size = new System.Drawing.Size(100, 13);
            this.lblServiceName.TabIndex = 0;
            this.lblServiceName.Text = "Service Name/SID:";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(129, 113);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(112, 20);
            this.txtPort.TabIndex = 5;
            this.txtPort.Text = "1521";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(129, 95);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(35, 13);
            this.lblPort.TabIndex = 0;
            this.lblPort.Text = "Porta:";
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(9, 113);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(112, 20);
            this.txtHost.TabIndex = 4;
            this.txtHost.Text = "172.25.100.205";
            // 
            // lblHost
            // 
            this.lblHost.AutoSize = true;
            this.lblHost.Location = new System.Drawing.Point(9, 95);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(32, 13);
            this.lblHost.TabIndex = 0;
            this.lblHost.Text = "Host:";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConnectionString.Location = new System.Drawing.Point(9, 35);
            this.txtConnectionString.Multiline = true;
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(622, 35);
            this.txtConnectionString.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "String de Conexão (Montada Automaticamente):";
            // 
            // tabTruncate
            // 
            this.tabTruncate.Controls.Add(this.label4);
            this.tabTruncate.Controls.Add(this.checkedListTables);
            this.tabTruncate.Controls.Add(this.btnDeselectAllTables);
            this.tabTruncate.Controls.Add(this.btnSelectAllTables);
            this.tabTruncate.Controls.Add(this.btnRefreshTables);
            this.tabTruncate.Controls.Add(this.btnTruncate);
            this.tabTruncate.Controls.Add(this.btnSortTruncateName);
            this.tabTruncate.Controls.Add(this.btnSortTruncateRows);
            this.tabTruncate.Location = new System.Drawing.Point(4, 22);
            this.tabTruncate.Name = "tabTruncate";
            this.tabTruncate.Size = new System.Drawing.Size(750, 438);
            this.tabTruncate.TabIndex = 3;
            this.tabTruncate.Text = "3 - Truncate";
            this.tabTruncate.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(153, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Tabelas disponíveis no banco:";
            // 
            // checkedListTables
            // 
            this.checkedListTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListTables.FormattingEnabled = true;
            this.checkedListTables.Location = new System.Drawing.Point(9, 50);
            this.checkedListTables.Name = "checkedListTables";
            this.checkedListTables.Size = new System.Drawing.Size(729, 334);
            this.checkedListTables.TabIndex = 4;
            // 
            // btnDeselectAllTables
            // 
            this.btnDeselectAllTables.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeselectAllTables.Location = new System.Drawing.Point(506, 407);
            this.btnDeselectAllTables.Name = "btnDeselectAllTables";
            this.btnDeselectAllTables.Size = new System.Drawing.Size(111, 26);
            this.btnDeselectAllTables.TabIndex = 3;
            this.btnDeselectAllTables.Text = "Desmarcar Todas";
            this.btnDeselectAllTables.UseVisualStyleBackColor = true;
            this.btnDeselectAllTables.Click += new System.EventHandler(this.btnDeselectAllTables_Click);
            // 
            // btnSelectAllTables
            // 
            this.btnSelectAllTables.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectAllTables.Location = new System.Drawing.Point(386, 407);
            this.btnSelectAllTables.Name = "btnSelectAllTables";
            this.btnSelectAllTables.Size = new System.Drawing.Size(111, 26);
            this.btnSelectAllTables.TabIndex = 2;
            this.btnSelectAllTables.Text = "Selecionar Todas";
            this.btnSelectAllTables.UseVisualStyleBackColor = true;
            this.btnSelectAllTables.Click += new System.EventHandler(this.btnSelectAllTables_Click);
            // 
            // btnRefreshTables
            // 
            this.btnRefreshTables.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshTables.Location = new System.Drawing.Point(626, 407);
            this.btnRefreshTables.Name = "btnRefreshTables";
            this.btnRefreshTables.Size = new System.Drawing.Size(111, 26);
            this.btnRefreshTables.TabIndex = 1;
            this.btnRefreshTables.Text = "Atualizar Lista";
            this.btnRefreshTables.UseVisualStyleBackColor = true;
            this.btnRefreshTables.Click += new System.EventHandler(this.btnRefreshTables_Click);
            // 
            // btnTruncate
            // 
            this.btnTruncate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTruncate.Location = new System.Drawing.Point(9, 407);
            this.btnTruncate.Name = "btnTruncate";
            this.btnTruncate.Size = new System.Drawing.Size(111, 26);
            this.btnTruncate.TabIndex = 0;
            this.btnTruncate.Text = "Truncar Selecionadas";
            this.btnTruncate.UseVisualStyleBackColor = true;
            this.btnTruncate.Click += new System.EventHandler(this.btnTruncate_Click);
            // 
            // btnSortTruncateName
            // 
            this.btnSortTruncateName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSortTruncateName.Location = new System.Drawing.Point(638, 19);
            this.btnSortTruncateName.Name = "btnSortTruncateName";
            this.btnSortTruncateName.Size = new System.Drawing.Size(100, 23);
            this.btnSortTruncateName.TabIndex = 15;
            this.btnSortTruncateName.Text = "Ordenar Nome";
            this.btnSortTruncateName.UseVisualStyleBackColor = true;
            this.btnSortTruncateName.Click += new System.EventHandler(this.btnSortTruncateName_Click);
            // 
            // btnSortTruncateRows
            // 
            this.btnSortTruncateRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSortTruncateRows.Location = new System.Drawing.Point(532, 19);
            this.btnSortTruncateRows.Name = "btnSortTruncateRows";
            this.btnSortTruncateRows.Size = new System.Drawing.Size(100, 23);
            this.btnSortTruncateRows.TabIndex = 16;
            this.btnSortTruncateRows.Text = "Ordenar Linhas";
            this.btnSortTruncateRows.UseVisualStyleBackColor = true;
            this.btnSortTruncateRows.Click += new System.EventHandler(this.btnSortTruncateRows_Click);
            // 
            // tabImport
            // 
            this.tabImport.Controls.Add(this.lstImportFiles);
            this.tabImport.Controls.Add(this.btnSelectImportFiles);
            this.tabImport.Controls.Add(this.btnRunImport);
            this.tabImport.Controls.Add(this.btnClearImportList);
            this.tabImport.Controls.Add(this.label5);
            this.tabImport.Controls.Add(this.lblLog);
            this.tabImport.Controls.Add(this.rtbImportLog);
            this.tabImport.Location = new System.Drawing.Point(4, 22);
            this.tabImport.Name = "tabImport";
            this.tabImport.Size = new System.Drawing.Size(750, 438);
            this.tabImport.TabIndex = 4;
            this.tabImport.Text = "4 - Importação";
            // 
            // lstImportFiles
            // 
            this.lstImportFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstImportFiles.FormattingEnabled = true;
            this.lstImportFiles.Location = new System.Drawing.Point(9, 55);
            this.lstImportFiles.Name = "lstImportFiles";
            this.lstImportFiles.Size = new System.Drawing.Size(729, 121);
            this.lstImportFiles.TabIndex = 0;
            // 
            // btnSelectImportFiles
            // 
            this.btnSelectImportFiles.Location = new System.Drawing.Point(202, 13);
            this.btnSelectImportFiles.Name = "btnSelectImportFiles";
            this.btnSelectImportFiles.Size = new System.Drawing.Size(140, 30);
            this.btnSelectImportFiles.TabIndex = 1;
            this.btnSelectImportFiles.Text = "Selecionar Arquivos .SQL";
            this.btnSelectImportFiles.Click += new System.EventHandler(this.btnSelectImportFiles_Click);
            // 
            // btnRunImport
            // 
            this.btnRunImport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRunImport.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRunImport.Location = new System.Drawing.Point(8, 391);
            this.btnRunImport.Name = "btnRunImport";
            this.btnRunImport.Size = new System.Drawing.Size(729, 40);
            this.btnRunImport.TabIndex = 2;
            this.btnRunImport.Text = "EXECUTAR IMPORTAÇÃO NO BANCO";
            this.btnRunImport.Click += new System.EventHandler(this.btnRunImport_Click);
            // 
            // btnClearImportList
            // 
            this.btnClearImportList.Location = new System.Drawing.Point(348, 13);
            this.btnClearImportList.Name = "btnClearImportList";
            this.btnClearImportList.Size = new System.Drawing.Size(100, 30);
            this.btnClearImportList.TabIndex = 3;
            this.btnClearImportList.Text = "Limpar Lista";
            this.btnClearImportList.Click += new System.EventHandler(this.btnClearImportList_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(188, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Selecione as tabelas para Importação:";
            // 
            // tabRestore
            // 
            this.tabRestore.Controls.Add(this.lblRestoreInfo);
            this.tabRestore.Controls.Add(this.btnRunRestore);
            this.tabRestore.Controls.Add(this.grpRestoreActions);
            this.tabRestore.Location = new System.Drawing.Point(4, 22);
            this.tabRestore.Name = "tabRestore";
            this.tabRestore.Padding = new System.Windows.Forms.Padding(3);
            this.tabRestore.Size = new System.Drawing.Size(750, 438);
            this.tabRestore.TabIndex = 2;
            this.tabRestore.Text = "5 - Restaurar / Habilitar";
            this.tabRestore.UseVisualStyleBackColor = true;
            // 
            // lblRestoreInfo
            // 
            this.lblRestoreInfo.AutoSize = true;
            this.lblRestoreInfo.ForeColor = System.Drawing.Color.DimGray;
            this.lblRestoreInfo.Location = new System.Drawing.Point(20, 319);
            this.lblRestoreInfo.Name = "lblRestoreInfo";
            this.lblRestoreInfo.Size = new System.Drawing.Size(250, 13);
            this.lblRestoreInfo.TabIndex = 2;
            this.lblRestoreInfo.Text = "Use esta aba após concluir a importação de dados.";
            // 
            // btnRunRestore
            // 
            this.btnRunRestore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRunRestore.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnRunRestore.ForeColor = System.Drawing.Color.DarkGreen;
            this.btnRunRestore.Location = new System.Drawing.Point(20, 260);
            this.btnRunRestore.Name = "btnRunRestore";
            this.btnRunRestore.Size = new System.Drawing.Size(710, 50);
            this.btnRunRestore.TabIndex = 1;
            this.btnRunRestore.Text = "EXECUTAR SELECIONADOS";
            this.btnRunRestore.UseVisualStyleBackColor = true;
            this.btnRunRestore.Click += new System.EventHandler(this.btnRunRestore_Click);
            // 
            // grpRestoreActions
            // 
            this.grpRestoreActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpRestoreActions.Controls.Add(this.chkEnableAllTriggers);
            this.grpRestoreActions.Controls.Add(this.chkEnableFKAndCheck);
            this.grpRestoreActions.Controls.Add(this.chkResetSequences);
            this.grpRestoreActions.Controls.Add(this.chkGatherStats);
            this.grpRestoreActions.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpRestoreActions.Location = new System.Drawing.Point(20, 38);
            this.grpRestoreActions.Name = "grpRestoreActions";
            this.grpRestoreActions.Size = new System.Drawing.Size(710, 215);
            this.grpRestoreActions.TabIndex = 0;
            this.grpRestoreActions.TabStop = false;
            this.grpRestoreActions.Text = "Selecione o que deseja reativar:";
            // 
            // chkEnableAllTriggers
            // 
            this.chkEnableAllTriggers.AutoSize = true;
            this.chkEnableAllTriggers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkEnableAllTriggers.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableAllTriggers.Location = new System.Drawing.Point(30, 35);
            this.chkEnableAllTriggers.Name = "chkEnableAllTriggers";
            this.chkEnableAllTriggers.Size = new System.Drawing.Size(186, 21);
            this.chkEnableAllTriggers.TabIndex = 0;
            this.chkEnableAllTriggers.Text = "Habilitar TODAS as Triggers";
            this.chkEnableAllTriggers.UseVisualStyleBackColor = true;
            // 
            // chkEnableFKAndCheck
            // 
            this.chkEnableFKAndCheck.AutoSize = true;
            this.chkEnableFKAndCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkEnableFKAndCheck.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableFKAndCheck.Location = new System.Drawing.Point(30, 70);
            this.chkEnableFKAndCheck.Name = "chkEnableFKAndCheck";
            this.chkEnableFKAndCheck.Size = new System.Drawing.Size(272, 21);
            this.chkEnableFKAndCheck.TabIndex = 1;
            this.chkEnableFKAndCheck.Text = "Habilitar Constraints (Foreign Key e Check)";
            this.chkEnableFKAndCheck.UseVisualStyleBackColor = true;
            // 
            // tabTriggers
            // 
            this.tabTriggers.Location = new System.Drawing.Point(0, 0);
            this.tabTriggers.Name = "tabTriggers";
            this.tabTriggers.Size = new System.Drawing.Size(200, 100);
            this.tabTriggers.TabIndex = 0;
            // 
            // btnDisableAllTriggers
            // 
            this.btnDisableAllTriggers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDisableAllTriggers.Location = new System.Drawing.Point(249, 407);
            this.btnDisableAllTriggers.Name = "btnDisableAllTriggers";
            this.btnDisableAllTriggers.Size = new System.Drawing.Size(111, 26);
            this.btnDisableAllTriggers.TabIndex = 7;
            this.btnDisableAllTriggers.Text = "Desabilitar Todas";
            this.btnDisableAllTriggers.UseVisualStyleBackColor = true;
            this.btnDisableAllTriggers.Click += new System.EventHandler(this.btnDisableAllTriggers_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Triggers disponíveis no banco:";
            // 
            // checkedListTriggers
            // 
            this.checkedListTriggers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListTriggers.FormattingEnabled = true;
            this.checkedListTriggers.Location = new System.Drawing.Point(9, 35);
            this.checkedListTriggers.Name = "checkedListTriggers";
            this.checkedListTriggers.Size = new System.Drawing.Size(729, 349);
            this.checkedListTriggers.TabIndex = 5;
            // 
            // btnDeselectAllTriggers
            // 
            this.btnDeselectAllTriggers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeselectAllTriggers.Location = new System.Drawing.Point(506, 407);
            this.btnDeselectAllTriggers.Name = "btnDeselectAllTriggers";
            this.btnDeselectAllTriggers.Size = new System.Drawing.Size(111, 26);
            this.btnDeselectAllTriggers.TabIndex = 4;
            this.btnDeselectAllTriggers.Text = "Desmarcar Todas";
            this.btnDeselectAllTriggers.UseVisualStyleBackColor = true;
            this.btnDeselectAllTriggers.Click += new System.EventHandler(this.btnDeselectAllTriggers_Click);
            // 
            // btnSelectAllTriggers
            // 
            this.btnSelectAllTriggers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectAllTriggers.Location = new System.Drawing.Point(386, 407);
            this.btnSelectAllTriggers.Name = "btnSelectAllTriggers";
            this.btnSelectAllTriggers.Size = new System.Drawing.Size(111, 26);
            this.btnSelectAllTriggers.TabIndex = 3;
            this.btnSelectAllTriggers.Text = "Selecionar Todas";
            this.btnSelectAllTriggers.UseVisualStyleBackColor = true;
            this.btnSelectAllTriggers.Click += new System.EventHandler(this.btnSelectAllTriggers_Click);
            // 
            // btnRefreshTriggers
            // 
            this.btnRefreshTriggers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshTriggers.Location = new System.Drawing.Point(626, 407);
            this.btnRefreshTriggers.Name = "btnRefreshTriggers";
            this.btnRefreshTriggers.Size = new System.Drawing.Size(111, 26);
            this.btnRefreshTriggers.TabIndex = 2;
            this.btnRefreshTriggers.Text = "Atualizar Lista";
            this.btnRefreshTriggers.UseVisualStyleBackColor = true;
            this.btnRefreshTriggers.Click += new System.EventHandler(this.btnRefreshTriggers_Click);
            // 
            // btnDisableTriggers
            // 
            this.btnDisableTriggers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDisableTriggers.Location = new System.Drawing.Point(9, 407);
            this.btnDisableTriggers.Name = "btnDisableTriggers";
            this.btnDisableTriggers.Size = new System.Drawing.Size(111, 26);
            this.btnDisableTriggers.TabIndex = 1;
            this.btnDisableTriggers.Text = "Desabilitar Selecionadas";
            this.btnDisableTriggers.UseVisualStyleBackColor = true;
            this.btnDisableTriggers.Click += new System.EventHandler(this.btnDisableTriggers_Click);
            // 
            // btnEnableTriggers
            // 
            this.btnEnableTriggers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEnableTriggers.Location = new System.Drawing.Point(129, 407);
            this.btnEnableTriggers.Name = "btnEnableTriggers";
            this.btnEnableTriggers.Size = new System.Drawing.Size(111, 26);
            this.btnEnableTriggers.TabIndex = 0;
            this.btnEnableTriggers.Text = "Habilitar Selecionadas";
            this.btnEnableTriggers.UseVisualStyleBackColor = true;
            this.btnEnableTriggers.Click += new System.EventHandler(this.btnEnableTriggers_Click);
            // 
            // tabConstraints
            // 
            this.tabConstraints.Controls.Add(this.btnDisableFKandCheck);
            this.tabConstraints.Controls.Add(this.label3);
            this.tabConstraints.Controls.Add(this.checkedListConstraints);
            this.tabConstraints.Controls.Add(this.btnDeselectAllConstraints);
            this.tabConstraints.Controls.Add(this.btnSelectAllConstraints);
            this.tabConstraints.Controls.Add(this.btnRefreshConstraints);
            this.tabConstraints.Controls.Add(this.btnDisableConstraints);
            this.tabConstraints.Controls.Add(this.btnEnableConstraints);
            this.tabConstraints.Location = new System.Drawing.Point(4, 22);
            this.tabConstraints.Name = "tabConstraints";
            this.tabConstraints.Size = new System.Drawing.Size(750, 438);
            this.tabConstraints.TabIndex = 2;
            this.tabConstraints.Text = "3 - Constraints";
            this.tabConstraints.UseVisualStyleBackColor = true;
            // 
            // btnDisableFKandCheck
            // 
            this.btnDisableFKandCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDisableFKandCheck.Location = new System.Drawing.Point(233, 407);
            this.btnDisableFKandCheck.Name = "btnDisableFKandCheck";
            this.btnDisableFKandCheck.Size = new System.Drawing.Size(146, 26);
            this.btnDisableFKandCheck.TabIndex = 6;
            this.btnDisableFKandCheck.Text = "Desabilitar FK´s e Check´s";
            this.btnDisableFKandCheck.UseVisualStyleBackColor = true;
            this.btnDisableFKandCheck.Click += new System.EventHandler(this.btnDisableFKandCheck_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Constraints disponíveis no banco:";
            // 
            // checkedListConstraints
            // 
            this.checkedListConstraints.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListConstraints.FormattingEnabled = true;
            this.checkedListConstraints.Location = new System.Drawing.Point(9, 35);
            this.checkedListConstraints.Name = "checkedListConstraints";
            this.checkedListConstraints.Size = new System.Drawing.Size(729, 349);
            this.checkedListConstraints.TabIndex = 5;
            // 
            // btnDeselectAllConstraints
            // 
            this.btnDeselectAllConstraints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeselectAllConstraints.Location = new System.Drawing.Point(506, 407);
            this.btnDeselectAllConstraints.Name = "btnDeselectAllConstraints";
            this.btnDeselectAllConstraints.Size = new System.Drawing.Size(111, 26);
            this.btnDeselectAllConstraints.TabIndex = 4;
            this.btnDeselectAllConstraints.Text = "Desmarcar Todas";
            this.btnDeselectAllConstraints.UseVisualStyleBackColor = true;
            this.btnDeselectAllConstraints.Click += new System.EventHandler(this.btnDeselectAllConstraints_Click);
            // 
            // btnSelectAllConstraints
            // 
            this.btnSelectAllConstraints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectAllConstraints.Location = new System.Drawing.Point(386, 407);
            this.btnSelectAllConstraints.Name = "btnSelectAllConstraints";
            this.btnSelectAllConstraints.Size = new System.Drawing.Size(111, 26);
            this.btnSelectAllConstraints.TabIndex = 3;
            this.btnSelectAllConstraints.Text = "Selecionar Todas";
            this.btnSelectAllConstraints.UseVisualStyleBackColor = true;
            this.btnSelectAllConstraints.Click += new System.EventHandler(this.btnSelectAllConstraints_Click);
            // 
            // btnRefreshConstraints
            // 
            this.btnRefreshConstraints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshConstraints.Location = new System.Drawing.Point(626, 407);
            this.btnRefreshConstraints.Name = "btnRefreshConstraints";
            this.btnRefreshConstraints.Size = new System.Drawing.Size(111, 26);
            this.btnRefreshConstraints.TabIndex = 2;
            this.btnRefreshConstraints.Text = "Atualizar Lista";
            this.btnRefreshConstraints.UseVisualStyleBackColor = true;
            this.btnRefreshConstraints.Click += new System.EventHandler(this.btnRefreshConstraints_Click);
            // 
            // btnDisableConstraints
            // 
            this.btnDisableConstraints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDisableConstraints.Location = new System.Drawing.Point(9, 407);
            this.btnDisableConstraints.Name = "btnDisableConstraints";
            this.btnDisableConstraints.Size = new System.Drawing.Size(111, 26);
            this.btnDisableConstraints.TabIndex = 1;
            this.btnDisableConstraints.Text = "Desabilitar Selecionadas";
            this.btnDisableConstraints.UseVisualStyleBackColor = true;
            this.btnDisableConstraints.Click += new System.EventHandler(this.btnDisableConstraints_Click);
            // 
            // btnEnableConstraints
            // 
            this.btnEnableConstraints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEnableConstraints.Location = new System.Drawing.Point(129, 407);
            this.btnEnableConstraints.Name = "btnEnableConstraints";
            this.btnEnableConstraints.Size = new System.Drawing.Size(98, 26);
            this.btnEnableConstraints.TabIndex = 0;
            this.btnEnableConstraints.Text = "Habilitar Selecionadas";
            this.btnEnableConstraints.UseVisualStyleBackColor = true;
            this.btnEnableConstraints.Click += new System.EventHandler(this.btnEnableConstraints_Click);
            // 
            // tabEnableConstraints
            // 
            this.tabEnableConstraints.Controls.Add(this.pnlConstraintFilter);
            this.tabEnableConstraints.Controls.Add(this.label6);
            this.tabEnableConstraints.Controls.Add(this.checkedListEnableConstraints);
            this.tabEnableConstraints.Controls.Add(this.btnDeselectAllEnableConstraints);
            this.tabEnableConstraints.Controls.Add(this.btnSelectAllEnableConstraints);
            this.tabEnableConstraints.Controls.Add(this.btnRefreshEnableConstraints);
            this.tabEnableConstraints.Controls.Add(this.btnEnableSelectedConstraints);
            this.tabEnableConstraints.Location = new System.Drawing.Point(4, 22);
            this.tabEnableConstraints.Name = "tabEnableConstraints";
            this.tabEnableConstraints.Padding = new System.Windows.Forms.Padding(3);
            this.tabEnableConstraints.Size = new System.Drawing.Size(750, 438);
            this.tabEnableConstraints.TabIndex = 5;
            this.tabEnableConstraints.Text = "6 - Habilitar Constraints";
            this.tabEnableConstraints.UseVisualStyleBackColor = true;
            // 
            // pnlConstraintFilter
            // 
            this.pnlConstraintFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlConstraintFilter.Controls.Add(this.lblFilter);
            this.pnlConstraintFilter.Controls.Add(this.chkForeign);
            this.pnlConstraintFilter.Controls.Add(this.chkPrimary);
            this.pnlConstraintFilter.Controls.Add(this.chkUnique);
            this.pnlConstraintFilter.Controls.Add(this.chkCheck);
            this.pnlConstraintFilter.Location = new System.Drawing.Point(6, 22);
            this.pnlConstraintFilter.Name = "pnlConstraintFilter";
            this.pnlConstraintFilter.Size = new System.Drawing.Size(738, 30);
            this.pnlConstraintFilter.TabIndex = 14;
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(3, 8);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(32, 13);
            this.lblFilter.TabIndex = 4;
            this.lblFilter.Text = "Filtro:";
            // 
            // chkForeign
            // 
            this.chkForeign.AutoSize = true;
            this.chkForeign.Checked = true;
            this.chkForeign.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkForeign.Location = new System.Drawing.Point(41, 7);
            this.chkForeign.Name = "chkForeign";
            this.chkForeign.Size = new System.Drawing.Size(99, 17);
            this.chkForeign.TabIndex = 0;
            this.chkForeign.Text = "Foreign Key (R)";
            this.chkForeign.UseVisualStyleBackColor = true;
            this.chkForeign.CheckedChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
            // 
            // chkPrimary
            // 
            this.chkPrimary.AutoSize = true;
            this.chkPrimary.Checked = true;
            this.chkPrimary.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPrimary.Location = new System.Drawing.Point(149, 7);
            this.chkPrimary.Name = "chkPrimary";
            this.chkPrimary.Size = new System.Drawing.Size(97, 17);
            this.chkPrimary.TabIndex = 1;
            this.chkPrimary.Text = "Primary Key (P)";
            this.chkPrimary.UseVisualStyleBackColor = true;
            this.chkPrimary.CheckedChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
            // 
            // chkUnique
            // 
            this.chkUnique.AutoSize = true;
            this.chkUnique.Checked = true;
            this.chkUnique.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUnique.Location = new System.Drawing.Point(249, 7);
            this.chkUnique.Name = "chkUnique";
            this.chkUnique.Size = new System.Drawing.Size(77, 17);
            this.chkUnique.TabIndex = 2;
            this.chkUnique.Text = "Unique (U)";
            this.chkUnique.UseVisualStyleBackColor = true;
            this.chkUnique.CheckedChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
            // 
            // chkCheck
            // 
            this.chkCheck.AutoSize = true;
            this.chkCheck.Checked = true;
            this.chkCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCheck.Location = new System.Drawing.Point(326, 7);
            this.chkCheck.Name = "chkCheck";
            this.chkCheck.Size = new System.Drawing.Size(73, 17);
            this.chkCheck.TabIndex = 3;
            this.chkCheck.Text = "Check (C)";
            this.chkCheck.UseVisualStyleBackColor = true;
            this.chkCheck.CheckedChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Constraints Desabilitadas:";
            // 
            // checkedListEnableConstraints
            // 
            this.checkedListEnableConstraints.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListEnableConstraints.FormattingEnabled = true;
            this.checkedListEnableConstraints.Location = new System.Drawing.Point(6, 58);
            this.checkedListEnableConstraints.Name = "checkedListEnableConstraints";
            this.checkedListEnableConstraints.Size = new System.Drawing.Size(738, 319);
            this.checkedListEnableConstraints.TabIndex = 12;
            // 
            // btnDeselectAllEnableConstraints
            // 
            this.btnDeselectAllEnableConstraints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeselectAllEnableConstraints.Location = new System.Drawing.Point(129, 399);
            this.btnDeselectAllEnableConstraints.Name = "btnDeselectAllEnableConstraints";
            this.btnDeselectAllEnableConstraints.Size = new System.Drawing.Size(111, 26);
            this.btnDeselectAllEnableConstraints.TabIndex = 11;
            this.btnDeselectAllEnableConstraints.Text = "Desmarcar Todos";
            this.btnDeselectAllEnableConstraints.UseVisualStyleBackColor = true;
            this.btnDeselectAllEnableConstraints.Click += new System.EventHandler(this.btnDeselectAllEnableConstraints_Click);
            // 
            // btnSelectAllEnableConstraints
            // 
            this.btnSelectAllEnableConstraints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectAllEnableConstraints.Location = new System.Drawing.Point(9, 399);
            this.btnSelectAllEnableConstraints.Name = "btnSelectAllEnableConstraints";
            this.btnSelectAllEnableConstraints.Size = new System.Drawing.Size(111, 26);
            this.btnSelectAllEnableConstraints.TabIndex = 10;
            this.btnSelectAllEnableConstraints.Text = "Marcar Todos";
            this.btnSelectAllEnableConstraints.UseVisualStyleBackColor = true;
            this.btnSelectAllEnableConstraints.Click += new System.EventHandler(this.btnSelectAllEnableConstraints_Click);
            // 
            // btnRefreshEnableConstraints
            // 
            this.btnRefreshEnableConstraints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshEnableConstraints.Location = new System.Drawing.Point(487, 399);
            this.btnRefreshEnableConstraints.Name = "btnRefreshEnableConstraints";
            this.btnRefreshEnableConstraints.Size = new System.Drawing.Size(111, 26);
            this.btnRefreshEnableConstraints.TabIndex = 9;
            this.btnRefreshEnableConstraints.Text = "Atualizar Lista";
            this.btnRefreshEnableConstraints.UseVisualStyleBackColor = true;
            this.btnRefreshEnableConstraints.Click += new System.EventHandler(this.btnRefreshEnableConstraints_Click);
            // 
            // btnEnableSelectedConstraints
            // 
            this.btnEnableSelectedConstraints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnableSelectedConstraints.Location = new System.Drawing.Point(604, 399);
            this.btnEnableSelectedConstraints.Name = "btnEnableSelectedConstraints";
            this.btnEnableSelectedConstraints.Size = new System.Drawing.Size(138, 26);
            this.btnEnableSelectedConstraints.TabIndex = 8;
            this.btnEnableSelectedConstraints.Text = "Habilitar Selecionados";
            this.btnEnableSelectedConstraints.UseVisualStyleBackColor = true;
            this.btnEnableSelectedConstraints.Click += new System.EventHandler(this.btnEnableSelectedConstraints_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 464);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStrip.Size = new System.Drawing.Size(758, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(43, 17);
            this.toolStripStatusLabel.Text = "Pronto";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 486);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQL Import Full";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabMaintenance.ResumeLayout(false);
            this.grpMaintenanceActions.ResumeLayout(false);
            this.grpMaintenanceActions.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabConnection.ResumeLayout(false);
            this.tabConnection.PerformLayout();
            this.tabTruncate.ResumeLayout(false);
            this.tabTruncate.PerformLayout();
            this.tabImport.ResumeLayout(false);
            this.tabImport.PerformLayout();
            this.tabRestore.ResumeLayout(false);
            this.tabRestore.PerformLayout();
            this.grpRestoreActions.ResumeLayout(false);
            this.grpRestoreActions.PerformLayout();
            this.tabConstraints.ResumeLayout(false);
            this.tabConstraints.PerformLayout();
            this.tabEnableConstraints.ResumeLayout(false);
            this.tabEnableConstraints.PerformLayout();
            this.pnlConstraintFilter.ResumeLayout(false);
            this.pnlConstraintFilter.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabConnection;
        private System.Windows.Forms.TabPage tabTriggers;
        private System.Windows.Forms.TabPage tabConstraints;
        private System.Windows.Forms.TabPage tabTruncate;
        private System.Windows.Forms.TabPage tabImport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Label lblConnectionStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox checkedListTriggers;
        private System.Windows.Forms.Button btnDeselectAllTriggers;
        private System.Windows.Forms.Button btnSelectAllTriggers;
        private System.Windows.Forms.Button btnRefreshTriggers;
        private System.Windows.Forms.Button btnDisableTriggers;
        private System.Windows.Forms.Button btnEnableTriggers;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckedListBox checkedListConstraints;
        private System.Windows.Forms.Button btnDeselectAllConstraints;
        private System.Windows.Forms.Button btnSelectAllConstraints;
        private System.Windows.Forms.Button btnRefreshConstraints;
        private System.Windows.Forms.Button btnDisableConstraints;
        private System.Windows.Forms.Button btnEnableConstraints;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSortTruncateName;
        private System.Windows.Forms.Button btnSortTruncateRows;
        private System.Windows.Forms.CheckedListBox checkedListTables;
        private System.Windows.Forms.Button btnDeselectAllTables;
        private System.Windows.Forms.Button btnSelectAllTables;
        private System.Windows.Forms.Button btnRefreshTables;
        private System.Windows.Forms.Button btnTruncate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabEnableConstraints;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckedListBox checkedListEnableConstraints;
        private System.Windows.Forms.Button btnDeselectAllEnableConstraints;
        private System.Windows.Forms.Button btnSelectAllEnableConstraints;
        private System.Windows.Forms.Button btnRefreshEnableConstraints;
        private System.Windows.Forms.Button btnEnableSelectedConstraints;
        private System.Windows.Forms.Panel pnlConstraintFilter;
        private System.Windows.Forms.CheckBox chkCheck;
        private System.Windows.Forms.CheckBox chkUnique;
        private System.Windows.Forms.CheckBox chkPrimary;
        private System.Windows.Forms.CheckBox chkForeign;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Label lblUserId;
        private System.Windows.Forms.TextBox txtServiceName;
        private System.Windows.Forms.Label lblServiceName;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.Button btnPasteString;
        private System.Windows.Forms.Button btnDisableAllTriggers;
        private System.Windows.Forms.Button btnDisableFKandCheck;
        private System.Windows.Forms.Button btnLoadConfig;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.ListBox lstImportFiles;
        private System.Windows.Forms.Button btnSelectImportFiles;
        private System.Windows.Forms.Button btnRunImport;
        private System.Windows.Forms.Button btnClearImportList;
        private System.Windows.Forms.RichTextBox rtbImportLog; 
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.TabPage tabMaintenance;
        private System.Windows.Forms.GroupBox grpMaintenanceActions;
        private System.Windows.Forms.CheckBox chkDisableAllTriggers;
        private System.Windows.Forms.CheckBox chkDisableFKAndCheck;
        private System.Windows.Forms.CheckBox chkPurgeRecycleBin;
        private System.Windows.Forms.CheckBox chkGatherStats;
        //private System.Windows.Forms.CheckBox chkGatherStats2;
        private System.Windows.Forms.Button btnRunMaintenance;
        private System.Windows.Forms.TabPage tabRestore;
        private System.Windows.Forms.GroupBox grpRestoreActions;
        private System.Windows.Forms.CheckBox chkEnableAllTriggers;
        private System.Windows.Forms.CheckBox chkEnableFKAndCheck;
        private System.Windows.Forms.Button btnRunRestore;
        private System.Windows.Forms.Label lblRestoreInfo;
        private System.Windows.Forms.CheckBox chkResetSequences;
        //private System.Windows.Forms.CheckBox chkResetSequences2;
        private Button button1;
        private CheckBox checkBox2;
    }
}