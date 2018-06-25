module Game.App.World
open Types

let location name exits =
    {   id = name
        name = name
        exits = exits
    }

let world =
    [   "Начало", [ "в середину", "Середина" ]
        "Середина", [ "на начало", "Начало"; "к концу", "Конец" ]
        "Конец", [ "в середину", "Середина" ]
    ]   |> Map.ofList
        |> Map.map location
