﻿#load "SampleSolutionFiles.fs"
#load "../../Src/SolutionTree/StringUtils.fs"
#load "../../Src/SolutionTree/SolutionTree.fs"

open System
open SolutionTree
open SolutionTree.SolutionTree

//// test
//match """Project("{F2A71F9B-5D33-465A-A702-920D77279786}") = "VsTemplar", "src\VsTemplar\VsTemplar.fsproj", "{F496D7E3-BC0F-449F-B3B7-9F1B73278C87}" """ with
//| ProjectStart (pId, pTypeId, name, path) -> sprintf "%A" (pId, pTypeId, name, path)
//| _ -> "None"
//;;
//// should return None
//match """xxx Project("{F2A71F9B-5D33-465A-A702-920D77279786}") = "VsTemplar", "src\VsTemplar\VsTemplar.fsproj", "{F496D7E3-BC0F-449F-B3B7-9F1B73278C87}" """ with
//| ProjectStart (pId, pTypeId, name, path) -> sprintf "%A"  (pId, pTypeId, name, path)
//| _ -> "None"

//;;
//// should return None
//match """Project("{F2A71F9B-5D33-465A-A702-920D77279786}") = "VsTemplar", "src\VsTemplar\VsTemplar.fsproj", "{F496D7E3-BC0F-449F-B3B7-9F1B73278C87}" """ with
//| ProjectStart (pId, FolderProjectType true, name, path) -> sprintf "%A" (pId, name, path)
//| _ -> "None"
//

//// test
//match """	ProjectSection(SolutionItems) = preProject""" with
//| ProjectSection "SolutionItems" -> true
//| _ -> false
//
//match """	ProjectSection(OtherItems) = preProject""" with
//| ProjectSection "SolutionItems" -> true
//| _ -> false

