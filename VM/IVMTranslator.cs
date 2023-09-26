using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
    public interface IVMTranslator
    {
        void TranslateVmToAsm(string file);
    }
}
