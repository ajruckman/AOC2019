open System.IO
open Console

// Learn more about F# at http://fsharp.org

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

//    Day1.Run1() |> ignore
//    Day1.Run2() |> ignore
//
//    let inputStrings = File.ReadAllLines("Day2/input").[0].Split ','
//
//    let input =
//        [| for x in inputStrings do
//            let v = x |> int
//            yield v |]
//
//    Day2.Run1(input) |> ignore
//    Day2.Run2(input) |> ignore
//
//    Day3.Run1() |> ignore
//    Day3.Run2() |> ignore
//
//    Day4.Run1() |> ignore
//    Day4.Run2() |> ignore

//    Day5.Run1() |> ignore
//    Day5.Run2() |> ignore

    Day5.Run() |> ignore
    
    0 // return an integer exit code
