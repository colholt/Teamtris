class Team {
    constructor(){
        this.playersInTeam = [];
        this.teamName = "";
    }
    addPlayer(player){
        this.playersInTeam.push(player);
    }
}