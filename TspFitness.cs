using GeneticSharp.Extensions;
using GeneticSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPGeneticAlgorithm
{
    public class TspFitness : IFitness
    {
        private readonly int[][] _cities;

        public TspFitness(int[][] cities)
        {
            _cities = cities;
        }

        public double Evaluate(IChromosome chromosome)
        {
            var route = ((TspChromosome)chromosome).GetCitiesOrder().ToArray();

            var distance = 0.0;
            for (int i = 0; i < route.Length - 1; i++)
            {
                var fromCity = _cities[route[i]];
                var toCity = _cities[route[i + 1]];
                distance += Math.Sqrt(Math.Pow(toCity[0] - fromCity[0], 2) + Math.Pow(toCity[1] - fromCity[1], 2));
            }

            return -distance;
        }
    }
}
