module Console.Day1

open System.IO

let MassToFuel(mass: float) =
    let fuel = (floor (float mass / float 3) - float 2)
    fuel

let MassTotalFuel(mass: float) =
    let mutable fuel = MassToFuel mass
    let mutable sum = fuel

    while fuel > float 0 do
        printfn "%f -> %f ++ %f" mass fuel (MassToFuel fuel)
        fuel <- MassToFuel fuel
        if fuel > float 0 then sum <- sum + fuel

    sum

let Run1() =
    let inputLines = File.ReadLines("Day1/input")
    let mutable sum = float 0

    for line in inputLines do
        let mass = line |> float
        let fuel = MassToFuel mass
        sum <- sum + fuel

        printfn "%f -> %f : %f" mass fuel sum

    printfn "---"
    printfn "Question 1: %d" (int sum)

    0

let Run2() =
    let inputLines = File.ReadLines("Day1/input")
    let mutable sum = float 0

    for line in inputLines do
        let mass = line |> float
        let fuel = MassTotalFuel mass
        sum <- sum + fuel

        printfn "%f -> %f : %f" mass fuel sum

    printfn "---"
    printfn "Question 2: %d" (int sum)

    0
