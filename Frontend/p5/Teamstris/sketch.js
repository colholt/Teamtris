var socket;
function setup() {
  /* Create our canvas with the max windowWidth and windowHeight */
  createCanvas(windowWidth, windowHeight)
  /* Have all our rectangles in center mode */
  rectMode(CENTER);
  /* Have all our text in center mode */
  textAlign(CENTER)

  socket = io.connect("ws://0.0.0.0:5202/play");
  // socket.emit('play', playerPreferences);
}

function draw() {
  background(0);
}
