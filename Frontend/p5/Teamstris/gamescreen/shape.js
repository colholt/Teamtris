class Shape {
	constructor (ID, Color="rand", BlueprintDimensions=4) {
		// ID of the player who 'owns' this square
		this.ID = ID

		// blueprint of the shape
		this.BlueprintDimensions = BlueprintDimensions;
		this.ShapeBlueprint = this.DefaultShape1()

		// calculate Dimensions of the shape in the form [rowTop, colLeft, width, height]
		this.ShapeDimensions = this.CalculateDimensions(this.ShapeBlueprint)

		// color of all the squares attached to this shape
		if (Color == "rand") {
			this.Color = this.RandomColor()
		} else {
			this.Color = Color
		}

		// List of squares associated with the object
		this.Squares = []
	}

	// adds the coordinates of a square to the shape
	AddSquare(Square) {
		this.Squares.push(Square)
		Square.ChangeOwner(this.ID, this.Color)
	}

	// sets the squares array to empty
	ResetSquares() {
		this.Squares = []
	}

	// returns a random color from the set of available colors
	RandomColor() {
		var Colors = ["red","blue","green","yellow"]
		return Colors[Math.floor(Math.random()*Colors.length)]
	}

	// sets all the squares associated with this shape to empty
	RemoveShape() {
		for (var i = 0; i < this.Squares.length; i++) {
			this.Squares[i].SetEmpty()
		}
	}

	// sets all the squares associated with this shape to frozen
	FreezeSquares() {
		for (var i = 0; i < this.Squares.length; i++) {
			this.Squares[i].SetFrozen()
		}
	}

	// Moves all squares associated with this shape in the specified direction
	MoveShape(GameArray, left=0, right=0, down=0) {
		this.RemoveShape() // remove the shape from the game array
		for (var k = 0; k < this.Squares.length; k++) {
			// get the next square
			this.Squares[k] = GameArray[this.Squares[k].i + down][this.Squares[k].j - left + right]
			
			// set this shape to be the owner of the square
			this.Squares[k].ChangeOwner(this.ID, this.Color)
		}
	}

	// finds the top left corner of the shape to allow for easy instaniatation along with width and height
	CalculateDimensions(arr) {
		var rowTop = this.BlueprintDimensions
		var rowBot = 0
		var colLeft = this.BlueprintDimensions
		var colRight = 0
		for (var i = 0; i < this.BlueprintDimensions; i++) {
			for (var j = 0; j < this.BlueprintDimensions; j++) {
			// if the blueprint has a square at this location, we check to see if it is a significant part of the bounding box
				if (arr[i][j] == 1) {
					rowTop = min(rowTop,i)
					colLeft = min(colLeft,j)
					rowBot = max(rowBot,i)
					colRight = max(colRight,j)

				}
			}
		}

		return [rowTop, colLeft, colRight-colLeft+1, rowBot-rowTop+1]
	}

	// returns a list of the new squares of which 
	RotateIndices(gameArrayRows, gameArrayCols) {
		// find the top left corner of the shape's bounding box
		var rowTop = gameArrayRows
		var colLeft = gameArrayCols
		for (var k = 0; k < this.Squares.length; k++) {
			rowTop = int(min(rowTop, this.Squares[k].i))
			colLeft = int(min(colLeft, this.Squares[k].j))
		}

		// rotate the existing blueprint of the image
		var RotatedBlueprint = this.RotateShapeBlueprint()

		// calculate Dimensions of the shape in the ShapeBlueprint in the form [rowTop, colLeft, width, height]
		var ShapeDims = this.CalculateDimensions(RotatedBlueprint)

		// based off of the bounding box coordiantes, find the new square coordinates
		var squareCoords = []
		for (var i = ShapeDims[0]; i < ShapeDims[0] + ShapeDims[3]; i++) {
			for (var j = ShapeDims[1]; j < ShapeDims[1] + ShapeDims[2]; j++) {
				if (RotatedBlueprint[i][j] == 1) {
					//console.log(rowTop+i-ShapeDims[0]+1,colLeft+j-ShapeDims[1])
					squareCoords.push([rowTop+i-ShapeDims[0],colLeft+j-ShapeDims[1]])
				}
			}
		}
		return [squareCoords,RotatedBlueprint,ShapeDims]
	}

	AdoptSquares(SquareArray){
		for (var i = 0; i < SquareArray.length; i++) {
			SquareArray[i].ChangeOwner(this.ID, this.Color)
		}
	}

	// update to a new set of dimensions and blueprint
	UpdateAfterRotate(newSquares, blueprint, Dimensions) {
		this.RemoveShape() // remove the shape from the game board
		this.ResetSquares() // set our list of squares to an empty array
		this.AdoptSquares(newSquares) // set this shape as the owner of each square

		// set associated class variables
		this.Squares = newSquares
		this.ShapeBlueprint = blueprint
		this.ShapeDimensions = Dimensions
	}

	// rotate the shape blueprint by 90 degrees
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

	Freeze() {
		this.FreezeSquares() // set all the squares to frozen
		this.ResetSquares() // reset this shape's 
	}

	DefaultShape1 () {
		return [[0,0,0,0],
				[0,0,1,1],
				[0,0,1,0],
				[0,0,1,1]]
	}
}
