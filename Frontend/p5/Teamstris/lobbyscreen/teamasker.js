

function teamNameAsker(team) {
    push();
    this.team = team;
    translate(windowWidth/2, windowHeight/2)
    fill(255)
    rect(0,0,windowWidth/2,windowHeight/4, 30)
    fill(0)
    textSize(windowWidth/20);
    text("Enter Team Name", 0, -windowHeight/15);
    textSize(windowWidth/30);
    text(team.teamName, 0, windowHeight/17);
    stroke("black")
    strokeWeight(6)
    line(windowWidth/5, windowHeight/10, -windowWidth/5, windowHeight/10)
    pop();
}