

# Help

This is a help document for DSLOG Reader. The document is organized by tab (e.g. Log Files, Graph Info, etc..)
# Bugs/Crashes
If your program has crashed or you found a bug please report it in the issues or DM on ChiefDelphi.
# Log Caching
In order to increase performance in the Log Files Tab the information for logs. The cache is stored in the same directory as the logs.
# Log Files Tab
The Log Files Tab shows all log files in the selected path (Default is C:\Users\Public\Documents\FRC\Log Files). Simply clicking on a item in the list will load the log.
## Log List
![The Log list](https://i.imgur.com/P2KGfNx.png)

The headers for the log list are as following 
* Time: When the log started
* Seconds: Total seconds the log file is
* Match #: FMS event match number
* Time Ago: Time since log was written
* Event: FMS Event code (this value is guessed if not available, this guessing can be turned off in the settings) 
* Name: File name

Logs that belong to FMS matches are bold and have the following color scheme:
* Green: Practice Match
* Yellow: Qualification Match
* Red: Elimination Match
* Blue: Match not specified (Usually a practice match)

![FMS Matches](https://i.imgur.com/9Yf6pPy.png)

#### Live matches
Logs that are currently being written to (Live) will appear as Lime green

![Live Match](https://i.imgur.com/ucu7D0z.png)
## Open Log Button
![enter image description here](https://i.imgur.com/YmHidpd.png)
## Open Folder Button
![enter image description here](https://i.imgur.com/JH1ylL7.png)
## Refresh Button
![enter image description here](https://i.imgur.com/Lx2H5ki.png)
## Filter Useless Logs Button
![enter image description here](https://i.imgur.com/jh6U2wE.png)
A log is defined useless if it less than 20 seconds or for FMS matches if the robot is never connected.
## Filter Logs By Event
![enter image description here](https://i.imgur.com/Pb6P7gl.png)
## Bulk Export Button
![enter image description here](https://i.imgur.com/TdoiPJj.png)

![enter image description here](https://i.imgur.com/ZIEO6xf.png)
## Settings Button
![enter image description here](https://i.imgur.com/l8LNUtM.png)

![enter image description here](https://i.imgur.com/SfXdiLa.png)

#### Auto FMS Event Name Filling
Because some logs from FMS matches do provide their event ID the reader can guess the event ID by finding the closest log that has an event ID. If you have logs from different events that happened near the same time this guessing will not work.
# Graph Info Tab
This tab lets to enable and disable graph series, view probing information, and export. This also controls what information will be displayed on the Competition Tab
## Series/Profile Selector
![enter image description here](https://i.imgur.com/qXbRLWS.png)

To enable or disable what series gets shown on the graph click the checkbox. Groups can also be minimized and maximized.
### Changing Profile
![enter image description here](https://i.imgur.com/CaI7qp5.png)

To change what profile is selected (change the names of the PDP items) click the Profile selector. If the Default is the only profile available you need to make a new profile with the Profile Editor.  
## Profile Editor
![enter image description here](https://i.imgur.com/q5vSWiE.png)

To open the Profile Editor click the cog icon In the Graph Info Tab.
Once open, the **default profile cannot edited** so I new one must be made.
#### Creating a new Profile
Creating a new profile can either be done by creating one from scratch by clicking the Add Profile button or from another profile by clicking the Copy Profile Button

![enter image description here](https://i.imgur.com/L5qIZL1.png)

#### Changing Profile Name
![enter image description here](https://i.imgur.com/WnX6guA.png)

The Profile can renamed with the Profile Name box
#### Adding a New Group to a Profile
![enter image description here](https://i.imgur.com/DRjUNqX.png)

Click the Add Group button to add a new group to the profile.
#### Changing Group Name
To change the name of a group just click and wait a second until it becomes editable.
#### Moving PDP Slots into Group
To move a PDP slot a different group just drag and drop the item into a different group.
#### Editing PDP Slot Name and Color
The PDP Slot label will show what slot number a selected slot is when the name has been changed.
To edit a PDP slot name select it and wait a second for it's name to become editable.
To change the color of PDP slot select it and click the change color button.
#### Adding Total or Delta to Group
![enter image description here](https://i.imgur.com/KmM7nb1.png)

To add a group total or group delta to a group select a group that has **at least two PDP slots** in it and check the box for "Total in Group" or "Delta in Group".
The Total is the sum of all the PDP slots in the group, you must have.
The Delta is the standard deviation of all the PDP slots in the group.
#### Removing a Group
To remove a group select it and click the Remove Group button.
#### Profile Save Location
The Profiles are saved to where the executable is in a file called ".dslogprofiles"
## Probe Tab
Click the graph to get the information at the point(should see red line). It only shows the information that is enabled in the series selector.
## Energy Tab
The Energy tab shows how much energy each PDP slot has used throughout the log.
## Export Tab
The Export tab has tree modes, the current mode depends on what tab you are in (Graph, Events, Competition).
##### Graph Mode
In this mode the log can be exported to CSV, the clipboard, or Image. ** Note when you export to CSV or clipboard only the values in view will be exported not the whole log**
##### Events Mode
The events can only export as a CSV
##### Competition Mode
The competition mode can only export as an Image
# Graph Tab
This tab shows the main graph the current log is graphed.
## Graph
This is where the data from the dslog file is displayed
#### Probing
To probe the graph simply click on the spot you want to know the values, only the series enabled will show up in the probe tab
#### Zooming
To zoom click and drag the area you want to see
#### Hovering Over Events
![enter image description here](https://i.imgur.com/yf5UGoR.png)

If you hover over one of the event points on the graph you can see what the event is at the bottom of the Graph Info tab.
#### The Y Axis
![enter image description here](https://i.imgur.com/DuYIyK3.png)

**The numbers in parentheses are only for Total PDP and group totals not for regular PDP slots and group deltas**
## Log Streaming (Live)
When the reader is streaming a log file the top bar will turn green, Autoscroll can also be turned on.
## Match Time Button
![enter image description here](https://i.imgur.com/qhK9NiL.png)

The button sets the X axis labels to be time from match start

![enter image description here](https://i.imgur.com/NUJyC1K.png)

The match time button only works with logs connected to FMS and that have an auto.
## Reset Zoom Button
![enter image description here](https://i.imgur.com/nvwqmRw.png)

Resets zoom to view full log
## Zoom into Match Button
![enter image description here](https://i.imgur.com/qu8Owm9.png)

Finds and zooms into match
## Live Autoscroll Button
![enter image description here](https://i.imgur.com/5vakhnJ.png)

When reader is streaming this button allows Autoscroll to be enabled (when zoomed in the graph stays to the right side)

# Events Tab
This tab shows all of the events in the log that is loaded
## Event List
![enter image description here](https://i.imgur.com/U1gpXBf.png)

The color of the DS Time column depends on what mode the robot is in when the event occurred (gray for disabled, blue teleop, green auto) 

![enter image description here](https://i.imgur.com/yf5UGoR.png)
### Selecting Event item
![enter image description here](https://i.imgur.com/LCuGjic.png)
### Double Clicking Event Item
Double clicking an event item will switch to the Graph Tab and set the probe to be where the event is.

## Event Search
![enter image description here](https://i.imgur.com/hY66jpH.png)

## Filter Important Button
![enter image description here](https://i.imgur.com/LuFn5mZ.png)

## Filter Code Output Button
![enter image description here](https://i.imgur.com/AWC3wxp.png)

## Filter Duplicated Output
![enter image description here](https://i.imgur.com/0uNzyp0.png)

## Filter Joystick Output
![enter image description here](https://i.imgur.com/MuQWaWK.png)

# Competition Tab 
This tab only shows up if there is at least one log where the FMS is connected (bold in log file list).
## Selecting an Event
![enter image description here](https://i.imgur.com/O0HYNbT.png)

 To select an Event use the Event filter in the Log Files Tab
## Mode Selection
![enter image description here](https://i.imgur.com/XmSsvDH.png)
 

