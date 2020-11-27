using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Sprites;
using MonoGame.Extended;

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
            GraphicsDevice = graphicsDevice;

            world = new WorldBuilder()
                .AddSystem(new InputSystem())
                .AddSystem(new MovementSystem())
                .AddSystem(new AIInputSystem())
                .AddSystem(new RenderSystem(graphicsDevice))
                .Build();

            Texture2D playerTexture = Content.Load<Texture2D>("platformChar_idle");
            Entity playerEntity = world.CreateEntity();
            playerEntity.Attach(new Sprite(playerTexture));
            playerEntity.Attach(new Transform2(new Vector2(100f, 100f)));
            playerEntity.Attach(new PlayerFlag());
            playerEntity.Attach(new MovementDirection());

            Texture2D enemyTexture = Content.Load<Texture2D>("platformPack_tile044");
            Entity enemyEntity = world.CreateEntity();
            enemyEntity.Attach(new Sprite(enemyTexture));
            enemyEntity.Attach(new Transform2(new Vector2(400f, 100f)));
            enemyEntity.Attach(new AIPattern());
            enemyEntity.Attach(new MovementDirection());
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
