using Oracle.ManagedDataAccess.Client;
using PLSQLImportFull.Business;
using PLSQLImportFull.Data;
using PLSQLImportFull.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace PLSQLImportFull.Forms
{
    public partial class MainForm : Form
    {
        private OracleConnectionManager _connectionManager;
        private OracleQueryExecutor _queryExecutor;
        private MetadataRepository _metadataRepository;
        private TriggerManager _triggerManager;
        private ConstraintManager _constraintManager;
        private TableManager _tableManager;
        private ImportManager _importManager;

        // Variável da bolinha
        private ToolStripStatusLabel lblStatusIcon;
        private bool _truncateSortAscName = true;
        private bool _truncateSortAscRows = false;
        private List<TriggerInfo> _allTriggers;
        private List<ConstraintInfo> _allConstraints;
        private List<TableInfo> _allTables;


        public MainForm()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new System.Drawing.Size(780, 530);
            this.lstImportFiles.AllowDrop = true;
            this.lstImportFiles.DragEnter += LstImportFiles_DragEnter;
            this.lstImportFiles.DragDrop += LstImportFiles_DragDrop;
            this.label7.AllowDrop = true;
            this.label7.DragEnter += LstImportFiles_DragEnter;
            this.label7.DragDrop += LstImportFiles_DragDrop;


            // TRAVAR A STRING DE CONEXÃO COMPLETA
            this.txtConnectionString.ReadOnly = true;
            this.txtConnectionString.BackColor = SystemColors.Control;

            // --- CONFIGURAÇÃO DA BOLINHA NO RODAPÉ (ANTES DO TEXTO) ---
            lblStatusIcon = new ToolStripStatusLabel();
            lblStatusIcon.Text = "●";
            lblStatusIcon.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblStatusIcon.ForeColor = Color.Gray;
            lblStatusIcon.Alignment = ToolStripItemAlignment.Left;
            lblStatusIcon.Margin = new Padding(0, -5, -8, 0); // Ajuste fino de posição

            // INSERE NA POSIÇÃO 0 (ESQUERDA EXTREMA)
            this.statusStrip.Items.Insert(0, lblStatusIcon);

            // Empurra o texto de status para preencher o resto
            this.toolStripStatusLabel.Spring = true;
            this.toolStripStatusLabel.TextAlign = ContentAlignment.MiddleLeft;
            // ----------------------------------------------------------

            _connectionManager = new OracleConnectionManager();

            // Eventos
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            this.tabControl.Selecting += new TabControlCancelEventHandler(this.tabControl_Selecting);
            this.FormClosing += new FormClosingEventHandler(this.ExportForm_FormClosing);

            // Atualização automática da string
            txtHost.TextChanged += (s, e) => UpdateConnectionString();
            txtPort.TextChanged += (s, e) => UpdateConnectionString();
            txtServiceName.TextChanged += (s, e) => UpdateConnectionString();
            txtUserId.TextChanged += (s, e) => UpdateConnectionString();
            txtPassword.TextChanged += (s, e) => UpdateConnectionString();

            // Inicializa UI
            UpdateConnectionString();
            UpdateConnectionStatus();

            if (!string.IsNullOrEmpty(Properties.Settings.Default.LastHost))
            {
                txtHost.Text = Properties.Settings.Default.LastHost;
                txtPort.Text = Properties.Settings.Default.LastPort;
                txtServiceName.Text = Properties.Settings.Default.LastService;
                txtUserId.Text = Properties.Settings.Default.LastUser;
                txtPassword.Text = Properties.Settings.Default.LastPassword;
            }
            else
            {
#if DEBUG
                txtHost.Text = "172.25.100.205";
                txtPort.Text = "1521";
                txtServiceName.Text = "XE";
                txtUserId.Text = "r22sp15";
                txtPassword.Text = "r22sp15";
#endif
            }

            UpdateConnectionStatus();
            UpdateConnectionString();
            AtualizarLabelArrastar();
        }

        #region Aba Conexão
        private void ExportForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 1. Desconecta se necessário
            if (_connectionManager != null && _connectionManager.IsConnected)
            {
                _connectionManager.Disconnect();
            }

            // 2. Salva os dados dos campos nas configurações
            Properties.Settings.Default.LastHost = txtHost.Text.Trim();
            Properties.Settings.Default.LastPort = txtPort.Text.Trim();
            Properties.Settings.Default.LastService = txtServiceName.Text.Trim();
            Properties.Settings.Default.LastUser = txtUserId.Text.Trim();
            Properties.Settings.Default.LastPassword = txtPassword.Text;

            // 3. Grava no disco
            Properties.Settings.Default.Save();
        }

        public class ConnectionProfile
        {
            public string Host { get; set; }
            public string Port { get; set; }
            public string Service { get; set; }
            public string User { get; set; }
            // Evite salvar senha em histórico por segurança, ou salve se for requisito interno

            public override string ToString()
            {
                // O que vai aparecer no ComboBox
                return $"{User}@{Host}:{Port}/{Service}";
            }
        }
        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage != tabConnection && !_connectionManager.IsConnected)
            {
                e.Cancel = true;
                MessageBox.Show("Por favor, conecte-se ao banco de dados primeiro.", "Acesso Negado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LstImportFiles_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void LstImportFiles_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                // Aceita apenas .sql, .pdc ou .7z
                string ext = Path.GetExtension(file).ToLower();
                if (ext == ".sql" || ext == ".pdc" || ext == ".7z")
                {
                    if (!lstImportFiles.Items.Contains(file))
                        lstImportFiles.Items.Add(file);
                }
            }
            SetStatus($"{files.Length} arquivos adicionados via arraste.");
            AtualizarLabelArrastar();
        }

        // --- ATUALIZA STATUS E TRAVA CAMPOS ---
        private void UpdateConnectionStatus()
        {
            bool isConnected = _connectionManager.IsConnected;

            if (isConnected)
            {
                lblConnectionStatus.Text = "🟢 Status: Conectado";
                lblConnectionStatus.ForeColor = Color.Green;
                lblConnectionStatus.BackColor = Color.Transparent;

                // Bolinha Verde
                lblStatusIcon.ForeColor = Color.Green;
                lblStatusIcon.ToolTipText = "Conectado";
            }
            else
            {
                lblConnectionStatus.Text = "🔴 Status: Desconectado";
                lblConnectionStatus.ForeColor = Color.Red;
                lblConnectionStatus.BackColor = Color.Transparent;

                // Bolinha Vermelha
                lblStatusIcon.ForeColor = Color.Red;
                lblStatusIcon.ToolTipText = "Desconectado";
            }
            lblConnectionStatus.AutoSize = true;

            bool enableInputs = !isConnected;

            txtHost.Enabled = enableInputs;
            txtPort.Enabled = enableInputs;
            txtServiceName.Enabled = enableInputs;
            txtUserId.Enabled = enableInputs;
            txtPassword.Enabled = enableInputs;
            txtConnectionString.Enabled = enableInputs;

            btnConnect.Enabled = enableInputs;
            //btnTestConnection.Enabled = enableInputs;
            btnPasteString.Enabled = enableInputs;

            // Encontra o botão load config dinâmico
            var btnLoad = this.Controls.Find("btnLoadConfig", true);
            if (btnLoad.Length > 0) btnLoad[0].Enabled = enableInputs;

            btnDisconnect.Enabled = isConnected;
        }

        // --- RESTANTE DO CÓDIGO MANTIDO IGUAL ---
        // (Copiei seus métodos existentes abaixo para garantir que o arquivo fique completo e funcional)

        private void btnLoadConfig_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Arquivos de Configuração (*.config)|*.config|Todos os Arquivos (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(ofd.FileName);
                        var addNodes = doc.GetElementsByTagName("add");
                        string strConexaoFull = "";

                        foreach (XmlNode node in addNodes)
                        {
                            if (node.Attributes["key"]?.Value == "strConexaoBD")
                            {
                                strConexaoFull = node.Attributes["value"]?.Value;
                                break;
                            }
                        }

                        if (!string.IsNullOrEmpty(strConexaoFull))
                        {
                            ProcessarStringConexao(strConexaoFull);
                            MessageBox.Show("Configuração importada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else MessageBox.Show("Chave 'strConexaoBD' não encontrada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
                }
            }
        }

        private void btnPasteString_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                string txt = Clipboard.GetText();
                if (txt.Contains("DESCRIPTION") && txt.Contains("HOST")) ProcessarStringConexao(txt);
                else ImportarStringConexaoSimples(txt);
            }
        }

        private void btnSortTruncateName_Click(object sender, EventArgs e)
        {
            if (_allTables == null || _allTables.Count == 0) return;

            // Alterna a ordem
            _truncateSortAscName = !_truncateSortAscName;

            if (_truncateSortAscName)
            {
                // A-Z
                _allTables.Sort((x, y) => string.Compare(x.TableName, y.TableName));
                btnSortTruncateName.Text = "Nome ▲";
            }
            else
            {
                // Z-A
                _allTables.Sort((x, y) => string.Compare(y.TableName, x.TableName));
                btnSortTruncateName.Text = "Nome ▼";
            }

            // Reseta o texto do outro botão
            btnSortTruncateRows.Text = "Ordenar Linhas";

            // Atualiza a lista visual
            UpdateTruncateList(_allTables);
        }

        private void btnSortTruncateRows_Click(object sender, EventArgs e)
        {
            if (_allTables == null || _allTables.Count == 0) return;

            // Alterna a ordem
            _truncateSortAscRows = !_truncateSortAscRows;

            if (_truncateSortAscRows)
            {
                // 0-9 (Crescente)
                _allTables.Sort((x, y) => x.NumRows.CompareTo(y.NumRows));
                btnSortTruncateRows.Text = "Linhas ▲";
            }
            else
            {
                // 9-0 (Decrescente - Mais útil para ver tabelas cheias)
                _allTables.Sort((x, y) => y.NumRows.CompareTo(x.NumRows));
                btnSortTruncateRows.Text = "Linhas ▼";
            }

            // Reseta o texto do outro botão
            btnSortTruncateName.Text = "Ordenar Nome";

            // Atualiza a lista visual
            UpdateTruncateList(_allTables);
        }
        private void ProcessarStringConexao(string fullString)
        {
            try
            {
                var builder = new OracleConnectionStringBuilder(fullString);
                if (!string.IsNullOrEmpty(builder.UserID)) txtUserId.Text = builder.UserID;
                if (!string.IsNullOrEmpty(builder.Password)) txtPassword.Text = builder.Password;

                string dataSource = builder.DataSource;
                if (!string.IsNullOrEmpty(dataSource))
                {
                    var matchHost = Regex.Match(dataSource, @"HOST\s*=\s*([^)\s]+)", RegexOptions.IgnoreCase);
                    if (matchHost.Success) txtHost.Text = matchHost.Groups[1].Value;

                    var matchPort = Regex.Match(dataSource, @"PORT\s*=\s*(\d+)", RegexOptions.IgnoreCase);
                    if (matchPort.Success) txtPort.Text = matchPort.Groups[1].Value;

                    var matchService = Regex.Match(dataSource, @"SERVICE_NAME\s*=\s*([^)\s]+)", RegexOptions.IgnoreCase);
                    if (matchService.Success) txtServiceName.Text = matchService.Groups[1].Value;
                    else
                    {
                        var matchSid = Regex.Match(dataSource, @"SID\s*=\s*([^)\s]+)", RegexOptions.IgnoreCase);
                        if (matchSid.Success) txtServiceName.Text = matchSid.Groups[1].Value;
                    }
                }
                UpdateConnectionString();
            }
            catch (Exception ex) { MessageBox.Show("Erro ao processar string: " + ex.Message); }
        }
        private void AtualizarLabelArrastar()
        {
            // Se a lista estiver vazia (Count == 0), a label aparece (Visible = true)
            // Se tiver arquivos, ela some (Visible = false)
            label7.Visible = (lstImportFiles.Items.Count == 0);

            // Dica: Se a label ficar por cima da lista, traz ela pra frente
            if (label7.Visible) label7.BringToFront();
        }

        private void ImportarStringConexaoSimples(string rawString)
        {
            string pattern = @"^(?<user>[^/]+)/(?<pass>[^@]+)@(?://)?(?<host>[^:/]+):(?<port>\d+)/(?<service>.+)$";
            var match = Regex.Match(rawString.Trim(), pattern);

            if (match.Success)
            {
                txtUserId.Text = match.Groups["user"].Value;
                txtPassword.Text = match.Groups["pass"].Value;
                txtHost.Text = match.Groups["host"].Value;
                txtPort.Text = match.Groups["port"].Value;
                txtServiceName.Text = match.Groups["service"].Value;
                UpdateConnectionString();
                MessageBox.Show("Dados colados com sucesso!", "Importação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Formato inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private string BuildConnectionString()
        {
            return $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={txtHost.Text.Trim()})(PORT={txtPort.Text.Trim()}))(CONNECT_DATA=(SERVICE_NAME={txtServiceName.Text.Trim()})));User Id={txtUserId.Text.Trim()};Password={txtPassword.Text};";
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
                    MessageBox.Show("Conexão OK!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetStatus("OK");
                }
                else
                {
                    MessageBox.Show("Falha na conexão.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SetStatus("Falha");
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }



        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateConnectionString();
                SetStatus("Conectando...");
                if (_connectionManager.IsConnected) _connectionManager.Disconnect();

                _connectionManager.ConnectionString = txtConnectionString.Text.Trim();
                _connectionManager.Connect();

                _queryExecutor = new OracleQueryExecutor(_connectionManager);
                _metadataRepository = new MetadataRepository(_queryExecutor);
                _triggerManager = new TriggerManager(_queryExecutor, _metadataRepository);
                _constraintManager = new ConstraintManager(_queryExecutor, _metadataRepository);
                _tableManager = new TableManager(_queryExecutor, _metadataRepository);
                // _exportManager = new ExportManager(_queryExecutor, _metadataRepository);
                _importManager = new ImportManager(_queryExecutor);

                UpdateConnectionStatus();
                MessageBox.Show("Conectado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetStatus("Conectado");
            }
            catch (Exception ex)
            {
                UpdateConnectionStatus();
                MessageBox.Show($"Erro ao conectar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Erro");
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                _connectionManager.Disconnect();
                UpdateConnectionStatus();
                MessageBox.Show("Desconectado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetStatus("Desconectado");
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        #endregion

        #region Aba Triggers
        private void btnRefreshTriggers_Click(object sender, EventArgs e)
        {
            if (!CheckConnection()) return;
            try
            {
                SetStatus("Carregando triggers...");
                _allTriggers = _triggerManager.GetAllTriggers();
                checkedListTriggers.Items.Clear();
                foreach (var t in _allTriggers) checkedListTriggers.Items.Add(t);
                SetStatus($"{_allTriggers.Count} triggers carregadas");
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        private void btnDisableTriggers_Click(object sender, EventArgs e)
        {
            if (!CheckConnection() || checkedListTriggers.CheckedItems.Count == 0) return;
            if (MessageBox.Show("Desabilitar triggers selecionadas?", "Confirmar", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            try
            {
                List<string> list = new List<string>();
                foreach (TriggerInfo t in checkedListTriggers.CheckedItems) list.Add(t.TriggerName);
                int count = _triggerManager.DisableTriggers(list);
                MessageBox.Show($"{count} triggers desabilitadas.");
                btnRefreshTriggers_Click(sender, e);
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        private void btnEnableTriggers_Click(object sender, EventArgs e)
        {
            if (!CheckConnection() || checkedListTriggers.CheckedItems.Count == 0) return;
            try
            {
                List<string> list = new List<string>();
                foreach (TriggerInfo t in checkedListTriggers.CheckedItems) list.Add(t.TriggerName);
                int count = _triggerManager.EnableTriggers(list);
                MessageBox.Show($"{count} triggers habilitadas.");
                btnRefreshTriggers_Click(sender, e);
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        private void btnDisableAllTriggers_Click(object sender, EventArgs e)
        {
            if (!CheckConnection()) return;
            if (_allTriggers == null || _allTriggers.Count == 0) btnRefreshTriggers_Click(sender, e);
            if (_allTriggers == null || _allTriggers.Count == 0) return;

            if (MessageBox.Show($"ATENÇÃO: Isso irá desabilitar TODAS as {_allTriggers.Count} triggers listadas.\nDeseja continuar?",
                "Confirmação em Massa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            try
            {
                SetStatus("Desabilitando TODAS as triggers...");
                List<string> allTriggerNames = new List<string>();
                foreach (TriggerInfo t in _allTriggers) allTriggerNames.Add(t.TriggerName);
                int count = _triggerManager.DisableTriggers(allTriggerNames);
                MessageBox.Show($"{count} triggers foram desabilitadas com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnRefreshTriggers_Click(sender, e);
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnSelectAllTriggers_Click(object sender, EventArgs e) { for (int i = 0; i < checkedListTriggers.Items.Count; i++) checkedListTriggers.SetItemChecked(i, true); }
        private void btnDeselectAllTriggers_Click(object sender, EventArgs e) { for (int i = 0; i < checkedListTriggers.Items.Count; i++) checkedListTriggers.SetItemChecked(i, false); }
        #endregion

        #region Aba Constraints
        private void btnRefreshConstraints_Click(object sender, EventArgs e)
        {
            if (!CheckConnection()) return;
            try
            {
                SetStatus("Carregando constraints...");
                _allConstraints = _constraintManager.GetAllConstraints();
                checkedListConstraints.Items.Clear();
                foreach (var c in _allConstraints) checkedListConstraints.Items.Add(c);
                SetStatus($"{_allConstraints.Count} constraints carregadas");
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        private void btnDisableConstraints_Click(object sender, EventArgs e)
        {
            if (!CheckConnection() || checkedListConstraints.CheckedItems.Count == 0) return;
            if (MessageBox.Show("Desabilitar constraints selecionadas?", "Confirmar", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            try
            {
                List<ConstraintInfo> list = new List<ConstraintInfo>();
                foreach (ConstraintInfo c in checkedListConstraints.CheckedItems) list.Add(c);
                int count = _constraintManager.DisableConstraints(list);
                MessageBox.Show($"{count} constraints desabilitadas.");
                btnRefreshConstraints_Click(sender, e);
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        private void btnEnableConstraints_Click(object sender, EventArgs e)
        {
            if (!CheckConnection() || checkedListConstraints.CheckedItems.Count == 0) return;
            try
            {
                List<ConstraintInfo> list = new List<ConstraintInfo>();
                foreach (ConstraintInfo c in checkedListConstraints.CheckedItems) list.Add(c);
                int count = _constraintManager.EnableConstraints(list);
                MessageBox.Show($"{count} constraints habilitadas.");
                btnRefreshConstraints_Click(sender, e);
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        private void btnDisableFKandCheck_Click(object sender, EventArgs e)
        {
            if (!CheckConnection()) return;
            if (_allConstraints == null || _allConstraints.Count == 0) btnRefreshConstraints_Click(sender, e);
            if (_allConstraints == null || _allConstraints.Count == 0) return;

            var target = _allConstraints.FindAll(c => c.ConstraintType == "R" || c.ConstraintType == "C");
            if (target.Count == 0) { MessageBox.Show("Nenhuma constraint do tipo FK ou Check encontrada na lista.", "Aviso"); return; }

            if (MessageBox.Show($"Deseja desabilitar todas as {target.Count} constraints do tipo Foreign Key e Check?",
                "Confirmação de Filtro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                SetStatus($"Desabilitando {target.Count} constraints...");
                int count = _constraintManager.DisableConstraints(target);
                MessageBox.Show($"{count} constraints (FK/Check) foram desabilitadas!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnRefreshConstraints_Click(sender, e);
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnSelectAllConstraints_Click(object sender, EventArgs e) { for (int i = 0; i < checkedListConstraints.Items.Count; i++) checkedListConstraints.SetItemChecked(i, true); }
        private void btnDeselectAllConstraints_Click(object sender, EventArgs e) { for (int i = 0; i < checkedListConstraints.Items.Count; i++) checkedListConstraints.SetItemChecked(i, false); }
        #endregion

        #region Aba Truncate
        private void btnRefreshTables_Click(object sender, EventArgs e)
        {
            if (!CheckConnection()) return;
            try
            {
                SetStatus("Carregando tabelas...");
                _allTables = _tableManager.GetAllTables();
                UpdateTruncateList(_allTables);
                SetStatus($"{_allTables.Count} tabelas carregadas");
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        private void btnTruncate_Click(object sender, EventArgs e)
        {
            if (!CheckConnection() || checkedListTables.CheckedItems.Count == 0) return;
            if (MessageBox.Show("ATENÇÃO: TRUNCATE APAGA TODOS OS DADOS PERMANENTEMENTE. Continuar?", "PERIGO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            try
            {
                List<string> names = new List<string>();
                foreach (TableInfo t in checkedListTables.CheckedItems) names.Add(t.TableName);
                int count = _tableManager.TruncateTables(names);
                MessageBox.Show($"{count} tabelas truncadas.");
                btnRefreshTables_Click(sender, e);
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        private void btnPurgeRecycleBin_Click(object sender, EventArgs e)
        {
            if (!CheckConnection()) return;
            if (MessageBox.Show("Deseja executar 'PURGE RECYCLEBIN'?\nIsso apagará permanentemente todos os objetos da lixeira.",
                "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            try
            {
                SetStatus("Limpando lixeira...");
                _connectionManager.ExecuteNonQuery("PURGE RECYCLEBIN");
                MessageBox.Show("Lixeira limpa com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetStatus("Lixeira limpa");
            }
            catch (Exception ex) { MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnGatherStats_Click(object sender, EventArgs e)
        {
            if (!CheckConnection()) return;
            if (MessageBox.Show("Deseja atualizar as estatísticas do esquema (Schema Stats)?\nIsso pode levar alguns minutos.",
                "Confirmar Atualização", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            SetStatus("Gerando estatísticas... (Aguarde)");
            this.Cursor = Cursors.WaitCursor;
            System.ComponentModel.BackgroundWorker worker = new System.ComponentModel.BackgroundWorker();

            worker.DoWork += (s, args) => _connectionManager.ExecuteNonQuery("BEGIN dbms_stats.gather_schema_stats(user); END;");
            worker.RunWorkerCompleted += (s, args) =>
            {
                this.Cursor = Cursors.Default;
                if (args.Error != null) MessageBox.Show($"Erro: {args.Error.Message}", "Erro");
                else
                {
                    MessageBox.Show("Estatísticas atualizadas com sucesso!", "Sucesso");
                    SetStatus("Estatísticas atualizadas");
                    btnRefreshTables_Click(sender, e);
                }
            };
            worker.RunWorkerAsync();
        }

        private void btnSelectAllTables_Click(object sender, EventArgs e) { for (int i = 0; i < checkedListTables.Items.Count; i++) checkedListTables.SetItemChecked(i, true); }
        private void btnDeselectAllTables_Click(object sender, EventArgs e) { for (int i = 0; i < checkedListTables.Items.Count; i++) checkedListTables.SetItemChecked(i, false); }
        #endregion

        #region Aba Habilitar Constraints (Filtro)
        private void btnRefreshEnableConstraints_Click(object sender, EventArgs e)
        {
            if (!CheckConnection()) return;
            try
            {
                SetStatus("Carregando constraints desabilitadas...");
                _allConstraints = _constraintManager.GetAllConstraints();
                var disabled = _allConstraints.FindAll(c => !c.IsEnabled && c.ConstraintType != "R");
                var filtered = ApplyConstraintFilter(disabled);
                checkedListEnableConstraints.Items.Clear();
                foreach (var c in filtered) checkedListEnableConstraints.Items.Add(c);
                SetStatus($"{filtered.Count} listadas");
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        private List<ConstraintInfo> ApplyConstraintFilter(List<ConstraintInfo> constraints)
        {
            var types = new List<string>();
            if (chkForeign.Checked) types.Add("R");
            if (chkPrimary.Checked) types.Add("P");
            if (chkUnique.Checked) types.Add("U");
            if (chkCheck.Checked) types.Add("C");
            return constraints.FindAll(c => types.Contains(c.ConstraintType));
        }
        private void chkFilter_CheckedChanged(object sender, EventArgs e) { btnRefreshEnableConstraints_Click(sender, e); }

        private void btnEnableSelectedConstraints_Click(object sender, EventArgs e)
        {
            if (!CheckConnection() || checkedListEnableConstraints.CheckedItems.Count == 0) return;
            if (MessageBox.Show("Habilitar selecionadas?", "Confirmação", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            try
            {
                List<ConstraintInfo> list = new List<ConstraintInfo>();
                foreach (ConstraintInfo c in checkedListEnableConstraints.CheckedItems) list.Add(c);
                int count = _constraintManager.EnableConstraints(list);
                MessageBox.Show($"{count} habilitadas.");
                btnRefreshEnableConstraints_Click(sender, e);
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }
        private void btnSelectAllEnableConstraints_Click(object sender, EventArgs e) { for (int i = 0; i < checkedListEnableConstraints.Items.Count; i++) checkedListEnableConstraints.SetItemChecked(i, true); }
        private void btnDeselectAllEnableConstraints_Click(object sender, EventArgs e) { for (int i = 0; i < checkedListEnableConstraints.Items.Count; i++) checkedListEnableConstraints.SetItemChecked(i, false); }
        #endregion

        #region Aba Exportação DDL

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_connectionManager.IsConnected) return;
            if (tabControl.SelectedTab.Text.Contains("Habilitar Constraints")) btnRefreshEnableConstraints_Click(sender, e);
            else if (tabControl.SelectedTab == tabTruncate) btnRefreshTables_Click(sender, e);
            else if (tabControl.SelectedTab == tabConstraints) btnRefreshConstraints_Click(sender, e);
            else if (tabControl.SelectedTab == tabTriggers) btnRefreshTriggers_Click(sender, e);
        }

        #endregion

        #region Aba Importação (Scripts)
        private void btnSelectImportFiles_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = true;
                // --- ALTERAÇÃO AQUI: Adicionado *.7z no filtro ---
                ofd.Filter = "Arquivos de Script (*.sql;*.pdc;*.7z)|*.sql;*.pdc;*.7z|Todos os Arquivos (*.*)|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (string file in ofd.FileNames)
                    {
                        if (!lstImportFiles.Items.Contains(file)) lstImportFiles.Items.Add(file);
                    }
                    SetStatus($"{lstImportFiles.Items.Count} arquivos na fila.");
                    AtualizarLabelArrastar();
                }
            }
        }

        private void btnClearImportList_Click(object sender, EventArgs e)
        {
            lstImportFiles.Items.Clear();
            SetStatus("Lista de importação limpa.");
            AtualizarLabelArrastar();
        }
        private void btnRunMaintenance_Click(object sender, EventArgs e)
        {
            if (!CheckConnection()) return;

            // 1. Captura opções marcadas DIRETAMENTE pelas variáveis
            bool doTriggers = chkDisableAllTriggers.Checked;
            bool doConstraints = chkDisableFKAndCheck.Checked;
            bool doPurge = chkPurgeRecycleBin.Checked;
            bool doStats = checkBox2.Checked;


            // Verifica se NENHUMA opção foi marcada
            if (!doTriggers && !doConstraints && !doPurge && !doStats)
            {
                MessageBox.Show("Selecione ao menos uma opção para executar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Confirmar execução das tarefas de manutenção selecionadas?", "Confirmação",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            // 2. Prepara UI
            this.Cursor = Cursors.WaitCursor;
            btnRunMaintenance.Enabled = false;
            grpMaintenanceActions.Enabled = false;
            SetStatus("Iniciando manutenção...");

            // 3. Executa em Background
            System.ComponentModel.BackgroundWorker worker = new System.ComponentModel.BackgroundWorker();
            worker.WorkerReportsProgress = true;

            worker.DoWork += (s, args) =>
            {
                try
                {
                    // A. Desabilitar Triggers
                    if (doTriggers)
                    {
                        worker.ReportProgress(0, "Carregando e desabilitando Triggers...");
                        var triggers = _triggerManager.GetAllTriggers();
                        if (triggers.Count > 0)
                        {
                            List<string> names = new List<string>();
                            foreach (var t in triggers) names.Add(t.TriggerName);
                            _triggerManager.DisableTriggers(names);
                        }
                    }

                    // B. Desabilitar FK e Checks
                    if (doConstraints)
                    {
                        worker.ReportProgress(0, "Carregando e desabilitando Constraints...");
                        var constraints = _constraintManager.GetAllConstraints();
                        var target = constraints.FindAll(c => c.ConstraintType == "R" || c.ConstraintType == "C");
                        if (target.Count > 0)
                        {
                            _constraintManager.DisableConstraints(target);
                        }
                    }

                    // C. Purge RecycleBin
                    if (doPurge)
                    {
                        worker.ReportProgress(0, "Limpando Lixeira (Purge)...");
                        _connectionManager.ExecuteNonQuery("PURGE RECYCLEBIN");
                    }

                    // D. Stats
                    if (doStats)
                    {
                        worker.ReportProgress(0, "Gerando Estatísticas...");
                        _connectionManager.ExecuteNonQuery("BEGIN dbms_stats.gather_schema_stats(user); END;");
                    }

                    args.Result = "Sucesso";
                }
                catch (Exception ex)
                {
                    args.Result = "Erro: " + ex.Message;
                }
            };

            worker.ProgressChanged += (s, args) =>
            {
                SetStatus(args.UserState.ToString());
            };

            worker.RunWorkerCompleted += (s, args) =>
            {
                this.Cursor = Cursors.Default;
                btnRunMaintenance.Enabled = true;
                grpMaintenanceActions.Enabled = true;

                if (args.Result.ToString().StartsWith("Erro"))
                {
                    MessageBox.Show(args.Result.ToString(), "Erro na Execução", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SetStatus("Erro na manutenção");
                }
                else
                {
                    MessageBox.Show("Todas as tarefas selecionadas foram concluídas!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetStatus("Manutenção concluída");
                }
            };

            worker.RunWorkerAsync();
        }
        private void btnRunImport_Click(object sender, EventArgs e)
        {
            if (!CheckConnection()) return;
            if (lstImportFiles.Items.Count == 0) { MessageBox.Show("Selecione arquivos.", "Aviso"); return; }

            if (MessageBox.Show($"Executar {lstImportFiles.Items.Count} scripts?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                SetStatus("Importando...");
                this.Cursor = Cursors.WaitCursor;
                btnRunImport.Enabled = false;
                rtbImportLog.Clear();
                AppendLog($"--- Início: {DateTime.Now} ---", Color.Blue);

                if (_importManager == null) _importManager = new ImportManager(_queryExecutor);

                List<string> files = new List<string>();
                foreach (var item in lstImportFiles.Items) files.Add(item.ToString());

                System.ComponentModel.BackgroundWorker worker = new System.ComponentModel.BackgroundWorker();
                worker.DoWork += (s, args) =>
                {
                    _importManager.ExecuteFiles(files, (msg) =>
                    {
                        Color color = Color.Black;
                        if (msg.Contains("[ERRO]") || msg.Contains("[FATAL]")) color = Color.Red;
                        else if (msg.StartsWith("Concluído") || msg.StartsWith("OK")) color = Color.Green;
                        else color = Color.DarkGray;
                        AppendLog(msg, color);
                    });
                };

                worker.RunWorkerCompleted += (s, args) =>
                {
                    this.Cursor = Cursors.Default;
                    btnRunImport.Enabled = true;
                    AppendLog($"--- Fim: {DateTime.Now} ---", Color.Blue);
                    MessageBox.Show("Processo finalizado. Verifique o log.", "Sucesso");
                    SetStatus("Importação concluída.");
                };
                worker.RunWorkerAsync();
            }
            catch (Exception ex) { this.Cursor = Cursors.Default; btnRunImport.Enabled = true; MessageBox.Show("Erro: " + ex.Message); }
        }

        private void AppendLog(string text, Color color)
        {
            if (rtbImportLog.InvokeRequired) rtbImportLog.Invoke(new Action<string, Color>(AppendLog), text, color);
            else
            {
                rtbImportLog.SelectionStart = rtbImportLog.TextLength;
                rtbImportLog.SelectionLength = 0;
                rtbImportLog.SelectionColor = color;
                rtbImportLog.AppendText(text + Environment.NewLine);
                rtbImportLog.SelectionColor = rtbImportLog.ForeColor;
                rtbImportLog.ScrollToCaret();
            }
        }
        #endregion


        private void btnRunRestore_Click(object sender, EventArgs e)
        {
            if (!CheckConnection()) return;

            // 1. Captura opções
            bool enableTriggers = chkEnableAllTriggers.Checked;
            bool enableConstraints = chkEnableFKAndCheck.Checked;
            bool doSequences = chkResetSequences.Checked;
            bool doStats = chkGatherStats.Checked;

            if (!enableTriggers && !enableConstraints && !doSequences && !doStats)
            {
                MessageBox.Show("Selecione ao menos uma opção para habilitar/restaurar.", "Aviso");
                return;
            }

            if (MessageBox.Show("Deseja executar as tarefas selecionadas?", "Confirmar Restauração",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            this.Cursor = Cursors.WaitCursor;
            btnRunRestore.Enabled = false;
            grpRestoreActions.Enabled = false;
            SetStatus("Iniciando restauração...");

            System.ComponentModel.BackgroundWorker worker = new System.ComponentModel.BackgroundWorker();
            worker.WorkerReportsProgress = true;

            worker.DoWork += (s, args) =>
            {
                try
                {
                    // A. Habilitar Triggers
                    if (enableTriggers)
                    {
                        worker.ReportProgress(0, "Habilitando Triggers...");
                        var triggers = _triggerManager.GetAllTriggers();
                        if (triggers.Count > 0)
                        {
                            List<string> names = new List<string>();
                            foreach (var t in triggers) names.Add(t.TriggerName);
                            _triggerManager.EnableTriggers(names);
                        }
                    }

                    // B. Habilitar Constraints
                    if (enableConstraints)
                    {
                        worker.ReportProgress(0, "Habilitando Constraints...");
                        var allConstraints = _constraintManager.GetAllConstraints();
                        var target = allConstraints.FindAll(c => c.ConstraintType == "R" || c.ConstraintType == "C");
                        if (target.Count > 0) _constraintManager.EnableConstraints(target);
                    }

                    // C. Resetar Sequences (ADICIONADO)
                    if (doSequences)
                    {
                        worker.ReportProgress(0, "Resetando Sequences...");
                        _connectionManager.ExecuteNonQuery("BEGIN prc_wms_util_reset_sequence(gravar=>'S'); END;");
                    }

                    // D. Gerar Estatísticas (ADICIONADO)
                    if (doStats)
                    {
                        worker.ReportProgress(0, "Gerando Estatísticas (Pode demorar)...");
                        _connectionManager.ExecuteNonQuery("BEGIN dbms_stats.gather_schema_stats(user); END;");
                    }

                    args.Result = "Sucesso";
                }
                catch (Exception ex)
                {
                    args.Result = "Erro: " + ex.Message;
                }
            };

            worker.ProgressChanged += (s, args) => SetStatus(args.UserState.ToString());

            worker.RunWorkerCompleted += (s, args) =>
            {
                this.Cursor = Cursors.Default;
                btnRunRestore.Enabled = true;
                grpRestoreActions.Enabled = true;

                if (args.Result.ToString().StartsWith("Erro"))
                {
                    MessageBox.Show(args.Result.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SetStatus("Erro na restauração");
                }
                else
                {
                    MessageBox.Show("Processo de restauração concluído com sucesso!", "Concluído", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetStatus("Restauração concluída");
                }
            };

            worker.RunWorkerAsync();
        }
        private void UpdateTruncateList(List<TableInfo> tablesToShow)
        {
            checkedListTables.Items.Clear();
            if (tablesToShow == null) return;
            foreach (var t in tablesToShow)
            {
                checkedListTables.Items.Add(t);
            }
        }
        private bool CheckConnection()
        {
            if (!_connectionManager.IsConnected)
            {
                MessageBox.Show("Não conectado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void SetStatus(string msg) { toolStripStatusLabel.Text = msg; statusStrip.Refresh(); }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) { if (_connectionManager?.IsConnected == true) _connectionManager.Disconnect(); }
        private void txtServiceName_TextChanged(object sender, EventArgs e) { }
        private void MainForm_Load(object sender, EventArgs e) { }

        private void validatorbtn(object sender, EventArgs e)
        {

            ProcessStartInfo parametro = new ProcessStartInfo("cmd.exe", "/C " + @"net use \\172.25.100.248 wms246@. /USER:wms246")
            {
                RedirectStandardOutput = true, // Redireciona a saída do comando
                UseShellExecute = false,
                CreateNoWindow = true // Oculta a janela do cmd
            };
            using (Process process = Process.Start(parametro))
            {
                // Lê a saída do comando
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

            }

            if (VerificaAtu())
            {
                string caminhoExe = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ImportFullUpdater.exe");
                Process.Start(caminhoExe);
                this.Close();
            }


        }

        public static bool VerificaAtu()
        {
            try
            {
                
                    if (Acesso248())
                    {
                        string caminhoVersion = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "version.txt");
                        if (!File.Exists(caminhoVersion))
                        {
                            File.WriteAllText(caminhoVersion, Assembly.GetExecutingAssembly().GetName().Version.ToString());
                        }
                        string currentAssembly = File.ReadAllText(caminhoVersion);
                        string version = File.ReadAllText($@"\\172.25.100.248\wms246\Builds\WMS\Outros\Import_Full_Updater\Atu\version.txt");

                        if (currentAssembly != version)
                        {
                            DialogResult dialogo = MessageBox.Show($"Existe uma atualização disponível:\nSua versão: {currentAssembly.Split(new[] { "\n" }, StringSplitOptions.None)[0]}\nNova versão: \n\n{version}\nDeseja Atualizar?", "Atualização detectada", MessageBoxButtons.YesNo);
                            if (dialogo == DialogResult.Yes)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }


                    }
                    return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public static bool Acesso248()
        {
            try
            {
                Directory.GetFiles($@"\\172.25.100.248\wms246");
                return true;
            }
            catch { return false; }
        }
    }
}