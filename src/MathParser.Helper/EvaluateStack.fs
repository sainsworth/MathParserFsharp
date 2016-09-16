module MathParser.EvaluateStack

open MathParser.Domain
open MathParser.DomainUtils

let private prepareStack (thisStack:StackItem List) =
  match thisStack.Head with
  | Subtract -> [Float 0.0] @ thisStack
//  | B_Open -> [Float 0.0; Add ] @ thisStack
  | B_Open -> thisStack
  | Float x -> thisStack
  | _ ->  failwith "The supplied expression is does not start with a valid term"

//let private returnIfFloat thisStackItem =
//  thisStackItem |> tryparseStackItemFloat
//                |> fun x -> match x with
//                            | None -> failwith "The supplied expression cannot be evaluated (NAN)"
//                            | _ -> x
//let private returnIfFloatBClose (thisStack:StackItem List) =
//  match thisStack.Tail with
//  | [B_Close] -> thisStack.Head |> returnIfFloat
//  | _ -> failwith "The supplied expression cannot be evaluated (NEFBC)"
//
//let private evaluateNextFloat (thisStack:StackItem List) =
//  match thisStack.Length with
//  | 2 -> thisStack |> returnIfFloatBClose
//  | _ -> thisStack.Head |> returnIfFloat
//
//let getNextStackItem thisStack =
//  thisStack |> List.tail |> List.head
// 
//let evaluateStack (acc:float) (thisStack:StackItem List) =
//  let total = match thisStack.Length with
//              | 0 -> failwith "invalid expression: no stack to accumulate"
//              | 1 -> failwith "invalid expression: not enough components"
//              | _ -> match thisStack.Head with
//                     | Add -> acc + thisStack |> getNextStackItem   
//                     | Subtract -> acc - thisStack |> getNextStackItem   
//                     | Multiply -> acc * thisStack |> getNextStackItem   
//                     | Divide -> acc / thisStack |> getNextStackItem
//
//


let rec private accumulate (thisStack:StackItem List) (accumulator:float) =
  let operator = thisStack |> List.head
  let value = thisStack
              |> List.tail
              |> List.head
              |> tryparseStackItemFloat
              |> fun x -> match x with
                          | Some(y) -> y
                          | _ -> failwith "Not A Number"
  let remainingList = thisStack |> List.tail |> List.tail

  let result = match operator with
               | Add -> accumulator + value
               | Subtract -> accumulator - value
               | Multiply -> accumulator * value
               | Divide -> accumulator / value
               | _ -> failwith "Not an operator"

  match remainingList.Length with
  | 0 -> result
  | _ -> accumulate remainingList result

let evaluate (thisStack:StackItem List)= 
  match thisStack |> List.head |> tryparseStackItemFloat with
  | None -> failwith "Invalid stack"
  | Some (x) -> accumulate thisStack.Tail x
 

