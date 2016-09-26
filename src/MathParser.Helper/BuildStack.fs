module MathParser.BuildStack

open MathParser.ErrorMessage
open MathParser.Railway
open MathParser.Domain
open MathParser.DomainUtils
open System.Text.RegularExpressions

let private parseRegex (expression:string) =
  let res = Regex("^(\d|[abcdef]){1,}$").Match(expression)
  match res.Success with
  | true -> Success expression
  | _ -> Failure TheSuppliedExpressionIsInvalid
  
let private addDelimiters (expression:string) =
  let expr = expression
             |> fun x -> x.Replace("a", ":+:")
             |> fun x -> x.Replace("b", ":-:")
             |> fun x -> x.Replace("c", ":*:")
             |> fun x -> x.Replace("d", ":/:")
             |> fun x -> x.Replace("e", ":(:")
             |> fun x -> x.Replace("f", ":):")
             |> fun x -> x.Replace("::", ":")
  expr

let private dropIfStartsColon (chars:char list) =
    match chars.Head with
    | ':' -> chars.Tail
    | _ -> chars

let private tidyExcessColons (expression:string) =
  expression |> fun x -> x.ToCharArray()
             |> Array.toList
             |> dropIfStartsColon
             |> List.rev
             |> dropIfStartsColon
             |> List.rev
             |> List.toArray
             |> fun x -> System.String(x)

let private splitIntoComponents (expression:string) =
  expression |> fun x -> x.Split(':')
             |> Array.map (fun x -> parseStackItem x)
             |> Array.toList

let private generalChecks (thisStack:StackItem List) =
  let c_open = thisStack |> List.filter (fun x -> x = B_Open) |> List.length
  let c_close = thisStack |> List.filter (fun x -> x = B_Close) |> List.length
  match (c_open = c_close) with
  | true -> match thisStack |> List.length with
            | i when i % 2 = 1 -> Success thisStack
            | _ -> Failure InvalidStackSize
  | _ -> Failure UnequalOpenAndCloseParentheses

//let build1 x =
//  let pr = x
//           |> parseRegex
//           >|> addDelimiters
//           >-> tidyExcessColons
//           >=> generalChecks
//  pr

//let build =
//  parseRegex
//  >>| addDelimiters
////  >> switch (map addDelimiters)
////  >>- tidyExcessColons
////  >>- tidyExcessColons
//  >>- tidyExcessColons
//
////    >>- splitIntoComponents
//    >>= generalChecks

