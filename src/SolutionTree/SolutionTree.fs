
/// Provides functions for parsing Visual Studio solution file `.sln` and returning its projects structure.
///
/// ## Example
///
///     let projects = SolutionTree.FromFile "Sample.sln"
///     printfn "%A" projects
///
module SolutionTree.SolutionTree

open System
open System.IO
open System.Text.RegularExpressions


type SolutionPart =
| Project of ProjectData * typeId:Guid
| Folder of ProjectData
| SolutionItem of path:string

and ProjectData = {Id:Guid;Name:string;Path:string;Items:SolutionPart list}

type internal ChildParent = {Id:Guid;ParentId:Guid}
type internal NestedProjects = NestedProjects of ChildParent list 

/// Returns projects hierarchy
///
/// ## Parameters
///  - `fileLines` - solution file (.sln) content lines
let FromLines (fileLines:string[]) =

    let (|Regexx|_|) pattern input =
        let m = Regex.Match(input, pattern)
        if m.Success then Some(List.tail [ for g in m.Groups -> g.Value ])
        else None

    let trim (s:string) = s.Trim()
    let trimMulti cs (s:string) = s.Trim(cs)
    let split (separator:char) trimChars (value:string) = value.Split(separator) |> List.ofArray |> List.map (trimMulti trimChars)

    let (|GuidOpt|_|) value =
        match Guid.TryParse value with
        | true, guid -> Some guid
        | _ -> None

    let (|SplitByEqual|_|) line =
        match line |> split '=' [||] with
        | [left;right] -> Some (left,right)
        | _ -> None

    let (|ProjectStart|_|) (line:string) =

        let (|ProjectStartTypeId|_|) str =
            match str with
            | Regexx """^Project\("\{(.*)\}"\)$""" [GuidOpt id] -> Some id
            | _ -> None

        let (|ProjectStartValues|_|) str =
            match str |> split ',' [|'"';' '|] with
            | [name;path; GuidOpt id] -> Some (name, path, id)
            | _ -> None

        match line with
        | SplitByEqual (ProjectStartTypeId projectType, ProjectStartValues (name, path, pId)) -> Some (pId, projectType, name, path) 
        | _ -> None
    
    
    let (|FolderProjectType|) typeId =
        typeId = Guid "2150E333-8FDC-42A3-9474-1A3956D46DE8"

    let (|FolderProjectStart|_|) (line:string) =
        match line with
        | ProjectStart (pId, FolderProjectType pTypeId, name, path) -> Some (pId, name, path) 
        | _ -> None


    let (|GlobalStart|GlobalEnd|ProjectEnd|EndGlobalSection|EndProjectSection|Other|) (line:string) =
        match (trim line) with
        | "Global" -> GlobalStart
        | "EndGlobal" -> GlobalEnd
        | "EndProject" -> ProjectEnd
        | "EndProjectSection" -> EndProjectSection
        | "EndGlobalSection" -> EndGlobalSection
        | _ -> Other


    let (|Section|_|) name line =
        let pattern = sprintf """^\s*%s\((.*)\)$""" name
        match line with
        | SplitByEqual (Regexx pattern [s], _)  -> Some s
        | _ -> None

    let (|ProjectSection|_|) = (|Section|_|) "ProjectSection"
    let (|GlobalSection|_|) = (|Section|_|) "GlobalSection"


    let rec parseProjects lines = 

        let rec parseFolderProjectSection lines = seq {
                match lines with
                | EndProjectSection :: rest -> ()
                | SplitByEqual (left,_) :: rest -> 
                    yield left
                    yield! parseFolderProjectSection rest
                | _ -> ()
            }

        seq {
        match lines with

        | ProjectStart (id, FolderProjectType true, name, path) :: (ProjectSection  "SolutionItems") :: rest ->
            let items = parseFolderProjectSection rest |> Seq.toList
            yield Folder {Id = id;Name = name; Path = path; Items = items |> List.map SolutionItem}
            yield! rest |> Seq.skip (items.Length) |> Seq.toList |> parseProjects

        | ProjectStart (id, FolderProjectType true, name, path) :: rest ->
            yield Folder {Id = id; Name = name; Path = path; Items = []}
            yield! parseProjects rest

        | ProjectStart (id, typeId, name, path) :: rest ->
            yield Project ({Id = id; Name = name; Path = path; Items = []}, typeId)
            yield! parseProjects rest

        | GlobalStart :: _ -> ()
        | [] -> ()
        | _::rest 
            -> yield! parseProjects rest
        }

    let parseGlobalSection lines = 

        let rec parseNestedProjects inScope lines = seq {
            match lines with
            | GlobalSection "NestedProjects" :: rest ->
                yield! parseNestedProjects true rest
            | SplitByEqual (left,right) :: rest when inScope -> 
                yield {Id = Guid left; ParentId = Guid right}
                yield! parseNestedProjects true rest
            | EndGlobalSection :: _ when inScope -> ()
            | [] -> ()
            | _ :: rest -> 
                yield! parseNestedProjects inScope rest
        }

        lines
        |> parseNestedProjects false
        |> Seq.toList
        |> NestedProjects


    let hierarchize (NestedProjects childrenParents) projects =
        
        let psById = projects
                      |> Seq.choose (function 
                        | (Folder d | Project (d,_)) as p -> Some (d.Id, p) 
                        | _ -> None)
                      |> Map.ofSeq

        let partId = function
            | ((Folder d) | (Project (d,_))) -> Some d.Id
            | _ -> None 

        // for projects not existing in childrenParents add child/parent pair with root parentId (as None)
        let childrenParentsFull = seq {
            let chps = [for {Id = id; ParentId = pId} in childrenParents -> psById.[id], Some pId]
            yield! chps
            yield! [for p in projects -> p, None] 
                    |> Seq.filter (fun (p1, _) -> chps |> Seq.tryFind (fun (p2,_) -> partId p1 = partId p2) = None)
        }

        // traverse the list and create tree structure
        let rec toHierarchy parentId childrenParents =
              childrenParents 
                |> Seq.map (function
                  | (Folder d as p, pId) when pId = parentId -> 
                        Some (Folder {d with Items = d.Items @ ((toHierarchy (Some d.Id) childrenParents) |> List.map fst)}, parentId)
                  | (Project (d,tId) as p, pId) when pId = parentId ->
                        Some (Project ({d with Items = d.Items @ ((toHierarchy (Some d.Id) childrenParents) |> List.map fst)}, tId), parentId)
                  | _ -> None)
                |> Seq.choose id
                |> Seq.toList

        [for si,_ in toHierarchy None childrenParentsFull -> si]

    
    let solutionLines = fileLines |> Array.filter (String.IsNullOrWhiteSpace >> not)
    let projects = solutionLines |> Array.toList |> parseProjects
    let nesting = solutionLines |> Array.toList |> parseGlobalSection

    projects |> hierarchize nesting
    
/// Returns projects hierarchy
///
/// ## Parameters
///  - `file` - location of a solution file (.sln)
let FromFile(file:string) : SolutionPart list =
    
    file 
    |> File.ReadAllLines 
    |> FromLines
   


