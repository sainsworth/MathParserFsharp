module MathParser.DomainUtils

open MathParser.Domain

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