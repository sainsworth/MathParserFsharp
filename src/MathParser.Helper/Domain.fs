module MathParser.Domain

open Microsoft.FSharp.Reflection

let toString (x:'a) = 
    match FSharpValue.GetUnionFields(x, typeof<'a>) with
    | case, _ -> case.Name

let fromString<'a> (s:string) =
    match FSharpType.GetUnionCases typeof<'a> |> Array.filter (fun case -> case.Name = s) with
    |[|case|] -> Some(FSharpValue.MakeUnion(case,[||]) :?> 'a)
    |_ -> None

type StackItem = 
| Float of float
| Add           // a
| Subtract      // b
| Multiply      // c
| Divide        // d
| B_Open        // e
| B_Close       // f
| UNKNOWN
| Stack of StackItem List