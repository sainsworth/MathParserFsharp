module MathParser.EvaluateStack

open MathParser.ErrorMessage
open MathParser.Domain
open MathParser.DomainUtils
open MathParser.Railway

let private getNext (thisStack:StackItem List) =
  thisStack.Head, thisStack.Tail

let private getNext2 (thisStack:StackItem List) =
  thisStack.Head, thisStack.Tail.Head, thisStack.Tail.Tail

let rec private consolidateStack (s_old:StackItem List) (s_new:StackItem List) =
  let h, t = s_old |> getNext

  let hh, tt = match h with
               | B_Open -> let oo, nn = consolidateStack t []
                           match nn with
                           | [] -> oo |> getNext
                           | _ -> Stack nn, oo 
               | B_Close -> h, [Stack s_new] @ t
               | _ -> h, t
  match hh with
  | B_Close -> tt, []
  | _ -> match tt.Length with
         | 0 -> tt, s_new @ [hh]
         | _ -> consolidateStack tt (s_new @ [hh])

let private doTheOperation (accumulator:float) (operator:StackItem) (value:float) =
  match operator with
  | Add -> Success (accumulator + value)
  | Subtract -> Success (accumulator - value)
  | Multiply -> Success (accumulator * value)
  | Divide -> Success (accumulator / value)
  | _ -> Failure NotAnOperator

let rec private accumulateStack (thisStack:StackItem List) =
  let rec accumulate (accumulator:float) (thisStack:StackItem List) =
    let operator, value, remainder = thisStack |> getNext2
    let isValue = match value with
                  | Float f -> Success f
                  | Stack s -> accumulateStack s
                  | _ -> Failure CannotAccumulateStack

    match isValue with
    | Success fvalue -> let res = doTheOperation accumulator operator fvalue
                        match res with
                        | Success acc -> match remainder.Length with
                                         | 0 -> Success acc
                                         | _ -> accumulate acc remainder
                        | Failure f -> Failure f
    | Failure f -> Failure f

  let h,t = thisStack |> getNext
  match h with
  | Stack s -> match accumulateStack s with
               | Success acc -> match t.Length with
                                | 0 -> Success acc
                                | _ -> accumulate acc t
               | Failure f -> Failure f
  | Float f -> accumulate f t
  | _ -> Failure NotANumberOrStack
  
let evaluate (thisStack:StackItem List) =
  let remainder, consolidated = consolidateStack thisStack []

  match remainder.Length with
  | 0 -> accumulateStack consolidated
  | _ -> Failure StackDidNotConsolidate