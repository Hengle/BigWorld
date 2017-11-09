using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using BigWorld.Entities;
using BigWorld.Entities.Components;
using BigWorld.Entities.Components.AI;
using BigWorld.Entities.Components.AI.Gens;
using BigWorld.Map;
using engenious;

namespace BigWorld.Services.AI
{
    public class NeuronalSimulationService : BaseServiceR2<NeuronalNetworkComponent,InputComponent>
    {
        public int Run { get; private set; }
        public int Count { get; private set; }
        public Genome CurrentGenome { get; private set; }
        public double MaxFitness { get; private set; }
        
        List<Tuple<Genome,double>> positiveGenomes = new List<Tuple<Genome, double>>();

        private List<Genome> genomes = new List<Genome>();
        public int CurrentGenomeNumber { get; private set; }
        private int? oldGenomeNumber;

        private double currentSeconds = 0;
        private double currentTimeLimit = 0;
        private bool init = true;
        
        private Random r = new Random();
        
        public readonly List<Species> Specieses = new List<Species>();
        
        protected override void Update(NeuronalNetworkComponent comp1, InputComponent comp2,
              Entity entity, WorldMap worldMap, GameTime gameTime)
        {
            if (!comp1.Enable)
                return;

            currentSeconds += gameTime.ElapsedGameTime.TotalSeconds;

            if (currentSeconds > currentTimeLimit || init)
            {
                init = false;

                if (oldGenomeNumber.HasValue)
                {
                    if (entity.TryGetComponent<FitnessComponent>(out var fitnessComp))
                    {
                        if (fitnessComp.Value > 0)
                        {
                            positiveGenomes.Add(new Tuple<Genome, double>(genomes[oldGenomeNumber.Value],fitnessComp.Value/currentTimeLimit));
                        }
                        fitnessComp.Reset();
                    }
                }
                
                if (CurrentGenomeNumber == 0)
                {
                    Run++;

                    genomes.Clear();
                    
                    if (positiveGenomes.Count >0)
                    {
                        var goodGenomes = positiveGenomes.OrderByDescending(i => i.Item2).Take(64);

                        var fitness = goodGenomes.First().Item2;
                        if (fitness > MaxFitness)
                            MaxFitness = fitness;
                        
                        foreach (var genome in goodGenomes)
                        {
                            var newGenome = genome.Item1.CreateNewGenation();
                            newGenome.Mutate();
                            
                            OrderToSpecies(newGenome);
                            
                            genomes.Add(newGenome);
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
                                    genomes.Add(childGenome);
                                }
                                else
                                {
                                    
                                }
                                

                            }
                        }
                        
                        positiveGenomes.Clear();
                    }
                    
                    

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
                        
                        Count++;
                    }
                    
                }
                
                var currentGenom = genomes[CurrentGenomeNumber];
                CurrentGenome = currentGenom;
                
                comp1.Reset(currentGenom);
                currentTimeLimit = 10; //(Math.Pow(10,currentGenom.Generation / 10)) * 0.1;

                if (entity.TryGetComponent<PositionComponent>(out var position))
                {
                    position.Reset();
                }

                oldGenomeNumber = CurrentGenomeNumber;
                CurrentGenomeNumber = (CurrentGenomeNumber + 1) % genomes.Count;
                currentSeconds = 0;
            }

            //TickNeuron
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if(comp1.Tick.Value == 0)
                comp1.Tick.SetValue(1);
            
            comp1.Const.SetValue(1);
            
            comp1.Tick.SetValue(-1* comp1.Tick.Value);
            
            //Update
            comp1.NeuronList.Update();
            
            comp2.MoveDirection = new Vector2((float)(comp1.MoveRight.Value - comp1.MoveLeft.Value)
                                              ,(float)(comp1.MoveUp.Value - comp1.MoveDown.Value ));       
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