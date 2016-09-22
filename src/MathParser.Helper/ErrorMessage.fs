module MathParser.ErrorMessage


type ErrorMessage =
// Test Error Messages
|NameMustNotBeBlank
|NameMustNotBeLongerThan50Chars
|EmailMustNotBeBlank

// BuildStack Parse Regex
| TheSuppliedExpressionIsInvalid

// BuildStack : General Checks
| InvalidStackSize
| UnequalOpenAndCloseParentheses

// EvaluateStack
| NotANumber
| NotAnOperator
| ItHassAllGoneWrong