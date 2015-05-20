namespace Agencies

open System.Collections

module Types =

    type NgoType =
        | HomelessShelter
        | DVShelter
        | TraffickingSurvivorAftercare
        | TraffickingVictimSafehouse
        | FoodPantry
        | ClothingPantry
        | MedicalDentalClinic
        | LegalAid

    type Ngo = Ngo of NgoType * string

    type Caller =
        | Client
        | Advocate

    and CallerStatus =
        | VictimServicesAdvocate of Caller
        | DVvictim of Caller
        | SexTraffickingSurvivor of Caller
        | NaturalDisasterVictim of Caller
        | PovertyVictim of Caller

    and Disabilities =
        | Pregnancy
        | PhysicallyDisabled
        | LearningDisabled
        | MentalIllness
        | SubstanceAddiction
        | ChronicIllness
        | Undiagnosed

    and SpecialNeeds = SpecialNeeds of Set<Disabilities>

    type UnmetNeeds =
        | TraffickingSafebed
        | DVsafebed
        | Housing
        | Clothing
        | Food
        | Legal
        | Medical
        | Dental
        | Vision
        | DrugRehab
        | TraumaCare
        | PsychiatricCare
        | SkillsTraining
        | EducationHelp
        | JobPlacement
        | EconomicSupport

    type CallerRequest =
        | PoliceDispatch 
        | TraffickingAftercare of Set<UnmetNeeds>
        | DVSupport of Set<UnmetNeeds>
        | PovertyRelief of Set<UnmetNeeds>

    type Followup =
        | NoFollowup
        | SocialWorker of Help
        | SelfDirected of Help

    and Help =
        | Helped //fully helped with everything caller needed
        | ExhaustedOptions //exhausted resources and still not helped
        | HelpFail of Followup// i.e. caller offered smoking cessation counseling when caller needed sex trafficking aftercare
        | DeniedHelp of Followup //denied help, maybe due to discrimination
        | GivenReferral of RefToNextNgo

    and RefToNextNgo = RefToNextNgo of Followup * Ngo

    type PoliceDisp =
        | VictimRescued
        | CopsNoHelp of Followup

    type CallOutcome =
        | GotHelped of Ngo
        | EmergencyResponse of Ngo*PoliceDisp
        | NotHelped of Ngo*Followup
        | Referred of Ngo*RefToNextNgo
        | CallDrop of Ngo*Followup

    type Call = Call of Caller * CallerRequest * CallOutcome


