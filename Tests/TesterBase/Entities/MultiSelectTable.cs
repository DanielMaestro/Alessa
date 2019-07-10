namespace TesterBase.Entities
{
    public class MultiSelectTable
    {
        public virtual string BasicColumnTypeId { get; set; }
        public virtual bool IsEnabled { get; set; }
        public virtual int MultiSelectSampleId { get; set; }

        public virtual MultiSelectSample MultiSelectSample { get; set; }
        public virtual BasicColumnType BasicColumnType { get; set; }
    }
}
