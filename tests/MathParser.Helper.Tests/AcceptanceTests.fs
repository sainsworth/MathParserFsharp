module MathParser.Tests.AcceptanceTests

open MathParser.Tests.TestUtils
open MathParser.Parser
open NUnit.Framework
open FsUnit

// Acceptance Criteria
//Input: 3a2c4
//Result: 20
//
//Input: 32a2d2
//Result: 17
//
//Input: 500a10b66c32
//Result: 14208
//
//Input: 3ae4c66fb32
//Result: 235
//
//Input: 3c4d2aee2a4c41fc4f
//Result: 990

[<Test>]
let ``Acceptance: When parsing 3a2c4 the result is 20`` () =
  "3a2c4"
  |> parse
  |> errorIfFailure
  |> should equal 20

[<Test>]
let ``Acceptance: When parsing 32a2d2 the result is 17`` () =
  "32a2d2"
  |> parse
  |> errorIfFailure
  |> should equal 17

[<Test>]
let ``Acceptance: When parsing 500a10b66c32 the result is 14208`` () =
  "500a10b66c32"
  |> parse
  |> errorIfFailure
  |> should equal 14208

[<Test>]
let ``Acceptance: When parsing 3ae4c66fb32 the result is 235`` () =
  "3ae4c66fb32"
  |> parse
  |> errorIfFailure
  |> should equal 235

[<Test>]
let ``Acceptance: When parsing 3c4d2aee2a4c41fc4f the result is 990`` () =
  "3c4d2aee2a4c41fc4f"
  |> parse
  |> errorIfFailure
  |> should equal 990
  
[<Test>]
let ``Acceptance: When parsing e1a2f the result is 3`` () =
  "e1a2f"
  |> parse
  |> errorIfFailure
  |> should equal 3

[<Test>]
let ``Acceptance: When parsing ee1a2fc3f the result is 9`` () =
  "ee1a2fc3f"
  |> parse
  |> errorIfFailure
  |> should equal 9

[<Test>]
let ``Acceptance: When parsing (1+2)*3+(4*5)-(6+(8/2)) the result is 19`` () =
  "e1a2fc3ae4c5fbe6ae8d2ff"
  |> parse
  |> errorIfFailure
  |> should equal 19
