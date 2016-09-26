module MathParser.Domain

open MathParser.ErrorMessage

type Result<'TSuccess> = 
    | Success of 'TSuccess
    | Failure of ErrorMessage

type StackItem = 
| Float of float
| Add           // a
| Subtract      // b
| Multiply      // c
| Divide        // d
| B_Open        // e
| B_Close       // f
| UNKNOWN
| Stack of StackItem List