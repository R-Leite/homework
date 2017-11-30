// ライブラリから棒グラフのコンポーネントを読込
const {BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, ReferenceArea} = Recharts;
// 最小時間と最大時間の定義
const minTime = 600;
const maxTime = 2400;
// グラフ及びテーブル用のラベル配列
const ticks = Array.from(Array((maxTime - minTime) / 100), (v, k) => k).map( (x) => ( x + 6) * 100 );
const timeArray = Array.from(Array((maxTime - minTime) / 100), (v, k) => k).map( (x) => zeroPadding( (x + 6), 2) + "時" );
// 各種横幅の定義
const dateWidth = 90;
const arrivalWidth = 85;
const leaveWidth = 85;
const timeWidth = 50;
const correctWidth = 5;
const barWidth = timeWidth * ticks.length;
const tableWidth = dateWidth + arrivalWidth + leaveWidth + barWidth - correctWidth;
// 時間の整形関数
const timeFormatter = (x) => { return zeroPadding(x,4).substr(0,2) + ":" + zeroPadding(x,4).substr(2,2) };
const timeShaper    = (x) => { const times = String(x).split(':'); return zeroPadding(times[0],2) + ":" + zeroPadding(times[1],2)+ ":00" };
const tooltipFormatter = (x) => { return timeFormatter(x[0]) + "～" + timeFormatter(x[1]) };
const timeToNumber = (x) => { return Number(timeSubstr(x).replace(":","")) }
// 曜日取得関数
const getWeekDay = (date) => {
    const WeekChars = [ " (日) ", " (月) ", " (火) ", " (水) ", " (木) ", " (金) ", " (土) " ];
    const dObj = new Date( date );
    const wDay = dObj.getDay();
    return WeekChars[wDay];
}
//日付種別に応じてツールチップ返却
const getTooltip = (datetype) => {
    // オブジェクトの定義
    const dateTooltip = {1:"",2:"",3:"特定休日",4:"一般休日",5:"フレックス休割当日",6:"ハッピー休割当日",7:"プレミアムフライデー"};
    return dateTooltip[datetype];
}
// 日付種別に応じてclass返却
const getClass = (datetype) => {
    // オブジェクトの定義
    const dateClass = {1:"",2:"",3:"specific_holiday",4:"nomal_holiday",5:"flex_holiday",6:"happy_holiday",7:"premium_friday"};
    return dateClass[datetype];
}
// 今日の日付取得
const getToday = () => {
    return new Date().getDate();
}
// 未来入力のバリデーション
const checkEdit = (year, month, date) => {    
    //比較用のDateオブジェクトを２つ生成する
    const dtEdit  = new Date(year, month, date, 0, 0, 0, 0);
    const dtToday = new Date();
    //Dateオブジェクトを比較する
    if(dtEdit > dtToday){ return false } 
    return true
}
// 時間入力のバリデーション
const checkValidate = (arrival, leave) => {
    if(arrival == undefined || leave == undefined) return true
    if(arrival.substr(11,8) == "00:00:59" || arrival.substr(11,8) == "00:00:00" ) return true
    if(leave.substr(11,8) == "00:00:59" || leave.substr(11,8) == "00:00:00" ) return true
    if(timeToNumber(arrival) >= timeToNumber(leave)) return false
    return true
}

// bootstrap-timepickerのラップコンポーネント
class WrapTimePicker extends React.Component{
    componentDidMount(){
        const targetId = "#" + this.props.config.id;
        const options = {
            minuteStep: this.props.config.minuteStep,
            maxHours: this.props.config.maxHours,
            snapToStep: this.props.config.snapToStep,
            showMeridian: this.props.config.showMeridian,
            defaultTime: this.props.config.defaultTime,
            showInputs: this.props.config.showInputs
        };

        $(targetId).timepicker(options);
        
        if(!this.props.config.handleChange) return
        $(targetId).on("hide.timepicker", this.props.config.handleChange)
    }
    componentWillUnmount(){
        const targetId = "#" + this.props.config.id;
        $(targetId).unbind("hide.timepicker");
    }
    render(){
        return(
            <input type="text" data-format={this.props.config.format} id={this.props.config.id} className={this.props.config.addClass} style={this.props.config.customStyle} defaultValue={this.props.config.defaultValue} readOnly required></input>
        )
    }
}

