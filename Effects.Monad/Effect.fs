namespace Effects.Monad

type Effect<'c, 'a, 'e> = Effect of ('c -> AsyncResult<'a, 'e>)

module Effect =

    let create fn = Effect (fn >> AsyncResult.ret)
    let createFromAsync fn = Effect (fn >> AsyncResult.retAsync)
    let createFromAsyncResult fn = Effect (fn >> AsyncResult)

    let ret x = Effect (fun _ -> AsyncResult.ret x)
    let retResult x = Effect (fun _ -> AsyncResult.retResult x)
    let retAsync x = Effect (fun _ -> AsyncResult.retAsync x)
    let retAsyncResult x = Effect (fun _ -> x)

    let run c (Effect effect) = effect c

    let bind fn x =
        Effect (fun c ->
            asyncResult {
                let! a = run c x
                let b = fn a
                return! run c b
            }
        )

    let map fn = bind (fn >> ret)
