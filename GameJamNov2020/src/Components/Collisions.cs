using MonoGame.Extended.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameJamNov2020
{
    class Collisions
    {
        public Bag<Collision> CollisionBag { get; set; }
        
        public Collisions()
        {
            CollisionBag = new Bag<Collision>();
        }
    }
}
