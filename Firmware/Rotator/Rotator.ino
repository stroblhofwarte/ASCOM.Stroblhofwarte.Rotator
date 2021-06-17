
#define STEP 3
#define DIR  6
#define EN   8
#define SW 10

// Enable only one stepper motor driver!
//#define NODRV
#define TMC2130_STANDALONE // TMC2130 SilentStick with SPI jumper closed (Standalone) and all three jumpers open (1/16 µStepping interpolate to 256 steps, realy silent!)
//#define DRV8825 // DRV8825: Must be set to 32 microsteps
//#define DRVST810 // ST820: Must be set to 256 microsteps
///////////////////////////////////////
#ifdef TMC2130_STANDALONE
  #define STEPPER_ENABLE LOW
  #define STEPPER_DISABLE HIGH

  #define RIGHT_DIRECTION LOW
  #define LEFT_DIRECTION HIGH
  #define STEP_DELAY_US 1600
  #define STEPS_PER_REVOLUTION 3200
#endif


#ifdef DRV8825
  #define STEPPER_ENABLE LOW
  #define STEPPER_DISABLE HIGH

  #define RIGHT_DIRECTION HIGH
  #define LEFT_DIRECTION LOW
  #define STEP_DELAY_US 800
  #define STEPS_PER_REVOLUTION 28800 // 6400
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


#define SW_ACTIVE LOW  // Define the response for the limit switch

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
#define CMD_INFO "IF"
#define CMD_STEP_SIZE "SZ"
#define CMD_INIT "IN"
#define CMD_INIT_SPEED "IS"
#define CMD_SPEED "SP"
#define CMD_SET_PARK "PP"
#define CMD_PARK "PA"

double g_steps_per_degree;
long g_pos_mech = 0;
long g_pos_goal = 0;
long g_max_steps = 0;
bool g_is_init = false;
bool g_perform_init = false;

int g_speed;
int g_init_speed;
float g_parkpos;

bool _notMotorPowerOff = false;

String g_command = "";
bool g_commandComplete = false;
String g_info = "Not initialized yet.";

void setup() { 
  pinMode(STEP, OUTPUT);
  pinMode(DIR, OUTPUT);
  pinMode(EN, OUTPUT);
  pinMode(SW, INPUT_PULLUP);
  digitalWrite(EN, STEPPER_DISABLE);

  g_steps_per_degree = STEPS_PER_REVOLUTION / 360.0;
  g_max_steps = FromDegreeToStep(MAX_ANGLE);
  g_speed = STEP_DELAY_US;
  g_init_speed = STEP_DELAY_US;
  Serial.begin(9600);
}

void initialize()
{
  if(digitalRead(SW) == SW_ACTIVE)
  {
    // Switch is active, first the rotator must be moved right (45°) to leave the switch:
    digitalWrite(EN, STEPPER_ENABLE);
    digitalWrite(DIR, RIGHT_DIRECTION);
    for(int i = 0; i < STEPS_PER_REVOLUTION/8; i++)
    {
      delayMicroseconds(g_init_speed);
      digitalWrite(STEP, HIGH); 
      delayMicroseconds(g_init_speed);
      digitalWrite(STEP, LOW); 
      if(digitalRead(SW) != SW_ACTIVE)
        break;
    }
  }
  g_pos_goal = -STEPS_PER_REVOLUTION;
  g_perform_init = true;
  g_info = "Initialize...";
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
    long reverse = g_max_steps - steps;
    MoveLeft(reverse);
    return;
  }
  g_pos_goal = g_pos_mech + steps;
}

void MoveLeft(long steps)
{
  if((g_pos_mech - steps) < 0.0)
  {
    long reverse = STEPS_PER_REVOLUTION - steps;
    MoveRight(reverse);
    return;
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
  else if (g_command.startsWith(CMD_INFO))
  {
    Serial.print(g_info);
    Serial.print("#");
  }
  else if (g_command.startsWith(CMD_STEP_SIZE))
  {
    double stepSize = 360.0/(double)STEPS_PER_REVOLUTION;
    Serial.print(stepSize);
    Serial.print("#");
  }
  else if(g_command.startsWith(CMD_INIT))
  {
    initialize();
    Serial.print("1#");
  }
  else if(g_command.startsWith(CMD_INIT_SPEED))
  {
    float val = Extract(CMD_INIT_SPEED, g_command);
    g_init_speed = (int)((float)STEP_DELAY_US * val);
    Serial.print("1#");
  }
  else if(g_command.startsWith(CMD_SPEED))
  {
    float val = Extract(CMD_SPEED, g_command);
    g_speed = (int)((float)STEP_DELAY_US * val);
    Serial.print("1#");
  }
  else if(g_command.startsWith(CMD_SET_PARK))
  {
    float val = Extract(CMD_SET_PARK, g_command); 
    g_parkpos = val;
    Serial.print("1#");
  }
  else if(g_command.startsWith(CMD_PARK))
  {
    Move(g_parkpos);
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
  if(g_perform_init == false && g_is_init == false)
      return;

  if(g_pos_goal > g_pos_mech)
  {
    digitalWrite(EN, STEPPER_ENABLE);
    digitalWrite(DIR, RIGHT_DIRECTION);
   
    if(g_perform_init) delayMicroseconds(g_init_speed);
    else delayMicroseconds(g_speed);
    digitalWrite(STEP, HIGH); 
    if(g_perform_init) delayMicroseconds(g_init_speed);
    else delayMicroseconds(g_speed);
    digitalWrite(STEP, LOW); 
    g_pos_mech++;
  }
  if(g_pos_goal < g_pos_mech)
  {
    digitalWrite(EN, STEPPER_ENABLE);
    digitalWrite(DIR, LEFT_DIRECTION);
   
    if(g_perform_init) delayMicroseconds(g_init_speed);
    else delayMicroseconds(g_speed);
    digitalWrite(STEP, HIGH); 
    if(g_perform_init) delayMicroseconds(g_init_speed);
    else delayMicroseconds(g_speed);
    digitalWrite(STEP, LOW); 
    g_pos_mech--;
  }
  if(g_perform_init)
  {
    if(digitalRead(SW) == SW_ACTIVE)
    {
      // Init done, limit found:
      g_pos_mech = 0;
      g_pos_goal = 0;
      g_perform_init = false;
      g_is_init = true;
      g_info = "Ready.";
    }
    else
    {
      if(g_pos_mech == g_pos_goal)
      {
        // Limit switch not found!
        g_info = "ERR: Limit switch not found!";
        g_perform_init = false;
        g_is_init = false;
        digitalWrite(EN, STEPPER_ENABLE);
        digitalWrite(DIR, RIGHT_DIRECTION);
        for(int i = 0; i < STEPS_PER_REVOLUTION; i++)
        {
          delayMicroseconds(g_init_speed);
          digitalWrite(STEP, HIGH); 
          delayMicroseconds(g_init_speed);
          digitalWrite(STEP, LOW); 
        }
        if(_notMotorPowerOff == false)
          digitalWrite(EN, STEPPER_DISABLE); 
      }
    }
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
