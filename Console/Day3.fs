module Console.Day3

open System
open System.Collections.Generic
open System.IO

let Run1() =
    let inputLines = File.ReadAllLines("Day3/input")

    let path1 = inputLines.[0].Split ","
    let path2 = inputLines.[1].Split ","

    let lines = Dictionary<Tuple<int, int>, char>()

    let mutable x, y = 0, 0

    let inline mark _x _y =
        //        printfn "Move: %d %d" _x _y
        if _x <> 0 || _y <> 0 then lines.[(_x, _y)] <- 'a'

    for path in path1 do
        let dir = path.[0]
        let dist = path.[1..] |> int

        match dir with
        | 'U' ->
            for i in y .. y + dist do
                mark x i

            y <- y + dist

        | 'D' ->
            for i in y .. -1 .. y - dist do
                mark x i

            y <- y - dist

        | 'R' ->
            for i in x .. x + dist do
                mark i y

            x <- x + dist

        | 'L' ->
            for i in x .. -1 .. x - dist do
                mark i y

            x <- x - dist

        | _ ->
            failwithf "Unknown direction: %c" dir

        printfn "%c %d -> %d, %d" dir dist x y

    /////

    printfn ""
    printfn "---"
    printfn ""

    x <- 0
    y <- 0

    let mutable collisions = new List<Tuple<int, int>>()

    let inline markAndCheck _x _y =
        //        printfn "Move: %d %d" _x _y

        if _x <> 0 || _y <> 0 then
            if lines.ContainsKey((_x, _y)) then
                if not (collisions.Contains((_x, _y))) then
                    let existing = lines.[(_x, _y)]
                    printf "-> Exists: %c; " existing

                    if existing = 'a' then
                        lines.[(_x, _y)] <- 'b'
                        printfn "New: b"
                        collisions.Add((_x, _y))
                    else if existing = 'b' then
                        lines.[(_x, _y)] <- 'c'
                        printfn "New: c"
                    else
                        printfn "Skipping."

            else
                lines.[(_x, _y)] <- 'b'


    for path in path2 do
        let dir = path.[0]
        let dist = path.[1..] |> int

        match dir with
        | 'U' ->
            for i in y .. y + dist do
                markAndCheck x i

            y <- y + dist

        | 'D' ->
            for i in y .. -1 .. y - dist do
                markAndCheck x i

            y <- y - dist

        | 'R' ->
            for i in x .. x + dist do
                markAndCheck i y

            x <- x + dist

        | 'L' ->
            for i in x .. -1 .. x - dist do
                markAndCheck i y

            x <- x - dist

        | _ ->
            printfn "Fail: %c" dir
            failwithf "Unknown direction: %c" dir

        printfn "%c %d -> %d, %d" dir dist x y

    printfn "Collisions: %s"
        ([| for c in collisions do
                yield sprintf "%d,%d" (fst c) (snd c) |]
         |> String.concat "; ")

    /////

    printfn ""
    printfn "---"
    printfn ""

    let mutable closest = collisions.[0]
    let mutable smallest = abs (fst closest) + abs (snd closest)

    for collision in collisions do
        let dist = abs (fst collision) + abs (snd collision)
        printfn "(%d, %d) -> %d" (fst collision) (snd collision) dist
        if dist < smallest then
            closest <- collision
            smallest <- dist

    printfn ""
    printfn "---"
    printfn ""

    printfn "%A -> %d" closest smallest

    0

type P (x: int, y: int) =
    let mutable seenA = false
    let mutable seenB = false
    
    let mutable distA : Nullable<int> = Nullable()
    
    let mutable distB : Nullable<int> = Nullable()
    
    member this.X = x
    member this.Y = y
    
    member this.SetA (dA: int) =
        seenA <- true
        if distA.HasValue then
            dA |> ignore
            if dA < distA.Value then
                distA <- Nullable dA
        else
            distA <- Nullable dA
 
    member this.SetB (dB: int) =
        seenB <- true
        if distB.HasValue then
            dB |> ignore
            if dB < distB.Value then
                distB <- Nullable dB
        else
            distB <- Nullable dB
    
    member this.Crossed = seenA && seenB
    member this.DistA = distA
    member this.DistB = distB
        

