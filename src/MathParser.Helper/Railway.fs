module MathParser.Railway

// http://fsharpforfunandprofit.com/posts/recipe-part2/
open MathParser.ErrorMessage

//type Result<'TSuccess,'TFailure> = 
//    | Success of 'TSuccess
//    | Failure of 'TFailure

type Result<'TSuccess> = 
    | Success of 'TSuccess
    | Failure of ErrorMessage

// Bind has one switch function parameter.
// It is an adapter that converts the switch function into a fully two-track function (with two-track input and two-track output). 
let bind switchFunction twoTrackInput = 
    match twoTrackInput with
    | Success s -> switchFunction s
    | Failure f -> Failure f

let (>>=) twoTrackInput switchFunction = 
    bind switchFunction twoTrackInput 

//Switch composition has two switch function parameters. It combines them in series to make another switch function.
let (>=>) switch1 switch2 x = 
    match switch1 x with
    | Success s -> switch2 s
    | Failure f -> Failure f 

// convert a normal function into a two-track function
let map oneTrackFunction twoTrackInput = 
    match twoTrackInput with
    | Success s -> Success (oneTrackFunction s)
    | Failure f -> Failure f

let (>>-) oneTrackFunction twoTrackInput =
  twoTrackInput >> map oneTrackFunction

// convert a normal function into a switch (datacentric)
let switch f x = 
    f x |> Success

let (>->) switchFunction f x =
  match switchFunction x with
  | Success s -> switch f x
  | Failure f -> Failure f

// Dead End Function called tee, after the UNIX tee command:
let tee deadEndFunction x = 
    deadEndFunction x |> ignore
    x

// switchFunction >=> switch ( tee deadEndFunction )

let (>|>) switchFunction deadEndFunction =
  switchFunction
  >=> switch ( tee deadEndFunction )

let (>>|) switchFunction deadEndFunction =
  switchFunction
  >>= map (tee deadEndFunction)

//###############################################################
// example

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

let onetrackFunction input =
   { input with email = input.email.Trim().ToLower() }

let deadend input =
  printf "%s has the email %s" input.name input.email
  ignore

let combinedFunctionOrientes = // Function Oriented
  let validate2' = bind validate2

  validate1
  // SWITCH - combine 2 X 2 TRACK ///
  >> validate2'
  >> bind validate2
  >=> validate2
  // Map 1 TRACK to ///
  >> map onetrackFunction  // is equivalent
  >-> onetrackFunction
  // Map in a dead-end function: one that does stuff but the previous result is passed through to the next 
  >> map (tee deadend) // is equivalent
  >|> deadend
  // AAnd it still fits the shape
  >=> validate3

let combinedDataOriented x =  // Data Oriented
  let validate2' = bind validate2
  let deadend' = tee deadend

  
  x
  |> validate1
  // Binding in 2 track
  |> bind validate2
  |> validate2'
  >>= validate2 // are all equivalent

  >>= validate3
