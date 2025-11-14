# Instruções de Compilação - PLSQL Migration Tool

## Pré-requisitos

### Software Necessário

1. **Visual Studio 2019 ou superior**
   - Download: https://visualstudio.microsoft.com/downloads/
   - Edições suportadas: Community, Professional, Enterprise
   - Durante a instalação, selecione:
     - ✅ Desenvolvimento de desktop com .NET
     - ✅ .NET Framework 4.8 SDK

2. **NuGet Package Manager**
   - Já incluído no Visual Studio
   - Versão 5.0 ou superior

3. **Oracle.ManagedDataAccess**
   - Será instalado automaticamente via NuGet
   - Versão: 23.7.0

## Opção 1: Compilar com Visual Studio (Recomendado)

### Passo 1: Abrir o Projeto

1. Extraia o arquivo `PLSQLMigrationTool.zip`
2. Navegue até a pasta extraída
3. Dê duplo clique em `PLSQLMigrationTool.sln`
4. O Visual Studio abrirá automaticamente

### Passo 2: Restaurar Pacotes NuGet

1. No Visual Studio, clique com botão direito na **Solução** (Solution Explorer)
2. Selecione **Restore NuGet Packages**
3. Aguarde o download do Oracle.ManagedDataAccess

**OU**

No menu superior:
- **Tools** > **NuGet Package Manager** > **Package Manager Console**
- Execute: `Update-Package -reinstall`

### Passo 3: Compilar

**Modo Debug (para desenvolvimento):**
1. No menu: **Build** > **Build Solution** (ou pressione `Ctrl+Shift+B`)
2. Aguarde a compilação
3. Verifique a janela **Output** para confirmar sucesso
4. O executável estará em: `PLSQLMigrationTool\bin\Debug\PLSQLMigrationTool.exe`

**Modo Release (para distribuição):**
1. No menu: **Build** > **Configuration Manager**
2. Altere **Active solution configuration** para **Release**
3. Clique em **Close**
4. No menu: **Build** > **Build Solution**
5. O executável estará em: `PLSQLMigrationTool\bin\Release\PLSQLMigrationTool.exe`

### Passo 4: Executar

**Dentro do Visual Studio:**
- Pressione `F5` (com debug) ou `Ctrl+F5` (sem debug)

**Fora do Visual Studio:**
- Navegue até a pasta `bin\Debug` ou `bin\Release`
- Dê duplo clique em `PLSQLMigrationTool.exe`

## Opção 2: Compilar via Linha de Comando

### Passo 1: Instalar Ferramentas

1. **Instalar .NET Framework 4.8 Developer Pack**
   - Download: https://dotnet.microsoft.com/download/dotnet-framework/net48

2. **Instalar NuGet CLI**
   ```cmd
   # Baixar nuget.exe
   curl -o nuget.exe https://dist.nuget.org/win-x86-commandline/latest/nuget.exe
   
   # Ou via PowerShell
   Invoke-WebRequest -Uri https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -OutFile nuget.exe
   ```

3. **Localizar MSBuild**
   - Geralmente em: `C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe`
   - Ou use Developer Command Prompt for VS

### Passo 2: Restaurar Pacotes

```cmd
cd PLSQLMigrationTool
nuget restore PLSQLMigrationTool.sln
```

### Passo 3: Compilar

**Usando Developer Command Prompt:**
```cmd
msbuild PLSQLMigrationTool.sln /p:Configuration=Release /p:Platform="Any CPU"
```

**Usando caminho completo do MSBuild:**
```cmd
"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe" PLSQLMigrationTool.sln /p:Configuration=Release /p:Platform="Any CPU"
```

### Passo 4: Localizar Executável

```
PLSQLMigrationTool\bin\Release\PLSQLMigrationTool.exe
```

## Opção 3: Compilar com MSBuild Tools

### Instalar Build Tools

Se você não tem Visual Studio completo:

1. Baixe **Build Tools for Visual Studio**
   - https://visualstudio.microsoft.com/downloads/#build-tools-for-visual-studio-2019

2. Durante instalação, selecione:
   - ✅ .NET desktop build tools
   - ✅ .NET Framework 4.8 SDK

3. Siga os passos da **Opção 2**

## Distribuição

### Arquivos Necessários para Distribuição

Ao distribuir o aplicativo, inclua os seguintes arquivos da pasta `bin\Release`:

```
PLSQLMigrationTool.exe          # Executável principal
PLSQLMigrationTool.exe.config   # Arquivo de configuração
Oracle.ManagedDataAccess.dll    # Driver Oracle
```

### Criar Pacote de Distribuição

**Manualmente:**
1. Copie os 3 arquivos acima para uma nova pasta
2. Compacte em ZIP
3. Distribua

**Via Script (PowerShell):**
```powershell
# Criar pasta de distribuição
New-Item -ItemType Directory -Force -Path ".\Distribuicao"

# Copiar arquivos
Copy-Item ".\PLSQLMigrationTool\bin\Release\PLSQLMigrationTool.exe" -Destination ".\Distribuicao\"
Copy-Item ".\PLSQLMigrationTool\bin\Release\PLSQLMigrationTool.exe.config" -Destination ".\Distribuicao\"
Copy-Item ".\PLSQLMigrationTool\bin\Release\Oracle.ManagedDataAccess.dll" -Destination ".\Distribuicao\"

# Criar ZIP
Compress-Archive -Path ".\Distribuicao\*" -DestinationPath "PLSQLMigrationTool_v1.0.zip"
```

