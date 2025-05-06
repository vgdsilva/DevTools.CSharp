using DevTools.Utils.Interactive;
using DevTools.Utils.Interfaces;

namespace DevTools.Utils.Generator;

public class EntityGenerator : IFileGenerator
{
    private readonly bool _isSyncEntity;
    private readonly bool _gerarBO;
    private readonly bool _gerarDAO;
    private readonly bool _gerarDTO;
    private readonly bool _gerarControllerApi;
    public EntityGenerator()
    {
        _isSyncEntity = InteractiveYesNoPrompt.Ask("É uma entidade que será sincronizada? (Os dados iram para o mobile/SQLite)", "Sim", "Não");
        _gerarBO = InteractiveYesNoPrompt.Ask("Deseja gerar o arquivo BO?", "Sim", "Não");
        _gerarDAO = InteractiveYesNoPrompt.Ask("Deseja gerar o arquivo DAO?", "Sim", "Não");
        _gerarDTO = InteractiveYesNoPrompt.Ask("Deseja gerar o arquivo DTO?", "Sim", "Não");
        _gerarControllerApi = InteractiveYesNoPrompt.Ask("Deseja gerar o arquivo Controller API?", "Sim", "Não");

        Console.WriteLine();
    }

    public void Generate(string entity)
    {
        throw new NotImplementedException();
    }
}
