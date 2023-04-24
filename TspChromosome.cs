using GeneticSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPGeneticAlgorithm
{
    public class TspChromosome : ChromosomeBase
    {
        private readonly int[] _cities;

        public TspChromosome(int length) : base(length)
        {
            _cities = Enumerable.Range(0, length).ToArray();
            var genes = _cities.Select(c => new Gene(c)).ToArray();
            ReplaceGenes(0, genes);
        }

        public TspChromosome(int[] citiesOrder) : base(citiesOrder.Length)
        {
            _cities = citiesOrder;
            var genes = _cities.Select(c => new Gene(c)).ToArray();
            ReplaceGenes(0, genes);
        }

        public int[] GetCitiesOrder()
        {
            return _cities;
        }

        public override Gene GenerateGene(int geneIndex)
        {
            throw new NotImplementedException();
        }

        public override IChromosome CreateNew()
        {
            return new TspChromosome(_cities.Length);
        }

        public void ReverseSequence(int startIndex, int endIndex)
        {
            while (startIndex < endIndex)
            {
                var temp = GetGene(endIndex);
                ReplaceGene(endIndex, GetGene(startIndex));
                ReplaceGene(startIndex, temp);
                startIndex++;
                endIndex--;
            }
        }
    }
}
