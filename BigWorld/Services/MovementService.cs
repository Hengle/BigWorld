using BigWorld.Map;
using engenious;

namespace BigWorld.Services
{
    public class MovementService : BaseService
    {
        public override void Update(Entity entity, World world, GameTime gameTime)
        {       
            entity.RoomPosition += entity.Velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}