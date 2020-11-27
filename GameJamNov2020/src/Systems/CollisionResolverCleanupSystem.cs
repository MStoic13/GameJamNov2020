using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameJamNov2020
{
    class CollisionResolverCleanupSystem : EntityUpdateSystem
    {
        private ComponentMapper<Collisions> collisionsMapper;

        public CollisionResolverCleanupSystem() : base(Aspect.All(typeof(Collisions))) { }

        public override void Initialize(IComponentMapperService mapperService)
        {
            collisionsMapper = mapperService.GetMapper<Collisions>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach(int entityId in ActiveEntities)
            {
                collisionsMapper.Get(entityId).CollisionBag.Clear();
            }
        }
    }
}
