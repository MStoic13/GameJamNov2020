using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace GameJamNov2020
{
    class RenderSystem : EntityDrawSystem
    {
        private GraphicsDevice graphicsDevice;
        private SpriteBatch spriteBatch;

        private ComponentMapper<Transform> transformMapper;

        public RenderSystem(GraphicsDevice graphicsDevice) : base(Aspect.All(typeof(Transform)))
        {
            this.graphicsDevice = graphicsDevice;
            spriteBatch = new SpriteBatch(graphicsDevice);
        }
        public override void Initialize(IComponentMapperService mapperService)
        {
            transformMapper = mapperService.GetMapper<Transform>();
        }
        public override void Draw(GameTime gameTime)
        {
            // TODO: Render
        }
    }
}
