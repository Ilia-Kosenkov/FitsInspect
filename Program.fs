
open CommandLine
open Options
open Reader
open Ganss.IO


let enumerateFiles path =
    Glob.Expand path

let writeFileName (fInfo : System.IO.Abstractions.IFileSystemInfo) =
    printfn "%s" fInfo.FullName

let reportErrors (fileInfos : Async<seq<System.IO.Abstractions.IFileSystemInfo>>) =
    printfn "The following files failed:" 
    async{
        let! awaitedFinfos = fileInfos
        return 
            match awaitedFinfos with
            | sequence when Seq.isEmpty sequence |> not -> sequence |> Seq.map writeFileName |> (fun _ -> -2)
            | _ -> 0
    }

let procssPaths (opts : ParserResult<CmdOptions>) =
    match opts with
    | :? Parsed<CmdOptions> as parsedOpts -> 
        seq {for path in parsedOpts.Value.Files do yield! enumerateFiles path} |>
            readFiles |>
            reportErrors
    | _ -> async {return -1}



[<EntryPoint>]
let main argv =
    argv 
        |> Parser.Default.ParseArguments<CmdOptions>
        |> procssPaths
        |> Async.RunSynchronously
