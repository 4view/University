int led1 = 8;
int led2 = 9;
int led3 = 10;
int buttonPin = 2;

bool lastButtonState = HIGH;
bool currentButtonState;
int pressCounter = 0;

void setup()
{
  pinMode(led1, OUTPUT);
  pinMode(led2, OUTPUT);
  pinMode(led3, OUTPUT);

  digitalWrite(led1, LOW);
  digitalWrite(led2, LOW);
  digitalWrite(led3, LOW);

  pinMode(buttonPin, INPUT_PULLUP);
}

void loop()
{
  currentButtonState = digitalRead(buttonPin);

  if (lastButtonState == HIGH && currentButtonState == LOW)
  {
    delay(50);

    if (digitalRead(buttonPin) == LOW)
    {
      pressCounter++;

      if (pressCounter % 2 == 1)
      {
        digitalWrite(led1, HIGH);
        digitalWrite(led2, HIGH);
        delay(500);
        digitalWrite(led1, LOW);
        digitalWrite(led2, LOW);
      }
      else
      {
        digitalWrite(led2, HIGH);
        digitalWrite(led3, HIGH);
        delay(500);
        digitalWrite(led2, LOW);
        digitalWrite(led3, LOW);
      }
    }
  }

  lastButtonState = currentButtonState;
}