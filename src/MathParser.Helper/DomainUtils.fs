module MathParser.DomainUtils

open MathParser.Domain
open MathParser.ErrorMessage
open Microsoft.FSharp.Reflection

// Used by error trapping in the tests
let toString (x:'a) = 
    match FSharpValue.GetUnionFields(x, typeof<'a>) with
    | case, _ -> case.Name

let fromString<'a> (s:string) =
    match FSharpType.GetUnionCases typeof<'a> |> Array.filter (fun case -> case.Name = s) with
    |[|case|] -> Some(FSharpValue.MakeUnion(case,[||]) :?> 'a)
    |_ -> None

let errorIfFailure result =
   match result with
   | Success s -> s
   | Failure f -> f |> toString |> failwith

// Used in the system

let isNullOrEmpty (x:obj) =
   match obj.ReferenceEquals(x, null) with
   | true -> true
   | _ -> match x with
          | :? string as s -> s.Length > 0
          | :? (obj list) as l -> l.Length > 0
          | _ -> false

let isNullOrWhitespace (x:string) =
  isNullOrEmpty (x.Replace(" ","").Replace("\t",""))

let (|Double|_|) str =
  match System.Double.TryParse(str) with
  | (true,double) -> Some(double)
  | _ -> None

let (|Int|_|) str =
   match System.Int32.TryParse(str) with
   | (true,int) -> Some(int)
   | _ -> None

let (|Bool|_|) str =
   match System.Boolean.TryParse(str) with
   | (true,bool) -> Some(bool)
   | _ -> None

let parseStackItem str = 
    match str with
    | Double f -> Float f
    | Int i -> Float (float i)
    | _ -> match str with
           | "+" -> Add
           | "-" -> Subtract
           | "*" -> Multiply
           | "/" -> Divide
           | "(" -> B_Open
           | ")" -> B_Close
           | _ -> UNKNOWN

let tryparseStackItemFloat stackItem =
  match stackItem with
  | Float x -> Some(x)
  | _ -> None