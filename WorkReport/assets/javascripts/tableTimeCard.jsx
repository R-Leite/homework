const tableFix = (id, maxWidth, rowHeight, fixRows, fixCols) => { 
    const maxHeight = 650;
    const rows = $(id).prop("rows").length;
    const setHeight = (rows) => { if(rows * rowHeight >= maxHeight) { return maxHeight } else { return 0 } };
    $(id).tablefix({ width: maxWidth, height: setHeight(rows), fixRows: fixRows, fixCols: fixCols });
}
class TableHead extends React.Component {
    render() {
        const theadStyle = () => { return {margin: 0, padding: 0, height: 25, paddingBottom: 3} }
        return (
            <thead>
                <tr>
                    <th className="emp-code" rowSpan="3" style={{margin: 0, padding: 0, lineHeight: "75px"}}>社員番号</th>
                    <th className="emp-name" rowSpan="3" style={{margin: 0, padding: 0, lineHeight: "75px"}}>名前</th>
                    {
                        this.props.timeData[0].info.map( (x) => {
                            return (
                                <th colSpan="2" style={theadStyle()}>{String(x.date).substr(8,2)}日</th>
                            )
                        })
                    }
                </tr>
                <tr>
                    {
                        this.props.timeData[0].info.map( (x) => {
                            return (
                                [<th style={theadStyle()}>出社</th>,<th style={theadStyle()}>退社</th>]
                            )
                        })
                    }
                </tr>
                <tr>
                    {
                        this.props.timeData[0].info.map( (x) => {
                            return (
                                [<th style={theadStyle()}>始業</th>,<th style={theadStyle()}>終業</th>]
                            )
                        })
                    }
                </tr>
            </thead>
        )
    }
}

class TableBody extends React.Component {
    constructor(props) {
        super(props)
        this.state = { timeData: this.props.timeData }
    }
    componentWillMount(){
        // 勤務管理対象者の場合、表示するタイムカードの情報を勤務管理対象者に限定する
        if(this.props.isManager){
            const manageMember = this.props.manageData.map((x) => { return x.employee_code})
            const filterData = this.props.timeData.filter((x) => { if(manageMember.indexOf(x.employee_code) != -1) { return true} })
            this.setState( () => { return { timeData: filterData }; });
        }
        else{
            const filterData = this.props.timeData.filter((x) => { if(x.is_left != 1) { return true } })
            this.setState( () => { return { timeData: filterData }; });
        }
    }
    // propsに変更があった場合走る(ここではフィルター項目が変更されたとき)
    componentWillReceiveProps(){   
        
        // フィルターの状態に応じて離職者入りのデータを使うかどうか判定
        const getTimeData = () => {
            if(this.props.filter.group == 0 || this.props.filter.group == "manage") { return this.props.timeData.filter((x) => { if(x.is_left != 1) { return true } })}
            return this.props.timeData
        }

        const originalData = getTimeData();      

        // 選択中の部署に所属しているユーザーの従業員番号
        const divisionUsers = originalData.filter((x) => { 
            if(this.props.filter.division == 0) { return true }
            if(x.internal_division_id == this.props.filter.division){ return true }
        }).map((x) => { return x.employee_code})

        // 選択中のカスタムグループに所属しているユーザーの従業員番号
        const groupMember = this.props.groupData.filter((x) => {
            if(x.group_id == this.props.filter.group){ return true }
        }).map((x) => { return x.employee_code})
        
        // 勤務管理対象者の従業員番号
        const manageMember = this.props.manageData.map((x) => { return x.employee_code})
        
        // 比較対象の判定(カスタムグループフィルターで"勤務管理対象者"を選択しているかどうかで変化)
        const comparisonMember = () => {
            if(this.props.filter.group == "manage"){ return manageMember }
            return groupMember
        }
        
        // タイムカード情報から選択中のカスタムグループに所属しているユーザーを抽出
        const groupUsers = originalData.filter((x) => { 
            if(this.props.filter.group == 0) { return true }
            if(comparisonMember().indexOf(x.employee_code) != -1) { return true }
        }).map((x) => { return x.employee_code})
        
        // 上記の2つの配列の積集合を取得
        const commonData = divisionUsers.filter((x) => { if(groupUsers.indexOf(x) != -1) { return true } });
        
        // タイムカードの情報にフィルターをかける
        const filterData = originalData.filter((x) => {
            if(commonData.indexOf(x.employee_code) != -1) { return true}
        })
        
        // 表示状態を更新
        this.setState( () => {
            return { timeData: filterData };
        });
    }
    render() {
        const concat = (x,y) => { return [x,y] }
        const edited = (first, current, baseClass) => { 
            if(first == current) return baseClass + " no-edit"; else return baseClass + " edited";
        }
        const getDetails = (first, current, edit_by, note) => {
            if(first == current) { return null }
            return "最終編集者：" + edit_by + "\n" + "変更理由：" + note
        }
        const isLeave = (baseClass,is_left) => { if(is_left == 1){ return baseClass + " left-user" } else { return baseClass + " active-user" } }
        const tbodyRow = (data) => {
            const timecardRow = (data) => {
                const timecardCell = (first, current, baseClass, edit_by, note, display, icColor) => {
                    return (
                        <td className={edited(first, current, baseClass)} title={getDetails(first, current, edit_by, note)} style={{margin: 0, padding: 0}} >
                            <p className="display-time">{getTimeValue(current)}</p>
                        </td>
                    )
                }
                return (
                    <tr className={isLeave("",data.is_left)}>
                        <td className={isLeave("emp-code",data.is_left)} rowSpan="2" style={{lineHeight: "60px"}}>{data.employee_code}</td>
                        <td className={isLeave("emp-name",data.is_left)} rowSpan="2" style={{lineHeight: "60px"}}>{data.employee_name}</td>
                        {data.info.map( (x) => concat(timecardCell(x.arrival_time, x.latest_arrival_time, "time-cell", x.edit_by_arrival_name, x.note_arrival,"出社時刻","green"),timecardCell(x.leave_time, x.latest_leave_time, "time-cell", x.edit_by_leave_name, x.note_leave,"退社時刻","blue")))}
                    </tr>
                )
            }
            const attendanceRow = (data) => {
                const attendanceCell = (baseClass, time, display, icColor) => {
                    return (
                        <td className={baseClass} style={{margin: 0, padding: 0}}>
                            <p className="display-time">{getTimeValue(time)}</p>
                        </td>
                    )
                }
                return (
                    <tr className={isLeave("",data.is_left)}>
                        {data.info.map( (x) => concat(attendanceCell("time-cell",x.arrived_at,"始業時刻","red"),attendanceCell("time-cell",x.left_at,"終業時刻","purple")))}
                    </tr>
                )
            }

            return concat(timecardRow(data), attendanceRow(data))
        }
        return (
            <tbody>
            {
                this.state.timeData.map( (data) => {
                    return tbodyRow(data)
                })
            }
            </tbody>
        )
    }
}

class TimecardTable extends React.Component {
    componentDidMount(){
        tableFix("#table-timecard", 1400, 30, 3, 2);
    }
    componentDidUpdate(){
        tableFix("#table-timecard", 1400, 30, 3, 2);
    }
    render() {
        return(
            <div id="base-content">
                <table id="table-timecard" className="table table-bordered">
                    <TableHead
                        timeData={this.props.timeData} 
                    />
                    <TableBody
                        empID={this.props.empID}
                        isManager={this.props.isManager}
                        timeData={this.props.timeData}
                        groupData={this.props.groupData}
                        manageData={this.props.manageData}
                        filter={this.props.filter}
                    />
                </table>
            </div>
        )
    }
}