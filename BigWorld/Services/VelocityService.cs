using BigWorld.Map;
using engenious;

namespace BigWorld.Services
{
    public class VelocityService : BaseService
    {
        public override void Update(Entity entity, World world, GameTime gameTime)
        {
            entity.Velocity = entity.CmdMoveDirection  * 4;
        }
    }
}