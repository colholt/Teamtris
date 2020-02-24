class Team {
    constructor(){
        this.playersInTeam = [];
        this.teamName = "";
        this.lobbyToken = "";
    }
    addPlayer(player){
        this.playersInTeam.push(player);
    }
}

module.exports = [Team]