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

let switch f x =
  x |> f |> Success

let (>->) switchFunction simpleFunction x =
  match switchFunction x with
  | Success s -> Success (simpleFunction s)
  | Failure f -> Failure f

let (>>-) twoTrackInput oneTrackFunction =
  match twoTrackInput with
  | Success s -> Success (oneTrackFunction s)
  | Failure f -> Failure f

// DeadEndFunction
// Like a one track function but disregard the result and pass through the original input
// Dead End Function called tee, after the UNIX tee command:
let tee deadEndFunction x = 
    deadEndFunction x |> ignore
    x

let (>|>) switchFunction deadEndFunction x =
  match switchFunction x with
  | Success s -> Success (tee deadEndFunction s)
  | Failure f -> Failure f

let (>>|) twoTrackInput deadEndFunction =
  match twoTrackInput with
  | Success s -> Success (tee deadEndFunction s)
  | Failure f -> Failure f

