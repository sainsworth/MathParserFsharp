module MathParser.EvaluateStack

open MathParser.Domain
open MathParser.DomainUtils

let secondItem (l:'a List) =
  l |> List.tail |> List.head
let tailOfTail (l:'a List) =
  l |> List.tail |> List.tail

let doTheOperation (accumulator:float) (operator:StackItem) (value:float) =
  match operator with
  | Add -> accumulator + value
  | Subtract -> accumulator - value
  | Multiply -> accumulator * value
  | Divide -> accumulator / value
  | _ -> failwith "Not an operator"

let rec private removeLeadingClose (thisStack:StackItem List) =
  match thisStack.Length with
  | 0 -> []
  | _ -> match thisStack.Head with
         | B_Close -> removeLeadingClose thisStack.Tail
         | _ -> thisStack

let rec private accumulate (accumulator:float) (thisStack:StackItem List) = 
  let rec evaluateStack (thisStack:StackItem List) = 
    let firstElement = thisStack.Head

    let rec keepEvaluating acc tail =
      let resAcc, (resTail:StackItem List) = accumulate acc tail
      match resTail.Length with
      | 0 -> resAcc, resTail
      | _ -> keepEvaluating resAcc resTail

    match firstElement with
    | Float x -> keepEvaluating x thisStack.Tail |> fun x -> [Float (fst x)] @ (snd x)
    | B_Open -> thisStack |> List.tail |> evaluateStack
    | _ -> failwith "Not A Number"

  let operator = thisStack |> List.head

  let remainingStack = match thisStack |> secondItem with
                       | B_Open -> thisStack |> tailOfTail |> evaluateStack
                       | _ -> thisStack.Tail

  let nextElement = remainingStack.Head
  let stackTail =  remainingStack.Tail

  let result = match nextElement with
               | Float x -> doTheOperation accumulator operator x
               | B_Close -> accumulator
               
               | _ -> failwith "It's all gone wrong"

  match stackTail |> List.length with
  | 0 -> (result, [])
  | _ -> stackTail |> fun t -> match t |> List.head with
                               | B_Close -> result, removeLeadingClose t.Tail
                               | _ -> accumulate result t

let rec evaluate (thisStack:StackItem List) = 
  match thisStack |> List.head with
  | Float x -> thisStack.Tail |> accumulate x
  | B_Open -> [ Add ] @ thisStack |> accumulate 0.0
 // | _ -> thisStack |> accumulate 0.0  // if we considered b4a8 (-4 + 8) to be valid: fettle here
  | _ -> failwith "It's all gone wrong"
  |> fst


