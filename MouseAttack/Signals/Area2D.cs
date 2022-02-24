using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Signals
{
    public class Area2D : CollisionObject2D
    {
        public static readonly string AreaEntered = "area_entered";
        public static readonly string AreaExited = "area_exited";
        public static readonly string AreaShapeEntered = "area_shape_entered";
        public static readonly string AreaShapeExited = "area_shape_exited";
        public static readonly string BodyEntered = "body_entered";
        public static readonly string BodyExited = "body_exited";
    }
}
