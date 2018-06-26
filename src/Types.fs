module Game.App.Types
type LocationId = string
type ExitName = string

type Location = {
    id: LocationId
    name: string
    description: string
    exits: (ExitName * LocationId) list
}

type Model = {
    locationId: LocationId
    world: Map<LocationId,Location>
}

type Msg = ChangeLocationId of LocationId
