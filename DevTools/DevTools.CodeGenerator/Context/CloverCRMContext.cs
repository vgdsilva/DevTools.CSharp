using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.Core.Context;

public class CloverCRMContext
{
    public string SelectedBranch { get; set; } = string.Empty;

    public CloverCRMContext(string selectedBranch)
    {
        this.SelectedBranch = selectedBranch;
    }
}
