# WindmillsGame

## For Developers

Add Changes to Data\_Main, Data\_Functions, Players\_Main and Players\_Functions 
In both classes:
 - Update() is called 60 times a second
 - Draw() is called as fast as GPU can handle
 - UpdateOthers() [And other Netcode], called as fast as internet can handle

Server files just relay clients, and auto adjust within client protocols (so no need to worry about them :P)
