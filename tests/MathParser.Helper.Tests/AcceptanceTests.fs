module MathParser.Parser.ParserTests

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
  |> should equal 20

[<Test>]
let ``Acceptance: When parsing 32a2d2 the result is 17`` () =
  "32a2d2"
  |> parse
  |> should equal 17

[<Test>]
let ``Acceptance: When parsing 500a10b66c32 the result is 14208`` () =
  "500a10b66c32"
  |> parse
  |> should equal 14208

[<Test>]
let ``Acceptance: When parsing 3ae4c66fb32 the result is 235`` () =
  "3ae4c66fb32"
  |> parse
  |> should equal 235

[<Test>]
let ``Acceptance: When parsing 3c4d2aee2a4c41fc4f the result is 990`` () =
  "3c4d2aee2a4c41fc4f"
  |> parse
  |> should equal 990
