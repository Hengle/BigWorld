using System.Collections.Generic;
using BigWorld.Entities;
using BigWorld.Map;
using BigWorld.Services;
using BigWorld.Services.AI;
using engenious;

namespace BigWorld
{
    public class Simulation
    {
        public WorldMap CurrentWorldMap { get; private set; }

        public bool IsRunning { get; private set; }

        private readonly List<Entity> entities = new List<Entity>();
        public IEnumerable<Entity> Entities => entities;

        public NeuronalSimulationService NeuronalSimulationService { get; }

        private readonly List<BaseService> services;

        public Simulation()
        {
            NeuronalSimulationService = new NeuronalSimulationService();
            services = new List<BaseService>()
            {
                NeuronalSimulationService,
                new StartAIService(),
                new VelocityService(),
                new BlockCollisionService(),
                new MovementService(),
                new RoomCollisionService(),
                new EndAIService()
            };
        }
        
        public void Start(WorldMap worldMap)
        {   
            CurrentWorldMap = worldMap ;
            entities.Clear();
            
            
            IsRunning = true;
        }

        public Player AddPlayer()
        {
            Player player = new Player();
            
            entities.Add(player);

            return player;
        }

        public void Stop()
        {
            IsRunning = false;
        }

        public void Update(GameTime gameTime)
        {
            if(!IsRunning)
                return;

            foreach (var entity in entities)
            {
                entity.Update(gameTime);
                foreach (var service in services)
                {
                    service.Update(entity,CurrentWorldMap,gameTime);
                }
            }
        }
    }
}