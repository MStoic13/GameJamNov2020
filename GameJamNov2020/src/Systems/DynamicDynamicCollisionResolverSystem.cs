using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Collections;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameJamNov2020
{
    class DynamicDynamicCollisionResolverSystem : EntityUpdateSystem
    {
        private ComponentMapper<Transform2> transformMapper;
        private ComponentMapper<Collisions> collisionsMapper;
        private ComponentMapper<DynamicObject> dynamicObjectMapper;
        private ComponentMapper<DynamicCollidedWithStatic> flagMapper;

        public DynamicDynamicCollisionResolverSystem() : base(Aspect.All(typeof(DynamicObject), typeof(Transform2), typeof(Collisions)).Exclude(typeof(DynamicCollidedWithStatic))) { }

        public override void Initialize(IComponentMapperService mapperService)
        {
            transformMapper = mapperService.GetMapper<Transform2>();
            collisionsMapper = mapperService.GetMapper<Collisions>();
            dynamicObjectMapper = mapperService.GetMapper<DynamicObject>();
            flagMapper = mapperService.GetMapper<DynamicCollidedWithStatic>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach(int entityId in ActiveEntities)
            {
                if (flagMapper.Has(entityId))
                {
                    // seems like .exlude doesn't always work
                    continue;
                }
                Vector2 displacement = Vector2.Zero;
                Collisions collisions = collisionsMapper.Get(entityId);
                Bag<Collision> newCollisions = new Bag<Collision>();

                foreach(Collision collision in collisions.CollisionBag)
                {
                    if (dynamicObjectMapper.Has(collision.OtherEntityId))
                    {
                        if (MathF.Abs(collision.Penetration.X) > MathF.Abs(displacement.X) ||
                            MathF.Abs(collision.Penetration.Y) > MathF.Abs(displacement.Y))
                        {
                            displacement -= collision.Penetration;
                        }
                        Collisions otherCollisions = collisionsMapper.Get(collision.OtherEntityId);
                        if (otherCollisions != null)
                        {
                            Bag<Collision> otherNewCollisions = new Bag<Collision>();
                            // Sloppiliy remove collisions with this object
                            foreach (Collision otherCollision in otherCollisions.CollisionBag)
                            {
                                if (otherCollision.OtherEntityId != entityId)
                                {
                                    otherNewCollisions.Add(otherCollision);
                                }
                            }
                            otherCollisions.CollisionBag = otherNewCollisions;
                        }
                    }
                    else
                    {
                        newCollisions.Add(collision);
                    }
                }
                transformMapper.Get(entityId).Position += displacement;
                collisions.CollisionBag = newCollisions;
            }
        }
    }
}
