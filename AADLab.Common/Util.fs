module Util

open System
open AADLab.Common

let GenerateOrderFor customer n = 
    let orderNumber _ = 
        { OrderId = customer + "-" + Guid.NewGuid().ToString()
          PlacedBy = customer }
    if n > 0 then Seq.initInfinite orderNumber |> Seq.take n 
    else Seq.empty<Order>