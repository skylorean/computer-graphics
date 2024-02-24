const ACCELERATION = 1;

class MovingLetterData {
  time = 0; // связать тайм с прошедшим временем
  shift = 0;
  startingSpeed = 1;

  constructor(startingSpeed) {
    this.startingSpeed = startingSpeed;
  }

  get time() {
    return this.time;
  }

  set time(value) {
    this.time = value;
  }

  get shift() {
    return this.shift;
  }

  set shift(value) {
    this.shift = value;
  }

  get startingSpeed() {
    return this.startingSpeed;
  }
}

window.onload = () => {
  const canvas = document.getElementById("canvas");
  const ctx = canvas.getContext("2d");

  const letters = [
    new MovingLetterData(12),
    new MovingLetterData(14),
    new MovingLetterData(16),
  ];

  const loop = () => {
    for (let letter of letters) {
      update(letter);
    }

    draw(ctx, letters);

    for (let letter of letters) {
      ++letter.time;
    }
  };

  setInterval(loop, 20); //requestAnimationFrame вместо setinterval
};

function draw(ctx, letters) {
  ctx.clearRect(0, 0, 800, 600);

  drawFirstLetter(ctx, letters[0].shift);
  drawSecondLetter(ctx, letters[1].shift);
  drawThirdLetter(ctx, letters[2].shift);
}

function update(letter) {
  letter.shift =
    letter.time * (letter.startingSpeed - (ACCELERATION * letter.time) / 2);

  if (letter.shift < 0) {
    letter.time = 0;
    letter.shift = 0;
  }
}

// Letters:

function drawFirstLetter(ctx, shift) {
  ctx.fillStyle = "blue";
  ctx.strokeStyle = "blue";
  ctx.lineWidth = 10;
  // трансформации у канваса ко всем выводимым объектам
  ctx.fillRect(10, 300 - shift, 10, 200);

  ctx.beginPath();
  ctx.arc(65, 445 - shift, 50, 0, 2 * Math.PI);
  ctx.closePath();
  ctx.stroke();

  ctx.beginPath();
  ctx.arc(55, 355 - shift, 40, 0, 2 * Math.PI);
  ctx.closePath();
  ctx.stroke();
}

function drawSecondLetter(ctx, shift) {
  ctx.fillStyle = "red";
  ctx.strokeStyle = "red";
  ctx.lineWidth = 10;

  ctx.fillRect(130, 300 - shift, 10, 200);

  ctx.beginPath();
  ctx.arc(185, 445 - shift, 50, 0, 2 * Math.PI);
  ctx.closePath();
  ctx.stroke();

  ctx.beginPath();
  ctx.arc(175, 355 - shift, 40, 0, 2 * Math.PI);
  ctx.closePath();
  ctx.stroke();
}

function drawThirdLetter(ctx, shift) {
  ctx.fillStyle = "black";
  ctx.strokeStyle = "black";
  ctx.lineWidth = 10;

  ctx.fillRect(280, 450 - shift, 100, 10);

  // left

  ctx.beginPath();
  ctx.moveTo(330, 300 - shift);
  ctx.lineTo(320, 300 - shift);
  ctx.lineTo(270, 460 - shift);
  ctx.lineTo(280, 460 - shift);
  ctx.lineTo(330, 300 - shift);
  ctx.closePath();
  ctx.fill();

  ctx.beginPath();
  ctx.moveTo(260, 450 - shift);
  ctx.lineTo(280, 450 - shift);
  ctx.lineTo(300, 500 - shift);
  ctx.lineTo(290, 500 - shift);
  ctx.lineTo(260, 450 - shift);
  ctx.closePath();
  ctx.fill();

  // right

  ctx.beginPath();
  ctx.moveTo(320, 300 - shift);
  ctx.lineTo(330, 300 - shift);
  ctx.lineTo(390, 460 - shift);
  ctx.lineTo(380, 460 - shift);
  ctx.lineTo(320, 300 - shift);
  ctx.closePath();
  ctx.fill();

  ctx.beginPath();
  ctx.moveTo(400, 450 - shift);
  ctx.lineTo(380, 450 - shift);
  ctx.lineTo(360, 500 - shift);
  ctx.lineTo(370, 500 - shift);
  ctx.lineTo(400, 450 - shift);
  ctx.closePath();
  ctx.fill();
}
