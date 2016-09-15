module MathParser.BuildStack

open MathParser.Domain

let buildStack (equationString:string) =
  let (ret:StackItem list) = []
  ret