// 勤務管理対象者表示用のセレクトボックス
class ManagementListBox extends React.Component{
    constructor(props) {
        super(props);
        this.state = {manageUserList: [], currentUser: this.props.config.currentUser}
    }
    componentWillMount(){
        $.ajaxSetup({ async: false });
        const data = $.getJSON(this.props.config.url, function(data){ return data })
        console.log(data)
        /*
        this.setState( () => {
            return { manageUserList: data}
        });
        */
    }
    componentDidMount(){
        $("#select_user").unbind("change");
        $("#select_user").on("change", function(){
            $("#date-form").submit();
        })
    }
    render() {
        const listBox = () => {
            if(this.state.manageUserList.length <= 1){
                console.log("勤務管理対象者なし")
            }
            console.log("勤務管理対象者あり")
        }
        return (
            <div id="change-user" class="form-group">
                <select id='select_user' name='select_user' className='form-control' >
                    <option value="1">testUser1</option>
                    <option value="2">testUser2</option>
                    <option value="3">testUser3</option>
                </select>
                {listBox()}
            </div>
        );
    }
}

// 勤務管理対象者表示用のセレクトボックス
function renderManagementListBox(config){
    ReactDOM.render(
        <ManagementListBox config={config} />,
        document.getElementById("content-managementListBox")
    );
}
/*
<html>
    <head></head>
    <body>
        <script>
        $.ajaxSetup({ async: false, cache: false });
        var href = window.location.href;
        var serch_str = "/attendance/";
        var emp_code = href.substr(href.indexOf(serch_str) + serch_str.length, 5);
        //Jsonデータ取得
        $.getJSON("/json/manager/managelist", function (data) {
            var html = "";
            if(data.length >= 2)
            {
                html += "<select id='select_user' name='select_user' class='form-control' onchange='this.form.submit()'>";
                for(var i in data)
                {
                    if(emp_code == data[i].employee_code)
                    {
                        html += "<option value='"+data[i].employee_code+"' selected>"+data[i].employee_code+"："+data[i].employee_name+"</option>";    
                    }
                    else
                    {
                        html += "<option value='"+data[i].employee_code+"'>"+data[i].employee_code+"："+data[i].employee_name+"</option>";
                    }
                }
                html += "</select>";
                document.getElementById("change-user").innerHTML = html;
            }
        });
        </script>
    </body>
</html>
*/
