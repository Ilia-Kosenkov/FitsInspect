module Reader
open System.IO
open FSharp.Control
open System.Linq

let parseFile (fInfo : System.IO.Abstractions.IFileSystemInfo) =
    async {
        try
            use fileStream = new FileStream(fInfo.FullName, FileMode.Open, FileAccess.Read)
            use fitsReader = new FitsCs.FitsReader(fileStream)

            let enumer = fitsReader.EnumerateBlocksAsync().GetAsyncEnumerator()
            let! outerFlag = enumer.MoveNextAsync().AsTask() |> Async.AwaitTask
            let mutable condition = outerFlag
            let mutable counter = 1
            if condition then printfn "%sInspecting file %s%s" System.Environment.NewLine fInfo.Name System.Environment.NewLine
            while condition do 
                printfn "---------%sBlock #%i:%s---------"  System.Environment.NewLine counter System.Environment.NewLine
                enumer.Current.Keys |> Seq.iteri (fun ind k -> printfn "%04i > %s" (ind + 1) (k.ToString(true)))
                let! flag = enumer.MoveNextAsync().AsTask() |> Async.AwaitTask
                condition <- flag
                counter <- counter + 1
            return Ok ()
        with 
            | _ -> return Error fInfo.FullName
    }

let buildKeySequence (i : int) (keys : System.Collections.Immutable.ImmutableList<FitsCs.IFitsValue>) =
    let init = [sprintf "---------%sBlock #%i:%s---------"  System.Environment.NewLine (i + 1) System.Environment.NewLine]
    init @ 
        (keys 
            |> Seq.mapi (fun ind k -> sprintf "%04i > %s" (ind + 1) (k.ToString(true))) 
            |> Seq.toList)

let printOutKeys (fInfo : System.IO.Abstractions.IFileSystemInfo) =
    async {
        try
            use fileStream = new FileStream(fInfo.FullName, FileMode.Open, FileAccess.Read)
            use fitsReader = new FitsCs.FitsReader(fileStream)
            printfn "%sInspecting file %s%s" System.Environment.NewLine fInfo.Name System.Environment.NewLine
            let! _ = 
                fitsReader
                    .EnumerateBlocksAsync()
                    .SelectMany((fun block i -> buildKeySequence i block.Keys |> System.Linq.AsyncEnumerable.ToAsyncEnumerable))
                    .ForEachAsync(printfn "%s")
                    |> Async.AwaitTask

            return Ok ()
        with
            | _ -> return Error fInfo.FullName

    }

let readFiles fileInfos =
    async {
        let! results = 
            fileInfos 
            |> Seq.distinctBy (fun (y : System.IO.Abstractions.IFileSystemInfo) -> y.FullName)
            |> AsyncSeq.ofSeq
            |> AsyncSeq.mapAsync printOutKeys
            |> AsyncSeq.toListAsync
        
        
        return seq {
            for result in results do
                match result with
                | Error e -> yield e
                | _ -> ()
        }
    }
        
    
