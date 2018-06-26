module Game.App.World
open Types

let item name description =
    {   id = name
        name = name
        description = description
    }

let place name background description items exits =
    {   id = name
        name = name
        background = background
        description = description
        items = items
        exits = exits
    }

let places =
    [   place "Спуск к морю" "hsl(60,60%,20%)"
            "Стопы утопают в песке пляжа. Шум волн заполняет всё \
            вокруг. Пахнет морем."
            []
            [   "вдоль моря налево", "Пляж с ракушками"
                "вдоль моря направо", "Пляж с камушками"
                "в дюны", "Дюны"
            ]
        place "Пляж с ракушками" "hsl(200,60%,20%)"
            "Море простирается до самого горизонта."
            [ "ракушка" ]
            [   "в море по колено", "Море по колено"
                "вдоль моря направо", "Спуск к морю"
            ]
        place "Пляж с камушками" "hsl(180,60%,20%)"
            "На севере море."
            []
            [ "вдоль моря налево", "Спуск к морю" ]
        place "Дюны" "hsl(340,60%,20%)"
            "Сосны, дюны, редкая трава, а в песке сухие палочки и шишки"
            []
            [ "вниз к морю", "Спуск к морю" ]
        place "Море по колено" "hsl(200,60%,10%)"
            "Вода очень освежающая. Ой тут плавает дельфин."
            []
            [ "назад на пляж с ракушками", "Пляж с ракушками" ]
    ]   |> List.map (fun place -> place.id, place)
        |> Map.ofList

let items =
    [   item "ракушка" ("Повсюду разбросаны ", "ракушки", ".", Take "ракушка")
    ]   |> List.map (fun item -> item.id, item)
        |> Map.ofList
