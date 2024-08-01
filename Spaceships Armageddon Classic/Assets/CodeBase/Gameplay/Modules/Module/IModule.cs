namespace CodeBase.Modules.Module
{
    public interface IModule
    {
        public WeaponTypeId weaponTypeId { get; set; }
        public DimensionType dimensionType { get; set; }
        public string Name { get; set; }
    }
}