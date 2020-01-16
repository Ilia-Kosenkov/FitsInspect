module Reader

let parseFile file =
    ()

let enumerateFiles files =
    files 
        |> List.map parseFile
        |> ignore
    0
