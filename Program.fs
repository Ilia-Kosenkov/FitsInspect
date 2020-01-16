
open CommandLine
open Options
open Reader
open Ganss.IO


let enumerateFiles path =
    Glob.Expand path

let procssPaths (opts : ParserResult<CmdOptions>) =
    match opts with
    | :? Parsed<CmdOptions> as parsedOpts -> 
        seq {for path in parsedOpts.Value.Files do yield! enumerateFiles path} |>
            readFiles
    | _ -> -1



[<EntryPoint>]
let main argv =
    argv 
        |> Parser.Default.ParseArguments<CmdOptions>
        |> procssPaths
        |> ignore
    0
