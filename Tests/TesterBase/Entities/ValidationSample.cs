namespace TesterBase.Entities
{
    public class ValidationSample
    {
        public virtual int AnythinButValue { get; set; }
        public virtual System.DateTime NotBeforeTwoDays { get; set; }
        public virtual int OnlyOneSupportedValue { get; set; }
        public virtual int RangeNumber { get; set; }
        public virtual string Regex { get; set; }
        public virtual string Required { get; set; }
        public virtual System.DateTime RequiredIfBasic { get; set; }
        public virtual string ValidationSampleId { get; set; }
        public virtual string VariableLength { get; set; }
    }
}
