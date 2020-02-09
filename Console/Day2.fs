module Console.Day2

let Run1 (input: int[]) (noun: int) (verb: int) =
    input.[1] <- noun
    input.[2] <- verb

    let mutable cursor = 0
    let mutable complete = false

    while (cursor < input.Length && not complete) do
        let code = input.[cursor]

        match code with
        | 1 ->
            let left = input.[input.[cursor + 1]]
            let right = input.[input.[cursor + 2]]
            let sum = left + right
//            printfn "%d %d,%d | %d => %d->%d + %d->%d = %d" noun verb cursor input.[cursor + 3] input.[cursor + 1] left
//                input.[cursor + 2] right sum

            input.[input.[cursor + 3]] <- sum
            cursor <- cursor + 4

        | 2 ->
            let left = input.[input.[cursor + 1]]
            let right = input.[input.[cursor + 2]]
            let product = left * right
//            printfn "%d %d,%d | %d => %d->%d * %d->%d = %d" noun verb cursor input.[cursor + 3] input.[cursor + 1] left
//                input.[cursor + 2] right product

            input.[input.[cursor + 3]] <- product
            cursor <- cursor + 4

        | 99 ->
//            let res = input.[0]
//            printfn "---"
//            printfn "Value at 0: %d" res
//            printfn "Final sequence: %s"
//                (input
//                 |> Array.map string
//                 |> String.concat ", ")

            complete <- true

    input.[0]

let Run2 (input: int[]) =
    let mutable complete = false
    while (not complete) do
        for x in 0 .. 100 do
            for y in 0 .. 100 do
                try
                    let res = Run1 (Array.copy input) x y
                    if (res = 19690720) then
                        printfn "Solution: %d %d %d" x y (100 * x + y)
                        complete <- true

                with e ->
                    printfn "Error: %d,%d %s" x y e.Message

    0
