# PLSQL Migration Tool

Ferramenta Windows Forms em .NET Framework 4.8 para gerenciamento de migração de dados em bancos de dados Oracle PLSQL.

## Funcionalidades

### 1. Gerenciamento de Conexão
- Testar conexão com banco de dados Oracle
- Conectar e desconectar do banco
- Suporte a string de conexão Oracle padrão

### 2. Gerenciamento de Triggers
- Listar todas as triggers do banco de dados
- Visualizar status (ENABLED/DISABLED)
- Desabilitar triggers selecionadas
- Habilitar triggers selecionadas
- Seleção múltipla com checkboxes

### 3. Gerenciamento de Constraints
- Listar todas as constraints (Primary Key, Foreign Key, Unique, Check)
- Visualizar tipo e status de cada constraint
- Desabilitar constraints selecionadas
- Habilitar constraints selecionadas
- Seleção múltipla com checkboxes

### 4. Truncate de Tabelas
- Listar todas as tabelas do banco
- Visualizar número aproximado de linhas
- Truncar tabelas selecionadas
- Confirmação antes de executar operação destrutiva
- Seleção múltipla com checkboxes

### 5. Exportação de DDL
- Exportar estrutura de tabelas selecionadas
- Opção para incluir constraints
- Opção para incluir foreign keys
- Geração de script SQL completo
- Salvar em arquivo .sql

## Requisitos

### Para Desenvolvimento
- Visual Studio 2019 ou superior
- .NET Framework 4.8 SDK
- Oracle.ManagedDataAccess (instalado via NuGet)

### Para Execução
- Windows 7 ou superior
- .NET Framework 4.8 Runtime
- Acesso a banco de dados Oracle

## Instalação

### Opção 1: Compilar no Visual Studio

1. Abra o arquivo `PLSQLMigrationTool.sln` no Visual Studio
2. Restaure os pacotes NuGet (clique com botão direito na solução > Restore NuGet Packages)
3. Compile a solução (Build > Build Solution ou F6)
4. Execute o aplicativo (Debug > Start ou F5)

### Opção 2: Usar MSBuild (Linha de Comando)

```cmd
# Navegar até o diretório da solução
cd PLSQLMigrationTool

# Restaurar pacotes NuGet
nuget restore PLSQLMigrationTool.sln

# Compilar
msbuild PLSQLMigrationTool.sln /p:Configuration=Release

# Executar
.\PLSQLMigrationTool\bin\Release\PLSQLMigrationTool.exe
```

## Configuração da String de Conexão

### Formato Padrão Oracle

```
Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=servidor)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=nome_servico)));User Id=usuario;Password=senha;
```

### Exemplos

**Conexão Local:**
```
Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)));User Id=system;Password=oracle;
```

**Conexão Remota:**
```
Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.1.100)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=PROD)));User Id=admin;Password=senha123;
```

**Usando TNS Alias:**
```
Data Source=ORCL;User Id=system;Password=oracle;
```

## Fluxo de Trabalho Recomendado

### Preparação para Importação de Dados

1. **Conectar ao Banco**
   - Vá para a aba "Conexão"
   - Insira a string de conexão
   - Clique em "Testar Conexão" para verificar
   - Clique em "Conectar"

2. **Desabilitar Triggers**
   - Vá para a aba "Triggers"
   - Clique em "Atualizar Lista"
   - Selecione as triggers que deseja desabilitar
   - Clique em "Desabilitar Selecionadas"

3. **Desabilitar Constraints**
   - Vá para a aba "Constraints"
   - Clique em "Atualizar Lista"
   - Selecione as constraints (especialmente Foreign Keys)
   - Clique em "Desabilitar Selecionadas"

4. **Limpar Tabelas (Opcional)**
   - Vá para a aba "Truncate"
   - Clique em "Atualizar Lista"
   - Selecione as tabelas que deseja limpar
   - Clique em "Truncar Selecionadas"
   - **ATENÇÃO:** Esta operação remove todos os dados permanentemente!

5. **Importar Dados**
   - Use sua ferramenta de importação preferida (SQL*Loader, Data Pump, etc.)

