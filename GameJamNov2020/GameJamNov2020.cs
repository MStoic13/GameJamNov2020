using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameJamNov2020
{
    public class GameJamNov2020 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Simulation simulation;
        private int level = 0;
        private GameState state = GameState.LevelComplete;

        public GameJamNov2020()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();        
            switch(state)
            {
                case GameState.LevelComplete:
                    level++;
                    if (level > 1)
                    {
                        Exit();
                    }
                    reset();
                    break;
                case GameState.Reset:
                    reset();
                    break;
                case GameState.Simulating:
                    state = simulation.Update(gameTime);
                    break;
            }
            base.Update(gameTime);
        }

        private void reset()
        {
            simulation = new Simulation(level, Content, GraphicsDevice);
            state = GameState.Simulating;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            switch(state)
            {
                case GameState.Simulating:
                    simulation.Draw(gameTime);
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
