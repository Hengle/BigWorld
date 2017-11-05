using BigWorld.Entities;
using BigWorld.Map;
using engenious;

namespace BigWorld.Services
{
    public abstract class BaseServiceR2<TC1,TC2> : BaseService
        where TC1 : Component
        where TC2 : Component
    {
        public override void Update(Entity entity, WorldMap worldMap, GameTime gameTime)
        {
            TC1 comp1;
            TC2 comp2;

            if (entity.TryGetComponent(out comp1) &&
                entity.TryGetComponent(out comp2))
            {
                Update(comp1,comp2, entity,worldMap,gameTime);
            }
        }

        protected abstract void Update(TC1 comp1, TC2 comp2,Entity entity, WorldMap worldMap, GameTime gameTime);
    }
}