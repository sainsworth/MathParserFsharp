module MathParser.EvaluateStackNew

open MathParser.Railway
open MathParser.Domain
open MathParser.DomainUtils

let evaluate (thisStack:StackItem List) =
  Success thisStack