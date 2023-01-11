#include "funshield.h"
int number = 0;
int position = 0;
constexpr int digit_count = 4;
constexpr int buttons[] = { button1_pin, button2_pin, button3_pin };
constexpr int buttonCount = sizeof(buttons) / sizeof(buttons[0]);
constexpr int divisors[] = {1, 10, 100, 1000};

void setup() {
  // put your setup code here, to run once:
  pinMode( latch_pin, OUTPUT);
  pinMode( data_pin, OUTPUT);
  pinMode( clock_pin, OUTPUT);
  for (auto button : buttons) {
    pinMode(button, INPUT);
  }
}

enum change {RUN_STOP, RUN_LAP, RESET, NO_CHANGE};
change global_change = NO_CHANGE;
enum button_state {PRESSED, RELEASED};
class Switcher {
  public:
    change change_;
    Switcher(change change) {
      change_ = change;
    }
    void ChangeState(bool button_pressed){
      if (button_pressed)
      {
        if (prev_state_ == RELEASED)
        {
          prev_state_ = PRESSED; 
          global_change = change_;
        }
        else
        {
          prev_state_ = PRESSED;
          global_change = NO_CHANGE;
        }
      }
      else
      {
        prev_state_ = RELEASED;
        global_change = NO_CHANGE;
      }
    }
  private:
    button_state prev_state_ = RELEASED;
};

Switcher Switchers[] = {Switcher(RUN_STOP), Switcher(RUN_LAP), Switcher(RESET)};

class Display {
  public:
    void set( int n) {
      data_ = n;
    }
    void do_loop() {
      writeDigit(data_, pos_);
      pos_ = (pos_ + 1) % digit_count;
    }
  private:
    int pos_ = 0;
    int data_ = 0;
    void writeDigit(int data, int pos) {
      if (pos <= 1 || data > divisors[pos])
      digitalWrite( latch_pin, LOW);
      if (pos == 1)
        shiftOut( data_pin, clock_pin, MSBFIRST, digits[(data/divisors[pos]) % 10 ] & 0x7F);
      else
        shiftOut( data_pin, clock_pin, MSBFIRST, digits[(data/divisors[pos]) % 10]);
      shiftOut( data_pin, clock_pin, MSBFIRST, digit_muxpos[3 - pos]);
      digitalWrite( latch_pin, HIGH);
    }
};

Display d;

enum State {STOPPED, RUNNING, LAPPED};

class Stopwatch {
  public:
    void setState(State new_state) {
      state_ = new_state;
    }
    void ChangeState(change state_change) {
      switch(state_change) {
        case RESET:
          if (state_ == STOPPED)
            Reset();
          break;
        case RUN_STOP:
          if (state_ == RUNNING)
            state_ = STOPPED;
          else if (state_ == STOPPED)
            state_ = RUNNING;
          break;
        case NO_CHANGE:
          break;
      }
    }
  private:
    unsigned long time_ = 0;
    State state_ = STOPPED;
    void Reset() {
      time_ = 0;
      state_ = STOPPED;
    }
};

void loop() {
  // put your main code here, to run repeatedly
  d.set(number);
  d.do_loop();
  for (int i = 0; i < buttonCount; i++)
  {
    bool button_pressed = ! digitalRead(buttons[i]);
    Switchers[i].ChangeState(button_pressed);
  }
}