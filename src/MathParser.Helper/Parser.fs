module MathParser.Parser

open MathParser.Railway
open MathParser.BuildStack
open MathParser.EvaluateStack

/// Documentation for my library
///
/// ## Example
///
///     let h = Library.hello 1
///     printfn "%d" h
///

//
//  
//let parse =
//  build
//  >=> evaluate


//let parse equationString =
//  equationString
//  |> build
//
let parse =
  build
  >=> evaluate