6. **Reabilitar Constraints**
   - Volte para a aba "Constraints"
   - Clique em "Atualizar Lista"
   - Selecione as constraints desabilitadas
   - Clique em "Habilitar Selecionadas"

7. **Reabilitar Triggers**
   - Volte para a aba "Triggers"
   - Clique em "Atualizar Lista"
   - Selecione as triggers desabilitadas
   - Clique em "Habilitar Selecionadas"

### Exportação de Estruturas

1. **Conectar ao Banco de Origem**
   - Siga os passos de conexão acima

2. **Exportar DDL**
   - Vá para a aba "Exportação"
   - Clique em "Atualizar Lista"
   - Selecione as tabelas que deseja exportar
   - Marque as opções desejadas:
     - ☑ Incluir Constraints
     - ☑ Incluir Foreign Keys
   - Clique em "Exportar DDL"
   - Escolha o local e nome do arquivo
   - O script SQL será gerado

3. **Usar o Script no Banco de Destino**
   - Execute o script SQL gerado no banco de destino
   - Isso recriará as estruturas de tabelas, constraints e FKs

## Estrutura do Projeto

```
PLSQLMigrationTool/
├── PLSQLMigrationTool.sln          # Arquivo de solução
├── README.md                        # Este arquivo
└── PLSQLMigrationTool/             # Projeto principal
    ├── PLSQLMigrationTool.csproj   # Arquivo de projeto
    ├── App.config                   # Configuração da aplicação
    ├── packages.config              # Pacotes NuGet
    ├── Program.cs                   # Ponto de entrada
    ├── Forms/                       # Formulários
    │   ├── MainForm.cs
    │   ├── MainForm.Designer.cs
    │   └── MainForm.resx
    ├── Business/                    # Lógica de negócio
    │   ├── TriggerManager.cs
    │   ├── ConstraintManager.cs
    │   ├── TableManager.cs
    │   └── ExportManager.cs
    ├── Data/                        # Acesso a dados
    │   ├── OracleConnectionManager.cs
    │   ├── OracleQueryExecutor.cs
    │   └── MetadataRepository.cs
    ├── Models/                      # Modelos de dados
    │   ├── TriggerInfo.cs
    │   ├── ConstraintInfo.cs
    │   └── TableInfo.cs
    └── Properties/                  # Propriedades do assembly
        ├── AssemblyInfo.cs
        ├── Resources.resx
        ├── Resources.Designer.cs
        ├── Settings.settings
        └── Settings.Designer.cs
```

## Arquitetura

### Camadas

**Apresentação (Forms)**
- `MainForm`: Interface principal com abas para cada funcionalidade

**Negócio (Business)**
- `TriggerManager`: Gerencia operações com triggers
- `ConstraintManager`: Gerencia operações com constraints
- `TableManager`: Gerencia operações com tabelas
- `ExportManager`: Gerencia exportação de DDL

**Dados (Data)**
- `OracleConnectionManager`: Gerencia conexões Oracle
- `OracleQueryExecutor`: Executa queries e comandos
- `MetadataRepository`: Consulta dicionário de dados Oracle

**Modelos (Models)**
- `TriggerInfo`: Representa informações de uma trigger
- `ConstraintInfo`: Representa informações de uma constraint
- `TableInfo`: Representa informações de uma tabela

## Queries Utilizadas

### Listar Triggers
```sql
SELECT TRIGGER_NAME, TABLE_NAME, STATUS, TRIGGER_TYPE, TRIGGERING_EVENT
FROM USER_TRIGGERS
ORDER BY TABLE_NAME, TRIGGER_NAME
```

### Desabilitar/Habilitar Trigger
```sql
ALTER TRIGGER [trigger_name] DISABLE
ALTER TRIGGER [trigger_name] ENABLE
```

### Listar Constraints
```sql
SELECT CONSTRAINT_NAME, TABLE_NAME, CONSTRAINT_TYPE, STATUS,
       SEARCH_CONDITION, R_CONSTRAINT_NAME
FROM USER_CONSTRAINTS
WHERE CONSTRAINT_TYPE IN ('P', 'R', 'U', 'C')
ORDER BY TABLE_NAME, CONSTRAINT_NAME
```

