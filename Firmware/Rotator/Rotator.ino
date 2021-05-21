
#define STEP 3
#define DIR  6
#define EN   8


// Enable only one stepper motor driver!
//#define NODRV
#define DRV8825 // DRV8825: Must be set to 32 microsteps
//#define DRVST810 // ST820: Must be set to 256 microsteps
///////////////////////////////////////

#ifdef DRV8825
  #define STEPPER_ENABLE LOW
  #define STEPPER_DISABLE HIGH

  #define RIGHT_DIRECTION HIGH
  #define LEFT_DIRECTION LOW
  #define STEP_DELAY_US 800
  #define STEPS_PER_REVOLUTION 6400
#endif

#ifdef ST820
  #define STEPPER_ENABLE HIGH
  #define STEPPER_DISABLE LOW

  #define RIGHT_DIRECTION LOW
  #define LEFT_DIRECTION HIGH
  #define STEP_DELAY_US 100
  #define STEPS_PER_REVOLUTION 51200
#endif

#ifdef NODRV
  #define STEPPER_ENABLE LOW
  #define STEPPER_DISABLE HIGH

  #define RIGHT_DIRECTION LOW
  #define LEFT_DIRECTION HIGH

  #define STEPS_PER_REVOLUTION 0
#endif




#define GEAR_RATIO 1.0

#define FLOAT_ERR 9999.99
#define MAX_ANGLE 360.0

#define DEVICE_IDENTIFICATION "ROTATOR"
#define CMD_IDENTIFICATION "ID"
#define CMD_TURN_RIGHT "TR"
#define CMD_TURN_LEFT "TL"
#define CMD_ABSOLUTE "TA"
#define CMD_POSITION "GP"
#define CMD_STOP "ST"
#define CMD_IS_MOVING "MV"
#define CMD_MOTOR_POWER_OFF "MOFF"
#define CMD_MOTOR_POWER_ON "MON"

double g_steps_per_degree;
long g_pos_mech = 0;
long g_pos_goal = 0;
long g_max_steps = 0;

bool _notMotorPowerOff = false;

String g_command = "";
bool g_commandComplete = false;

void setup() {
  Serial.begin(9600);
  
  pinMode(STEP, OUTPUT);
  pinMode(DIR, OUTPUT);
  pinMode(EN, OUTPUT);
  digitalWrite(EN, STEPPER_DISABLE);

  g_steps_per_degree = STEPS_PER_REVOLUTION / 360.0;
  g_max_steps = FromDegreeToStep(MAX_ANGLE);
}


long FromDegreeToStep(float deg)
{
  return (long)(deg * g_steps_per_degree);
}

float FromStepToDegree(long step)
{
  return (float)(step / g_steps_per_degree);
}

void MoveRight(long steps)
{
  if((g_pos_mech + steps) > g_max_steps)
  {
    steps = g_max_steps - g_pos_mech;
  }
  g_pos_goal = g_pos_mech + steps;
}

void MoveLeft(long steps)
{
  if((g_pos_mech - steps) < 0.0)
  {
    steps = g_pos_mech;
  }
  g_pos_goal = g_pos_mech -  steps;
}

void Move(float angle)
{
  float setAngle = FromStepToDegree(g_pos_mech);
  float diff = angle - setAngle;

  if(diff < 0.0)
  {
    // Left turn requested
    long steps = FromDegreeToStep(-diff);
    MoveLeft(steps);
  }
  if(diff > 0.0)
  {
    long steps = FromDegreeToStep(diff);
    MoveRight(steps);
  }
}

float Extract(String cmdid, String cmdstring)
{
  cmdstring.remove(0, cmdid.length());
  cmdstring.replace(':', ' ');
  cmdstring.trim();
  return cmdstring.toFloat();
}

void Dispatcher()
{
  if(g_command.startsWith(CMD_IDENTIFICATION))
  {
    Serial.print(DEVICE_IDENTIFICATION);
    Serial.print('#');
  }
  else if(g_command.startsWith(CMD_TURN_RIGHT))
  {
    float val = Extract(CMD_TURN_RIGHT, g_command);
    long steps = FromDegreeToStep(val);
    MoveRight(steps);
    Serial.print("1#");
  }
  else if(g_command.startsWith(CMD_TURN_LEFT))
  {
    float val = Extract(CMD_TURN_RIGHT, g_command);
    long steps = FromDegreeToStep(val);
    MoveLeft(steps);
    Serial.print("1#");
  }
  else if(g_command.startsWith(CMD_ABSOLUTE))
  {
    float val = Extract(CMD_ABSOLUTE, g_command);
    Move(val);
    Serial.print("1#");
  }
  else if(g_command.startsWith(CMD_POSITION))
  {
    float val = FromStepToDegree(g_pos_mech);
    Serial.print(val);
    Serial.print('#');
  }
  else if(g_command.startsWith(CMD_STOP))
  {
    g_pos_goal = g_pos_mech;
    Serial.print("1#");
  }
  else if(g_command.startsWith(CMD_IS_MOVING))
  {
    if(g_pos_goal == g_pos_mech) 
      Serial.print("0#");
    else
      Serial.print("1#");
  }
  else if(g_command.startsWith(CMD_MOTOR_POWER_OFF))
  {
    _notMotorPowerOff = false;
    Serial.print("1#");
  }
  else if(g_command.startsWith(CMD_MOTOR_POWER_ON))
  {
    _notMotorPowerOff = true;
    Serial.print("1#");
  }
  else
    Serial.print("0#");
  
  g_command = "";
  g_commandComplete = false;
}

void loop() {
  if(g_commandComplete)
  {
    Dispatcher();
  }
  if(g_pos_goal > g_pos_mech)
  {
    digitalWrite(EN, STEPPER_ENABLE);
    digitalWrite(DIR, RIGHT_DIRECTION);
   
    delayMicroseconds(STEP_DELAY_US);
    digitalWrite(STEP, HIGH); 
    delayMicroseconds(STEP_DELAY_US);
    digitalWrite(STEP, LOW); 
    g_pos_mech++;
  }
  if(g_pos_goal < g_pos_mech)
  {
    digitalWrite(EN, STEPPER_ENABLE);
    digitalWrite(DIR, LEFT_DIRECTION);
   
    delayMicroseconds(STEP_DELAY_US);
    digitalWrite(STEP, HIGH); 
    delayMicroseconds(STEP_DELAY_US);
    digitalWrite(STEP, LOW); 
    g_pos_mech--;
  }
  if(g_pos_goal == g_pos_mech && _notMotorPowerOff == false)
    digitalWrite(EN, STEPPER_DISABLE); 
}

void serialEvent() {
  while (Serial.available()) {
    // get the new byte:
    char inChar = (char)Serial.read();
    if(inChar == '\n') continue;
    if(inChar == '\r') continue;
    // add it to the inputString:
    g_command += inChar;
    // if the incoming character is a newline, set a flag so the main loop can
    // do something about it:
    if (inChar == ':') {
      g_commandComplete = true;
    }
  }
}
