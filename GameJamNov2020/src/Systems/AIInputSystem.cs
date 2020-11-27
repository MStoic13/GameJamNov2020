using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameJamNov2020
{
    class AIInputSystem : EntityUpdateSystem
    {
        private ComponentMapper<MovementDirection> movementDirectionMapper;
        private ComponentMapper<AIPattern> aiPatternMapper;

        public AIInputSystem() : base(Aspect.All(typeof(AIPattern), typeof(MovementDirection)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            movementDirectionMapper = mapperService.GetMapper<MovementDirection>();
            aiPatternMapper = mapperService.GetMapper<AIPattern>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (int entityId in ActiveEntities)
            {
                MovementDirection movementDirection = movementDirectionMapper.Get(entityId);
                AIPattern aiPattern = aiPatternMapper.Get(entityId);
                int index = (int)(gameTime.TotalGameTime.TotalSeconds / 3) % 2;
                movementDirection.Direction = aiPattern.Pattern[index].Direction;
                movementDirection.Speed = aiPattern.Pattern[index].Speed;
            }
        }
    }
}
