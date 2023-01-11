#include "funshield.h"
unsigned long number = 0;
int position = 0;
constexpr int dot = 0x7F;
constexpr int digit_count = 4;
constexpr int buttons[] = { button1_pin, button2_pin, button3_pin };
constexpr int buttonCount = sizeof(buttons) / sizeof(buttons[0]);
constexpr byte LETTER_GLYPH[] {
  0b10001000,   // A
  0b10000011,   // b
  0b11000110,   // C
  0b10100001,   // d
  0b10000110,   // E
  0b10001110,   // F
  0b10000010,   // G
  0b10001001,   // H
  0b11111001,   // I
  0b11100001,   // J
  0b10000101,   // K
  0b11000111,   // L
  0b11001000,   // M
  0b10101011,   // n
  0b10100011,   // o
  0b10001100,   // P
  0b10011000,   // q
  0b10101111,   // r
  0b10010010,   // S
  0b10000111,   // t
  0b11000001,   // U
  0b11100011,   // v
  0b10000001,   // W
  0b10110110,   // X
  0b10010001,   // Y
  0b10100100,   // Z
};

void setup() {
  // put your setup code here, to run once:
  pinMode( latch_pin, OUTPUT);
  pinMode( data_pin, OUTPUT);
  pinMode( clock_pin, OUTPUT);
  for (auto button : buttons) {
    pinMode(button, INPUT);
  }
}

class Display {
  public:
    void set( int n) {
      letter_number_ = n;
    }
    void do_loop() {
      writeDigit(letter_number_, pos_);
      pos_ = (pos_ + 1) % digit_count;
    }
  private:
    int pos_ = 0;
    int letter_number_ = 0;
    void writeDigit(int data, int pos) {
      digitalWrite( latch_pin, LOW);
      shiftOut( data_pin, clock_pin, MSBFIRST, LETTER_GLYPH[data]);
      shiftOut( data_pin, clock_pin, MSBFIRST, digit_muxpos[3 - pos]);
      digitalWrite( latch_pin, HIGH);
    }
};

Display d;

void loop() {
  // put your main code here, to run repeatedly
  for (int i = 0; i < 26; i++)
  {
    d.set(i);
    d.do_loop();
    delay(1000);
  }
}