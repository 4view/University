const int BUTTON_START_PIN = 2; 
const int BUTTON_STOP_PIN = 3;   
const int LED_PINS[] = {9, 10, 11};
const int LED_COUNT = 3;

bool startPressed = false;
bool stopPressed = false;
bool systemActive = false; 

void setup() {
  Serial.begin(9600);

  for (int i = 0; i < LED_COUNT; i++) {
    pinMode(LED_PINS[i], OUTPUT);
    digitalWrite(LED_PINS[i], LOW);
  }

  pinMode(BUTTON_START_PIN, INPUT_PULLUP);
  pinMode(BUTTON_STOP_PIN, INPUT_PULLUP);
  
  Serial.println("Система готова. Нажмите:");
  Serial.println("Кнопка START (пин 2) - запуск системы");
  Serial.println("Кнопка STOP  (пин 3) - остановка системы");
}

void loop() {
  bool startButtonState = digitalRead(BUTTON_START_PIN);
  bool stopButtonState = digitalRead(BUTTON_STOP_PIN);
  
  if (startButtonState == LOW) {
    if (digitalRead(BUTTON_START_PIN) == LOW && !startPressed) {
      startPressed = true;
      startSystem();
    }
  } else {
    startPressed = false;
  }
  
  if (stopButtonState == LOW) {
    if (digitalRead(BUTTON_STOP_PIN) == LOW && !stopPressed) {
      stopPressed = true;
      stopSystem();
    }
  } else {
    stopPressed = false;
  }
  
  delay(10);
}

void startSystem() {
  if (!systemActive) {
    systemActive = true;
    
    for (int i = 0; i < LED_COUNT; i++) {
      digitalWrite(LED_PINS[i], HIGH);
    }
    
    Serial.println(">>> СИСТЕМА ЗАПУЩЕНА: мотор работает, светодиоды горят");
  } else {
    Serial.println("Система уже работает!");
  }
}

void stopSystem() {
  if (systemActive) {
    systemActive = false;
    
    for (int i = 0; i < LED_COUNT; i++) {
      digitalWrite(LED_PINS[i], LOW);
    }
    
    Serial.println("<<< СИСТЕМА ОСТАНОВЛЕНА: мотор остановлен, светодиоды погашены");
  } else {
    Serial.println("Система уже остановлена!");
  }
}