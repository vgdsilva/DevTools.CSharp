using DevTools.Utils.Executables;
using DevTools.Utils.Models;
using DevTools.Views;

namespace DevTools.Data;

public static class MockData
{
    public static List<MenuOption> MainMenuOptions = new List<MenuOption>
    {
        new MenuOption(new[] { "c", "code" }, "Gera códigos C# automaticamente", CodeView.Show),
        new MenuOption("iniciarSincronizador", "Inicia o projeto .bat do projeto de sincronização (CRMSync)", args => CRMSyncExecute.Start()),
        new MenuOption(new[] { "m", "migrations" }, "Geração de Migrations", (args) => { MigrationsView.Show(); }),
        new MenuOption(new[] { "trdz", "traducoes" }, "Geração ou manipulação das traduções", (args) => TraducoesView.Show()),
        new MenuOption("sair", "Encerra este prompt", args => Environment.Exit(0)),
    };

    public static List<MenuOption> MainOptions = new List<MenuOption>
    {
        MenuOption.InstanceNew(CodeView.Show, "Gera códigos C# automaticamente", "code"),
        MenuOption.InstanceNew(args => MigrationsView.Show(), "Geração de Migrations", "migrations"),
        MenuOption.InstanceNew(args => TraducoesView.Show(), "Geração ou manipulação das traduções", "trdz", "traducoes"),
        MenuOption.InstanceNew(args => {}, "Manipulação do projeto de sincronização (CRMSync)", "sync", "sincronizador"),
        MenuOption.InstanceNew(args => Environment.Exit(0), "Encerra este prompt", "sair"),
    };
}
