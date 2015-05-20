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

let rec ngoinfo(Ngo(cat,name)) =
    match cat with 
    | HomelessShelter -> printfn "Homeless Shelter: %s" name
    | DVShelter -> printfn "Domestic Violence Shelter for Women and Children: %s" name
    | TraffickingSurvivorAftercare -> printfn "Sex Trafficking Survivor Aftercare: %s" name
    | TraffickingVictimSafehouse -> printfn "Sex Trafficking Victim Temp Safehouse: %s" name
    | FoodPantry -> printfn "Food Assistance: %s" name
    | ClothingPantry -> printfn "Donated Clothing Assistance: %s" name
    | MedicalDentalClinic -> printfn "Medical and/or Dental Care Help: %s" name
    | LegalAid -> printfn "Pro bono legal aid for trafficking victim arrest records: %s" name


let rec help(h:Help) =
    match h with
    | Helped -> printfn "Got all the help I needed"
    | ExhaustedOptions -> printfn "Exhausted all referrals and still not helped"
    | HelpFail (fu)-> printfn "Denied any help"
                      followupper(fu)
    | DeniedHelp (fu) -> printfn "Denied any help"
                         followupper(fu)
    | GivenReferral(crngo) -> printfn "Not helped but given referral to another NGO"
                              //ref(crngo)

and followupper(fu:Followup) =
    match fu with
    | NoFollowup -> printfn "No Followup"
    | SocialWorker (h) -> printfn "Social worker is following up/has followed up"
                          help(h)
    | SelfDirected (h) -> printfn "Self-reporting/No caseworker or NGO followup"
                          help(h)

let outcome_of_pol_disp(pd) =
    match pd with
    | VictimRescued -> printfn "Victim rescued and NOT arrested"
    | CopsNoHelp(fu) -> printfn "Cops No-Show/Cops Re-Victimize or Arrest Victim"
                        followupper(fu)


let gx(di:Set<Disabilities>) =
    Seq.iter(fun x ->
        match x with
        | Pregnancy -> printfn "Pregnant"
        | PhysicallyDisabled -> printfn "Physical Disability"
        | LearningDisabled -> printfn "Learning Disability"
        | MentalIllness -> printfn "Mental Health Problems"
        | SubstanceAddiction -> printfn "Addiction Issues"
        | ChronicIllness -> printfn "Chronically Ill (terminal illness, incurable STD's, etc.)"
        | Undiagnosed -> printfn "Don't Know/Not Diagnosed") (di)

let fx(un:Set<UnmetNeeds>) =
    Seq.iter(fun x ->
        match x with
        | TraffickingSafebed -> printfn "Sex Trafficking Victim Safehouse"
        | DVsafebed -> printfn "Domestic Violence Shelter Safebed"
        | Housing -> printfn "Housing"
        | Clothing -> printfn "Clothes"
        | Food -> printfn "Food Assistance"
        | Legal -> printfn "Legal Help"
        | Medical -> printfn "Medical Care"
        | Dental -> printfn "Dental Care"
        | Vision -> printfn "Vision Care"
        | DrugRehab -> printfn "Drug Rehab"
        | TraumaCare -> printfn "Trauma Therapy"
        | PsychiatricCare -> printfn "Psychiatric Care"
        | SkillsTraining -> printfn "Job Skills Training"
        | EducationHelp -> printfn "Education Assistance"
        | JobPlacement -> printfn "Employment"
        | EconomicSupport -> printfn "Economic Support") (un)


let callerReq(cr:CallerRequest) =
    match cr with
    | PoliceDispatch -> printfn "911 dispatch to rescue victim"
    | TraffickingAftercare(un) -> printfn "Trafficking Survivor Aftercare"
                                  fx(un) 
    | DVSupport(un) -> printfn "Domestic Violence Victim Assistance"
                       fx(un)
    | PovertyRelief(un) -> printfn "Economic Support for Poverty Relief"
                           fx(un)

let showreferral(RefToNextNgo(fu,n))=
    ngoinfo(n) 
    followupper(fu)

let callOutcome(co:CallOutcome) =
    match co with
    | GotHelped(n) -> printfn "Victim/Survivor/Client got helped"
                      ngoinfo(n)                      
    | EmergencyResponse (n, pd) -> printfn "911 dispatched to rescue trafficking victim"
                                   ngoinfo(n) 
                                   outcome_of_pol_disp(pd)
    | NotHelped (n,fu) -> printfn "Not helped and not referred to anyone that will help"
                          ngoinfo(n)
                          followupper(fu)
    | Referred (n, crngo) -> printfn "Not helped but given a referral to another NGO"
                             ngoinfo(n)
                             showreferral(crngo)
                         
    | CallDrop (n, fu) -> printfn "Hung up on/Call dropped"
                          ngoinfo(n)
                          followupper(fu)
    


let caller(c:Caller) =
    match c with
    | Client -> "Client/Victim/Survivor in need of help"
    | Advocate -> "Advocate for someone else seeking help"

let helpreport(Call(ca, cr, co)) =
    printfn "*********************************************************"
    printfn "* Survivor/Advocate Report on Outcomes of Help Requests *"
    printfn "*********************************************************"
    printfn "\r"

 //   caller(c)
 //   printfn "First NGO contacted for help was: %s" 
    callerReq(cr) 
    callOutcome(co)

//    firstngo(fngo)


[<EntryPoint>]
let main argv =
    let c = Agencies.TerminalBuilder.caller()
    let cr = (callerRequest())
   // let co = (callOutcome(CallOutcome()))
    let cd = (specialNeeds())
  //  let fn = Agencies.TerminalBuilder.firstngo()
    let co = Agencies.TerminalBuilder.callOutcome()
    let ca = Call(c,cr,co)
    printfn "Your outcome in seeking help has been recorded \r"
    printfn "and your anonymity has been protected"
    let hr = (helpreport(Call(c,cr, co)))


//    let firstngo(fngo) =
//        printfn "First NGO: %s" fngo
    0 // return an integer exit code

