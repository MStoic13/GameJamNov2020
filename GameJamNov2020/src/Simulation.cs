using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Entities;

namespace GameJamNov2020
{
    class Simulation
    {
        private World world;
        private ContentManager Content;
        private GraphicsDevice GraphicsDevice;
        public Simulation(int level, ContentManager content, GraphicsDevice graphicsDevice)
        {
            Content = content;

            world = new WorldBuilder()
                .AddSystem(new RenderSystem(graphicsDevice))
                .Build();
        }

        public GameState Update(GameTime gameTime)
        {
            world.Update(gameTime);
            return GameState.Simulating;
        }

        public void Draw(GameTime gameTime)
        {
            world.Draw(gameTime);
        }
    }
}
