namespace TesterBase.Entities
{
    public class MultiSelectList
    {
        public virtual int CatalogValueId { get; set; }
        public virtual bool IsEnabled { get; set; }
        public virtual int MultiSelectSampleId { get; set; }

        public virtual MultiSelectSample MultiSelectSample { get; set; }
        public virtual CatalogValue CatalogValue { get; set; }
    }
}
