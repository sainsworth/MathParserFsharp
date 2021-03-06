﻿module MathParser.Tests.RailwayTests

open MathParser.ErrorMessage
open MathParser.Domain
open MathParser.Tests.TestUtils
open MathParser.Railway
open NUnit.Framework
open FsUnit
open System.IO

let mutable deadEndResult = "UNSET"
let TwoTrack1 (x:string) =
  match x.Length with
  | i when i > 4 -> x.Replace ('a', 'A') |> Success
  | _ -> Failure RailwayTestFailure1

let TwoTrack2 (x:string) =
  match x.Length with
  | 6 -> x.Replace ('d', 'D') |> Success
  | _ -> Failure RailwayTestFailure2

let OneTrack (x:string) =
  x.Replace ('b', 'B')


// a dead end is a one track where we don't care about the result
let DeadEnd (x:string) =
  deadEndResult <- "UNSET"
  deadEndResult <- x.Replace('c','C')
  deadEndResult

let okString = "abcdef"
let badString1 = "abcde"
let badString2 = "abc"

[<Test>]
let ``Railway: Data: 2 two track functions`` () =
  let res = okString
            |> TwoTrack1
            >>= TwoTrack2

  res |> should equal (Success "AbcDef")
  
[<Test>]
let ``Railway: Data: 2 two track functions: failure`` () =
  let res = badString1
            |> TwoTrack1
            >>= TwoTrack2

  match res with
  | Success s -> s |> should equal ""
  | Failure f -> f |> should equal RailwayTestFailure2

[<Test>]
let ``Railway: Functional: 2 two track functions: failure2`` () =
  let res = badString2
            |> TwoTrack1
            >>= TwoTrack2

  match res with
  | Success s -> s |> should equal ""
  | Failure f -> f |> should equal RailwayTestFailure1

[<Test>]
let ``Railway: Functional: 2 two track functions: failure3`` () =
  let fn = TwoTrack1
           >=> TwoTrack2
  
  let res = badString1
            |> fn

  match res with
  | Success s -> s |> should equal ""
  | Failure f -> f |> should equal RailwayTestFailure2

[<Test>]
let ``Railway: Data: 2 two track functions: OneTrack and DeadEnd`` () =
  let fn = TwoTrack1
           >-> OneTrack
           >|> DeadEnd
           >=> TwoTrack2

  let res = try
              okString
              |> fn
              |> errorIfFailure
            with
            | ex -> ex.Message
  res |> should equal "ABcDef"
  deadEndResult |> should equal "ABCdef"

[<Test>]
let ``Railway: Functional: 2 two track functions: OneTrack and DeadEnd`` () =
  let res = try
              okString
              |> TwoTrack1
              >>- OneTrack
              >>| DeadEnd
              >>= TwoTrack2
              |> errorIfFailure
            with
            | ex -> ex.Message
  res |> should equal "ABcDef"
  deadEndResult |> should equal "ABCdef"
