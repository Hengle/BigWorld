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
        
        List<Tuple<Genome,double>> positiveGenomes = new List<Tuple<Genome, double>>();

        private List<Genome> genomes = new List<Genome>();
        public int CurrentGenomeNumber { get; private set; }
        private int? oldGenomeNumber;

        private double currentSeconds = 0;
        private double currentTimeLimit = 0;
        private bool init = true;
        
        private Random r = new Random();
        
        
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
                        var goodGenomes = positiveGenomes.OrderByDescending(i => i.Item2).Take(16);

                        foreach (var genome in goodGenomes)
                        {
                            genomes.Add(genome.Item1.CopyGenome());
                            genomes.Add(genome.Item1.CopyGenome());
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

                                var childGenome = firstGenome.Combine(secoundGenome);
                                genomes.Add(childGenome);
                            }
                        }
                        
                        positiveGenomes.Clear();
                    }
                    
                    

                    for (int i = genomes.Count; i < 128; i++)
                    {
                        var newGenom = new Genome(r.Next());
                        newGenom.Add(new InputGen(comp1.Tick));
                        newGenom.Add(new InputGen(comp1.DeltaPositionX));
                        newGenom.Add(new InputGen(comp1.DeltaPositionY));
                    
                        newGenom.Add(new OutputGen(comp1.MoveUp));
                        newGenom.Add(new OutputGen(comp1.MoveDown));
                        newGenom.Add(new OutputGen(comp1.MoveLeft));
                        newGenom.Add(new OutputGen(comp1.MoveRight));
                        genomes.Add(newGenom);
                        Count++;
                    }
                    
                }
                
                var currentGenom = genomes[CurrentGenomeNumber];
                CurrentGenome = currentGenom;
                
                currentGenom.Mutate();
                
                comp1.Reset(currentGenom);
                currentTimeLimit = ((currentGenom.Generation / 10)+1) * 0.1;

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
            
            comp1.Tick.SetValue(-1* comp1.Tick.Value);
            
            //Update
            comp1.NeuronList.Update();
            
            comp2.MoveDirection = new Vector2((float)(comp1.MoveRight.Value - comp1.MoveLeft.Value)
                                              ,(float)(comp1.MoveUp.Value - comp1.MoveDown.Value ));
        }
    }
}