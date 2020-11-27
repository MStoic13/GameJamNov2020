using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameJamNov2020
{
    class CollisionSystem : EntityUpdateSystem
    {
        private ComponentMapper<Transform2> transformMapper;
        private ComponentMapper<Collider> colliderMapper;
        private ComponentMapper<Sprite> spriteMapper;
        private ComponentMapper<Collisions> collisionsMapper;

        public CollisionSystem() : base(Aspect.All(typeof(Collider), typeof(Transform2)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            transformMapper = mapperService.GetMapper<Transform2>();
            colliderMapper = mapperService.GetMapper<Collider>();
            spriteMapper = mapperService.GetMapper<Sprite>();
            collisionsMapper = mapperService.GetMapper<Collisions>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (int entityIdA in ActiveEntities)
            {
                foreach (int entityIdB in ActiveEntities)
                {
                    if (entityIdA == entityIdB)
                    {
                        continue;
                    }
                    RectangleF entityRectA = getRect(entityIdA);
                    RectangleF entityRectB = getRect(entityIdB);

                    if (entityRectA.Intersects(entityRectB))
                    {
                        Collision collision = new Collision(entityIdB, getPenetration(entityRectA, entityRectB));
                        Collisions collisions = collisionsMapper.Get(entityIdA);
                        if (collisions != null)
                        {
                            collisions.CollisionBag.Add(collision);
                        }
                    }
                }
            }
        }

        private RectangleF getRect(int entityId)
        {
            Transform2 transform = transformMapper.Get(entityId);
            Sprite sprite = spriteMapper.Get(entityId);
            if (sprite != null)
            {
                return sprite.GetBoundingRectangle(transform);
            }
            Collider collider = colliderMapper.Get(entityId);
            return new RectangleF(transform.Position.X - (collider.Hitbox.Width / 2f), transform.Position.Y - (collider.Hitbox.Height / 2f), collider.Hitbox.Width, collider.Hitbox.Height);
        }

        private Vector2 getPenetration(RectangleF A, RectangleF B)
        {
            RectangleF intersection = A.Intersection(B);
            if (intersection.Width < intersection.Height)
            {
                if (A.Center.X < B.Center.X)
                {
                    return new Vector2(intersection.Width, 0);
                } 
                else
                {
                    return new Vector2(-intersection.Width, 0);
                }
            }
            else
            {
                if (A.Center.Y < B.Center.Y)
                {
                    return new Vector2(0, intersection.Height);
                }
                else
                {
                    return new Vector2(0, -intersection.Height);
                }
            }
        }
    }
}
