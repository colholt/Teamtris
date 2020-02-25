class HighLight {
	constructor (i, j, ID, color, SquareEdgeLength) {
    this.i = i;
    this.j = j;
    this.ID = ID;
    this.color = color;
    this.SquareEdgeLength = SquareEdgeLength;
	}

	draw() {
    push();
    stroke(200,200,200,100)
    noFill();
  	fill(this.color)
  	rect(this.i * this.SquareEdgeLength, this.j * this.SquareEdgeLength, this.SquareEdgeLength, this.SquareEdgeLength)
    pop();
	}

  checkCollision(arr, BoardSquareSize) {
		arr.sort(function(a, b){return b.j-a.j});
    for(let i = 0; i < arr.length; i++){
      if(arr[i].j == this.j+1 && arr[i].i == this.i) {
        console.log("HERE222");
        if(arr[i].ID == 1){
          this.changeOwner(arr);
          console.log("FALSEss");
          return false;
        }
      }
    }
    if(this.ID == 1) {
      console.log("HERE");
      return false;
    }
    if(this.j+1 > BoardSquareSize[0]-1){
      this.changeOwner(arr);
      return false;
    }
    return true;
  }

  changeOwner(arr) {
    let oldID = this.ID
    // console.log(arr);
  	for(let j = 0; j < arr.length; j++){
			if(arr[j].ID == oldID){
				arr[j].ID = 1;
        arr[j].color = "yellow"
			}
  	}
  }
}
