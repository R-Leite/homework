class CommonFilter extends React.Component {
    render() {
        const commonOption = () => {
            if(this.props.isBusiness != "true"){
                return null
            }
            return <option value="0">全員表示</option>
        }
        const managerOption = () => {
            if(this.props.isManager != "true"){
                return null
            }
            return <option value="manage" selected>勤務管理対象者</option>
        }
        return (
            <div style={{marginTop: 10, paddingBottom: 10}}>
                <div style={{marginTop: 10}}>
                    <label>所属グループでフィルタ</label><br />
                    <select className="form-control filter-group" id="id-select-type" onChange={this.props.filterChange.bind(this,"division")} >
                        <option value="0">全員表示</option>
                        {
                            this.props.divisionData.map( (x) => {
                                return <option value={x.id}>{x.name}</option>
                            })
                        }
                    </select>
                </div>
                <div style={{marginTop: 10}}>
                    <label>カスタムグループでフィルタ</label><br />
                    <select className="form-control filter-custom-group" id="id-select-custom" onChange={this.props.filterChange.bind(this,"group")}>
                        {commonOption()}
                        {managerOption()}
                        {
                            this.props.groupData
                            .filter((x) => {if(x.owner_user_id == this.props.empID) return true})
                            .filter(distinct(["group_id"]))
                            .map( (x) => {
                                return <option value={x.group_id}>{x.group_name}</option>
                            })
                        }
                    </select>
                </div>
            </div>
        );
    }      
}
