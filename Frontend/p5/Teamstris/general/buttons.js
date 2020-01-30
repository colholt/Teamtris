var buttonList = [];

class Buttons {
  // [x cord, y cord, width, height, gamestate number]
  constructor(x, y, w, h, gs, color) {
    if (buttons_constructor) console.log("Creating Button");
    if (buttons_constructor) console.log("x: " + x + " y: " + y + " w: " + w + " h: " + h);
    this.x = x; // pixel values
    this.y = y; // pixel values
    this.w = w;
    this.h = h;
    this.gs = gs; // game state
    this.LeftX = x + w / 2;
    this.RightX = x - w / 2;
    this.TopY = y - h / 2;
    this.BotY = y + h / 2;

    this.round = 20;
    this.id = "";

    this.text = "Test"
    this.hoverColor = 0;
    this.color = color;
    this.currentColor = this.color;

    this.invalid = false;
  }

  draw() {
    push();
    if (this.gs == gameState && !this.invalid) {
      translate(windowWidth / 2, windowHeight / 2);
      this.currentColor = this.color
      if (this.checkMouse()) { // Checking to see if the mouse is over the button
        this.currentColor = this.hoverColor;
      }
      fill(this.currentColor);
      rect(this.x, this.y, this.w, this.h, this.round);

      fill(0);
      textSize(windowWidth/50);
      text(this.text, this.x, this.y);
      if (buttons_draw) console.log(this.checkMouse());
    }
    pop();
  }
  /**
   * buttons_checkmouse: Checks to see if the mouse is over the button.
   * 
   * @param void
   * 
   * @returns boolean
   *          true - if mouse is over button
   *          false - if mouse is NOT over button
   */
  checkMouse() {
    if (!this.invalid) {
      if (buttons_checkmouse) console.log(mouseX - (windowWidth / 2) + ":" + this.RightX);
      if ((mouseX - (windowWidth / 2) >= this.RightX) && (mouseX - (windowWidth / 2) <= this.LeftX)) {
        if ((mouseY - (windowHeight / 2) >= this.TopY) && (mouseY - (windowHeight / 2) <= this.BotY)) {
          return true;
        }
      }
    }
  }
}

function Buttonloop() {
  buttonList.forEach(function (button) {
    button.draw();
  })
}

function ClickedLoop() {
  for (let i = 0; i < buttonList.length; i++) {
    if (buttonList[i].checkMouse()) {
      return buttonList[i].id;
    }
  }
}

function FindButtonbyID(id) {
  for (let i = 0; i < buttonList.length; i++) {
    if (buttonList[i].id == id) {
      return i;
    }
  }
}