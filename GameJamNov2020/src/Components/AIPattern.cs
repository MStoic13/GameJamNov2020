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
                    Direction = new Vector2(1, 0),
                    Speed = 100f
                },
                new MovementDirection()
                {
                    Direction = new Vector2(-1, 0),
                    Speed = 100f
                }
            };
        }

        public AIPattern(IEnumerable<MovementDirection> pattern)
        {
            Pattern = pattern.ToArray();
        }
    }
}
