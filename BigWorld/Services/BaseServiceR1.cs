using BigWorld.Entities;
using BigWorld.Map;
using engenious;

namespace BigWorld.Services
{
    public abstract class BaseServiceR1<TC1> : BaseService
        where TC1 : Component
    {
        public override void Update(Entity entity, WorldMap worldMap, GameTime gameTime)
        {
            TC1 comp;
            if (entity.TryGetComponent(out comp))
            {
                Update(comp,entity,worldMap,gameTime);
            }
        }

        protected abstract void Update(TC1 comp,Entity entity, WorldMap worldMap, GameTime gameTime);
    }
}