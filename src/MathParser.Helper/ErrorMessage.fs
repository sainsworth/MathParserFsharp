module MathParser.ErrorMessage

type ErrorMessage =
// Test Error Messages
|RailwayTestFailure1
|RailwayTestFailure2

// BuildStack Parse Regex
| TheSuppliedExpressionIsInvalid

// BuildStack : General Checks
| InvalidStackSize
| UnequalOpenAndCloseParentheses

// EvaluateStack
| StackDidNotConsolidate
| CannotAccumulateStack
| NotANumberOrStack
| NotAnOperator