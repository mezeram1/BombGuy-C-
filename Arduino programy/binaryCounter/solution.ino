#include "funshield.h"
int cislo = 0;
int pozice = 0;
int posun = 1;
constexpr int leds[] = { led1_pin, led2_pin, led3_pin, led4_pin };
constexpr int ledCount = sizeof(leds) / sizeof(leds[0]);
constexpr int buttons[] = { button1_pin, button2_pin, button3_pin };
constexpr int buttonCount = sizeof(buttons) / sizeof(buttons[0]);
void setup() {
  // put your setup code here, to run once:
  pinMode( latch_pin, OUTPUT);
  pinMode( data_pin, OUTPUT);
  pinMode( clock_pin, OUTPUT);
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
    void Visualize() {
      for (int k = 0; k < 4; k++) {
      if (((cislo << k) & 8) == 8)
        digitalWrite(leds[k],ON);
      else
        digitalWrite(leds[k],OFF);
      }
    }
    void ChangeNumber() {
      switch(pozice) {
        case 0:
          posun = 1;
          break;
        case 1:
          posun = 10;
          break;
        case 2:
          posun = 100;
          break;
        case 3:
          posun = 1000;
          break;
      if (index_ == 0)
        cislo = (cislo + 1) % 10000;
      else if (index_ == 1)
        cislo = (cislo - 1) % 10000;
      else
        pozice = (pozice + 1) % 4;
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
        if (button_pressed)
        {
          if (prev_state == RELEASED)
          {
            last_press = current_time; 
            ChangeNumber();
          }
          else
          {
            if (current_time >= 1000 + last_press)
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
  private:
    bool do_loop = false;
    unsigned long last_press = 0;
    button_state prev_state = RELEASED;
};
Switcher Switchers[] = {Switcher(0), Switcher(1), Switcher(2)};
void writeGlyphR( byte glyph, int pos) {
  digitalWrite( latch_pin, LOW);
  shiftOut( data_pin, clock_pin, MSBFIRST, glyph);
  shiftOut( data_pin, clock_pin, MSBFIRST, digit_muxpos[3-pos]);
  digitalWrite( latch_pin, HIGH);
}
void writeGlyphL( byte glyph, int pos) {
  digitalWrite( latch_pin, LOW);
  shiftOut( data_pin, clock_pin, MSBFIRST, glyph);
  shiftOut( data_pin, clock_pin, MSBFIRST, digit_muxpos[pos]);
  digitalWrite( latch_pin, HIGH);
}

void writeDigit(int cislo, int pos)  {
  int zobraz;
  switch(cislo) {
    case 0:
      zobraz = cislo % 10;
      break;
    case 1:
      zobraz = (cislo % 10)/10;
      break;
    case 2:
      zobraz = (cislo % 1000)/100;
      break;
    case 3:
      zobraz = (cislo % 10)/1000;
      break;
  }
  digitalWrite( latch_pin, LOW);
  shiftOut( data_pin, clock_pin, MSBFIRST, digits[cislo]);
  shiftOut( data_pin, clock_pin, MSBFIRST, digit_muxpos[3-pos]);
  digitalWrite( latch_pin, HIGH);
}
void loop() {
  // put your main code here, to run repeatedly:¨
  for (int i = 0; i < buttonCount; i++)
  {
    writeDigit(cislo,pozice);
    bool button_pressed = ! digitalRead(buttons[i]);
    auto current_time = millis();
    Switchers[i].DoYourFunction(button_pressed);
  }
}