import './AdminView.scss';
import { strings } from '../Lang/DarkSoulsStrings';
import DarkSoulsData from '../Models/DarkSoulsData';
import ConnectionService from '../Services/ConnectionService';
import CloseIcon from '@mui/icons-material/Close';
import DoneIcon from '@mui/icons-material/Done';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import PowerSettingsNewIcon from '@mui/icons-material/PowerSettingsNew';
import { Button, CircularProgress, Modal, Switch, TextField, Typography } from '@mui/material';
import Accordion from '@mui/material/Accordion';
import AccordionDetails from '@mui/material/AccordionDetails';
import AccordionSummary from '@mui/material/AccordionSummary';
import { createTheme, Theme, ThemeProvider } from '@mui/material/styles';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import { ProviderContext, withSnackbar } from 'notistack';
import React from 'react';

export interface AdminViewState {
    data: DarkSoulsData | null,
    connected: boolean,
    shutdown: boolean;
    theme: Theme;
    updateInterval: number;
    compareEventFlags: boolean;
    showEventComparisonWarning: boolean;
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
            theme: lightTheme,
            updateInterval: 1,
            showEventComparisonWarning: false,
            compareEventFlags: false,
        };

        this.client = new ConnectionService();
        this.client.onChange = connected => this.setState({connected: connected});
        this.client.onMessage = data => {
            if(!this.state.data || this.state.data.Settings.CompareEventFlags !== data.Settings.CompareEventFlags || this.state.data.Settings.UpdateInterval !== data.Settings.UpdateInterval) {
                this.setState({
                    updateInterval: data.Settings.UpdateInterval || this.state.updateInterval,
                    compareEventFlags: data.Settings.CompareEventFlags
                });
            }
            this.setState({data: data});
        };
        this.client.onError = err => this.props.enqueueSnackbar(err, {variant: 'error'});
        this.client.onShutdown = () => this.setState({shutdown: true});
    }

    public render() {
        return(
            <ThemeProvider theme={this.state.theme}>
                <div id="App">
                    <Button id="ShutdownButton" variant="contained" color="error" disabled={this.state.shutdown} startIcon={<PowerSettingsNewIcon/>} onClick={() => this.client.shutdown()}>{strings.Labels.Shutdown}</Button>

                    {!this.state.shutdown && <>
                        {(!this.state.connected || !this.state.data || !this.state.data.Connected || !this.state.data.Loaded || !this.state.data.Char.CharacterName ) && <>
                            <div className="loading">
                                <CircularProgress color="primary" />
                                <Typography className="loading-text">{!this.state.connected ? strings.Labels.ConnectingService : !this.state.data || !this.state.data.Connected ? strings.Labels.ConnectingGame : strings.Labels.LoadSaveGame}</Typography>
                            </div>
                        </>}

                        {this.state.connected && this.state.data && this.state.data?.Connected && this.state.data.Char.CharacterName && <>
                            <div className="content">

                                <div id="settings">
                                    <Typography variant="h5">{strings.Labels.Settings}</Typography>
                                    <Modal
                                        open={this.state.showEventComparisonWarning}
                                        onClose={() => this.dismissEventComparisonWarning()}
                                        className="warning"
                                    >
                                        <>
                                            <div className="warning-headline"><Typography variant="h5">{strings.Labels.WarningHeadline}</Typography></div>
                                            <div className="warning-text"><Typography>{strings.Labels.EventFlagWarning}</Typography></div>
                                            <div className="warning-button"><Button variant="contained" color="primary" onClick={() => this.dismissEventComparisonWarning()}>{strings.Labels.Ok}</Button></div>
                                        </>
                                    </Modal>
                                    <Table>
                                        <TableHead></TableHead>
                                        <TableBody>
                                            <TableRow>
                                                <TableCell align="left">{strings.Labels.UpdateInterval}</TableCell>
                                                <TableCell align="right"><TextField value={this.state.updateInterval} type="number" variant="standard" onChange={(ev: any) => this.onChangeUpdateInterval(ev)} /></TableCell>
                                            </TableRow>
                                            <TableRow>
                                                <TableCell align="left">{strings.Labels.CompareEventFlags}</TableCell>
                                                <TableCell align="right"><Switch checked={this.state.compareEventFlags} onChange={(ev: any, checked: boolean) => this.onChangeEventComparison(checked)} /></TableCell>
                                            </TableRow>
                                        </TableBody>
                                    </Table>
                                    <div id="settings-confirm">
                                        <Button variant="contained" color="primary" onClick={() => this.updateSettings()}>{strings.Labels.Apply}</Button>
                                    </div>
                                </div>

                                <div id="events">
                                    <Typography variant="h5">{strings.Labels.Events}</Typography>
                                    {this.displayData(this.state.data)}
                                </div>
                            </div>
                        </>}
                    </>}
                    
                </div>
            </ThemeProvider>
        );
    }

    private displayData(data: DarkSoulsData): JSX.Element[] {
        let elements: JSX.Element[] = [];
        let key: keyof typeof data.Areas;
        for(key in data.Areas) {
            elements.push(this.displayArea(data.Areas[key] as any, key));
        }
        return elements;
    }

    private displayArea(data: any, headline: string): JSX.Element {
        let elements: JSX.Element[] = [];

        let key: keyof typeof data;
        let areaKey: keyof typeof strings.Areas = headline as keyof typeof strings.Areas;
        let areaStrings: any = strings.Areas[areaKey] as any;

        for(key in data) {
            let eventKey: keyof typeof areaStrings = key as keyof typeof areaStrings;
            elements.push(
                <div key={key} className="event-box">
                    {data[key] as boolean ? <DoneIcon color="success" /> : <CloseIcon color="error" /> }
                    <Typography>{areaStrings ? areaStrings[eventKey] || key : key}</Typography>
                </div>
            );
        }

        return(
            <Accordion key={headline}>
                <AccordionSummary expandIcon={<ExpandMoreIcon />}>
                    <Typography>{areaStrings ? areaStrings["Name"] || headline : headline}</Typography>
                </AccordionSummary>
                <AccordionDetails>
                    {elements}
                </AccordionDetails>
            </Accordion>
        );
    }

    private onChangeEventComparison(checked: boolean) {
        this.setState({
            compareEventFlags: checked,
            showEventComparisonWarning: checked
        });
    }

    private onChangeUpdateInterval(ev: any) {
        this.setState({
            updateInterval: +(ev.target.value)
        })
    }

    private dismissEventComparisonWarning() {
        this.setState({
            showEventComparisonWarning: false
        });
    }

    private updateSettings() {
        this.client.updateSettings({
            CompareEventFlags: this.state.compareEventFlags,
            UpdateInterval: this.state.updateInterval
        })
        .then(() => this.props.enqueueSnackbar(strings.Labels.SettingsUpdated, {variant: "info"}))
        .catch(() => {});
    }
}

export default withSnackbar(AdminView);