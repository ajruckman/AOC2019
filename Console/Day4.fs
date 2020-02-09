module Console.Day4

open System.Collections.Generic
open Microsoft.FSharp.Primitives.Basics

let Run1() =

    let mutable valid = 0

    for i in 172851 .. 675869 do
        let str = i |> string
        let mutable lst = -1

        let mutable anyLess = false
        let mutable anyDouble = false

        //        printfn "%d" i

        for chr in str do
            let t = (chr |> string) |> int
            //            printfn "\t%d" t

            if t = lst then
                //                printfn "Double: %d | %d -> %d" i lst t
                anyDouble <- true

            if t < lst then
                //                printfn "Less:   %d | %d -> %d" i lst t
                anyLess <- true

            lst <- t

        if not anyLess && anyDouble then
            printfn "Valid: %d" i
            valid <- valid + 1

    printfn ""
    printfn "---"
    printfn ""

    printfn "Valid: %d" valid

    0

let Run2() =
    let mutable valid = 0

    for i in 172851 .. 675869 do
        let str = i |> string

        let mutable lst = -1
        let mutable consecutive = 0

        let mutable anyLess = false

        let groups = List<int>()

        for chr in str do
            let t = (chr |> string) |> int

            if t < lst then anyLess <- true

            if t = lst then
                consecutive <- consecutive + 1
            else if consecutive > 0 then
                groups.Add(consecutive + 1)
                consecutive <- 0

            lst <- t

        if consecutive > 0 then groups.Add(consecutive + 1)

        if not anyLess then
            if groups.Contains 2 then
                printfn "Valid: %d %A" i groups
                valid <- valid + 1

    printfn ""
    printfn "---"
    printfn ""

    printfn "Valid: %d" valid

    0
