namespace AADLab.Common

open System

[<StructuralEqualityAttribute; NoComparisonAttribute>]
type Order = 
    { OrderId : string
      PlacedBy : string }
    static member Empty = { OrderId = String.Empty; PlacedBy = String.Empty }

[<StructuralEqualityAttribute; NoComparisonAttribute>]
type OrderRequest = 
    { PlacedBy : string }
    static member Empty = { PlacedBy = String.Empty }

open System.Threading.Tasks
type IOrderRepository =
    abstract member Save : order : Order -> Task<string>
    abstract member Load : orderId : string -> Task<Order>
    abstract member OrdersBy : userId : string -> Task<seq<Order>>

type RepositoryCommand =
    | Save of Order
    | Load of string * AsyncReplyChannel<Order>
    | LoadOrdersBy of string * AsyncReplyChannel<seq<Order>>

type InMemoryRepository() =
    static let store = MailboxProcessor<RepositoryCommand>.Start(fun inbox ->
        let rec loop state =
            async {
                let! cmd = inbox.Receive()
                match cmd with
                | Save order ->
                    if Map.containsKey order.OrderId state then return! loop state
                    else return! loop (Map.add order.OrderId order state)
                | Load (id, ch) ->
                    if (Map.containsKey id state) then ch.Reply state.[id]
                    else ch.Reply Order.Empty
                    return! loop state
                | LoadOrdersBy (id, ch) ->
                    Map.toSeq state
                    |> Seq.map snd
                    |> Seq.filter (fun o -> o.PlacedBy = id)
                    |> ch.Reply
                    return! loop state

                return! loop state
            }
        loop Map.empty)

    static let generateOrder customer n = 
        let orderNumber _ = 
            { OrderId = customer.GetHashCode().ToString("x") + "-" + Guid.NewGuid().ToString()
              PlacedBy = customer }
        if n > 0 then Seq.initInfinite orderNumber |> Seq.take n 
        else Seq.empty<Order>

    interface IOrderRepository with
        member x.Save order =
            async {
                store.Post (Save order)
                return order.OrderId
            } |> Async.StartAsTask
        member x.Load orderId =
            async {
                return! store.PostAndAsyncReply (fun ch -> (Load (orderId, ch)))
            } |> Async.StartAsTask
        member x.OrdersBy userId =
            let min = 15
            async {
                let! orders = store.PostAndAsyncReply (fun ch -> (LoadOrdersBy (userId, ch)))
                let l = min - (Seq.length orders)
                if l > 0 then return Seq.append orders (generateOrder userId l)
                else return orders
            } |> Async.StartAsTask