open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Oxpecker
open Effects.Api

module App =

    let app =
        WebApplication
            .CreateBuilder()
            .Build()

    app
        .UseRouting()
        .UseOxpecker(Endpoints.list)
    |> ignore

    app.Run()
