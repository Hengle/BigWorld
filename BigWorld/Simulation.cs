using System.Collections.Generic;
using BigWorld.Entities;
using BigWorld.Map;
using BigWorld.Services;
using engenious;

namespace BigWorld
{
    public class Simulation
    {
        public WorldMap CurrentWorldMap { get; private set; }

        public bool IsRunning { get; private set; }

        private readonly List<Entity> entities = new List<Entity>();
        public IEnumerable<Entity> Entities => entities;

        private readonly List<BaseService> services = new List<BaseService>()
        {
            new VelocityService(),
            new BlockCollisionService(),
            new MovementService(),
            new RoomCollisionService(),
        };
        
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