namespace UrlAsyc

[<AutoOpen>]
module Utils =

    open System
    open System.IO
    open System.Net.Http

    let getURLAsync (url: string) =
        async {
            let uri = System.Uri(url)
            let client = new HttpClient()
            let html = client.GetAsync(uri)
            let contents = html.Result

            // get UTF8 scring from the byte array
            let utfs = contents.Content.ReadAsByteArrayAsync () 
                    |> Async.AwaitTask 
                    |> Async.RunSynchronously 
                    |> System.Text.Encoding.UTF8.GetString 
            
            // read the stream -> the sequence of strings
            let sres = seq { 
                use sr = new StreamReader (contents.Content.ReadAsStream ())
                while not sr.EndOfStream do
                    yield sr.ReadLine ()
            }
            // converting the sequence of strings -> string
            let nonutfs = sres |> Seq.concat |> Seq.toArray |> System.String
            return url, utfs, nonutfs
        }

    let processUrlList u =
        u
        |> Seq.map getURLAsync
        |> Async.Parallel
        |> Async.RunSynchronously

    let printUrlInfo (l: string * string * String) =
        match l with 
        | url, utfs, nonutfs ->
                printfn $"async value length for {url}: {utfs.Length}, {nonutfs.Length}"
                printfn "UTF8 encoded    : %A" (utfs |> Seq.toArray |> Array.take 32 |> System.String )
                printfn "non-UTF8 encoded: %A" (nonutfs |> Seq.toArray |> Array.take 32 |> System.String )