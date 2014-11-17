namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("SolutionTree")>]
[<assembly: AssemblyProductAttribute("SolutionTree")>]
[<assembly: AssemblyDescriptionAttribute("Parses Visual Studio solution file `.sln` and returns its project structure.")>]
[<assembly: AssemblyVersionAttribute("0.0.1")>]
[<assembly: AssemblyFileVersionAttribute("0.0.1")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.0.1"
