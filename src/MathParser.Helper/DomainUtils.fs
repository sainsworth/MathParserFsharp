module MathParser.DomainUtils

open MathParser.Domain
open MathParser.ErrorMessage

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
