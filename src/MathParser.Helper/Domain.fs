module MathParser.Domain

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