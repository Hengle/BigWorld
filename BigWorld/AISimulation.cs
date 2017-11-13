using System;
using System.Collections.Generic;
using System.Linq;
using BigWorld.Entities.Components;
using BigWorld.Entities.Components.AI;
using BigWorld.Entities.Components.AI.Gens;
using BigWorld.Map;
using engenious;

namespace BigWorld
{
    public class AISimulation
    {
        
        

        private Player currentPlayer = null;
        private Random r = new Random();
        private Simulation simulation = new Simulation();
        
        public readonly List<Species> Specieses = new List<Species>();

        public int RunCounter { get; private set; }
        
        public double MaxFitness { get; private set; }
        public Genome BestGenome { get;  private set; }
        
        
        public Genome Run(int count,WorldMap map)
        {
            RunCounter = 0;
            
            simulation.Start(map);

            currentPlayer = simulation.AddPlayer();
            
            for (int i = 0; i < count; i++)
            {
                Update();
                RunCounter++;
            }

            
            return BestGenome;
        }

        private List<Genome> newGenomes = new List<Genome>();
        
        private void Update()
        {
            NeuronalNetworkComponent comp1 = null;

            currentPlayer.TryGetComponent(out comp1);

            List<Genome> genomes = new List<Genome>();
            List<Tuple<Genome, double>> positiveGenomes = new List<Tuple<Genome, double>>();

            genomes.AddRange(newGenomes);
            newGenomes.Clear();
            
            for (int i = genomes.Count; i < 128; i++)
            {
                var newGenome = new Genome(r.Next());
                newGenome.Add(new InputGen(comp1.Tick));
                newGenome.Add(new InputGen(comp1.Const));
                newGenome.Add(new InputGen(comp1.DeltaPositionX));
                newGenome.Add(new InputGen(comp1.DeltaPositionY));

                newGenome.Add(new OutputGen(comp1.MoveUp));
                newGenome.Add(new OutputGen(comp1.MoveDown));
                newGenome.Add(new OutputGen(comp1.MoveLeft));
                newGenome.Add(new OutputGen(comp1.MoveRight));

                newGenome.Mutate();

                genomes.Add(newGenome);
                OrderToSpecies(newGenome);

            }

            foreach (var genome in genomes)
            {
                comp1.Reset(genome);

                if (currentPlayer.TryGetComponent<PositionComponent>(out var position))
                {
                    position.Reset();
                }

                TimeSpan globalTime = new TimeSpan();
                TimeSpan elapsedSpan = TimeSpan.FromMilliseconds(20);

                for (int i = 0; i < 1000; i++)
                {
                    globalTime += elapsedSpan;
                    simulation.Update(new GameTime(globalTime, elapsedSpan));
                }

                if (currentPlayer.TryGetComponent<FitnessComponent>(out var fitnessComp))
                {
                    if (fitnessComp.Value > 0)
                    {
                        positiveGenomes.Add(new Tuple<Genome, double>(genome,
                            fitnessComp.Value / globalTime.TotalSeconds));
                    }
                    fitnessComp.Reset();
                }
            }

            if (positiveGenomes.Count > 0)
            {
                var goodGenomes = positiveGenomes.OrderByDescending(i => i.Item2).Take(64);

                var fitness = goodGenomes.First();
                if (fitness.Item2 > MaxFitness)
                {
                    BestGenome = fitness.Item1;
                    MaxFitness = fitness.Item2;
                }

                foreach (var genome in goodGenomes)
                {
                    var newGenome = genome.Item1.CreateNewGenation();
                    newGenome.Mutate();

                    OrderToSpecies(newGenome);

                    newGenomes.Add(newGenome);
                }

                var combinegenomes = goodGenomes.Take(8).ToArray();

                if (combinegenomes.Length == 8)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        var k = r.Next(combinegenomes.Length);
                        var l = r.Next(combinegenomes.Length);
                        if (k == l)
                            continue;

                        var firstGenome = combinegenomes[k].Item1;
                        var secoundGenome = combinegenomes[l].Item1;

                        var diff = firstGenome.Distance(secoundGenome);

                        if (diff < 3)
                        {
                            var childGenome = firstGenome.Combine(secoundGenome);
                            childGenome.Mutate();
                            OrderToSpecies(childGenome);
                            newGenomes.Add(childGenome);
                        }
                        else
                        {

                        }


                    }
                }
            }
            
            

        }



        public void OrderToSpecies(Genome genome)
        {
            foreach (var species in Specieses)
            {
                if (species.Check(genome))
                {
                    genome.Species = species;
                    return;
                }
            }
            
            var newSpecies = new Species(genome);
            genome.Species = newSpecies;
                
            Specieses.Add(newSpecies);
        }
    }
}