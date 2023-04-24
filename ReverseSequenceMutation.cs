using GeneticSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPGeneticAlgorithm
{
    public class ReverseSequenceMutation : IMutation
    {
        public void Mutate(IChromosome chromosome, float probability)
        {
            if (RandomizationProvider.Current.GetDouble() > probability)
            {
                return;
            }

            var tspChromosome = (TspChromosome)chromosome;

            var indexes = RandomizationProvider.Current.GetUniqueInts(2, 0, tspChromosome.Length);
            var index1 = Math.Min(indexes[0], indexes[1]);
            var index2 = Math.Max(indexes[0], indexes[1]);

            tspChromosome.ReverseSequence(index1, index2);
        }
        public bool IsOrdered
        {
            get { return false; }
        }
    }
}
