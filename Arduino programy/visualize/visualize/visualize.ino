#include "funshield.h"
int cislo = 0;
constexpr int leds[] = { led1_pin, led2_pin, led3_pin, led4_pin };
constexpr int ledCount = sizeof(leds) / sizeof(leds[0]);
constexpr int buttons[] = { button1_pin, button2_pin, button3_pin };
constexpr int buttonCount = sizeof(buttons) / sizeof(buttons[0]);

void setup() {
  // put your setup code here, to run once:
  for (auto led : leds) {
    pinMode(led, OUTPUT);
    digitalWrite(led, OFF);
  }
  for (auto button : buttons) {
    pinMode(button, INPUT);
  }
}
enum function {ADD, SUBTRACT, SET_TO_ZERO};
enum button_state {PRESSED, RELEASED};
class Switcher {
  public:
    int index_;
    Switcher(int index) {
      index_ = index;
      }
    void ChangeNumber() {
      if (index_ == 0)
        cislo = (cislo + 1) % 16;
      else if (index_ == 1)
        cislo = (cislo - 1) % 16;
      else
        cislo = 0;
    }
    void DoYourFunction(bool button_pressed){
      unsigned long current_time = millis();
      if (do_loop)
      {
        if (button_pressed)
        {
          if (current_time >= last_press + 300)
          {
            ChangeNumber();
            last_press = current_time;
          }
        }
        else
          do_loop = false;
      }
      else
      {
        if (button_pressed)
        {
          if (prev_state == RELEASED)
          {
            last_press = current_time; 
            ChangeNumber();
          }
          else
          {
            if (current_time - last_press >= 1000)
            {
              ChangeNumber();
              do_loop = true;
              last_press = current_time;
            }
          }
          prev_state = PRESSED;
        }
        else
          prev_state = RELEASED;
      }
    }
  private:
    button_state prev_state = RELEASED;
    unsigned long last_press = 0;
    bool do_loop = false;
};

Switcher Switchers[] = {Switcher(0), Switcher(1), Switcher(2)};

void Visualize() {
  for (int k = 0; k < 4; k++) {
  if (((cislo << k) & 8) == 8)
    digitalWrite(leds[k],ON);
  else
    digitalWrite(leds[k],OFF);
  }
}

void loop() {
  // put your main code here, to run repeatedly:
  for (int i = 0; i < buttonCount; i++)
  {
    bool button_pressed = ! digitalRead(buttons[i]);
    Switchers[i].DoYourFunction(button_pressed);
    Visualize();
  }
}