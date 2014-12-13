namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("SolutionTree")>]
[<assembly: AssemblyProductAttribute("SolutionTree")>]
[<assembly: AssemblyDescriptionAttribute("Parses Visual Studio solution file `.sln` and returns its projects structure.")>]
[<assembly: AssemblyVersionAttribute("0.1.0")>]
[<assembly: AssemblyFileVersionAttribute("0.1.0")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.1.0"
