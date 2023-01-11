#include "funshield.h"
/*
Matěj Mezera
Počítačové systémy 2021/2022
Program that simulates a dice throw in game Dungeons and dragons
*/
constexpr int leds[] = { led1_pin, led2_pin, led3_pin, led4_pin };
constexpr int ledCount = sizeof(leds) / sizeof(leds[0]);
constexpr int buttons[] = { button1_pin, button2_pin, button3_pin };
constexpr int buttonCount = sizeof(buttons) / sizeof(buttons[0]);
constexpr int dices[] = { 4, 6, 10, 12, 20, 100};
constexpr int divisors[] = {1 ,10 , 100, 1000};
constexpr int diceCount = sizeof(dices) / sizeof(dices[0]);
constexpr int glyph_d = 0b10100001;
constexpr int empty_glyph = 0b11111111;
constexpr int digitCount = 4;

int dice_index = 0;
int throws = 1;
int sum = 0;
int led_pos = 0;
unsigned long last_blick = 0;

enum Mode {NORMAL, CONFIG, GENERATING};
Mode mode = CONFIG;

void setup() {
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

void ChangeMode(Mode mode_) {
  if (mode != mode_)
  {
    mode = mode_;
    if (mode == CONFIG)
    {
      dice_index = 0;
      throws = 1;
    }
  }
}

enum function {GENERATE, RAISE_THROWS, CHANGE_DICE};
enum button_state {PRESSED, RELEASED};

class Switcher {
  public:
    int index_;
    Switcher(int index) {
      index_ = index;
      fce_ = index_;
    }
    void Work(bool button_pressed) {
      if (button_pressed)
      {
        if (prev_state_ == RELEASED)
        {
          DoYourFunction();
        }
        prev_state_ = PRESSED;
      }
      else
      {
        if (prev_state_ == PRESSED && fce_ == GENERATE)
        {
          ChangeMode(NORMAL);
          led_pos = 0;
          TurnOffLeds();
        }
        prev_state_ = RELEASED;
      }
    }
  private:
    button_state prev_state_ = RELEASED;
    function fce_;
    void DoYourFunction()
    {
      switch(fce_)
      {
        case GENERATE:
          ChangeMode(GENERATING);
          break;
        case RAISE_THROWS:
          if (mode == NORMAL)
            ChangeMode(CONFIG);
          else if (mode == CONFIG)
            throws++;
          if (throws == 10)
            throws = 1;
          break;
        case CHANGE_DICE:
          if (mode == NORMAL)
            ChangeMode(CONFIG);
          else if (mode == CONFIG)
            dice_index = (dice_index + 1)%diceCount;
          break;
      }
    }
};

Switcher Switchers[] = {Switcher(0), Switcher(1), Switcher(2)};

class Display {
  public:
    void set( int n) {
      letter_number_ = n;
    }
    void do_loop() {
      writeDigit(letter_number_, pos_);
      pos_ = (pos_ + 1) % digitCount;
    }
  private:
    int pos_ = 0;
    int letter_number_ = 0;
    void writeDigit(int data, int pos) {
      digitalWrite( latch_pin, LOW);
      if (pos == 2 && mode == CONFIG)
        shiftOut( data_pin, clock_pin, MSBFIRST, glyph_d);
      else if (data >= divisors[pos])
        shiftOut( data_pin, clock_pin, MSBFIRST, digits[(data/divisors[pos])%10]);
      else
        shiftOut( data_pin, clock_pin, MSBFIRST, empty_glyph);
      shiftOut( data_pin, clock_pin, MSBFIRST, digit_muxpos[3 - pos]);
      digitalWrite( latch_pin, HIGH);
    }
};

Display d;

int GenerateNumber(int throws, int dice_sides) {
  int sum = 0;
  for (int i = 0;  i < throws; i++)
    sum += random(1, dices[dice_index] + 1);
  return sum;
}

int LedAnimation(int curr_time, int last_blick) {
  if (curr_time - last_blick == 100)
  {
    led_pos = (led_pos + 1)%ledCount;
    last_blick = curr_time;
  }
  for (int j = 0; j < ledCount;j++)
  {
    if (j <= led_pos)
      digitalWrite(leds[3 - j],ON);
    else
      digitalWrite(leds[3 - j],OFF);
  }
  return last_blick;
}

void TurnOffLeds() {
  for (int i = 0; i < ledCount;i++)
        digitalWrite(leds[i],OFF);
}
void loop() {
  switch(mode)
  {
    case CONFIG:
      last_blick = millis();
      sum = throws*1000 + dices[dice_index];
      break;
    case NORMAL:
      last_blick = millis();
      break;
    case GENERATING:
      sum = GenerateNumber(throws, dices[dice_index]);
      d.set(sum);
      last_blick = LedAnimation(millis(), last_blick);
      break;
  }
  d.set(sum);
  d.do_loop();
  for (int i = 0; i < buttonCount; i++)
  {
    bool button_pressed = ! digitalRead(buttons[i]);
    Switchers[i].Work(button_pressed);
  }
}
