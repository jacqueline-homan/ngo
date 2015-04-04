namespace Agencies

module TerminalBuilder =

    open System
    open System.IO
    open Agencies.Types

    let rec caller():Caller =
        printfn "Are you reporting for yourself or as an advocate for someone else?"
        printfn " 1 for Advocate"
        printfn " 2 for Self"
        let answer = Console.ReadLine()
        match answer with 
        | "1" -> Advocate
        | "2" -> Client
        | _ -> printfn "INVALID ENTRY"
               caller()

    let specialNeeds():Set<Disabilities> =
        let rec spnds(s:Set<Disabilities>): Set<Disabilities> =
            printfn "Do you/the person on whose behalf you're reporting have a disability?"
            printfn " 1 for Learning Disability"
            printfn " 2 for Physical Disability"
            printfn " 3 for Mental Health Issues"
            printfn " 4 for Substance Addition Issues"
            printfn " 5 for Pregnancy-Related Limitations/Complications"
            printfn " 6 for Chronic Illness"
            printfn " 7 for Undiagnosed/Don't Know"
            printfn " Enter 'NO' for No Disabling Conditions"
            let ans = Console.ReadLine()
            match ans.Trim().ToLower() with
            | "no" -> s
            | _ -> 
                let sn =
                    match ans.Trim().ToLower() with
                    | "1" -> Some LearningDisabled
                    | "2" -> Some PhysicallyDisabled
                    | "3" -> Some MentalIllness
                    | "4" -> Some SubstanceAddiction
                    | "5" -> Some Pregnancy
                    | "6" -> Some ChronicIllness
                    | "7" -> Some Undiagnosed
                    | "exit" -> printfn "No disabling conditions"
                                None
                    | _ -> printfn "INVALID ENTRY"
                           None
                match sn with
                | None -> spnds(s)
                | Some(x) -> spnds(s.Add(x))
        spnds(new Set<Disabilities> ([]))

    let needs():Set<UnmetNeeds> =
        let rec unmetneeds(u:Set<UnmetNeeds>):Set<UnmetNeeds> =
            printfn "What following unmet needs did you seek help for?"
            printfn "=========================================================="
            printfn "               List of Unmet Needs                        "
            printfn "=========================================================="
            printfn "Trafficking Safehouse Bed, Domestic Violence Shelter Bed,"
            printfn "Housing, Clothes, Food, Legal, Dental, Medical, Vision,"
            printfn "Drug Rehab, Trauma Care, Psychiatric Care, Skills Training,"
            printfn "Education Help, Job Placement, Economic Support"
            printfn "-----------------------------------------------------------"
            printfn "\r"
            printfn "Enter one need at a time, press 'Enter' after each need."
            printfn "Type 'Done' and press 'Enter' when finished."
            let response = Console.ReadLine()
            match response.Trim().ToLower() with 
            | "done" -> u
            | _ ->
                let t =
                    match response.Trim().ToLower() with
                    | "trafficking safehouse bed" -> Some TraffickingSafebed
                    | "domestic violence shelter bed" -> Some DVsafebed
                    | "housing" -> Some Housing
                    | "clothes" -> Some Clothing
                    | "legal" -> Some Legal
                    | "dental" -> Some Dental
                    | "medical" -> Some Medical
                    | "vision" -> Some Vision
                    | "drug rehab" -> Some DrugRehab
                    | "trauma care" -> Some TraumaCare
                    | "psychiatric care" -> Some PsychiatricCare
                    | "skills training" -> Some SkillsTraining
                    | "economic support" -> Some EconomicSupport
                    | "job placement" -> Some JobPlacement
                    | _ -> printfn "INVALID ENTRY"
                           None
                match t with
                | None -> unmetneeds(u)
                | Some(y) -> unmetneeds(u.Add(y))
        unmetneeds(new Set<UnmetNeeds> ([]))  

    let rec callerRequest():CallerRequest =
        printfn "What kind of help did you need and ask for?"
        printfn "1 for Police Dispatch"
        printfn "2 for Sex Trafficking Survivor Aftercare"
        printfn "3 for Domestic Violence Survivor Services"
        printfn "4 for Poverty/Homelessness Relief"
        let answer = Console.ReadLine()
        match answer with
        | "1" -> PoliceDispatch
        | "2" -> TraffickingAftercare (needs())
        | "3" -> DVSupport (needs())
        | "4" -> PovertyRelief (needs())
        | _ -> printfn "INVALID ENTRY"
               callerRequest()

    let rec ngo():Ngo =
        printfn "What kind of NGO were you referred to?"
        printfn "1 for Homeless Shelter"
        printfn "2 for Domestic Violence Shelter"
        printfn "3 for Trafficking Survivor Residential Aftercare Program"
        printfn "4 for Trafficking Victim Safehouse"
        printfn "5 for Food Assistance"
        printfn "6 for Clothing Assistance"
        printfn "7 for Medical and Dental Care charity clinic"
        let answer = Console.ReadLine()
        printfn "Enter the name of the NGO you  were referred to: "
        let ans = Console.ReadLine()
        printfn "%s" ans
        match ans.Trim().ToLower() with
        | "1" -> Ngo(HomelessShelter, ans)
        | "2" -> Ngo(DVShelter, ans)
        | "3" -> Ngo(TraffickingSurvivorAftercare, ans)
        | "4" -> Ngo(TraffickingVictimSafehouse, ans)
        | "5" -> Ngo(FoodPantry, ans)
        | "7" -> Ngo(ClothingPantry, ans)
        | "8" -> Ngo(MedicalDentalClinic, ans)
        | "9" -> Ngo(LegalAid, ans)
        | _ -> printfn "INVALID ENTRY"
               ngo()

    let rec followup() =
        printfn "Did anyone follow up with you?"
        printfn "1 for NO (and I didn't know what else to do)"
        printfn "2 for YES (a caseworker/social worker said they would follow up"
        printfn "3 for Self-followup/No followup from caseworker/socialworker."
        let reply = Console.ReadLine()
        match reply with
        | "1" -> NoFollowup
        | "2" -> SocialWorker (help())
        | "3" -> SelfDirected (help())
        | _ -> printfn "INVALID ENTRY"
               followup()
    
    and help():Help =
        printfn "What help did you get?"
        printfn "1 if you got all the help you needed and requested"
        printfn "2 if you were not helped and all referrals were exhausted"
        printfn "3 if you were denied help and not offered a referral"
        printfn "4 if you were offered the WRONG help (i.e. offered drug rehab instead of shelter)"
        printfn "5 if you were not helped but given a referral to another NGO"
        let response = Console.ReadLine()
        match response with
        | "1" -> Helped
        | "2" -> ExhaustedOptions
        | "3" -> DeniedHelp (followup())
        | "4" -> HelpFail (followup())
        | "5" -> GivenReferral (ref())
        | _ -> printfn "INVALID ENTRY"
               help()

    and ref():RefToNextNgo =
        let n = ngo()
        let f = followup()
        RefToNextNgo( f, n)

    let rec police_disp():PoliceDisp =
        printfn "Did police help victim without arresting her/him(Y or N)?"
        let reply = Console.ReadLine().Trim().ToLower()
        match reply with
        | "Y" -> VictimRescued
        | "N" -> CopsNoHelp (followup())
        | _ -> printfn "INVALID ENTRY"
               police_disp()

    let firstngo() =
        printfn "Which NGO did you contact for help first?"
        let resp = Console.ReadLine()
        printfn "First NGO: "
      //  match resp.Trim().ToLower() with
       // | "%s" -> printfn "First NGO called: **%s**" resp
       // | _ ->
    let rec callOutcome():CallOutcome =
        printfn "Name of NGO: "
        let resp = Console.ReadLine().Trim().ToUpper()
        printfn "What help did %s provide?" resp
        printfn "1 for %s hotline operator dispatched 911" resp
        printfn "2 for %s provided aftercare to survivor" resp
        printfn "3 for %s referred me to another NGO" resp
        printfn "4 for %s denied me help/did not help at all" resp
        printfn "5 for call got disconnected and %s did not follow up" resp
        let re = Console.ReadLine()
        match re with
        |"1" -> EmergencyResponse (police_disp())
        |"2" -> GotHelped
        |"3" -> Referred (ref())
        |"4" -> NotHelped (followup())
        |"5" -> CallDrop (followup())
        | _ -> printfn "INVALID ENTRY"
               callOutcome()
        //    firstngo()

    
     
