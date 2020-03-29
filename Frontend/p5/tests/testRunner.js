

/**
 * #class FrontendTests |
 * @author Steven Dellamore, Richard Hansen |
 * @language javascript | 
 * @desc This is the testing doc for all the frontend tests. 
 * We decided to not go with a framework because we didnt think 
 * we needed everything the framework gives us. This framework 
 * uses the idea of dependency injection. We mock out all the 
 * p5 variables like so:
 * @link{testGlobalVar1}
 * This allows us to control all aspects of the test
 * and really unit test every line of code in our functions.
 * More over, we are able to mock out other classes that are being 
 * used by the class we are trying to test like so:
 * @link{buttonListVarTest1}
 * Once again, we can really drill down to the functions and have a really good
 * understanding of what its doing and its return values. |
 */


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
// #code buttonListVarTest1 javascript
global.buttonList = button[0];
global.Buttons = button[1];
global.Buttonloop = button[2];
global.ClickedLoop = button[3];
global.FindButtonbyID = button[4];
// |

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
global.Team.lobbyToken = "noEmpty"
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

/**
 * #function FrontendTests::CheckSame |
 * @author Steven Dellamore, Richard Hansen |
 * @desc Checks to see if the @inline{given} and @inline{expected} strings are the same.
 * If they are not this function will return false and print what the expected was. 
 * @link{CheckSameVar1}
 * If its true it will print a success message. | 
 * @header CheckSame( string, string, string, boolean = false ) |
 * @param string given : real output |
 * @param string expect : expected output |
 * @param string name : name of test |
 * @param boolean debug : @inline{true} if you want debug statments printed |
 * @returns boolean : true if given and expected match, false otherwise | 
 */
function CheckSame( given, expect, name, debug = false ){
    if( debug ){
        console.log(blue, "given: " + given + " expect: " + expect + " name: " + name);
    }
    if(given === expect){
        console.log(green, numTests++ + ". " + name + " passed");
        return true;
    } else {
        // #code CheckSameVar1 javascript
        console.log(
            red, numTests++ + ". " + name + " failed should have been " + 
                                            expect + " but was " + given);
        // |
        numFailed++;
        return false;
    }
}

/**
 * #function FrontendTests::testDefaultUsername |
 * @author Steven Dellamore |
 * @desc Checks to see if the default @inline{mStartScreen.usernameText} is @inline{"username"}. |
 * @header async function testDefaultUsername() |
 */
async function testDefaultUsername() {
    CheckSame(mStartScreen.usernameText,"username","testDefaultUsername");
}

/**
 * #function FrontendTests::testDefaultTokenValue |
 * @author Steven Dellamore |
 * @desc Checks to see if the default @inline{mStartScreen.TokenBoxText} is @inline{""}. |
 * @header async function testDefaultTokenValue() |
 */
async function testDefaultTokenValue() {
    CheckSame(mStartScreen.TokenBoxText,"","testDefaultUsernameUserText");
}

/**
 * #function FrontendTests::testCheckInitStartScreenValues |
 * @author Steven Dellamore |
 * @desc Checks to see if all the other init startscreen values are correct.
 * @link{testCheckInitStartScreenValuesVar1} |
 * @header async function testCheckInitStartScreenValues() |
 */
async function testCheckInitStartScreenValues() {
    CheckSame(
        mStartScreen.usernameTextTouched,false,
            "checkInitStartScreenValues.usernameTextTouched");
    CheckSame(
        mStartScreen.titleAnimation[0],300,
            "checkInitStartScreenValues.titleAnimation[0]");
    CheckSame(
        mStartScreen.titleAnimation[1],500,
            "checkInitStartScreenValues.titleAnimation[1]");
    CheckSame(
        mStartScreen.titleAnimation[2],400,
            "checkInitStartScreenValues.titleAnimation[2]");
    CheckSame(
        mStartScreen.titleAnimation[3],700,
            "checkInitStartScreenValues.titleAnimation[3]");
    CheckSame(
        mStartScreen.usernameBoxStroke,false,
            "checkInitStartScreenValues.usernameBoxStroke");
}

/**
 * #function FrontendTests::testCheckTitlePosAfterTwoDraw |
 * @author Steven Dellamore |
 * @desc Run @inline{mStartScreen.draw()} twice and check that the 
 * title pos values have been updated correctly. |
 * @header async function testCheckTitlePosAfterTwoDraw() |
 */
async function testCheckTitlePosAfterTwoDraw() {
    mStartScreen.draw();
    mStartScreen.draw();
    CheckSame(mStartScreen.titleAnimation[0],280,"checkInitStartScreenValuesAfterTwoDraw.titleAnimation[0]");
    CheckSame(mStartScreen.titleAnimation[1],480,"checkInitStartScreenValuesAfterTwoDraw.titleAnimation[1]");
    CheckSame(mStartScreen.titleAnimation[2],380,"checkInitStartScreenValuesAfterTwoDraw.titleAnimation[2]");
    CheckSame(mStartScreen.titleAnimation[3],680,"checkInitStartScreenValuesAfterTwoDraw.titleAnimation[3]");
}

