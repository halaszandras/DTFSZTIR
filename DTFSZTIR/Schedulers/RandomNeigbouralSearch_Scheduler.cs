using DTFSZTIR.Models;
using System;

namespace DTFSZTIR.Schedulers
{
    public class RandomNeigbouralSearch_Scheduler
    {
        //Keresés - Fm
        public static void Search(Job[] jobs, int numberOfJobs, Resource[] resources, int numberOfResources, int[] solution, int STEP, int LOOP, int cut_mode, double[] w, int K)
        {
            int[] actualSolution = new int[numberOfJobs];
            int[] bestSolution = new int[numberOfJobs];
            int[] baseSolution = new int[numberOfJobs];
            int[] sol_ext = new int[numberOfJobs]; //legjobb szomszéd ütemterv

            //Tobbcélú
            double[] actual_f = new double[K];
            double[] best_f = new double[K];
            double[] f_ext = new double[K];

            Services.CopySolutionToNewInstance.Copy(baseSolution, solution, numberOfJobs); //kezdeti bázis
            Services.CopySolutionToNewInstance.Copy(bestSolution, solution, numberOfJobs); // legjobb ismert
            Simulators.Simulator.Simulate(jobs, numberOfJobs, resources, numberOfResources, bestSolution, 0);

            Evaluators.Evaluator.Evaluate(jobs, numberOfJobs, numberOfResources, bestSolution, best_f);

            for (int i = 1; i <= STEP; i++)
            {
                for (int j = 1; j < LOOP; j++)
                {
                    //szomszéd generálása
                    Neighbour(baseSolution, actualSolution, numberOfJobs);
                    //szimu
                    Simulators.Simulator.Simulate(jobs, numberOfJobs, resources, numberOfResources, actualSolution, 0);
                    //kiertekeles
                    Evaluators.Evaluator.Evaluate(jobs, numberOfJobs, numberOfResources, actualSolution, actual_f);

                    if (j == 0)
                    {//best szomszéd
                        Services.CopyGoalFunctionsToNewInstance.Copy(f_ext, actual_f, K);
                        Services.CopySolutionToNewInstance.Copy(sol_ext, actualSolution, numberOfJobs);
                    }
                    else
                    {
                        if (Evaluators.RelativeChangeEvaluator.F(f_ext, actual_f, w, K) < 0)
                        {
                            Services.CopyGoalFunctionsToNewInstance.Copy(f_ext, actual_f, K);
                            Services.CopySolutionToNewInstance.Copy(sol_ext, actualSolution, numberOfJobs);
                        }
                    }
                }

                //új bázist ad a legjobb szomszéd
                Services.CopySolutionToNewInstance.Copy(baseSolution, sol_ext, numberOfJobs);

                if (Evaluators.RelativeChangeEvaluator.F(f_ext, actual_f, w, K) < 0)
                {
                    Services.CopyGoalFunctionsToNewInstance.Copy(best_f, f_ext, K);
                    Services.CopySolutionToNewInstance.Copy(bestSolution, sol_ext, numberOfJobs);
                }
            }
            Services.CopySolutionToNewInstance.Copy(solution, bestSolution, numberOfJobs);
        }        

        private static void Neighbour(int[] sol_0, int[] actual_sol, int numberJob)
        {//pelda szomszédsagi operátorra
            int x;
            Random rand = new Random();
            //teljes masolat
            Services.CopySolutionToNewInstance.Copy(actual_sol, sol_0, numberJob);
            //modositas
            x = rand.Next(0, numberJob); // random pozicio
            if (x == 0)
            {
                actual_sol[x] = sol_0[numberJob - 1];
                actual_sol[numberJob - 1] = sol_0[x];
            }
            else
            {
                actual_sol[x] = sol_0[x - 1];
                actual_sol[x - 1] = sol_0[x];
            }
        }
    }
}

