/** 
  * @classDesc A grouping of squares, used for simplifying moving/rotating shapes
  */
class Shape {
	constructor (ID, ShapeBlueprint=null, Color="rand") {
		// ID of the player who 'owns' this square
		this.ID = ID

		// dimensions of the shape blueprint
		
		this.BlueprintDimensions;
		this.ShapeBlueprint;
		// blueprint of the shape
		if (ShapeBlueprint == null) {
			this.BlueprintDimensions = 4;
			this.ShapeBlueprint = this.DefaultShape1()
		} else {
			this.BlueprintDimensions = ShapeBlueprint.length;
			this.ShapeBlueprint = ShapeBlueprint
		}
		

		// calculate Dimensions of the shape in the form [rowTop, colLeft, width, height]
		this.ShapeDimensions = this.CalculateDimensions(this.ShapeBlueprint)
		this.ShapeCenter = this.CalculateCentering()

		// determines if we rotateon the odd or even side of a shape
		this.RotateSign = Math.pow(-1, Number((this.ShapeDimensions[2] > this.ShapeDimensions[3])))
		//this.RotateSign = 1
		// color of all the squares attached to this shape
		if (Color == "rand") {
			this.Color = this.RandomColor()
		} else {
			this.Color = Color
		}

		// List of squares associated with the object
		this.Squares = []
	}

	/** 
     * @description Adds the provided square to this shape's list of squares, and changes the square
	 * to be under ownership of this shape
     * 
     * @param Square - Square object
     * 
     * @return void
     */
	AddSquare(Square) {
		this.Squares.push(Square)
		Square.ChangeOwner(this.ID, this.Color)
	}

	/** 
     * @description Resets the array of squares to an empty array
     * 
     * @return void
     */
	ResetSquares() {
		this.Squares = []
	}

	/** 
     * @description Returns a random color from the list of available colors
     * 
     * @return String
     */
	RandomColor() {
		var Colors = ["red","blue","green","yellow"]
		return Colors[Math.floor(Math.random()*Colors.length)]
	}

	/** 
     * @description Sets all squares associated with this shape to empty
     * 
     * @return void
     */
	RemoveShape() {
		for (var i = 0; i < this.Squares.length; i++) {
			this.Squares[i].SetEmpty()
		}
	}

	/** 
     * @description Sets all the squares associated with this shape to be frozen
     * 
     * @return void
     */
	FreezeSquares() {
		for (var i = 0; i < this.Squares.length; i++) {
			this.Squares[i].SetFrozen()
		}
	}

	/** 
     * @description Moves all squares associated with this shape in the specified directions.
	 * 				
	 * IMPORTANT: Does not check collision
	 * 
	 * @param GameArray - A 2D array of square objects
	 * @param left - How far left the specified shape should move
	 * @param right - How far right the specified shape should move
	 * @param down - How far down the specified shape should move
     * 
     * @return void
     */
	MoveShape(GameArray, left=0, right=0, down=0, reply=true) {
		this.RemoveShape() // remove the shape from the game array
		var boardIndices = []
		var newSquares = []
		for (var k = 0; k < this.Squares.length; k++) {

			boardIndices.push([this.Squares[k].i + down,this.Squares[k].j - left + right])
			newSquares.push(GameArray[this.Squares[k].i + down][this.Squares[k].j - left + right])
			// get the next square
			//this.Squares[k] = GameArray[this.Squares[k].i + down][this.Squares[k].j - left + right]
			
			// set this shape to be the owner of the square
			//this.Squares[k].ChangeOwner(this.ID, this.Color)
		}

		// determine which action to send to the server
		if (reply) {
			if (left != 0) {
				this.SendAction(this.ID, boardIndices, "left")
			} else if (right != 0) {
				this.SendAction(this.ID, boardIndices, "right")
			} else if (down != 0) {
				this.SendAction(this.ID, boardIndices, "down")
			}
		}

		// adopt all new squares
		this.AdoptSquares(newSquares)
	}

	/** 
     * @description Calculates the dimensions of the shape that is contained within the provided 2D array.
     * 
	 * @param arr - Square 2D array representing a shape
	 * 
     * @return Array - Takes the form [Lowest row index, lowest column index, shape width, shape height]
     */
	CalculateDimensions(arr) {
		var rowTop = arr.length
		var rowBot = 0
		var colLeft = arr[0].length
		var colRight = 0
		for (var i = 0; i < arr.length; i++) {
			for (var j = 0; j < arr[0].length; j++) {
			// if the blueprint has a square at this location, we check to see if it is a significant part of the bounding box
				if (arr[i][j] == 1) {
					rowTop = Math.min(rowTop,i)
					colLeft = Math.min(colLeft,j)
					rowBot = Math.max(rowBot,i)
					colRight = Math.max(colRight,j)

				}
			}
		}

		return [rowTop, colLeft, colRight-colLeft+1, rowBot-rowTop+1]
	}


	/** 
     * @description Calculates centering used for shape rotation
	 * 
     * @return integer
     */
	CalculateCentering() {
		return Math.abs(Math.floor(this.ShapeDimensions[2]/2)-Math.floor(this.ShapeDimensions[3]/2))
	}

