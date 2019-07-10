namespace TesterBase.Entities
{
    public class MultiSelectSample
    {
        public virtual System.DateTime CreatedDate { get; set; }
        public virtual int MultiSelectSampleId { get; set; }

        public virtual System.Collections.Generic.ICollection<MultiSelectList> MultiSelectLists { get; set; }
        public virtual System.Collections.Generic.ICollection<MultiSelectCheckbox> MultiSelectCheckboxes { get; set; }
        public virtual System.Collections.Generic.ICollection<MultiSelectTable> MultiSelectTables { get; set; }
    }
}
