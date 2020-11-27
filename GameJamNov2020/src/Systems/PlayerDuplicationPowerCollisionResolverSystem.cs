using Microsoft.Xna.Framework;
using MonoGame.Extended.Collections;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameJamNov2020
{
    class PlayerDuplicationPowerCollisionResolverSystem : EntityUpdateSystem
    {
        private ComponentMapper<Collisions> collisionsMapper;
        private ComponentMapper<DuplicationPowerFlag> duplicationPowerFlagMapper;

        public PlayerDuplicationPowerCollisionResolverSystem() : base(Aspect.All(typeof(PlayerFlag), typeof(Collisions)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            collisionsMapper = mapperService.GetMapper<Collisions>();
            duplicationPowerFlagMapper = mapperService.GetMapper<DuplicationPowerFlag>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach(int entityId in ActiveEntities)
            {
                Collisions playerCollisions = collisionsMapper.Get(entityId);
                Bag<Collision> newCollisions = new Bag<Collision>();
                foreach (Collision collision in playerCollisions.CollisionBag)
                {
                    if (duplicationPowerFlagMapper.Has(collision.OtherEntityId))
                    {
                        // destroy the power up entity since it has been consumed
                        DestroyEntity(collision.OtherEntityId);

                        // give the consuming player the flag that it needs to duplicate
                        Entity playerEntity = GetEntity(entityId);
                        playerEntity.Attach(new NeedsToDuplicate());
                    }
                    else
                    {
                        newCollisions.Add(collision);
                    }
                }

                playerCollisions.CollisionBag = newCollisions;
            }
        }
    }
}
