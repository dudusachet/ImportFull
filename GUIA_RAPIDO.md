# Guia Rápido - PLSQL Migration Tool

## Início Rápido

### 1. Conectar ao Banco

1. Abra o aplicativo
2. Na aba **Conexão**, insira sua string de conexão:
   ```
   Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)));User Id=system;Password=oracle;
   ```
3. Clique em **Testar Conexão**
4. Se OK, clique em **Conectar**

### 2. Preparar Banco para Importação

#### Passo 1: Desabilitar Triggers
- Vá para aba **Triggers**
- Clique em **Atualizar Lista**
- Clique em **Selecionar Todas** (ou selecione manualmente)
- Clique em **Desabilitar Selecionadas**

#### Passo 2: Desabilitar Constraints
- Vá para aba **Constraints**
- Clique em **Atualizar Lista**
- Clique em **Selecionar Todas** (ou selecione manualmente)
- Clique em **Desabilitar Selecionadas**

#### Passo 3: Limpar Tabelas (Opcional)
- Vá para aba **Truncate**
- Clique em **Atualizar Lista**
- Selecione as tabelas desejadas
- Clique em **Truncar Selecionadas**
- ⚠️ **ATENÇÃO**: Isso apaga todos os dados!

### 3. Importar Dados

Use sua ferramenta de importação (SQL*Loader, Data Pump, etc.)

### 4. Restaurar Banco

#### Passo 1: Reabilitar Constraints
- Volte para aba **Constraints**
- Clique em **Atualizar Lista**
- Selecione as constraints desabilitadas
- Clique em **Habilitar Selecionadas**

#### Passo 2: Reabilitar Triggers
- Volte para aba **Triggers**
- Clique em **Atualizar Lista**
- Selecione as triggers desabilitadas
- Clique em **Habilitar Selecionadas**

### 5. Exportar Estruturas

1. Vá para aba **Exportação**
2. Clique em **Atualizar Lista**
3. Selecione as tabelas desejadas
4. Marque:
   - ☑ **Incluir Constraints**
   - ☑ **Incluir Foreign Keys**
5. Clique em **Exportar DDL**
6. Escolha onde salvar o arquivo .sql

## Dicas Importantes

✅ **Sempre faça backup antes de operações destrutivas**

✅ **Teste em ambiente de desenvolvimento primeiro**

✅ **Desabilite Foreign Keys antes de truncar tabelas**

✅ **Reabilite constraints e triggers após importação**

⚠️ **Truncate é permanente - não há undo!**

⚠️ **Verifique permissões do usuário no banco**

## Atalhos Úteis

| Ação | Botão |
|------|-------|
| Selecionar todas | Selecionar Todas |
| Desmarcar todas | Desmarcar Todas |
| Atualizar lista | Atualizar Lista |
| Desabilitar | Desabilitar Selecionadas |
| Habilitar | Habilitar Selecionadas |

## Solução Rápida de Problemas

**Não conecta?**
- Verifique se o Oracle está rodando
- Confirme host, porta e service name
- Teste com SQL*Plus ou SQL Developer

**Erro de permissão?**
- Usuário precisa de ALTER ANY TRIGGER
- Usuário precisa de ALTER ANY TABLE
- Usuário precisa de DROP ANY TABLE (para truncate)

**Truncate falha?**
- Desabilite Foreign Keys primeiro
- Verifique se há constraints ativas

**Aplicativo não abre?**
- Instale .NET Framework 4.8
- Execute como Administrador

## Exemplo de String de Conexão

**Conexão Local:**
```
Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)));User Id=system;Password=oracle;
```

**Conexão Remota:**
```
Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.1.100)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=PROD)));User Id=admin;Password=senha123;
```

**Com TNS Alias:**
```
Data Source=ORCL;User Id=system;Password=oracle;
```

## Fluxo Visual

```
┌─────────────────┐
│   1. CONECTAR   │
└────────┬────────┘
         │
┌────────▼────────┐
│ 2. DESABILITAR  │
│   TRIGGERS      │
└────────┬────────┘
         │
┌────────▼────────┐
│ 3. DESABILITAR  │
│  CONSTRAINTS    │
└────────┬────────┘
         │
┌────────▼────────┐
│ 4. TRUNCAR      │
│   TABELAS       │
│   (Opcional)    │
└────────┬────────┘
         │
┌────────▼────────┐
│ 5. IMPORTAR     │
│    DADOS        │
│  (Externo)      │
└────────┬────────┘
         │
┌────────▼────────┐
│ 6. HABILITAR    │
│  CONSTRAINTS    │
└────────┬────────┘
         │
┌────────▼────────┐
│ 7. HABILITAR    │
│   TRIGGERS      │
└─────────────────┘
```

## Suporte

Para mais detalhes, consulte o **README.md** completo.
