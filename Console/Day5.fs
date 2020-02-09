module Console.Day5

open System
open System.IO

let Run() =
    let input = File.ReadAllLines("Day5/input").[0].Split ',' |> Array.map int

    let mutable cursor = 0
    let mutable complete = false

    while (cursor < input.Length && not complete) do
        let d = input.[cursor]
        let opcode = d % 100
        let modeP1 = (d / 100) % 10
        let modeP2 = (d / 1000) % 10
        let modeP3 = (d / 10000) % 10

        match opcode with
        | 1
        | 2
        | 7
        | 8 ->
            let mutable left = 0
            let mutable right = 0
            let mutable target = 0

            if (modeP1 = 0) then left <- input.[input.[cursor + 1]]
            else left <- input.[cursor + 1]

            if (modeP2 = 0) then right <- input.[input.[cursor + 2]]
            else right <- input.[cursor + 2]

            if (modeP3 = 0) then target <- input.[cursor + 3]
            else target <- (cursor + 3)

            match opcode with
            // add
            | 1 ->
                printfn "%d | add(%d, %d, %d): %d" cursor left right input.[cursor + 3] (left + right)
                input.[target] <- left + right

            // multiply
            | 2 ->
                printfn "%d | add(%d, %d, %d): %d" cursor left right input.[cursor + 3] (left * right)
                input.[target] <- left * right

            // less-than
            | 7 ->
                printfn "%d | less-than(%d, %d, %d): %b" cursor left right input.[cursor + 3] (left < right)
                input.[target] <- if (left < right) then 1
                                  else 0

            // equals
            | 8 ->
                printfn "%d | equals(%d, %d, %d): %b" cursor left right input.[cursor + 3] (left = right)
                input.[target] <- if (left = right) then 1
                                  else 0

            cursor <- cursor + 4

        | 5
        | 6 ->
            let mutable value = 0
            let mutable jumpTo = 0

            if (modeP1 = 0) then value <- input.[input.[cursor + 1]]
            else value <- input.[cursor + 1]

            if (modeP2 = 0) then jumpTo <- input.[input.[cursor + 2]]
            else jumpTo <- input.[cursor + 2]

            match opcode with
            // jump-if-true
            | 5 ->
                printfn "%d | jump-if-true(%d, %d): %b" cursor value jumpTo (not (value = 0))
                cursor <-
                    if (not (value = 0)) then jumpTo
                    else (cursor + 3)

            // jump-if-false
            | 6 ->
                printfn "%d | jump-if-false(%d, %d): %b" cursor value jumpTo (value = 0)
                cursor <-
                    if (value = 0) then jumpTo
                    else (cursor + 3)

        // save-at
        | 3 ->
            printf "Input for position %d: " cursor
            let read = Console.ReadLine()
            let value = read |> int
            let dest = input.[cursor + 1]

            printfn "%d | save-at(%d): %d" cursor dest value

            input.[dest] <- value
            cursor <- cursor + 2

        // output-from
        | 4 ->
            printfn "%d | output-from(%d): %d" cursor input.[cursor + 1] input.[input.[cursor + 1]]
            cursor <- cursor + 2

        | 99 ->
            complete <- true

        | _ ->
            printfn "%s" (String.Join(",", input))
            failwithf "Unexpected opcode: %d" opcode

    0
