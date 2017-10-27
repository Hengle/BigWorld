using BigWorld.Map;
using engenious;

namespace BigWorld.Services
{
    public class VelocityService : BaseService
    {
        public override void Update(Entity entity, WorldMap worldMap, GameTime gameTime)
        {
            entity.Velocity = entity.CmdMoveDirection  * 4;
        }
    }
}