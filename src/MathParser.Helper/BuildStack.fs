module MathParser.BuildStack

open MathParser.Domain
open MathParser.DomainUtils
open System.Text.RegularExpressions

let private parseRegex template expression =
  let res = Regex(template).Match(expression)
  match res.Success with
  | true -> expression
  | _ -> failwith "The supplied expression is invalid"
 
let private addDelimiters (expression:string) =
  expression |> fun x -> x.Replace("a", ":+:")
             |> fun x -> x.Replace("b", ":-:")
             |> fun x -> x.Replace("c", ":*:")
             |> fun x -> x.Replace("d", ":/:")
             |> fun x -> x.Replace("e", ":(:")
             |> fun x -> x.Replace("f", ":):")
             |> fun x -> x.Replace("::", ":")

let private splitIntoComponents (expression:string) =
  expression |> fun x -> x.ToCharArray()
             |> Array.toList
             |> List.rev
             |> fun x -> match x.Head with
                         | ':' -> x.Tail
                         | _ -> x
             |> List.rev
             |> List.toArray
             |> fun x -> System.String(x)
             |> fun x -> x.Split(':')
             |> Array.map (fun x -> parseStackItem x)
             |> Array.toList

let build (equationString:string) =
  equationString |> parseRegex "^(\d|[abcdef]){1,}$"
                 |> addDelimiters
                 |> splitIntoComponents
               