using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Input;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameJamNov2020
{
    class DuplicationSystem : EntityUpdateSystem
    {
        public DuplicationSystem() : base(Aspect.All(typeof(PlayerFlag)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
        }

        public override void Update(GameTime gameTime)
        {
            if ()
            {
                int lastActiveEntityIndex = ActiveEntities.Count - 1;
                Entity lastExistingPlayer = GetEntity(ActiveEntities[lastActiveEntityIndex]);

                Entity duplicatePlayer = CreateEntity();
                duplicatePlayer.Attach(lastExistingPlayer.Get<Sprite>());
                duplicatePlayer.Attach(lastExistingPlayer.Get<PlayerFlag>());
                duplicatePlayer.Attach(lastExistingPlayer.Get<MovementDirection>());
                Vector2 lastExistingPlayerPosition = lastExistingPlayer.Get<Transform2>().Position;
                duplicatePlayer.Attach(new Transform2(new Vector2(lastExistingPlayerPosition.X + 70, lastExistingPlayerPosition.Y)));
            }
        }
    }
}
