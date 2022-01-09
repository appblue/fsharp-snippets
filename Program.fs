open Avg
open UrlAsyc

[<EntryPoint>]
let main args =
    
    // #######################################
    // Avreager module with information hiding
    // #######################################
    let avg = makeAverager (fun (x:string) -> float x)

    for arg in args do
        avg.Record arg

    printfn $"{avg.Value}"
    
    [ @"http://google.com"; @"http://onet.pl" ]
        |> processUrlList
        |> Array.map printUrlInfo
        |> ignore

    // ##############################
    // sting <-> byte array encoding
    // ##############################
    let ex1 = "Hello"B
    printfn "%A" ( System.Text.Encoding.ASCII.GetString ex1)

    let ex2 = "Hello"
    printfn "%A" ( System.Text.Encoding.ASCII.GetBytes ex2 )

    0
