// Определяем пины
const int buttonPin = 2;   
const int ledPin = 11;     

// Переменные для управления
int brightness = 0;        
int fadeStep = 5;          
bool isRising = true;      
bool wasPressed = false; 

void setup() {
  pinMode(ledPin, OUTPUT);
  pinMode(buttonPin, INPUT_PULLUP);
  
  analogWrite(ledPin, 0);
  // Монитор
  Serial.begin(9600);
}

void loop() {
  bool buttonState = digitalRead(buttonPin);
  
  if (buttonState == LOW) {
    wasPressed = true; 
    
    analogWrite(ledPin, brightness);
    delay(30);
    
    if (isRising) {
      brightness += fadeStep;
      if (brightness >= 255) {
        isRising = false;
        Serial.println("Достигнут максимум, начинаю затухать");
      }
    } else {
      brightness -= fadeStep;
      if (brightness <= 0) {
        isRising = true;
        Serial.println("Достигнут минимум, начинаю разгораться");
      }
    }
  } 
  else if (wasPressed) {
    wasPressed = false;
    Serial.print("Кнопку отпустили. Останавливаюсь на яркости: ");
    Serial.println(brightness);
  }
  delay(10);
}