namespace Effects.Monad

open Effect

type EffectBuilder() =

    member _.Bind(x, fn) = bind fn x

    member _.Bind(x:Async<_>, fn) = retAsync x |> bind fn

    member _.Bind(x:Result<_,_>, fn) = retResult x |> bind fn

    member _.Bind(x:AsyncResult<_,_>, fn) = retAsyncResult x |> bind fn

    member _.Zero() = ret ()

    member _.Return(x) = ret x

    member _.ReturnFrom(x) = x

[<AutoOpen>]
module EffectBuilder =

    let effect = EffectBuilder()
