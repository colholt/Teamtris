

// const expect = require('chai').expect;

const button = require('../Teamstris/general/buttons.js');
const startScreen = require('../Teamstris/startscreen/startScreen.js');

/* vars being used for testing */
var mStartScreen;

/* p5 stuff */
global.windowWidth = 2560;
global.windowHeight = 1600;
global.CORNER = 0;
global.ARROW = 0;
global.mouseX = 30;
global.mouseY = 30;
global.createCanvas = function (x,y) { }
global.push = function () { }
global.pop = function () { }
global.translate = function () { }
global.fill = function () { }
global.textSize = function () { }
global.text = function () { }
global.rect = function () { }
global.rectMode = function () { }
global.cursor = function () { }

/* Debug vars */
global.startscreen_constructor       = false;
global.startscreen_draw              = false;
global.startscreen_mouseClickedStart = false;
global.buttons_constructor           = false;
global.buttons_draw                  = false;
global.buttons_checkmouse            = false;

/* Button class vars */
global.buttonList = button[0];
global.Buttons = button[1];
global.Buttonloop = button[2];
global.ClickedLoop = button[3];
global.FindButtonbyID = button[4];

var red = '\x1b[31m%s\x1b[0m';
var green = '\x1b[32m%s\x1b[0m';
var blue = "\x1b[35m";

testRunnerSetupStartScreen();

function CheckSame( given, expect, name, debug = false ){
    if( debug ){
        console.log(blue, "given: " + given + " expect: " + expect + " name: " + name);
    }
    if(given === expect){
        console.log(green, name + " passed");  //cyan
        return true;
    } else {
        console.log(red, name + " failed should have been " + expect + " but was " + given);  //red
        return false;
    }
}

function testRunnerSetupStartScreen() {
    mStartScreen = new startScreen[0];
    testDefaultUsername();
    testDefaultTokenValue();
    testCheckInitStartScreenValues();
    testCheckTitlePosAfterTwoDraw();
    changeUserUsername();
}

function testDefaultUsername() {
    CheckSame(mStartScreen.usernameText,"username","testDefaultUsername");
}

function testDefaultTokenValue() {
    CheckSame(mStartScreen.TokenBoxText,"","testDefaultUsernameUserText");
}
  
function testCheckInitStartScreenValues() {
    CheckSame(mStartScreen.usernameTextTouched,false,"checkInitStartScreenValues.usernameTextTouched");
    CheckSame(mStartScreen.titleAnimation[0],300,"checkInitStartScreenValues.titleAnimation[0]");
    CheckSame(mStartScreen.titleAnimation[1],500,"checkInitStartScreenValues.titleAnimation[1]");
    CheckSame(mStartScreen.titleAnimation[2],400,"checkInitStartScreenValues.titleAnimation[2]");
    CheckSame(mStartScreen.titleAnimation[3],700,"checkInitStartScreenValues.titleAnimation[3]");
}

function testCheckTitlePosAfterTwoDraw() {
    mStartScreen.draw();
    mStartScreen.draw();
    CheckSame(mStartScreen.titleAnimation[0],280,"checkInitStartScreenValuesAfterTwoDraw.titleAnimation[0]");
    CheckSame(mStartScreen.titleAnimation[1],480,"checkInitStartScreenValuesAfterTwoDraw.titleAnimation[1]");
    CheckSame(mStartScreen.titleAnimation[2],380,"checkInitStartScreenValuesAfterTwoDraw.titleAnimation[2]");
    CheckSame(mStartScreen.titleAnimation[3],680,"checkInitStartScreenValuesAfterTwoDraw.titleAnimation[3]");
}

function changeUserUsername() {
    global.keyCode = 65; // A
    mStartScreen.keyPressedStart();
    mStartScreen.drawUsernameBox();
    CheckSame(mStartScreen.usernameText,"A","testChangeUserUsername1");
    CheckSame(mStartScreen.usernameTextTouched,true,"testUsernameTextTouched");
    global.keyCode = 66; // A
    mStartScreen.keyPressedStart();
    mStartScreen.drawUsernameBox();
    CheckSame(mStartScreen.usernameText,"AB","testChangeUserUsername2");
}

console.log(mStartScreen);
