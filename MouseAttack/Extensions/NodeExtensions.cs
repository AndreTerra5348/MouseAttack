using Godot;
using System;
using MouseAttack.Misc;
using MouseAttack.World;
using System.ComponentModel;
using MouseAttack.Entity.Player;
using System.Reflection;
using System.Linq;
using MouseAttack.Constants;
using System.Threading.Tasks;

namespace MouseAttack.Extensions
{
    public static class NodeExtensions
    {
        public static SignalAwaiter CreateTimer(this Node caller, float timeout) =>
            caller.ToSignal(caller.GetTree().CreateTimer(timeout), Signals.Timeout);

        public static SignalAwaiter SkipNextFrame(this Node caller) =>
            caller.ToSignal(caller.GetTree(), Signals.IdleFrame);
    }
}
