using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PLSQLMigrationTool.Data;
using PLSQLMigrationTool.Models;

namespace PLSQLMigrationTool.Business
{
    /// <summary>
    /// Gerencia exportação de estruturas de banco de dados
    /// </summary>
    public class ExportManager
    {
        private OracleQueryExecutor _queryExecutor;
        private MetadataRepository _metadataRepository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="queryExecutor">Executor de queries</param>
        /// <param name="metadataRepository">Repositório de metadados</param>
        public ExportManager(OracleQueryExecutor queryExecutor, MetadataRepository metadataRepository)
        {
            _queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
            _metadataRepository = metadataRepository ?? throw new ArgumentNullException(nameof(metadataRepository));
        }

        /// <summary>
        /// Exporta DDL de tabelas selecionadas
        /// </summary>
        /// <param name="tableNames">Lista de nomes de tabelas</param>
        /// <param name="includeConstraints">Incluir constraints</param>
        /// <param name="includeForeignKeys">Incluir foreign keys</param>
        /// <param name="outputFilePath">Caminho do arquivo de saída</param>
        public void ExportTablesDDL(List<string> tableNames, bool includeConstraints, bool includeForeignKeys, string outputFilePath)
        {
            if (tableNames == null || tableNames.Count == 0)
            {
                throw new ArgumentException("Nenhuma tabela selecionada para exportação.");
            }

            if (string.IsNullOrEmpty(outputFilePath))
            {
                throw new ArgumentException("Caminho do arquivo de saída não pode ser vazio.");
            }

            StringBuilder ddlScript = new StringBuilder();
            
            // Cabeçalho do script
            ddlScript.AppendLine("-- ========================================");
            ddlScript.AppendLine("-- Script de Exportação DDL");
            ddlScript.AppendLine($"-- Gerado em: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
            ddlScript.AppendLine($"-- Tabelas: {tableNames.Count}");
            ddlScript.AppendLine("-- ========================================");
            ddlScript.AppendLine();

            // Obter todas as constraints se necessário
            List<ConstraintInfo> allConstraints = null;
            if (includeConstraints || includeForeignKeys)
            {
                allConstraints = _metadataRepository.GetAllConstraints();
            }

            // Exportar cada tabela
            foreach (string tableName in tableNames)
            {
                try
                {
                    ddlScript.AppendLine($"-- ========================================");
                    ddlScript.AppendLine($"-- Tabela: {tableName}");
                    ddlScript.AppendLine($"-- ========================================");
                    ddlScript.AppendLine();

                    // DDL da tabela
                    string tableDDL = _metadataRepository.GetTableDDL(tableName);
                    if (!string.IsNullOrEmpty(tableDDL))
                    {
                        ddlScript.AppendLine(tableDDL);
                        ddlScript.AppendLine();
                    }

                    // Constraints da tabela
                    if (includeConstraints && allConstraints != null)
                    {
                        List<ConstraintInfo> tableConstraints = allConstraints.FindAll(c => 
                            c.TableName == tableName && !c.IsForeignKey);

                        if (tableConstraints.Count > 0)
                        {
                            ddlScript.AppendLine($"-- Constraints da tabela {tableName}");
                            
                            foreach (ConstraintInfo constraint in tableConstraints)
                            {
                                try
                                {
                                    string constraintDDL = _metadataRepository.GetConstraintDDL(constraint.ConstraintName);
                                    if (!string.IsNullOrEmpty(constraintDDL))
                                    {
                                        ddlScript.AppendLine(constraintDDL);
                                        ddlScript.AppendLine();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ddlScript.AppendLine($"-- Erro ao obter DDL da constraint {constraint.ConstraintName}: {ex.Message}");
                                }
                            }
                        }
                    }

                    // Foreign Keys da tabela
                    if (includeForeignKeys && allConstraints != null)
                    {
                        List<ConstraintInfo> foreignKeys = allConstraints.FindAll(c => 
                            c.TableName == tableName && c.IsForeignKey);

                        if (foreignKeys.Count > 0)
                        {
                            ddlScript.AppendLine($"-- Foreign Keys da tabela {tableName}");
                            
                            foreach (ConstraintInfo fk in foreignKeys)
                            {
                                try
                                {
                                    string fkDDL = _metadataRepository.GetConstraintDDL(fk.ConstraintName);
                                    if (!string.IsNullOrEmpty(fkDDL))
                                    {
                                        ddlScript.AppendLine(fkDDL);
                                        ddlScript.AppendLine();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ddlScript.AppendLine($"-- Erro ao obter DDL da FK {fk.ConstraintName}: {ex.Message}");
                                }
                            }
                        }
                    }

                    ddlScript.AppendLine();
                }
                catch (Exception ex)
                {
                    ddlScript.AppendLine($"-- Erro ao exportar tabela {tableName}: {ex.Message}");
                    ddlScript.AppendLine();
                }
            }

            // Rodapé do script
            ddlScript.AppendLine("-- ========================================");
            ddlScript.AppendLine("-- Fim do Script");
            ddlScript.AppendLine("-- ========================================");

            // Salvar arquivo
            File.WriteAllText(outputFilePath, ddlScript.ToString(), Encoding.UTF8);
        }

        /// <summary>
        /// Exporta apenas constraints selecionadas
        /// </summary>
        /// <param name="constraints">Lista de constraints</param>
        /// <param name="outputFilePath">Caminho do arquivo de saída</param>
        public void ExportConstraints(List<ConstraintInfo> constraints, string outputFilePath)
        {
            if (constraints == null || constraints.Count == 0)
            {
                throw new ArgumentException("Nenhuma constraint selecionada para exportação.");
            }

            if (string.IsNullOrEmpty(outputFilePath))
            {
                throw new ArgumentException("Caminho do arquivo de saída não pode ser vazio.");
            }

            StringBuilder ddlScript = new StringBuilder();
            
            ddlScript.AppendLine("-- ========================================");
            ddlScript.AppendLine("-- Script de Exportação de Constraints");
            ddlScript.AppendLine($"-- Gerado em: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
            ddlScript.AppendLine($"-- Constraints: {constraints.Count}");
            ddlScript.AppendLine("-- ========================================");
            ddlScript.AppendLine();

            foreach (ConstraintInfo constraint in constraints)
            {
                try
                {
                    ddlScript.AppendLine($"-- Constraint: {constraint.ConstraintName} ({constraint.ConstraintTypeDescription})");
                    ddlScript.AppendLine($"-- Tabela: {constraint.TableName}");
                    
                    string constraintDDL = _metadataRepository.GetConstraintDDL(constraint.ConstraintName);
                    if (!string.IsNullOrEmpty(constraintDDL))
                    {
                        ddlScript.AppendLine(constraintDDL);
                        ddlScript.AppendLine();
                    }
                }
                catch (Exception ex)
                {
                    ddlScript.AppendLine($"-- Erro ao exportar constraint {constraint.ConstraintName}: {ex.Message}");
                    ddlScript.AppendLine();
                }
            }

            ddlScript.AppendLine("-- ========================================");
            ddlScript.AppendLine("-- Fim do Script");
            ddlScript.AppendLine("-- ========================================");

            File.WriteAllText(outputFilePath, ddlScript.ToString(), Encoding.UTF8);
        }

        /// <summary>
        /// Gera script para reabilitar constraints
        /// </summary>
        /// <param name="constraints">Lista de constraints</param>
        /// <param name="outputFilePath">Caminho do arquivo de saída</param>
        public void GenerateEnableConstraintsScript(List<ConstraintInfo> constraints, string outputFilePath)
        {
            if (constraints == null || constraints.Count == 0)
            {
                throw new ArgumentException("Nenhuma constraint selecionada.");
            }

            if (string.IsNullOrEmpty(outputFilePath))
            {
                throw new ArgumentException("Caminho do arquivo de saída não pode ser vazio.");
            }

            StringBuilder script = new StringBuilder();
            
            script.AppendLine("-- ========================================");
            script.AppendLine("-- Script para Reabilitar Constraints");
            script.AppendLine($"-- Gerado em: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
            script.AppendLine("-- ========================================");
            script.AppendLine();

            foreach (ConstraintInfo constraint in constraints)
            {
                script.AppendLine($"-- {constraint.ConstraintName} ({constraint.ConstraintTypeDescription})");
                script.AppendLine($"ALTER TABLE {constraint.TableName} ENABLE CONSTRAINT {constraint.ConstraintName};");
                script.AppendLine();
            }

            File.WriteAllText(outputFilePath, script.ToString(), Encoding.UTF8);
        }
    }
}
