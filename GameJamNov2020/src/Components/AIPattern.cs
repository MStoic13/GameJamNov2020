using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameJamNov2020
{
    class AIPattern
    {
        public MovementDirection[] Pattern { get; set; }

        public AIPattern()
        {
            Pattern = new MovementDirection[]
            {
                new MovementDirection()
                {
                    Direction = new Vector2(0, 1),
                    Speed = 200f
                },
                new MovementDirection()
                {
                    Direction = new Vector2(0, -1),
                    Speed = 200f
                }
            };
        }

        public AIPattern(IEnumerable<MovementDirection> pattern)
        {
            Pattern = pattern.ToArray();
        }
    }
}
