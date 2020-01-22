var buttonList = [];

class Buttons {
  // [x cord, y cord, width, height, gamestate number]
  constructor(x, y, w, h, gs) {
    if(buttons_constructor) console.log("Creating Button");
    if(buttons_constructor) console.log("x: " + x + " y: " + y + " w: " + w + " h: " + h);
    this.x = x;
    this.y = y;
    this.w = w;
    this.h = h;
    this.gs = gs;
  }

  draw(){
    push();
    translate(windowWidth / 2, windowHeight / 2);
    if(buttons_draw) console.log("drawing buttons");
    rect(this.x, this.y, this.w, this.h);
    pop();
  }
}

function Buttonloop(){
  buttonList.forEach(function(button){
    button.draw();
  })
}
