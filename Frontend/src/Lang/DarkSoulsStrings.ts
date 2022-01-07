import LocalizedStrings, { LocalizedStringsMethods } from "react-localization";
import { en } from "./en";

export interface IStrings extends LocalizedStringsMethods, IDarkSoulsStrings {}

export interface IDarkSoulsStrings {
    Labels: {
        Shutdown: string;
        Apply: string;
        ConnectingService: string;
        ConnectingGame: string;
        LoadSaveGame: string;
        Settings: string;
        Events: string;
        UpdateInterval: string;
        CompareEventFlags: string;
        Ok: string;
        EventFlagWarning: string;
        WarningHeadline: string;
        SettingsUpdated: string;
    },
    Errors: {
        NoJson: string;
        Connection: string;
        Shutdown: string;
        UpdateSettings: string;
    },
    Areas: {
        AnorLondo: {
            Name: string;
            MainHallDoor: string;
        };
    }
}

export const strings: IStrings = new LocalizedStrings({
    en: en,
});