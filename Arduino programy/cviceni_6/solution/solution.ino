#include "funshield.h"
int number = 0;
int position = 0;
constexpr int buttons[] = { button1_pin, button2_pin, button3_pin };
constexpr int buttonCount = sizeof(buttons) / sizeof(buttons[0]);
constexpr int adds[] = { 1, 10, 100, 1000 };
constexpr int maxInt = 10000;
void setup() {
  // put your setup code here, to run once:
  pinMode( latch_pin, OUTPUT);
  pinMode( data_pin, OUTPUT);
  pinMode( clock_pin, OUTPUT);
  for (auto button : buttons) {
    pinMode(button, INPUT);
  }
}
enum function {ADD, SUBTRACT, MOVE_POSITION};
enum button_state {PRESSED, RELEASED};
class Switcher {
  public:
    function fce_;
    Switcher(function fce) {
      fce_ = fce;
      }
    void ChangeNumber() {
      switch (fce_) {
      case ADD:
        number = (number + adds[position]) % 10000;
        break;
      case SUBTRACT:
        number = (number - adds[position]);
        if (number < 0)
          number += 10000;
        break;
      case MOVE_POSITION:
        position = (position + 1) % 4;
        break;
      }
    }
    void DoYourFunction(bool button_pressed){
      if (button_pressed)
      {
        if (prev_state == RELEASED)
        { 
          ChangeNumber();
        }
        prev_state = PRESSED;
      }
      else
        prev_state = RELEASED;
    }
  private:
    button_state prev_state = RELEASED;
};
Switcher Switchers[] = {Switcher(ADD), Switcher(SUBTRACT), Switcher(MOVE_POSITION)};

void writeDigit(int number, int pos)  {
  int view = 0;
  view = (int)(number / adds[pos]) % 10;
  digitalWrite( latch_pin, LOW);
  shiftOut( data_pin, clock_pin, MSBFIRST, digits[(int)view]);
  shiftOut( data_pin, clock_pin, MSBFIRST, digit_muxpos[3-pos]);
  digitalWrite( latch_pin, HIGH);
}
void visualize()  //zobrazuje číslo přes celý display
{
  writeDigit(number ,0);
  writeDigit(number ,1);
  writeDigit(number ,2);
  writeDigit(number ,3);
}
void loop() {
  // put your main code here, to run repeatedly:¨
  for (int i = 0; i < buttonCount; i++)
  {
    writeDigit(number ,position);
    bool button_pressed = ! digitalRead(buttons[i]);
    Switchers[i].DoYourFunction(button_pressed);
  }
}