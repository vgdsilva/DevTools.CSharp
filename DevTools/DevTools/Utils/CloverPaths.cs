using DevTools.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.Utils;

public class CloverPaths
{
    public static string MainDirectoryPath = Configuration.Instance["CurrentBranch"];

    // REGRAS FOLDER
    public static string RegrasFolderPath = Path.Combine(MainDirectoryPath, "regras", "CRM.Comum");

    public static string RegrasProjectPath = Path.Combine(RegrasFolderPath, "CRM.Regras", "CRM.Regras.csproj");
    public static string RegrasProjectDllPath = Path.Combine(RegrasFolderPath, "CRM.Regras", "bin", "Debug", "netstandard2.0", "CRM.Regras.dll");

    public static string DAOProjectPath = Path.Combine(RegrasFolderPath, "CRM.DAO", "CRM.DAO.csproj");
    public static string DAOProjectDllPath = Path.Combine(RegrasFolderPath, "CRM.DAO", "bin", "Debug", "netstandard2.0", "CRM.DAO.dll");

    public static string EntidadeProjectPath = Path.Combine(RegrasFolderPath, "CRM.Entidades", "CRM.Entidades.csproj");
    public static string EntidadeProjectDllPath = Path.Combine(RegrasFolderPath, "CRM.Entidades", "bin", "Debug", "netstandard2.0", "CRM.Entidades.dll");

    // DB FOLDER    
    public static string DbFolderPath = Path.Combine(MainDirectoryPath, "db");
    public static string CRMSyncProjectPath = Path.Combine(DbFolderPath, "CRMSync", "CRMSync.Web.Api", "CRMSync.Web.Api.csproj");
    
    public static string PostgreSQLFolderPath = Path.Combine(DbFolderPath, "PostgreSQL");

    public static string DbProjectPath = Path.Combine(DbFolderPath, "CRM.DB", "CRM.DB.csproj");
}