class CustomLabel extends React.Component{
    render() {
        const {x, y, fill} = this.props;
        const value = this.props[this.props["keyName"]];
        return (
            <text x={x+42} y={y+9} dy={-4} fill={fill} textAnchor="middle">{timeFormatter(value[0]) + "～" + timeFormatter(value[1])}</text>
        )
    }
}

class WrapBarChart extends React.Component {
    render() {
        const dispBarChart = (keyName, name, fill, size, data) => {
            if(data[keyName][0] == 0 || data[keyName][1] == 0 ) { return null }
            return (
                <Bar dataKey={keyName} name={name} label={<CustomLabel keyName={keyName} />} barSize={size} fill={fill}  />
            )
        }
        return (
            <BarChart layout="vertical" width={barWidth} height={35} margin={{ top: 0, right: 0, bottom: 0, left: 0 }} data={[this.props.data]} >
                <XAxis type="number" domain={[minTime, maxTime]} hide={true} ticks={ticks} interval={0} />
                <YAxis type="category" dataKey="date" hide={true}  />
                <Tooltip formatter={tooltipFormatter} wrapperStyle={{ zIndex: 10 }} isAnimationActive={false} />
                {dispBarChart("timecard","タイムカード","#CC9966", 10, this.props.data)}
                {dispBarChart("attendance","勤怠データ","#0099FF", 5, this.props.data)}
                <ReferenceArea x1={minTime} x2={830}     fill="#EEEEEE" />
                <ReferenceArea x1={830}     x2={1030}    fill="#FFFFBB" />
                <ReferenceArea x1={1030}    x2={1530}    fill="#FFFF55" />
                <ReferenceArea x1={1530}    x2={1715}    fill="#FFFFBB" />
                <ReferenceArea x1={1715}    x2={2200}    fill="#EEEEEE" />
                <ReferenceArea x1={2200}    x2={maxTime} fill="#BAD3FF" />
            </BarChart>
        );
    }
};

class EditComponent extends React.Component{
    constructor(props) {
        super(props);
        this.handleFocus = this.handleFocus.bind(this); 
    }
    handleFocus(e){
        $("#id-" + this.props.keyName + "-" + String(Number(this.props.index) + 1)).timepicker("hideWidget");
    }
    render(){
        const displayValue = (x) => {
            if(x == emptyDefault) return "";
            if(String(x).substr(11,8) == "00:00:59") { return "" }
            if(String(x).substr(11,8) == "00:00:00") { return "" }
            return timeSubstr(x); 
        }
        const noteID = "note-" + this.props.keyName + "-" + String(Number(this.props.index) + 1);
        const config = {
            id: "id-" + this.props.keyName + "-" + String(Number(this.props.index) + 1),
            name: this.props.keyName + "-time",
            addClass: "",
            defaultValue: displayValue(this.props.value), 
            format: "hh:mm",
            minuteStep: 1,
            maxHours: 24,
            snapToStep: true,
            showMeridian: false,
            defaultTime: "current",
            showInputs: false,
            customStyle: {width: 55, height: 30, textAlign: "center", marginRight: 10},
            handleChange: ""
        }
        const submitID = () => {
            return "submit-" + this.props.keyName + "-" + String(Number(this.props.index) + 1)
        }
        return (
            <div className="full-width" >
                <div className="full-width left-flaot" >
                    <div className="left-float" style={{paddingLeft: 10}}>
                        <form className="left-float" onSubmit={this.props.onUpdate}>
                            <label>{this.props.labelName}<WrapTimePicker config={config} /></label>
                            <label>変更理由：<input type="text" id={noteID} className="change-reason" defaultValue={this.props.note_value} onFocus={this.handleFocus} required ></input></label>
                            <input id={submitID()} type="submit" className="btn btn-default btn-standard btn-submit" onFocus={this.handleFocus} value="更新"></input>
                        </form>
                        <div className="left-float" style={{paddingLeft: 10}}><button className="btn btn-default btn-standard btn-cancel" onFocus={this.handleFocus} onClick={this.props.handleCancel} >キャンセル</button></div>
                    </div>
                </div>
            </div>
        )
    }
}

