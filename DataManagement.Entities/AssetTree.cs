namespace DataManagement.Entities
{
    public class AssetTree
    {
        public int IdAssetClassChild { get; set; }
        public int Level { get; set; }

        public string AssetClassParent { get; set; }
        public string AssetClassChild { get; set; }
        public string AssetClassChildName { get; set; }
        public int IdAssetClassParent { get; set; }
        public string LocalId { get; set; }
        public int AssetClassSetID { get; set; }
    }
}