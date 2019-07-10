namespace TesterBase.Entities
{
    public class CatalogValue
    {
        public virtual int CatalogTypeId { get; set; }
        public virtual bool CatalogValueDisplayEnabled { get; set; }
        public virtual int CatalogValueId { get; set; }
        public virtual string CatalogValueName { get; set; }
        public virtual string CatalogValueText { get; set; }
        public virtual bool IsEnabled { get; set; }

        public virtual CatalogType CatalogType { get; set; }
        public virtual System.Collections.Generic.ICollection<MultiSelectList> MultiSelectLists { get; set; }
        public virtual System.Collections.Generic.ICollection<MultiSelectCheckbox> MultiSelectCheckboxes { get; set; }
        public virtual System.Collections.Generic.ICollection<HideEnableMultiselection> HideEnableMultiselections { get; set; }
        public virtual System.Collections.Generic.ICollection<CatalogsJoinSample> Categories { get; set; }
        public virtual System.Collections.Generic.ICollection<CatalogsJoinSample> RecordTypes { get; set; }
    }
}
