// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
open System 
open System.IO
open System.Data
open System.Data.SqlClient
open System.Data.Linq
open Microsoft.FSharp.Data
open Newtonsoft.Json
open Agencies.Types
open Agencies.TerminalBuilder


let helpreport() =
    printfn "---------------------------------------------------------"
    printfn "--Survivor/Advocate Report on Outcomes of Help Requests--"
    printfn "---------------------------------------------------------"



[<EntryPoint>]
let main argv =
    printfn "Your outcome in seeking help has been recorded \r"
    printfn "and your anonymity has been protected"
    let hr = (helpreport())
    0 // return an integer exit code

