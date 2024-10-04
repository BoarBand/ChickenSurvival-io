using SurvivalChicken.ScriptableObjects.EquipmentsParameters;

namespace SurvivalChicken.Interfaces
{
    public interface IPlayerEquipment
    {
        public HelmetEquipmentParameters HelmetEquipment { get; set; }
        public ArmorEquipmentParameters ArmorEquipment { get; set; }
        public BootsEquipmentParameters BootsEquipment { get; set; }
        public AttributeEquipmentParameters AttributeEquipment { get; set; }
        public WeaponModuleEquipmentParameters WeaponModuleEquipment { get; set; }
        public PetEquipmentParameters PetEquipment { get; set; }
    }
}
