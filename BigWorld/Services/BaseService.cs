using BigWorld.Map;
using engenious;

namespace BigWorld.Services
{
    public abstract class BaseService
    {
        public abstract void Update(Entity entity,World world, GameTime gameTime);
    }
}