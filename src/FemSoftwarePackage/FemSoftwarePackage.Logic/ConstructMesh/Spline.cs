using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FemSoftwarePackage.Logic.ConstructMesh
{
    public class Spline
    {
        public CreateSpline()
        {
            foreach (var elem in finite)
            {
                
            }

            b = np.zeros(shape = (16))
            matrix = np.zeros(shape = (16, 16))
            x1 = Setpoints[elem[0]][0]
            x2 = Setpoints[elem[15]][0]
            hx = x2 - x1
            y1 = Setpoints[elem[0]][1]
            y2 = Setpoints[elem[15]][1]
            hy = y2 - y1
            for i in range(16):
            for j in range(16):
            for k in range(len(a)):
            if x1 <= a[k][0] and a[k][0] <= x2 and y1 <= a[k][1] and a[k][1] <= y2:
            matrix[i][j] += 1 * pfi_lag(i, a[k][0], x1, x2, a[k][1], y1, y2) * pfi_lag(j, a[k][0], x1, x2, a[k][1], y1, y2)

            for i in range(16):
            for k in range(len(a)):
            if x1 <= a[k][0] and a[k][0] <= x2 and y1 <= a[k][1] and a[k][1] <= y2:
            b[i] += 1 * pfi_lag(i, a[k][0], x1, x2, a[k][1], y1, y2) * a[k][2]

            A1 = np.zeros(shape = (16, 16))
            e = np.array([-1 * sqrt(3 / 7 - 2 / 7 * sqrt(6 / 5)), sqrt(3 / 7 - 2 / 7 * sqrt(6 / 5)), -1 * sqrt(3 / 7 + 2 / 7 * sqrt(6 / 5)), sqrt(3 / 7 + 2 / 7 * sqrt(6 / 5))])
            w = np.array([(18 + sqrt(30)) / 36, (18 + sqrt(30)) / 36, (18 - sqrt(30)) / 36, (18 - sqrt(30)) / 36])
            J = (x2 - x1) * (y2 - y1) / 4
            for i in range(16):
            for j in range(16):
            for k in range(len(w)):
            for l in range(len(w)):
            xsi = ((x2 + x1) / 2.0) + ((x2 - x1) / 2.0) * e[k]
            nsi = ((y2 + y1) / 2.0) + ((y2 - y1) / 2.0) * e[l]
            A1[i][j] += 0.0 * w[k] * w[l] * d_pfi_lag_dx(i, xsi, x1, x2, nsi, y1, y2) * d_pfi_lag_dy(i, xsi, x1, x2, nsi, y1, y2) * d_pfi_lag_dx(j, xsi, x1, x2, nsi, y1, y2) * d_pfi_lag_dy(j, xsi, x1, x2, nsi, y1, y2) * J

            matrix = matrix + A1

            sborka(elem, matrix, b)
        }
    }
}
