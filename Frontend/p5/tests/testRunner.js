

//const WebSocket = require('ws');

// const expect = require('chai').expect;

const button = require('../Teamstris/general/buttons.js');
const startScreen = require('../Teamstris/startscreen/startScreen.js');
const lobbyScreen = require('../Teamstris/lobbyscreen/lobbyScreen.js');
const playerCard = require('../Teamstris/lobbyscreen/playerCard.js');
const teamasker = require('../Teamstris/lobbyscreen/teamasker.js');
const gameScreen = require('../Teamstris/gamescreen/gamescreen.js')
const gameArray = require('../Teamstris/gamescreen/gamearray.js')
const gameShape = require('../Teamstris/gamescreen/shape.js')
const gameSquare = require('../Teamstris/gamescreen/square.js')
const player = require('../Teamstris/general/player.js');
const team = require('../Teamstris/general/team.js');

/* vars being used for testing */
var mStartScreen;
var mGameScreen;

global.gameState = 0;

var lol = false;
var numTests = 1;
var numFailed = 0;

/* p5 stuff */
global.windowWidth = 2560;
global.windowHeight = 1600;
global.CORNER = 0;
global.ARROW = 0;
global.mouseX = 30;
global.mouseY = 30;
global.LEFT_ARROW = 37;
global.RIGHT_ARROW = 39;
global.DOWN_ARROW = 40;
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
global.stroke = function () { }
global.strokeWeight = function () { }

/* Debug vars */
global.startscreen_constructor       = false;
global.startscreen_draw              = false;
global.startscreen_mouseClickedStart = false;
global.gamescreen_constructor        = false;
global.gamescreen_draw               = false;
global.gamescreen_updateflag         = false;
global.gamescreen_invalidshape       = false;
global.buttons_constructor           = false;
global.buttons_draw                  = false;
global.buttons_checkmouse            = false;

/* Button class vars */
global.buttonList = button[0];
global.Buttons = button[1];
global.Buttonloop = button[2];
global.ClickedLoop = button[3];
global.FindButtonbyID = button[4];

/* Lobby */
global.LobbyScreen = lobbyScreen[0];
global.mLobbyScreen = 5;

/* GameScreen */
global.GameScreen = gameScreen[0];
global.GameArray = gameArray[0];
global.Shape = gameShape[0];
global.Square = gameSquare[0];

/* Teammaker stuff */
global.checkMouseTeamAccept = teamasker[0];
global.teamNameAsker = teamasker[1];

/* Player */
global.Player = player[0];

/* Team */
global.Team = team[0];
global.team = Team

/* PlayerCard */
global.PlayerCard = playerCard[0];

class mockSocket{
    constructor(){}
    send(x) {}
    onmessage(x) {}
}
/* socket */
global.socket = new mockSocket();

/* color */
var red = '\x1b[31m%s\x1b[0m';
var green = '\x1b[32m%s\x1b[0m';
var blue = "\x1b[35m";

testRunnerSetupStartScreen();

function CheckSame( given, expect, name, debug = false ){
    if( debug ){
        console.log(blue, "given: " + given + " expect: " + expect + " name: " + name);
    }
    if(given === expect){
        console.log(green, numTests++ + ". " + name + " passed");
        return true;
    } else {
        console.log(red, numTests++ + ". " + name + " failed should have been " + expect + " but was " + given);
        numFailed++;
        return false;
    }
}


async function testDefaultUsername() {
    CheckSame(mStartScreen.usernameText,"username","testDefaultUsername");
}

async function testDefaultTokenValue() {
    CheckSame(mStartScreen.TokenBoxText,"","testDefaultUsernameUserText");
}

async function testCheckInitStartScreenValues() {
    CheckSame(mStartScreen.usernameTextTouched,false,"checkInitStartScreenValues.usernameTextTouched");
    CheckSame(mStartScreen.titleAnimation[0],300,"checkInitStartScreenValues.titleAnimation[0]");
    CheckSame(mStartScreen.titleAnimation[1],500,"checkInitStartScreenValues.titleAnimation[1]");
    CheckSame(mStartScreen.titleAnimation[2],400,"checkInitStartScreenValues.titleAnimation[2]");
    CheckSame(mStartScreen.titleAnimation[3],700,"checkInitStartScreenValues.titleAnimation[3]");
    CheckSame(mStartScreen.usernameBoxStroke,false,"checkInitStartScreenValues.usernameBoxStroke");
}

