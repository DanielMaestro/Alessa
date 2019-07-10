using Alessa.QueryBuilder;
using Microsoft.EntityFrameworkCore;
using TesterBase.Entities;

namespace TesterBase.DataContext
{
    public class SqlQueryBuilderTestDataContext : QueryBuilderDbContext
    {
        private string SampleSchema;

        public DbSet<CatalogType> CatalogTypes { get; set; }
        public DbSet<CatalogValue> CatalogValues { get; set; }
        public DbSet<BasicColumnType> BasicColumnTypes { get; set; }
        public DbSet<CatalogsJoinSample> CatalogsJoinSamples { get; set; }
        //public DbSet<CatalogsJoinSampleView> CatalogsJoinSampleViews { get; set; }
        public DbSet<HideEnableMultiselection> HideEnableMultiselections { get; set; }
        //public DbSet<HideEnableSampleView> HideEnableSampleViews { get; set; }
        public DbSet<HideEnableSample> HideEnableSamples { get; set; }
        public DbSet<MultiSelectCheckbox> MultiSelectCheckboxs { get; set; }
        public DbSet<MultiSelectList> MultiSelectLists { get; set; }
        public DbSet<MultiSelectSample> MultiSelectSamples { get; set; }
        //public DbSet<MultiSelectSampleView> MultiSelectSampleViews { get; set; }
        public DbSet<MultiSelectTable> MultiSelectTables { get; set; }
        public DbSet<ValidationSample> ValidationSamples { get; set; }

        public SqlQueryBuilderTestDataContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (string.IsNullOrWhiteSpace(base.SchemaPrefix))
            {
                this.SampleSchema = null;
            }
            else
            {
                this.SampleSchema = "Samples";
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatalogType>(table =>
            {

                table.ToTable("CatalogType", this.SampleSchema).HasKey(f => f.CatalogTypeId);

                table.Property(f => f.CatalogTypeId).IsRequired();
                table.Property(f => f.CatalogTypeName).IsUnicode(false).HasMaxLength(50).IsRequired();
                table.Property(f => f.CatalogTypeText).IsUnicode(false).HasMaxLength(255).IsRequired();
                table.Property(f => f.IsEnabled).IsRequired();


                table
                    .HasMany(f => f.CatalogValues)
                    .WithOne(f => f.CatalogType)
                    .HasForeignKey(f => f.CatalogTypeId);
            });

            modelBuilder.Entity<CatalogValue>(table =>
            {

                table.ToTable("CatalogValue", this.SampleSchema).HasKey(f => f.CatalogValueId);

                table.Property(f => f.CatalogTypeId).IsRequired();
                table.Property(f => f.CatalogValueDisplayEnabled).IsRequired();
                table.Property(f => f.CatalogValueId).IsRequired();
                table.Property(f => f.CatalogValueName).IsUnicode(false).HasMaxLength(50).IsRequired();
                table.Property(f => f.CatalogValueText).IsUnicode(false).HasMaxLength(255).IsRequired();
                table.Property(f => f.IsEnabled).IsRequired();

                table
                    .HasMany(f => f.Categories)
                    .WithOne(f => f.Category)
                    .HasForeignKey(f => f.CategoryId);

                table
                    .HasMany(f => f.HideEnableMultiselections)
                    .WithOne(f => f.CatalogValue)
                    .HasForeignKey(f => f.CatalogValueId);
                table
                    .HasMany(f => f.MultiSelectCheckboxes)
                    .WithOne(f => f.CatalogValue)
                    .HasForeignKey(f => f.CatalogValueId);
                table
                    .HasMany(f => f.MultiSelectLists)
                    .WithOne(f => f.CatalogValue)
                    .HasForeignKey(f => f.CatalogValueId);
                table
                    .HasMany(f => f.RecordTypes)
                    .WithOne(f => f.RecordType)
                    .HasForeignKey(f => f.RecordTypeId);
            });

            modelBuilder.Entity<BasicColumnType>(table =>
            {

                table.ToTable("BasicColumnType", this.SampleSchema).HasKey(f => f.BasicColumnTypeId);

                table.Property(f => f.BasicColumnTypeId).IsUnicode(false).HasMaxLength(10).IsRequired().ValueGeneratedNever();
                table.Property(f => f.ColCheckbox).IsRequired();
                table.Property(f => f.ColDate).IsRequired();
                table.Property(f => f.ColDateTime).IsRequired();
                table.Property(f => f.ColDouble).IsRequired();
                table.Property(f => f.ColInteger).IsRequired();
                table.Property(f => f.ColMoney).IsRequired();
                table.Property(f => f.ColRichTextArea).IsUnicode(false).IsRequired();
                table.Property(f => f.ColText).IsUnicode(false).HasMaxLength(100).IsRequired();
                table.Property(f => f.ColTextArea).IsUnicode(false).HasMaxLength(500).IsRequired();
                table.Property(f => f.ColTime).IsRequired();

                table
                    .HasMany(f => f.MultiSelectTables)
                    .WithOne(f => f.BasicColumnType)
                    .HasForeignKey(f => f.BasicColumnTypeId);
            });

            modelBuilder.Entity<CatalogsJoinSample>(table =>
            {

                table.ToTable("CatalogsJoinSample", this.SampleSchema).HasKey(f => f.JoinSampleId);

                table.Property(f => f.CategoryId);
                table.Property(f => f.Comments).IsUnicode(false).HasMaxLength(1000);
                table.Property(f => f.CreatedDate);
                table.Property(f => f.JoinSampleId).IsRequired();
                table.Property(f => f.RecordTypeId);
                table.Property(f => f.IsEnabled).IsRequired();
                table.Property(f => f.IsCommited).IsRequired();

                table
                    .HasOne(f => f.Category)
                    .WithMany(f => f.Categories)
                    .HasForeignKey(f => f.CategoryId);
                table
                  .HasOne(f => f.RecordType)
                  .WithMany(f => f.RecordTypes)
                  .HasForeignKey(f => f.RecordTypeId);
            });

            modelBuilder.Entity<HideEnableMultiselection>(table =>
            {

                table.ToTable("HideEnableMultiselection", this.SampleSchema).HasKey(f => new { f.CatalogValueId, f.HideEnableSampleId });

                table.Property(f => f.CatalogValueId).IsRequired().ValueGeneratedNever();
                table.Property(f => f.IsEnabled).IsRequired();
                table.Property(f => f.HideEnableSampleId).IsUnicode(false).HasMaxLength(7).IsRequired().ValueGeneratedNever();

                table
                    .HasOne(f => f.CatalogValue)
                    .WithMany(f => f.HideEnableMultiselections)
                    .HasForeignKey(f => f.CatalogValueId);
                table
                  .HasOne(f => f.HideEnableSample)
                  .WithMany(f => f.HideEnableMultiselections)
                  .HasForeignKey(f => f.HideEnableSampleId);
            });

            modelBuilder.Entity<HideEnableSample>(table =>
            {

                table.ToTable("HideEnableSample", this.SampleSchema).HasKey(f => f.HideEnableSampleId);

                table.Property(f => f.Checkbox);
                table.Property(f => f.EnableWhen5).IsUnicode(false).HasMaxLength(25);
                table.Property(f => f.HideWhen2OrMore);
                table.Property(f => f.GridList);
                table.Property(f => f.ShowChkbox).IsUnicode(false).HasMaxLength(32);
                table.Property(f => f.ShowWhenBasic).IsUnicode(false).HasMaxLength(13);
                table.Property(f => f.HideEnableSampleId).IsUnicode(false).HasMaxLength(7).ValueGeneratedNever();
                table.Property(f => f.IsEnabled).IsRequired();
                table.Property(f => f.IsCommited).IsRequired();

                table
                    .HasMany(f => f.HideEnableMultiselections)
                    .WithOne(f => f.HideEnableSample)
                    .HasForeignKey(f => f.HideEnableSampleId);
            });

            modelBuilder.Entity<MultiSelectCheckbox>(table =>
            {

                table.ToTable("MultiSelectCheckbox", this.SampleSchema).HasKey(f => new { f.CatalogValueId, f.MultiSelectSampleId });

                table.Property(f => f.CatalogValueId).IsRequired().ValueGeneratedNever();
                table.Property(f => f.IsEnabled).IsRequired();
                table.Property(f => f.MultiSelectSampleId).IsRequired().ValueGeneratedNever();

                table
                    .HasOne(f => f.CatalogValue)
                    .WithMany(f => f.MultiSelectCheckboxes)
                    .HasForeignKey(f => f.CatalogValueId);
                table
                  .HasOne(f => f.MultiSelectSample)
                  .WithMany(f => f.MultiSelectCheckboxes)
                  .HasForeignKey(f => f.MultiSelectSampleId);
            });

            modelBuilder.Entity<MultiSelectList>(table =>
            {

                table.ToTable("MultiSelectList", this.SampleSchema).HasKey(f => new { f.CatalogValueId, f.MultiSelectSampleId });

                table.Property(f => f.CatalogValueId).IsRequired().ValueGeneratedNever();
                table.Property(f => f.IsEnabled).IsRequired();
                table.Property(f => f.MultiSelectSampleId).IsRequired().ValueGeneratedNever();

                table
                    .HasOne(f => f.CatalogValue)
                    .WithMany(f => f.MultiSelectLists)
                    .HasForeignKey(f => f.CatalogValueId);
                table
                  .HasOne(f => f.MultiSelectSample)
                  .WithMany(f => f.MultiSelectLists)
                  .HasForeignKey(f => f.MultiSelectSampleId);
            });

            modelBuilder.Entity<MultiSelectSample>(table =>
            {

                table.ToTable("MultiSelectSample", this.SampleSchema).HasKey(f => f.MultiSelectSampleId);

                table.Property(f => f.CreatedDate).IsRequired();
                table.Property(f => f.MultiSelectSampleId).IsRequired();

                table
                    .HasMany(f => f.MultiSelectCheckboxes)
                    .WithOne(f => f.MultiSelectSample)
                    .HasForeignKey(f => f.MultiSelectSampleId);
                table
                    .HasMany(f => f.MultiSelectLists)
                    .WithOne(f => f.MultiSelectSample)
                    .HasForeignKey(f => f.MultiSelectSampleId);
                table
                    .HasMany(f => f.MultiSelectTables)
                    .WithOne(f => f.MultiSelectSample)
                    .HasForeignKey(f => f.MultiSelectSampleId);
            });


            modelBuilder.Entity<MultiSelectTable>(table =>
            {

                table.ToTable("MultiSelectTable", this.SampleSchema).HasKey(f => new { f.BasicColumnTypeId, f.MultiSelectSampleId });

                table.Property(f => f.BasicColumnTypeId).IsUnicode(false).HasMaxLength(10).IsRequired().ValueGeneratedNever();
                table.Property(f => f.IsEnabled).IsRequired();
                table.Property(f => f.MultiSelectSampleId).IsRequired().ValueGeneratedNever();

                table
                    .HasOne(f => f.BasicColumnType)
                    .WithMany(f => f.MultiSelectTables)
                    .HasForeignKey(f => f.BasicColumnTypeId);
                table
                  .HasOne(f => f.MultiSelectSample)
                  .WithMany(f => f.MultiSelectTables)
                  .HasForeignKey(f => f.MultiSelectSampleId);
            });

            modelBuilder.Entity<ValidationSample>(table =>
            {

                table.ToTable("ValidationSample", this.SampleSchema).HasKey(f => f.ValidationSampleId);

                table.Property(f => f.AnythinButValue).IsRequired();
                table.Property(f => f.NotBeforeTwoDays).IsRequired();
                table.Property(f => f.OnlyOneSupportedValue).IsRequired();
                table.Property(f => f.RangeNumber).IsRequired();
                table.Property(f => f.Regex).IsUnicode(false).HasMaxLength(300);
                table.Property(f => f.Required).IsUnicode(false).HasMaxLength(200).IsRequired();
                table.Property(f => f.RequiredIfBasic).IsRequired();
                table.Property(f => f.ValidationSampleId).IsUnicode(false).HasMaxLength(5).IsRequired().ValueGeneratedNever();
                table.Property(f => f.VariableLength).IsUnicode(false).HasMaxLength(5);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