class MainTbody extends React.Component{
    constructor(props) {
        super(props);
        this.state = { timeData: this.props.timeData };
    }
    render() {
        const concat = (x,y) => { return [x,y] }
        const checkAlert = (t_arrival, t_leave, a_arrival, a_leave) => {
            if(!isExistTime(t_arrival) && !isExistTime(t_leave)) { return null }
            if(!isExistTime(t_arrival) && isExistTime(t_leave)) { return <img className="contradiction-alert" src="/assets/images/alert_mark.png" alt="警告" width="18" height="18" title="出社時刻が設定されていません。"/> }
            if(isExistTime(t_arrival) && !isExistTime(t_leave)) { return <img className="contradiction-alert" src="/assets/images/alert_mark.png" alt="警告" width="18" height="18" title="退社時刻が設定されていません。"/> }            
            if(timeToNumber(t_arrival) > timeToNumber(a_arrival)) return <img className="contradiction-alert" src="/assets/images/alert_mark.png" alt="警告" width="18" height="18" title="出社時刻が始業時刻より遅い、あるいは退社時刻が終業時刻より早いです。"/>
            if(timeToNumber(t_leave) < timeToNumber(a_leave)) return <img className="contradiction-alert" src="/assets/images/alert_mark.png" alt="警告" width="18" height="18" title="出社時刻が始業時刻より遅い、あるいは退社時刻が終業時刻より早いです。"/>
            return null
        }
        const editing = (input_mode) => { 
            if(input_mode) return {width: 85, backgroundColor: "#CCFFCC"}
            return {width: 85}
        }
        const edited = (first, current) => { 
            if(first == current) return "times no-edit"; else return "times edited"
        }        
        const getTimeValue = (x) => {
            if(x == undefined) { return emptyDefault }
            if(String(x).substr(11,8) == "00:00:59") { return emptyDefault }
            if(String(x).substr(11,8) == "00:00:00") { return emptyDefault }
            return timeSubstr(x)
        }
        const getTimeNumber = (x) => {
            if(x != undefined) { return timeToNumber(x) }
            return 0
        }
        const getDetails = (first, current, edit_by, note) => {
            if(edited(first, current) == "times edited") { 
                return "最終編集者：" + edit_by + "\n" + "変更理由：" + note 
            }
            return null
        }
        const tbodyRow = (data, index, arrivalClick, leaveClick) => {
            const elementID = (key) => {
                return "id-" + key + "-" + String(index + 1)
            }
            return (
                <tr className={getClass(data.date_type_id)} >
                    <td className="date" title={getTooltip(data.date_type_id)} ><p className="p-text" >{String(data.date).substr(8,2) + "日" + getWeekDay(data.date) }{checkAlert(data.t_arrival,data.t_leave,data.a_arrival,data.a_leave)}</p></td>
                    <td id={elementID("arrivalcell")} className={edited(data.arrival_time, data.t_arrival)} style={editing(data.input_mode.arrival)} title={getDetails(data.arrival_time, data.t_arrival, data.edit_by_arrival_name, data.note_arrival)} onClick={arrivalClick} >{getTimeValue(data.t_arrival)}</td>
                    <td id={elementID("leavecell")} className={edited(data.leave_time, data.t_leave)} style={editing(data.input_mode.leave)} title={getDetails(data.leave_time, data.t_leave, data.edit_by_leave_name, data.note_leave)} onClick={leaveClick} >{getTimeValue(data.t_leave)}</td>
                    <td className="bar-chart" colSpan={ticks.length}>
                        <WrapBarChart
                            data={{date: "", timecard: [timeToNumber(data.t_arrival),timeToNumber(data.t_leave)], attendance: [timeToNumber(data.a_arrival),timeToNumber(data.a_leave)] }} 
                        />
                    </td>
                </tr>
            )
        }
        const editRow = (keyName ,isDisply, data, index, onUpdate, handleCancel) => {
            const inlineStyle = () => { 
                if(isDisply) return {}
                return {display: "none"}
            }
            const editComponent = () => {
                if(keyName == "arrival") { 
                    return (
                        <EditComponent
                            keyName={keyName}
                            index={index}
                            value={data.t_arrival}
                            labelName={"出社時刻："}
                            note_value={data.note_arrival}
                            onUpdate={onUpdate}
                            handleCancel={handleCancel}
                        />
                    )
                } 
                return (
                    <EditComponent
                        keyName={keyName}
                        index={index}
                        value={data.t_leave}
                        labelName={"退社時刻："}
                        note_value={data.note_leave}
                        onUpdate={onUpdate}
                        handleCancel={handleCancel}
                    />
                )
            }
            return (
                <tr className="edit-row" style={inlineStyle()}>
                    <td colSpan="21">
                        {editComponent()}
                    </td>
                </tr>
            )
        }
        return (
            <tbody>
                {
                    this.state.timeData.map( (x, index) => {
                        return concat( tbodyRow(x, index, this.props.arrivalClick.bind(this, index), this.props.leaveClick.bind(this, index)), concat( editRow("arrival", x.input_mode.arrival, x, index, this.props.arrivalUpdate.bind(this, index), this.props.handleCancel), editRow("leave", x.input_mode.leave, x, index, this.props.leaveUpdate.bind(this, index), this.props.handleCancel) ) ) 
                    })
                }
            </tbody>
        );
    }
}

