﻿module MathParser.ErrorMessage

type ErrorMessage =
// Test Error Messages
|RailwayNoFailure
|RailwayTestFailure1
|RailwayTestFailure2

// BuildStack Parse Regex
| TheSuppliedExpressionIsInvalid

// BuildStack : General Checks
| InvalidStackSize
| UnequalOpenAndCloseParentheses

// EvaluateStack
| NotANumber
| NotAnOperator
| ItHassAllGoneWrong