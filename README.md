# WSMediaServer

The WSMediaServer, is a solution to manage a Plex multimedia server. This solution, will allow you to save energy by switching off the server, if the time of inactivity has exceeded a certain threshold (predefined in the configuration file).

The inactivity time has been defined as the period during which :

- no (remote) viewing is in progress
- no download is in progress
- no action has been performed by the connected Windows profiles

If one of the 3 conditions is not validated the inactivity counter is reset to 0.
