class CurrentTime extends React.Component {
    render() {
        return (
            <div style={{marginTop: 13, marginRight: 10, fontSize: 18, color: "white"}}>{this.props.currentTime}</div>
        );
    }
}

function rendarCurrentTime() {
    
    const getCurrentTime = () => {
        // 曜日を表す文字列の配列を作っておく
        const WeekChars = [ "(日)", "(月)", "(火)", "(水)", "(木)", "(金)", "(土)" ];
        // 日付オブジェクトの取得
        const dObj = new Date();
        return dObj.getFullYear() + "年" + zeroPadding(dObj.getMonth()+1,2) + "月" + zeroPadding(dObj.getDate(),2) + "日" + WeekChars[dObj.getDay()] + "\u0020" + zeroPadding(dObj.getHours(),2) + ":" + zeroPadding(dObj.getMinutes(),2)
    }

    setInterval(function() {
        ReactDOM.render(
            <CurrentTime currentTime={getCurrentTime()} />,
            document.getElementById("current-time")
        );
    }, 1000);
}
