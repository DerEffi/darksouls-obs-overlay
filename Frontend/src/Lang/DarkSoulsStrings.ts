import LocalizedStrings, { LocalizedStringsMethods } from "react-localization";
import { en } from "./en";

export interface IStrings extends LocalizedStringsMethods, IDarkSoulsStrings {}

export interface IDarkSoulsStrings {
    Labels: {
        Shutdown: string;
        Apply: string;
        ConnectingService: string;
        ConnectingGame: string;
    },
    Errors: {
        NoJson: string;
        ConnectionError: string;
        ShutdownError: string;
    }
}

export const strings: IStrings = new LocalizedStrings({
    en: en,
});