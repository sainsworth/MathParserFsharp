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

  let getNextParts (thisStack:StackItem List) =
    let operator = thisStack |> List.head
    let nextElement = thisStack |> List.tail |> List.head
    let stackTail =  thisStack |> List.tail |> List.tail

    operator, nextElement, stackTail

  let operator, nextElement, stackTail = getNextParts thisStack
  let result = match nextElement with
               | Float x -> doTheOperation accumulator operator x
               | B_Close -> accumulator
               | _ -> failwith "It's all gone wrong"

  match stackTail |> List.length with
  | 0 -> (result, [])
  | _ -> stackTail |> fun t -> match t |> List.head with
                               | B_Close -> result, t.Tail
                               | _ -> accumulate result t

//let rec startStack (thisStack:StackItem List)
//  let firstElement = thisStack |> List.head
//
//  match firstElement with
//  | B_Open -> 

//// 1 + ( 2 * ( 3 + 4 ) - 5 )
////       ^ first element of substack
////                       ^ last
//let rec private buildSubStack (thisStack:StackItem List)
//  let firstElement = thisStack |> List.head
//  match firstElement with
//  | 

let rec evaluateRec (thisStack:StackItem List) = 
  let firstElement = thisStack |> List.head

  let rec keepAccumulating acc tail =
    let resAcc, resTail = accumulate acc tail
    match resTail.Length with
    | 0 -> resAcc, resTail
    | _ -> keepAccumulating resAcc resTail

  match firstElement with
  | Float x -> keepAccumulating x (thisStack |> List.tail) |> fun x -> [Float (fst x)] @ (snd x)
  | B_Open -> thisStack |> List.tail |> evaluateRec
  | _ -> failwith "Not A Number"

let rec evaluate (thisStack:StackItem List) = 
  let res = thisStack |> evaluateRec
  match res.Length with
  | 0 -> failwith "Empty stack"
  | 1 ->  match thisStack |> evaluateRec |> List.head with
          | Float x -> x
          | _ -> failwith ""

