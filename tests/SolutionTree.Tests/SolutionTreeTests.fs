module SolutionTree.SolutionTreeTests

open System
open SolutionTree
open NUnit.Framework
open FsUnit

let toLines (s:string) = s.Split([|'\r'; '\n'|], StringSplitOptions.RemoveEmptyEntries)

let hierarchy = [
        Folder {Id = Guid.NewGuid(); Name=""; Path = ""; Items = [SolutionItem ""]}
        Folder {Id = Guid.NewGuid(); Name=""; Path = ""; Items = 
            [Project ({Id = Guid.NewGuid(); Name=""; Path = ""; Items = []}, Guid.NewGuid())
             Project ({Id = Guid.NewGuid(); Name=""; Path = ""; Items = []}, Guid.NewGuid())]}
        SolutionItem ""
        ]

[<Test>]
let ``Counting items in empty projects hierarchy returns zero``() =

    let result = [] |> SolutionTree.Count
    result |> should equal 0


[<Test>]
let ``Counting items in projects hierarchy returns correct result``() =

    let result = hierarchy |> SolutionTree.Count
    result |> should equal 6


[<Test>]
let ``Getting depth of empty projects hierarchy returns zero``() =

    let result = [] |> SolutionTree.Depth
    result |> should equal 0


[<Test>]
let ``Getting depth of projects hierarchy returns correct result``() =

    let result = hierarchy |> SolutionTree.Depth
    result |> should equal 2


[<Test>]
let ``Parsing solution from lines returns correct results``() =

    let result = 
        SampleSolutionFiles.``17 items, 3 levels`` |> toLines
        |> SolutionTree.FromLines
        
    result |> SolutionTree.Count |> should equal 17
    result |> SolutionTree.Depth |> should equal 3