async function testCheckTitlePosAfterTwoDraw() {
    mStartScreen.draw();
    mStartScreen.draw();
    CheckSame(mStartScreen.titleAnimation[0],280,"checkInitStartScreenValuesAfterTwoDraw.titleAnimation[0]");
    CheckSame(mStartScreen.titleAnimation[1],480,"checkInitStartScreenValuesAfterTwoDraw.titleAnimation[1]");
    CheckSame(mStartScreen.titleAnimation[2],380,"checkInitStartScreenValuesAfterTwoDraw.titleAnimation[2]");
    CheckSame(mStartScreen.titleAnimation[3],680,"checkInitStartScreenValuesAfterTwoDraw.titleAnimation[3]");
}

async function testChangeUserUsername() {
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

async function testChangeMaxUsername() {
    mStartScreen.usernameText = "";
    CheckSame(mStartScreen.usernameText,"","testUsernameTextReset");
    global.keyCode = 65; // A
    var str = "";
    var strFull = "ABCDEFGHIJK"

    for(var i = 0; i < 15; i++) {
        if(lol) await new Promise(r => setTimeout(r, 100));
        mStartScreen.keyPressedStart();
        str += strFull.charAt(i);
        CheckSame(mStartScreen.usernameText,str,"testUsernameText" + str);
        global.keyCode++;
    }
}

async function testDeleteUsername() {
    global.keyCode = 8; // delete
    mStartScreen.keyPressedStart();
    var str = mStartScreen.usernameText;
    for(var i = 0; i < 15; i++) {
        if(lol) await new Promise(r => setTimeout(r, 200));
        mStartScreen.keyPressedStart();
        str = str.substr(0, str.length - 1);
        if (str == "") {
            CheckSame(mStartScreen.usernameText,str,"testUsernameTextNothing" + str);
        } else {
            CheckSame(mStartScreen.usernameText,str,"testUsernameText" + str);
        }
    }
}

async function testCheckSpecialChars() {
    global.keyCode = 10; // special
    mStartScreen.keyPressedStart();
    CheckSame(mStartScreen.usernameText,"","testCheckSpecialChars");

    global.keyCode = 240; // special
    mStartScreen.keyPressedStart();
    CheckSame(mStartScreen.usernameText,"","testCheckSpecialChars");

    global.keyCode = 33; // special
    mStartScreen.keyPressedStart();
    CheckSame(mStartScreen.usernameText,"","testCheckSpecialChars");
}

async function testHighScoreButton() {
    global.mouseX = mStartScreen.RightX + 1;
    global.mouseY = mStartScreen.TopY + 1;
    CheckSame(mStartScreen.gameStateStartScreen,0,"testCheckInitGameStateScoreButton");
    CheckSame(mStartScreen.drawHighScoreButtonCheckMouse(),true,"testDrawHighScoreButtonCheckMouse");
    mStartScreen.mouseClickedStart();
    CheckSame(mStartScreen.gameStateStartScreen,-1,"testMouseClickedScoreButton1");
    mStartScreen.gameStateStartScreen = 0; //reset gameState;
    global.mouseX += 100;
    mStartScreen.mouseClickedStart();
    if(lol) await new Promise(r => setTimeout(r, 300));
    CheckSame(mStartScreen.gameStateStartScreen,-1,"testMouseClickedScoreButton2");
    CheckSame(global.gameState,3,"testMouseClickedScoreButton2"); // 3 == score screen game state
    mStartScreen.gameStateStartScreen = 0; //reset gameState;
    global.gameState = 0; // reset gameState
    global.mouseX += 100;
    mStartScreen.mouseClickedStart();
    CheckSame(mStartScreen.gameStateStartScreen,0,"testMouseClickedScoreButtonMissed");
    if(lol) await new Promise(r => setTimeout(r, 1000));
    CheckSame(global.gameState,0,"testMouseClickedScoreButtonMissedRealGamestate");
}

async function testCreateGameButton() {
    mStartScreen.usernameText = "";
    CheckSame(mStartScreen.usernameText,"","testResetUsernametext");
    global.mouseX = 1300; // should be on the createGame
    global.mouseY = 1300;
    CheckSame(mStartScreen.gameStateStartScreen,0,"testCheckInitGameStateCreateButton");
    // CheckSame(mStartScreen.usernameBoxStroke,true,"testCheckInitGameStateCreateButton");
    mStartScreen.mouseClickedStart();
    CheckSame(mStartScreen.gameStateStartScreen,0,"testClickCreateButtonWithNoUsername");
    CheckSame(mStartScreen.usernameBoxStroke,true,"testCheckInitGameStateCreateButton");
    mStartScreen.usernameText = "Steven";
    mStartScreen.mouseClickedStart();
    CheckSame(global.gameState,1,"testClickCreateButtonWithUsername");
}

async function testJoinLobbyButton() {
    mStartScreen.usernameText = "";
    global.gameState = 0;
    CheckSame(mStartScreen.usernameText,"","testResetUsernametext");
    global.mouseX = 1200;
    global.mouseY = 1200;
    CheckSame(mStartScreen.gameStateStartScreen,0,"testCheckInitGameStateJoinButton");
    mStartScreen.mouseClickedStart();
    CheckSame(mStartScreen.gameStateStartScreen,0,"testClickJoinButtonWithNoUsername");
    mStartScreen.usernameText = "Steven";
    mStartScreen.mouseClickedStart();
    if(lol) await new Promise(r => setTimeout(r, 400));
    CheckSame(mStartScreen.gameStateStartScreen,1,"testClickJoinButtonWithUsername");
}

async function testCheckLobbyInitValues() {
    CheckSame(mLobbyScreen.player.username,"Steven","testCheckInitUsername");
    CheckSame(mLobbyScreen.player.owner,true,"testCheckInitOwnerTrue");
    CheckSame(typeof mLobbyScreen.player.id,"number","testCheckInitID");

    CheckSame(mLobbyScreen.team.playersInTeam[0].username,"Steven","testCheckInitTeamUsername");
    CheckSame(mLobbyScreen.team.playersInTeam[0].owner,true,"testCheckInitTeamOwnerTrue");
    CheckSame(typeof mLobbyScreen.team.playersInTeam[0].id,"number","testCheckInitTeamID");

    CheckSame(mLobbyScreen.team.teamName,"","testCheckInitTeamName");
    CheckSame(typeof mLobbyScreen.team.lobbyToken,"string","testCheckInitLobbyToken");
    CheckSame(mLobbyScreen.botNames[0],"Arnold","testInitBot0");
    CheckSame(mLobbyScreen.botNames[1],"Steve","testInitBot1");
    CheckSame(mLobbyScreen.botNames[2],"John","testInitBot2");
    CheckSame(mLobbyScreen.playerCards[0].player,mLobbyScreen.player,"checkTheInitPlayerValuesAreTheSame");
}

async function testCheckTokenIsBeingDisplayed() {
    var strInside;
    var x;
    var y;
    global.text = function(str, xx, yy) {
        x = xx;
        y = yy;
        strInside = str;
        // console.log("strInside: " + strInside + " x: " + x + " y: " + y);
    };
    mLobbyScreen.drawToken();
    CheckSame(strInside,"Token: ","testCheckTextPositionWithNoValue");
    CheckSame(x,256,"testCheckYOfTextCall");
    CheckSame(y,1454.5454545454545,"testCheckYOfTextCall");

    mLobbyScreen.team.lobbyToken = "abcd";
    mLobbyScreen.drawToken();
    CheckSame(strInside,"Token: abcd","testCheckTextPositionWithRealValues");
    CheckSame(x,256,"testCheckYOfTextCall");
    CheckSame(y,1454.5454545454545,"testCheckYOfTextCall");
}

async function testAddAndRemoveBotsFromLobby() {
    global.mouseX = 1039;
    global.mouseY = 1039;
    CheckSame(mLobbyScreen.lobbyGameState,0,"haveNotYetClickedAcceptOnTeamMakerButton");
    global.keyCode = 64;
    var fullStr = "ABCDEFGHIJK";
    for(var i = 0; i < 15; i++) {
        global.keyCode++;
        mLobbyScreen.keyPressedLobby();
        CheckSame(mLobbyScreen.team.teamName,fullStr.substring(0, i+1),"typedKeyforTeamName" + fullStr.substring(0, i+1));
    }
    if(lol) await new Promise(r => setTimeout(r, 200));
    mLobbyScreen.mouseClickedLobby();
    CheckSame(mLobbyScreen.lobbyGameState,1,"clickedAcceptOnTeamAskerButton");
    CheckSame(mLobbyScreen.team.teamName,fullStr,"typedKeyforTeamNameAfterAccept" + fullStr);
    global.mouseX = 2486;
    global.mouseY = 1353;
    global.gameState = 1;
    CheckSame(ClickedLoop(),"addbot","checkClickedLoop");
    mLobbyScreen.mouseClickedLobby();
    CheckSame(ClickedLoop(),"addbot","checkClickAddBot");
    CheckSame(mLobbyScreen.team.playersInTeam.length,2,"checkBotAddedSuccesfully");
    CheckSame(mLobbyScreen.playerCards.length,2,"checkBotPlayerCardAdded");
    mLobbyScreen.mouseClickedLobby();
    CheckSame(mLobbyScreen.team.playersInTeam.length,3,"checkBotAddedSuccesfully");
    CheckSame(mLobbyScreen.playerCards.length,3,"checkBotPlayerCardAdded");
    mLobbyScreen.mouseClickedLobby();
    CheckSame(mLobbyScreen.team.playersInTeam.length,4,"checkBotAddedSuccesfully");
    CheckSame(mLobbyScreen.playerCards.length,4,"checkBotPlayerCardAdded");
    mLobbyScreen.mouseClickedLobby();
    CheckSame(mLobbyScreen.team.playersInTeam.length,4,"checkBotAddedSuccesfully");
    CheckSame(mLobbyScreen.playerCards.length,4,"checkBotPlayerCardAdded");
    mLobbyScreen.mouseClickedLobby();
    CheckSame(mLobbyScreen.team.playersInTeam.length,4,"checkBotAddedSuccesfully");
    CheckSame(mLobbyScreen.playerCards.length,4,"checkBotPlayerCardAdded");
    global.mouseX = 0;
    global.mouseY = 0;
    CheckSame(ClickedLoop(),undefined,"checkClickedLoopMiss");
    if(lol) await new Promise(r => setTimeout(r, 200));
    CheckSame(mLobbyScreen.team.playersInTeam[0].username,"Steven","checkUsernameOfTeamMember1");
    CheckSame(mLobbyScreen.team.playersInTeam[0].owner,true,"checkOwnerOfTeamMember1");
    CheckSame(mLobbyScreen.team.playersInTeam[1].username,"Arnold","checkUsernameOfTeamMember2");
    CheckSame(mLobbyScreen.team.playersInTeam[1].owner,false,"checkOwnerOfTeamMember2");
    CheckSame(mLobbyScreen.team.playersInTeam[2].username,"Steve","checkUsernameOfTeamMember3");
    CheckSame(mLobbyScreen.team.playersInTeam[2].owner,false,"checkOwnerOfTeamMember3");
    CheckSame(mLobbyScreen.team.playersInTeam[3].username,"John","checkUsernameOfTeamMember4");
    CheckSame(mLobbyScreen.team.playersInTeam[3].owner,false,"checkOwnerOfTeamMember4");
    if(lol) await new Promise(r => setTimeout(r, 200));
    /* Remove button clicked */
    global.mouseX = 2486;
    global.mouseY = 1504;
    mLobbyScreen.mouseClickedLobby();
    CheckSame(mLobbyScreen.team.playersInTeam.length,3,"checkBotAddedSuccesfullyAfterRemove");
    CheckSame(mLobbyScreen.playerCards.length,3,"checkBotPlayerCardAddedAfterRemove");
    CheckSame(mLobbyScreen.team.playersInTeam[0].username,"Steven","checkUsernameOfTeamMember1AfterRemove");
    CheckSame(mLobbyScreen.team.playersInTeam[0].owner,true,"checkOwnerOfTeamMember1AfterRemov");
    CheckSame(mLobbyScreen.team.playersInTeam[1].username,"Arnold","checkUsernameOfTeamMember2AfterRemov");
    CheckSame(mLobbyScreen.team.playersInTeam[1].owner,false,"checkOwnerOfTeamMember2AfterRemov");
    CheckSame(mLobbyScreen.team.playersInTeam[2].username,"Steve","checkUsernameOfTeamMember3AfterRemov");
    CheckSame(mLobbyScreen.team.playersInTeam[2].owner,false,"checkOwnerOfTeamMember3AfterRemov");

    mLobbyScreen.mouseClickedLobby();
    CheckSame(mLobbyScreen.team.playersInTeam.length,2,"checkBotAddedSuccesfullyAfterRemove");
    CheckSame(mLobbyScreen.playerCards.length,2,"checkBotPlayerCardAddedAfterRemove");
    CheckSame(mLobbyScreen.team.playersInTeam[0].username,"Steven","checkUsernameOfTeamMember1AfterRemove");
    CheckSame(mLobbyScreen.team.playersInTeam[0].owner,true,"checkOwnerOfTeamMember1AfterRemov");
    CheckSame(mLobbyScreen.team.playersInTeam[1].username,"Arnold","checkUsernameOfTeamMember2AfterRemov");
    CheckSame(mLobbyScreen.team.playersInTeam[1].owner,false,"checkOwnerOfTeamMember2AfterRemov");
    if(lol) await new Promise(r => setTimeout(r, 200));
    mLobbyScreen.mouseClickedLobby();
    CheckSame(mLobbyScreen.team.playersInTeam.length,1,"checkBotAddedSuccesfullyAfterRemove");
    CheckSame(mLobbyScreen.playerCards.length,1,"checkBotPlayerCardAddedAfterRemove");
    CheckSame(mLobbyScreen.team.playersInTeam[0].username,"Steven","checkUsernameOfTeamMember1AfterRemove");
    CheckSame(mLobbyScreen.team.playersInTeam[0].owner,true,"checkOwnerOfTeamMember1AfterRemov");

    mLobbyScreen.mouseClickedLobby();
    CheckSame(mLobbyScreen.team.playersInTeam.length,1,"shouldNotBeAbleToRemoveLastPersonCheckPlayerInTeamsLength");
    CheckSame(mLobbyScreen.playerCards.length,1,"shouldNotBeAbleToRemoveLastPersonCheckplayerCardsLength");
    CheckSame(mLobbyScreen.team.playersInTeam[0].username,"Steven","checkMakeSureDataForNonRemovedPlayerIsGood");
    CheckSame(mLobbyScreen.team.playersInTeam[0].owner,true,"checkMakeSureDataForNonRemovedPlayerIsGood");
}

async function checkPlayCardValues() {
    CheckSame(mLobbyScreen.playerCards[0].scale,1,"checkPlayCardValuesScale");
    if(mLobbyScreen.playerCards[0].x <= global.windowWidth && mLobbyScreen.playerCards[0].x > 0){
        CheckSame(1,1,"checkPlayCardValuesx");
    }else {
        CheckSame(0,1,"checkPlayCardValuesx");
    }

    if(mLobbyScreen.playerCards[0].y <= global.windowWidth && mLobbyScreen.playerCards[0].y > 0){
        CheckSame(1,1,"checkPlayCardValuesy");
    }else {
        CheckSame(0,1,"checkPlayCardValuesy");
    }

    if(mLobbyScreen.playerCards[0].w > 0){
        CheckSame(1,1,"checkPlayCardValuesw");
    }else {
        CheckSame(0,1,"checkPlayCardValuesw");
    }

    if(mLobbyScreen.playerCards[0].h > 0){
        CheckSame(1,1,"checkPlayCardValuesh");
    }else {
        CheckSame(0,1,"checkPlayCardValuesh");
    }

    if(mLobbyScreen.playerCards[0].sqaureLength > 0){
        CheckSame(1,1,"checkPlayCardValuesSqaureLength");
    }else {
        CheckSame(0,1,"checkPlayCardValuesSqaureLength");
    }
}

async function integrationTest1() {
    mStartScreen = new startScreen[0];
    console.log(mStartScreen);
    global.gameState = 0;
    for(var i = 0; i < 90000; i++) {
        mStartScreen.draw();
        if(i == 500) { // mouse clicked
            global.mouseX = 0;
            global.mouseY = 0;
            mStartScreen.mouseClickedStart();
            CheckSame(global.gameState,0,"checkNothingHappenedOnRandomClickgameState");
            CheckSame(mStartScreen.gameStateStartScreen,0,"checkNothingHappenedOnRandomClick");
        }
        if(i == 700) { // mouse clicked
            global.mouseX = 1300; // should be on the createGame
            global.mouseY = 1300;
            CheckSame(mStartScreen.usernameBoxStroke,false,"checkInitStartScreenValues.usernameBoxStroke");
            mStartScreen.mouseClickedStart();
            CheckSame(global.gameState,0,"checkClickedCreateGameWithNoUsername gameState");
            CheckSame(mStartScreen.gameStateStartScreen,0,"checkNothingHappenedOnRandomClick gameStateStartScreen");
            CheckSame(mStartScreen.usernameBoxStroke,true,"checkInitStartScreenValues.usernameBoxStroke");
        }
    }
    CheckSame(1,1,"check90000Frames");
    console.log(mStartScreen);
}

async function testGameArrayNotNull() {
    mGameScreen = new GameScreen();
    CheckSame(mGameScreen.GameArray != null, true, "testGameboardNotNull")
}

async function testGameScreenRotateKeyPress() {
    mGameScreen = new GameScreen();
    var custom_shape = [[0,0,0,0],
                        [0,0,0,0],
                        [0,1,1,0],
                        [0,1,1,0]]
    mGameScreen.GameArray.ForceChangeShape(1, custom_shape,0,0,false)
    global.keyCode = 65
    mGameScreen.keyPressedGame()
    CheckSame(mGameScreen.GameArray.ShapeArray[0].RotateSign, -1, "testGameScreenRotateKeyPress")
}

async function testGameScreenFailRotateKeyPress() {
    mGameScreen = new GameScreen();
    var custom_shape = [[0,0,0,0],
                        [0,0,0,0],
                        [0,1,0,0],
                        [0,1,0,0]]
    mGameScreen.GameArray.ForceChangeShape(1, custom_shape,0,0,false)
    global.keyCode = 65
    mGameScreen.keyPressedGame()
    CheckSame(mGameScreen.GameArray.ShapeArray[0].RotateSign, 1, "testGameScreenFailRotateKeyPress")
}

async function testGameScreenFailRotateKeyPress() {
    mGameScreen = new GameScreen();
    var custom_shape = [[0,0,0,0],
                        [0,0,0,0],
                        [0,1,0,0],
                        [0,1,0,0]]
    mGameScreen.GameArray.ForceChangeShape(1, custom_shape,0,0,false)
    global.keyCode = 65
    mGameScreen.keyPressedGame()
    CheckSame(mGameScreen.GameArray.ShapeArray[0].RotateSign, 1, "testGameScreenFailRotateKeyPress")
}

async function testFourRotate() {
    mGameScreen = new GameScreen();
    var custom_shape = [[0,0,0,0],
                        [0,0,0,0],
                        [0,1,1,0],
                        [0,1,1,0]]
    mGameScreen.GameArray.ForceChangeShape(1, custom_shape,0,0,false)
    global.keyCode = 65
    const temp_arr = mGameScreen.GameArray.ShapeArray[0].ShapeBlueprint
    mGameScreen.keyPressedGame()
    mGameScreen.keyPressedGame()
    mGameScreen.keyPressedGame()
    mGameScreen.keyPressedGame()
    for (var i = 0; i < temp_arr.length; i++) {
        for (var j = 0; j < temp_arr.length; j++) {
            if (temp_arr[i][j] != mGameScreen.GameArray.ShapeArray[0].ShapeBlueprint[i][j]) {
                CheckSame(0,1,"TestFourRotate")
            }
        }
    }
    CheckSame(1,1, "TestFourRotate")
}

async function testMove() {
    mGameScreen = new GameScreen();
    var custom_shape = [[0,0,0,0],
                        [0,0,0,0],
                        [0,1,1,0],
                        [0,1,1,0]]
    mGameScreen.GameArray.ForceChangeShape(1, custom_shape,0,0,false)

    const temp_arr = mGameScreen.GameArray.arr.slice()
    var temp_col = mGameScreen.GameArray.ShapeArray[0].Squares[0].j
    global.keyCode = RIGHT_ARROW
    mGameScreen.keyPressedGame()
    CheckSame(mGameScreen.GameArray.ShapeArray[0].Squares[0].j,temp_col+1, "testMoveR")
    mGameScreen.keyPressedGame()
    CheckSame(mGameScreen.GameArray.ShapeArray[0].Squares[0].j,temp_col+2, "testMoveRR")
    mGameScreen.keyPressedGame()
    CheckSame(mGameScreen.GameArray.ShapeArray[0].Squares[0].j,temp_col+3, "testMoveRRR")
    global.keyCode = LEFT_ARROW
    mGameScreen.keyPressedGame()
    CheckSame(mGameScreen.GameArray.ShapeArray[0].Squares[0].j,temp_col+2, "testMoveRRRL")
    mGameScreen.keyPressedGame()
    CheckSame(mGameScreen.GameArray.ShapeArray[0].Squares[0].j,temp_col+1, "testMoveRRRLL")
    mGameScreen.keyPressedGame()
    CheckSame(mGameScreen.GameArray.ShapeArray[0].Squares[0].j,temp_col, "testMoveRRRLLL")
    for (var i = 0; i < temp_arr.length; i++) {
        for (var j = 0; j < temp_arr.length; j++) {
            if (temp_arr[i][j] != mGameScreen.GameArray.arr[i][j]) {
                CheckSame(0,1,"testMove")
                return;
            }
        }
    }
    var temp_row = mGameScreen.GameArray.ShapeArray[0].Squares[0].i
    global.keyCode = DOWN_ARROW
    mGameScreen.keyPressedGame()
    CheckSame(mGameScreen.GameArray.ShapeArray[0].Squares[0].i,temp_row+1, "testMoveRRRLLD")
    mGameScreen.keyPressedGame()
    CheckSame(mGameScreen.GameArray.ShapeArray[0].Squares[0].i,temp_row+2, "testMoveRRRLLDD")
    mGameScreen.keyPressedGame()
    CheckSame(mGameScreen.GameArray.ShapeArray[0].Squares[0].i,temp_row+3, "testMoveRRRLLDDD")

    mGameScreen.GameArray.ForceChangeShape(1, custom_shape,17,0,false)
    temp_row = mGameScreen.GameArray.ShapeArray[0].Squares[0].i
    mGameScreen.keyPressedGame()
    CheckSame(mGameScreen.GameArray.ShapeArray[0].Squares[0].i,temp_row+1, "testMoveRRRLLD_noMove")
    mGameScreen.keyPressedGame()
    CheckSame(mGameScreen.GameArray.ShapeArray[0].Squares[0].i,0, "testMoveRRRLLDD_resetMove")
    mGameScreen.keyPressedGame()
    CheckSame(mGameScreen.GameArray.ShapeArray[0].Squares[0].i,1, "testMoveRRRLLDDD_resetMove2")
    
    CheckSame(1,1, "testMove")
}

async function testNewSquare() {
    mGameScreen = new GameScreen();
    var custom_shape = [[0,0,0,0],
                        [0,0,0,0],
                        [0,1,1,0],
                        [0,1,1,0]]
    mGameScreen.GameArray.ForceChangeShape(1, custom_shape,18,0,false)
    var temp_col = mGameScreen.GameArray.ShapeArray[0].Squares[0].j
    var shape = mGameScreen.GameArray.ShapeArray[0]
    mGameScreen.GameArray.CheckFreeze(shape, 1, mGameScreen.GameArray.CollisionType.OutOfBounds)
    if (temp_col != mGameScreen.GameArray.ShapeArray[0].Squares[0].j) {
        CheckSame(1,1,"testNewSquare")
    } else {
        CheckSame(temp_col,mGameScreen.GameArray.ShapeArray[0].Squares[0].j, "testNewSquare")
    }

    mGameScreen.GameArray.ForceChangeShape(1, custom_shape,0,0,false)
    temp_col = mGameScreen.GameArray.ShapeArray[0].Squares[0].j
    mGameScreen.GameArray.CheckFreeze(shape, 0, mGameScreen.GameArray.CollisionType.OutOfBounds)
    if (temp_col == mGameScreen.GameArray.ShapeArray[0].Squares[0].j) {
        CheckSame(1,1,"testNewSquareNoUpdate")
    } else {
        CheckSame(temp_col,mGameScreen.GameArray.ShapeArray[0].Squares[0].j, "testNewSquareNoUpdate")
    }

    mGameScreen.GameArray.ForceChangeShape(1, custom_shape,0,0,false)
    temp_col = mGameScreen.GameArray.ShapeArray[0].Squares[0].j
    mGameScreen.GameArray.CheckFreeze(shape, 1, mGameScreen.GameArray.CollisionType.OtherPlayer)
    if (temp_col == mGameScreen.GameArray.ShapeArray[0].Squares[0].j) {
        CheckSame(1,1,"testNewSquareNoUpdateOtherPlayerDown")
    } else {
        CheckSame(temp_col,mGameScreen.GameArray.ShapeArray[0].Squares[0].j, "testNewSquareNoUpdateOtherPlayerDown")
    }
}

async function testNumberOfPlayers() {
    mGameScreen = new GameScreen();
    CheckSame(mGameScreen.NumPlayers, mGameScreen.GameArray.ShapeArray.length, "testNumberOfPlayers")
}


async function testRunnerSetupStartScreen() {
    /* Start screen tests*/
    mStartScreen = new startScreen[0];
    await testDefaultUsername();
    await testDefaultTokenValue();
    await testCheckInitStartScreenValues();
    await testCheckTitlePosAfterTwoDraw();
    await testChangeUserUsername();
    await testChangeMaxUsername();
    await testDeleteUsername();
    await testCheckSpecialChars();
    await testHighScoreButton();
    await testCreateGameButton();
    await testJoinLobbyButton();
    /* End start screen tests */

    /* Lobby Screen tests */
    await testCheckLobbyInitValues();
    await testCheckTokenIsBeingDisplayed();
    await testAddAndRemoveBotsFromLobby();
    await checkPlayCardValues();

    /* Game Screen tests*/
    await testGameArrayNotNull(); 
    await testGameScreenRotateKeyPress();
    await testGameScreenFailRotateKeyPress();
    await testFourRotate();
    await testMove();
    await testNewSquare();
    await testNumberOfPlayers();
    /* End Game Screen tests*/

    await integrationTest1();
    // console.log(mStartScreen);
    // console.log(mLobbyScreen);
    console.log(green, "\n" + (numTests-numFailed-1) + " passed");
    console.log(red, numFailed + " failed");
}

/**********
 *
 *
 *
 *
 *     BUTTON FINDER 3700
for(var i = 0 ; i < 10000; i++) {
    global.mouseX += 1;
    global.mouseY = 0;
    for(var j = 0 ; j < 10000; j++) {
        global.mouseY += 1;
        if(ClickedLoop() == "addbot"){
            console.log("HERRE: " + global.mouseX + ":" + global.mouseY);
        }
    }
}
 */
