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
                .AddSystem(new DuplicationSystem())
                .AddSystem(new CollisionSystem())           
                .AddSystem(new DynamicStaticCollisionResolverSystem())
                .AddSystem(new PlayerEnemyCollisionResolverSystem())
                .AddSystem(new PlayerDuplicationPowerCollisionResolverSystem())
                .AddSystem(new CollisionResolverCleanupSystem())
                .AddSystem(new RenderSystem(graphicsDevice))
                .Build();

            Texture2D playerTexture = Content.Load<Texture2D>("platformChar_idle");
            Entity playerEntity = world.CreateEntity();
            playerEntity.Attach(new Sprite(playerTexture));
            playerEntity.Attach(new Transform2(new Vector2(100f, 100f)));
            playerEntity.Attach(new PlayerFlag());
            playerEntity.Attach(new MovementDirection());
            playerEntity.Attach(new Collider());
            playerEntity.Attach(new Collisions());
            playerEntity.Attach(new DynamicObject());

            Texture2D enemyTexture = Content.Load<Texture2D>("platformPack_tile044");
            Entity enemyEntity = world.CreateEntity();
            enemyEntity.Attach(new Sprite(enemyTexture));
            enemyEntity.Attach(new Transform2(new Vector2(400f, 100f)));
            enemyEntity.Attach(new AIPattern());
            enemyEntity.Attach(new MovementDirection());
            enemyEntity.Attach(new Collider());
            enemyEntity.Attach(new EnemyFlag());

            Texture2D duplicationPowerTexture = Content.Load<Texture2D>("duplicate_power");
            Entity duplicationPowerEntity = world.CreateEntity();
            duplicationPowerEntity.Attach(new Sprite(duplicationPowerTexture));
            duplicationPowerEntity.Attach(new Transform2(new Vector2(200f, 300f)));
            duplicationPowerEntity.Attach(new DuplicationPowerFlag());
            duplicationPowerEntity.Attach(new Collider());

            // bug workaround because if a system tries to fetch an entity with a component that has never touched the world yet, it crashes
            // with a null exception from the Monogame library instead of returning an empty collection
            // So we create this empty entity with the problematic component and then immediately delete it just so that this component
            // gets to touch the world so it doesn't crash
            Entity emptyEntity = world.CreateEntity();
            emptyEntity.Attach(new NeedsToDuplicate());
            world.DestroyEntity(emptyEntity.Id);
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
