import Settings from "./Settings";

export default interface DarkSoulsData {
    Settings: Settings;
    Connected: boolean;
    Loaded: boolean;
    Version: string;
    UpdatedEventFlags: number[];
    Char: CharData;
    
    //EventFlags
    Common: boolean[];
    AnorLondo: boolean[];
    Blighttown: boolean[];
    Catacombs: boolean[];
    ChasmOfTheAbyss: boolean[];
    CrystalCave: boolean[];
    DarkrootGarden: boolean[];
    DemonRuins: boolean[];
    Depths: boolean[];
    FirelinkShrine: boolean[];
    KilnOfTheFirstFlame: boolean[];
    LostIzalith: boolean[];
    NewLondoRuins: boolean[];
    OolacileTownship: boolean[];
    PaintedWorld: boolean[];
    QueelagsDomain: boolean[];
    RoyalWoods: boolean[];
    SanctuaryGarden: boolean[];
    SensFortress: boolean[];
    TombOfGiants: boolean[];
    UndeadAsylum: boolean[];
    UndeadBurg: boolean[];
    UndeadParish: boolean[];
    ValleyOfDrakes: boolean[];
}

export interface CharData {
    Area: string;
    AreaId: number;
    CharacterName: string;
    CharacterClass: string;
    CharacterClassId: number;
    LastBonfire: string;
    LastBonfireId: number;
    Health: number;
    HealthMax: number;
    Deaths: number;
    Clock: number;
    SaveSlot: number;
}