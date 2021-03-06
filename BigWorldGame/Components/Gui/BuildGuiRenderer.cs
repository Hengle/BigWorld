﻿using System.Linq.Expressions;
using BigWorld.Map;
using BigWorldGame.Controls;
using engenious;
using engenious.Graphics;
using engenious.Input;

namespace BigWorldGame.Components.Gui
{
    public class BuildGuiRenderer : DrawableGameComponent
    {
        public new readonly MainGame Game;
        
        public Point CurrentRoomCoordinate { get; set; }
        
        private SelectTileSheetControl tileSheetControl;
        
        private Texture2D dungeonSheet;
        private Texture2D pixelTexture;
        private SpriteFont gameFont;
        
        private SpriteBatch batch;
        
        private Point? mouseMapPoint = null;

        private bool isBlockMode = false;
        
        private int currentLayer = 0;
        private readonly Trigger<bool> pageUpTrigger = new Trigger<bool>();
        private readonly Trigger<bool> pageDownTrigger = new Trigger<bool>();

        private readonly Trigger<bool> groundFill = new Trigger<bool>();
        
        private readonly Trigger<bool> upTrigger = new Trigger<bool>();
        private readonly Trigger<bool> downTrigger = new Trigger<bool>();
        private readonly Trigger<bool> leftTrigger = new Trigger<bool>();
        private readonly Trigger<bool> rightTrigger = new Trigger<bool>();
        
        public BuildGuiRenderer(MainGame game) : base(game)
        {
            Game = game;
        }

        public new void LoadContent()
        {
            batch = new SpriteBatch(GraphicsDevice);
            
            dungeonSheet = Game.Content.Load<Texture2D>("Spritesheets/TileSheetDungeon");
            
            pixelTexture = new Texture2D(GraphicsDevice,1,1);
            pixelTexture.SetData(new []{Color.White});

            gameFont = Game.Content.Load<SpriteFont>("Fonts/GameFont");
            
            tileSheetControl= new SelectTileSheetControl();
            tileSheetControl.LoadContent(Game);
            tileSheetControl.SetTileSheet(dungeonSheet);
            tileSheetControl.Position = new Rectangle(GraphicsDevice.Viewport.Width-300,10,300,GraphicsDevice.Viewport.Height);
        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            
            var height = (GraphicsDevice.Viewport.Height - Room.SizeY * RenderSettings.TileSize)/2;
            
            
            mouseMapPoint = new Point((mouseState.X -height)/RenderSettings.TileSize, (mouseState.Y -height)/RenderSettings.TileSize);

            if (mouseMapPoint.Value.X < 0 || mouseMapPoint.Value.Y < 0)
            {
                mouseMapPoint = null;
            }
            else if(mouseMapPoint.Value.X >= Room.SizeX || mouseMapPoint.Value.Y >= Room.SizeY)
            {
                mouseMapPoint = null;
            }

            if (mouseMapPoint.HasValue && mouseState.LeftButton == ButtonState.Pressed)
            {
                var room = Game.BuildWorldMap.LoadOrCreateRoom(CurrentRoomCoordinate);

                if (!isBlockMode)
                {
                    room[currentLayer].SetValue(mouseMapPoint.Value,tileSheetControl.SelectTextureInteger);
                }
                else
                {
                    room.BlockLayer.SetValue(mouseMapPoint.Value,true);
                }
                
                
            }
            else if (mouseMapPoint.HasValue && mouseState.RightButton == ButtonState.Pressed)
            {
                var room = Game.BuildWorldMap.LoadOrCreateRoom(CurrentRoomCoordinate);

                if (!isBlockMode)
                {
                    room[currentLayer].SetValue(mouseMapPoint.Value,null);
                }
                else
                {
                    room.BlockLayer.SetValue(mouseMapPoint.Value,false);
                }
                
            }
            
            
            //Keyboard
            var keyState = Keyboard.GetState();

            if (pageUpTrigger.IsChanged(keyState.IsKeyDown(Keys.PageUp),i => i))
            {
                currentLayer++;
            }
            else if (pageDownTrigger.IsChanged(keyState.IsKeyDown(Keys.PageDown),i => i))
            {
                currentLayer--;
                if (currentLayer < 0)
                {
                    currentLayer = 0;
                }
            }

            isBlockMode = keyState.IsKeyDown(Keys.B);
            if (groundFill.IsChanged((keyState.IsKeyDown(Keys.F)),i => i))
            {
                var room = Game.BuildWorldMap.LoadOrCreateRoom(CurrentRoomCoordinate);
                
                for (int x = 0; x < Room.SizeX; x++)
                {
                    for (int y = 0; y < Room.SizeY; y++)
                    {
                        room[0].SetValue(new Point(x,y), tileSheetControl.SelectTextureInteger);
                    }
                    
                }
            }
            
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
            

            Game.CurrentLayer = currentLayer;
            tileSheetControl.Update();
        }

        

        public override void Draw(GameTime gameTime)
        {
            batch.Begin();
            
            
            var height = (GraphicsDevice.Viewport.Height - Room.SizeY * RenderSettings.TileSize)/2;
            
            Color mouseColor = Color.LightBlue * 0.7f;

            if (isBlockMode)
            {
                mouseColor = Color.Yellow * 0.7f;
            }
            
            if (mouseMapPoint.HasValue)
            {
                batch.Draw(pixelTexture,new Rectangle(mouseMapPoint.Value.X*RenderSettings.TileSize+height,mouseMapPoint.Value.Y*RenderSettings.TileSize+height,RenderSettings.TileSize,RenderSettings.TileSize),mouseColor );
            }
            
            var room = Game.BuildWorldMap.LoadOrCreateRoom(CurrentRoomCoordinate);

            for (int x = 0; x < Room.SizeX; x++)
            {
                for (int y = 0; y < Room.SizeY; y++)
                {
                    var block = room.BlockLayer.GetValue(x, y);
                    if (block.HasValue && block.Value)
                    {
                        batch.Draw(pixelTexture,new Rectangle(x*RenderSettings.TileSize+height,y * RenderSettings.TileSize+height,RenderSettings.TileSize,RenderSettings.TileSize),Color.Yellow * 0.7f ); 
                    }
                }
            }
            
            batch.DrawString(gameFont,$"Layer:{currentLayer}",new Vector2(10,10),Color.White );
            tileSheetControl.Draw();

            batch.End();
        }
    }
}