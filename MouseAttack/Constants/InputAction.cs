using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Constants
{
    public enum MenuInputAction
    {
        CharacterPanel
    }
    public class InputAction
    {
        public static readonly string LMB = "LMB";
        public static readonly string RMB = "RMB";
        public static readonly string ElapseTurn = "ElapseTurn";

        public static readonly string HotkeyFormat = "Hotkey{0}";
    }
}
