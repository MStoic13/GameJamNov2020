using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace GameJamNov2020
{
    class MovementSystem : EntityUpdateSystem
    {
        private ComponentMapper<Transform2> transformMapper;
        private ComponentMapper<MovementDirection> movementDirectionMapper;

        public MovementSystem() : base(Aspect.All(typeof(MovementDirection), typeof(Transform2)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            transformMapper = mapperService.GetMapper<Transform2>();
            movementDirectionMapper = mapperService.GetMapper<MovementDirection>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach(int entityId in ActiveEntities)
            {
                Transform2 transform = transformMapper.Get(entityId);
                MovementDirection movementDirection = movementDirectionMapper.Get(entityId);
                transform.Position += movementDirection.Direction * (movementDirection.Speed * gameTime.GetElapsedSeconds());
            }
        }
    }
}
