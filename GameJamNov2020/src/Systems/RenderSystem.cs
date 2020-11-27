using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;
using MonoGame.Extended;

namespace GameJamNov2020
{
    class RenderSystem : EntityDrawSystem
    {
        private GraphicsDevice graphicsDevice;
        private SpriteBatch spriteBatch;

        private ComponentMapper<Transform2> transformMapper;
        private ComponentMapper<Sprite> spriteMapper;

        public RenderSystem(GraphicsDevice graphicsDevice) : base(Aspect.All(typeof(Transform2), typeof(Sprite)))
        {
            this.graphicsDevice = graphicsDevice;
            spriteBatch = new SpriteBatch(graphicsDevice);
        }
        public override void Initialize(IComponentMapperService mapperService)
        {
            transformMapper = mapperService.GetMapper<Transform2>();
            spriteMapper = mapperService.GetMapper<Sprite>();
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            foreach(int entityId in ActiveEntities)
            {
                Transform2 transform = transformMapper.Get(entityId);
                Sprite sprite = spriteMapper.Get(entityId);

                SpriteExtensions.Draw(spriteBatch, sprite, transform);
            }
            spriteBatch.End();
        }
    }
}
