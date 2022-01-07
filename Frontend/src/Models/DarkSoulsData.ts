import Settings from "./Settings";

export default interface DarkSoulsData {
    Settings: Settings;
    Connected: boolean;
    Loaded: boolean;
    Version: string;
    UpdatedEventFlags: number[];
    Char: CharData;
    
    //EventFlags
    Areas: {
        Common: object;
        AnorLondo: object;
        Blighttown: object;
        Catacombs: object;
        ChasmOfTheAbyss: object;
        CrystalCave: object;
        DarkrootGarden: object;
        DemonRuins: object;
        Depths: object;
        FirelinkShrine: object;
        KilnOfTheFirstFlame: object;
        LostIzalith: object;
        NewLondoRuins: object;
        OolacileTownship: object;
        PaintedWorld: object;
        QueelagsDomain: object;
        RoyalWoods: object;
        SanctuaryGarden: object;
        SensFortress: object;
        TombOfGiants: object;
        UndeadAsylum: object;
        UndeadBurg: object;
        UndeadParish: object;
        ValleyOfDrakes: object;
    }
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