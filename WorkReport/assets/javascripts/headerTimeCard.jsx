//タイムカード出社(子コンポーネント)
class ArrivalTimeCard extends React.Component {
    render() {
        const dataContent = () => {
            if(this.props.arrival == emptyDefault){ return "<div style='width: 210px; height: 32px;'><p style='float: left'><img src='/assets/images/notice_mark.png' alt='警告' width='32' height'32' /></p><p style='float: left; padding-left: 3px; font-size: 12px;'>ここをクリックして<br />出社時刻を打刻してください。</p></div>" }
            return "<div style='width: 220px; height: 32px;'><p style='float: left'><img src='/assets/images/notice_mark.png' alt='警告' width='32' height'32' /></p><p style='float: left; padding-left: 3px; font-size: 12px;'>出社時刻が設定されていますので<br />打刻はできません。</p></div>"
        }
        return (
            <div id="id-input-arrival" className="left-float" style={{height: 50, paddingTop: 6}} >
                <div className="left-float pointer" style={{paddingTop: 5}}><img src="/assets/images/arrived_at.png" alt="出" width="30" height="30" onClick={this.props.setArrival} /></div>
                <div className="left-float"><p id="id-arrival-text" className="container-fluid" data-placement="bottom" data-html="true" data-content={dataContent()} onClick={this.props.setArrival} >{this.props.arrival}</p></div>
            </div>
        );
    }
};

//タイムカード退社(子コンポーネント)
class LeaveTimeCard extends React.Component {
    render() {
        const dataContent = () => {
            if(this.props.arrival == emptyDefault && this.props.leave == emptyDefault){ return "<div style='width: 150px; height: 32px;'><p style='float: left'><img src='/assets/images/notice_mark.png' alt='警告' width='32' height'32' /></p><p style='float: left; padding-left: 3px; font-size: 12px;'>先に出社時刻を<br />打刻してください。</p></div>" }
            if(this.props.leave == emptyDefault){ return "<div style='width: 270px; height: 32px;'><p style='float: left'><img src='/assets/images/notice_mark.png' alt='警告' width='32' height'32' /></p><p style='float: left; padding-left: 3px; font-size: 12px;'>退社するときは、ここをクリックして<br />退社時刻を打刻してください。</p></div>" }
            return "<div style='width: 220px; height: 32px;'><p style='float: left'><img src='/assets/images/notice_mark.png' alt='警告' width='32' height'32' /></p><p style='float: left; padding-left: 3px; font-size: 12px;'>退社時刻が設定されていますので<br />打刻はできません。</p></div>"
        }
        return (
            <div id="id-input-leave" className="left-float" style={{height: 50, paddingTop: 7}}>
                <div className="left-float pointer" style={{paddingTop: 4}} ><img src="/assets/images/leave_at.png" alt="退" width="30" height="30" onClick={this.props.setLeave} /></div>
                <div className="left-float"><p id="id-leave-text" className="container-fluid" data-placement="bottom" data-html="true" data-content={dataContent()} onClick={this.props.setLeave} >{this.props.leave}</p></div>
            </div>
        );
    }
};

//タイムカード本体(親コンポーネント)
class TimeCard extends React.Component {
    constructor(props) {
        super(props);
        this.state = { arrival: emptyDefault, leave: emptyDefault , arrivalDisabled: false, leaveDisabled: true };
        //関数内の「this」が機能するように、constructor()内でthisをbind(イベントハンドラの紐付け)
        this.setArrival = this.setArrival.bind(this);
        this.setLeave = this.setLeave.bind(this);
    }
    componentWillMount(){
        $.ajaxSetup({ async: false });
        const timecardData =  $.getJSON(this.props.getTimeCardURL).responseJSON
        const today = getDate()
        const todayData = timecardData.filter((x) => { if( String(x.t_arrival).substr(0,10) == today || String(x.t_leave).substr(0,10) == today) return true })[0];
        // 当日のタイムカード情報が無い場合は更新無し
        if(!todayData){ return }
        // 出社のデータを確認して存在する場合は更新
        if(isExistTime(todayData.t_arrival)) {
            this.setState( () => { return { arrival: getTimeValue(todayData.t_arrival) } } )
            this.setState( () => { return { arrivalDisabled: true }} )
            this.setState( () => { return { leaveDisabled: false }} ) 
        }
        // 退社のデータを確認して存在する場合は更新
        if(isExistTime(todayData.t_leave)) {
            this.setState( () => { return { leave: getTimeValue(todayData.t_leave) } } ) 
        }
    }
    componentDidMount(){
        const data = Object.assign(this.state);
        showBalloon(data);
    }
    //出社時刻の打刻
    setArrival() {
        // 更新は日に一度まで
        if(this.state.arrivalDisabled){ return };
        
        const time = getTime();
        //状態を更新
        this.setState( () => {
            return { arrival: time };
        });

        //出社時刻の更新(必要なデータをjson形式でpost)
        const url = this.props.postArrivalURL;
        const sendData = { "date": getDate() , "arrival": time };
        
        if(this.state.arrival != emptyDefault) { return }

        //POST通信
        superagent
        .post( url )
        .set("Content-Type", "application/json")
        .send( JSON.stringify( sendData ) )
        .end(function(err,res){
            console.log("error:"+err+","+"res:"+res)
        });

        //状態を更新(ボタンの制御)
        this.setState( () => {
            return { arrivalDisabled: true, leaveDisabled: false };
        });

        hidePopover("#id-arrival-text");
        setTimeout(function(){ showPopover("#id-leave-text") }, 300);
    }
    //退社時刻の打刻
    setLeave() {
        // 出社時刻を打刻していない場合は無効
        if(!isExistTime(this.state.arrival)) { return }

        const time = getTime();
        //状態を更新(退社時刻)
        this.setState( () => {
            return { leave: time }
        });

        //退社時刻の更新(必要なデータをjson形式でpost)
        const url = this.props.postLeaveURL;
        const sendData = { "date": getDate() , "leave": time };
        
        //POST通信
        superagent
        .post( url )
        .set("Content-Type", "application/json")
        .send( JSON.stringify( sendData ) )
        .end(function(err,res){
            console.log("error:"+err+","+"res:"+res)
        });

        //状態を更新(ボタンの制御)
        this.setState( () => {
            return { leaveDisabled: true };
        });

        hidePopover("#id-leave-text");
    }
    render() {
        return (
            <div style={{height: 50}}>
                <ArrivalTimeCard
                    arrival={this.state.arrival}
                    setArrival={this.setArrival}
                    buttonDisabled={this.state.arrivalDisabled} 
                />
                <LeaveTimeCard
                    leave={this.state.leave}
                    arrival={this.state.arrival}
                    setLeave={this.setLeave}
                    buttonDisabled={this.state.leaveDisabled} 
                />
            </div>
        );
    }
};

//タイムカードのレンダリング
function renderTimeCard(getJsonUrl, postArrivalUrl, postLeaveUrl){
    $("#content-timecard").empty();
    ReactDOM.render(
        <TimeCard
            getTimeCardURL={getJsonUrl}
            postArrivalURL={postArrivalUrl}
            postLeaveURL={postLeaveUrl}
        />,
        document.getElementById("content-timecard")
    );
}