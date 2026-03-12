const int POT_PIN = A0;
const int LED_PINS[] = {5, 6, 7, 8, 9};  
const int LED_COUNT = 5;           

int potValue = 0;                 
int ledsToLight = 0;               
int lastPotValue = -1;            

void setup() {
  Serial.begin(9600);
  
  for (int i = 0; i < LED_COUNT; i++) {
    pinMode(LED_PINS[i], OUTPUT);
    digitalWrite(LED_PINS[i], LOW);  
  }
}

void loop() {
  potValue = analogRead(POT_PIN);
  
  ledsToLight = map(potValue, 0, 1023, 0, LED_COUNT);
  
  updateLEDs(ledsToLight);
  
  if (abs(potValue - lastPotValue) > 20) {  
    lastPotValue = potValue;
    
    Serial.print("Потенциометр: ");
    Serial.print(potValue);
    Serial.print(" / Включено светодиодов: ");
    Serial.print(ledsToLight);
    
    Serial.println("");
  }
  
  delay(50);
}

void updateLEDs(int ledsOn) {
  for (int i = 0; i < LED_COUNT; i++) {
    if (i < ledsOn) {
      digitalWrite(LED_PINS[i], HIGH);
    } else {
      digitalWrite(LED_PINS[i], LOW);
    }
  }
}