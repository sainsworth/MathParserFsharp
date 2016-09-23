module MathParser.RailwayTests

open MathParser.Railway
open MathParser.ErrorMessage
open NUnit.Framework
open FsUnit

type Request = {name:string; email:string}

let validate1 input =
   if input.name = "" then Failure NameMustNotBeBlank
   else Success input

let validate2 input =
   if input.name.Length > 50 then Failure NameMustNotBeLongerThan50Chars
   else Success input

let validate3 input =
   if input.email = "" then Failure EmailMustNotBeBlank
   else Success input

let emailToLower input = //onetrackFunction
   { input with email = input.email.Trim().ToLower() }

let printEmailOut input = //deadendFunction
  printf "%s has the email %s" input.name input.email
  ignore

//let combinedFunctionOrientes = // Function Oriented
//  let validate2' = bind validate2
//
//  validate1
//  // SWITCH - combine 2 X 2 TRACK ///
//  >> validate2'
//  >> bind validate2
//  >=> validate2
//  // Map 1 TRACK to ///
//  >> map onetrackFunction  // is equivalent
//  >-> emailToLower
//  // Map in a dead-end function: one that does stuff but the previous result is passed through to the next 
//  >> map (tee deadend) // is equivalent
//  >|> printEmailOut
//  // And it still fits the shape
//  >=> validate3
//
//let combinedDataOriented x =  // Data Oriented
//  let validate2' = bind validate2
//  let deadend' = tee deadend
//  
//  x
//  |> validate1
//  // Binding in 2 track
//  |> bind validate2
//  |> validate2'
//  >>= validate2 // are all equivalent
//
//  |> map onetrackFunction
//  >>- emailToLower // are all equivalent
//  
//  |> map (tee deadend)
//  >>- tee deadend
//  >>| printEmailOut // are equivalent
//  
//  >>= validate3

let failed = {name=""; email=""}

let stewtest_Upper = {name="Stewart Richard Ainsworth"; email="STEWART.AINSWORTH@NICE.ORG.UK"}
let stewtest = {name="Stewart Richard Ainsworth"; email="stewart.ainsworth@nice.org.uk"}

[<Test>]
let ``Railway: Data Oriented: Success results in all the way passes it all through`` () =

  let res = stewtest_Upper
            |> validate1
            >>= validate2
            >>= validate3
            >>- emailToLower
            >>| printEmailOut
  
  let x = match res with
          | Success s -> s
          | _ -> failed
  x |> should equal stewtest

[<Test>]
let ``Railway: Function Oriented: Success results in all the way passes it all through`` () =

  let combo = validate1
              >=> validate2
              >=> validate3
              >-> emailToLower
              >|> printEmailOut
     
  let res = stewtest_Upper |> combo
            
  let x = match res with
          | Success s -> s
          | _ -> failed
  x |> should equal stewtest