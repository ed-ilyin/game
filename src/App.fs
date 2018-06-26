module Game.App.View
open Elmish
open Elmish.Debug
open Elmish.HMR
open Elmish.React
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Types
open State


let button kind dispatch label msg =
    button [
        Class kind
        // Style [
        //     // Margin "4px"
        //     Background "#666"
        //     Color "#ddd"
        //     Display "inline-block"
        //     Padding "8px"
        // ]
        OnClick <| fun _ -> dispatch msg
    ] [ str label ]

let exit dispatch label placeId =
    button "exit" dispatch label (ChangePlace placeId)

let error message = div [ Style [ Color "red" ] ] [ str message ]

let item items dispatch itemId =
    match Map.tryFind itemId items with
    | Some (item: Item) ->
        let begining, itemDescription, ending, msg = item.description
        ofList [
            str begining
            button "" dispatch itemDescription msg
            str ending
        ]
    | None -> sprintf "%s потерялась :(" itemId |> error

let handItem dispatch itemId = button "hand" dispatch itemId ignore

let hands dispatch =
    function
        | [] -> [ str "На легке" ]
        | items -> List.map (handItem dispatch) items
    >> section []

let place dispatch model =
    match Map.tryFind model.placeId model.places with
    | None -> [ error "я заблудился :(" ]
    | Some place ->
        [   h1 [] [ str place.name ]
            str place.description
            str " "
            List.map (item model.items dispatch) place.items|> ofList
            hr []
            List.map (exit dispatch |> uncurry) place.exits |> ofList
        ]
    |> section []

let root model dispatch =
    ofList [
        place dispatch model
        hands ignore model.hands
    ]

// App
Program.mkProgram init update root
// |> Program.toNavigable (parseHash pageParser) urlUpdate
//-:cnd:noEmit
#if DEBUG
|> Program.withDebugger
|> Program.withHMR
#endif
//+:cnd:noEmit
|> Program.withReact "app"
|> Program.run
