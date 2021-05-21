# ASCOM.Stroblhofwarte.Rotator

The Stroblhof Rotator is a simple and sturdy rotator device
for astronomical observations. It could be build without access to
a lathe or milling machine. The rotator device is made out of aluminium
and steel. It is driven by a NEMA 14 stepper motor. The rotator could 
rotate (approximately) between between 0° and 330°. 
The electronics are made out of a arduino uno and a shield with ST820 driver.
Each stepper driver can be used, the DRV8825 driver supports 32 microsteps for
smooth operation.
The communication between this driver and the arduino is done with a simple
serial protocol, 9600 baud. A command ends with
a colon (:), the response ends with a hash (#).
              
 ```
Command         Response        Description
-----------------------------------------------------------------------------
ID:             ROTATOR#        Device identification
TRxxx:          1#              Move right xxx degrees (float with decimal point)
TLxxx:          1#              Move left xxx degrees
TAxxx:          1#              Move absolute to xxx degrees
GP:             xxxx#           Return the current position
ST:             1#              Stop the current movement
MV:             0# or 1#        #1: Rotator is moving, otherwise 0#
MOFF:           1#              The motor is disabled after movement
MON:            1#              The motor is always powerd
```
