using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PLSQLMigrationTool.Business;
using PLSQLMigrationTool.Data;
using PLSQLMigrationTool.Models;
using System.Text.RegularExpressions; // <--- ADICIONADO

namespace PLSQLMigrationTool.Forms
{
    public partial class MainForm : Form
    {
        private OracleConnectionManager _connectionManager;
        private OracleQueryExecutor _queryExecutor;
        private MetadataRepository _metadataRepository;
        private TriggerManager _triggerManager;
        private ConstraintManager _constraintManager;
        private TableManager _tableManager;
        private ExportManager _exportManager;

        private List<TriggerInfo> _allTriggers;
        private List<ConstraintInfo> _allConstraints;
        private List<TableInfo> _allTables;

        public MainForm()
        {
            InitializeComponent();
            _connectionManager = new OracleConnectionManager();
            UpdateConnectionStatus();
            UpdateConnectionString(); // Inicializa a string de conexão
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);

            // Garante que a alteração manual nos campos atualize a string
            txtHost.TextChanged += (s, e) => UpdateConnectionString();
            txtPort.TextChanged += (s, e) => UpdateConnectionString();
            txtServiceName.TextChanged += (s, e) => UpdateConnectionString();
            txtUserId.TextChanged += (s, e) => UpdateConnectionString();
            txtPassword.TextChanged += (s, e) => UpdateConnectionString();
        }

        #region Connection Tab