## Requisitos do Sistema de Destino

Para executar o aplicativo compilado, o sistema precisa ter:

1. **Windows 7 SP1 ou superior**
   - Windows 7, 8, 8.1, 10, 11
   - Windows Server 2008 R2 ou superior

2. **.NET Framework 4.8 Runtime**
   - Download: https://dotnet.microsoft.com/download/dotnet-framework/net48
   - Geralmente já instalado no Windows 10/11

3. **Acesso de rede ao banco Oracle**
   - Porta 1521 (padrão) liberada no firewall
   - Conectividade TCP/IP com o servidor Oracle

## Solução de Problemas de Compilação

### Erro: "The type or namespace name 'Oracle' could not be found"

**Solução:**
```cmd
# Limpar e restaurar pacotes
nuget restore PLSQLMigrationTool.sln -Force
```

### Erro: "MSBuild is not recognized"

**Solução:**
Use o Developer Command Prompt ou adicione MSBuild ao PATH:
```cmd
set PATH=%PATH%;C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin
```

### Erro: "The SDK 'Microsoft.NET.Sdk' specified could not be found"

**Solução:**
Este projeto usa o formato antigo de .csproj (não SDK-style). Certifique-se de usar MSBuild do Visual Studio, não o dotnet CLI.

### Erro: "Could not load file or assembly 'Oracle.ManagedDataAccess'"

**Solução:**
1. Verifique se o arquivo `Oracle.ManagedDataAccess.dll` está na mesma pasta do executável
2. Ou reinstale o pacote NuGet:
   ```cmd
   nuget install Oracle.ManagedDataAccess -Version 23.7.0 -OutputDirectory packages
   ```

### Aviso: "Found conflicts between different versions of the same dependent assembly"

**Solução:**
Este aviso pode ser ignorado. O binding redirect no App.config resolve isso automaticamente.

## Verificação da Compilação

Após compilar, verifique:

✅ **Arquivo executável criado:**
```
PLSQLMigrationTool\bin\Release\PLSQLMigrationTool.exe
```

✅ **Tamanho aproximado:** 50-100 KB

✅ **DLL Oracle presente:**
```
PLSQLMigrationTool\bin\Release\Oracle.ManagedDataAccess.dll
```

✅ **Tamanho aproximado:** 4-5 MB

✅ **Config presente:**
```
PLSQLMigrationTool\bin\Release\PLSQLMigrationTool.exe.config
```

## Teste Rápido

Após compilar, teste o executável:

1. Execute `PLSQLMigrationTool.exe`
2. A janela principal deve abrir
3. Vá para aba "Conexão"
4. Clique em "Testar Conexão" (mesmo sem string válida)
5. Deve aparecer mensagem de erro (esperado sem banco)
6. Se aparecer, a compilação foi bem-sucedida!

## Configurações Avançadas

### Alterar Ícone do Aplicativo

1. Crie ou obtenha um arquivo `.ico`
2. No Visual Studio, clique com botão direito no projeto
3. **Properties** > **Application** > **Icon and manifest**
4. Selecione o arquivo `.ico`
5. Recompile

### Alterar Informações de Versão

Edite `Properties\AssemblyInfo.cs`:

```csharp
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: AssemblyCompany("Sua Empresa")]
[assembly: AssemblyCopyright("Copyright © 2025")]
```

### Habilitar Otimizações

No arquivo `.csproj`, certifique-se de que na configuração Release:

```xml
<PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
  <Optimize>true</Optimize>
  <DebugType>pdbonly</DebugType>
</PropertyGroup>
```

## Criando Instalador (Opcional)

Para criar um instalador profissional, use:

### Opção 1: Inno Setup (Gratuito)
- Download: https://jrsoftware.org/isinfo.php
- Crie script de instalação

### Opção 2: WiX Toolset (Gratuito)
- Download: https://wixtoolset.org/
- Integração com Visual Studio

### Opção 3: Advanced Installer (Comercial)
- Download: https://www.advancedinstaller.com/
- Interface visual intuitiva

## Suporte

Para problemas de compilação:

1. Verifique se todos os pré-requisitos estão instalados
2. Tente limpar e recompilar: **Build** > **Clean Solution** > **Build Solution**
3. Verifique a janela **Error List** no Visual Studio
4. Consulte a documentação do Visual Studio

## Checklist Final

Antes de distribuir, verifique:

- [ ] Compilação em modo Release
- [ ] Testado em máquina limpa (sem Visual Studio)
- [ ] .NET Framework 4.8 instalado na máquina de teste
- [ ] Aplicativo abre sem erros
- [ ] Conexão com Oracle funciona
- [ ] Todas as funcionalidades testadas
- [ ] README.md incluído
- [ ] GUIA_RAPIDO.md incluído
- [ ] Versão documentada

## Versão

**Versão do Documento**: 1.0  
**Data**: Novembro 2025  
**Compatível com**: Visual Studio 2019/2022, .NET Framework 4.8
