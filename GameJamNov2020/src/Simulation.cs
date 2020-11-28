using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Sprites;
using MonoGame.Extended;
using System.Collections.Generic;

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
            Viewport viewport = GraphicsDevice.Viewport;
            viewport.Width = 1600;
            viewport.Height = 900;

            world = new WorldBuilder()
                .AddSystem(new InputSystem())
                .AddSystem(new MovementSystem())
                .AddSystem(new AIInputSystem())
                .AddSystem(new DuplicationSystem())
                .AddSystem(new CollisionSystem())           
                .AddSystem(new DynamicStaticCollisionResolverSystem())
                .AddSystem(new PlayerEnemyCollisionResolverSystem())
                .AddSystem(new PlayerDuplicationPowerCollisionResolverSystem())
                .AddSystem(new DynamicDynamicCollisionResolverSystem())
                .AddSystem(new DoorPlayerCollisionResolverSystem())
                .AddSystem(new CollisionResolverCleanupSystem())
                .AddSystem(new FlagCleanupSystem())
                .AddSystem(new RenderSystem(graphicsDevice))
                .Build();

            // bug workaround because if a system tries to fetch an entity with a component that has never touched the world yet, it crashes
            // with a null exception from the Monogame library instead of returning an empty collection
            // So we create this empty entity with the problematic component and then immediately delete it just so that this component
            // gets to touch the world so it doesn't crash

            Entity emptyEntity = world.CreateEntity();
            emptyEntity.Attach(new NeedsToDuplicate());
            world.DestroyEntity(emptyEntity.Id);

            Texture2D wallTexture = Content.Load<Texture2D>("wall");
            Texture2D doorTexture = Content.Load<Texture2D>("door");
            Texture2D duplicationPowerTexture = Content.Load<Texture2D>("duplicate_power");
            Texture2D playerTexture = Content.Load<Texture2D>("platformChar_idle");
            Texture2D enemyTexture = Content.Load<Texture2D>("platformPack_tile044");

            List<Texture2D> textures = new List<Texture2D>() 
            {
                wallTexture,
                doorTexture,
                duplicationPowerTexture,
                playerTexture,
                enemyTexture
            };

            Utility.MakeLevel2(textures, world);            
        }

        public GameState Update(GameTime gameTime)
        {
            world.Update(gameTime);
            if (Utility.IsLevelComplete)
            {
                Utility.IsLevelComplete = false;
                return GameState.LevelComplete;               
            }
            return GameState.Simulating;
        }

        public void Draw(GameTime gameTime)
        {
            world.Draw(gameTime);
        }
    }
}
