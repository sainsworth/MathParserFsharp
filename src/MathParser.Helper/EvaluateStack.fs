module MathParser.EvaluateStack

open MathParser.Domain
open MathParser.DomainUtils

let doTheOperation (accumulator:float) (operator:StackItem) (value:float) =
  match operator with
  | Add -> accumulator + value
  | Subtract -> accumulator - value
  | Multiply -> accumulator * value
  | Divide -> accumulator / value
  | _ -> failwith "Not an operator"

let rec private accumulate (accumulator:float) (thisStack:StackItem List) =
  let operator = thisStack |> List.head
  let nextElement = thisStack |> List.tail |> List.head
  let result = match nextElement with
               | Float x -> doTheOperation accumulator operator x
               | _ -> failwith "It's all gone wrong"

  match thisStack |> List.tail |> List.tail |> List.length with
  | 0 -> result
  | _ -> thisStack |> List.tail |> List.tail |> accumulate result 

//// 1 + ( 2 * ( 3 + 4 ) - 5 )
////       ^ first element of substack
////                       ^ last
//let rec private buildSubStack (thisStack:StackItem List)
//  let firstElement = thisStack |> List.head
//  match firstElement with
//  | 

let evaluate (thisStack:StackItem List) = 
  let firstElement = thisStack |> List.head
  match firstElement with
  | Float x -> accumulate x (thisStack |> List.tail)
  | _ -> failwith "Not A Number"
 

