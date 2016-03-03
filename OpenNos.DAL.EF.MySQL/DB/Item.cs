//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OpenNos.DAL.EF.MySQL.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            this.inventoryitem = new HashSet<InventoryItem>();
            this.shopitem = new HashSet<ShopItem>();
        }
    
        public short VNum { get; set; }
        public long Price { get; set; }
        public string Name { get; set; }
        public byte ItemType { get; set; }
        public byte EquipmentSlot { get; set; }
        public short Morph { get; set; }
        public byte Type { get; set; }
        public byte Class { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsDroppable { get; set; }
        public bool IsTradable { get; set; }
        public bool IsSoldable { get; set; }
        public bool IsMinilandObject { get; set; }
        public bool IsWarehouse { get; set; }
        public byte LevelMinimum { get; set; }
        public short DamageMinimum { get; set; }
        public short DamageMaximum { get; set; }
        public short Concentrate { get; set; }
        public short HitRate { get; set; }
        public short CriticalLuckRate { get; set; }
        public short CriticalRate { get; set; }
        public short CloseDefence { get; set; }
        public short DistanceDefence { get; set; }
        public short MagicDefence { get; set; }
        public short DistanceDefenceDodge { get; set; }
        public short DefenceDodge { get; set; }
        public short Hp { get; set; }
        public short Mp { get; set; }
        public byte LevelJobMinimum { get; set; }
        public byte MaxCellon { get; set; }
        public byte MaxCellonLvl { get; set; }
        public short FireResistance { get; set; }
        public short WaterResistance { get; set; }
        public short LightResistance { get; set; }
        public short DarkResistance { get; set; }
        public byte DarkElement { get; set; }
        public byte LightElement { get; set; }
        public byte FireElement { get; set; }
        public byte WaterElement { get; set; }
        public short PvpStrength { get; set; }
        public byte Speed { get; set; }
        public byte Element { get; set; }
        public short ElementRate { get; set; }
        public short PvpDefence { get; set; }
        public short ReduceOposantResistance { get; set; }
        public short HpRegeneration { get; set; }
        public short MpRegeneration { get; set; }
        public short MoreHp { get; set; }
        public short MoreMp { get; set; }
        public bool IsColored { get; set; }
        public bool IsConsumable { get; set; }
        public byte ReputationMinimum { get; set; }
        public byte FairyMaximumLevel { get; set; }
        public byte MaximumAmmo { get; set; }
        public byte BasicUpgrade { get; set; }
        public short Color { get; set; }
        public long ItemValidTime { get; set; }
        public short Effect { get; set; }
        public int EffectValue { get; set; }
        public byte CellonLvl { get; set; }
        public byte SpType { get; set; }
        public byte Sex { get; set; }
        public byte SecondaryElement { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryItem> inventoryitem { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShopItem> shopitem { get; set; }
    }
}
