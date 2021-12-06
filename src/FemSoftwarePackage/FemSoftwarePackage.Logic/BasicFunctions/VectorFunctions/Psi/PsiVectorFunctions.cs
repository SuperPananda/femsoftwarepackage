using System.Collections.Generic;
using FemSoftwarePackage.Logic.BasicFunctions.Base;

namespace FemSoftwarePackage.Logic.BasicFunctions.VectorFunctions.Psi
{
    public class PsiVectorFunctions
    {
        public IEnumerable<IBaseBasicFunction> Functions { get; set; }

        public PsiVectorFunctions()
        {
            Functions = GetFunction();
        }

        private static IEnumerable<IBaseBasicFunction> GetFunction()
        {
            yield return new Psi1();
            yield return new Psi2();
            yield return new Psi3();
            yield return new Psi4();
            yield return new Psi5();
            yield return new Psi6();
            yield return new Psi7();
            yield return new Psi8();
            yield return new Psi9();
            yield return new Psi10();
            yield return new Psi11();
            yield return new Psi12();
        }
    }
}
