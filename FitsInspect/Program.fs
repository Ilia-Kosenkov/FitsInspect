
open CommandLine
open Options
open Reader
open Ganss.IO


let enumerateFiles path =
    Glob.Expand path

let reportErrors (fileInfos : Async<seq<string>>) =
    async{
        let! awaitedFinfos = fileInfos
        return 
            match awaitedFinfos with
            | sequence when Seq.isEmpty sequence |> not -> 
                printfn "%sThe following files failed:" System.Environment.NewLine
                for item in sequence do printfn "%s" item
                -2
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
