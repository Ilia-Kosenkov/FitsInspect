module Options

open CommandLine

type CmdOptions = {
    [<Value(0, Min = 1, HelpText = "List of files to process")>] Files : string seq;
}