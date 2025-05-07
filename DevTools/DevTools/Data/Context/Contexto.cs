namespace DevTools.Data.Context;

public class Contexto
{
    public static Contexto Instance { get; private set; }
    public static ContextoUtil Util => Instance?.ContextoUtil;
    public ContextoUtil ContextoUtil { get; private set; }

    public string CurrentBranch => Util.GetBranchPath();


    public static void AssignNewInstance()
    {
        // Garante que a configuração seja carregada
        var _utils = new ContextoUtil();

        string branch = _utils.Configuration?.GetSection("CloverCRM")?["CurrentBranch"];

        // Valida ou solicita a CurrentBranch
        if (string.IsNullOrEmpty(branch))
        {
            Console.Clear();
            Views.MainView.DisplayMainHeader(); // Assumindo que MainView está no namespace Views
            branch = _utils.SolicitarBranch();
            _utils.SalvarBranch(branch);
        }

        _utils.LoadCloverCRMAssemblys();

        Instance = new Contexto
        {
            ContextoUtil = _utils,
        };
    }
    
}
