using GXPEngine.PhysicsEngine.Colliders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.Golgrath.Objects
{
    public class Cap : Circle
    {

        public Cap(int radius, Vec2 position): base(radius, position, radius * 2 + 1, radius * 2 + 1)
        {
            
        }
    }
}