class MainTable extends React.Component {
    constructor(props) {
        super(props);
        this.state = { timeData: this.props.timeData };
        this.arrivalUpdate = this.arrivalUpdate.bind(this);
        this.leaveUpdate   = this.leaveUpdate.bind(this);
        this.handleCancel  = this.handleCancel.bind(this);
    }
    handleCancel(e){
        const data = this.state.timeData.map( (x) => { x.input_mode.arrival = 0; x.input_mode.leave = 0; return x; } );
        this.setState( () => {
            return { timeData: data };
        });
    }
    arrivalClick(index, e){
        const display_date = $("#display-date").val();
        const years = display_date.split('-');
        if(!checkEdit(Number(years[0]), Number(years[1]) - 1, index + 1)) { return } 
        const data = this.state.timeData.map( (x) => { x.input_mode.arrival = 0; x.input_mode.leave = 0; return x; } );
        data[index].input_mode.arrival = 1;
        this.setState( () => {
            return { timeData: data };
        });
    }
    leaveClick(index, e){
        const display_date = $("#display-date").val();
        const years = display_date.split('-');
        if(!checkEdit(Number(years[0]), Number(years[1]) - 1, index + 1)) { return }
        const data = this.state.timeData.map( (x) => { x.input_mode.arrival = 0; x.input_mode.leave = 0; return x; } );
        data[index].input_mode.leave = 1;
        this.setState( () => {
            return { timeData: data };
        });
    }
    arrivalUpdate(index, e){
        // defaultのイベントを上書き※submit処理などを無効にしている。
        e.preventDefault();
        
        const data = this.state.timeData.map( (x) => { x.input_mode.arrival = 0; x.input_mode.leave = 0; return x; } );
        const timeID = "#id-arrival-"+String(index+1)
        const noteID = "#note-arrival-"+String(index+1)
        const display_date = $("#display-date").val() + "-" + zeroPadding(index+1, 2) 
        const latest_arrival_time = display_date + " " + timeShaper($(timeID).val())
        const note_arrival = $(noteID).val()

        const headerConfig = this.props.headerConfig
        console.log(headerConfig)

        if(!checkValidate(latest_arrival_time, data[index].t_leave)){
            alert("出社時刻は退社時刻より早くなるように入力してください。")
            return
        }

        const sendData = {
            "date": display_date,
            "latest_arrival_time": latest_arrival_time,
            "note_arrival": note_arrival
        }

        //POST通信
        superagent
        .post( this.props.postURL.updateArrival )
        .set( "Content-Type", "application/json" )
        .send( JSON.stringify(sendData) )
        .end(function(err,res){
            // header連動
            if(getToday() == index + 1) { renderTimeCard(headerConfig.jsonURL.getTimeCard,headerConfig.jsonURL.postArrival,headerConfig.jsonURL.postLeave) }
        });

        data[index].t_arrival = latest_arrival_time;
        data[index].note_arrival = note_arrival;
        data[index].edit_by_arrival_name = this.props.fullName;

        this.setState( () => {
            return { timeData: data };
        });
    }
    leaveUpdate(index, e){
        e.preventDefault();
        
        const data = this.state.timeData.map( (x) => { x.input_mode.arrival = 0; x.input_mode.leave = 0; return x; } );
        const timeID = "#id-leave-"+String(index+1)
        const noteID = "#note-leave-"+String(index+1)
        const display_date = $("#display-date").val() + "-" + zeroPadding(index+1, 2) 
        const latest_leave_time = display_date + " " + timeShaper($(timeID).val())
        const note_leave = $(noteID).val()
        
        const headerConfig = this.props.headerConfig

        if(!checkValidate(data[index].t_arrival, latest_leave_time)){ 
            alert("退社時刻は出社時刻より遅くなるように入力してください。")
            return
        }

        const sendData = {
            "date": display_date,
            "latest_leave_time": latest_leave_time,
            "note_leave": note_leave
        }

        //POST通信
        superagent
        .post( this.props.postURL.updateLeave )
        .set( "Content-Type", "application/json" )
        .send( JSON.stringify(sendData) )
        .end(function(err,res){
            // header連動
            if(getToday() == index + 1) { renderTimeCard(headerConfig.jsonURL.getTimeCard,headerConfig.jsonURL.postArrival,headerConfig.jsonURL.postLeave) }
        });

        data[index].t_leave = latest_leave_time;
        data[index].note_leave = note_leave;
        data[index].edit_by_leave_name = this.props.fullName;

        this.setState( () => {
            return { timeData: data };
        });

    }
    render() {
        return (
            <div style={{marginTop: 10}}>
                <table id="table-together" className="table table-bordered" style={{width: tableWidth}}>
                    <thead>
                        <tr>
                            <th className="date">日付</th>
                            <th className="times">出社時刻</th>
                            <th className="times">退社時刻</th>
                            { timeArray.map( (x) => <th style={{width: timeWidth}}>{x}</th> ) }
                        </tr>
                    </thead>
                    <MainTbody
                        timeData={this.props.timeData}
                        arrivalClick={this.arrivalClick}
                        leaveClick={this.leaveClick}
                        arrivalUpdate={this.arrivalUpdate}
                        leaveUpdate={this.leaveUpdate}
                        handleCancel={this.handleCancel}
                    />
                </table>
            </div>
        );
    }      
}

function renderChart(fullName, years, url, postURL, headerConfig) {
    //POST通信
    superagent
    .post( url )
    .set("Content-Type", "application/json")
    .send( JSON.stringify( { "date": years } ) )
    .end(function(err,res){
        render(res.body)
    });
    function render(data){
        data = data.map( (x) => { 
            return Object.assign(x, { input_mode: {arrival: 0, leave: 0} });
        })
        ReactDOM.render(
            <MainTable
                timeData={data}
                postURL={postURL}
                fullName={fullName}
                headerConfig={headerConfig}
            />,
            document.getElementById('id-individual-canvas')
        );
    }
}