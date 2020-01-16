module Reader

let parseFile (fInfo : System.IO.Abstractions.IFileSystemInfo) =
    printfn "%s" fInfo.FullName
    async {
        return
            match fInfo.FullName with
            | s when s.Length > 10 -> Ok ()
            | _ -> Error fInfo
        
    }

let readFiles fileInfos =
    async {
        let! results = 
            fileInfos 
            |> Seq.map parseFile 
            |> Async.Sequential 
        
        
        return seq {
            for result in results do
                match result with
                | Error e -> yield e
                | _ -> ()
        }
    }
        
    