        // --- NOVA LÓGICA DO BOTÃO COLAR STRING ---
        private void btnPasteString_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                string textoCopiado = Clipboard.GetText();
                ImportarStringConexao(textoCopiado);
            }
        }

        private void ImportarStringConexao(string rawString)
        {
            // Regex para extrair: Usuario/Senha@//Host:Porta/Servico
            string pattern = @"^(?<user>[^/]+)/(?<pass>[^@]+)@(?://)?(?<host>[^:/]+):(?<port>\d+)/(?<service>.+)$";

            var match = Regex.Match(rawString.Trim(), pattern);

            if (match.Success)
            {
                // Preenche os campos individuais
                txtUserId.Text = match.Groups["user"].Value;
                txtPassword.Text = match.Groups["pass"].Value;
                txtHost.Text = match.Groups["host"].Value;
                txtPort.Text = match.Groups["port"].Value;
                txtServiceName.Text = match.Groups["service"].Value;

                // Força a atualização da string de conexão visual
                UpdateConnectionString();

                MessageBox.Show("Dados colados e preenchidos com sucesso!", "Importação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("O formato da string na área de transferência não é válido.\n\nFormato esperado: Usuario/Senha@//Host:Porta/Servico", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        // ------------------------------------------

        private string BuildConnectionString()
        {
            string host = txtHost.Text.Trim();
            string port = txtPort.Text.Trim();
            string serviceName = txtServiceName.Text.Trim();
            string userId = txtUserId.Text.Trim();
            string password = txtPassword.Text;

            // Formato padrão para ODP.NET Managed Driver
            string connectionString =
                $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={host})(PORT={port}))(CONNECT_DATA=(SERVICE_NAME={serviceName})));" +
                $"User Id={userId};Password={password};";

            return connectionString;
        }

        private void UpdateConnectionString()
        {
            txtConnectionString.Text = BuildConnectionString();
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateConnectionString();
                SetStatus("Testando conexão...");
                _connectionManager.ConnectionString = txtConnectionString.Text.Trim();

                if (_connectionManager.TestConnection())
                {
                    MessageBox.Show("Conexão testada com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetStatus("Conexão testada com sucesso");
                }
                else
                {
                    MessageBox.Show("Falha ao testar conexão. Verifique os parâmetros e o status do banco.", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SetStatus("Falha ao testar conexão");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao testar conexão:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Erro ao testar conexão");
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateConnectionString();
                SetStatus("Conectando...");
                _connectionManager.ConnectionString = txtConnectionString.Text.Trim();
                _connectionManager.Connect();

                // Inicializar componentes de acesso a dados
                _queryExecutor = new OracleQueryExecutor(_connectionManager);
                _metadataRepository = new MetadataRepository(_queryExecutor);
                _triggerManager = new TriggerManager(_queryExecutor, _metadataRepository);
                _constraintManager = new ConstraintManager(_queryExecutor, _metadataRepository);
                _tableManager = new TableManager(_queryExecutor, _metadataRepository);
                _exportManager = new ExportManager(_queryExecutor, _metadataRepository);

                UpdateConnectionStatus();
                MessageBox.Show("Conectado com sucesso!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetStatus("Conectado ao banco de dados");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao conectar:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Erro ao conectar");
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                _connectionManager.Disconnect();
                UpdateConnectionStatus();
                MessageBox.Show("Desconectado com sucesso!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetStatus("Desconectado");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao desconectar:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateConnectionStatus()
        {
            if (_connectionManager.IsConnected)
            {
                lblConnectionStatus.Text = "Status: Conectado";
                lblConnectionStatus.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblConnectionStatus.Text = "Status: Desconectado";
                lblConnectionStatus.ForeColor = System.Drawing.Color.Red;
            }
        }

        #endregion

        #region Triggers Tab

        private void btnRefreshTriggers_Click(object sender, EventArgs e)
        {
            if (!CheckConnection()) return;

            try
            {
                SetStatus("Carregando triggers...");
                _allTriggers = _triggerManager.GetAllTriggers();

                checkedListTriggers.Items.Clear();
                foreach (TriggerInfo trigger in _allTriggers)
                {
                    checkedListTriggers.Items.Add(trigger);
                }

                SetStatus($"{_allTriggers.Count} triggers carregadas");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar triggers:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Erro ao carregar triggers");
            }
        }

        private void btnDisableTriggers_Click(object sender, EventArgs e)
        {
            if (!CheckConnection()) return;

            if (checkedListTriggers.CheckedItems.Count == 0)
            {
                MessageBox.Show("Selecione pelo menos uma trigger.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Desabilitar {checkedListTriggers.CheckedItems.Count} trigger(s) selecionada(s)?",
                "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            try
            {
                SetStatus("Desabilitando triggers...");
                List<string> triggerNames = new List<string>();

                foreach (TriggerInfo trigger in checkedListTriggers.CheckedItems)
                {
                    triggerNames.Add(trigger.TriggerName);
                }

                int count = _triggerManager.DisableTriggers(triggerNames);

                MessageBox.Show($"{count} trigger(s) desabilitada(s) com sucesso!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetStatus($"{count} triggers desabilitadas");

                btnRefreshTriggers_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao desabilitar triggers:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Erro ao desabilitar triggers");
            }
        }

        private void btnEnableTriggers_Click(object sender, EventArgs e)
        {
            if (!CheckConnection()) return;

            if (checkedListTriggers.CheckedItems.Count == 0)
            {
                MessageBox.Show("Selecione pelo menos uma trigger.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SetStatus("Habilitando triggers...");
                List<string> triggerNames = new List<string>();

                foreach (TriggerInfo trigger in checkedListTriggers.CheckedItems)
                {
                    triggerNames.Add(trigger.TriggerName);
                }

                int count = _triggerManager.EnableTriggers(triggerNames);

                MessageBox.Show($"{count} trigger(s) habilitada(s) com sucesso!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetStatus($"{count} triggers habilitadas");

                btnRefreshTriggers_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao habilitar triggers:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Erro ao habilitar triggers");
            }
        }

        private void btnSelectAllTriggers_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListTriggers.Items.Count; i++)
            {
                checkedListTriggers.SetItemChecked(i, true);
            }
        }

        private void btnDeselectAllTriggers_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListTriggers.Items.Count; i++)
            {
                checkedListTriggers.SetItemChecked(i, false);
            }
        }

        #endregion

        #region Constraints Tab

        private void btnRefreshConstraints_Click(object sender, EventArgs e)
        {
            if (!CheckConnection()) return;

            try
            {
                SetStatus("Carregando constraints...");
                _allConstraints = _constraintManager.GetAllConstraints();

                checkedListConstraints.Items.Clear();
                foreach (ConstraintInfo constraint in _allConstraints)
                {
                    checkedListConstraints.Items.Add(constraint);
                }

                SetStatus($"{_allConstraints.Count} constraints carregadas");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar constraints:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Erro ao carregar constraints");
            }
        }

        private void btnDisableConstraints_Click(object sender, EventArgs e)
        {
            if (!CheckConnection()) return;

            if (checkedListConstraints.CheckedItems.Count == 0)
            {
                MessageBox.Show("Selecione pelo menos uma constraint.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Desabilitar {checkedListConstraints.CheckedItems.Count} constraint(s) selecionada(s)?",
                "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            try
            {
                SetStatus("Desabilitando constraints...");
                List<ConstraintInfo> constraints = new List<ConstraintInfo>();

                foreach (ConstraintInfo constraint in checkedListConstraints.CheckedItems)
                {
                    constraints.Add(constraint);
                }

                int count = _constraintManager.DisableConstraints(constraints);

                MessageBox.Show($"{count} constraint(s) desabilitada(s) com sucesso!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetStatus($"{count} constraints desabilitadas");

                btnRefreshConstraints_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao desabilitar constraints:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Erro ao desabilitar constraints");
            }
        }

        private void btnEnableConstraints_Click(object sender, EventArgs e)
        {
            if (!CheckConnection()) return;

            if (checkedListConstraints.CheckedItems.Count == 0)
            {
                MessageBox.Show("Selecione pelo menos uma constraint.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SetStatus("Habilitando constraints...");
                List<ConstraintInfo> constraints = new List<ConstraintInfo>();

                foreach (ConstraintInfo constraint in checkedListConstraints.CheckedItems)
                {
                    constraints.Add(constraint);
                }

                int count = _constraintManager.EnableConstraints(constraints);

                MessageBox.Show($"{count} constraint(s) habilitada(s) com sucesso!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetStatus($"{count} constraints habilitadas");

                btnRefreshConstraints_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao habilitar constraints:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Erro ao habilitar constraints");
            }
        }

        private void btnSelectAllConstraints_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListConstraints.Items.Count; i++)
            {
                checkedListConstraints.SetItemChecked(i, true);
            }
        }

        private void btnDeselectAllConstraints_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListConstraints.Items.Count; i++)
            {
                checkedListConstraints.SetItemChecked(i, false);
            }
        }

        #endregion

        #region Truncate Tab

        private void btnRefreshTables_Click(object sender, EventArgs e)
        {
            if (!CheckConnection()) return;

            try
            {
                SetStatus("Carregando tabelas...");
                _allTables = _tableManager.GetAllTables();

                checkedListTables.Items.Clear();
                foreach (TableInfo table in _allTables)
                {
                    checkedListTables.Items.Add(table);
                }

                SetStatus($"{_allTables.Count} tabelas carregadas");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar tabelas:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Erro ao carregar tabelas");
            }
        }

        private void btnTruncate_Click(object sender, EventArgs e)
        {
            if (!CheckConnection()) return;

            if (checkedListTables.CheckedItems.Count == 0)
            {
                MessageBox.Show("Selecione pelo menos uma tabela.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"ATENÇÃO: Esta operação irá TRUNCAR {checkedListTables.CheckedItems.Count} tabela(s), " +
                "removendo TODOS os dados permanentemente!\n\nDeseja continuar?",
                "CONFIRMAÇÃO CRÍTICA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            try
            {
                SetStatus("Truncando tabelas...");
                List<string> tableNames = new List<string>();

                foreach (TableInfo table in checkedListTables.CheckedItems)
                {
                    tableNames.Add(table.TableName);
                }

                int count = _tableManager.TruncateTables(tableNames);

                MessageBox.Show($"{count} tabela(s) truncada(s) com sucesso!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetStatus($"{count} tabelas truncadas");

                btnRefreshTables_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao truncar tabelas:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Erro ao truncar tabelas");
            }
        }

        private void btnSelectAllTables_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListTables.Items.Count; i++)
            {
                checkedListTables.SetItemChecked(i, true);
            }
        }

        private void btnDeselectAllTables_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListTables.Items.Count; i++)
            {
                checkedListTables.SetItemChecked(i, false);
            }
        }

        #endregion

        #region Enable Constraints Tab

        private void btnRefreshEnableConstraints_Click(object sender, EventArgs e)
        {
            if (!CheckConnection()) return;

            try
            {
                SetStatus("Carregando constraints desabilitadas...");
                // Re-carrega todas as constraints para garantir o status atualizado
                _allConstraints = _constraintManager.GetAllConstraints();

                // Filtra apenas as constraints desabilitadas
                List<ConstraintInfo> disabledConstraints = _allConstraints.FindAll(c => !c.IsEnabled).FindAll(c => c.ConstraintType != "R");

                // Aplica o filtro de tipo de constraint
                List<ConstraintInfo> filteredConstraints = ApplyConstraintFilter(disabledConstraints);

                checkedListEnableConstraints.Items.Clear();
                foreach (ConstraintInfo constraint in filteredConstraints)
                {
                    checkedListEnableConstraints.Items.Add(constraint);
                }

                SetStatus($"{checkedListEnableConstraints.Items.Count} constraints desabilitadas carregadas");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar constraints desabilitadas:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Erro ao carregar constraints desabilitadas");
            }
        }

        private List<ConstraintInfo> ApplyConstraintFilter(List<ConstraintInfo> constraints)
        {
            List<string> allowedTypes = new List<string>();
            if (chkForeign.Checked) allowedTypes.Add("R");
            if (chkPrimary.Checked) allowedTypes.Add("P");
            if (chkUnique.Checked) allowedTypes.Add("U");
            if (chkCheck.Checked) allowedTypes.Add("C");

            return constraints.FindAll(c => allowedTypes.Contains(c.ConstraintType));
        }

        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
            // Recarrega a lista ao alterar o filtro
            btnRefreshEnableConstraints_Click(sender, e);
        }

        private void btnEnableSelectedConstraints_Click(object sender, EventArgs e)
        {
            if (!CheckConnection()) return;

            if (checkedListEnableConstraints.CheckedItems.Count == 0)
            {
                MessageBox.Show("Selecione pelo menos uma constraint para habilitar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Habilitar {checkedListEnableConstraints.CheckedItems.Count} constraint(s) selecionada(s)?",
                "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            try
            {
                SetStatus("Habilitando constraints...");
                List<ConstraintInfo> constraintsToEnable = new List<ConstraintInfo>();

                foreach (ConstraintInfo constraint in checkedListEnableConstraints.CheckedItems)
                {
                    constraintsToEnable.Add(constraint);
                }

                int count = _constraintManager.EnableConstraints(constraintsToEnable);

                MessageBox.Show($"{count} constraint(s) habilitada(s) com sucesso!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetStatus($"{count} constraints habilitadas");

                btnRefreshEnableConstraints_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao habilitar constraints:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Erro ao habilitar constraints");
            }
        }

        private void btnSelectAllEnableConstraints_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListEnableConstraints.Items.Count; i++)
            {
                checkedListEnableConstraints.SetItemChecked(i, true);
            }
        }

        private void btnDeselectAllEnableConstraints_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListEnableConstraints.Items.Count; i++)
            {
                checkedListEnableConstraints.SetItemChecked(i, false);
            }
        }

        #endregion

        #region Export Tab

        private void btnRefreshExportTables_Click(object sender, EventArgs e)
        {
            if (!CheckConnection()) return;

            try
            {
                SetStatus("Carregando tabelas...");
                _allTables = _tableManager.GetAllTables();

                checkedListExportTables.Items.Clear();
                foreach (TableInfo table in _allTables)
                {
                    checkedListExportTables.Items.Add(table);
                }

                SetStatus($"{_allTables.Count} tabelas carregadas");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar tabelas:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Erro ao carregar tabelas");
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab != null && tabControl.SelectedTab.Text == "Habilitar Constraints")
            {
                btnRefreshEnableConstraints_Click(sender, e);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (!CheckConnection()) return;

            if (checkedListExportTables.CheckedItems.Count == 0)
            {
                MessageBox.Show("Selecione pelo menos uma tabela.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "SQL Files (*.sql)|*.sql|All Files (*.*)|*.*";
            saveDialog.DefaultExt = "sql";
            saveDialog.FileName = $"DDL_Export_{DateTime.Now:yyyyMMdd_HHmmss}.sql";

            if (saveDialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                SetStatus("Exportando DDL...");
                List<string> tableNames = new List<string>();

                foreach (TableInfo table in checkedListExportTables.CheckedItems)
                {
                    tableNames.Add(table.TableName);
                }

                _exportManager.ExportTablesDDL(
                    tableNames,
                    chkIncludeConstraints.Checked,
                    chkIncludeForeignKeys.Checked,
                    saveDialog.FileName);

                MessageBox.Show($"DDL exportado com sucesso para:\n{saveDialog.FileName}", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetStatus("DDL exportado com sucesso");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao exportar DDL:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Erro ao exportar DDL");
            }
        }

        private void btnSelectAllExportTables_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListExportTables.Items.Count; i++)
            {
                checkedListExportTables.SetItemChecked(i, true);
            }
        }

        private void btnDeselectAllExportTables_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListExportTables.Items.Count; i++)
            {
                checkedListExportTables.SetItemChecked(i, false);
            }
        }

        #endregion

        #region Helper Methods

        private bool CheckConnection()
        {
            if (!_connectionManager.IsConnected)
            {
                MessageBox.Show("Não há conexão ativa com o banco de dados.\nConecte-se primeiro na aba 'Conexão'.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void SetStatus(string message)
        {
            toolStripStatusLabel.Text = message;
            statusStrip.Refresh();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_connectionManager != null && _connectionManager.IsConnected)
            {
                _connectionManager.Disconnect();
            }
        }

        #endregion

        private void txtServiceName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}