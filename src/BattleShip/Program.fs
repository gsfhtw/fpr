﻿[<EntryPoint>]
let main argv =

    System.Console.Clear()

    printfn ""
    printfn "   ================================="
    printfn "   Welcome to BattleShip!"
    printfn "   Press CTRL+C to stop the program."
    printfn "   ================================="
    printfn ""

    let game = DomainFunctions.initNewGame 5

    printfn ""
    printfn "   Please choose where you want to put your ships"
    printfn "   (e.g.: Set A1)"
    printfn ""
    ConsoleHelper.drawHumanBoard game
    printfn ""

    Repl.loop game
    0 // return an integer exit code
