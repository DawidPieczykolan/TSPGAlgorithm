using GeneticSharp;
using System.IO;

namespace TSPGeneticAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            var cities = ReadCitiesFromFile("bier127.tsp");

            var fitness = new TspFitness(cities);
            var chromosome = new TspChromosome(cities.Length);

            var population = new Population(500, 1000, chromosome);
            var selection = new EliteSelection(1);
            var crossover = new OrderedCrossover();
            var mutation = new ReverseSequenceMutation();

            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation)
            {
                Termination = new TimeEvolvingTermination(TimeSpan.FromMinutes(5))
            };

            ga.GenerationRan += (sender, e) =>
            {
                Console.WriteLine($"Generation {ga.GenerationsNumber}: {ga.BestChromosome.Fitness}");
            };

            ga.Start();

            var bestRoute = ((TspChromosome)ga.BestChromosome).GetCitiesOrder().ToArray();
            var distance = fitness.Evaluate(ga.BestChromosome);

            WriteRouteToFile("solution.txt", bestRoute, distance);

            Console.WriteLine($"Best route: {string.Join(" -> ", bestRoute)}, Distance: {distance}");
        }

        private static int[][] ReadCitiesFromFile(string filename)
        {
            var lines = File.ReadAllLines(filename);
            var startIndex = Array.IndexOf(lines, "NODE_COORD_SECTION") + 1;

            return lines
                .Skip(startIndex)
                .TakeWhile(l => !string.IsNullOrWhiteSpace(l))
                .Select(l =>
                {
                    var parts = l.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 3)
                    {
                        return new[] { int.Parse(parts[1]), int.Parse(parts[2]) };
                    }
                    else
                    {
                        return new[] { 0, 0 }; // or any other default value
                    }
                }).ToArray();
        }

        private static void WriteRouteToFile(string filename, int[] route, double distance)
        {
            var lines = route.Select((c, i) => $"{i + 1} {c}").ToArray();
            var content = new[] { $"DIMENSION: {route.Length}", $"DISTANCE: {distance:F}", "TOUR_SECTION" }
                .Concat(lines)
                .Concat(new[] { "-1" })
                .ToArray();

            File.WriteAllLines(filename, content);
        }
    }
}