using Godot;
using MouseAttack.Misc;

namespace MouseAttack.World.UI.Buff
{
	public class BuffDisplay : VBoxContainer, ISharable
	{
		[Export]
		PackedScene BuffLabelPanelScene { get; set; }
		public BuffDisplay() =>
			TreeSharer.RegistryNode(this);

		public void AddBuff(string text, int duration)
		{
			var label = BuffLabelPanelScene.Instance<BuffLabelPanel>();
			label.Text = text;
			label.Duration = duration;
			AddChild(label);
		}
	}
}
