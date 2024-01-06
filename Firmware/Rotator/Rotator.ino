

#include <EEPROM.h>
#include <AccelStepper.h>
#include <TimerOne.h>
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
#define CMD_RATE "RA" // Derotation rate in steps/sec (float value)
#define CMD_GET_RATE "GR" // Get current derotation rate
#define STEPPER_MAX_SPEED 1000
#define ACCEL 600

volatile AccelStepper rotator(AccelStepper::DRIVER,STEP,DIR); 

double g_steps_per_degree;
long g_pos_mech = 0;
long g_pos_goal = 0;
long g_max_steps = 0;
bool g_is_init = false;
bool g_perform_init = false;

int g_foc_last_pot = 0;

int g_speed;
int g_init_speed;
float g_parkpos;

bool _notMotorPowerOff = false;
long g_powerOffTimeout = 0;
double g_derotation_steps = 0.0; // steps for derotation per second.
double g_derotation_goal = 0;
long g_derotationTimeout = 0; // once per second
#define DEROTATION_PERIOD 1000

// ---------------------------------------------------- Focuser
// Celestron C9.25
#define FOC_ROUNDS 48
#define FOC_GEAR_RATIO 4.1311
#define FOC_STEPPER_MAX_SPEED 4000
#define FOC_ACCEL 600
#define FOC_STEP 2
#define FOC_DIR  5
#define FOC_EN   12



// Enable only one stepper motor driver!
//#define NODRV
#define FOC_TMC2130_STANDALONE // TMC2130 SilentStick with SPI jumper closed (Standalone) and all three jumpers open (1/16 µStepping interpolate to 256 steps, realy silent!)
//#define DRV8825 // DRV8825: Must be set to 32 microsteps
//#define DRVST810 // ST820: Must be set to 256 microsteps
///////////////////////////////////////

#ifdef FOC_TMC2130_STANDALONE
  #define FOC_STEPPER_ENABLE LOW
  #define FOC_STEPPER_DISABLE HIGH

  #define FOC_RIGHT_DIRECTION HIGH
  #define FOC_LEFT_DIRECTION LOW
  #define FOC_STEP_DELAY_US 150
  #define FOC_STEPS_PER_REVOLUTION 3200
#endif


#ifdef FOC_DRV8825
  #define FOC_STEPPER_ENABLE LOW
  #define FOC_STEPPER_DISABLE HIGH

  #define FOC_RIGHT_DIRECTION HIGH
  #define FOC_LEFT_DIRECTION LOW
  #define FOC_STEP_DELAY_US 200
  #define FOC_STEPS_PER_REVOLUTION 6400 
#endif

#ifdef FOC_ST820
  #define FOC_STEPPER_ENABLE HIGH
  #define FOC_STEPPER_DISABLE LOW

  #define FOC_RIGHT_DIRECTION LOW
  #define FOC_LEFT_DIRECTION HIGH
  #define FOC_STEP_DELAY_US 100
  #define FOC_STEPS_PER_REVOLUTION 51200
  
#endif

#ifdef FOC_NODRV
  #define FOC_STEPPER_ENABLE LOW
  #define FOC_STEPPER_DISABLE HIGH

  #define FOC_RIGHT_DIRECTION LOW
  #define FOC_LEFT_DIRECTION HIGH

  #define FOC_STEPS_PER_REVOLUTION 0
#endif

#define FOC_CMD_STOP "FOCST"
#define FOC_CMD_IS_MOVING "FOCMV"
#define FOC_CMD_POS "FOCPO"
#define FOC_CMD_MOTOR_POWER_OFF "FOCMOFF"
#define FOC_CMD_MOTOR_POWER_ON "FOCMON"
#define FOC_CMD_SET_POS "FOCSPOS"           // in steps
#define FOC_CMD_GET_MAX_POS "FOCMGPOS"      // Get maximal position (used only in abs mode)
#define FOC_CMD_MOVE_ABS "FOCMOVABS"        // In abs mode move the focuser to this position. 


// Celestron 9.25 focuser use AccelStepper 
// and works only as absolute focuser
volatile AccelStepper focuser(AccelStepper::DRIVER,FOC_STEP,FOC_DIR); 
 
int g_foc_speed;
long g_foc_pos_mech = 0;
long g_foc_pos_goal = 0;
bool _foc_notMotorPowerOff = false;
long g_foc_abs_max_pos = (long)((double)FOC_ROUNDS * (double)FOC_STEPS_PER_REVOLUTION * (double)FOC_GEAR_RATIO);
bool g_foc_isReverse = false;

long g_foc_powerOffTimeout = 0;
#define FOC_POWERTIMEOUT 1000

// --------------------------------

String g_command = "";
bool g_commandComplete = false;
String g_info = "Not initialized yet.";
bool g_focuserOperation = false;
bool g_rotatorOperation = false;

