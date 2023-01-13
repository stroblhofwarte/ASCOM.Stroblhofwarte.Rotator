// ------------------------------------------------ Rotator
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
#define GEAR_RATIO 4.8 // for Stroblhofwarte.Rotator device

// Main wheel: 96 teeth, Motorwheel: 20 teeth

#ifdef TMC2130_STANDALONE
  #define STEPPER_ENABLE LOW
  #define STEPPER_DISABLE HIGH

  #define RIGHT_DIRECTION LOW
  #define LEFT_DIRECTION HIGH
  #define STEP_DELAY_US 1600
  #define STEPS_PER_REVOLUTION 3200 * GEAR_RATIO
#endif


#ifdef DRV8825
  #define STEPPER_ENABLE LOW
  #define STEPPER_DISABLE HIGH

  #define RIGHT_DIRECTION HIGH
  #define LEFT_DIRECTION LOW
  #define STEP_DELAY_US 800
  #define STEPS_PER_REVOLUTION 6400 * GEAR_RATIO
#endif

#ifdef ST820
  #define STEPPER_ENABLE HIGH
  #define STEPPER_DISABLE LOW

  #define RIGHT_DIRECTION LOW
  #define LEFT_DIRECTION HIGH
  #define STEP_DELAY_US 100
  #define STEPS_PER_REVOLUTION 51200 * GEAR_RATIO
  
#endif

#ifdef NODRV
  #define STEPPER_ENABLE LOW
  #define STEPPER_DISABLE HIGH

  #define RIGHT_DIRECTION LOW
  #define LEFT_DIRECTION HIGH

  #define STEPS_PER_REVOLUTION 0
#endif


#define SW_ACTIVE LOW  // Define the response for the limit switch

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
#define CMD_SYNC "SY"

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

// ---------------------------------------------------- Focuser

#define FOC_STEP 2
#define FOC_DIR  5
#define FOC_EN   8

#define FOC_GEAR_RATIO 1.0

long FOC_OVERSHOOT_RIGHT= 0;
long FOC_OVERSHOOT_LEFT= 0;

// Enable only one stepper motor driver!
//#define NODRV
#define FOC_DRV8825 // TMC2130 SilentStick with SPI jumper closed (Standalone) and all three jumpers open (1/16 µStepping interpolate to 256 steps, realy silent!)
//#define DRV8825 // DRV8825: Must be set to 32 microsteps
//#define DRVST810 // ST820: Must be set to 256 microsteps
///////////////////////////////////////

#ifdef FOC_TMC2130_STANDALONE
  #define FOC_STEPPER_ENABLE LOW
  #define FOC_STEPPER_DISABLE HIGH

  #define FOC_RIGHT_DIRECTION HIGH
  #define FOC_LEFT_DIRECTION LOW
  #define FOC_STEP_DELAY_US 1600
  #define FOC_STEPS_PER_REVOLUTION 3200 * FOC_GEAR_RATIO
#endif


#ifdef FOC_DRV8825
  #define FOC_STEPPER_ENABLE LOW
  #define FOC_STEPPER_DISABLE HIGH

  #define FOC_RIGHT_DIRECTION HIGH
  #define FOC_LEFT_DIRECTION LOW
  #define FOC_STEP_DELAY_US 100
  #define FOC_STEPS_PER_REVOLUTION 6400 * FOC_GEAR_RATIO
#endif

#ifdef FOC_ST820
  #define FOC_STEPPER_ENABLE HIGH
  #define FOC_STEPPER_DISABLE LOW

  #define FOC_RIGHT_DIRECTION LOW
  #define FOC_LEFT_DIRECTION HIGH
  #define FOC_STEP_DELAY_US 100
  #define FOC_STEPS_PER_REVOLUTION 51200 * FOC_GEAR_RATIO
  
#endif

#ifdef FOC_NODRV
  #define FOC_STEPPER_ENABLE LOW
  #define FOC_STEPPER_DISABLE HIGH

  #define FOC_RIGHT_DIRECTION LOW
  #define FOC_LEFT_DIRECTION HIGH

  #define FOC_STEPS_PER_REVOLUTION 0
