module Game.App.Global
open Fable.Import
open Fable.Core
open Fable.Core.JsInterop

type DateStatic =
    [<Emit("new $0($1...)")>] abstract Create: unit -> JS.Date

let [<Global>] Date: DateStatic = jsNative
let mutable lastTouchEnd = 0.

do Browser.document.addEventListener ("touchend", !^(fun event ->
        let now = Date.Create().getTime()
        if (now - lastTouchEnd <= 500.)
            then do event.preventDefault ()
            else do lastTouchEnd <- now
    ), false
)