	/** 
     * @description Returns a list of indices of which the shape would rotate to during a successful
	 * rotation.
	 * 
	 * @param gameArrayRows - Number of rows of the game array
	 * @param gameArrayCols - Number of columns of the game array
	 * 
     * @return Array - Takes the form [2D array of indices, rotated shape blueprint, dimensions of the new shape in the blueprint]
     */
	RotateIndices(gameArrayRows, gameArrayCols) {

		// rotate the existing blueprint of the image
		var RotatedBlueprint = this.RotateShapeBlueprint()

		// calculate Dimensions of the shape in the ShapeBlueprint in the form [rowTop, colLeft, width, height]
		var ShapeDims = this.CalculateDimensions(RotatedBlueprint)

		// find the top left corner of the shape's bounding box
		var rowTop = gameArrayRows // top row of the current shape
		var colLeft = gameArrayCols // leftmost column of the current shape
		for (var k = 0; k < this.Squares.length; k++) {
			rowTop = Math.floor(Math.min(rowTop, this.Squares[k].i))
			colLeft = Math.floor(Math.min(colLeft, this.Squares[k].j))
		}
		
		// calculate average row/col values, then remove the top left corner of the shape's bounding box
		// this provides a physical offset for where the center of the shape should be

		// based off of the bounding box coordiantes, find the new square coordinates
		var squareCoords = []
		for (var i = ShapeDims[0]; i < ShapeDims[0] + ShapeDims[3]; i++) {
			for (var j = ShapeDims[1]; j < ShapeDims[1] + ShapeDims[2]; j++) {
				if (RotatedBlueprint[i][j] == 1) {
					var p1 = rowTop  + i - ShapeDims[0] + this.ShapeCenter*this.RotateSign
					var p2 = colLeft + j - ShapeDims[1] - this.ShapeCenter*this.RotateSign
					squareCoords.push([p1,p2])
	
				}
			}
		}
		return [squareCoords,RotatedBlueprint,ShapeDims]
	}

	/** 
     * @description Changes every square in the provided array to be owned by this shape
	 * 
	 * @param SquareArray - Array of Squares
	 * 
     * @return void
     */
	AdoptSquares(SquareArray){
		this.Squares = SquareArray
		for (var i = 0; i < SquareArray.length; i++) {
			SquareArray[i].ChangeOwner(this.ID, this.Color)
		}
	}

	/** 
     * @description After a successful rotation, update the necessary values for this shape
	 * 
	 * @param newSquares - Array of Squares to be adopted into this shape
	 * @param Blueprint - New, rotated, shape blueprint
	 * @param Dimension - Dimensions of the new, rotated shape
	 * 
     * @return void
     */
	UpdateAfterRotate(newSquares, Blueprint, Dimensions) {
		// update rotation specific completion variables
		this.RotateSign = -1*this.RotateSign

		this.RemoveShape() // remove the shape from the game board
		this.ResetSquares() // set our list of squares to an empty array
		this.AdoptSquares(newSquares) // set this shape as the owner of each square

		// set associated class variables
		this.Squares = newSquares
		this.ShapeBlueprint = Blueprint
		this.ShapeDimensions = Dimensions
	}

	/** 
     * @description Returns an array of the current blueprint after a 90 degree rotation
	 * 
     * @return 2D Array
     */
	RotateShapeBlueprint() {
		// allows for scaling from the typical 4x4 shape bounding box
		var dims = this.ShapeBlueprint.length

		// make new blueprint array
		var NewBlueprint = new Array(dims)
        for (var r = 0; r < dims; r++) {
            NewBlueprint[r] = new Array(dims)
		}
		
		// calculate the rotated blueprint
		for (var i = 0; i < dims; i++) {
			for (var j = 0; j < dims; j++) {
				NewBlueprint[i][j] = this.ShapeBlueprint[dims-1-j][i]
			}
		}

		// set the shape's blueprint to the new rotated blueprint
		return NewBlueprint
	}

	/** 
     * @description Freeze the entire shape
	 * 
     * @return void
     */
	Freeze() {
		this.FreezeSquares() // set all the squares to frozen
		this.ResetSquares() // reset this shape's list of squares
	}

	/** 
     * @description Sends a user action to the server
     * 
     * @param ID - ID of a shape object
     * @param boardIndices - Indices in the game array that the shape will take after performing the action
     * @param action - action the shape is taking
     * 
     * @return void
     */
    SendAction(ID, boardIndices, action) {
        var data = JSON.stringify({"lobbyID":(team.lobbyToken).toLowerCase(),"playerID":ID,"shapeIndices": boardIndices, "move": action})
        socket.send(JSON.stringify({"type": "6", "data": data}))
    }

	/** 
     * @description Used for testing
	 * 
     * @return 2D array
     */
	DefaultShape1 () {
		return [[0,1,0,0],
				[0,1,1,1],
				[0,0,0,0],
				[0,0,0,0]]
	}
}

/* This export is used for testing*/
module.exports = [Shape]
