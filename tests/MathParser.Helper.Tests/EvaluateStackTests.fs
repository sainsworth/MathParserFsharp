module MathParser.Parser.EvaluateStackTests

open MathParser.Domain
open MathParser.EvaluateStack
open NUnit.Framework
open FsUnit 

[<Test>]
let ``EvaluateStack: When parsing an equation starting with invalid expression an exception is thrown`` () =
  let res = try
              [ Add; Float 3.0]
              |> evaluate
              |> ignore
              "No exception caught"
            with
            | Failure msg -> msg
  res |> should equal "Not A Number"