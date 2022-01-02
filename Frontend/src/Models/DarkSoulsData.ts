import Settings from "./Settings";

export default interface DarkSoulsData {
    Settings: Settings;
    Connected: boolean;
    Loaded: boolean;
    Version: string;
    CharacterName: string;
    CharacterClass: string;
    CharacterClassId: number;
    Health: number;
    HealthMax: number;
    LastBonfire: string;
    LastBonfireId: number;
    Area: string;
    AreaId: number;
    Deaths: number;
    Clock: number;
    UndeadAsylum: UndeadAsylumData | null;
    FirelinkShrine: FirelinkShrineData | null;
}

export interface UndeadAsylumData {
    PreOscarFog: boolean;
    CellDoor: boolean;
    F2WestDoor: boolean;
    ShortcutDoor: boolean;
    F2EastDoor: boolean;
    BigPilgrimDoor: boolean;
    OscarTrapSprung: boolean;
    AsylumDeamon: boolean;
}

export interface FirelinkShrineData {
    TalismanChest: boolean;
    HomewardBoneChest: boolean;
    CrackedRedEyeOrbChest: boolean;
    LloydTalismanChest: boolean;
}