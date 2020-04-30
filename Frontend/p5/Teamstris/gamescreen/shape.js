/** 
  * @classDesc A grouping of squares, used for simplifying moving/rotating shapes
  */
class Shape {
	constructor (ID, ShapeBlueprint=null, Color="rand", time=1, sendToServer=true) {
		// ID of the player who 'owns' this square
		this.ID = ID

		// dimensions of the shape blueprint
		
		this.BlueprintDimensions;
		this.ShapeBlueprint;
		// blueprint of the shape
		if (ShapeBlueprint == null) {
			this.BlueprintDimensions = 4;
			this.ShapeBlueprint = this.GenerateRandomShape(time)
		} else {
			this.BlueprintDimensions = ShapeBlueprint.length;
			this.ShapeBlueprint = ShapeBlueprint
		}

		if (sendToServer) {
			this.SendNewShape(this.ID, this.ShapeBlueprint)
		}
		
		// calculate Dimensions of the shape in the form [rowTop, colLeft, width, height]
		this.ShapeDimensions = this.CalculateDimensions(this.ShapeBlueprint)
		this.ShapeCenter = this.CalculateCentering()

		// determines if we rotateon the odd or even side of a shape
		this.RotateSign = Math.pow(-1, Number((this.ShapeDimensions[2] > this.ShapeDimensions[3])))
		
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
		Square.ChangeOwner(this.ID, this.Color, Square.PowerCubeType)
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
		return Colors[this.ID-1]
		//return Colors[Math.floor(Math.random()*Colors.length)]
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
		var boardIndices = []
		var newSquares = []
		for (var k = 0; k < this.Squares.length; k++) {
			boardIndices.push([this.Squares[k].i + down, this.Squares[k].j - left + right, this.Squares[k].PowerCubeType])

			newSquares.push([this.Squares[k].i + down, this.Squares[k].j - left + right, this.Squares[k].PowerCubeType])
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
		this.RemoveShape() // remove the current shape from the game array
		//this.ResetSquares()
		this.AdoptSquareIndices(GameArray, newSquares) // adopt all new squares
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
				if (arr[i][j] != 0) {
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
				if (RotatedBlueprint[i][j] != 0) {
					var p1 = rowTop  + i - ShapeDims[0] + this.ShapeCenter*this.RotateSign
					var p2 = colLeft + j - ShapeDims[1] - this.ShapeCenter*this.RotateSign
					squareCoords.push([p1,p2,RotatedBlueprint[i][j]])
	
				}
			}
		}
		return [squareCoords,RotatedBlueprint,ShapeDims]
	}

	/** 
     * @description Changes every square in the provided array to be owned by this shape
	 * 
	 * @param GameArray - 2D array of squares
	 * @param SquareArray - Array of Squares indices
	 * 
     * @return void
     */
	AdoptSquareIndices(GameArray, SquareArray){
		this.ResetSquares()
		for (var i = 0; i < SquareArray.length; i++) {
			var newSquare = GameArray[SquareArray[i][0]][SquareArray[i][1]]
			newSquare.ChangeOwner(this.ID, this.Color, SquareArray[i][2])
			this.Squares.push(newSquare)
		}
	}

	/** 
     * @description Changes every square in the provided array to be owned by this shape
	 * 
	 * @param SquareArray - Array of Squares
	 * 
     * @return void
     */
	AdoptSquares(SquareArray) {
		this.Squares = SquareArray
		for (var i = 0; i < this.Squares.length; i++) {
			this.Squares[i].ChangeOwner(this.ID,this.Color,this.Squares[i].PowerCubeType)
		}
	}

