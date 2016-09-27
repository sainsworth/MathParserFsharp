module MathParser.EvaluateStackNew

open MathParser.ErrorMessage
open MathParser.Domain
open MathParser.DomainUtils
open MathParser.Railway

let getNext (thisStack:StackItem List) =
  thisStack.Head, thisStack.Tail

let rec consolidateStack (s_old:StackItem List) (s_new:StackItem List) =
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

let evaluate (thisStack:StackItem List) =
//  let (h:StackItem), (t:StackItem List) = getNext thisStack

  let remainder, consolidated = consolidateStack thisStack []

  match remainder.Length with
  | 0 -> Success consolidated
  | _ -> Failure StackDidNotConsolidate

//let rec parseStack (s_old:StackItem List) (s_new:StackItem List) =
//  let rec getSubStack (t_stack:StackItem List) (s_new:StackItem List) =
//     let  remainder, subStack, keepGoing = parseStack t_stack []
//     match keepGoing with
//     | true -> getSubStack remainder subStack
//     | false -> remainder, (s_new @ [Stack subStack]), match remainder.Length with | 0 -> false | _ -> true 
////
////  let (h:StackItem), (t:StackItem List) = getNext s_old
////  match h with
////  |  
////  | Add | Subtract | Multiply | Divide -> t, s_new @ [h]
////  | B_Open -> getSubStack t s_new
////  | B_Close -> t, s_new
//
//  let h, t = getNext s_old
//
//  let oo, nn, kk = match h with
//                   | Float f -> t, s_new @ [h], true
//                   | Add | Subtract | Multiply | Divide -> t, s_new @ [h], true
//                   | B_Open -> getSubStack t s_new
//                   | B_Close -> t, s_new, false
//  match kk with
//  | false -> oo, nn, kk
//  | _ -> match oo.Length with
//         | 0 -> oo, nn, false
//         | _ -> parseStack oo nn