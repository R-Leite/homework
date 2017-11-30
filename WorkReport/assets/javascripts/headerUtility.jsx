// 時刻空白時の表示
const emptyDefault = "--:--";

//現在時刻の取得
const getTime = () => {
    const times = new Date();
    return zeroPadding(times.getHours(), 2) + ":" + zeroPadding(times.getMinutes(), 2);
}

//現在日付の取得
const getDate = () => {
    const dates = new Date();
    return dates.getFullYear() + "-" + zeroPadding(dates.getMonth()+1, 2) + "-" + zeroPadding(dates.getDate(), 2);
}

// 現在時刻の取得
const getCurrentTime = () => {
    const WeekChars = [ "(日)", "(月)", "(火)", "(水)", "(木)", "(金)", "(土)" ];
    const dObj = new Date();
    return dObj.getFullYear() + "年" + zeroPadding(dObj.getMonth()+1,2) + "月" + zeroPadding(dObj.getDate(),2) + "日" + WeekChars[dObj.getDay()] + "\u0020" + zeroPadding(dObj.getHours(),2) + ":" + zeroPadding(dObj.getMinutes(),2)
}

// 表示時刻切り出し
const timeSubstr = (datetime) => { 
    return String(datetime).substr(11,5) 
}

// バルーンの削除
const hidePopover = (elementID) => {
    $(elementID).popover("hide");
}

// バルーンの表示
const showPopover = (elementID) => {
    $(elementID).popover("show");
}

// 時刻の存在チェック
const isExistTime = (datetime) => {
    if(datetime == undefined) { return false }
    if(datetime == emptyDefault) { return false }
    if(String(datetime).substr(11,8) == "00:00:59") { return false }
    if(String(datetime).substr(11,8) == "00:00:00") { return false }
    return true
}

// 表示時刻の取得
const getTimeValue = (datetime) => {
    if(datetime == undefined) { return emptyDefault }
    if(String(datetime).substr(11,8) == "00:00:59") { return emptyDefault }
    if(String(datetime).substr(11,8) == "00:00:00") { return emptyDefault }
    return timeSubstr(datetime)
}

// バルーンの表示
const showBalloon = (data) => {
    // 出社無・退社無
    if(!isExistTime(data.arrival) && !isExistTime(data.leave)){ showPopover("#id-arrival-text"); }
    // 出社無・退社有
    if(!isExistTime(data.arrival) && isExistTime(data.leave)){ showPopover("#id-arrival-text"); }
    // 出社有・退社無
    if(isExistTime(data.arrival) && !isExistTime(data.leave)){ showPopover("#id-leave-text"); }
}