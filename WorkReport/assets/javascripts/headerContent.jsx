// datePicker
class DatePicker extends React.Component{
    constructor(props) {
        super(props);
        this.handleClick = this.handleClick.bind(this);
    }
    handleClick(e) {
        $("#display-date").datepicker('show');
    }
    render(){
        return (
            <div className="col-xs-6" id="content-datepicker">
                <div className="input-group" id="display-date_field">
                    <input type="text" id="display-date" name="display-date" className="form-control component-datepicker" value={this.props.config.defaultValue} onClick={this.handleClick} readonly></input>
                    <span className="input-group-addon btn component-datepicker" id="open-datetimepicker" onClick={this.handleClick}><span className="glyphicon glyphicon-calendar"></span></span>
                </div>
            </div>
        )
    }
    componentDidMount() {
        $('#display-date').datepicker({
            language: this.props.config.language,
            format: this.props.config.format,
            startDate: this.props.config.startDate,
            minViewMode : this.props.config.minViewMode,
            autoclose: this.props.config.autoclose,
            beforeShowDay: function(date) {
                const myDate = new Object();
                if(date.getDay() == 0) { myDate.classes = 'class-sunday'  }
                if(date.getDay() == 6) { myDate.classes = 'class-saturday'}
                return myDate;
            }
        }).on("changeDate", this.props.config.handleChange)
    }
}
// 勤務管理対象者のセレクトボックス
class ManagementListBox extends React.Component{
    constructor(props) {
        super(props);
        this.state = { manageUserList: [] }
    }
    componentWillMount(){
        var manageData = [];
        $.ajaxSetup({ async: false });
        const data = $.getJSON(this.props.config.url, function(x){ 
            manageData = x;
        })
        this.setState( () => { return { manageUserList: manageData} });
        console.log(manageData);
    }
    render(){
        const getEmpCode = () => {
            const href = window.location.href;
            const serch_str = this.props.config.search;
            const empCode = href.substr(href.indexOf(serch_str) + serch_str.length, 5);
            return empCode;
        }
        const listBox = () => {
            if(this.state.manageUserList.length <= 1){
                return <input type="hidden" name="select_user" value={getEmpCode()}></input>
            }
            return (
                <select id='select_user' name='select_user' className='form-control' onChange={this.props.config.handleChange}>
                    {
                        this.state.manageUserList.map( (x) => {
                            if(x.employee_code == getEmpCode()) { 
                                return <option value={x.employee_code} selected>{x.employee_code + "：" + x.employee_name}</option>
                            }
                            return <option value={x.employee_code}>{x.employee_code + "：" + x.employee_name}</option>
                        })
                    }
                </select>
            )
        }
        return (
            <div className="col-xs-6" id="content-managementListBox">
                <div id="change-user" class="form-group">
                    {listBox()}
                </div>
            </div>
        )
    }
}
// メインコンテンツ
class HeaderContent extends React.Component{
    constructor(props) {
        super(props);
        this.handleChange = this.handleChange.bind(this);
    }
    handleChange(e){
        $("#date-form").submit();
    }
    render(){
        const datePickerConfig = {
            language: "ja",
            format: "yyyy-mm",
            startDate: "2017-04",
            minViewMode : 1,
            autoclose: true,
            defaultValue: this.props.years,
            handleChange: this.handleChange
        }
        const managementListConfig = {
            url: this.props.getManageList,
            search: "/edit/",
            handleChange: this.handleChange
        }
        return (
            <div className="row">
                <form id="date-form" action={this.props.action} method="POST" class="form-inline" role="form">
                    <DatePicker config={datePickerConfig} />
                    <ManagementListBox config={managementListConfig} />
                </form>
            </div>
        )
    }
}

function renderHeaderContent(action, years, getManageList){
    ReactDOM.render(
        <HeaderContent action={action} years={years} getManageList={getManageList} />,
        document.getElementById("content-header")
    );
}