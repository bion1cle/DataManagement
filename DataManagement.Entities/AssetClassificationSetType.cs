namespace DataManagement.Entities
{
    public class AssetClassificationSetType
    {
        public int IdAssetClassificationSetType
        {
            get; set;
        }

        public string AssetClassificationSetTypeName
        {
            get; set;
        }

        public string AssetClassificationSetTypeShortName
        {
            get; set;
        }
        public bool IsDeleted
        {
            get; set;
        }
    }
}