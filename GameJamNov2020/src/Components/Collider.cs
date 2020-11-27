using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameJamNov2020
{
    class Collider
    {
        public RectangleF Hitbox { get; set; }

        // Only used for entities without a sprite/transform, or for custom hitboxes
        public Collider(RectangleF hitbox)
        {
            Hitbox = hitbox;
        }

        public Collider()
        {
            Hitbox = new RectangleF();
        }
    }
}
