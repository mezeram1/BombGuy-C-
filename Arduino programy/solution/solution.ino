#include "funshield.h"

constexpr int leds[] {led1_pin,led2_pin,led3_pin,led4_pin};
constexpr int led_count = 4;

void setup() {
  // put your setup code here, to run once:
  for (int i = 0; i < led_count; i++)
  {
    pinMode(leds[i], OUTPUT);
    digitalWrite(leds[i],OFF);
  }  
  }

unsigned long global_last_time = 0;

bool elapsed( unsigned long interval)
{
  auto cur_time = millis();
  if (cur_time >= global_last_time + interval)
  {
    global_last_time = cur_time;
    return true;
  }
  else return false;
}

int sviti = 5;
bool smer = false;

void blick(bool nastal_cas)
{
  if (nastal_cas)
  {
    if (sviti == 0 || sviti == 3)
      smer = ! smer;
    if (sviti == 5)
    {
      digitalWrite(leds[0],ON);
      sviti = 0;
    }
    else
    {
      digitalWrite(leds[sviti],OFF);
      if (smer)
        sviti++;
      else sviti--;
      digitalWrite(leds[sviti],ON);
    }
  }
}

void loop() {
  // put your main code here, to run repeatedly:
  blick(elapsed(300));
}
