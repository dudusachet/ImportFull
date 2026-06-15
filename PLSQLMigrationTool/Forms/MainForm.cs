using Oracle.ManagedDataAccess.Client;
using PLSQLImportFull.Business;
using PLSQLImportFull.Data;
using PLSQLImportFull.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq; // Importante para o Cast
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
        private bool _isBlueTheme;
        private Color _corDestaqueAtual;
        private ToolStripStatusLabel lblDbNameFooter;

        // Variável da bolinha
        private ToolStripStatusLabel lblStatusIcon;
        private bool _truncateSortAscName = true;
        private bool _truncateSortAscRows = false;
        private List<TriggerInfo> _allTriggers;
        private List<ConstraintInfo> _allConstraints;
        private List<TableInfo> _allTables;
        private List<ConstraintInfo> _allEnableConstraints; // Auxiliar para o filtro

        public MainForm()
        {
            InitializeComponent();

            _isBlueTheme = Properties.Settings.Default.IsBlueTheme;

            AplicarEstiloModerno();
            ConfigurarRodapeBanco();

            this.KeyPreview = true;
            this.KeyDown += MainForm_KeyDown;

            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new System.Drawing.Size(850, 600);

            // =========================================================
            // 1. CONFIGURAÇÃO DA LISTA DE ARQUIVOS (lstImportFiles)
            // =========================================================
            this.lstImportFiles.AllowDrop = true;
            this.lstImportFiles.DragEnter += LstImportFiles_DragEnter;
            this.lstImportFiles.DragDrop += LstImportFiles_DragDrop;

            this.label7.AllowDrop = true;
            this.label7.DragEnter += LstImportFiles_DragEnter;
            this.label7.DragDrop += LstImportFiles_DragDrop;

            // =========================================================
            // 2. MENU DE CONTEXTO
            // =========================================================
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem copyItem = new ToolStripMenuItem("Copiar Seleção");
            copyItem.Click += (s, e) => { if (rtbImportLog.SelectedText.Length > 0) rtbImportLog.Copy(); };
            ToolStripMenuItem clearItem = new ToolStripMenuItem("Limpar Log");
            clearItem.Click += (s, e) => { rtbImportLog.Clear(); };
            contextMenu.Items.Add(copyItem);
            contextMenu.Items.Add(new ToolStripSeparator());
            contextMenu.Items.Add(clearItem);
            rtbImportLog.ContextMenuStrip = contextMenu;

            // =========================================================
            // 3. CONFIGURAÇÕES GERAIS DE UI
            // =========================================================
            this.txtConnectionString.ReadOnly = true;
            this.txtConnectionString.BackColor = SystemColors.Control;

            lblStatusIcon = new ToolStripStatusLabel();
            lblStatusIcon.Text = "●";
            lblStatusIcon.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblStatusIcon.ForeColor = Color.Gray;
            lblStatusIcon.Alignment = ToolStripItemAlignment.Left;
            lblStatusIcon.Margin = new Padding(0, -5, -8, 0);

            this.statusStrip.Items.Insert(0, lblStatusIcon);
            this.toolStripStatusLabel.Spring = true;
            this.toolStripStatusLabel.TextAlign = ContentAlignment.MiddleLeft;

            // =========================================================
            // 4. DEPENDÊNCIAS
            // =========================================================
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

            // Carrega configs
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

            UpdateConnectionString();
            UpdateConnectionStatus();
            AtualizarLabelArrastar();
        }

        private void ConfigurarRodapeBanco()
        {
            // Verifica se a variável do designer existe (geralmente statusStrip ou statusStrip1)
            // Se der erro na linha abaixo, troque 'this.statusStrip' por 'this.statusStrip1'
            // 2. CRIAR LABEL DO RODAPÉ (Corrigido para ficar à Direita)
            if (this.statusStrip != null)
            {
                // --- PASSO IMPORTANTE: Empurrar tudo para a direita ---
                // Procura o primeiro Label existente (o que mostra "Pronto" ou "Conectado")
                // e diz para ele ocupar todo o espaço sobrando.
                foreach (ToolStripItem item in this.statusStrip.Items)
                {
                    if (item is ToolStripStatusLabel labelExistente)
                    {
                        labelExistente.Spring = true;
                        labelExistente.TextAlign = ContentAlignment.MiddleLeft; // Mantém o texto dele na esquerda
                        break; // Só precisa fazer no primeiro
                    }
                }
                // ------------------------------------------------------

                lblDbNameFooter = new ToolStripStatusLabel();
                lblDbNameFooter.Text = "";
                lblDbNameFooter.ForeColor = Color.FromArgb(49, 49, 48);
                lblDbNameFooter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                lblDbNameFooter.BorderSides = ToolStripStatusLabelBorderSides.Left;
                lblDbNameFooter.BorderStyle = Border3DStyle.Etched;
                lblDbNameFooter.Padding = new Padding(10, 0, 0, 0);

                // Garante o alinhamento
                lblDbNameFooter.Alignment = ToolStripItemAlignment.Right;

                this.statusStrip.Items.Add(lblDbNameFooter);
            }
            else
            {
                MessageBox.Show("Erro: Não foi possível encontrar o componente statusStrip no formulário.");
            }
        }

        // ===================================================================================
        // LÓGICA VISUAL MODERNA (Aplicada no Load para garantir que o Windows obedeça)
        // ===================================================================================

        private void General_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Proteção
            if (e.Index < 0) return;

            ListBox list = (ListBox)sender;
            CheckedListBox chkList = list as CheckedListBox;

            // --- CORES DO TEMA ---
            Color corTexto = Color.FromArgb(49, 49, 48);           // Cinza Chumbo
            Color corFundoNormal = Color.White;                    // Branco
            Color corFundoSelecao = Color.FromArgb(245, 246, 250); // Cinza MUITO claro (Substitui o Azul)
            Color corDestaque = Color.FromArgb(229, 35, 41);       // Vermelho (para bordas/detalhes se quiser)

            // 1. PINTAR O FUNDO
            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            using (SolidBrush bgBrush = new SolidBrush(isSelected ? corFundoSelecao : corFundoNormal))
            {
                e.Graphics.FillRectangle(bgBrush, e.Bounds);
            }

            int textoOffset = 5;

            // 2. DESENHAR O CHECKBOX (Se for CheckedListBox)
            if (chkList != null)
            {
                int boxSize = 14;
                int boxY = e.Bounds.Y + (e.Bounds.Height - boxSize) / 2;
                int boxX = e.Bounds.X + 4;
                Rectangle boxRect = new Rectangle(boxX, boxY, boxSize, boxSize);

                bool isChecked = chkList.GetItemChecked(e.Index);

                using (Pen penBorder = new Pen(corTexto, 1))
                using (SolidBrush brushFill = new SolidBrush(corTexto))
                {
                    if (isChecked)
                    {
                        // Marcado: Quadrado cheio
                        e.Graphics.FillRectangle(brushFill, boxRect);
                        // Vzinho branco
                        e.Graphics.DrawLine(new Pen(Color.White, 2), boxX + 3, boxY + 6, boxX + 5, boxY + 10);
                        e.Graphics.DrawLine(new Pen(Color.White, 2), boxX + 5, boxY + 10, boxX + 11, boxY + 3);
                    }
                    else
                    {
                        // Desmarcado: Só borda fina
                        e.Graphics.DrawRectangle(penBorder, boxRect);
                    }
                }
                textoOffset = boxSize + 10;
            }

            // 3. DESENHAR O TEXTO
            // Tenta pegar o texto (pode ser objeto complexo)
            string text = list.Items[e.Index].ToString();

            // Define a área do texto
            Rectangle textRect = new Rectangle(
                e.Bounds.X + textoOffset + 4,
                e.Bounds.Y,
                e.Bounds.Width - textoOffset - 4,
                e.Bounds.Height
            );

            // Renderiza o texto
            TextRenderer.DrawText(e.Graphics, text, list.Font, textRect, corTexto,
                                  TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
        }

        private void AplicarEstiloModerno()
        {

            Color corVermelha = Color.FromArgb(229, 35, 41);  //Vermelho Fullsoft
            Color corAzul = Color.FromArgb(13, 128, 191);     // Azul G
            Color corTexto = Color.FromArgb(49, 49, 48);      // Cinza Escuro
            Color corFundo = Color.FromArgb(245, 246, 250);   // Off-White

            _corDestaqueAtual = _isBlueTheme ? corAzul : corVermelha;

            this.BackColor = corFundo;
            this.ForeColor = corTexto;
            this.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular);

            EstilizarControlesRecursivo(this, _corDestaqueAtual, corTexto);
        }

        private void EstilizarControlesRecursivo(Control container, Color corDestaque, Color corTexto)
        {
            foreach (Control c in container.Controls)
            {
                if (c is Button btn)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.BackColor = corDestaque;
                    btn.ForeColor = Color.White;
                    btn.Cursor = Cursors.Hand;
                    btn.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);

                    if (btn.Name.ToLower().Contains("cancel") || btn.Name.ToLower().Contains("clear") || btn.Name.ToLower().Contains("disconnect"))
                    {
                        btn.BackColor = Color.FromArgb(189, 195, 199);
                        btn.ForeColor = Color.Black;
                    }
                }
                else if (c is TextBox txt)
                {
                    txt.BorderStyle = BorderStyle.FixedSingle;
                    txt.BackColor = Color.White;
                    txt.ForeColor = corTexto;
                }
                else if (c is CheckBox chk)
                {
                    chk.FlatStyle = FlatStyle.Flat;
                    chk.FlatAppearance.BorderSize = 0;
                    chk.Cursor = Cursors.Hand;
                    chk.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular);
                    chk.ForeColor = corTexto;

                    // Pintura customizada do checkbox isolado
                    chk.Paint -= CheckBox_Paint;
                    chk.Paint += CheckBox_Paint;
                }
                else if (c is Label lbl)
                {
                    // --- AQUI ESTÁ A LÓGICA ESPECÍFICA ---

                    // Verifica se é um dos labels que você quer destacar
                    if (lbl.Name == "label8" || lbl.Name == "label9" || lbl.Name == "label10")
                    {
                        lbl.ForeColor = Color.Red; // Vermelho Chamativo
                                                   // Opcional: Colocar em negrito para destacar ainda mais
                        lbl.Font = new Font("Segoe UI", lbl.Font.Size, FontStyle.Bold);
                    }
                    else
                    {
                        // Todos os outros labels seguem o tema (Cinza)
                        lbl.ForeColor = corTexto;
                    }
                }

                if (c.HasChildren) EstilizarControlesRecursivo(c, corDestaque, corTexto);
            }
        }

        private void CheckBox_Paint(object sender, PaintEventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            e.Graphics.Clear(this.BackColor);

            Color corTema = Color.FromArgb(49, 49, 48);
            int boxSize = 14;
            int boxY = (chk.Height - boxSize) / 2;
            int boxX = 0;
            Rectangle boxRect = new Rectangle(boxX, boxY, boxSize, boxSize);

            using (Pen penBorder = new Pen(corTema, 1))
            using (SolidBrush brushFill = new SolidBrush(corTema))
            {
                if (chk.Checked)
                {
                    e.Graphics.FillRectangle(brushFill, boxRect);
                    Point p1 = new Point(boxX + 3, boxY + 6);
                    Point p2 = new Point(boxX + 5, boxY + 10);
                    Point p3 = new Point(boxX + 11, boxY + 3);
                    using (Pen penCheck = new Pen(Color.White, 2))
                    {
                        e.Graphics.DrawLine(penCheck, p1, p2);
                        e.Graphics.DrawLine(penCheck, p2, p3);
                    }
                }
                else
                {
                    e.Graphics.DrawRectangle(penBorder, boxRect);
                }
            }

            Rectangle textRect = new Rectangle(boxSize + 5, 0, chk.Width - boxSize - 5, chk.Height);
            TextRenderer.DrawText(e.Graphics, chk.Text, chk.Font, textRect, chk.ForeColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
        }

        // =========================================================
        // MÉTODOS DE NEGÓCIO (Mantidos intactos)
        // =========================================================

        private void ExportForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_connectionManager != null && _connectionManager.IsConnected)
                _connectionManager.Disconnect();

            Properties.Settings.Default.LastHost = txtHost.Text.Trim();
            Properties.Settings.Default.LastPort = txtPort.Text.Trim();
            Properties.Settings.Default.LastService = txtServiceName.Text.Trim();
            Properties.Settings.Default.LastUser = txtUserId.Text.Trim();
            Properties.Settings.Default.LastPassword = txtPassword.Text;
            Properties.Settings.Default.Save();
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

        private void UpdateConnectionStatus()
        {
            bool isConnected = _connectionManager.IsConnected;

            if (isConnected)
            {
                lblConnectionStatus.Text = "🟢 Status: Conectado";
                lblConnectionStatus.ForeColor = Color.Green;
                lblStatusIcon.ForeColor = Color.Green;
                if (lblDbNameFooter != null)
                {
                    string db = txtServiceName.Text.Trim().ToUpper();
                    string host = txtHost.Text.Trim();
                    // AQUI É ONDE O TEXTO APARECE DE VERDADE
                    lblDbNameFooter.Text = $"{txtUserId.Text.ToUpper()} | {db}@{host}";
                    lblDbNameFooter.ForeColor = Color.FromArgb(49, 49, 48);
                }

            }
            else
            {
                lblConnectionStatus.Text = "🔴 Status: Desconectado";
                lblConnectionStatus.ForeColor = Color.Red;
                lblStatusIcon.ForeColor = Color.Red;
                if (lblDbNameFooter != null)
                {
                    lblDbNameFooter.Text = "Sem conexão"; // Texto padrão quando desconectado
                    lblDbNameFooter.ForeColor = Color.Gray;
                }
            }
            lblConnectionStatus.BackColor = Color.Transparent;

            bool enableInputs = !isConnected;
            txtHost.Enabled = enableInputs;
            txtPort.Enabled = enableInputs;
            txtServiceName.Enabled = enableInputs;
            txtUserId.Enabled = enableInputs;
            txtPassword.Enabled = enableInputs;
            txtConnectionString.Enabled = enableInputs;
            btnConnect.Enabled = enableInputs;
            btnPasteString.Enabled = enableInputs;

            var btnLoad = this.Controls.Find("btnLoadConfig", true);
            if (btnLoad.Length > 0) btnLoad[0].Enabled = enableInputs;

            btnDisconnect.Enabled = isConnected;
        }

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
                            //MessageBox.Show("Configuração importada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnConnect_Click(this, EventArgs.Empty);
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
            _truncateSortAscName = !_truncateSortAscName;

            if (_truncateSortAscName)
            {
                _allTables.Sort((x, y) => string.Compare(x.TableName, y.TableName));
                btnSortTruncateName.Text = "Nome ▲";
            }
            else
            {
                _allTables.Sort((x, y) => string.Compare(y.TableName, x.TableName));
                btnSortTruncateName.Text = "Nome ▼";
            }
            btnSortTruncateRows.Text = "Ordenar Linhas";
            UpdateTruncateList(_allTables);
        }

        private void btnSortTruncateRows_Click(object sender, EventArgs e)
        {
            if (_allTables == null || _allTables.Count == 0) return;
            _truncateSortAscRows = !_truncateSortAscRows;

            if (_truncateSortAscRows)
            {
                _allTables.Sort((x, y) => x.NumRows.CompareTo(y.NumRows));
                btnSortTruncateRows.Text = "Linhas ▲";
            }
            else
            {
                _allTables.Sort((x, y) => y.NumRows.CompareTo(x.NumRows));
                btnSortTruncateRows.Text = "Linhas ▼";
            }
            btnSortTruncateName.Text = "Ordenar Nome";
            UpdateTruncateList(_allTables);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            // Atalho: Ctrl + Alt + G
            if (e.Control && e.Alt && e.KeyCode == Keys.G)
            {
                // 1. Inverte o valor
                _isBlueTheme = !_isBlueTheme;

                // 2. Salva nas configurações
                Properties.Settings.Default.IsBlueTheme = _isBlueTheme;
                Properties.Settings.Default.Save();

                // 3. Reaplica o visual
                AplicarEstiloModerno();

                // 4. Força redesenho da tela (importante para atualizar as listas)
                this.Refresh();
            }
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
            label7.Visible = (lstImportFiles.Items.Count == 0);
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
                btnConnect_Click(this, EventArgs.Empty);
                //MessageBox.Show("Dados colados com sucesso!", "Importação", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                _importManager = new ImportManager(_queryExecutor);

                UpdateConnectionStatus();
                //MessageBox.Show("Conectado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                //MessageBox.Show("Desconectado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetStatus("Desconectado");
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        private void btnRefreshTriggers_Click(object sender, EventArgs e)
        {
            if (!CheckConnection()) return;
            try
            {
                SetStatus("Carregando triggers...");

                // --- CORREÇÃO DO ERRO ---
                // Como o método exige 'bool enabled', buscamos as duas listas e juntamos
                var triggersAtivas = _triggerManager.GetAllTriggers(true);
                var triggersInativas = _triggerManager.GetAllTriggers(false);

                _allTriggers = new List<TriggerInfo>();

                if (triggersAtivas != null) _allTriggers.AddRange(triggersAtivas);
                if (triggersInativas != null) _allTriggers.AddRange(triggersInativas);
                // ------------------------

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

        private void btnRefreshConstraints_Click(object sender, EventArgs e)
        {
            if (!CheckConnection()) return;
            try
            {
                SetStatus("Carregando constraints...");
                _allConstraints = _constraintManager.GetAllConstraints(enabled: true);
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

                // MUDANÇA: Captura os erros
                string errosConstraints;
                int count = _constraintManager.EnableConstraints(list, out errosConstraints);

                // MUDANÇA: Mostra o popup se deu erro, senão mostra a mensagem de sucesso normal
                if (!string.IsNullOrEmpty(errosConstraints))
                {
                    MessageBox.Show(errosConstraints, "Aviso: Falhas em Constraints", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show($"{count} constraints habilitadas com sucesso.");
                }

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

        private void btnRefreshEnableConstraints_Click(object sender, EventArgs e)
        {
            if (!CheckConnection()) return;
            try
            {
                SetStatus("Carregando constraints desabilitadas...");
                _allEnableConstraints = _constraintManager.GetAllConstraints(enabled: true); // Ajuste: Aqui deveria ser enabled: false se o objetivo é buscar desabilitadas? Mantenho o original.
                // Na verdade, a lógica original estava comentada ou pegando tudo. Vou assumir buscar as que precisam ser habilitadas.
                var disabled = _constraintManager.GetAllConstraints(enabled: false);

                checkedListEnableConstraints.Items.Clear();
                foreach (var c in disabled) checkedListEnableConstraints.Items.Add(c);
                SetStatus($"{disabled.Count} listadas");
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
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

                // MUDANÇA: Captura os erros
                string errosConstraints;
                int count = _constraintManager.EnableConstraints(list, out errosConstraints);

                // MUDANÇA: Mostra o popup de aviso ou sucesso
                if (!string.IsNullOrEmpty(errosConstraints))
                {
                    MessageBox.Show(errosConstraints, "Aviso: Falhas em Constraints", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show($"{count} habilitadas com sucesso.");
                }

                btnRefreshEnableConstraints_Click(sender, e);
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }
        private void btnSelectAllEnableConstraints_Click(object sender, EventArgs e) { for (int i = 0; i < checkedListEnableConstraints.Items.Count; i++) checkedListEnableConstraints.SetItemChecked(i, true); }
        private void btnDeselectAllEnableConstraints_Click(object sender, EventArgs e) { for (int i = 0; i < checkedListEnableConstraints.Items.Count; i++) checkedListEnableConstraints.SetItemChecked(i, false); }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_connectionManager.IsConnected) return;
            if (tabControl.SelectedTab.Text.Contains("Habilitar Constraints")) btnRefreshEnableConstraints_Click(sender, e);
            else if (tabControl.SelectedTab == tabTruncate) btnRefreshTables_Click(sender, e);
            else if (tabControl.SelectedTab == tabConstraints) btnRefreshConstraints_Click(sender, e);
            else if (tabControl.SelectedTab == tabTriggers) btnRefreshTriggers_Click(sender, e);
        }

        private void btnSelectImportFiles_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = true;
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

            bool doTriggers = chkDisableAllTriggers.Checked;
            bool doConstraints = chkDisableFKAndCheck.Checked;
            bool doPurge = chkPurgeRecycleBin.Checked;
            bool doStats = checkBox2.Checked;
            bool doAllowNull = chkAllowNullUserMachine.Checked;
            bool doRebuild = checkBoxIndex.Checked;

            bool doCreateTable = false;
            if (this.Controls.Find("chkCreateValidatorTable", true).Length > 0)
                doCreateTable = ((CheckBox)this.Controls.Find("chkCreateValidatorTable", true)[0]).Checked;

            if (!doTriggers && !doConstraints && !doPurge && !doCreateTable && !doAllowNull && !doStats && !doRebuild)
            {
                MessageBox.Show("Selecione ao menos uma opção para executar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Confirmar execução das tarefas de manutenção selecionadas?", "Confirmação",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            this.Cursor = Cursors.WaitCursor;
            btnRunMaintenance.Enabled = false;
            grpMaintenanceActions.Enabled = false;
            SetStatus("Iniciando manutenção...");

            System.ComponentModel.BackgroundWorker worker = new System.ComponentModel.BackgroundWorker();
            worker.WorkerReportsProgress = true;

            worker.DoWork += (s, args) =>
            {
                try
                {
                    if (doTriggers)
                    {
                        worker.ReportProgress(0, "Carregando e desabilitando Triggers...");
                        var triggers = _triggerManager.GetAllTriggers(enabled: true);
                        if (triggers.Count > 0)
                        {
                            List<string> names = new List<string>();
                            foreach (var t in triggers) names.Add(t.TriggerName);
                            _triggerManager.DisableTriggers(names);
                        }
                    }

                    if (doConstraints)
                    {
                        worker.ReportProgress(0, "Desabilitando FK, Check");
                        var constraints = _constraintManager.GetAllConstraints(enabled: true);
                        if (constraints.Count > 0)
                        {
                            _constraintManager.DisableConstraints(constraints);
                        }
                    }

                    if (doPurge)
                    {
                        worker.ReportProgress(0, "Limpando Lixeira...");
                        _connectionManager.ExecuteNonQuery("PURGE RECYCLEBIN");
                    }

                    if (doAllowNull)
                    {
                        worker.ReportProgress(0, "Excluindo Checks de MAQUINA/USUARIO (Limpeza Total)...");
                        string sqlNull = @"
BEGIN
    FOR k IN (SELECT uc.table_name, uc.constraint_name FROM user_constraints uc JOIN user_cons_columns ucc ON uc.constraint_name = ucc.constraint_name WHERE uc.constraint_type = 'C' AND ucc.column_name IN ('MAQUINA', 'USUARIO') AND uc.table_name <> 'WMS_CHECKOUT') LOOP BEGIN EXECUTE IMMEDIATE 'ALTER TABLE ' || k.table_name || ' DROP CONSTRAINT ' || k.constraint_name; EXCEPTION WHEN OTHERS THEN NULL; END; END LOOP;
    FOR r IN (SELECT table_name, column_name FROM user_tab_columns WHERE column_name IN ('MAQUINA', 'USUARIO') AND nullable = 'N' AND table_name <> 'WMS_CHECKOUT') LOOP BEGIN EXECUTE IMMEDIATE 'ALTER TABLE ' || r.table_name || ' MODIFY ' || r.column_name || ' NULL'; EXCEPTION WHEN OTHERS THEN NULL; END; END LOOP;
END;";
                        _connectionManager.ExecuteNonQuery(sqlNull);
                    }
                    args.Result = "Sucesso";
                }
                catch (Exception ex) { args.Result = "Erro: " + ex.Message; }
            };

            worker.ProgressChanged += (s, args) => SetStatus(args.UserState.ToString());
            worker.RunWorkerCompleted += (s, args) =>
            {
                this.Cursor = Cursors.Default;
                btnRunMaintenance.Enabled = true;
                grpMaintenanceActions.Enabled = true;
                if (args.Result.ToString().StartsWith("Erro")) MessageBox.Show(args.Result.ToString(), "Erro");
                else MessageBox.Show("Manutenção concluída!", "Sucesso");
                SetStatus("Pronto");
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

        private void btnRunRestore_Click(object sender, EventArgs e)
        {
            if (!CheckConnection()) return;

            bool enableTriggers = chkEnableAllTriggers.Checked;
            bool enableConstraints = chkEnableFKAndCheck.Checked;
            bool doSequences = chkResetSequences.Checked;
            bool doStats = chkGatherStats.Checked;
            bool doRebuild = checkBoxIndex.Checked;
            bool doCompile = checkBox1.Checked;
            bool doCheck = chkValidarConstraints.Checked;

            if (!doRebuild && !enableTriggers && !enableConstraints && !doSequences && !doStats && !doCompile && !doCheck)
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
                    if (doRebuild)
                    {
                        worker.ReportProgress(0, "Refazendo Índices...");
                        string sqlRebuild = @"begin for cur in (select 'ALTER INDEX ' || i.index_name || ' REBUILD TABLESPACE ' || i.tablespace_name || ' storage (initial 64K)' as cmd from user_indexes i where i.index_type <> 'LOB' order by i.leaf_blocks, i.table_name, i.index_name) loop begin execute immediate cur.cmd; exception when others then null; end; end loop; end;";
                        _connectionManager.ExecuteNonQuery(sqlRebuild);
                    }

                    if (enableConstraints)
                    {
                        worker.ReportProgress(0, "Habilitando Constraints...");
                        var allConstraints = _constraintManager.GetAllConstraints(enabled: false);
                        if (allConstraints.Count > 0)
                        {
                            // MUDANÇA: Captura e mostra o erro de forma segura usando o this.Invoke (pois estamos num Worker)
                            string errosConstraints;
                            _constraintManager.EnableConstraints(allConstraints, out errosConstraints);

                            if (!string.IsNullOrEmpty(errosConstraints))
                            {
                                this.Invoke(new Action(() => {
                                    MessageBox.Show(errosConstraints, "Aviso: Falhas em Constraints", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }));
                            }
                        }
                    }

                    if (doSequences)
                    {
                        worker.ReportProgress(0, "Resetando Sequences...");
                        _connectionManager.ExecuteNonQuery(@"
create or replace procedure prc_wms_util_reset_sequence(gravar            in char default 'N',
                                                        mostra_seq_maior in char default 'S') is
   ----------------------
   -- Versão 22.15.004
   ----------------------
   cursor sequence_cursor is
      select us.sequence_name as seq_name, t.table_name, t.column_name, us.last_number, us.min_value
        from user_sequences us
        left join ger_sequences t on upper(us.sequence_name) = upper(t.seq_name)
       where upper(t.resetamanual) = 'N' and upper(us.cycle_flag) = 'N'
         and us.sequence_name not in ('SEQ_GER_MENUS') and us.increment_by = 1
         and t.table_name is not null and us.last_number > 1 order by t.seq_name;

   lin sequence_cursor%rowtype;
   val          number := 0;
   l_current    number := 0;
   l_difference number := 0;

   procedure p_reg_seq(p_seq varchar2, p_tab varchar2, p_col varchar2) is
   begin
      merge into ger_sequences t
      using (select p_seq as seq, p_tab as tab, p_col as col from dual) orig
      on (upper(t.seq_name) = upper(orig.seq))
      when matched then
         update set t.table_name  = orig.tab, t.column_name = orig.col
      when not matched then
         insert (seq_name, table_name, column_name, resetamanual)
         values (orig.seq, orig.tab, orig.col, 'N');
   end p_reg_seq;

begin
   p_reg_seq('SEQ_COMPOSICAO_LINHAS_PEDIDOS', 'COMPOSICAO_LINHAS_PEDIDOS', 'ID');
   p_reg_seq('SEQ_WMS_ETIQ_LIN_PED_ERP', 'ETIQUETAS_LINHAS_PEDIDOS_ERP', 'ID');
   p_reg_seq('SEQ_GER_AVISOS', 'GER_AVISOS', 'GER_AVISO_ID');
   p_reg_seq('SEQ_GER_BALANCAS', 'GER_BALANCAS', 'GER_BALANCA_ID');
   p_reg_seq('SEQ_GER_CIDADES', 'GER_CIDADES', 'GER_CIDADE_ID');
   p_reg_seq('SEQ_GER_FERIADOS', 'GER_FERIADOS', 'GER_FERIADO_ID');
   p_reg_seq('SEQ_GER_IMPRESSORAS', 'GER_IMPRESSORAS', 'GER_IMPRESSORA_ID');
   p_reg_seq('SEQ_GER_LAYOUT', 'GER_LAYOUT', 'GER_LAYOUT_ID');
   p_reg_seq('SEQ_GER_LAYOUTLIN', 'GER_LAYOUTLIN', 'GER_LAYOUTLIN_ID');
   p_reg_seq('SEQ_GER_LAYOUTVAR', 'GER_LAYOUTVAR', 'GER_LAYOUTVAR_ID');
   p_reg_seq('SEQ_GER_LOG', 'GER_LOG', 'GER_LOG_ID');
   p_reg_seq('SEQ_GER_MENUS_ACESSOS', 'GER_MENUS_ACESSOS', 'MENU_ID');
   p_reg_seq('SEQ_GER_ROTINAS', 'GER_ROTINAS', 'ID');
   p_reg_seq('SEQ_GER_ROTINAS_ACESSOS', 'GER_ROTINAS_ACESSOS', 'ROTINA_ID');
   p_reg_seq('SEQ_GER_SINC_LOGS', 'GER_SINC_LOGS', 'ID');
   p_reg_seq('SEQ_GER_TEMPLATE_ETIQUETAS', 'GER_TEMPLATE_ETIQUETAS', 'GER_TEMPLATE_ETIQUETA_ID');
   p_reg_seq('SEQ_GER_TIPOIMPRESSORAS', 'GER_TIPOIMPRESSORAS', 'GER_TIPOIMPRESSORA_ID');
   p_reg_seq('SEQ_GER_TIPO_AVISOS', 'GER_TIPO_AVISOS', 'GER_TIPOAVISO_ID');
   p_reg_seq('SEQ_GER_USUARIOS', 'GER_USUARIOS', 'GER_USUARIO_ID');
   p_reg_seq('SEQ_GER_USUARIOS_AVISOS', 'GER_USUARIOS_AVISOS', 'GER_USUARIOAVISO_ID');
   p_reg_seq('SEQ_GER_USUARIOS_LOGADOS', 'GER_USUARIOS_LOGADOS', 'GER_USUARIOLOGADO_ID');
   p_reg_seq('SEQ_GER_USU_GRUPOS', 'GER_USU_GRUPOS', 'GER_USUGRUPO_ID');
   p_reg_seq('SEQ_GER_VERSOES_ATUALIZACAO', 'GER_VERSOES_ATUALIZACAO', 'GER_VERSOESATUALIZACAO_ID');
   p_reg_seq('SEQ_GER_VERSOES_ATUAL_STEP', 'GER_VERSOES_ATUALIZACAO_STEP', 'ID');
   p_reg_seq('SEQ_INVENTARIO', 'INVENTARIO', 'INVENTARIO_ID');
   p_reg_seq('SEQ_INVENTARIO_CAB', 'INVENTARIO_CAB', 'NUM_INVENTARIO');
   p_reg_seq('WMS_SEQ_ITEM', 'ITEM', 'ID');
   p_reg_seq('SEQ_WMS_ITENS_SEM_ESTOQUE_ERP', 'ITENS_SEM_ESTOQUE_ERP', 'ID');
   p_reg_seq('SEQ_ITENS_TRANSELEVADOR', 'ITENS_TRANSELEVADOR', 'ID');
   p_reg_seq('SEQ_LINHAS_PEDIDOS', 'LINHAS_PEDIDOS', 'ID');
   p_reg_seq('SEQ_LINHA_PEDIDOS_ERP_501', 'LINHAS_PEDIDOS_ERP_501', 'ID');
   p_reg_seq('SEQ_LINHAS_PEDIDOS_ERP_LOTES', 'LINHAS_PEDIDOS_ERP_LOTES', 'ID');
   p_reg_seq('SEQ_LINHAS_PEDIDOS_ERP_SERIAIS', 'LINHAS_PEDIDOS_ERP_SERIAIS', 'ID');
   p_reg_seq('SEQ_LINHAS_RESERVA', 'LINHAS_RESERVA', 'ID');
   p_reg_seq('SEQ_PEDIDOS', 'PEDIDOS', 'ID');
   p_reg_seq('SEQ_PEDIDOS_EXCLUIDOS', 'PEDIDOS_EXCLUIDOS', 'ID');
   p_reg_seq('SEQ_PEDIDOS_PROBLEMA_GERA_ONDA', 'PEDIDOS_PROBLEMA_GERA_ONDA', 'ID');
   p_reg_seq('SEQ_RESERVA', 'RESERVA', 'ID');
   p_reg_seq('SEQ_TAREFAS_TRANSELEVADOR', 'TAREFAS_TRANSELEVADOR', 'TAREFA_TRANSELEVADOR_ID');
   p_reg_seq('SEQ_VOICE_TAREFAS', 'VOICE_TAREFAS', 'VOICE_TAREFA_ID');
   p_reg_seq('SEQ_WMS_ACERTO_ESTOQUE_CD', 'WMS_ACERTO_ESTOQUE_CD', 'WMS_ACERTOESTOQUECD_ID');
   p_reg_seq('SEQ_WMS_ALMOX_PERMISSAO_TRANSF', 'WMS_ALMOX_PERMISSAO_TRANSF', 'ID');
   p_reg_seq('SEQ_WMS_AREAS_CARREGAMENTO', 'WMS_AREAS_CARREGAMENTO', 'WMS_AREAS_CARREGAMENTO_ID');
   p_reg_seq('SEQ_WMS_AREAS_COLABORADORES', 'WMS_AREAS_COLABORADORES', 'WMS_AREASCOLABORADORES_ID');
   p_reg_seq('SEQ_WMS_AREAS_COLETAS', 'WMS_AREAS_COLETAS', 'WMS_AREACOLETA_ID');
   p_reg_seq('SEQ_WMS_AUDITORIA_CARGAS', 'WMS_AUDITORIA_CARGAS', 'ID');
   p_reg_seq('SEQ_WMS_AUDIT_ITENS', 'WMS_AUDIT_ITENS', 'ID');
   p_reg_seq('SEQ_WMS_AUTORIZACOES_ID', 'WMS_AUTORIZACOES', 'ID');
   p_reg_seq('SEQ_WMS_AUTORIZACOES', 'WMS_AUTORIZACOES', 'AUTREC_ID');
   p_reg_seq('SEQ_WMS_AUTORIZACOES_EXCLUIDAS', 'WMS_AUTORIZACOES_EXCLUIDAS', 'ID');
   p_reg_seq('SEQ_WMS_AUTORIZACOES_HISTORICO', 'WMS_AUTORIZACOES_HISTORICO', 'WMS_AUTORIZACAOHISTORICO_ID');
   p_reg_seq('COM_WMS_AUTREC_ID_SEQ', 'WMS_AUTORIZACOES_RECEBIMENTOS', 'AUTREC_ID');
   p_reg_seq('SEQ_WMS_AUTORIZACOES_RESERVA', 'WMS_AUTORIZACOES_RESERVA', 'ID');
   p_reg_seq('SEQ_WMS_CARGAS', 'WMS_CARGAS', 'ID');
   p_reg_seq('SEQ_WMS_CENTDIST_USUARIOS', 'WMS_CENTDIST_USUARIOS', 'CENTDISTUSU_ID');
   p_reg_seq('SEQ_WMS_CHAPAS', 'WMS_CHAPAS', 'WMS_CHAPA_ID');
   p_reg_seq('SEQ_WMS_CHECKOUT_AUDIT_PESO', 'WMS_CHECKOUT_AUDIT_PESO', 'ID');
   p_reg_seq('SEQ_WMS_CLIENTES', 'WMS_CLIENTES', 'WMS_CLIENTE_ID');
   p_reg_seq('SEQ_WMS_SEPARACAOCOLETOR', 'WMS_COLABORADORES_ALOCADOS', 'SEPCOL_SEPCOL_ID');
   p_reg_seq('COMWMS_SEQ_COLALOC', 'WMS_COLABORADORES_LOGADOS', 'COLOG_ID');
   p_reg_seq('WMS_COMPOSICAO_ITEM_SEQ', 'WMS_COMPOSICAO_ITEM', 'WMS_COMPOSICAO_ITEM_ID');
   p_reg_seq('SEQ_WMS_CROSS_DOCKING', 'WMS_CROSS_DOCKING', 'ID');
   p_reg_seq('SEQ_WMS_DEVOLUCAO', 'WMS_DEVOLUCAO', 'WMS_DEVOLUCAO_ID');
   p_reg_seq('SEQ_WMS_DIVERGENCIAS', 'WMS_DIVERGENCIAS', 'WMS_DIVERGENCIA_ID');
   p_reg_seq('SEQ_WMS_DOCAS_CHECKOUT', 'WMS_DOCAS_CHECKOUT', 'WMS_DOCA_CHECKOUT_ID');
   p_reg_seq('SEQ_WMS_DOCAS_CONSOLIDACAO', 'WMS_DOCAS_CONSOLIDACAO', 'WMS_DOCA_CONSOLIDACAO_ID');
   p_reg_seq('SEQ_WMS_EMBALAGENS', 'WMS_EMBALAGENS', 'WMS_EMBALAGEM_ID');
   p_reg_seq('SEQ_WMS_EMBARQUE', 'WMS_EMBARQUE', 'ID');
   p_reg_seq('SEQ_WMS_EMBARQUE_HIST', 'WMS_EMBARQUE_HIST', 'ID');
   p_reg_seq('SEQ_WMS_EMBARQUE_ITEM', 'WMS_EMBARQUE_ITEM', 'ID');
   p_reg_seq('SEQ_WMS_EMBARQUE_UNIT', 'WMS_EMBARQUE_UNIT', 'ID');
   p_reg_seq('SEQ_WMS_ENDERECOS', 'WMS_ENDERECOS', 'ID');
   p_reg_seq('COMWMS_SEQ_EST_ID', 'WMS_ESTOQUES_CD', 'ESTCD_ID');
   p_reg_seq('SEQ_WMS_ESTOQUE_ERP', 'WMS_ESTOQUE_ERP', 'WMS_ESTOQUEERP_ID');
   p_reg_seq('SEQ_WMS_ETIQUETAS', 'WMS_ETIQUETAS', 'WMS_ETIQUETA_ID');
   p_reg_seq('SEQ_WMS_ETITENS', 'WMS_ETIQUETAS_ITENS', 'ETITENS_ID');
   p_reg_seq('SEQ_WMS_FAIXA_PERC_MIN_RECEB', 'WMS_FAIXA_PERC_MIN_RECEB', 'WMS_FAIXAPERCMINRECEB_ID');
   p_reg_seq('SEQ_WMS_FILA_PROCESSO_SINC', 'WMS_FILA_PROCESSO_SINC', 'FILA_ID');
   p_reg_seq('SEQ_WMS_INDICES_DIST_RUAS', 'WMS_INDICES_DIST_RUAS', 'IDR_ID');
   p_reg_seq('SEQ_WMS_INSPECAO', 'WMS_INSPECAO', 'WMS_INSPECAO_ID');
   p_reg_seq('SEQ_WMS_ITEM_ALTERNATIVO', 'WMS_ITEM_ALTERNATIVO', 'ID');
   p_reg_seq('SEQ_WMS_ITENS_CHECKOUT', 'WMS_ITENS_CHECKOUT', 'IT_CHECKOUT_ID');
   p_reg_seq('SEQ_WMS_ITENS_EMBALAGENS', 'WMS_ITENS_EMBALAGENS', 'ID_ITEM_EMBALAGEM');
   p_reg_seq('SEQ_ITENS_INVENTARIO', 'WMS_ITENS_INVENTARIO', 'ID_INVENTARIO');
   p_reg_seq('SEQ_WMS_ITENS_UNITIZADORES', 'WMS_ITENS_UNITIZADORES', 'WMS_ITENSUNITIZADORES_ID');
   p_reg_seq('SEQ_WMS_LINHAS_CARGAS', 'WMS_LINHAS_CARGAS', 'ID');
   p_reg_seq('SEQ_WMS_LINHAS_MINUTAS', 'WMS_LINHAS_MINUTAS', 'WMS_LINHAMINUTA_ID');
   p_reg_seq('SEQ_WMS_LOGS', 'WMS_LOGS', 'WMS_LOG_ID');
   p_reg_seq('SEQ_WMS_LOTES', 'WMS_LOTES', 'WMS_LOTE_ID');
   p_reg_seq('SEQ_WMS_LOTES_AGRUPADOS', 'WMS_LOTES_AGRUPADOS', 'WMS_LOTEAGRUPADO_ID');
   p_reg_seq('SEQ_WMS_MINUTAS', 'WMS_MINUTAS', 'WMS_MINUTA_ID');
   p_reg_seq('COM_WMS_MOV_EST_CD', 'WMS_MOV_ESTOQUES_CD', 'MOVESTCD_ID');
   p_reg_seq('SEQ_WMS_MOV_UNITIZADORES', 'WMS_MOV_UNITIZADORES', 'WMS_MOVUNITIZADORES_ID');
   p_reg_seq('COM_WMS_OCORRENCIAS', 'WMS_OCORRENCIAS', 'COD_WMS_OCORRENCIAS');
   p_reg_seq('SEQ_WMS_OCUPACAO_CD', 'WMS_OCUPACAO_CD', 'WMS_OCUPACAOCD_ID');
   p_reg_seq('COM_WMS_ONDAS_ID_SEQ', 'WMS_ONDAS', 'ONDA_ID');
   p_reg_seq('SEQ_WMS_PALETES', 'WMS_PALETES', 'ID');
   p_reg_seq('SEQ_WMS_PREDIOS', 'WMS_PREDIOS', 'PREDIO_ID');
   p_reg_seq('SEQ_WMS_QUERYRELATORIOS', 'WMS_QUERYRELATORIOS', 'WMS_QUERYRELATORIO_ID');
   p_reg_seq('SEQ_WMS_QUERYRELATORIOS_PAR', 'WMS_QUERYRELATORIOS_PARAMETROS', 'WMS_QUERYRELATORIOPARAMETRO_ID');
   p_reg_seq('SEQ_WMS_RECEBIMENTOS', 'WMS_RECEBIMENTOS', 'WMS_RECEBIMENTO_ID');
   p_reg_seq('SEQ_WMS_RECEBIMENTOS_ITENS', 'WMS_RECEBIMENTOS_ITENS', 'WMS_RECEBIMENTOITEM_ID');
   p_reg_seq('SEQ_WMS_REGIOESCOLABORADORES', 'WMS_REGIOES_COLABORADORES', 'REGCOL_ID');
   p_reg_seq('SEQ_WMS_SEP_INVERSA_ITEM', 'WMS_SEP_INVERSA_ITEM', 'ID');
   p_reg_seq('SEQ_WMS_SEP_INVERSA_UNIT', 'WMS_SEP_INVERSA_UNIT', 'ID');
   p_reg_seq('SEQ_WMS_SERIAIS', 'WMS_SERIAIS', 'WMS_SERIAL_ID');
   p_reg_seq('SEQ_WMS_SERIAIS_SEPARACAO', 'WMS_SERIAIS_SEPARACAO', 'ID');
   p_reg_seq('SEQ_WMS_SERIAIS_VOLUMES', 'WMS_SERIAIS_VOLUMES', 'ID');
   p_reg_seq('SEQ_WMS_SIT_RECEBIMENTO', 'WMS_SIT_RECEBIMENTO', 'ID');
   p_reg_seq('SEQ_WMS_SUB_TAREFAS_ETIQUETAS', 'WMS_SUB_TAREFAS_ETIQUETAS', 'ID');
   p_reg_seq('SEQ_WMS_TABELAS', 'WMS_TABELAS', 'WMS_TABELA_ID');
   p_reg_seq('COM_WMS_TARCD_ID_SEQ', 'WMS_TAREFAS_CD', 'COD_TAREFA_CD');
   p_reg_seq('SEQ_WMS_TAREFAS_GERAL', 'WMS_TAREFAS_GERAL', 'WMS_TAREFAS_GERAL_ID');
   p_reg_seq('SEQ_WMS_TIPOS_ESTRUTURA', 'WMS_TIPOS_ESTRUTURA', 'ID');
   p_reg_seq('SEQ_WMS_TIPOS_PEDIDOS', 'WMS_TIPOS_PEDIDOS', 'WMS_TIPOPEDIDO_ID');
   p_reg_seq('SEQ_WMS_TP_MOV_ESTOQUE', 'WMS_TP_MOV_ESTOQUE', 'WMS_TPMOVESTOQUE_ID');
   p_reg_seq('SEQ_WMS_TRANSPORTADORAS', 'WMS_TRANSPORTADORAS', 'WMS_TRANSPORTADORA_ID');
   p_reg_seq('SEQ_WMS_TURNOS', 'WMS_TURNOS', 'ID');
   p_reg_seq('SEQ_WMS_UNITIZADORESONDA', 'WMS_UNITIZADORES_ONDA', 'UNITOND_ID');
   p_reg_seq('SEQ_WMS_UNIT_ONDA_HIST', 'WMS_UNITIZADORES_ONDA_HIST', 'ID');
   p_reg_seq('SEQ_WMS_VOLUMES', 'WMS_VOLUMES', 'WMS_VOLUME_ID');
   p_reg_seq('SEQ_WMS_VOLUMES_DELETADOS', 'WMS_VOLUMES_DELETADOS', 'ID');

   commit;

   for lin in sequence_cursor
   loop
      execute immediate 'select nvl(max(t.' || lin.column_name || '),0) from ' || lin.table_name || ' t'
         into val;
      if lin.table_name = 'PEDIDOS' then
         execute immediate 'select greatest (nvl(max(t.' || lin.column_name || '),0),' || val ||
                           ') from ' || 'PEDIDOS_ERP' || ' t'
            into val;
      end if;
      
      if lin.table_name = 'LINHAS_PEDIDOS' then
         execute immediate 'select greatest (nvl(max(t.' || lin.column_name || '),0),' || val ||
                           ') from ' || 'LINHAS_PEDIDOS_ERP' || ' t'
            into val;
      end if;
   
      if lin.last_number = (val + 1) or (val = 0 and lin.last_number <= 2) then
         continue;
      end if;
   
      if mostra_seq_maior = 'N' and lin.last_number > val then
         continue;
      end if;
      
      if gravar = 'S' then
         execute immediate 'select ' || lin.seq_name || '.nextval from dual'
            into l_current;
         l_difference := val - l_current;
         if l_difference = 0 then
            continue;
         end if;
         if lin.min_value = 1 and val  = 0 then
            l_difference := l_difference + 1;
         end if;
         execute immediate 'alter sequence ' || lin.seq_name || ' increment by ' || l_difference;
         execute immediate 'select ' || lin.seq_name || '.nextval from dual'
            into l_difference;
         execute immediate 'alter sequence ' || lin.seq_name || ' increment by 1';
      end if;
   end loop;
end prc_wms_util_reset_sequence;
");
                        _connectionManager.ExecuteNonQuery("BEGIN prc_wms_util_reset_sequence(gravar=>'S'); END;");
                    }

                    if (doStats)
                    {
                        worker.ReportProgress(0, "Gerando Estatísticas...");
                        _connectionManager.ExecuteNonQuery("BEGIN dbms_stats.gather_schema_stats(user); END;");
                    }

                    if (enableTriggers)
                    {
                        worker.ReportProgress(0, "Habilitando Triggers...");
                        var triggers = _triggerManager.GetAllTriggers(enabled: false);
                        if (triggers.Count > 0)
                        {
                            List<string> names = new List<string>();
                            foreach (var t in triggers) names.Add(t.TriggerName);
                            _triggerManager.EnableTriggers(names);
                        }
                    }
                    if (doCheck)
                    {
                        ExecutarValidacaoConstraints();
                    }
                    if (doCompile)
                    {
                        worker.ReportProgress(0, "Recompilando Schema...");
                        _connectionManager.ExecuteNonQuery("BEGIN dbms_utility.compile_schema(user, compile_all => false); END;");
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
                if (args.Result.ToString().StartsWith("Erro")) MessageBox.Show(args.Result.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else MessageBox.Show("Processo de restauração concluído com sucesso!", "Concluído", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetStatus("Restauração concluída");
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

        private void AdicionarLog(string mensagem)
        {
            if (string.IsNullOrEmpty(mensagem)) return;

            string textoFinal = $"{DateTime.Now:HH:mm:ss} - {mensagem}\r\n";

            // Tenta achar o componente pelo nome padrão
            // Se o seu componente se chamar "richTextBox1", vai funcionar agora
            Control[] controles = this.Controls.Find("richTextBox1", true);

            if (controles.Length > 0 && controles[0] is RichTextBox rtb)
            {
                rtb.AppendText(textoFinal);
                rtb.ScrollToCaret();
            }
            else
            {
                // Se não achou, tenta achar com nome "Log"
                controles = this.Controls.Find("Log", true);
                if (controles.Length > 0 && controles[0] is RichTextBox rtbLog)
                {
                    rtbLog.AppendText(textoFinal);
                    rtbLog.ScrollToCaret();
                }
                // Se ainda não achou, tenta "txtLog"
                else
                {
                    controles = this.Controls.Find("txtLog", true);
                    if (controles.Length > 0 && controles[0] is RichTextBox rtbTxt)
                    {
                        rtbTxt.AppendText(textoFinal);
                        rtbTxt.ScrollToCaret();
                    }
                }
            }
        }
        private void ExecutarValidacaoConstraints()
        {
            // --- MUDANÇA AQUI: De Log() para AdicionarLog() ---
            AdicionarLog("Iniciando validação de constraints...");

            try
            {
                string sqlGenerator = @"
            SELECT 'ALTER TABLE ' || table_name || 
                   ' MODIFY CONSTRAINT ' || constraint_name || 
                   ' VALIDATE' AS COMANDO
            FROM user_constraints
            WHERE validated = 'NOT VALIDATED'
              AND constraint_type IN ('P', 'U', 'R', 'C')";

                DataTable dt = _connectionManager.ExecuteQuery(sqlGenerator);

                if (dt.Rows.Count == 0)
                {
                    AdicionarLog("Nenhuma constraint 'NOT VALIDATED' encontrada.");
                    return;
                }

                AdicionarLog($"Encontradas {dt.Rows.Count} constraints para validar.");

                int countSucesso = 0;
                int countErro = 0;

                foreach (DataRow row in dt.Rows)
                {
                    string ddl = row["COMANDO"].ToString();

                    try
                    {
                        _connectionManager.ExecuteNonQuery(ddl);
                        countSucesso++;
                    }
                    catch (Exception ex)
                    {
                        countErro++;
                        // Loga o erro mas não para o processo
                        AdicionarLog($"Erro ao validar ({ddl}): {ex.Message}");
                    }
                }

                AdicionarLog($"Fim da validação. Sucesso: {countSucesso} | Erros: {countErro}");
            }
            catch (Exception ex)
            {
                AdicionarLog($"Erro crítico ao buscar constraints: {ex.Message}");
                MessageBox.Show(ex.Message, "Erro na Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetStatus(string msg) { toolStripStatusLabel.Text = msg; statusStrip.Refresh(); }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) { if (_connectionManager?.IsConnected == true) _connectionManager.Disconnect(); }
        private void txtServiceName_TextChanged(object sender, EventArgs e) { }
        private void MainForm_Load(object sender, EventArgs e)
        {
            validatorbtn(sender, e);
        }

        private void validatorbtn(object sender, EventArgs e)
        {
            ProcessStartInfo parametro = new ProcessStartInfo("cmd.exe", "/C " + @"net use \\172.25.100.248 wms246@. /USER:wms246")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using (Process process = Process.Start(parametro))
            {
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
                        return dialogo == DialogResult.Yes;
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