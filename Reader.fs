module Reader

let parseFile file =
    ()

let readFiles files =
    files 
        |> Seq.map parseFile
        |> ignore
    0
