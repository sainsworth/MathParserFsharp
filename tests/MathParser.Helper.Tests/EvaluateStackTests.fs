module MathParser.Parser.EvaluateStackTests

open MathParser.Domain
open MathParser.DomainUtils
open MathParser.EvaluateStackNew
open NUnit.Framework
open FsUnit 

[<Test>]
let ``EvaluateStack: When parsing an equation starting with invalid expression an exception is thrown`` () =
  let res = try
              [ Add; Float 3.0]
              |> evaluate
              |> errorIfFailure
              |> ignore
              "No exception caught"
            with
            | ex -> ex.Message
  res |> should equal "ItHassAllGoneWrong"