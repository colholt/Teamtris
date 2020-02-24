class PlayerCard {
    constructor(player, x, y, scale, shift) {
        // console.log("PlayerCard made");
        this.x = x;
        this.y = y;
        this.player = player;
        this.scale  = scale;
        this.squareArray = [];
        this.sqaureLength = this.scale*(windowHeight / 19) - shift;
        // console.log("AHFEAFEA: " + this.sqaureLength);
        this.w = this.sqaureLength*9;
        this.h = this.sqaureLength*19;
        this.initGrid();
    }

    initGrid() {
        let spaceCalcX = this.x - this.scale*(this.sqaureLength*9 / 2);
        let spaceCalcY = this.y - this.scale*(this.sqaureLength*19 / 2);
        // for (let i = 0; i < 20; i++) {
        //     for (let j = 0; j < 10; j++) {
        //         this.squareArray.push(createVector(j*this.sqaureLength + spaceCalcX, i*this.sqaureLength + spaceCalcY));
        //     }
        // }
    }

    draw() {
        push();
        translate(0,0);
        fill(102,102,255,100)
        // fill("red")
        rect(this.x, this.y, this.w+2*this.sqaureLength, this.h+2*this.sqaureLength);
        pop();
        // this.drawGrid();
    }

    drawGrid() {
        push();
        translate(0,0);
        stroke(56,238,255) //this.squareArray.length - 9
        // for (let i = 0; i < 1; i++) {
        // for (let i = 0; i < this.squareArray.length - 11; i++) {
        //     line(this.squareArray[i].x,this.squareArray[i].y,this.squareArray[i+(11)].x,this.squareArray[i].y)
        //     line(this.squareArray[i].x,this.squareArray[i].y,this.squareArray[i].x,this.squareArray[i+(11)].y)
        //     line(this.squareArray[i].x,this.squareArray[i+(11)].y,this.squareArray[i+(11)].x,this.squareArray[i+(11)].y)
        //     line(this.squareArray[i+(11)].x,this.squareArray[i].y,this.squareArray[i+(11)].x,this.squareArray[i+(11)].y)
        // }
        // let test = 0;
        let test = 0;
        strokeWeight(4)
        line(
            this.squareArray[test].x,
            this.squareArray[test].y,
            this.squareArray[test+(11)].x - ((this.squareArray[test+(11)].x - this.squareArray[test].x)/2.8),
            this.squareArray[test].y
            )
        line(
            this.squareArray[test].x,
            this.squareArray[test].y,
            this.squareArray[test].x,
            this.squareArray[test+(11)].y - ((this.squareArray[test+(11)].y - this.squareArray[test].y)/2.8)
            )
        // line(this.squareArray[test].x,this.squareArray[test+(11)].y,this.squareArray[test+(11)].x,this.squareArray[test+(11)].y)
        // line(this.squareArray[test+(11)].x,this.squareArray[test].y,this.squareArray[test+(11)].x,this.squareArray[test+(11)].y)
        strokeWeight(1)
        test = 8;
        line(this.squareArray[test].x,this.squareArray[test].y,this.squareArray[test+(11)].x,this.squareArray[test].y)
        line(this.squareArray[test].x,this.squareArray[test].y,this.squareArray[test].x,this.squareArray[test+(11)].y)
        line(this.squareArray[test].x,this.squareArray[test+(11)].y,this.squareArray[test+(11)].x,this.squareArray[test+(11)].y)
        line(this.squareArray[test+(11)].x,this.squareArray[test].y,this.squareArray[test+(11)].x,this.squareArray[test+(11)].y)
        pop();
    }
}

module.exports = [PlayerCard];
/** 
Board
* * * * * * * * * *
* * * * * * * * * *
* * * * * * * * * *
* * * * * * * * * *
* * * * * * * * * *
* * * * * * * * * *
* * * * * * * * * *
* * * * * * * * * *
* * * * * * * * * *
* * * * * * * * * *
* * * * * * * * * *
* * * * * * * * * *
* * * * * * * * * *
* * * * * * * * * *
* * * * * * * * * *
* * * * * * * * * *
* * * * * * * * * *
* * * * * * * * * *
* * * * * * * * * *
* * * * * * * * * *
*/
