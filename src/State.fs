module Game.App.State
open Elmish
open Types

let init () =
    {   world = World.world
        locationId = "Начало"
    }, Cmd.none

let update msg model =
    match msg with
    | ChangeLocationId locationId ->
        { model with locationId = locationId }, Cmd.none
