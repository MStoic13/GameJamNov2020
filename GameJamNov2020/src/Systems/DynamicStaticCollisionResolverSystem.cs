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
    class DynamicStaticCollisionResolverSystem : EntityUpdateSystem
    {
        private ComponentMapper<Transform2> transformMapper;
        private ComponentMapper<Collisions> collisionsMapper;
        private ComponentMapper<StaticObject> staticObjectMapper;

        public DynamicStaticCollisionResolverSystem() : base(Aspect.All(typeof(DynamicObject), typeof(Transform2), typeof(Collisions))) { }

        public override void Initialize(IComponentMapperService mapperService)
        {
            transformMapper = mapperService.GetMapper<Transform2>();
            collisionsMapper = mapperService.GetMapper<Collisions>();
            staticObjectMapper = mapperService.GetMapper<StaticObject>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach(int entityId in ActiveEntities)
            {
                Vector2 displacement = Vector2.Zero;
                Collisions collisions = collisionsMapper.Get(entityId);
                Bag<Collision> newCollisions = new Bag<Collision>();
                bool hasCollidedWithStatic = false;
                foreach(Collision collision in collisions.CollisionBag)
                {
                    if (staticObjectMapper.Has(collision.OtherEntityId))
                    {
                        hasCollidedWithStatic = true;
                        // Simple Collision fixing as everything is a rectangle
                        if (MathF.Abs(collision.Penetration.X) > MathF.Abs(displacement.X) || 
                            MathF.Abs(collision.Penetration.Y) > MathF.Abs(displacement.Y))
                        {
                            displacement -= collision.Penetration;
                        }
                    }
                    else
                    {
                        newCollisions.Add(collision);
                    }
                }
                transformMapper.Get(entityId).Position += displacement;
                collisions.CollisionBag = newCollisions;
                if (hasCollidedWithStatic)
                {
                    Entity dynamicEntity = GetEntity(entityId);
                    dynamicEntity.Attach(new DynamicCollidedWithStatic());
                }
            }
        }
    }
}
