namespace TesterBase.Entities
{
    public class CatalogsJoinSample
    {
        public virtual int? CategoryId { get; set; }
        public virtual string Comments { get; set; }
        public virtual System.DateTime? CreatedDate { get; set; }
        public virtual int JoinSampleId { get; set; }
        public virtual int? RecordTypeId { get; set; }
        public virtual bool IsEnabled { get; set; }
        public virtual bool IsCommited { get; set; }

        public virtual CatalogValue Category { get; set; }
        public virtual CatalogValue RecordType { get; set; }
    }
}
