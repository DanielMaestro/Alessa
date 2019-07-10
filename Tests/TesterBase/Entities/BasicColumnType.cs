namespace TesterBase.Entities
{
    public class BasicColumnType
    {
        public virtual string BasicColumnTypeId { get; set; }
        public virtual bool ColCheckbox { get; set; }
        public virtual System.DateTime ColDate { get; set; }
        public virtual System.DateTime ColDateTime { get; set; }
        public virtual decimal ColDouble { get; set; }
        public virtual int ColInteger { get; set; }
        public virtual decimal ColMoney { get; set; }
        public virtual string ColRichTextArea { get; set; }
        public virtual string ColText { get; set; }
        public virtual string ColTextArea { get; set; }
        public virtual System.TimeSpan ColTime { get; set; }

        public virtual System.Collections.Generic.ICollection<MultiSelectTable> MultiSelectTables { get; set; }
    }
}
