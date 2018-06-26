module Game.App.View
open Elmish
open Elmish.Debug
open Elmish.HMR
open Elmish.React
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Types
open State
open Fable.Core
open Fable.Import
open Fable.Core.JsInterop

type IDate = [<Emit("new $0()")>] abstract Create: unit -> JS.Date
let [<Global>] Date: IDate = jsNative
let mutable lastTouchEnd = 0.

do Browser.document.addEventListener ("touchend", !^(fun event ->
        let now = Date.Create().getTime()
        if (now - lastTouchEnd <= 400.)
            then do event.preventDefault ()
            else do lastTouchEnd <- now
    ), false
)

let button kind dispatch background label msg =
    button [
        Key label
        Class kind
        Style [ Background background ]
        OnClick <| fun _ -> dispatch msg
    ] [ str label ]

let exit dispatch background label placeId =
    ChangePlace (placeId, label)
    |> button "exit" dispatch background label

let error message = div [ Style [ Color "red" ] ] [ str message ]

let item items dispatch itemId =
    match Map.tryFind itemId items with
    | Some (item: Item) ->
        let begining, itemDescription, ending, msg = item.description
        ofList [
            str begining
            button "inline" dispatch "" itemDescription msg
            str ending
        ]
    | None -> sprintf "%s потерялась :(" itemId |> error

let handItem dispatch itemId = button "hand" dispatch "" itemId ignore
let hands dispatch = List.map (handItem dispatch) >> section []

let place dispatch model =
    match Map.tryFind model.placeId model.places with
    | None -> section [] [ error "я заблудился :(" ]
    | Some place ->
        [   h1 [] [ str place.name ]
            str place.description
            str " "
            List.map (item model.items dispatch) place.items |> ofList
            hr []
            List.map (fun (direction, placeId) ->
                let background =
                    match Map.tryFind placeId model.places with
                    | None -> ""
                    | Some place -> place.background
                exit dispatch background direction placeId
            ) place.exits |> ofList
        ]   |> section [ Style [ Background place.background ] ]

let root model dispatch =
    ofList [
        match model.status with
        | "" -> None | s -> section [] [ str s ] |> Some
        |> ofOption
        place dispatch model
        match model.hands with
        | [] -> None | h -> hands ignore h |> Some
        |> ofOption
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
