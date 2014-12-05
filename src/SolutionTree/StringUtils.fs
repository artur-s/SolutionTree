module StringUtils

open System.Text.RegularExpressions

let (|Regexx|_|) pattern input =
       let m = Regex.Match(input, pattern)
       if m.Success then Some(List.tail [ for g in m.Groups -> g.Value ])
       else None

let trim (s:string) = s.Trim()
let trimMulti cs (s:string) = s.Trim(cs)
let split (separator:char) trimChars (value:string) = value.Split(separator) |> List.ofArray |> List.map (trimMulti trimChars)

let (|SplitByEqual|_|) line =
        match line |> split '=' [||] with
        | [left;right] -> Some (left,right)
        | _ -> None