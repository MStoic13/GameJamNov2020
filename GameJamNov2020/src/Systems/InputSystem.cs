using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameJamNov2020
{
    class InputSystem : EntityUpdateSystem
    {
        private ComponentMapper<MovementDirection> movementDirectionMapper;

        public InputSystem() : base(Aspect.All(typeof(PlayerFlag), typeof(MovementDirection)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            movementDirectionMapper = mapperService.GetMapper<MovementDirection>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (int entityId in ActiveEntities)
            {
                MovementDirection movementDirection = movementDirectionMapper.Get(entityId);
                KeyboardStateExtended inputState = KeyboardExtended.GetState();
                movementDirection.Direction = Vector2.Zero;
                movementDirection.Speed = 100;
                if (inputState.IsKeyDown(Keys.W))
                {
                    movementDirection.Direction = new Vector2(0, -1);
                }
                else if (inputState.IsKeyDown(Keys.S))
                {
                    movementDirection.Direction = new Vector2(0, 1);
                }
                else if (inputState.IsKeyDown(Keys.A))
                {
                    movementDirection.Direction = new Vector2(-1, 0);
                }
                else if (inputState.IsKeyDown(Keys.D))
                {
                    movementDirection.Direction = new Vector2(1, 0);
                }
            }
        }
    }
}
