namespace DataManagement.Entities
{
    public class AssetClassificationSet
    {
        public int IdAssetClassificationSet
        {
            get; set;
        }
        public int IdAssetClassificationSetType
        {
            get; set;
        }        

        public string AssetClassificationSetName
        {
            get; set;
        }

        public string AssetClassificationSetShortName
        {
            get; set;
        }
        public bool IsDeleted
        {
            get; set;
        }
    }
}