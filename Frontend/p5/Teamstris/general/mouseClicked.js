
// #class General |
// @author Steven Dellamore, Richard Hansen |
// @desc This is an abstract class that will hold mouseClicked and 
// keyPressed p5 functions. |

// #function General::mouseClicked |
// @author Steven Dellamore, Richard Hansen |
// @desc Will be called whenever the user clicks on anywhere on the screen.
// Once called, it will go straight into a switch to decide where to 
// route to based on the gameState
// @link{mouseClickedGeneralVar1} 
// The varibles @inline{gameState}, @inline{mStartScreen},
// @inline{mLobbyScreen} are all defined in sketch.js | 
// @param void : takes no parameters |
// @returns void : returns nothing |
// @header mouseClicked() |
function mouseClicked() {
    // #code mouseClickedGeneralVar1 javascript
    switch (gameState) {
        case 0:
            // start screens mouseClicked
            mStartScreen.mouseClickedStart(); 
            break;
        case 1:
            // lobby screens mouseClicked
            mLobbyScreen.mouseClickedLobby();
            break;
        case 2:
            break;
        case 3:
            break;
    }
    // |
}