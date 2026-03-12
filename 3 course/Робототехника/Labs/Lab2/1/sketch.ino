int ledPins[] = {8,9,10};
int ledCount = 3;

void setup() {
  for (int i = 0; i < ledCount; i++)
  {
    pinMode(ledPins[i], OUTPUT);
  }
}

void loop() {
  for (int i = 0; i < ledCount; i++)
  {
    digitalWrite(ledPins[i], HIGH);
  }

  delay(1000);

  for (int i = 0; i < ledCount; i++)
  {
    digitalWrite(ledPins[i], LOW);
    delay(500);
  }
}
