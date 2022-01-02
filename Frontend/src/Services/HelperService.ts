export default class Helper {
    public static secondsToHMS(seconds: number) {
        var h = Math.floor(seconds / 3600);
        var m = Math.floor(seconds % 3600 / 60);
        var s = Math.floor(seconds % 3600 % 60);

        return `${h} : ${m < 10 ? 0 : ""}${m} : ${s < 10 ? 0 : ""}${s}`; 
    }
}