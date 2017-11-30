class SubMenu extends React.Component {
    render() {
        return (
            <div className="col-xs-2 col-md-side">
                <h3 className="menu_side_text">サブメニュー</h3>
                <ul className="nav nav-pills nav-stacked menu_side_nav" id="pills-first">
                    <li className="menu_side_nav"><a href="#">&nbsp;&nbsp;サブメニュー項目</a></li>
                    <li className="menu_side_nav"><a href="#">&nbsp;&nbsp;サブメニュー項目</a></li> 
                </ul>
            </div>
        )
    }
}

class MainContents extends React.Component {
    render() {
        return (
            <div className="col-xs-10 col-md-main" id="pills-first">
                <h3 className="menu_side_text">メインコンテンツ</h3>
            </div>
        )
    }
}

// ヘッダー
class PageHeader extends React.Component{
    render(){
        const managerDropDownMenu = () => {
            if(this.props.config.role.isManager == "false") { return null }
            return (
                <li>
                    <a role="menuitem" href={this.props.config.moveURL.manager} id="managedPerson">
                        <img src={this.props.config.img.manager} />&nbsp;勤務管理
                    </a>
                </li>
            )
        }
        const businessDropDownMenu = () => {
            if(this.props.config.role.isBusiness == "false") { return null }
            return (
                <li>
                    <a role="menuitem" href={this.props.config.moveURL.business} id="businessManaged">
                        <img src={this.props.config.img.business} />&nbsp;業務管理
                    </a>
                </li>
            )
        }
        return(
            <nav className="navbar navbar-default navbar-fixed-top">
                <div className="container-fluid">
                    <div className="navbar-header">
                        <a className="navbar-brand" href={this.props.config.moveURL.home} >
                            <img style={{verticalAlign: top}} src={this.props.config.img.home} />
                        </a>
                    </div>
                    <div className="collapse navbar-collapse" id="nav-menu-mini">
                        <ul className="nav navbar-nav navbar-right" role="menu">
                            <li id="current-time">
                                <CurrentTime />
                            </li>
                            <li id="content-timecard">
                                <TimeCard
                                    getTimeCardURL={this.props.config.jsonURL.getTimeCard}
                                    postArrivalURL={this.props.config.jsonURL.postArrival}
                                    postLeaveURL={this.props.config.jsonURL.postLeave}
                                />
                            </li>
                            <li>
                                <a id="move-timecard" href={this.props.config.moveURL.timecardEdit} style={{mergin: 0, padding: 0}} >
                                    <div style={{paddingTop: 13}} title="タイムカード編集">
                                        <img src={this.props.config.img.pencil} alt="編集" width="25" height="25" />
                                    </div>
                                </a>
                            </li>
                            <li className="dropdown">
                                <a className="dropdown-toggle" data-toggle="dropdown" role="button" id="username">
                                    <img src={this.props.config.img.account} />&nbsp;{this.props.config.fullName}<span className="caret"></span>
                                </a>
                                <ul className="dropdown-menu">
                                    <li>
                                        <a role="menuitem" href={this.props.config.moveURL.personalSetting} id="personalData">
                                            <img src={this.props.config.img.setting} />&nbsp;個人設定
                                        </a>
                                    </li>
                                    {managerDropDownMenu()}
                                    {businessDropDownMenu()}
                                    <li>
                                        <a role="menuitem" href={this.props.config.moveURL.version} id="versionCheck">
                                            <img src={this.props.config.img.version} />&nbsp;バージョン情報
                                        </a>
                                    </li>
                                    <li className="divider"></li>
                                    <li>
                                        <a role="menuitem" href={this.props.config.moveURL.logout} id="logut">
                                            <img src={this.props.config.img.logout} />&nbsp;ログアウト
                                        </a>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
        )
    }
}
// メイン
class PageMain extends React.Component{
    render(){
        return(
            <div className="row">
                <SubMenu config={this.props.submenuConfig}/>
                <MainContents config={this.props.mainConfig} />
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