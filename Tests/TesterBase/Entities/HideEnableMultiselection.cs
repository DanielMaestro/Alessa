namespace TesterBase.Entities
{
    public class HideEnableMultiselection
    {
        public virtual int CatalogValueId { get; set; }
        public virtual bool IsEnabled { get; set; }
        public virtual string HideEnableSampleId { get; set; }

        public virtual CatalogValue CatalogValue { get; set; }
        public virtual HideEnableSample HideEnableSample { get; set; }
    }
}