#endif

#define FOC_CMD_TURN_RIGHT "FOCTR"
#define FOC_CMD_TURN_LEFT "FOCTL"
#define FOC_CMD_STOP "FOCST"
#define FOC_CMD_IS_MOVING "FOCMV"
#define FOC_CMD_POS "FOCPO"
#define FOC_CMD_MOTOR_POWER_OFF "FOCMOFF"
#define FOC_CMD_MOTOR_POWER_ON "FOCMON"
#define FOC_CMD_OVERSHOOT_RIGHT "FOCOVERR"
#define FOC_CMD_OVERSHOOT_LEFT "FOCOVERL"

int g_foc_speed;
long g_foc_pos_mech = 0;
long g_foc_pos_goal = 0;

bool _foc_notMotorPowerOff = false;

#define FOC_OBVERSHOOT_DONE 0
#define FOC_OVERSHOOT_DIR_RIGHT 1
#define FOC_OVERSHOOT_DIR_LEFT 2
int g_foc_overshoot_handling = FOC_OBVERSHOOT_DONE;
long g_foc_powerOffTimeout = 0;
#define FOC_POWERTIMEOUT 1000

// --------------------------------

String g_command = "";
bool g_commandComplete = false;
String g_info = "Not initialized yet.";
bool g_focuserOperation = false;
bool g_rotatorOperation = false;

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

  pinMode(FOC_STEP, OUTPUT);
  pinMode(FOC_DIR, OUTPUT);
  pinMode(FOC_EN, OUTPUT);
  digitalWrite(FOC_EN, FOC_STEPPER_DISABLE);
  g_foc_speed = FOC_STEP_DELAY_US;
  
  Serial.begin(9600);
}

void initialize()
{
  g_pos_mech = 0;
  g_pos_goal = 0;
  g_perform_init = false;
  g_is_init = true;
  g_info = "Ready.";
}

// Region: Focuser

void FocMoveRight(long steps)
{
  g_foc_pos_goal = g_foc_pos_mech + steps;
}

void FocMoveLeft(long steps)
{
  g_foc_pos_goal = g_foc_pos_mech -  steps;
}

long FocExtract(String cmdid, String cmdstring)
{
  cmdstring.remove(0, cmdid.length());
  cmdstring.replace(':', ' ');
  cmdstring.trim();
  char tarray[32];
  cmdstring.toCharArray(tarray, sizeof(tarray));
  long steps = atol(tarray);
  return steps;
}

bool FocDispatcher()
{
  if(g_command.startsWith(FOC_CMD_TURN_RIGHT))
  {
    long val = Extract(FOC_CMD_TURN_RIGHT, g_command);
    long steps = val + FOC_OVERSHOOT_RIGHT;
    g_foc_overshoot_handling = FOC_OVERSHOOT_DIR_RIGHT;
    FocMoveRight(steps);
    Serial.print("1#");
  }
  else if(g_command.startsWith(FOC_CMD_TURN_LEFT))
  {
    long val = Extract(FOC_CMD_TURN_RIGHT, g_command);
    long steps = val + FOC_OVERSHOOT_LEFT;
    g_foc_overshoot_handling = FOC_OVERSHOOT_DIR_LEFT;
    FocMoveLeft(steps);
    Serial.print("1#");
  }
  else if(g_command.startsWith(FOC_CMD_STOP))
  {
    g_foc_pos_goal = g_foc_pos_mech;
    Serial.print("1#");
  }
  else if(g_command.startsWith(FOC_CMD_IS_MOVING))
  {
    if(g_foc_pos_goal == g_foc_pos_mech) 
      Serial.print("0#");
    else
      Serial.print("1#");
  }
  else if(g_command.startsWith(FOC_CMD_MOTOR_POWER_OFF))
  {
    _foc_notMotorPowerOff = false;
    Serial.print("1#");
  }
  else if(g_command.startsWith(FOC_CMD_MOTOR_POWER_ON))
  {
    _foc_notMotorPowerOff = true;
    Serial.print("1#");
  }
  else if(g_command.startsWith(FOC_CMD_POS))
  {
    Serial.print(g_foc_pos_mech);
    Serial.print("#");
  }
  else if(g_command.startsWith(FOC_CMD_OVERSHOOT_RIGHT))
  {
    long val = Extract(FOC_CMD_OVERSHOOT_RIGHT, g_command);
    long steps = val;
    FOC_OVERSHOOT_RIGHT = steps;
    FOC_OVERSHOOT_LEFT = 0;
    Serial.print("1#");
  }
  else if(g_command.startsWith(FOC_CMD_OVERSHOOT_LEFT))
  {
    long val = Extract(FOC_CMD_OVERSHOOT_LEFT, g_command);
    long steps = val;
    FOC_OVERSHOOT_LEFT = steps;
    FOC_OVERSHOOT_RIGHT  = 0;
    Serial.print("1#");
  }
  else
    return false;
  return true;
}

