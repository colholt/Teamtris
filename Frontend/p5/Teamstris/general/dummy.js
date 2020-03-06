
// #code keyPressedStartExample javascript
switch(this.gameStateStartScreen) {
    case 0:
        // username box active
        ...
    case 1:
        // token box active
        ...
}
// |

// #code testGlobalVar1 javascript
global.mouseY = 30;
global.LEFT_ARROW = 37;
global.RIGHT_ARROW = 39;
global.DOWN_ARROW = 40;
global.createCanvas = function (x,y) { }
global.push = function () { }
global.pop = function () { }
global.translate = function () { }
... // Keeps going
// |


// #code testCheckInitStartScreenValuesVar1 javascript
// check usernameTextTouched is false
CheckSame(
    mStartScreen.usernameTextTouched,false,
        "checkInitStartScreenValues.usernameTextTouched");

// check titleAnimation [0-4] is set to the correct values
CheckSame(
    mStartScreen.titleAnimation[0],300,
        "checkInitStartScreenValues.titleAnimation[0]");
... // other indexs of titleAnimation

// Check the stroke of the box is set to false
CheckSame(
    mStartScreen.usernameBoxStroke,false,
        "checkInitStartScreenValues.usernameBoxStroke");
// |


// #code testChangeMaxUsernameVar1 javascript
for(var i = 0; i < 15; i++) {
    mStartScreen.keyPressedStart(); // Press Key
    str += strFull.charAt(i);
    CheckSame(mStartScreen.usernameText,str,"testUsernameText" + str);
    global.keyCode++; // go next key
}
// |

// #code testintergrationVar1
for(var i = 0; i < 90000; i++) {
    mStartScreen.draw();
    if(i == 500) { 
        // Do Action
    } else if(i == 700) { 
        // Do another action
    } else if(i == 1000) { 
        // Do another action
    } else if(i == 55000) { 
        // Do another action
    }
}
// |