let Run2() =
    let inputLines = File.ReadAllLines("Day3/input")

    let path1 = inputLines.[0].Split ","
    let path2 = inputLines.[1].Split ","

    let points = Dictionary<Tuple<int, int>, P>()

    let mutable x, y, dA, dB = 0, 0, 0, 0
    let mutable first = true

    let inline mark _x _y _d =
        if first then
            first <- false
        else
            dA <- dA + 1
//        printfn "Move: %d %d ===== %d" _x _y dA
        if _x <> 0 || _y <> 0 then
            if not (points.ContainsKey((_x, _y))) then
                points.[(_x, _y)] <- P(_x, _y)
            points.[(_x, _y)].SetA dA
            
    for path in path1 do
        let dir = path.[0]
        let dist = path.[1..] |> int

        match dir with
        | 'U' ->
            for i in y .. y + dist do
                mark x i dA

            y <- y + dist

        | 'D' ->
            for i in y .. -1 .. y - dist do
                mark x i dA

            y <- y - dist

        | 'R' ->
            for i in x .. x + dist do
                mark i y dA

            x <- x + dist

        | 'L' ->
            for i in x .. -1 .. x - dist do
                mark i y dA

            x <- x - dist

        | _ ->
            failwithf "Unknown direction: %c" dir

//        printfn "%c %d -> %d, %d @ %d" dir dist x y dA
        first <- true

    /////

    printfn ""
    printfn "--- End A"
    printfn ""

    x <- 0
    y <- 0
    first <- true

    let inline markAndCheck _x _y _d =
        if first then
            first <- false
        else
            dB <- dB + 1
//        printfn "Move: %d %d ===== %d" _x _y dB

        if _x <> 0 || _y <> 0 then
            if not (points.ContainsKey((_x, _y))) then
                points.[(_x, _y)] <- P(_x, _y)
            
            points.[(_x, _y)].SetB dB

    for path in path2 do
        let dir = path.[0]
        let dist = path.[1..] |> int

        match dir with
        | 'U' ->
            for i in y .. y + dist do
                markAndCheck x i dB

            y <- y + dist

        | 'D' ->
            for i in y .. -1 .. y - dist do
                markAndCheck x i dB

            y <- y - dist

        | 'R' ->
            for i in x .. x + dist do
                markAndCheck i y dB

            x <- x + dist

        | 'L' ->
            for i in x .. -1 .. x - dist do
                markAndCheck i y dB

            x <- x - dist

        | _ ->
            printfn "Fail: %c" dir
            failwithf "Unknown direction: %c" dir

//        printfn "%c %d -> %d, %d @ %d" dir dist x y dB
        first <- true

    /////

    printfn ""
    printfn "--- End B"
    printfn ""
    
    let mutable shortest = 100000
    let mutable point = P(0, 0)
    
    for KeyValue(k, v) in points do
        if v.Crossed then
            let dist = v.DistA.Value + v.DistB.Value
            printfn "%d,%d -> %d" v.X v.Y dist
            
            if dist < shortest then
                shortest <- dist
                point <- v
                
        

//    let mutable closest = collisions.[0]
//    let mutable smallest = abs closest.X + abs closest.Y
//
//    for collision in collisions do
//        let dist = abs collision.X + abs collision.Y
//        printfn "(%d, %d) -> %d" collision.X collision.Y dist
//        if dist < smallest then
//            closest <- collision
//            smallest <- dist

    printfn ""
    printfn "---"
    printfn ""

//    printfn "%d,%d @ %d,%d -> %d" closest.X closest.Y closest.DA closest.DB smallest

    printfn "%d,%d @ %d,%d -> %d" point.X point.Y point.DistA.Value point.DistB.Value shortest

    0
