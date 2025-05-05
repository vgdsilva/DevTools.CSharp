using DevTools.Utils.Models;

namespace DevTools.Data;

public static class MockData
{
    public static List<MenuOption> MainMenuOptions = new List<MenuOption>
    {
        new MenuOption(
            new[] { "c", "code" },
            "Gera códigos C# automaticamente",
            CodeGenerator.Initialize.Start
        ),
        new MenuOption(
            new[] { "m", "migrations" }, 
            "Geração de Migrations",
            (args) => { throw new NotImplementedException(); }
        ),
        new MenuOption(
            new[] { "trdz", "traducoes" }, 
            "Geração ou manipulação das traduções", 
            (args) => { throw new NotImplementedException(); }
        ),
        new MenuOption(
            new[] { "sair" }, 
            "Encerra este prompt", 
            args => Environment.Exit(0)
        ),
    };
}
