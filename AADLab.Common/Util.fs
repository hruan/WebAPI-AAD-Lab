module Util

open System
open AADLab.Common

let (|Positive|ZeroOrNegative|) x = 
    if x > 0 then Positive
    else ZeroOrNegative

let GenerateOrderFor customer n = 
    let orderNumber _ = 
        { OrderId = customer + "-" + Guid.NewGuid().ToString()
          PlacedBy = customer }
    match n with
    | ZeroOrNegative -> Seq.empty<Order>
    | Positive -> Seq.initInfinite orderNumber |> Seq.take n
