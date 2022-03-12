using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Bombs_Away
{
    public static class Collision
    {

        public static float DotProduct(float x1, float y1, float x2, float y2) {
           return x1 * x2 + y1 * y2;
        }
        public static bool AreCollinear(float x1, float y1, float x2, float y2, float x3, float y3) {
            if(Convert.ToInt16(1000000*(x1 * y2 + x2 * y3 + x3 * y1 - x3 * y2 - y3 * x1 - y1 * x2)) == 0)
                return true;
            return false;
        }

        public static bool TestLineLineCollision(float xA1, float yA1, float xB1, float yB1, float xA2, float yA2, float xB2, float yB2) {
            float x = ((yA2 - yA1) * (xB1 - xA1) * (xB2 - xA2) + xA1 * (yB1 - yA1) * (xB2 - xA2) - xA2 * (yB2 - yA2) * (xB1 - xA1)) / ((yB1 - yA1) * (xB2 - xA2) - (yB2 - yA2) * (xB1 - xA1));
            float y = ((x - xA1) * (yB1 - yA1)) / (xB1 - xA1) + yA1;
            if(0 <= DotProduct(xB1 - xA1, yB1 - yA1, x - xA1, y - yA1) && DotProduct(xB1 - xA1, yB1 - yA1, x - xA1, y - yA1) <= DotProduct(xB1 - xA1, yB1 - yA1, xB1 - xA1, yB1 - yA1) &&
               0 <= DotProduct(xB2 - xA2, yB2 - yA2, x - xA2, y - yA2) && DotProduct(xB2 - xA2, yB2 - yA2, x - xA2, y - yA2) <= DotProduct(xB2 - xA2, yB2 - yA2, xB2 - xA2, yB2 - yA2))
                return true;
            return false;

        }
    }
}
