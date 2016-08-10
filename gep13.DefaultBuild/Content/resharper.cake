///////////////////////////////////////////////////////////////////////////////
// ADDINS
///////////////////////////////////////////////////////////////////////////////

#addin nuget:?package=Cake.ReSharperReports&version=0.3.1

///////////////////////////////////////////////////////////////////////////////
// TOOLS
///////////////////////////////////////////////////////////////////////////////

#tool nuget:?package=JetBrains.ReSharper.CommandLineTools&version=2016.1.20160504.105828
#tool nuget:?package=ReSharperReports&version=0.2.0

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