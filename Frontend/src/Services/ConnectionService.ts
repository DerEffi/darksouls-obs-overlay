import { strings } from "../Lang/DarkSoulsStrings";
import DarkSoulsData from "../Models/DarkSoulsData";

export default class ConnectionService {

    //TODO reset to prod
    // private readonly host: string = window.location.host;
    private readonly host: string = "localhost:80";
    private readonly subscribeEndpoint: string = `ws://${this.host}/status/subscribe`;
    private readonly shutdownEndpoint: string = `http://${this.host}/close`;
    
    private timer: NodeJS.Timer;
    private connection: WebSocket | null = null;

    public onChange = (connected: boolean) => {};
    public onMessage = (data: DarkSoulsData) => {};
    public onError = (err: string) => {};
    public onShutdown = () => {};

    public constructor() {
        this.timer = setInterval(() => this.connect(), 10000);
    }

    private connect() {
        if(this.connection == null) {
            let connection: WebSocket = new WebSocket(this.subscribeEndpoint);
            connection.onopen = () => {
                clearInterval(this.timer);
                console.log("Connection established");
                this.onChange(true);
                this.connection = connection;
            }

            connection.onclose = () => {
                //console.log("Connection closed");
                this.onChange(false);
                this.connection = null;
                clearInterval(this.timer);
                this.timer = setInterval(() => this.connect(), 5000);
            }

            connection.onmessage = evt => {
                try {
                    let data: DarkSoulsData = JSON.parse(evt.data);
                    this.onMessage(data);
                } catch {
                    this.onError(strings.Errors.NoJson);
                }
            }

            connection.onerror = err => {
                //console.error(err);
                //this.onError(strings.Errors.ConnectionError);
            }
        }
    }

    public isConnected() {
        return this.connection != null;
    }

    public shutdown() {
        fetch(this.shutdownEndpoint)
        .then((resp) => {
            return resp.json();
        })
        .then(json => {
            if(json.message != null) {
                this.onShutdown();
            } else {
                throw "Message not set in shutdown response";
            }
        })
        .catch((err) => {
            console.error(err);
            this.onError(strings.Errors.ShutdownError);
        });
    }
}