// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
let helpreport() =
    printfn "---------------------------------------------------------"
    printfn "--Survivor/Advocate Report on Outcomes of Help Requests--"
    printfn "---------------------------------------------------------"



[<EntryPoint>]
let main argv = 
    let hr = (helpreport())
    0 // return an integer exit code

