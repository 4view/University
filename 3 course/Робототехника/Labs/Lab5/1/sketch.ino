#include <Servo.h>

Servo servo;
const int button = 2;
const int servoPin = 9;
int pos = 0;
bool rotating = false;
bool endReached = false;

void setup() {
  servo.attach(servoPin);
  pinMode(button, INPUT_PULLUP);
  servo.write(0);
  Serial.begin(9600);
}

void loop() {
  if (digitalRead(button) == LOW) {
    rotating = true;

    if (endReached == false)
    {
      pos = (pos + 1);
      servo.write(pos);
      delay(5);
    }
    else
    {
      pos = (pos - 1);
      servo.write(pos);
      delay(5);
    }

    if (pos == 180)
    {
      endReached = true;
    }
    else if (pos == 0)
    {
      endReached = false;
    }
  } 
  else {
    if (rotating) {
      rotating = false;
      Serial.print("ОСТАНОВЛЕН на позиции: ");
      Serial.println(pos);
    }
  }
  
  delay(10);
}