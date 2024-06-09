namespace Effects.Monad

open AsyncResult

type AsyncResultBuilder() =

    member _.Return(x) = ret x

    member _.Bind(x, fn) = bind fn x

    member _.Zero() = ret ()

    member _.ReturnFrom(x) = x

[<AutoOpen>]
module AsyncResultBuilder =

    let asyncResult = AsyncResultBuilder()
