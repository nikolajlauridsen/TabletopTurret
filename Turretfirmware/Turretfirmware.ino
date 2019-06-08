#include <Servo.h>

// Hardware pins
#define xServo 10
#define yServo 11
#define firePin 2


Servo xAxis, yAxis;
bool cannonOn = false;

void setup() {
  // put your setup code here, to run once:
  xAxis.attach(xServo);
  yAxis.attach(yServo);
  pinMode(firePin, OUTPUT);
  Serial.begin(115200);
  Serial.setTimeout(10);
}

void loop() {
  // put your main code here, to run repeatedly:

}

void serialEvent(){
  while(Serial.available()){
    parseCommand(Serial.readString());
  }
}

char command;
String data;
void parseCommand(String data){
  command = data.charAt(0);
  data = data.substring(2);

  switch(command){
    case 'm':
      turretMove(data);
    break;
    case 's':
      turretSweep();
    break;
    case 'f':
      // TODO: Take state from data
      if(!cannonOn){
        digitalWrite(firePin, HIGH);
        cannonOn = true; 
      } else {
        digitalWrite(firePin, LOW);
        cannonOn = false; 
      }
    break;
    default:
      Serial.println("Invalid command");
    break;
  }
}

String xCoord, yCoord;
int splitIndex;
void turretMove(String coords){
  splitIndex = coords.indexOf(',');
  xCoord = coords.substring(0, splitIndex);
  yCoord = coords.substring(splitIndex+1);
  
  xAxis.write(xCoord.toInt());
  yAxis.write(yCoord.toInt());
}

void turretSweep(){
  xAxis.write(0);
  yAxis.write(120);
  delay(1000);
  for(int i=0; i<=180; i+=1){
    xAxis.write(i);
    delay(10);
  }
  for(int i=180; i>=0; i-=1){
    xAxis.write(i);
    delay(10);
  }
  xAxis.write(0);
}

