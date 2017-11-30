// 現在時刻の表示コンポーネント
class CurrentTime extends React.Component {
    constructor(props) {
        super(props);
        this.state = { currentTime: getCurrentTime() };
        //関数内の「this」が機能するように、constructor()内でthisをbind(イベントハンドラの紐付け)
        this.updateCurrentTime = this.updateCurrentTime.bind(this);
    }
    updateCurrentTime() {
        this.setState( () => {
            return { currentTime: getCurrentTime() };
        });
    }
    componentDidMount() {
        setInterval(this.updateCurrentTime, 1000);
    }
    render() {
        return (
            <div style={{marginTop: 13, marginRight: 10, fontSize: 18, color: "white"}}>{this.state.currentTime}</div>
        );
    }
}