// ************************************************** Roling EEPROM storage 
// To prevent EEPROM cell degeneration for one value EEPROM_ROLING_NUMBER*2
// cells are used and written once each EEPROM_ROLING_NUMBER write cycles.
#define EEPROM_ROLING_NUMBER 11

#define EEPROM_TYPE_BASE_ADR 0
#define EEPROM_REVERSE_ADR      24
#define EEPROM_STEPS_PERMM_0    48
#define EEPROM_STEPS_PERMM_1    72
#define EEPROM_STEPS_PERMM_2    96
#define EEPROM_STEPS_PERMM_3    120
#define EEPROM_POS_PERMM_0    144
#define EEPROM_POS_PERMM_1   168
#define EEPROM_POS_PERMM_2    192
#define EEPROM_POS_PERMM_3   216
#define EEPROM_POS_MAX_POS_0    240
#define EEPROM_POS_MAX_POS_1   264
#define EEPROM_POS_MAX_POS_2    288
#define EEPROM_POS_MAX_POS_3   312


void Write(int baseAddr, long typ)
{
  long maxNumber = 0;
  long maxAdr = 0;
  long minNumber = 512;
  long minAdr = 0;

  for(long i = baseAddr; i < baseAddr + (EEPROM_ROLING_NUMBER*2); i+=2)
  {
    long idx = EEPROM.read(i+1);
    if(idx < minNumber)
    {
      minNumber = idx;
      minAdr = i;
    }
    if(idx > maxNumber)
    {
      maxNumber = idx;
      maxAdr = i;
    }
  }
  // Handle when value was written 256*256 times:
  if(maxNumber +1 > 255)
  {
    // Here simply reset the idx's to 0:
    for(long i = baseAddr; i < baseAddr + (EEPROM_ROLING_NUMBER*2); i+=2)
    {
      EEPROM.update(i+1,0);
    }
    minAdr = baseAddr;
    maxNumber = 0;
  } 
  EEPROM.update(minAdr, typ);
  EEPROM.update(minAdr+1, maxNumber + 1);
}

long Read(int baseAddr)
{
  long maxNumber = -1;
  long maxAdr = 0;
  for(long i = baseAddr; i < baseAddr + (EEPROM_ROLING_NUMBER*2); i+=2)
  {
    long idx = EEPROM.read(i+1);
    if(idx > maxNumber)
    {
      maxNumber = idx;
      maxAdr = i;
    }
  }
  return EEPROM.read(maxAdr);
}

// ************************************************************************

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
  
  Serial.begin(9600);

  byte* bytesPos = (byte*)&g_foc_pos_mech;
  bytesPos[0] = Read(EEPROM_POS_PERMM_0);
  bytesPos[1] = Read(EEPROM_POS_PERMM_1);
  bytesPos[2] = Read(EEPROM_POS_PERMM_2);
  bytesPos[3] = Read(EEPROM_POS_PERMM_3);
  g_foc_pos_goal = g_foc_pos_mech;
  focuser.setCurrentPosition(g_foc_pos_goal);

  focuser.setMaxSpeed(FOC_STEPPER_MAX_SPEED);
  focuser.setAcceleration(FOC_ACCEL);

  rotator.setCurrentPosition(0);
  rotator.setMaxSpeed(STEPPER_MAX_SPEED);
  rotator.setAcceleration(ACCEL);
  rotator.setPinsInverted(true, false, false);
  Timer1.initialize(200);
  Timer1.attachInterrupt(stepperRun); 
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

float FocFloatExtract(String cmdid, String cmdstring)
{
  cmdstring.remove(0, cmdid.length());
  cmdstring.replace(':', ' ');
  cmdstring.trim();
  char tarray[32];
  cmdstring.toCharArray(tarray, sizeof(tarray));
  float steps = atof(tarray);
  return steps;
}

bool FocDispatcher()
{
  if(g_command.startsWith(FOC_CMD_STOP))
  {
    focuser.stop();
    g_foc_pos_mech = focuser.currentPosition();
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
    Serial.print(focuser.currentPosition());
    Serial.print("#");
  }
  else if(g_command.startsWith(FOC_CMD_SET_POS))
  {
    long val = FocExtract(FOC_CMD_SET_POS, g_command);
    g_foc_pos_mech = val;
    g_foc_pos_goal = val;

    byte* bytesPos = (byte*)&val;
    Write(EEPROM_POS_PERMM_0, bytesPos[0]);
    Write(EEPROM_POS_PERMM_1, bytesPos[1]);
    Write(EEPROM_POS_PERMM_2, bytesPos[2]);
    Write(EEPROM_POS_PERMM_3, bytesPos[3]);
    g_foc_pos_goal = g_foc_pos_mech;
    focuser.setCurrentPosition(g_foc_pos_goal);
    Serial.print("1#");
  }
  else if(g_command.startsWith(FOC_CMD_GET_MAX_POS))
  {
    Serial.print(g_foc_abs_max_pos);
    Serial.print("#");
  }
  else if(g_command.startsWith(FOC_CMD_MOVE_ABS))
  {
   if(g_focuserOperation) 
   {
      Serial.print("0#");
      return true;
   }
   long val = FocExtract(FOC_CMD_MOVE_ABS, g_command);
    if(val <= g_foc_abs_max_pos && val >= 0) 
    {
      g_foc_pos_goal = val;
      Serial.print("1#");
    }
    else
      Serial.print("0#");
  }
  return true;
}

