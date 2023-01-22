


ildasm.exe /all /typelist /out=M2Mqtt.Net.il packages\M2Mqtt.4.3.0.0\lib\net45\M2Mqtt.Net.dll
ilasm.exe /dll /optimize /key=ASCOM.Stroblhofwarte.Rotator\ASCOMDriverTemplate.snk M2Mqtt.Net.il
move M2Mqtt.Net.dll Signed3rdParty
del M2Mqtt.Net.il
del M2Mqtt.Net.res
