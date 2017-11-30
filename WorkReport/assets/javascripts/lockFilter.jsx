class LockFilter extends React.Component {
    render() {
        return (
            <div style={{marginTop: 10, paddingBottom: 10}}>
                <div style={{marginTop: 10}}>
                    <label>ロック状態でフィルタ</label><br />
                    <select className="form-control filter-lock" id="id-select-lock" onChange={this.props.filterChange.bind(this,"division")} >
                        <option value="0">全員表示</option>
                        <option value="0">確定</option>
                        <option value="0">未確定</option>
                    </select>
                </div>
            </div>
        );
    }
}
