namespace Effects.Api

open System
open Effects.Monad

module Handlers =

    let getStatusHandler () =
        effect {
            let! date = Instant.now ()

            let! status =
                Uri "https://httpstat.us/200"
                |> HttpClient.getString

            do! Logger.log $"Got %s{status} at %s{date.ToString()}."

            return status
        }
