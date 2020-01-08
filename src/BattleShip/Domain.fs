module Domain


type FieldShipStatus =
    | Water
    | Ship
    | DestroyedShip

type FieldAttemptStatus =
    | NotAttempted
    | Attempted

type Coord =
    { X: char
      Y: int }

// field is one of field on the 5x5 board
type Field =
    { Coord: Coord
      AttemptStatus: FieldAttemptStatus
      ShipStatus: FieldShipStatus }

type Board =
    { Fields: Field list
      Size: int }

// Get character Range for provided board
let getCharacterRange (length: int): List<char> = [ 'A' .. 'Z' ].[0..(length - 1)]

// Return a list of all Fields with FieldAttemptStatus = NotAttempted
let getNotAttemptedFieldsForBoard (board: Board): List<Field> =
    board.Fields |> List.filter (fun field -> field.AttemptStatus = NotAttempted)

// Add a new ShipPoint at the provided Coord to the provided Board
let addShipPointAtCoordToBoard (coord: Coord, board: Board): Board =
    let newFields =
        board.Fields
        |> List.map (fun field ->
            match field.Coord with
            | { X = xValue; Y = yValue } when xValue = coord.X && yValue = coord.Y ->
                match field.ShipStatus with
                | Water ->
                    printfn ("New ship at %c%d") coord.X coord.Y
                    { field with ShipStatus = Ship }
                | _ -> field
            | _ -> field)
    printfn "%A" newFields
    { board with Fields = newFields }

type Player =
    | Human
    | Computer

type GameStatus =
    | WonBy of Player
    | Running
    | SetupShips of int

type Game =
    { HumanBoard: Board
      ComputerBoard: Board
      Status: GameStatus
      Size: int }

// Check if the provided coordinate is even on the board
let isValidGameCoord (game: Game, coord: Coord): bool =
    let isValidBoardCharacter = getCharacterRange game.Size |> List.contains coord.X
    let isValidBoardInteger = coord.Y >= 1 && coord.Y <= game.Size
    isValidBoardInteger && isValidBoardCharacter

// Message is a command entered by the user
type Message =
    | Set of Coord
    | Try of Coord
    | ShowShips
