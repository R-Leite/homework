class HeaderDropDown extends React.Component{
    render() {
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
        return (
            <li className="dropdown">
                <a className="dropdown-toggle" data-toggle="dropdown" role="button" id="username">
                    <img src={this.props.config.img.account} />&nbsp;{this.props.config.fullName}<span className="caret"></span>
                </a>
                <ul className="dropdown-menu" style={{zIndex: 10, position: "absolute"}} >
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
        )
    }
}