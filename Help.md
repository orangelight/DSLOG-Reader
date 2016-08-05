# Help

This is a help document for DSLOG Reader

## Log Files Tab

The Log Files Tab shows all log files in the selected path (Defult is C:\Users\Public\Documents\FRC\Log Files) to change the path go to 
*File>Log Files Path* and select the folder. 

### Headers
* Time: When the log started
* Seconds: Total seconds the log file is
* Match #: FMS event match number
* Time Ago: Time since log was written
* Event: FMS Event code
* Name: File name

### Colors
If the log is a color other than white it means it has conncted to FMS at an event. The colors coraspond to diffent match types
* Green: Pratice Match
* Yellow: Qualification Match
* Red: Elimination Match
* Blue: Match not specified (Usally a practice match)


## Graph Tab

### Probe
Click the graph to get the information at the point. It only shows the information that is enabled.

### Export
Only the area viewable on the graph is exported. (If you are zoomed in, only the zoomed in part of the graph will be exported) Also only enabled values are exported. </p>
Add Total Current will add a column to the exported data the sums up all the enabled PDP currents.

### Groups
These checkboxs will enabl groups of values on the graph.

### Plots
These checkboxs will enable individual values on the graph.
