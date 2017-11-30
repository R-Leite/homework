class DatePicker extends React.Component{
    
    constructor(props) {
        super(props);
        this.handleClick = this.handleClick.bind(this);
    }
    
    handleClick(e) {
        // datepicker起動
        $("#display-date").datepicker('show');
    }

    render() {
        return (
            <div>
                <div className="input-group" id="display-date_field">
                    <form id="date-form" action={this.props.config.action} method="POST" className="form-inline" role="form" style={{marginBottom: 0}} >
                        <input type="text" id="display-date" name="display-date" className="form-control component-datepicker" style={this.props.config.customStyle} value={this.props.config.default} onClick={this.handleClick} readonly></input>
                        <span className="input-group-addon btn component-datepicker" id="open-datetimepicker" onClick={this.handleClick}><span className="glyphicon glyphicon-calendar"></span></span>
                    </form>
                </div>
            </div>
        );
    }
    //レンダリング後に走る処理
    componentDidMount() {
        const url = this.props.config.action
        // datepicker設定
        $('#display-date').datepicker({
            language: this.props.config.language,
            format: this.props.config.format,
            startDate: this.props.config.startDate,
            minViewMode : this.props.config.minViewMode,
            autoclose: this.props.config.autoclose,
            beforeShowDay: function(date) {
                var myDate = new Object();
                if(date.getDay() == 0) { myDate.classes = 'class-sunday'  }
                if(date.getDay() == 6) { myDate.classes = 'class-saturday'}
                return myDate;
            }
        }).on("changeDate", function(e){
            $("#date-form").submit();
        });
    }
}

//datepickerのレンダリング
function renderDatePicker(years, config){
    ReactDOM.render(
        <DatePicker years={years} config={config} />,
        document.getElementById("content-datepicker")
    );
}
