module MathParser.Helper.ParserTests

open MathParser.Parser
open NUnit.Framework
//open FsUnit

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
let ``When parsing 3a2c4 the result is 20`` () =
  let result = Parser.parse "3a2c4"
  printfn "%i" result
  Assert.AreEqual(20,result)

[<Test>]
let ``When parsing 32a2d2 the result is 17`` () =
  let result = Parser.parse "32a2d2"
  printfn "%i" result
  Assert.AreEqual(17,result)

[<Test>]
let ``When parsing 500a10b66c32 the result is 14208`` () =
  let result = Parser.parse "500a10b66c32"
  printfn "%i" result
  Assert.AreEqual(14208,result)

[<Test>]
let ``When parsing 3ae4c66fb32 the result is 235`` () =
  let result = Parser.parse "3ae4c66fb32"
  printfn "%i" result
  Assert.AreEqual(235,result)

[<Test>]
let ``When parsing 3c4d2aee2a4c41fc4f the result is 990`` () =
  let result = Parser.parse "3c4d2aee2a4c41fc4f"
  printfn "%i" result
  Assert.AreEqual(990,result)
