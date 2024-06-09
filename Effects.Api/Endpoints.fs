namespace Effects.Api

open Effects.Monad
open System.Net.Http
open Oxpecker

module Endpoints =

    let private httpClient = new HttpClient()

    // Effect handler

    let run handler : EndpointHandler =
        fun context -> task {
            let effectHandler = EffectsHandler.create httpClient

            let! result =
                handler ()
                |> Effect.run effectHandler
                |> AsyncResult.value

            let httpHandler =
                match result with
                | Ok v -> text v >=> setStatusCode 200
                | Error e -> text $"Something went wrong: %s{e}" >=> setStatusCode 500

            return! httpHandler context
        }

    // Endpoints

    let list =
        GET [
            route "/status" (run Handlers.getStatusHandler)
        ]
        |> List.singleton
