class LockTableHeader extends React.Component {
    render() {
        return (
            <thead>
                <tr>
                    <th className="emp-code" rowSpan="3">社員番号</th>
                    <th className="emp-name" rowSpan="3">名前</th>
                    <th className="emp-name" rowSpan="3">府が</th>
                </tr>
            </thead>
        )
    }
}

class LockTableBody extends React.Component {
    constructor(props) {
        super(props)
    }
    render() {
        return (
            <tbody>
                <tr>
                    <td>hoge</td>
                    <td>hoge</td>
                    <td>hoge</td>
                </tr>
            </tbody>
        )
    }
}

class LockTable extends React.Component {
    render() {
        return(
            <div id="base-content">
                <table id="" className="table table-bordered">
                    <LockTableHeader
                    />
                    <LockTableBody
                    />
                </table>
            </div>
        )
    }
}
