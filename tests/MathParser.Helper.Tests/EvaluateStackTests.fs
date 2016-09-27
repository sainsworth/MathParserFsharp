module MathParser.Tests.EvaluateStackTests

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
  res |> should equal "ItHasAllGoneWrong"

[<Test>]
let ``EvaluateStack: When evaluating a simple stack the stack is returned`` () =
  let data = [ Float 2.0;Add; Float 3.0]
  let res = data
            |> evaluate
  res |> should equal (Success data)

[<Test>]
let ``EvaluateStack: When evaluating a complex stack the stack is returned with sub-stacks`` () =
  let data = [ Float 2.0; Add; Float 3.0; Subtract; B_Open; Float 5.0; Subtract; Float 4.0; B_Close; Multiply; Float 3.0]
  let res = try
              data
              |> evaluate
              |> errorIfFailure
              |> fun r -> r |> should equal [ Float 2.0; Add; Float 3.0; Subtract; Stack [ Float 5.0; Subtract; Float 4.0 ]; Multiply; Float 3.0]
              |> ignore
              "No exception caught"
            with
            | ex -> ex.Message
  res |> should equal "No exception caught"
