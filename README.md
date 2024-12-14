Webpage movet to: https://astro.karglhoefchen-aldersbach.de/2021/06/27/der-dreh-am-himmel-ein-rotator/

and for the focuser part: https://astro.karglhoefchen-aldersbach.de/2023/04/09/ein-neuer-focuser-fuer-das-c9-25/

Remark: For C9.25 focuser: GT2 mit 155 Zähnen (310mm)


# ASCOM.Stroblhofwarte.Rotator

**** PLEASE use tag v1.0.0 for the hardware version described below *****

**** The master branch contains a development version for a hardware version without zero position switch ****

**** Development: Derotation for Alt/Az mounts ****
Derotation formula: Field Rotation Rate (arcsec/sec) = R =  15.04 (deg/hr) COS(Latitude)*COS(Azimuth Angle) / COS(Altitude Angle)

The Stroblhof Rotator is a simple and sturdy rotator device
for astronomical observations. It could be build without access to
a lathe or milling machine. 

![Rotator](https://github.com/stroblhofwarte/ASCOM.Stroblhofwarte.Rotator/blob/master/Rotator.jpg )

The rotator device is made out of aluminium
and steel. It is driven by a NEMA 14 stepper motor. The rotator could 
rotate (approximately) between between 0° and 359.9°. It will never rotate more than one revolution to prevent tangled cords. 
The electronics are made out of a arduino uno and a shield with TMC2130 driver.
Each Pololu compatible stepper driver can be used, the TMC2130 driver supports 16 microsteps and is the best choice
for silent operation. 
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
IF:             <INFO>#         Hardware state info
SZ:             xxx.xx#         Minimal step size in degree
IN:             1#              Rotator initialize its position via limit switch
IS:             1#              Set the motor speed factor f for initialization (base speed * f)
SP:             1#              Set the motor speed factor f for normal movements (base speed * f)
PPxxx.xx:       1#              Set the park position to xxx.xx°
PA:             1#              Move the rotator to the park position
RAxxx.xx:       1#              Set derotation rate for Alt-Az mounts. In steps/sec.
                                Can be positive or negative. Set this to 0.0 for derotation off.
GR:             xxx.xx#         Return the current derotation rate.
```

**** 13.02.2023: Focuser device added ****
A focuser interface added. Hardware: The focuser use the Pololu for the X-Axis. The ASCOM device is ASCOM.Stroblhofwarte.Focus.
````
Command         Response        Description
-------------------------------------------------------------------------------
FOCTRxxxx:      1#              Turn the focuser stepper xxxx steps right
FOCTLxxxx:      1#              Turn the focuser stepper xxxx steps left
FOCST:          1#              Stop a current running movement
FOCMV:          1# or 0#        1# when the focuser is moving, otherwise 0#
FOCPO:          xxxx#           Returns the current position of the focuser 
FOCMOFF:        1#              Switch motor off after movement
FOCMON:         1#              Keep motor current on after movement
FOCOVERRxxxxx:  1#              Set the amount of steps for right overshoot
                                This call will set the left overshoot to 0.
FOCOVERLxxxxx:  1#              Set the amount of steps for left overshoot
                                This call will set the right overshoot to 0.
````
The focuser device is a relative focuser.
