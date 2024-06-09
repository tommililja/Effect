namespace Effects.Api

open System
open System.Net.Http
open NodaTime

type EffectsHandler(httpClient:HttpClient) =

    interface IGuid with
        member _.Create() = Guid.NewGuid()

    interface ILogger with
        member _.Log(m:string) = Console.WriteLine(m)

    interface IInstant with
        member _.Now() = SystemClock.Instance.GetCurrentInstant()

    interface IHttpClient with
        member _.GetString(url:Uri) =
            httpClient.GetStringAsync(url)
            |> Async.AwaitTask

module EffectsHandler =

    let create httpClient = EffectsHandler httpClient