// Region: Rotator

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
  else if(g_command.startsWith(CMD_SYNC))
  {
    float val = Extract(CMD_SYNC, g_command);
    g_pos_mech = g_pos_goal = FromDegreeToStep(val);
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
  {
    if(!FocDispatcher())
      Serial.print("0#");
  }
  
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

  // ***********************************************   Rotator:
  if(g_pos_goal > g_pos_mech && g_focuserOperation == false)
  {
    g_rotatorOperation = true;
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
  if(g_pos_goal < g_pos_mech && g_focuserOperation == false)
  {
    g_rotatorOperation = true;
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
  if(g_pos_goal == g_pos_mech)
    g_rotatorOperation = false;
  // ***********************************************   Focuser:
  if(millis() - g_foc_powerOffTimeout > 1000 && g_foc_pos_goal == g_foc_pos_mech)
  {
    g_focuserOperation = false;
    if(g_foc_pos_goal == g_foc_pos_mech && _foc_notMotorPowerOff == false)
      digitalWrite(FOC_EN, FOC_STEPPER_DISABLE); 
  }
  if(g_foc_pos_goal > g_foc_pos_mech && g_rotatorOperation == false)
  {
    g_focuserOperation = true;
    digitalWrite(FOC_EN, FOC_STEPPER_ENABLE);
    digitalWrite(FOC_DIR, FOC_RIGHT_DIRECTION);
   
    delayMicroseconds(g_foc_speed);
    digitalWrite(FOC_STEP, HIGH); 
    delayMicroseconds(g_foc_speed);
    digitalWrite(FOC_STEP, LOW); 
    g_foc_pos_mech++;
    g_foc_powerOffTimeout = millis();
  }
  if(g_foc_pos_goal < g_foc_pos_mech && g_rotatorOperation == false)
  {
    g_focuserOperation = true;
    digitalWrite(FOC_EN, FOC_STEPPER_ENABLE);
    digitalWrite(FOC_DIR, FOC_LEFT_DIRECTION);
   
    delayMicroseconds(g_foc_speed);
    digitalWrite(FOC_STEP, HIGH); 
    delayMicroseconds(g_foc_speed);
    digitalWrite(FOC_STEP, LOW); 
    g_foc_pos_mech--;
    g_foc_powerOffTimeout = millis();
  }
  if(g_foc_pos_goal == g_foc_pos_mech && g_foc_overshoot_handling != FOC_OBVERSHOOT_DONE)
  {
    // Overshoot handling
    if(g_foc_overshoot_handling == FOC_OVERSHOOT_DIR_RIGHT)
    {
      g_foc_overshoot_handling = FOC_OBVERSHOOT_DONE;
      FocMoveLeft(FOC_OVERSHOOT_RIGHT);
    }
    if(g_foc_overshoot_handling == FOC_OVERSHOOT_DIR_LEFT)
    {
      g_foc_overshoot_handling = FOC_OBVERSHOOT_DONE;
      FocMoveRight(FOC_OVERSHOOT_LEFT);
    }
  }
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
