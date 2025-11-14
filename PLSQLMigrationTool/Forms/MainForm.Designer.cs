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
            this.tabControl.Controls.Add(this.tabExport);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(884, 537);
            this.tabControl.TabIndex = 0;
            // 
            // tabConnection
            // 
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
            this.tabConnection.Location = new System.Drawing.Point(4, 24);
            this.tabConnection.Name = "tabConnection";
            this.tabConnection.Padding = new System.Windows.Forms.Padding(3);
            this.tabConnection.Size = new System.Drawing.Size(876, 509);
            this.tabConnection.TabIndex = 0;
            this.tabConnection.Text = "Conexão";
            this.tabConnection.UseVisualStyleBackColor = true;
            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.AutoSize = true;
            this.lblConnectionStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblConnectionStatus.Location = new System.Drawing.Point(10, 230);
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new System.Drawing.Size(150, 15);
            this.lblConnectionStatus.TabIndex = 5;
            this.lblConnectionStatus.Text = "Status: Desconectado";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(290, 180);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(130, 30);
            this.btnDisconnect.TabIndex = 11;
            this.btnDisconnect.Text = "Desconectar";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(150, 180);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(130, 30);
            this.btnConnect.TabIndex = 10;
            this.btnConnect.Text = "Conectar";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(10, 180);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(130, 30);
            this.btnTestConnection.TabIndex = 9;
            this.btnTestConnection.Text = "Testar Conexão";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(580, 130);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(280, 23);
            this.txtPassword.TabIndex = 8;
            this.txtPassword.Text = "oracle";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(580, 110);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(47, 15);
            this.lblPassword.TabIndex = 0;
            this.lblPassword.Text = "Senha:";
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(580, 70);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(280, 23);
            this.txtUserId.TabIndex = 7;
            this.txtUserId.Text = "system";
            // 
            // lblUserId
            // 
            this.lblUserId.AutoSize = true;
            this.lblUserId.Location = new System.Drawing.Point(580, 50);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(53, 15);
            this.lblUserId.TabIndex = 0;
            this.lblUserId.Text = "Usuário:";
            // 
            // txtServiceName
            // 
            this.txtServiceName.Location = new System.Drawing.Point(290, 130);
            this.txtServiceName.Name = "txtServiceName";
            this.txtServiceName.Size = new System.Drawing.Size(280, 23);
            this.txtServiceName.TabIndex = 6;
            this.txtServiceName.Text = "ORCL";
            // 
            // lblServiceName
            // 
            this.lblServiceName.AutoSize = true;
            this.lblServiceName.Location = new System.Drawing.Point(290, 110);
            this.lblServiceName.Name = "lblServiceName";
            this.lblServiceName.Size = new System.Drawing.Size(100, 15);
            this.lblServiceName.TabIndex = 0;
            this.lblServiceName.Text = "Service Name/SID:";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(150, 130);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(130, 23);
            this.txtPort.TabIndex = 5;
            this.txtPort.Text = "1521";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(150, 110);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(37, 15);
            this.lblPort.TabIndex = 0;
            this.lblPort.Text = "Porta:";
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(10, 130);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(130, 23);
            this.txtHost.TabIndex = 4;
            this.txtHost.Text = "localhost";
            // 
            // lblHost
            // 
            this.lblHost.AutoSize = true;
            this.lblHost.Location = new System.Drawing.Point(10, 110);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(37, 15);
            this.lblHost.TabIndex = 0;
            this.lblHost.Text = "Host:";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(10, 40);
            this.txtConnectionString.Multiline = true;
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(850, 40);
            this.txtConnectionString.TabIndex = 1;
            this.txtConnectionString.Text = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)));User Id=system;Password=oracle;";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 15);
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
            this.tabTriggers.Location = new System.Drawing.Point(4, 24);
            this.tabTriggers.Name = "tabTriggers";
            this.tabTriggers.Padding = new System.Windows.Forms.Padding(3);
            this.tabTriggers.Size = new System.Drawing.Size(876, 509);
            this.tabTriggers.TabIndex = 1;
            this.tabTriggers.Text = "Triggers";
            this.tabTriggers.UseVisualStyleBackColor = true;
            // 
            // btnEnableTriggers
            // 
            this.btnEnableTriggers.Location = new System.Drawing.Point(150, 470);
            this.btnEnableTriggers.Name = "btnEnableTriggers";
            this.btnEnableTriggers.Size = new System.Drawing.Size(130, 30);
            this.btnEnableTriggers.TabIndex = 0;
            this.btnEnableTriggers.Text = "Habilitar Selecionadas";
            this.btnEnableTriggers.UseVisualStyleBackColor = true;
            this.btnEnableTriggers.Click += new System.EventHandler(this.btnEnableTriggers_Click);
            // 
            // btnDisableTriggers
            // 
            this.btnDisableTriggers.Location = new System.Drawing.Point(10, 470);
            this.btnDisableTriggers.Name = "btnDisableTriggers";
            this.btnDisableTriggers.Size = new System.Drawing.Size(130, 30);
            this.btnDisableTriggers.TabIndex = 1;
            this.btnDisableTriggers.Text = "Desabilitar Selecionadas";
            this.btnDisableTriggers.UseVisualStyleBackColor = true;
            this.btnDisableTriggers.Click += new System.EventHandler(this.btnDisableTriggers_Click);
            // 
            // btnRefreshTriggers
            // 
            this.btnRefreshTriggers.Location = new System.Drawing.Point(730, 470);
            this.btnRefreshTriggers.Name = "btnRefreshTriggers";
            this.btnRefreshTriggers.Size = new System.Drawing.Size(130, 30);
            this.btnRefreshTriggers.TabIndex = 2;
            this.btnRefreshTriggers.Text = "Atualizar Lista";
            this.btnRefreshTriggers.UseVisualStyleBackColor = true;
            this.btnRefreshTriggers.Click += new System.EventHandler(this.btnRefreshTriggers_Click);
            // 
            // btnSelectAllTriggers
            // 
            this.btnSelectAllTriggers.Location = new System.Drawing.Point(450, 470);
            this.btnSelectAllTriggers.Name = "btnSelectAllTriggers";
            this.btnSelectAllTriggers.Size = new System.Drawing.Size(130, 30);
            this.btnSelectAllTriggers.TabIndex = 3;
            this.btnSelectAllTriggers.Text = "Selecionar Todas";
            this.btnSelectAllTriggers.UseVisualStyleBackColor = true;
            this.btnSelectAllTriggers.Click += new System.EventHandler(this.btnSelectAllTriggers_Click);
            // 
            // btnDeselectAllTriggers
            // 
            this.btnDeselectAllTriggers.Location = new System.Drawing.Point(590, 470);
            this.btnDeselectAllTriggers.Name = "btnDeselectAllTriggers";
            this.btnDeselectAllTriggers.Size = new System.Drawing.Size(130, 30);
            this.btnDeselectAllTriggers.TabIndex = 4;
            this.btnDeselectAllTriggers.Text = "Desmarcar Todas";
            this.btnDeselectAllTriggers.UseVisualStyleBackColor = true;
            this.btnDeselectAllTriggers.Click += new System.EventHandler(this.btnDeselectAllTriggers_Click);
            // 
            // checkedListTriggers
            // 
            this.checkedListTriggers.FormattingEnabled = true;
            this.checkedListTriggers.Location = new System.Drawing.Point(10, 40);
            this.checkedListTriggers.Name = "checkedListTriggers";
            this.checkedListTriggers.Size = new System.Drawing.Size(850, 418);
            this.checkedListTriggers.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Triggers disponíveis no banco:";
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
            this.tabConstraints.Location = new System.Drawing.Point(4, 24);
            this.tabConstraints.Name = "tabConstraints";
            this.tabConstraints.Size = new System.Drawing.Size(876, 509);
            this.tabConstraints.TabIndex = 2;
            this.tabConstraints.Text = "Constraints";
            this.tabConstraints.UseVisualStyleBackColor = true;
            // 
            // btnEnableConstraints
            // 
            this.btnEnableConstraints.Location = new System.Drawing.Point(150, 470);
            this.btnEnableConstraints.Name = "btnEnableConstraints";
            this.btnEnableConstraints.Size = new System.Drawing.Size(130, 30);
            this.btnEnableConstraints.TabIndex = 0;
            this.btnEnableConstraints.Text = "Habilitar Selecionadas";
            this.btnEnableConstraints.UseVisualStyleBackColor = true;
            this.btnEnableConstraints.Click += new System.EventHandler(this.btnEnableConstraints_Click);
            // 
            // btnDisableConstraints
            // 
            this.btnDisableConstraints.Location = new System.Drawing.Point(10, 470);
            this.btnDisableConstraints.Name = "btnDisableConstraints";
            this.btnDisableConstraints.Size = new System.Drawing.Size(130, 30);
            this.btnDisableConstraints.TabIndex = 1;
            this.btnDisableConstraints.Text = "Desabilitar Selecionadas";
            this.btnDisableConstraints.UseVisualStyleBackColor = true;
            this.btnDisableConstraints.Click += new System.EventHandler(this.btnDisableConstraints_Click);
            // 
            // btnRefreshConstraints
            // 
            this.btnRefreshConstraints.Location = new System.Drawing.Point(730, 470);
            this.btnRefreshConstraints.Name = "btnRefreshConstraints";
            this.btnRefreshConstraints.Size = new System.Drawing.Size(130, 30);
            this.btnRefreshConstraints.TabIndex = 2;
            this.btnRefreshConstraints.Text = "Atualizar Lista";
            this.btnRefreshConstraints.UseVisualStyleBackColor = true;
            this.btnRefreshConstraints.Click += new System.EventHandler(this.btnRefreshConstraints_Click);
            // 
            // btnSelectAllConstraints
            // 
            this.btnSelectAllConstraints.Location = new System.Drawing.Point(450, 470);
            this.btnSelectAllConstraints.Name = "btnSelectAllConstraints";
            this.btnSelectAllConstraints.Size = new System.Drawing.Size(130, 30);
            this.btnSelectAllConstraints.TabIndex = 3;
            this.btnSelectAllConstraints.Text = "Selecionar Todas";
            this.btnSelectAllConstraints.UseVisualStyleBackColor = true;
            this.btnSelectAllConstraints.Click += new System.EventHandler(this.btnSelectAllConstraints_Click);
            // 
            // btnDeselectAllConstraints
            // 
            this.btnDeselectAllConstraints.Location = new System.Drawing.Point(590, 470);
            this.btnDeselectAllConstraints.Name = "btnDeselectAllConstraints";
            this.btnDeselectAllConstraints.Size = new System.Drawing.Size(130, 30);
            this.btnDeselectAllConstraints.TabIndex = 4;
            this.btnDeselectAllConstraints.Text = "Desmarcar Todas";
            this.btnDeselectAllConstraints.UseVisualStyleBackColor = true;
            this.btnDeselectAllConstraints.Click += new System.EventHandler(this.btnDeselectAllConstraints_Click);
            // 
            // checkedListConstraints
            // 
            this.checkedListConstraints.FormattingEnabled = true;
            this.checkedListConstraints.Location = new System.Drawing.Point(10, 40);
            this.checkedListConstraints.Name = "checkedListConstraints";
            this.checkedListConstraints.Size = new System.Drawing.Size(850, 418);
            this.checkedListConstraints.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(220, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Constraints disponíveis no banco:";
            // 
            // tabTruncate
            // 
            this.tabTruncate.Controls.Add(this.label4);
            this.tabTruncate.Controls.Add(this.checkedListTables);
            this.tabTruncate.Controls.Add(this.btnDeselectAllTables);
            this.tabTruncate.Controls.Add(this.btnSelectAllTables);
            this.tabTruncate.Controls.Add(this.btnRefreshTables);
            this.tabTruncate.Controls.Add(this.btnTruncate);
            this.tabTruncate.Location = new System.Drawing.Point(4, 24);
            this.tabTruncate.Name = "tabTruncate";
            this.tabTruncate.Size = new System.Drawing.Size(876, 509);
            this.tabTruncate.TabIndex = 3;
            this.tabTruncate.Text = "Truncate";
            this.tabTruncate.UseVisualStyleBackColor = true;
            // 
            // btnTruncate
            // 
            this.btnTruncate.Location = new System.Drawing.Point(10, 470);
            this.btnTruncate.Name = "btnTruncate";
            this.btnTruncate.Size = new System.Drawing.Size(130, 30);
            this.btnTruncate.TabIndex = 0;
            this.btnTruncate.Text = "Truncar Selecionadas";
            this.btnTruncate.UseVisualStyleBackColor = true;
            this.btnTruncate.Click += new System.EventHandler(this.btnTruncate_Click);
            // 
            // btnRefreshTables
            // 
            this.btnRefreshTables.Location = new System.Drawing.Point(730, 470);
            this.btnRefreshTables.Name = "btnRefreshTables";
            this.btnRefreshTables.Size = new System.Drawing.Size(130, 30);
            this.btnRefreshTables.TabIndex = 1;
            this.btnRefreshTables.Text = "Atualizar Lista";
            this.btnRefreshTables.UseVisualStyleBackColor = true;
            this.btnRefreshTables.Click += new System.EventHandler(this.btnRefreshTables_Click);
            // 
            // btnSelectAllTables
            // 
            this.btnSelectAllTables.Location = new System.Drawing.Point(450, 470);
            this.btnSelectAllTables.Name = "btnSelectAllTables";
            this.btnSelectAllTables.Size = new System.Drawing.Size(130, 30);
            this.btnSelectAllTables.TabIndex = 2;
            this.btnSelectAllTables.Text = "Selecionar Todas";
            this.btnSelectAllTables.UseVisualStyleBackColor = true;
            this.btnSelectAllTables.Click += new System.EventHandler(this.btnSelectAllTables_Click);
            // 
            // btnDeselectAllTables
            // 
            this.btnDeselectAllTables.Location = new System.Drawing.Point(590, 470);
            this.btnDeselectAllTables.Name = "btnDeselectAllTables";
            this.btnDeselectAllTables.Size = new System.Drawing.Size(130, 30);
            this.btnDeselectAllTables.TabIndex = 3;
            this.btnDeselectAllTables.Text = "Desmarcar Todas";
            this.btnDeselectAllTables.UseVisualStyleBackColor = true;
            this.btnDeselectAllTables.Click += new System.EventHandler(this.btnDeselectAllTables_Click);
            // 
            // checkedListTables
            // 
            this.checkedListTables.FormattingEnabled = true;
            this.checkedListTables.Location = new System.Drawing.Point(10, 40);
            this.checkedListTables.Name = "checkedListTables";
            this.checkedListTables.Size = new System.Drawing.Size(850, 418);
            this.checkedListTables.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(200, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "Tabelas disponíveis no banco:";
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
            this.tabExport.Location = new System.Drawing.Point(4, 24);
            this.tabExport.Name = "tabExport";
            this.tabExport.Size = new System.Drawing.Size(876, 509);
            this.tabExport.TabIndex = 4;
            this.tabExport.Text = "Exportação";
            this.tabExport.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(10, 470);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(130, 30);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "Exportar DDL";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnRefreshExportTables
            // 
            this.btnRefreshExportTables.Location = new System.Drawing.Point(730, 470);
            this.btnRefreshExportTables.Name = "btnRefreshExportTables";
            this.btnRefreshExportTables.Size = new System.Drawing.Size(130, 30);
            this.btnRefreshExportTables.TabIndex = 1;
            this.btnRefreshExportTables.Text = "Atualizar Lista";
            this.btnRefreshExportTables.UseVisualStyleBackColor = true;
            this.btnRefreshExportTables.Click += new System.EventHandler(this.btnRefreshExportTables_Click);
            // 
            // btnSelectAllExportTables
            // 
            this.btnSelectAllExportTables.Location = new System.Drawing.Point(450, 470);
            this.btnSelectAllExportTables.Name = "btnSelectAllExportTables";
            this.btnSelectAllExportTables.Size = new System.Drawing.Size(130, 30);
            this.btnSelectAllExportTables.TabIndex = 2;
            this.btnSelectAllExportTables.Text = "Selecionar Todas";
            this.btnSelectAllExportTables.UseVisualStyleBackColor = true;
            this.btnSelectAllExportTables.Click += new System.EventHandler(this.btnSelectAllExportTables_Click);
            // 
            // btnDeselectAllExportTables
            // 
            this.btnDeselectAllExportTables.Location = new System.Drawing.Point(590, 470);
            this.btnDeselectAllExportTables.Name = "btnDeselectAllExportTables";
            this.btnDeselectAllExportTables.Size = new System.Drawing.Size(130, 30);
            this.btnDeselectAllExportTables.TabIndex = 3;
            this.btnDeselectAllExportTables.Text = "Desmarcar Todas";
            this.btnDeselectAllExportTables.UseVisualStyleBackColor = true;
            this.btnDeselectAllExportTables.Click += new System.EventHandler(this.btnDeselectAllExportTables_Click);
            // 
            // chkIncludeForeignKeys
            // 
            this.chkIncludeForeignKeys.AutoSize = true;
            this.chkIncludeForeignKeys.Checked = true;
            this.chkIncludeForeignKeys.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeForeignKeys.Location = new System.Drawing.Point(300, 477);
            this.chkIncludeForeignKeys.Name = "chkIncludeForeignKeys";
            this.chkIncludeForeignKeys.Size = new System.Drawing.Size(120, 19);
            this.chkIncludeForeignKeys.TabIndex = 4;
            this.chkIncludeForeignKeys.Text = "Incluir Foreign Keys";
            this.chkIncludeForeignKeys.UseVisualStyleBackColor = true;
            // 
            // chkIncludeConstraints
            // 
            this.chkIncludeConstraints.AutoSize = true;
            this.chkIncludeConstraints.Checked = true;
            this.chkIncludeConstraints.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeConstraints.Location = new System.Drawing.Point(160, 477);
            this.chkIncludeConstraints.Name = "chkIncludeConstraints";
            this.chkIncludeConstraints.Size = new System.Drawing.Size(120, 19);
            this.chkIncludeConstraints.TabIndex = 5;
            this.chkIncludeConstraints.Text = "Incluir Constraints";
            this.chkIncludeConstraints.UseVisualStyleBackColor = true;
            // 
            // checkedListExportTables
            // 
            this.checkedListExportTables.FormattingEnabled = true;
            this.checkedListExportTables.Location = new System.Drawing.Point(10, 40);
            this.checkedListExportTables.Name = "checkedListExportTables";
            this.checkedListExportTables.Size = new System.Drawing.Size(850, 418);
            this.checkedListExportTables.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(250, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "Selecione as tabelas para exportar DDL:";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 537);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(884, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(50, 17);
            this.toolStripStatusLabel.Text = "Pronto";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
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
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Label lblUserId;
        private System.Windows.Forms.TextBox txtServiceName;
        private System.Windows.Forms.Label lblServiceName;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Label lblHost;
    }
}
