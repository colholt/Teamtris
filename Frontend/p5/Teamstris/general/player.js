// #class Player |
// @author Steven Dellamore, Richard Hansen |
// @desc Every user will have their own object of the Player class.
// This is going to be passed around to other people in the lobby.
// This class will tell the game screen who is who and will help identify 
// moves. |
class Player {

    // #function Player::constructor |
    // @author Steven Dellamore |
    // @desc The constructor takes in three things, a name, id 
    // and a owner flag. It will then create an object of @inline{Player}
    // and init all class varibles. This Class is used throughout all stages
    // of the program. | 
	// @param String username : username of the new Player |
    // @param int id : id, @inline{[0,4]}, of the new player. |
    // @param boolean owner : @inline{true} or @inline{false} if they are owner |
	// @returns Player : An object of Player class | 
    // @header constructor(username, id, owner) |
    // #code PlayerContor javascript
    constructor(username, id, owner){
        this.username = username;
        this.id = id;
        this.owner = owner;
        this.playerNum;
    }
    //|

    // #function Player::setPlayerNum |
    // @author Steven Dellamore |
    // @desc Will set @inline{this.playerNum} equal to @inline{num}. This
    // is just a helper function. | 
	// @param int num : sets the @inline{this.playerNum = num} |
	// @returns void : returns nothing | 
    // @header setPlayerNum(num) |
    setPlayerNum(num) {
        this.playerNum = num;
    }
}

module.exports = [Player]