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
        | CharityMedicalDentalClinic

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
        | SocialWorkerFollowup of Help
        | CallerSelfDirected of Help

    and Help =
        | Helped //fully helped with everything caller needed
        | ExhaustedOptions //exhausted resources and still not helped
        | HelpFail // i.e. caller offered smoking cessation counseling when caller needed sex trafficking aftercare
        | DeniedHelp of Followup
        | GivenReferral of ReferredToNextNgo

    and ReferredToNextNgo = ReferredToNextNgo of Followup * Ngo

    type PoliceDisp =
        | VictimRescued
        | CopsNoHelp of Followup

    type CallOutcome =
        | Helped
        | EmergencyResponse of PoliceDisp
        | NotHelped of Followup
        | Referred of ReferredToNextNgo

    type Call = Call of Caller * CallerRequest * CallOutcome


