class NavigationMenu extends React.Component{
    render() {
        return (
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
                    <HeaderDropDown config={this.props.config} />
                </ul>
            </div>
        )
    }
}
