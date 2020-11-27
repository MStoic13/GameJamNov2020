using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameJamNov2020
{
    class FlagCleanupSystem : EntityUpdateSystem
    {
        // Add more flags to clean up at the end of each loop
        public FlagCleanupSystem() : base(Aspect.One(typeof(DynamicCollidedWithStatic))) { }

        public override void Initialize(IComponentMapperService mapperService)
        {
        }

        public override void Update(GameTime gameTime)
        {
            foreach(int entityId in ActiveEntities)
            {
                Entity entity = GetEntity(entityId);
                entity.Detach<DynamicCollidedWithStatic>();
            }
        }
    }
}
