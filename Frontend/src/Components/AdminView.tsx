import React from "react";
import DarkSoulsData from "../Models/DarkSoulsData";
import Helper from "../Services/HelperService";

export interface AdminViewState {
    data: DarkSoulsData | null,
    connected: boolean
}

export default class AdminView extends React.Component<{}, AdminViewState> {

    private establishConnection: NodeJS.Timer;

    constructor(props: {}) {
        super(props);
        this.state = {
            data: null,
            connected: false
        };
        this.establishConnection = setInterval(() => this.connect(), 10000);
    }

    private connect() {
        let connection: WebSocket = new WebSocket(`ws://${window.location.host}/status/subscribe`);
		connection.onopen = () => {
            clearInterval(this.establishConnection);
            console.log("Connection established");
			this.setState({
                connected: true,
                data: null
            });
		}

		connection.onclose = () => {
            console.log("Connection closed");
			this.setState({
                connected: false,
                data: null
            });
            clearInterval(this.establishConnection);
            this.establishConnection = setInterval(() => this.connect(), 10000);
		}

		connection.onmessage = evt => {
			try {
                console.log("Received data");
                console.dir(JSON.parse(evt.data));
				let data: DarkSoulsData = JSON.parse(evt.data);
                this.setState({
                    data: data
                });
			} catch {
				console.error("No json object in message.");
			}
		}

		connection.onerror = err => {
			console.error(err);
		}
    }

    public render() {
        return(
            <div>
                
                <button onClick={() => {fetch("/close")}}>Exit App</button>

                {!this.state.connected &&
                    <div>Verbinde mit Backgroundservice...</div>
                }

                {this.state.connected && (this.state.data == null || !this.state.data.Connected) &&
                    <div>Suche nach Spiel...</div>
                }

                {this.state.connected && this.state.data != null && this.state.data.Connected &&
                    <table>
                        <thead></thead>
                        <tbody>
                            <tr>
                                <td>Verbunden mit</td>
                                <td>Dark Souls: Remastered - Version {this.state.data.Version || "0"}</td>
                            </tr>
                            <tr>
                                <td>Character</td>
                                <td>{this.state.data.CharacterName ? this.state.data.CharacterName + (this.state.data.CharacterClass ? ` (${this.state.data.CharacterClass})` : "" ): ""}</td>
                            </tr>
                            <tr>
                                <td>Last Bonfire</td>
                                <td>{this.state.data.LastBonfire || ""}</td>
                            </tr>
                            <tr>
                                <td>Area</td>
                                <td>{this.state.data.Area || ""}</td>
                            </tr>
                            <tr>
                                <td>Health</td>
                                <td>{this.state.data.Health || 0} / {this.state.data.HealthMax || 0}</td>
                            </tr>
                            <tr>
                                <td>Deaths</td>
                                <td>{this.state.data.Deaths || 0}</td>
                            </tr>
                            <tr>
                                <td>Time</td>
                                <td>{this.state.data.Clock ? Helper.secondsToHMS(this.state.data.Clock) : ""}</td>
                            </tr>
                            <tr>
                                <td colSpan={2}><br/></td>
                            </tr>
                            {this.state.data.UndeadAsylum &&
                            <>
                                <tr>
                                    <td>Pre Oscar Fog</td>
                                    <td>{this.state.data.UndeadAsylum.PreOscarFog ? "klar" : "neblig"}</td>
                                </tr>
                                <tr>
                                    <td>Cell Door</td>
                                    <td>{this.state.data.UndeadAsylum.CellDoor ? "geöffnet" : "geschlossen"}</td>
                                </tr>
                                <tr>
                                    <td>F2 West Door</td>
                                    <td>{this.state.data.UndeadAsylum.F2WestDoor ? "geöffnet" : "geschlossen"}</td>
                                </tr>
                                <tr>
                                    <td>F2 East Door</td>
                                    <td>{this.state.data.UndeadAsylum.F2EastDoor ? "geöffnet" : "geschlossen"}</td>
                                </tr>
                                <tr>
                                    <td>Shortcut Door</td>
                                    <td>{this.state.data.UndeadAsylum.ShortcutDoor ? "geöffnet" : "geschlossen"}</td>
                                </tr>
                                <tr>
                                    <td>Big Pilgrim Door</td>
                                    <td>{this.state.data.UndeadAsylum.BigPilgrimDoor ? "geöffnet" : "geschlossen"}</td>
                                </tr>
                                <tr>
                                    <td>Oscar Trap</td>
                                    <td>{this.state.data.UndeadAsylum.OscarTrapSprung ? "ausgelöst" : "scharf"}</td>
                                </tr>
                                <tr>
                                    <td>Asylum Deamon</td>
                                    <td>{this.state.data.UndeadAsylum.AsylumDeamon ? "besiegt" : "lebendig"}</td>
                                </tr>
                            </>
                            }
                        </tbody>
                    </table>
                }
            </div>
        );
    }
}