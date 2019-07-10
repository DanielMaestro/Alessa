CREATE VIEW Samples.CatalogsJoinSamplesView
AS
SELECT CJS.CategoryId AS Category, CJS.Comments, CJS.CreatedDate, CJS.JoinSampleId, CJS.RecordTypeId AS RecordType
FROM Samples.CatalogsJoinSample CJS;

GO

CREATE VIEW Samples.HideEnableSamplesView
AS
SELECT HES.Checkbox, HES.EnableWhen5, HES.HideWhen2OrMore, HES.GridList, HES.HideEnableSampleId AS MultiSelect, HES.ShowChkbox, HES.ShowWhenBasic, HES.HideEnableSampleId
FROM Samples.HideEnableSample HES;

GO

CREATE VIEW Samples.MultiSelectSamplesView
AS
SELECT MSS.MultiSelectSampleId AS CheckboxSelection, MSS.CreatedDate, MSS.MultiSelectSampleId, MSS.MultiSelectSampleId AS Multiselection, MSS.MultiSelectSampleId AS RecordsGrid
FROM Samples.MultiSelectSample MSS;
GO

