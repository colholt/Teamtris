/** 
  * @classDesc Represents lowest functional unit of teamtris
  */
class Square {
	constructor (SquareEdgeLength, ID=0, Color="grey") {
		// edge length of the square
		this.SquareEdgeLength = SquareEdgeLength

		// ID of the player who 'owns' this square
		this.ID = ID

		// Color of the square
		this.DefaultColor = "grey";
		this.Color = Color

		// edge color of the square
		//this.DefaultGridStroke = DefaultGridStroke

		// i, j location of the square in the game array
		this.i = -1
		this.j = -1
	}

	/** 
     * @description Sets class variables representing this square's position in the game array
	 * 
	 * @param i - row index
	 * @param j - column index
	 * 
     * @return void
     */
	SetPosition(i,j) {
		this.i = i
		this.j = j
	}

	/** 
     * @description Sets this square to be an empty square
	 * 
     * @return void
     */
	SetEmpty() {
		this.ID = 0
		this.Color = this.DefaultColor
	}

	/** 
     * @description Sets this square to be a frozen square
	 * 
     * @return void
     */
	SetFrozen() {
		this.ID = 5
		this.Color = "grey"
	}

	/** 
     * @description Returns true if this square is frozen
	 * 
     * @return boolean
     */
	IsFrozen() {
		return this.ID == 5
	}

	/** 
     * @description Returns true if this square is empty (ID == 0)
	 * 
     * @return boolean
     */
	IsEmpty() {
		return (this.ID == 0)
	}

	/** 
     * @description Changes the owner of the square
	 * 
	 * @param ID - ID of the owner of the square
	 * @param Color - color to set the square to
	 * 
     * @return boolean
     */
	ChangeOwner(ID, Color) {
		this.ID = ID
		this.Color = Color
	}

	/** 
     * @description Called everytime we want to draw a square
	 * 
     * @return void
     */
	Draw() {
		push();
		stroke("grey")
		strokeWeight(1)
		if (this.ID == 0) {
			noFill()
		} else {
			fill(this.Color)
		}
		rect(this.j * this.SquareEdgeLength, this.i * this.SquareEdgeLength, this.SquareEdgeLength, this.SquareEdgeLength)
		pop();
	}
}

/* This export is used for testing*/
module.exports = [Square]
