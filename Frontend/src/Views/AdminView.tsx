import React from "react";
import DarkSoulsData from "../Models/DarkSoulsData";
import { strings } from '../Lang/DarkSoulsStrings';
import ConnectionService from "../Services/ConnectionService";
import { withSnackbar, ProviderContext } from "notistack";
import { Button, CircularProgress, Typography } from "@mui/material";
import PowerSettingsNewIcon from '@mui/icons-material/PowerSettingsNew';
import { ThemeProvider, createTheme, Theme } from '@mui/material/styles';
import './AdminView.scss';

export interface AdminViewState {
    data: DarkSoulsData | null,
    connected: boolean,
    shutdown: boolean;
    theme: Theme;
}

const lightTheme: Theme = createTheme({
    palette: {
        primary: {
            main: "#EA60B6"
        }
    }
});

class AdminView extends React.Component<ProviderContext, AdminViewState> {

    private client: ConnectionService;

    constructor(props: ProviderContext) {
        super(props);
        this.state = {
            data: null,
            connected: false,
            shutdown: false,
            theme: lightTheme
        };

        this.client = new ConnectionService();
        this.client.onChange = connected => this.setState({connected: connected});
        this.client.onMessage = data => this.setState({data: data});
        this.client.onError = err => this.props.enqueueSnackbar(err, {variant: 'error'});
        this.client.onShutdown = () => this.setState({shutdown: true});
    }

    public render() {
        return(
            <ThemeProvider theme={this.state.theme}>
                <div id="App">
                    <Button id="ShutdownButton" variant="contained" color="error" disabled={this.state.shutdown} startIcon={<PowerSettingsNewIcon/>} onClick={() => this.client.shutdown()}>{strings.Labels.Shutdown}</Button>

                    {!this.state.shutdown && <>
                        {(!this.state.connected || this.state.data == null || !this.state.data?.Connected ) && <>
                            <div className="loading">
                                <CircularProgress color="primary" />
                                <Typography className="loading-text">{!this.state.connected ? strings.Labels.ConnectingService : strings.Labels.ConnectingGame}</Typography>
                            </div>
                        </>}
                    </>}
                    
                </div>
            </ThemeProvider>
        );
    }
}

export default withSnackbar(AdminView);