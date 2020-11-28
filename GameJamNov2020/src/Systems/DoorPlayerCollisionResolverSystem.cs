using Microsoft.Xna.Framework;
using MonoGame.Extended.Collections;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameJamNov2020
{
    class DoorPlayerCollisionResolverSystem : EntityUpdateSystem
    {
        private ComponentMapper<Collisions> collisionsMapper;
        private ComponentMapper<PlayerFlag> playerFlagMapper;

        public DoorPlayerCollisionResolverSystem() : base(Aspect.All(typeof(DoorFlag), typeof(Collisions))) { }

        public override void Initialize(IComponentMapperService mapperService)
        {
            collisionsMapper = mapperService.GetMapper<Collisions>();
            playerFlagMapper = mapperService.GetMapper<PlayerFlag>();
        }

        public override void Update(GameTime gameTime)
        {
            bool levelComplete = true;
            foreach(int entityId in ActiveEntities)
            {
                bool doorHasCollidedWithPlayer = false;
                Collisions collisions = collisionsMapper.Get(entityId);
                Bag<Collision> newCollisions = new Bag<Collision>();
                foreach(Collision collision in collisions.CollisionBag)
                {
                    if (playerFlagMapper.Has(collision.OtherEntityId))
                    {
                        doorHasCollidedWithPlayer = true;
                    }
                    else
                    {
                        newCollisions.Add(collision);
                    }
                }
                if (!doorHasCollidedWithPlayer)
                {
                    levelComplete = false;
                }
                collisions.CollisionBag = newCollisions;
            }
            if (levelComplete)
            {
                Utility.IsLevelComplete = true;
            }
        }
    }
}
