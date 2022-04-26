using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.Golgrath.Objects
{
    public interface Moveable
    {
        //An interface, which should only be given to objects who are meant to move throughout the scene.
        //This interface is there to enforce the creation of the physics parameters.
        Vec2 Velocity { get; set; }
        Vec2 Gravity { get; set; }
        Vec2 OldPosition { get; }
        void Step();
    }
}
