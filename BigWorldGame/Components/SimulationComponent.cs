using System;
using BigWorld;
using BigWorld.Map;
using engenious;
using engenious.Input;

namespace BigWorldGame.Components
{
    public class SimulationComponent : GameComponent
    {
        public new MainGame Game;
        
        public WorldMap BuildWorldMap { get; private set; }

        
        
        public readonly Simulation Simulation = new Simulation();
        public WorldMap CurrentWorldMap => Simulation.CurrentWorldMap;
        
        
        public Point CurrentRoomCoordinate { get; private set; }
        public Room CurrentRoom { get; private set; }
        

        public Player Player { get; private set; }
        
        
        private readonly Trigger<bool> upTrigger = new Trigger<bool>();
        private readonly Trigger<bool> downTrigger = new Trigger<bool>();
        private readonly Trigger<bool> leftTrigger = new Trigger<bool>();
        private readonly Trigger<bool> rightTrigger = new Trigger<bool>();
        
        public SimulationComponent(MainGame game) : base(game)
        {
            Game = game;
            BuildWorldMap = new WorldMap();
        }

        public void Reset(GameState state)
        {
            if (state == GameState.Build || state == GameState.Debug)
            {
                Simulation.Stop();
            }
            else if (state == GameState.Running)
            {
                Simulation.Start(BuildWorldMap);
                Player = Simulation.AddPlayer();
            }
        }
        
        public override void Update(GameTime gameTime)
        {
            var keyState = Keyboard.GetState();

            if (Game.CurrentGameState == GameState.Build || Game.CurrentGameState == GameState.Debug )
            {
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
            }
            else
            {
                Vector2 direction = Vector2.Zero;

                if (keyState.IsKeyDown(Keys.A))
                    direction += new Vector2(-1,0);
                
                if (keyState.IsKeyDown(Keys.D))
                    direction += new Vector2(1,0);
                
                if (keyState.IsKeyDown(Keys.W))
                    direction += new Vector2(0,-1);
                
                if (keyState.IsKeyDown(Keys.S))
                    direction += new Vector2(0,1);

                Player.CmdMoveDirection = direction;
            }
            
            
            

            Room currentRoom;
            
            if (!BuildWorldMap.TryGetRoom(CurrentRoomCoordinate,out currentRoom))
            {
                currentRoom = null;
            }

            CurrentRoom = currentRoom;

            if (Simulation.IsRunning)
            {
                Simulation.Update(gameTime);
            }
        }

        public void SetWorld(WorldMap loadWorld)
        {
            BuildWorldMap = loadWorld;
        }
    }
}