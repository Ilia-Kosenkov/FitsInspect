module Reader
open System.IO
open FSharp.Control

let parseFile (fInfo : System.IO.Abstractions.IFileSystemInfo) =
    async {
        use fileStream = new FileStream(fInfo.FullName, FileMode.Open, FileAccess.Read)
        use fitsReader = new FitsCs.FitsReader(fileStream)

        let enumer = fitsReader.EnumerateBlocksAsync().GetAsyncEnumerator()
        try
            let! outerFlag = enumer.MoveNextAsync().AsTask() |> Async.AwaitTask
            let mutable condition = outerFlag
            let mutable counter = 1
            if condition then printfn "%sInspecting file %s%s" System.Environment.NewLine fInfo.Name System.Environment.NewLine
            while condition do 
                printfn "---------%sBlock #%i:%s---------"  System.Environment.NewLine counter System.Environment.NewLine
                for key in enumer.Current.Keys do printfn "%s" (key.ToString(true))
                let! flag = enumer.MoveNextAsync().AsTask() |> Async.AwaitTask
                condition <- flag
                counter <- counter + 1
            return Ok ()
        with 
            | _ -> return Error fInfo.FullName
    }

let readFiles fileInfos =
    async {
        let! results = 
            fileInfos 
            |> AsyncSeq.ofSeq
            |> AsyncSeq.mapAsync parseFile
            |> AsyncSeq.toListAsync
        
        
        return seq {
            for result in results do
                match result with
                | Error e -> yield e
                | _ -> ()
        }
    }
        
    
