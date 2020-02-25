class Square {
	constructor (SquareEdgeLength, ID=0, Color="grey", DefaultGridStroke=100) {
		// edge length of the square
		this.SquareEdgeLength = SquareEdgeLength

		// ID of the player who 'owns' this square
		this.ID = ID

		// Color of the square
		this.DefaultColor = "grey";
		this.Color = Color

		// edge color of the square
		this.DefaultGridStroke = DefaultGridStroke

		// i, j location of the square in the game array
		this.i = -1
		this.j = -1
	}

	// Set the position of the quare
	SetPosition(i,j) {
		this.i = i
		this.j = j
	}

	// Sets this to be an empty square
	SetEmpty() {
		this.ID = 0
		this.Color = this.DefaultColor
	}

	SetFrozen() {
		this.ID = 5
		//this.Color = "black"
	}

	IsFrozen() {
		return this.ID == 5
	}

	// returns true if the ID of this square is 0
	IsEmpty() {
		return (this.ID == 0)
	}

	// Draw the square at it's (i,j) position.
	Draw() {
		push();
		stroke(this.DefaultGridStroke)
		if (this.ID == 0) {
			noFill()
		} else {
			fill(this.Color)
		}
		rect(this.j * this.SquareEdgeLength, this.i * this.SquareEdgeLength, this.SquareEdgeLength, this.SquareEdgeLength)
		pop();
	}

	// change the owner of the square.
	ChangeOwner(ID, Color) {
		this.ID = ID
		this.Color = Color
	}
}