/**
 * #function FrontendTests::testChangeUserUsername |
 * @author Steven Dellamore |
 * @desc Will set the @inline{keyCode} equal to @inline{65} 
 * and @inline{66} and call the @inline{keyPressedStart()} 
 * function. Which tells the start screen that a key has been
 * presesed. We then check if @inline{mStartScreen.usernameText} 
 * was changed to @inline{"A"} and @inline{"AB"}. |
 * @header async function testChangeUserUsername() |
 */
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

/**
 * #function FrontendTests::testChangeMaxUsername |
 * @author Steven Dellamore |
 * @desc Will call the @inline{keyPressedStart()} function 
 * with letters ABCDEFGHIJKLMNOPQRS and check to ensure that 
 * the @inline{mStartScreen.usernameText} does not get above 
 * 11 chars.
 * @link{testChangeMaxUsernameVar1} |
 * @header async function testChangeMaxUsername() |
 */
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

/**
 * #function FrontendTests::testDeleteUsername |
 * @author Steven Dellamore |
 * @desc Does the same thing as testChangeMaxUsername but 
 * deletes characters 15 times and checks @inline{mStartScreen.usernameText}
 * to ensure that everything has been deleted. \\Note: @inline{KeyCode=8} is
 * the delete key. |
 * @header async function testDeleteUsername() |
 */
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

/**
 * #function FrontendTests::testCheckSpecialChars |
 * @author Steven Dellamore |
 * @desc Will try to add special chars like ASCII codes @inline{10},
 * @inline{240}, @inline{33} and then make sure 
 * @inline{mStartScreen.usernameText} is unchanged because
 * you can't have special chars in ur username. |
 * @header async function testCheckSpecialChars() |
 */
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

/**
 * #function FrontendTests::testHighScoreButton |
 * @author Steven Dellamore |
 * @desc Sets the mouse positions to be over the high score button.
 * @link{testHighScoreButtonVar1}
 * Then we check that @inline{gameStateStartScreen == 0} still
 * equals zero since we havent clicked yet, and check that the high score
 * button is being highlighted correctly. The test then checks if we click on
 * the Score Button @inline{gameState == 1}. |
 * @header async function testHighScoreButton() |
 */
