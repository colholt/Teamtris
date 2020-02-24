class Player {
    constructor(username, id, owner){
        this.username = username;
        this.id = id;
        this.owner = owner;
        this.playerNum;
    }
    setPlayerNum(num) {
        this.playerNum = num;
    }
}

module.exports = [Player]