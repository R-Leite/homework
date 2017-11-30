
// サブメニュー
class SubMenu extends React.Component {
    render() {
        const moveShowLink = () => {
            if(this.props.config.role.isBusiness != "true") { return null }
            return (
                <li className="menu_side_nav"><a id="timecard_show" href={this.props.config.moveURL.timecardShow}>&nbsp;&nbsp;タイムカード確認</a></li>
            )
        }
        return (
            <div className="col-xs-2 col-md-side">
                <h3 className="menu_side_text">タイムカード</h3>
                <ul className="nav nav-pills nav-stacked menu_side_nav" id="pills-first">
                    <li className="menu_side_nav"><a id="timecard_edit" href={this.props.config.moveURL.timecardEdit}>&nbsp;&nbsp;タイムカード編集</a></li>
                    {moveShowLink()}
                </ul>
            </div>
        )
    }
}

// メインコンテンツ
class MainContents extends React.Component {
    constructor(props) {
        super(props);
        this.state = { timeData: {}, divisionData: {}, groupData: {}, manageData: {}, filter: { division: 0 , group: 0 } };
        this.filterChange = this.filterChange.bind(this);
    }
    componentWillMount(){
        $.ajaxSetup({ async: false });
        // 各種JSONデータの取得
        const timeData     =  $.getJSON(this.props.config.jsonURL.getTimeCard).responseJSON
        const divisionData =  $.getJSON(this.props.config.jsonURL.getInternalDivision).responseJSON
        const groupData    =  $.getJSON(this.props.config.jsonURL.getCustomGroup).responseJSON
        const manageData   =  $.getJSON(this.props.config.jsonURL.getManageList).responseJSON
        // 下位コンポーネントへ反映
        this.setState( () => { return { timeData: timeData } } )
        this.setState( () => { return { divisionData: divisionData } } )
        this.setState( () => { return { groupData: groupData } } )
        this.setState( () => { return { manageData: manageData } } )
        if(this.props.config.role.isManager == "true"){
            const filterState = this.state.filter;
            filterState["group"] = "manage";
            this.setState( () => {
                return { filter: filterState };
            });
        }
    }
    // フィルターの変更を検知
    filterChange(key,e){
        const filterState = this.state.filter;
        filterState[key] = e.target.value;
        this.setState( () => {
            return { filter: filterState };
        });
    }
    render() {
        return (
            <div className="col-xs-10 col-md-main" id="pills-first">
                <h3 id="page-title" className="text-left page_title"><img src={this.props.config.img.title} />&nbsp;タイムカード確認</h3>
                <div><label>表示させたい年月を選択して下さい</label></div>
                <DatePicker config={this.props.config.datepicker.config} />
                <CommonFilter
                    empID={this.props.config.empID}
                    isManager={this.props.config.role.isManager}
                    isBusiness={this.props.config.role.isBusiness}
                    divisionData={this.state.divisionData}
                    groupData={this.state.groupData}
                    filterChange={this.filterChange}
                />
                <div id="fix-content"></div>
                <TimecardTable
                    empID={this.props.config.empID}
                    isManager={this.props.config.role.isManager}
                    timeData={this.state.timeData}
                    groupData={this.state.groupData}
                    manageData={this.state.manageData}
                    filter={this.state.filter}
                />
            </div>
        )
    }
}

// ヘッダー
class PageHeader extends React.Component{
    render(){
        return(
            <nav className="navbar navbar-default navbar-fixed-top">
                <div className="container-fluid">
                    <NavigationHeader config={this.props.config} />
                    <NavigationMenu config={this.props.config} />
                </div>
            </nav>
        )
    }
}

// メイン
class PageMain extends React.Component{
    render(){
        return(
            <div className="container left-approach" style={{width: 1800}}>
                <div className="row">
                    <SubMenu config={this.props.submenuConfig}/>
                    <MainContents config={this.props.mainConfig}/>
                </div>
            </div>
        )
    }
}

// フッター
class PageFooter extends React.Component{
    render(){
        return(
            <nav className="navbar navbar-default navbar-fixed-bottom" role="navigation">
                <div className="container-fluid">
                    <div className="col-xs-12">
                        <div className="navbar-footer">
                            <p> © 2017 住精エンジニアリング株式会社</p>
                        </div>
                    </div>
                </div>
            </nav>
        )
    }
}

// 基本ページ
class BasePage extends React.Component{
    render(){
        return(
            <div>
                <PageHeader config={this.props.headerConfig} />
                <PageMain submenuConfig={this.props.submenuConfig} mainConfig={this.props.mainConfig}/>
                <PageFooter />
            </div>
        )
    }
}

// bodyのレンダリング
function renderPageBody(headerConfig, submenuConfig, mainConfig){
    ReactDOM.render(
        <BasePage
            headerConfig={headerConfig}
            submenuConfig={submenuConfig}
            mainConfig={mainConfig}
        />,
        document.getElementById("id-page-body")
    );
}