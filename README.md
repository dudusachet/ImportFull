# PLSQLImportFull 📥

**PLSQLImportFull** é uma ferramenta ágil desenvolvida em Windows Forms (.NET) para a execução e importação de scripts SQL em bancos de dados **Oracle**.

Projetada para complementar o *PLSQLExportFull*, esta ferramenta foca na restauração de dados e execução de comandos DML/DDL com segurança, oferecendo feedback visual claro e conexão simplificada.

![Status do Projeto](https://img.shields.io/badge/Status-Estável-green) ![Platform](https://img.shields.io/badge/Plataforma-Windows-blue) ![Database](https://img.shields.io/badge/Oracle-Database-red)

## ✨ Funcionalidades Principais

* **Conexão Inteligente (Auto-Connect):**
    * Suporta Strings TNS Full (com `DESCRIPTION` e parênteses).
    * Suporta Strings Simplificadas (`user/pass@host:port/service`).
    * Conecta automaticamente ao carregar arquivos `.config` ou colar strings válidas.
* **Interface Responsiva:**
    * **Grid Otimizado:** Renderização com *DoubleBuffered* para evitar "piscar" (flickering) ao passar o mouse.
    * **Feedback Visual:** Labels de "Atenção", "Erro" ou "Importante" são destacados automaticamente em vermelho e negrito.
* **Monitoramento de Status:**
    * Rodapé informativo alinhado à direita mostrando: `USUARIO | BANCO @ HOST`.
    * Indicadores visuais de conexão (Verde/Vermelho) na aba e na barra de status.
* **Execução de Scripts:** Processamento eficiente de arquivos `.sql` e `.pdc` gerados pelo módulo de exportação.

## 🎨 Personalização (Temas)

Assim como no módulo de exportação, o sistema possui temas integrados que persistem entre as sessões.
* **Atalho:** Pressione `Ctrl + Alt + G` para alternar.
    * 🔴 **Red Enterprise** (Padrão - ideal para Prod/Atenção)
    * 🔵 **Blue Ocean** (Alternativo - ideal para Dev/Test)

## 🛠️ Instalação e Requisitos

1.  **Requisitos:**
    * Windows 10/11
    * .NET Framework 4.8
    * Oracle Client ou `Oracle.ManagedDataAccess.dll`.

2.  **Como Usar:**
    * Execute `PLSQLImportFull.exe`.
    * Carregue um arquivo de configuração ou cole a string de conexão.
    * O sistema conectará automaticamente (modo silencioso).
    * Selecione o arquivo de script para importação.
    * Acompanhe o progresso e logs de execução.

## ⚙️ Arquivo de Configuração (.config)

O sistema aceita o mesmo padrão de configuração do Export, facilitando a troca de ambientes.

```xml
<?xml version="1.0"?>
<configuration>
    <appSettings>
        <add key="strConexaoBD" value="data source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.25.100.205)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=wms;Password=wms;"/>
    </appSettings>
</configuration>
