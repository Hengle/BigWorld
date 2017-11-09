using System;
using System.Collections.Generic;
using BigWorld;
using BigWorld.Map;
using engenious;

namespace AISimulation
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            WorldMap map = WorldMap.LoadWorld();
            
            Simulation simulation = new Simulation();
            simulation.Start(map);

            var player = simulation.AddPlayer();
            
            TimeSpan globalTimeSpan = TimeSpan.Zero;
            TimeSpan eleTimeSpan =  TimeSpan.FromMilliseconds(20);

            int i = 0;
            
            while (i < 10000000)
            {
                globalTimeSpan += eleTimeSpan;
                GameTime gameTime = new GameTime(globalTimeSpan,eleTimeSpan);
                
                simulation.Update(gameTime);


                if (++i % 1000 == 0)
                {
                    Console.WriteLine(globalTimeSpan);
                    Console.WriteLine(simulation.NeuronalSimulationService.MaxFitness);
                }
                
                
            }
            
            
        }
    }
}