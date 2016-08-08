///////////////////////////////////////////////////////////////////////////////
// ADDINS
///////////////////////////////////////////////////////////////////////////////
#addin Cake.ReSharperReports

///////////////////////////////////////////////////////////////////////////////
// TOOLS
///////////////////////////////////////////////////////////////////////////////
#tool JetBrains.ReSharper.CommandLineTools
#tool ReSharperReports

///////////////////////////////////////////////////////////////////////////////
// TASK DEFINITIONS
///////////////////////////////////////////////////////////////////////////////

Task("DupFinder")
    .IsDependentOn("Clean")
    .Does(() =>
{
    DupFinder(solutionFilePath, new DupFinderSettings() {
        ShowStats = true,
        ShowText = true,
        OutputFile = parameters.Paths.Directories.DupFinderTestResults.CombineWithFilePath("dupfinder.xml"),
        ThrowExceptionOnFindingDuplicates = true
    });
})
.ReportError(exception =>
{
    Information("Duplicates were found in your codebase, creating HTML report...");
    ReSharperReports(
        parameters.Paths.Directories.DupFinderTestResults.CombineWithFilePath("dupfinder.xml"),
        parameters.Paths.Directories.DupFinderTestResults.CombineWithFilePath("dupfinder.html"));
});

Task("InspectCode")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .Does(() =>
{
    InspectCode(solutionFilePath, new InspectCodeSettings() {
        SolutionWideAnalysis = true,
        Profile = parameters.Paths.Directories.Source.CombineWithFilePath(resharperSettingsFileName),
        OutputFile = parameters.Paths.Directories.InspectCodeTestResults.CombineWithFilePath("inspectcode.xml"),
        ThrowExceptionOnFindingViolations = true
    });
})
.ReportError(exception =>
{
    Information("Violations were found in your codebase, creating HTML report...");
    ReSharperReports(
        parameters.Paths.Directories.InspectCodeTestResults.CombineWithFilePath("inspectcode.xml"),
        parameters.Paths.Directories.InspectCodeTestResults.CombineWithFilePath("inspectcode.html"));
});

Task("Analyze")
    .IsDependentOn("DupFinder")
    .IsDependentOn("InspectCode");