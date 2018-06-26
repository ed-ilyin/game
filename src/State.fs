module Game.App.State
open Elmish
open Types

let init () =
    {   places = World.places
        placeId = "Выход к морю"
        items = World.items
        hands = []
        status = "Одинокая девочка гуляла и вышла к морю."
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
        addItemToHands itemId model,
            Cmd.none
    | Talk objectId ->
        { model with status = sprintf "%s сказал 'кря'" objectId },
            Cmd.none
    | ChangePlace (placeId, placeDirection) ->
        { model with
            placeId = placeId
            status = "Девочка пошла " + placeDirection + "."
        }, Cmd.none
