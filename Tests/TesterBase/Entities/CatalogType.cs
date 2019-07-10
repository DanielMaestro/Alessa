namespace TesterBase.Entities
{
    public class CatalogType
    {
        public virtual int CatalogTypeId { get; set; }
        public virtual string CatalogTypeName { get; set; }
        public virtual string CatalogTypeText { get; set; }
        public virtual bool IsEnabled { get; set; }

        public virtual System.Collections.Generic.ICollection<CatalogValue> CatalogValues { get; set; }
    }
}
