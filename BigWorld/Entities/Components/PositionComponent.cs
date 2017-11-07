using engenious;

namespace BigWorld.Entities.Components
{
    public class PositionComponent : Component
    {
        public Point CurrentRoom { get; set; }
        public Vector2 RoomPosition { get; set; }

        public void Reset()
        {
            CurrentRoom = Point.Zero;
            RoomPosition = Vector2.Zero;
        }
    }
}