module MathParser.Tests.EvaluateStackTests

open MathParser.Domain
open MathParser.DomainUtils
open MathParser.EvaluateStack
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
  res |> should equal "NotANumberOrStack"

[<Test>]
let ``EvaluateStack: When evaluating a complex stack the stack is returned with sub-stacks`` () =
  let data = [ Float 2.0; Add; Float 3.0; Subtract; B_Open; Float 5.0; Subtract; Float 4.0; B_Close; Multiply; Float 3.0]
  let res = data
            |> evaluate
  res |> should equal (Success 12.0)

[<Test>]
let ``EvaluateStack: When evaluating a simple stack correct result`` () =
  let data = [ Float 2.0;Add; Float 3.0]
  let res = data
            |> evaluate
  res |> should equal (Success 5.0)