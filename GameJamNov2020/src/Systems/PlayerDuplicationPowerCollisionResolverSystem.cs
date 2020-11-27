using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameJamNov2020
{
    class PlayerDuplicationPowerCollisionResolverSystem : EntityUpdateSystem
    {
        //private ComponentMapper<Collisions> collisionsMapper;

        public PlayerDuplicationPowerCollisionResolverSystem() : base(Aspect.All(typeof(PlayerFlag), typeof(Collisions)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            //collisionsMapper = mapperService.GetMapper<Collisions>();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
