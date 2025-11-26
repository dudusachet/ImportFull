namespace PLSQLMigrationTool.Forms
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabConnection = new System.Windows.Forms.TabPage();
            this.btnPasteString = new System.Windows.Forms.Button();
            this.lblConnectionStatus = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnTestConnection = new System.Windows.Forms.Button();
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
            this.tabTriggers = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.checkedListTriggers = new System.Windows.Forms.CheckedListBox();
            this.btnDeselectAllTriggers = new System.Windows.Forms.Button();
            this.btnSelectAllTriggers = new System.Windows.Forms.Button();
            this.btnRefreshTriggers = new System.Windows.Forms.Button();
            this.btnDisableTriggers = new System.Windows.Forms.Button();
            this.btnEnableTriggers = new System.Windows.Forms.Button();
            this.tabConstraints = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.checkedListConstraints = new System.Windows.Forms.CheckedListBox();
            this.btnDeselectAllConstraints = new System.Windows.Forms.Button();
            this.btnSelectAllConstraints = new System.Windows.Forms.Button();
            this.btnRefreshConstraints = new System.Windows.Forms.Button();
            this.btnDisableConstraints = new System.Windows.Forms.Button();
            this.btnEnableConstraints = new System.Windows.Forms.Button();
            this.tabTruncate = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.checkedListTables = new System.Windows.Forms.CheckedListBox();
            this.btnDeselectAllTables = new System.Windows.Forms.Button();
            this.btnSelectAllTables = new System.Windows.Forms.Button();
            this.btnRefreshTables = new System.Windows.Forms.Button();
            this.btnTruncate = new System.Windows.Forms.Button();
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
            this.tabExport = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.checkedListExportTables = new System.Windows.Forms.CheckedListBox();
            this.chkIncludeConstraints = new System.Windows.Forms.CheckBox();
            this.chkIncludeForeignKeys = new System.Windows.Forms.CheckBox();
            this.btnDeselectAllExportTables = new System.Windows.Forms.Button();
            this.btnSelectAllExportTables = new System.Windows.Forms.Button();
            this.btnRefreshExportTables = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl.SuspendLayout();
            this.tabConnection.SuspendLayout();
            this.tabTriggers.SuspendLayout();
            this.tabConstraints.SuspendLayout();
            this.tabTruncate.SuspendLayout();
            this.tabEnableConstraints.SuspendLayout();
            this.pnlConstraintFilter.SuspendLayout();
            this.tabExport.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabConnection);
            this.tabControl.Controls.Add(this.tabTriggers);
            this.tabControl.Controls.Add(this.tabConstraints);
            this.tabControl.Controls.Add(this.tabTruncate);
            this.tabControl.Controls.Add(this.tabEnableConstraints);
            this.tabControl.Controls.Add(this.tabExport);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(758, 464);
            this.tabControl.TabIndex = 0;
            // 
            // tabConnection
            // 
            this.tabConnection.Controls.Add(this.btnPasteString);
            this.tabConnection.Controls.Add(this.lblConnectionStatus);
            this.tabConnection.Controls.Add(this.btnDisconnect);
            this.tabConnection.Controls.Add(this.btnConnect);
            this.tabConnection.Controls.Add(this.btnTestConnection);
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
            this.lblConnectionStatus.Size = new System.Drawing.Size(127, 15);
            this.lblConnectionStatus.TabIndex = 5;
            this.lblConnectionStatus.Text = "Status: Desconectado";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(249, 156);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(111, 26);
            this.btnDisconnect.TabIndex = 11;
            this.btnDisconnect.Text = "Desconectar";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(129, 156);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(111, 26);
            this.btnConnect.TabIndex = 10;
            this.btnConnect.Text = "Conectar";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(9, 156);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(111, 26);
            this.btnTestConnection.TabIndex = 9;
            this.btnTestConnection.Text = "Testar Conexão";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
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
            // tabTriggers
            // 
            this.tabTriggers.Controls.Add(this.label2);
            this.tabTriggers.Controls.Add(this.checkedListTriggers);
            this.tabTriggers.Controls.Add(this.btnDeselectAllTriggers);
            this.tabTriggers.Controls.Add(this.btnSelectAllTriggers);
            this.tabTriggers.Controls.Add(this.btnRefreshTriggers);
            this.tabTriggers.Controls.Add(this.btnDisableTriggers);
            this.tabTriggers.Controls.Add(this.btnEnableTriggers);
            this.tabTriggers.Location = new System.Drawing.Point(4, 22);
            this.tabTriggers.Name = "tabTriggers";
            this.tabTriggers.Padding = new System.Windows.Forms.Padding(3);
            this.tabTriggers.Size = new System.Drawing.Size(750, 438);
            this.tabTriggers.TabIndex = 1;
            this.tabTriggers.Text = "2 - Triggers";
            this.tabTriggers.UseVisualStyleBackColor = true;
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
            this.btnEnableConstraints.Size = new System.Drawing.Size(111, 26);
            this.btnEnableConstraints.TabIndex = 0;
            this.btnEnableConstraints.Text = "Habilitar Selecionadas";
            this.btnEnableConstraints.UseVisualStyleBackColor = true;
            this.btnEnableConstraints.Click += new System.EventHandler(this.btnEnableConstraints_Click);
            // 
            // tabTruncate
            // 
            this.tabTruncate.Controls.Add(this.label4);
            this.tabTruncate.Controls.Add(this.checkedListTables);
            this.tabTruncate.Controls.Add(this.btnDeselectAllTables);
            this.tabTruncate.Controls.Add(this.btnSelectAllTables);
            this.tabTruncate.Controls.Add(this.btnRefreshTables);
            this.tabTruncate.Controls.Add(this.btnTruncate);
            this.tabTruncate.Location = new System.Drawing.Point(4, 22);
            this.tabTruncate.Name = "tabTruncate";
            this.tabTruncate.Size = new System.Drawing.Size(750, 438);
            this.tabTruncate.TabIndex = 3;
            this.tabTruncate.Text = "4 - Truncate";
            this.tabTruncate.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 13);
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
            this.checkedListTables.Location = new System.Drawing.Point(9, 35);
            this.checkedListTables.Name = "checkedListTables";
            this.checkedListTables.Size = new System.Drawing.Size(729, 349);
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
            this.tabEnableConstraints.Text = "5 - Habilitar Constraints";
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
            // tabExport
            // 
            this.tabExport.Controls.Add(this.label5);
            this.tabExport.Controls.Add(this.checkedListExportTables);
            this.tabExport.Controls.Add(this.chkIncludeConstraints);
            this.tabExport.Controls.Add(this.chkIncludeForeignKeys);
            this.tabExport.Controls.Add(this.btnDeselectAllExportTables);
            this.tabExport.Controls.Add(this.btnSelectAllExportTables);
            this.tabExport.Controls.Add(this.btnRefreshExportTables);
            this.tabExport.Controls.Add(this.btnExport);
            this.tabExport.Location = new System.Drawing.Point(4, 22);
            this.tabExport.Name = "tabExport";
            this.tabExport.Size = new System.Drawing.Size(750, 438);
            this.tabExport.TabIndex = 4;
            this.tabExport.Text = "6 - Importação*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(198, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Selecione as tabelas para exportar DDL:";
            // 
            // checkedListExportTables
            // 
            this.checkedListExportTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListExportTables.FormattingEnabled = true;
            this.checkedListExportTables.Location = new System.Drawing.Point(9, 35);
            this.checkedListExportTables.Name = "checkedListExportTables";
            this.checkedListExportTables.Size = new System.Drawing.Size(729, 349);
            this.checkedListExportTables.TabIndex = 6;
            // 
            // chkIncludeConstraints
            // 
            this.chkIncludeConstraints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkIncludeConstraints.AutoSize = true;
            this.chkIncludeConstraints.Checked = true;
            this.chkIncludeConstraints.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeConstraints.Location = new System.Drawing.Point(137, 413);
            this.chkIncludeConstraints.Name = "chkIncludeConstraints";
            this.chkIncludeConstraints.Size = new System.Drawing.Size(109, 17);
            this.chkIncludeConstraints.TabIndex = 5;
            this.chkIncludeConstraints.Text = "Incluir Constraints";
            this.chkIncludeConstraints.UseVisualStyleBackColor = true;
            // 
            // chkIncludeForeignKeys
            // 
            this.chkIncludeForeignKeys.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkIncludeForeignKeys.AutoSize = true;
            this.chkIncludeForeignKeys.Checked = true;
            this.chkIncludeForeignKeys.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeForeignKeys.Location = new System.Drawing.Point(257, 413);
            this.chkIncludeForeignKeys.Name = "chkIncludeForeignKeys";
            this.chkIncludeForeignKeys.Size = new System.Drawing.Size(118, 17);
            this.chkIncludeForeignKeys.TabIndex = 4;
            this.chkIncludeForeignKeys.Text = "Incluir Foreign Keys";
            this.chkIncludeForeignKeys.UseVisualStyleBackColor = true;
            // 
            // btnDeselectAllExportTables
            // 
            this.btnDeselectAllExportTables.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeselectAllExportTables.Location = new System.Drawing.Point(506, 407);
            this.btnDeselectAllExportTables.Name = "btnDeselectAllExportTables";
            this.btnDeselectAllExportTables.Size = new System.Drawing.Size(111, 26);
            this.btnDeselectAllExportTables.TabIndex = 3;
            this.btnDeselectAllExportTables.Text = "Desmarcar Todas";
            this.btnDeselectAllExportTables.UseVisualStyleBackColor = true;
            this.btnDeselectAllExportTables.Click += new System.EventHandler(this.btnDeselectAllExportTables_Click);
            // 
            // btnSelectAllExportTables
            // 
            this.btnSelectAllExportTables.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectAllExportTables.Location = new System.Drawing.Point(386, 407);
            this.btnSelectAllExportTables.Name = "btnSelectAllExportTables";
            this.btnSelectAllExportTables.Size = new System.Drawing.Size(111, 26);
            this.btnSelectAllExportTables.TabIndex = 2;
            this.btnSelectAllExportTables.Text = "Selecionar Todas";
            this.btnSelectAllExportTables.UseVisualStyleBackColor = true;
            this.btnSelectAllExportTables.Click += new System.EventHandler(this.btnSelectAllExportTables_Click);
            // 
            // btnRefreshExportTables
            // 
            this.btnRefreshExportTables.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshExportTables.Location = new System.Drawing.Point(626, 407);
            this.btnRefreshExportTables.Name = "btnRefreshExportTables";
            this.btnRefreshExportTables.Size = new System.Drawing.Size(111, 26);
            this.btnRefreshExportTables.TabIndex = 1;
            this.btnRefreshExportTables.Text = "Atualizar Lista";
            this.btnRefreshExportTables.UseVisualStyleBackColor = true;
            this.btnRefreshExportTables.Click += new System.EventHandler(this.btnRefreshExportTables_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(9, 407);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(111, 26);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "Exportar DDL";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
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
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PLSQL Migration Tool - Ferramenta de Migração Oracle";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.tabControl.ResumeLayout(false);
            this.tabConnection.ResumeLayout(false);
            this.tabConnection.PerformLayout();
            this.tabTriggers.ResumeLayout(false);
            this.tabTriggers.PerformLayout();
            this.tabConstraints.ResumeLayout(false);
            this.tabConstraints.PerformLayout();
            this.tabTruncate.ResumeLayout(false);
            this.tabTruncate.PerformLayout();
            this.tabEnableConstraints.ResumeLayout(false);
            this.tabEnableConstraints.PerformLayout();
            this.pnlConstraintFilter.ResumeLayout(false);
            this.pnlConstraintFilter.PerformLayout();
            this.tabExport.ResumeLayout(false);
            this.tabExport.PerformLayout();
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
        private System.Windows.Forms.TabPage tabExport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Button btnTestConnection;
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
        private System.Windows.Forms.CheckedListBox checkedListTables;
        private System.Windows.Forms.Button btnDeselectAllTables;
        private System.Windows.Forms.Button btnSelectAllTables;
        private System.Windows.Forms.Button btnRefreshTables;
        private System.Windows.Forms.Button btnTruncate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckedListBox checkedListExportTables;
        private System.Windows.Forms.CheckBox chkIncludeConstraints;
        private System.Windows.Forms.CheckBox chkIncludeForeignKeys;
        private System.Windows.Forms.Button btnDeselectAllExportTables;
        private System.Windows.Forms.Button btnSelectAllExportTables;
        private System.Windows.Forms.Button btnRefreshExportTables;
        private System.Windows.Forms.Button btnExport;
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
        private System.Windows.Forms.Button btnPasteString; // DECLARAÇÃO ADICIONADA
        private System.Windows.Forms.TextBox txtPassword;
    }
}