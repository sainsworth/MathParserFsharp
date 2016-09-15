module MathParser.Parser.BuildStackTests

open MathParser.Domain
open MathParser.BuildStack
open NUnit.Framework
open FsUnit

// Stack Criteria
//Input: 3a2c4
let result_1 = [ Float 3.0; Add; Float 2.0; Multiply; Float 4.0 ]
//
//Input: 32a2d2
let result_2 = [ Float 32.0; Add; Float 2.0; Divide; Float 4.0 ]
//
//Input: 500a10b66c32
let result_3 = [ Float 500.0; Add; Float 10.0; Subtract; Float 66.0; Multiply; Float 32.0 ]
//
//Input: 3ae4c66fb32
let result_4 = [ Float 3.0; Add; Stack [ Float 4.0; Multiply; Float 66.0 ]; Subtract; Float 32.0]
//
//Input: 3c4d2aee2a4c41fc4f
let result_5 = [ Float 3.0; Multiply; Float 4.0; Divide; Float 2.0; Add; Stack [ Stack [ Float 2.0; Add; Float 4.0; Multiply; Float 41.0; ]; Multiply; Float 4.0; ] ]

[<Test>]
let ``BuildStack: When parsing 3a2c4 the result is stack_1`` () =
  "3a2c4"
  |> buildStack
  |> should equal result_1
  
[<Test>]
let ``BuildStack: When parsing 32a2d2 the result is stack_2`` () =
  "32a2d2"
  |> buildStack
  |> should equal result_2
[<Test>]

let ``BuildStack: When parsing 500a10b66c32 the result is stack_3`` () =
  "500a10b66c32"
  |> buildStack
  |> should equal result_3
  
[<Test>]
let ``BuildStack: When parsing 3ae4c66fb32 the result is stack_4`` () =
  "3ae4c66fb32"
  |> buildStack
  |> should equal result_4
  
[<Test>]
let ``BuildStack: When parsing 3c4d2aee2a4c41fc4f the result is stack_5`` () =
  "3c4d2aee2a4c41fc4f"
  |> buildStack
  |> should equal result_2