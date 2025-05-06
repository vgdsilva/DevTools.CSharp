namespace DevTools.Data.Context;

public class Contexto
{
    public static Contexto Instance { get; private set; }
    public static ContextoUtil Util => Instance?.ContextoUtil;
    public ContextoUtil ContextoUtil { get; private set; }
    public string CurrentBranch { get; private set; }

    

    public static void AssignNewInstance()
    {
        // Garante que a configuração seja carregada
        var _utils = new ContextoUtil();

        // Valida ou solicita a CurrentBranch
        if ( string.IsNullOrEmpty(_utils.Configuration?.GetSection("CloverCRM")?["CurrentBranch"]) )
        {
            Console.Clear();
            Views.MainView.DisplayMainHeader(); // Assumindo que MainView está no namespace Views
            string branch = _utils.SolicitarBranch();
            _utils.SalvarBranch(branch);
        }
    }


    
}
