module Game.App.View
open Elmish
open Elmish.Debug
open Elmish.HMR
open Elmish.React
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Types
open State

let exit dispatch label locationId =
    button [ OnClick <| fun _ -> ChangeLocationId locationId |> dispatch
    ] [ str label ]

let root model dispatch =
    match Map.tryFind model.locationId model.world with
    | None -> str "я заблудился"
    | Some location ->
        List.map (List.singleton >> section []) [
            h1 [] [ str location.name ]
            str location.description
            List.map (exit dispatch |> uncurry) location.exits |> p []
        ]   |> div [ Style [ TextAlign "center" ] ]


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
