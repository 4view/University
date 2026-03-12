void morseChar(String pattern)
{
  for (int i = 0; i < pattern.length(); i++)
  {
    if (pattern[i] == '.')
    {
      digitalWrite(13, HIGH);
      delay(200);
      digitalWrite(13, LOW);
      delay(200);
    }
    else if (pattern[i] == '-')
    {
      digitalWrite(13, HIGH);
      delay(600);
      digitalWrite(13, LOW);
      delay(200);
    }
  }

  delay(1000);
}

void setup() {
  pinMode(13, OUTPUT);
}

void loop() {
  morseChar(".-");
  morseChar("-.");
  morseChar("-");
  morseChar("---");
  morseChar("-.");

  delay(2000);
}
