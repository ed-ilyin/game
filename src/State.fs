module Game.App.State
open Elmish
open Types

let init () =
    {   places = World.places
        placeId = "Спуск к морю"
        items = World.items
        hands = []
    }, Cmd.none

let bindPlace placeId func model =
    match Map.tryFind placeId model.places |> Option.bind func with
    | None -> model
    | Some place ->
        { model with places = Map.add model.placeId place model.places }
let removeItemFromPlace itemId model =
    bindPlace model.placeId (fun place ->
        Some
            { place with items = List.filter ((<>) itemId) place.items }
    ) model

let addItemToHands itemId model =
    { model with hands = model.hands @ [ itemId ] }

let update msg model =
    match msg with
    | Take itemId ->
        removeItemFromPlace itemId model |> addItemToHands itemId,
            Cmd.none
    | ChangePlace placeId ->
        { model with placeId = placeId }, Cmd.none
