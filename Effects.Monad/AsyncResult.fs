namespace Effects.Monad

type AsyncResult<'a, 'e> = AsyncResult of Result<'a, 'e> Async

module AsyncResult =

    let value (AsyncResult r) = r

    let ret x =
        Ok x
        |> Async.ret
        |> AsyncResult

    let retError e =
        e
        |> Error
        |> Async.ret
        |> AsyncResult

    let retAsync x =
        x
        |> Async.map Ok
        |> AsyncResult

    let retResult x =
        x
        |> Async.ret
        |> AsyncResult

    let bind fn x =
        async {
            let! a = value x
            let! b =
                match a with
                | Ok v -> fn v
                | Error e -> retError e
                |> value

            return b
        }
        |> AsyncResult

    let map fn = bind (fn >> ret)
