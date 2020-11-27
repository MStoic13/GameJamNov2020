using Microsoft.Xna.Framework;
using MonoGame.Extended.Collections;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameJamNov2020
{
    class PlayerEnemyCollisionResolverSystem : EntityUpdateSystem
    {
        private ComponentMapper<EnemyFlag> enemyFlagMapper;
        private ComponentMapper<Collisions> collisionsMapper;

        public PlayerEnemyCollisionResolverSystem() : base(Aspect.All(typeof(PlayerFlag), typeof(Collisions))) { }

        public override void Initialize(IComponentMapperService mapperService)
        {
            enemyFlagMapper = mapperService.GetMapper<EnemyFlag>();
            collisionsMapper = mapperService.GetMapper<Collisions>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (int entityId in ActiveEntities)
            {
                bool collidedWithEnemy = false;
                Collisions collisions = collisionsMapper.Get(entityId);
                Bag<Collision> newCollisions = new Bag<Collision>();
                foreach (Collision collision in collisions.CollisionBag)
                {
                    if (enemyFlagMapper.Has(collision.OtherEntityId))
                    {
                        collidedWithEnemy = true;
                        DestroyEntity(collision.OtherEntityId);
                    }
                    else
                    {
                        newCollisions.Add(collision);
                    }
                }
                collisions.CollisionBag = newCollisions;
                if (collidedWithEnemy)
                {
                    DestroyEntity(entityId);
                }
            }
        }
    }
}
