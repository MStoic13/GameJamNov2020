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
        private ComponentMapper<Sprite> spriteMapper;
        private ComponentMapper<Transform2> transformMapper;

        public DuplicationSystem() : base(Aspect.All(typeof(NeedsToDuplicate), typeof(PlayerFlag), typeof(Sprite)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            spriteMapper = mapperService.GetMapper<Sprite>();
            transformMapper = mapperService.GetMapper<Transform2>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach(int entityId in ActiveEntities)
            {
                Entity currentEntity = GetEntity(entityId);
                currentEntity.Detach<NeedsToDuplicate>();

                Entity duplicatePlayer = CreateEntity();
                duplicatePlayer.Attach(spriteMapper.Get(entityId));
                duplicatePlayer.Attach(new PlayerFlag());
                duplicatePlayer.Attach(new MovementDirection());
                Transform2 transform = transformMapper.Get(entityId);
                duplicatePlayer.Attach(new Transform2(new Vector2(transform.Position.X + 70, transform.Position.Y)));
                duplicatePlayer.Attach(new Collider());
                duplicatePlayer.Attach(new Collisions());
                duplicatePlayer.Attach(new DynamicObject());
            }
        }
    }
}
