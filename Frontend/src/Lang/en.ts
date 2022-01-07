import { IDarkSoulsStrings } from "./DarkSoulsStrings";

export const en: IDarkSoulsStrings = {
    Labels: {
        Shutdown: "Shutdown",
        Apply: "Apply Changes",
        ConnectingService: "Connecting to background service...",
        ConnectingGame: "Searching for DarkSouls...",
        LoadSaveGame: "Please load or start a game...",
        Settings: "Settings",
        Events: "Events",
        UpdateInterval: "Update Interval",
        CompareEventFlags: "Show all Events",
        Ok: "Ok",
        EventFlagWarning: "This feature queries all DarkSouls. Depending on your system it could lead to performance hits. Only turn this feature on, if you want to find new Event-Flags in the game. The Changing events are also logged to '%appdata%/DerEffi/DarkSoulsOBSOverlay' if you want to search them later.",
        WarningHeadline: "Warning",
        SettingsUpdated: "Settings updated"
    },
    Errors: {
        NoJson: "Received data was in the wrong format",
        Connection: "Error trying to reach background service",
        Shutdown: "Error trying to shutdown application",
        UpdateSettings: "Error updating settings"
    },
    Areas: {
        AnorLondo: {
            Name: "Anor Londo",
            MainHallDoor: "Opened door to the main hall"
        }
    }
}