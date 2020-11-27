using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameJamNov2020
{
    class Collision
    {
        public int OtherEntityId { get; set; }
        public Vector2 Penetration { get; set; }

        public Collision(int otherEntityId, Vector2 penetration)
        {
            OtherEntityId = otherEntityId;
            Penetration = penetration;
        }
    }
}
