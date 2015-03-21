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
        | _ -> printfn "Invalid Entry"
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
                    | _ -> printfn "Invalid Entry"
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
                    | _ -> printfn "Invalid Entry"
                           None
                match t with
                | None -> unmetneeds(u)
                | Some(y) -> unmetneeds(u.Add(y))
        unmetneeds(new Set<UnmetNeeds> ([]))