async function testHighScoreButton() {
    // #code testHighScoreButtonVar1 javascript
    global.mouseX = mStartScreen.RightX + 1;
    global.mouseY = mStartScreen.TopY + 1;
    CheckSame(mStartScreen.gameStateStartScreen,0,
        "testCheckInitGameStateScoreButton");
    CheckSame(mStartScreen.drawHighScoreButtonCheckMouse(),true,
        "testDrawHighScoreButtonCheckMouse");
    // |
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

/**
 * #function FrontendTests::testCreateGameButton |
 * @author Steven Dellamore |
 * @desc Sets the mouse to be over the "Create Game" Button and checks
 * to see if it gets highlighted correctly. Then we click on the button 
 * with  an empty @inline{mStartScreen.usernameText} and check to make sure
 * we did not get moved into the Lobby screen. Finally we add a username \\
 * @inline{mStartScreen.usernameText = "Steven"} and click on the 
 * "Create Game" button. We then check we got moved into the lobby screen correctly. |
 * @header async function testCreateGameButton() |
 */
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

/**
 * #function FrontendTests::testJoinLobbyButton |
 * @author Steven Dellamore |
 * @desc Sets the @inline{mouseX} and @inline{mouseY} to be over the 
 * "Join Game" button. Then we call @inline{mStartScreen.mouseClickedStart()}
 * and check to that we are being put into the token screen correctly.  |
 * @header async function testJoinLobbyButton() |
 */
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

/**
 * #function FrontendTests::testCheckLobbyInitValues |
 * @author Steven Dellamore |
 * @desc Check the init values when moving to the lobby screen
 * from the start screen. We first check to make sure the 
 * @inline{Player} object is set corretly like so:
 * @link{testCheckLobbyInitValuesVar1}
 * Then we need to check the @inline{Team} object like so:
 * @link{testCheckLobbyInitValuesVar2} 
 * Once these are checked we know that we have good init values. |
 * @header async function testCheckLobbyInitValues() |
 */
async function testCheckLobbyInitValues() {
    // #code testCheckLobbyInitValuesVar1 javascript
    CheckSame(mLobbyScreen.player.username,"Steven","testCheckInitUsername");
    CheckSame(mLobbyScreen.player.owner,true,"testCheckInitOwnerTrue");
    CheckSame(typeof mLobbyScreen.player.id,"number","testCheckInitID");
    // |
    // #code testCheckLobbyInitValuesVar2 javascript
    CheckSame(mLobbyScreen.team.playersInTeam[0].username,"Steven","testCheckInitTeamUsername");
    CheckSame(mLobbyScreen.team.playersInTeam[0].owner,true,"testCheckInitTeamOwnerTrue");
    CheckSame(typeof mLobbyScreen.team.playersInTeam[0].id,"number","testCheckInitTeamID");
    CheckSame(mLobbyScreen.team.teamName,"","testCheckInitTeamName");
    CheckSame(typeof mLobbyScreen.team.lobbyToken,"string","testCheckInitLobbyToken");
    // |
    CheckSame(mLobbyScreen.botNames[0],"Arnold","testInitBot0");
    CheckSame(mLobbyScreen.botNames[1],"Steve","testInitBot1");
    CheckSame(mLobbyScreen.botNames[2],"John","testInitBot2");
    CheckSame(mLobbyScreen.playerCards[0].player,mLobbyScreen.player,"checkTheInitPlayerValuesAreTheSame");
}

/**
 * #function FrontendTests::testCheckTokenIsBeingDisplayed |
 * @author Steven Dellamore |
 * @desc Checks to see if the Token is being displayed 
 * by the frontend in the correct position. This is an example of 
 * how we can use Dependency Injection:
 * @link{exampleOfDEVar1}
 * As you can see we are checking what @inline{drawToken()} is sending 
 * the p5 function @inline{text()}, which is sent the token, 
 * xPos and yPos. |
 * @header async function testCheckTokenIsBeingDisplayed() |
 */
async function testCheckTokenIsBeingDisplayed() {
    // #code exampleOfDEVar1 javascript
    var strInside;
    var x;
    var y;
    global.text = function(str, xx, yy) {
        x = xx;
        y = yy;
        strInside = str;
    };
    mLobbyScreen.drawToken();
    CheckSame(strInside,"Token: ","testCheckTextPositionWithNoValue");
    CheckSame(x,256,"testCheckYOfTextCall");
    CheckSame(y,1454.5454545454545,"testCheckYOfTextCall");
    // |

    mLobbyScreen.team.lobbyToken = "abcd";
    mLobbyScreen.drawToken();
    CheckSame(strInside,"Token: abcd","testCheckTextPositionWithRealValues");
    CheckSame(x,256,"testCheckYOfTextCall");
    CheckSame(y,1454.5454545454545,"testCheckYOfTextCall");
}

/**
 * #function FrontendTests::testAddAndRemoveBotsFromLobby |
 * @author Steven Dellamore |
 * @desc Checks to see if the owner of the lobby can add
 * and remove bots from their lobby. We set @inline{mouseX} and 
 * @inline{mouseY} to the position of the add bot button and then 
 * call @inline{mouseClickedLobby()} and check if the bot has been 
 * increased. |
 * @header async function testAddAndRemoveBotsFromLobby() |
 */
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

/**
 * #function FrontendTests::checkPlayCardValues |
 * @author Steven Dellamore |
 * @desc Checks the init values of the player cards.
 * Also check that the playcards are being rendered 
 * within the bounds of @inline{windowWidth} and @inline{windowHeight}. |
 * @header async function checkPlayCardValues() |
 */
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

/**
 * #function FrontendTests::integrationTest1 |
 * @author Steven Dellamore |
 * @desc This integration test will render the start screen
 * 90,000 times and do different actions at certain times
 * to ensure the start screen as a whole is working correctly. 
 * @link{testintergrationVar1}
 * Here we are rending the draw method 90,000 times and at different
 * renders we are doing different actions (like mouse clicking, or key pressing).  |
 * @header async function integrationTest1() |
 */
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

/**
 * #function FrontendTests::testGameArrayNotNull |
 * @author Richard Hansen |
 * @desc TODO |
 * @header async function testGameArrayNotNull() |
 */
async function testGameArrayNotNull() {
    mGameScreen = new GameScreen();
    CheckSame(mGameScreen.GameArray != null, true, "testGameboardNotNull")
}

/**
 * #function FrontendTests::testGameScreenRotateKeyPress |
 * @author Richard Hansen |
 * @desc TODO |
 * @header async function testGameScreenRotateKeyPress() |
 */
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

/**
 * #function FrontendTests::testGameScreenFailRotateKeyPress |
 * @author Richard Hansen |
 * @desc TODO |
 * @header async function testGameScreenFailRotateKeyPress() |
 */
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

/**
 * #function FrontendTests::testGameScreenFailRotateKeyPress |
 * @author Richard Hansen |
 * @desc TODO |
 * @header async function testGameScreenFailRotateKeyPress() |
 */
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

/**
 * #function FrontendTests::testFourRotate |
 * @author Richard Hansen |
 * @desc TODO |
 * @header async function testFourRotate() |
 */
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

/**
 * #function FrontendTests::testMove |
 * @author Richard Hansen |
 * @desc TODO |
 * @header async function testMove() |
 */
async function testMove() {
    team.lobbyToken = "ABCD"
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

/**
 * #function FrontendTests::testNewSquare |
 * @author Richard Hansen |
 * @desc TODO |
 * @header async function testNewSquare() |
 */
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

/**
 * #function FrontendTests::testNumberOfPlayers |
 * @author Richard Hansen |
 * @desc TODO |
 * @header async function testNumberOfPlayers() |
 */
async function testNumberOfPlayers() {
    mGameScreen = new GameScreen();
    CheckSame(mGameScreen.NumPlayers, mGameScreen.GameArray.ShapeArray.length, "testNumberOfPlayers")
}

async function testRandomGeneratedShapes() {
    mGameScreen = new GameScreen();
    var custom_shape = [[0,0,0,0],
                        [0,0,0,0],
                        [0,1,1,0],
                        [0,1,1,0]]
    mGameScreen.GameArray.ForceChangeShape(1, custom_shape,18,0,false)
    var shape = mGameScreen.GameArray.ShapeArray[0]

    var randArr = shape.GenerateRandomShape()
    var count = 0
    for (var i = 0; i < randArr.length; i++) {
        for (var j = 0; j < randArr.length; j++) {
            if (randArr[i][j] != 0) {
                count++
            }
        }
    }

    var tests = 0
    for (var i = 0; i < randArr.length; i++) {
        for (var j = 0; j < randArr.length; j++) {
            if (randArr[i][j] != 0) {
                CheckSame(randomShapeHelper(randArr,i,j), count, "testConnectedShapes" + tests)
                count = 0 // after the first check, there should be no remaining values that are not 0 or -1
                tests++
            }
        }
    }
}

function randomShapeHelper(arr,i,j) {
    if (i < 0 || i > arr.length-1 || j < 0 || j > arr.length-1) {
        return 0
    } else if (arr[i][j] == 0 || arr[i][j] == -1) {
        return 0
    } else {
        arr[i][j] = -1;
        return 1 + randomShapeHelper(arr,i-1,j) 
                 + randomShapeHelper(arr,i+1,j) 
                 + randomShapeHelper(arr,i,j-1)
                 + randomShapeHelper(arr,i,j+1)
    }
}

/**
 * #function FrontendTests::testRunnerSetupStartScreen |
 * @author Steven Dellamore, Richard Hansen |
 * @desc TODO |
 * @header async function testRunnerSetupStartScreen() |
 */
async function testRunnerSetupStartScreen() {
    /* Start screen tests*/
    mStartScreen = new startScreen[0];
    // await testDefaultUsername();
    // await testDefaultTokenValue();
    // await testCheckInitStartScreenValues();
    // await testCheckTitlePosAfterTwoDraw();
    // await testChangeUserUsername();
    // await testChangeMaxUsername();
    // await testDeleteUsername();
    // await testCheckSpecialChars();
    // await testHighScoreButton();
    // await testCreateGameButton();
    // await testJoinLobbyButton();
    /* End start screen tests */

    /* Lobby Screen tests */
    // await testCheckLobbyInitValues();
    // await testCheckTokenIsBeingDisplayed();
    // await testAddAndRemoveBotsFromLobby();
    // await checkPlayCardValues();

    class mockSocketGameScreen{
        constructor(){
            this.callNumber = 1
        }
        send(x) {
            var e = JSON.parse(x);
            var d = JSON.parse(e.data);
            if (x != null) {
                CheckSame(1,1,"ServerReceiving_" + d.move.toUpperCase() + "_" + this.callNumber)
            } else {
                CheckSame(0,1,"ServerReceiving_" + d.move.toUpperCase() + "_" + this.callNumber)
            }
            this.callNumber += 1
        }
        onmessage(x) {}
    }
    /* socket */
    global.socket = new mockSocketGameScreen();

    /* Game Screen tests*/
    await testGameArrayNotNull(); 
    await testGameScreenRotateKeyPress();
    await testGameScreenFailRotateKeyPress();
    await testFourRotate();
    await testMove();
    await testNewSquare();
    await testNumberOfPlayers();
    await testRandomGeneratedShapes();
    /* End Game Screen tests*/

    await integrationTest1();
    // console.log(mStartScreen);
    // console.log(mLobbyScreen);
    console.log(green, "\n" + (numTests-numFailed-1) + " passed");
    console.log(red, numFailed + " failed");
}

/** 
BUTTON FINDER 3700
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
