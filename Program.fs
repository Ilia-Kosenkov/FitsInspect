// Learn more about F# at http://fsharp.org

open System
open CommandLine
open Options

let enumerateFiles path =
    Seq.empty

let process (opts : ParserResult<CmdOptions>) =
    match opts with
    | :? Parsed<CmdOptions> as parsedOpts -> seq {for path in parsedOpts.Value.Files do yield! enumerateFiles path}
    | _ -> Seq.empty



[<EntryPoint>]
let main argv =
    argv 
        |> Parser.Default.ParseArguments<CmdOptions>
        |> process
        |> ignore
    0
