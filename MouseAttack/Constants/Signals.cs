using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Constants
{
    public class Signals
    {
        public static readonly string MouseEntered = "mouse_entered";
        public static readonly string MouseExited = "mouse_exited";
        public static readonly string GuiInput = "gui_input";

        public static readonly string AreaEntered = "area_entered";
        public static readonly string AreaExited = "area_exited";
        public static readonly string AreaShapeEntered = "area_shape_entered";
        public static readonly string AreaShapeExited = "area_shape_exited";
        public static readonly string BodyEntered = "body_entered";
        public static readonly string BodyExited = "body_exited";

        public static readonly string Timeout = "timeout";

        public static readonly string ValueChanged = "value_changed";

        public static readonly string Pressed = "pressed";

        public static readonly string Ready = "ready";

        public static readonly string IdleFrame = "idle_frame";

        public static readonly string TweenAllCompleted = "tween_all_completed";

        public static readonly string AnimationFinished = "animation_finished";

        public static readonly string IndexPressed = "index_pressed";

        public static readonly string PopupHide = "popup_hide";
    }
    
}
