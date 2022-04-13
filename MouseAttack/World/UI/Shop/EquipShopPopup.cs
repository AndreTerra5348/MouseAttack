using Godot;
using MouseAttack.Equip.Data;

namespace MouseAttack.World.UI.Shop
{
    public class EquipShopPopup : CommonShopPopup
    {
        [Export]
        EquipTier Tier = EquipTier.Epic;
        public override void Popup()
        {
            if ((Item as CommonEquip).Tier == Tier)
                base.Popup();
            else
                Sold();
        }
    }
}
