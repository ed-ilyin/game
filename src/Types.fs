module Game.App.Types
type PlaceId = string
type ExitName = string
type ItemId = string

type Msg =
    Take of ItemId | Talk of ItemId | ChangePlace of (PlaceId * string)

type Description = string * ItemId * string * Msg

type Item = {
    id: string
    name: string
    description: Description
}

type Place = {
    id: PlaceId
    name: string
    background: string
    description: string
    items: ItemId list
    exits: (ExitName * PlaceId) list
}

type Model = {
    placeId: PlaceId
    places: Map<PlaceId,Place>
    items: Map<ItemId,Item>
    hands: ItemId list
    status: string
}