### Desabilitar/Habilitar Constraint
```sql
ALTER TABLE [table_name] DISABLE CONSTRAINT [constraint_name]
ALTER TABLE [table_name] ENABLE CONSTRAINT [constraint_name]
```

### Listar Tabelas
```sql
SELECT TABLE_NAME, NUM_ROWS, TABLESPACE_NAME
FROM USER_TABLES
ORDER BY TABLE_NAME
```

### Truncar Tabela
```sql
TRUNCATE TABLE [table_name]
```

### Obter DDL
```sql
SELECT DBMS_METADATA.GET_DDL('TABLE', '[table_name]') FROM DUAL
SELECT DBMS_METADATA.GET_DDL('CONSTRAINT', '[constraint_name]') FROM DUAL
```

## Tratamento de Erros

O aplicativo inclui tratamento de erros abrangente:

- Validação de conexão antes de operações
- Confirmação para operações destrutivas (truncate)
- Mensagens de erro detalhadas
- Rollback automático em caso de falha em transações
- Log de erros individuais em operações em lote

## Limitações Conhecidas

1. **Permissões**: O usuário conectado precisa ter permissões adequadas para:
   - Alterar triggers e constraints
   - Truncar tabelas
   - Acessar views do dicionário de dados (USER_TRIGGERS, USER_CONSTRAINTS, etc.)
   - Executar DBMS_METADATA.GET_DDL

2. **Truncate com Foreign Keys**: Se uma tabela tem foreign keys apontando para ela, o truncate pode falhar. Neste caso:
   - Desabilite as foreign keys primeiro
   - Ou use DELETE ao invés de TRUNCATE (não implementado nesta versão)

3. **Schemas**: Esta versão trabalha apenas com o schema do usuário conectado (USER_*). Para trabalhar com outros schemas, seria necessário usar ALL_* ou DBA_* views.

## Segurança

- **Nunca** armazene senhas em texto plano no código
- Use variáveis de ambiente ou arquivos de configuração criptografados para produção
- Limite as permissões do usuário do banco ao mínimo necessário
- Faça backup antes de operações destrutivas

## Solução de Problemas

### Erro: "ORA-01031: insufficient privileges"
**Solução**: O usuário não tem permissões suficientes. Conceda as permissões necessárias:
```sql
GRANT ALTER ANY TRIGGER TO usuario;
GRANT ALTER ANY TABLE TO usuario;
GRANT DROP ANY TABLE TO usuario;
```

### Erro: "ORA-02266: unique/primary keys in table referenced by enabled foreign keys"
**Solução**: Desabilite as foreign keys que referenciam a tabela antes de truncar.

### Erro: "Could not load file or assembly 'Oracle.ManagedDataAccess'"
**Solução**: Restaure os pacotes NuGet ou instale manualmente:
```cmd
nuget install Oracle.ManagedDataAccess -Version 23.7.0
```

### Aplicativo não inicia
**Solução**: Verifique se o .NET Framework 4.8 está instalado:
- Baixe em: https://dotnet.microsoft.com/download/dotnet-framework/net48

## Melhorias Futuras

- [ ] Suporte a múltiplos schemas
- [ ] Backup automático antes de operações destrutivas
- [ ] Histórico de operações com undo
- [ ] Importação de dados diretamente pelo app
- [ ] Suporte a PostgreSQL e SQL Server
- [ ] Agendamento de tarefas
- [ ] Interface em inglês
- [ ] Modo batch/linha de comando
- [ ] Geração de relatórios

## Licença

Este projeto é fornecido como está, sem garantias. Use por sua conta e risco.

## Suporte

Para dúvidas ou problemas, consulte a documentação Oracle:
- [Oracle Data Provider for .NET](https://docs.oracle.com/en/database/oracle/oracle-data-access-components/index.html)
- [Oracle Database SQL Language Reference](https://docs.oracle.com/en/database/oracle/oracle-database/19/sqlrf/)

## Versão

**Versão**: 1.0.0  
**Data**: Novembro 2025  
**Framework**: .NET Framework 4.8
