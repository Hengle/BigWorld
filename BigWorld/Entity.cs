using BigWorld.Map;
using engenious;

namespace BigWorld
{
    public abstract class Entity 
    {
        public Vector2 CmdMoveDirection { get; set; }

        public Vector2 Velocity { get; set; }
        
        public Point CurrentRoom { get; set; }
        public Vector2 RoomPosition { get; set; }

        public virtual void Update(GameTime gameTime)
        {
            
        }
    }
}