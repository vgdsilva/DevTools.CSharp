using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.CodeGenerator.Interfaces;

public interface IFileGenerator
{
    void Generate(string entity);
}
