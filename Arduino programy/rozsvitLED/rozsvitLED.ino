#include "funshield.h"

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


class Counter
{
  public Counter(int pin, int funkce) {
    pin_ = pin;
    funkce_ = funkce;
  }
  void Visualize(int cislo)
  {
    for (int i = 0; i < 4; i++)
    {
      if i
    }
  }
  void Proved() {
    if (_funkce == 0)
      cislo++;
    if (_funkce == 1)
      cislo--;
    if (funkce == 2)
      cislo = 0;
    Visualize(cislo);
  }
}

void loop() {
  // put your main code here, to run repeatedly:
  for (int i = 0; i < buttonCount; i++) {
    bool pressed = !digitalRead(buttons[i]);
  }
}