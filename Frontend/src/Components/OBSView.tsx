import React from "react";
import DarkSoulsData from "../Models/DarkSoulsData";
import Helper from "../Services/HelperService";
import "./OBS.scss";

export interface AdminViewState {
    data: DarkSoulsData | null,
    connected: boolean
}

export default class OBSView extends React.Component<{}, AdminViewState> {
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
                {this.state.connected && this.state.data != null && this.state.data.Connected &&
                    <table style={{width: "100%"}}>
                        <thead>
                            <td style={{width: "180px"}}></td>
                            <td></td>
                        </thead>
                        <tbody>
                            {this.state.data.CharacterName &&
                            <>
                                <tr>
                                    <td>Character</td>
                                    <td>{this.state.data.CharacterName ? this.state.data.CharacterName + (this.state.data.CharacterClass ? ` (${this.state.data.CharacterClass})` : "" ): ""}</td>
                                </tr>
                                <tr>
                                    <td>Last Bonfire</td>
                                    <td>{this.state.data.LastBonfire || "-"}</td>
                                </tr>
                                {this.state.data.Settings.HealthEnabled &&
                                    <tr>
                                        <td>Health</td>
                                        <td>{this.state.data.Health || 0} / {this.state.data.HealthMax || 0}</td>
                                    </tr>
                                }
                                <tr>
                                    <td>Deaths</td>
                                    <td>{this.state.data.Deaths || 0}</td>
                                </tr>
                                {this.state.data.Settings.ClockEnabled &&
                                    <tr>
                                        <td>Time</td>
                                        <td>{this.state.data.Clock ? Helper.secondsToHMS(this.state.data.Clock) : "0 : 00 : 00"}</td>
                                    </tr>
                                }
                                <tr>
                                    <td colSpan={2}><br/></td>
                                </tr>
                            </>
                            }
                            {this.state.data.Loaded &&
                            <>
                                <tr>
                                    <td colSpan={2}><b style={{fontSize: "14pt"}}>{this.state.data.Area}</b></td>
                                </tr>
                                {this.state.data.Area == "Northern Undead Asylum" && this.state.data.UndeadAsylum &&
                                <>
                                    <tr className={this.state.data.UndeadAsylum.CellDoor ? "" : "inactive"}>
                                        <td>Cell Door</td>
                                        <td>{this.state.data.UndeadAsylum.CellDoor ? "geöffnet" : "geschlossen"}</td>
                                    </tr>
                                    <tr className={this.state.data.UndeadAsylum.PreOscarFog ? "" : "inactive"}>
                                        <td>Pre Oscar Fog</td>
                                        <td>{this.state.data.UndeadAsylum.PreOscarFog ? "klar" : "neblig"}</td>
                                    </tr>
                                    <tr className={this.state.data.UndeadAsylum.ShortcutDoor ? "" : "inactive"}>
                                        <td>Shortcut Door</td>
                                        <td>{this.state.data.UndeadAsylum.ShortcutDoor ? "geöffnet" : "geschlossen"}</td>
                                    </tr>
                                    <tr className={this.state.data.UndeadAsylum.F2EastDoor ? "" : "inactive"}>
                                        <td>F2 East Door</td>
                                        <td>{this.state.data.UndeadAsylum.F2EastDoor ? "geöffnet" : "geschlossen"}</td>
                                    </tr>
                                    <tr className={this.state.data.UndeadAsylum.AsylumDeamon ? "" : "inactive"}>
                                        <td>Asylum Deamon</td>
                                        <td>{this.state.data.UndeadAsylum.AsylumDeamon ? "besiegt" : "lebendig"}</td>
                                    </tr>
                                    <tr className={this.state.data.UndeadAsylum.BigPilgrimDoor ? "" : "inactive"}>
                                        <td>Big Pilgrim Door</td>
                                        <td>{this.state.data.UndeadAsylum.BigPilgrimDoor ? "geöffnet" : "geschlossen"}</td>
                                    </tr>
                                </>
                                }
                                {this.state.data.Area == "Firelink Shrine" && this.state.data.FirelinkShrine &&
                                <>
                                    <tr className={this.state.data.FirelinkShrine.TalismanChest ? "" : "inactive"}>
                                        <td>Talisman Chest</td>
                                        <td>{this.state.data.FirelinkShrine.TalismanChest ? "geöffnet" : "geschlossen"}</td>
                                    </tr>
                                    <tr className={this.state.data.FirelinkShrine.HomewardBoneChest ? "" : "inactive"}>
                                        <td>Homeward Bone Chest</td>
                                        <td>{this.state.data.FirelinkShrine.HomewardBoneChest ? "geöffnet" : "geschlossen"}</td>
                                    </tr>
                                    <tr className={this.state.data.FirelinkShrine.CrackedRedEyeOrbChest ? "" : "inactive"}>
                                        <td>Cracked Red Eye Orb Chest</td>
                                        <td>{this.state.data.FirelinkShrine.CrackedRedEyeOrbChest ? "geöffnet" : "geschlossen"}</td>
                                    </tr>
                                    <tr className={this.state.data.FirelinkShrine.LloydTalismanChest ? "" : "inactive"}>
                                        <td>Lloyd's Talisman Chest</td>
                                        <td>{this.state.data.FirelinkShrine.LloydTalismanChest ? "geöffnet" : "geschlossen"}</td>
                                    </tr>
                                </>
                                }
                            </>
                            }
                        </tbody>
                    </table>
                }
            </div>
        );
    }
}