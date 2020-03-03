

// #class Team |
// @author Steven Dellamore, Richard Hansen |
// @desc The team class will contain all the
// other players that are in your game, the team name
// and the token for your lobby. Once new players come
// addPlayer will be called to push a newplayer onto
// the playersInTeam array. |
class Team {

    // #function Team::constructor |
    // @author Steven Dellamore |
	// @desc The constructor gets called anytime
    // someone joins or create a game. |
    // @header constructor() | 
	// @param void: no parameters |
	// @returns Team : A object of the class | 
    constructor(){
        this.playersInTeam = [];
        this.teamName = "";
        this.lobbyToken = "";
    }

    // #function Team::addPlayer |
    // @author Steven Dellamore |
	// @desc The add player function gets called 
    // whenever a bot or a real player joins your lobby.
    // This function will also be called to populate the lobby 
    // when you join. |
    // @header addPlayer(player) | 
	// @param Player player: This parameter
    // is the new player/bot that is joining your team. |
	// @returns void : no return | 
    addPlayer(player){
        this.playersInTeam.push(player);
    }
}

module.exports = [Team]