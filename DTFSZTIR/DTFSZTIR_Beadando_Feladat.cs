//F, Calm | perm, si,j,m, Trk,l | [Cmax, Tmax,Sum(Ti),Sum(Ui)]

using DTFSZTIR.Models;
using System;

namespace DTFSZTIR
{
    public class DTFSZTIR_Beadando_Feladat
    {
        static void Main(string[] args)
        {
            double[] f = new double[4]; //célfgvek
            double[] w = new double[4];//célfgvek prioritása

            int numberOfJobs; //munkák száma
            int numberOfResources; //erőforrások száma
            Job[] jobs; // munkák
            Resource[] resources; //erőforrások
            int[] solution; //közös ütemterv            
            int cut_mode; //operációk TimeWindowra illesztése: 0 nem megszakítható, 1 megszakítható
            int STEP;
            int LOOP;

            Console.WriteLine("Feladatunk: F, Calm | perm, si,j,m, Trk,l | [Cmax, Tmax, Sum(Ti), Sum(Ui)]");
            Console.WriteLine("Adja meg a munkak szamat!");
            numberOfJobs = int.Parse(Console.ReadLine());

            Console.WriteLine("Adja meg az eroforrasok szamat!");
            numberOfResources = int.Parse(Console.ReadLine());

            Console.WriteLine("Célfuggvenyek prioritasa:");
            for (int i = 0; i < w.Length; i++)
            {
                Console.WriteLine(i + 1 + ". prioritas:");
                w[i] = int.Parse(Console.ReadLine());
            }

            Console.WriteLine("Az operációk megszakíthatók? (1-igen, 0-nem)");
            cut_mode = int.Parse(Console.ReadLine());

            resources = new Resource[numberOfResources];
            Random rand = new Random();
            for (int i = 0; i < numberOfResources; i++)
            {
                resources[i] = new Resource
                {
                    Id = i,
                    TransportTime = new int[numberOfResources]
                };
                for (int j = 0; j < numberOfResources; j++)
                {
                    resources[i].TransportTime[j] = rand.Next(10, 20);
                }

                resources[i].SetupTime = new int[numberOfJobs, numberOfJobs];
                for (int k = 0; k < numberOfJobs; k++)
                {
                    for (int l = 0; l < numberOfJobs; l++)
                    {
                        if (k == l)
                        {
                            resources[i].SetupTime[k, l] = 0;
                        }
                        else
                        {
                            resources[i].SetupTime[k, l] = rand.Next(10, 100);
                        }

                    }
                }

                //intervallumok
                resources[i].IntervalNumber = rand.Next(2, 10);
                resources[i].Intervals = new TimeWindow[resources[i].IntervalNumber];
                for (int m = 0; m < resources[i].IntervalNumber; m++)
                {
                    resources[i].Intervals[m] = new TimeWindow();

                    if (m == 0)
                    {
                        resources[i].Intervals[m].StartTime = rand.Next(10, 30);
                    }
                    else
                    {
                        resources[i].Intervals[m].StartTime = rand.Next(resources[i].Intervals[m - 1].EndTime, resources[i].Intervals[m - 1].EndTime + 30);
                    }
                    resources[i].Intervals[m].EndTime = rand.Next(resources[i].Intervals[m].StartTime, resources[i].Intervals[m].StartTime + 100);
                }
            }

            solution = new int[numberOfJobs];
            jobs = new Job[numberOfJobs];
            for (int i = 0; i < numberOfJobs; i++)
            {
                jobs[i] = new Job
                {
                    Id = i,
                    ProcessTime = new int[numberOfResources]
                };
                for (int j = 0; j < numberOfResources; j++)
                {
                    jobs[i].ProcessTime[j] = rand.Next(1, 100);
                }
                jobs[i].StartTime = new int[numberOfResources];
                jobs[i].EndTime = new int[numberOfResources];

                //határidő
                jobs[i].DueDate = rand.Next(100, 5000);
                solution[i] = numberOfJobs - i - 1;
            }

            //ad-hoc sorrend
            Console.WriteLine("\nAd-hoc sorrend:");
            Simulators.Simulator.Simulate(jobs, numberOfJobs, resources, numberOfResources, solution, 0);
            Console.WriteLine("Cmax értéke:{0}", jobs[solution[numberOfJobs - 1]].EndTime[numberOfResources - 1]);

            //Johnson_alg - F2
            Console.WriteLine("\nJohnson-algoritmus:");
            Schedulers.Johnson_Scheduler.Schedule(jobs, numberOfJobs, 0, solution);
            Simulators.Simulator.Simulate(jobs, numberOfJobs, resources, numberOfResources, solution, 0);
            Console.WriteLine("Cmax értéke:{0}", jobs[solution[numberOfJobs - 1]].EndTime[numberOfResources - 1]);

            //Kiterjesztett Johnson-alg - F3
            Console.WriteLine("\nKiterjesztett Johnson-algoritmus:");
            Schedulers.ExpandedJohnson_Scheduler.Schedule(jobs, numberOfJobs, 0, solution);
            Simulators.Simulator.Simulate(jobs, numberOfJobs, resources, numberOfResources, solution, 0);
            Console.WriteLine("Cmax értéke:{0}", jobs[solution[numberOfJobs - 1]].EndTime[numberOfResources - 1]);

            //Palmer - Fm
            Console.WriteLine("\nPalmer algoritmus:");
            Schedulers.Palmer_Scheduler.Schedule(jobs, numberOfJobs, numberOfResources, solution);
            Simulators.Simulator.Simulate(jobs, numberOfJobs, resources, numberOfResources, solution, 0);
            Console.WriteLine("Cmax értéke:{0}", jobs[solution[numberOfJobs - 1]].EndTime[numberOfResources - 1]);

            //Dannenbring - Fm
            Console.WriteLine("\nDannenbring algoritmus:");
            Schedulers.Dannenbring.Scheduler(jobs, numberOfJobs, numberOfResources, solution);
            Simulators.Simulator.Simulate(jobs, numberOfJobs, resources, numberOfResources, solution, 0);
            Console.WriteLine("Cmax értéke:{0}", jobs[solution[numberOfJobs - 1]].EndTime[numberOfResources - 1]);

            //CDS algoritmus - Fm 
            Console.WriteLine("\nCDS algoritmus:");
            Schedulers.CDS_Scheduler.Schedule(jobs, numberOfJobs, resources, numberOfResources, solution, cut_mode);
            Simulators.Simulator.Simulate(jobs, numberOfJobs, resources, numberOfResources, solution, 0);
            Console.WriteLine("Cmax értéke:{0}", jobs[solution[numberOfJobs - 1]].EndTime[numberOfResources - 1]);

            //Kereso
            Console.WriteLine("\nKereso algoritmus:");
            Console.WriteLine("Adja meg a kereso lepesek szamat:");
            STEP = int.Parse(Console.ReadLine());
            Console.WriteLine("Adja meg a szomszédok szamat:");
            LOOP = int.Parse(Console.ReadLine());
            Schedulers.RandomNeigbouralSearch_Scheduler.Search(jobs, numberOfJobs, resources, numberOfResources, solution, STEP, LOOP, cut_mode, w, f.Length);
            Simulators.Simulator.Simulate(jobs, numberOfJobs, resources, numberOfResources, solution, 0);

            //Result
            Evaluators.Evaluator.Evaluate(jobs, numberOfJobs, numberOfResources, solution, f);
            Console.WriteLine("\n\n Celfuggvenyek ertekei:");

            for (int k = 0; k < f.Length; k++)
            {
                Console.WriteLine("\n f[{0}] = {1}", k, f[k]);
            }

        }
    }
}
