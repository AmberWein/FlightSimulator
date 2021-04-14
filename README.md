# FlightSimulator
A flight simulator app, at Advanced Programming 2nd course, Bar-Ilan University.
Created by: Nicole Sharabany, Amber Weiner and Avia wolf.

## Overview and Features:
A flight simulator desktop application that interacts with a dedicated server. Our multithread application using C# and WPF with the MVVM software architectural pattern. Using FlightGear simulator, the program features a convenient user interface to view and analyze several attributes related to the flight.
 The GUI contains 5 main components: media player, dashboard, gear control, graphs and DLL controller. 
Each one is for a different purpose:
1. Media player- allows the user to control both the speed of the playback and the time reference. Meaning, play playback of the flight, pause it or stop the simulation, forward or backward the track and jump to a different point of time.
2. Dashboard- displays some aspects of the flight such as the altimeter, airspeed, orientation of the flight and measures of pitch, roll and yaw.
3. Graphs- allows the user to evaluate the different aspects of the flight:
 * a selected attribute from an attributes box
 * the most correlated attribute to the one we selected.
 * the linear regression between the two attributes from the first two graphs. 
 * anomalies from the flight in relation to the two attributes.
4. Gear control- includind a joystick to inspect the direction that the plane is heading and sliders to visualize the changes of the throttle and rudder. 
5. DLL controller- give an option to upload a dll anomaly detector to present anomalies

## Organization of the Project:
Our project is organized in five folders:
1. Models- contains all of the models.
2. ViewModels- contains all of the view models.
3. Views- contains all of the views including an Images folder for the media player.
4. IO- contains all of the classes involve with parsing data from files and writing to them as well.
5. plugins- contians dynamic link libraries.

## Required files:
1. reg_flight.csv - for a valid flight data.
Make sure to save it in FlightSimulator\bin\Debug folder
2. anomaly_flight.csv - for an anomaly flight data.
Make sure to save it in FlightSimulator\Folders folder.
4. play_back.xml - contains names of different features.
Make sure to save it in FlightSimulator\Folders folder.
6. Optional: add a dll file to FlightSimulator\plugins folder.

## Required Installations:
1. Recompile dll on your own native environment
2. Install the latest version of FlightGear on your computer
3. Import oxyplot in appropriate files to view graphs.
4. Import Syncfusion package to present the dashboard and the graphs
5. Execute the solution
6. In order to run a different dll from our built in dll's, make sure to implement the following methods:
צריך ליישר לגבי זה את הקו!

## Manual:
1. Run the application and a GUI should be opened.
2. Press the "start simulator" to start the simulator
3. Upload your CSV file and press the enter button.
Please notice to write a valid CSV file path, otherwise you will be requested to try again.
4. press "next" button to continue
5. take a few moments to get ready to fly and press "next"
6. The simulation is on, feel free to check it out and use the different features!
Notice: in any case you want to go back one page, yto the opening window, you can use the "back" buuton on the left hand side.

## UMLs and Class Diagrams:
Our desktop application consists of 3 main parts that communicate and run. The first component is the MyFlightModel that interacts with the server via TCP communication. The second component is the ViewModel that sends data requests to the MyFlightModel and recieves notifications when data changes from the MyFlightModel. Our last component is the View (MainWindow file) that sends commands to the ViewModel and gets notified about changed data from the ViewModel. Data is displayed in our MainWindow through the process of data binding. The following link is our project UML:

## Short Video About Our Project:
