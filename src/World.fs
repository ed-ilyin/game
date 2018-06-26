module Game.App.World
open Types

let item name description =
    {   id = name
        name = name
        description = description
    }

let place name description items exits =
    {   id = name
        name = name
        description = description
        items = items
        exits = exits
    }

let places =
    [   place "Спуск к морю"
            "Стопы утопают в песке пляжа. Шум волн заполняет всё \
            вокруг. Пахнет морем."
            []
            [   "вдоль моря налево", "Пляж с ракушками"
                "вдоль моря направо", "Пляж с камушками"
                "в дюны", "Дюны"
            ]
        place "Пляж с ракушками"
            "Море простирается до самого горизонта."
            [ "ракушка" ]
            [ "вдоль моря направо", "Спуск к морю" ]
        place "Пляж с камушками"
            "На севере море."
            []
            [ "вдоль моря налево", "Спуск к морю" ]
        place "Дюны"
            "Сосны, дюны, редкая трава, а в песке сухие палочки и шишки"
            []
            [ "вниз к морю", "Спуск к морю" ]
    ]   |> List.map (fun place -> place.id, place)
        |> Map.ofList

let items =
    [   item "ракушка" ("Повсюду разбросаны ", "ракушки", ".", Take "ракушка")
    ]   |> List.map (fun item -> item.id, item)
        |> Map.ofList
