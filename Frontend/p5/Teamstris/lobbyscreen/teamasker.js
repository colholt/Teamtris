

function teamNameAsker(team) {
    push(); // push my settings 
    translate(windowWidth/2, windowHeight/2) // translate to the center of the screen
    fill(255) // fill white
    rect(0,0,windowWidth/2,windowHeight/2.5, 30) // draw the white square onto the screen
    fill(0) // fill black
    textSize(windowWidth/20); // set textsize relative to screen width
    text("Enter Team Name", 0, -windowHeight/9); // Draw "Enter Team Name" onto the screen
    textSize(windowWidth/30); // reset text size, still relative to screen width
    text(team.teamName, 0, windowHeight/50); // draw the teamname they are typing
    stroke("black") // set my stroke to black
    strokeWeight(6) // make my lines thicker
    line(windowWidth/5, windowHeight/15, -windowWidth/5, windowHeight/15) // draw the line poeple draw on
    

    /** 
     * The accept button will be drawn below this comment
     */
    let CBoxStroke = "black"; // default box color stroke
    let CTextfill = "white"; // defualt text color
    let Cfill = "black" // default button color

    this.LeftX = 0 + windowWidth/5 / 2;
    this.RightX = 0 - windowWidth/5 / 2;
    this.TopY = windowHeight/7.4 - windowHeight/10 / 2;
    this.BotY = windowHeight/7.4 + windowHeight/10 / 2;

    if(checkMouseTeamAccept()){
        Cfill = "white"
    }

    fill(Cfill) // fill black
    rect(0,windowHeight/7.4,windowWidth/5,windowHeight/10) // draw the accept box
    fill(CTextfill) // fill the text color
    text("Accept", 0, windowHeight/7.4) // draw the "Accept onto the screen"

    // stroke("black") // fill

    pop(); // restore my old settings
}

function checkMouseTeamAccept() {
    if ((mouseX - (windowWidth / 2) >= this.RightX) && (mouseX - (windowWidth / 2) <= this.LeftX)) {
        if ((mouseY - (windowHeight / 2) >= this.TopY) && (mouseY - (windowHeight / 2) <= this.BotY)) {
            return true;
        }
    }
    return false;
}