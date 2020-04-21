using System;

namespace DTFSZTIR.Evaluators
{
    public class RelativeChangeEvaluator
    {
        //relatív változások súlyozott összege
        public static double F(double[] fx, double[] fy, double[] w, int K)
        //2 célfgvek tömbje, prioritásokat tartalmazó vektor, célfgv száma
        {
            double F = 0;
            double a, b;
            double D;

            for (int k = 0; k < K; k++)
            {
                a = fx[k];
                b = fy[k];
                //D (distance) értékének meghatározása
                if (Math.Max(a, b) == 0)
                {
                    D = 0;
                }
                else
                {
                    D = (b - a) / Math.Max(a, b);
                }

                //Összeszummázzuk, adott prioritással
                F += w[k] * D;
            }
            return F;
        }
    }
}
