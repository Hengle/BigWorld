using BigWorld.Map;
using engenious;
using engenious.Input;

namespace BigWorldGame.Components
{
    public class SimulationComponent : GameComponent
    {
        public new MainGame Game;
        
        public World CurrentWorld { get; private set; }
        
        public Point CurrentRoomCoordinate { get; private set; }
        public Room CurrentRoom { get; private set; }
        
        
        private readonly Trigger<bool> upTrigger = new Trigger<bool>();
        private readonly Trigger<bool> downTrigger = new Trigger<bool>();
        private readonly Trigger<bool> leftTrigger = new Trigger<bool>();
        private readonly Trigger<bool> rightTrigger = new Trigger<bool>();
        
        public SimulationComponent(MainGame game) : base(game)
        {
            Game = game;
            CurrentWorld = new World();
        }
        
        public override void Update(GameTime gameTime)
        {
            var keyState = Keyboard.GetState();

            if (upTrigger.IsChanged(keyState.IsKeyDown(Keys.W),k => k))
            {
                CurrentRoomCoordinate = new Point(CurrentRoomCoordinate.X,CurrentRoomCoordinate.Y-1);
            }
            if (downTrigger.IsChanged(keyState.IsKeyDown(Keys.S),k => k))
            {
                CurrentRoomCoordinate = new Point(CurrentRoomCoordinate.X,CurrentRoomCoordinate.Y+1);
            }
            
            if (leftTrigger.IsChanged(keyState.IsKeyDown(Keys.A),k => k))
            {
                CurrentRoomCoordinate = new Point(CurrentRoomCoordinate.X-1,CurrentRoomCoordinate.Y);
            }
            
            if (rightTrigger.IsChanged(keyState.IsKeyDown(Keys.D),k => k))
            {
                CurrentRoomCoordinate = new Point(CurrentRoomCoordinate.X+1,CurrentRoomCoordinate.Y);
            }

            Room currentRoom;
            
            if (!CurrentWorld.TryGetRoom(CurrentRoomCoordinate,out currentRoom))
            {
                currentRoom = null;
            }

            CurrentRoom = currentRoom;
        }
    }
}