// Region: Rotator

long FromDegreeToStep(double deg)
{
  return (long)(deg * g_steps_per_degree);
}

double FromStepToDegree(long step)
{
  return (double)(step / g_steps_per_degree);
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

void Move(double angle)
{
  double setAngle = FromStepToDegree(g_pos_mech);
  double diff = angle - setAngle;

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

double Extract(String cmdid, String cmdstring)
{
  cmdstring.remove(0, cmdid.length());
  cmdstring.replace(':', ' ');
  cmdstring.trim();
  return cmdstring.toDouble();
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
    rotator.setCurrentPosition(g_pos_mech);
    Serial.print("1#");
  }
  else if(g_command.startsWith(CMD_POSITION))
  {
    Serial.print(FromStepToDegree(rotator.currentPosition()));
    Serial.print('#');
  }
  else if(g_command.startsWith(CMD_STOP))
  {
    rotator.stop();
    g_pos_mech = rotator.currentPosition();
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
    double dblRevolution = STEPS_PER_REVOLUTION; // double calculation error if STEPS_PER_REVOLUTION is used directly!
    double stepSize = 360.0/dblRevolution;
    Serial.print(stepSize,6);
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
  else if(g_command.startsWith(CMD_RATE))
  {
    float val = Extract(CMD_RATE, g_command);
    g_derotation_steps = val;
    Serial.print("1#");
  }
  else if(g_command.startsWith(CMD_GET_RATE))
  {
    float val = Extract(CMD_RATE, g_command);
    Serial.print(g_derotation_steps,6);
    Serial.print("#");
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
  if(millis() - g_derotationTimeout >= DEROTATION_PERIOD)
  {
    g_derotationTimeout = millis();
    // Integration of the derotation frequency:
    g_derotation_goal += g_derotation_steps;
    // Transfer to the goal:
    long temp_goal = g_pos_goal;
    g_pos_goal += (long)(g_derotation_goal); 
    if(g_pos_goal != temp_goal)
    {
      g_derotation_goal = g_derotation_goal - ((double)(g_pos_goal - temp_goal));
    }
  }
  if(g_pos_goal != g_pos_mech && g_rotatorOperation == false && g_focuserOperation == false)
  {
    
    g_rotatorOperation = true;
    digitalWrite(EN, STEPPER_ENABLE);
    rotator.moveTo(g_pos_goal);
    g_powerOffTimeout = millis();
  }
  if(rotator.distanceToGo()!= 0)
    g_powerOffTimeout = millis();

  if(millis() - g_powerOffTimeout > 1000 && rotator.distanceToGo()== 0)
  {
    if(g_rotatorOperation)
    {
      g_pos_mech = g_pos_goal;
      g_rotatorOperation = false;
    }
    if(g_pos_goal == g_pos_mech && _notMotorPowerOff == false && g_derotation_steps == 0)
      digitalWrite(EN, STEPPER_DISABLE); 
  }
  if(g_perform_init)
  {
      g_pos_mech = 0;
      g_pos_goal = 0;
      g_perform_init = false;
      g_is_init = true;
      g_info = "Ready.";
  }
  // ***********************************************   Focuser:
  
  if(millis() - g_foc_powerOffTimeout > 1000 && focuser.distanceToGo()== 0)
  {
    if(g_focuserOperation)
    {
      g_foc_pos_mech = g_foc_pos_goal;
      byte* bytesPos = (byte*)&g_foc_pos_mech;
      Write(EEPROM_POS_PERMM_0, bytesPos[0]);
      Write(EEPROM_POS_PERMM_1, bytesPos[1]);
      Write(EEPROM_POS_PERMM_2, bytesPos[2]);
      Write(EEPROM_POS_PERMM_3, bytesPos[3]);
      g_focuserOperation = false;
    }
    if(g_foc_pos_goal == g_foc_pos_mech && _foc_notMotorPowerOff == false)
      digitalWrite(FOC_EN, FOC_STEPPER_DISABLE); 
  }
  if(g_foc_pos_goal != g_foc_pos_mech && g_rotatorOperation == false && g_focuserOperation == false)
  {
    g_focuserOperation = true;
    digitalWrite(FOC_EN, FOC_STEPPER_ENABLE);
    focuser.moveTo(g_foc_pos_goal);
    g_foc_powerOffTimeout = millis();
  }
  if(focuser.distanceToGo()!= 0)
    g_foc_powerOffTimeout = millis();
}

void stepperRun(void)
{
  focuser.run();
  rotator.run();
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
