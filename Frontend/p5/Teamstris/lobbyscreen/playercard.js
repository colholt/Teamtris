class PlayerCard {
    constructor(player, x, y, w, h) {
        console.log("PlayerCard made");
        this.w = w;
        this.h = h;
        this.x = x;
        this.y = y;
        this.player = player;

        this.squareArray = [];
        if(windowHeight > windowWidth){
            this.sqaureLength = windowWidth / 20;
        } else {
            this.sqaureLength = windowHeight / 20;
        }
        this.initGrid();
    }

    initGrid() {
        let spaceCalc = (windowWidth - (this.sqaureLength * (9))) / 2;
        for (let i = 0; i < 20; i++) {
            for (let j = 0; j < 10; j++) {
                this.squareArray.push(createVector(j*this.sqaureLength + spaceCalc, i*this.sqaureLength + windowHeight/40));
            }
        }
        // console.log(this.squareArray)
    }

    draw() {
        push();
        translate(windowWidth/2, windowHeight/2);
        fill(102,102,255,100)
        // fill("red")
        rect(this.x, this.y, this.w, this.h);
        pop();
        // this.drawGrid();
    }

    drawGrid() {
        push();
        translate(0,0);
        stroke("red") //this.squareArray.length - 9
        console.log("AHH")
        console.log(this.squareArray[0].x)
        for (let i = 0; i < this.squareArray.length - 9; i++) {
            line(this.squareArray[i].x,this.squareArray[i].y,this.squareArray[i+(11)].x,this.squareArray[i].y)
            line(this.squareArray[i].x,this.squareArray[i].y,this.squareArray[i].x,this.squareArray[i+(11)].y)
            line(this.squareArray[i].x,this.squareArray[i+(11)].y,this.squareArray[i+(11)].x,this.squareArray[i+(11)].y)
            line(this.squareArray[i+(11)].x,this.squareArray[i].y,this.squareArray[i+(11)].x,this.squareArray[i+(11)].y)
        }
        pop();
    }
}
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
