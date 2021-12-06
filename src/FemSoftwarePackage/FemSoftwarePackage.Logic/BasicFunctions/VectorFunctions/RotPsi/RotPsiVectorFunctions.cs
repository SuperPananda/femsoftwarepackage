using System.Collections.Generic;
using FemSoftwarePackage.Logic.BasicFunctions.Base;

namespace FemSoftwarePackage.Logic.BasicFunctions.VectorFunctions.RotPsi
{
    public class RotPsiVectorFunctions
    {
        public IEnumerable<IBaseBasicFunction> Functions { get; set; }

        public RotPsiVectorFunctions()
        {
            Functions = GetFunction();
        }

        private static IEnumerable<IBaseBasicFunction> GetFunction()
        {
            yield return new RotPsi1();
            yield return new RotPsi2();
            yield return new RotPsi3();
            yield return new RotPsi4();
            yield return new RotPsi5();
            yield return new RotPsi6();
            yield return new RotPsi7();
            yield return new RotPsi8();
            yield return new RotPsi9();
            yield return new RotPsi10();
            yield return new RotPsi11();
            yield return new RotPsi12();
        }
    }
}
