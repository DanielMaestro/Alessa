namespace TesterBase.Entities
{
    public class HideEnableSample
    {
        public virtual bool? Checkbox { get; set; }
        public virtual string EnableWhen5 { get; set; }
        public virtual int? HideWhen2OrMore { get; set; }
        public virtual decimal? GridList { get; set; }
        public virtual string ShowChkbox { get; set; }
        public virtual string ShowWhenBasic { get; set; }
        public virtual string HideEnableSampleId { get; set; }
        public virtual bool IsEnabled { get; set; }
        public virtual bool IsCommited { get; set; }

        public virtual System.Collections.Generic.ICollection<HideEnableMultiselection> HideEnableMultiselections { get; set; }
    }
}
