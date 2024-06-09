namespace Effects.Api

open System
open NodaTime
open Effects.Monad

type IGuid =
    abstract Create: unit -> Guid

module Guid =

    let create () = Effect.create (fun (guid:#IGuid) -> guid.Create())

type IInstant =
    abstract Now: unit -> Instant

module Instant =

    let now () = Effect.create (fun (instant:#IInstant) -> instant.Now())

type ILogger =
    abstract Log: string -> unit

module Logger =

    let log str = Effect.create (fun (logger:#ILogger) -> logger.Log(str))

type IHttpClient =
    abstract GetString: Uri -> Async<string>

module HttpClient =

    let getString url = Effect.createFromAsync (fun (httpClient:#IHttpClient) -> httpClient.GetString(url))
