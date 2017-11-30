class NavigationHeader extends React.Component{
    render() {
        return (
            <div className="navbar-header">
                <a id="work-report" className="navbar-brand" href={this.props.config.moveURL.home} >
                    <img style={{verticalAlign: top}} src={this.props.config.img.home} />
                </a>
            </div>
        )
    }
}