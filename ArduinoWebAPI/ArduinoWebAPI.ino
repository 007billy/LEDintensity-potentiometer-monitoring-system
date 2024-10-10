String data;
char d1;
String x;
String val;
int ledval;


void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  pinMode(3, OUTPUT);
}

void loop() {
  // put your main code here, to run repeatedly:
  if(Serial.available()){
    data = Serial.readString();
    d1 = data.charAt(0);
    switch(d1) {
      case 'L':
      x = data.substring(1);
      ledval = x.toInt();
      analogWrite(3,ledval);
      break;
      case 'G':
      Serial.println(val);
      }
  }
  
  HandlePot();
}

 void HandlePot(void) 
 {
  
  static int old = 0;
  int current = 0, upper, lower;
  current = analogRead(A0);
  upper = current +2;
  lower = current -2;
  if(current != old)
  {
    if((old <= lower) || (old >= upper))
    {
      val = String(current);
      old =current;  
    }
    
  }
 }
