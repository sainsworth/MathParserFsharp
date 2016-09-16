module MathParser.Parser

open MathParser.BuildStack
open MathParser.EvaluateStack

/// Documentation for my library
///
/// ## Example
///
///     let h = Library.hello 1
///     printfn "%d" h
///


  
  let parse equationString =
    equationString
    |> build
    |> evaluate


