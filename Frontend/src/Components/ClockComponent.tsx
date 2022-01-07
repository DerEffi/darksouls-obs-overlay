import React from "react";

export interface IClockComponentProps {
    clock: number;
    loaded: boolean;
}

export interface IClockComponentState {
    clock: number;
}

export class ClockComponent extends React.Component<IClockComponentProps, IClockComponentState> {

    constructor(props: IClockComponentProps) {
        super(props);
        this.state = {
            clock: 0
        };

        setInterval(() => {
            if(this.props.loaded) {
                this.setState({
                    clock: this.state.clock + 1
                });
            }
        }, 1000);
    }

    componentDidUpdate(prevProps: IClockComponentProps) {
        if(prevProps.clock != this.props.clock || prevProps.loaded != this.props.loaded) {
            this.setState({
                clock: this.props.clock
            });
        }
    }

    public render() {
        return (
            <div id="Clock">
                {this.secondsToHMS(this.state.clock)}
            </div>
        );
    }

    private secondsToHMS(seconds: number) {
        var h = Math.floor(seconds / 3600);
        var m = Math.floor(seconds % 3600 / 60);
        var s = Math.floor(seconds % 3600 % 60);

        return `${h} : ${m < 10 ? 0 : ""}${m} : ${s < 10 ? 0 : ""}${s}`; 
    }

}