	/** 
     * @description After a successful rotation, update the necessary values for this shape
	 * 
	 * @param GameArray - 2D array of squares
	 * @param newSquares - Array of square indices
	 * @param Blueprint - New, rotated, shape blueprint
	 * @param Dimension - Dimensions of the new, rotated shape
	 * 
     * @return void
     */
	UpdateAfterRotate(GameArray, newSquares, Blueprint, Dimensions) {
		// update rotation specific completion variables
		this.RotateSign = -1*this.RotateSign

		this.RemoveShape() // remove the shape from the game board
		this.ResetSquares()
		this.AdoptSquareIndices(GameArray, newSquares) // set this shape as the owner of each square

		// set associated class variables
		//this.Squares = newSquares
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
     * @return Set of rows that were impacted by the freeze (used for checking if a row should be removed)
     */
	Freeze(sendToServer=true) {
		var boardIndices = []
		var rowsImpacted = new Set()
		for (var k = 0; k < this.Squares.length; k++) {
			boardIndices.push([this.Squares[k].i, this.Squares[k].j, this.Squares[k].PowerCubeType])
			rowsImpacted.add(this.Squares[k].i)
		}
		if (sendToServer) {
			this.SendAction(this.ID, boardIndices, "freeze")
		}
		this.FreezeSquares() // set all the squares to frozen
		this.ResetSquares() // reset this shape's list of squares
		return rowsImpacted
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
		if (!team) {
			return
		}
		if (action == "freeze") {
			console.log("SENDING FREEZE TO SERVER")
		}
		
        var data = JSON.stringify({"lobbyID":team.lobbyToken.toLowerCase(),"playerID":ID,"shapeIndices": boardIndices, "move": action})
        socket.send(JSON.stringify({"type": "6", "data": data}))
	}

	/** 
     * @description Sends a newly created shape to the server
     * 
     * @param ID - ID of a shape object
     * @param shapeBlueprint - The blueprint the new shape will be instantiated using
     * 
     * @return void
     */
    SendNewShape(ID, shapeBlueprint) {
        if (!team) {
			console.log("no team")
            return
        }
        var data = JSON.stringify({"lobbyID":team.lobbyToken.toLowerCase(),"playerID":ID,"shapeBlueprint": shapeBlueprint})
        socket.send(JSON.stringify({"type": "11", "data": data}))
    }
	
	/** 
     * @description Generates the shape blueprint for a random shape
     * 
     * @return shape blueprint (2D array)
     */
	GenerateRandomShape(gameTime=1, eps=0.05, max=4) {
		var randShape = new Array(this.BlueprintDimensions)
        for (var r = 0; r < this.BlueprintDimensions; r++) {
            randShape[r] = new Array(this.BlueprintDimensions).fill(0)
		}

		// get the size of the shape we will generate
		//var shapeSizes = [1,2,2,2,3,3,3,4,4,4,5,5,5,6,6,7,7,8,8,9,9,10]
		var shapeSizes = [2,2,2,3,3,3,4,4,5,5]
		var count = shapeSizes[Math.min(Math.floor(Math.random() * shapeSizes.length) + (Math.floor(Math.log10(Math.log10(gameTime)))),shapeSizes.length-1)]
		
		var curri = Math.min(Math.floor(Math.random() * this.BlueprintDimensions),this.BlueprintDimensions-1)
		var currj = Math.min(Math.floor(Math.random() * this.BlueprintDimensions),this.BlueprintDimensions-1)
		randShape[curri][currj] = Math.random() < eps ? 2 : 1
		count--
		var priorityList = [0,0,0,0]
		while(count > 0) {
			priorityList[0] = this.GetNewSquarePriority(curri,currj-1, randShape)
			priorityList[1] = this.GetNewSquarePriority(curri-1,currj, randShape)
			priorityList[2] = this.GetNewSquarePriority(curri,currj+1, randShape)
			priorityList[3] = this.GetNewSquarePriority(curri+1,currj, randShape)
			var maxPriority = Math.max(priorityList)
			var chosenInd = this.argmax(priorityList)

			if (maxPriority == 0) {
				count = 0
				break
			} else {
				curri += (chosenInd == 1 ? -1 : 0) + (chosenInd == 3 ? 1 : 0)
				currj += (chosenInd == 0 ? -1 : 0) + (chosenInd == 2 ? 1 : 0)
				curri = Math.min(Math.max(0, curri),this.BlueprintDimensions-1)
				currj = Math.min(Math.max(0, currj),this.BlueprintDimensions-1)
				randShape[curri][currj] = Math.random() < eps ? Math.max(2,Math.floor(Math.random() * max)) : 1
			}
			count--;
		}
		
		// this function can potentially go back to squares that have already been visited.
		return randShape
	}

	/** 
     * @description Given a pair of indices, returns a random priority value, or 0 if the provided indices is out of bound
     * 
     * @return double
     */
	GetNewSquarePriority(i, j, arr) {
		if (i < 0 || i >= this.BlueprintDimensions || j < 0 || j >= this.BlueprintDimensions || arr[i][j] == 1) {
			return 0;
		}
		return Math.random()
	}

	/** 
	 * @description - Returns the argmax of an array
	 *  
	 * @param arr - array
	 * 
     * @return integer
     */
	argmax(arr) {
		var maxV = arr[0]
		var argmax = 0;
		for (var i = 1; i < arr.length; i++) {
			maxV = (arr[i] > maxV) ? arr[i] : maxV
			argmax = (arr[i] == maxV) ? i : argmax
		}
		return argmax
	}

	/** 
     * @description Draws the shape blueprint
	 * 
     * @return Void
     */
	DrawShape(SquareEdgeLength, ghost=false) {
		if (!ghost) {
			push();
			noFill();
			stroke("orange")
			strokeWeight(2)
			rect(0, 0, 4*SquareEdgeLength, 4*SquareEdgeLength)
			pop();
		}
		

		// draw each part of the shape blueprint
		for (var i = 0; i < this.ShapeBlueprint.length; i++) {
			for (var j = 0; j < this.ShapeBlueprint[0].length; j++) {
				this.DrawSquare(i, j , this.Color, this.ShapeBlueprint[i][j], SquareEdgeLength);
			}
		}
	}

	/** 
     * @description Sets the necessary variables to draw a square not on the board.
	 * 
     * @return Void
     */
	DrawSquare(row, col, color, cubeType, edgeLength, alpha=255) {

		if (cubeType == 0) {return}

		var tempSquare = new Square(edgeLength, -1, color);
		tempSquare.SetPowerCube(cubeType);
		tempSquare.i = row;
		tempSquare.j = col;
		tempSquare.Draw(alpha)
	}

	/** 
     * @description Draws the shape at the provided offset and opacity
	 * 
     * @return Void
     */
	DrawAtOffset(alpha, SquareEdgeLength, rowOffset, colOffset=0) {
		for (var i = 0; i < this.Squares.length; i++) {
			this.DrawSquare(this.Squares[i].i + rowOffset, this.Squares[i].j + colOffset, this.Color, this.Squares[i].PowerCubeType, SquareEdgeLength, alpha);
		}
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
