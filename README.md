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
 * displays a time series graph of a selected attribute from an attributes box, updated continuously during the flight
 * a second time series graph displaying the data of most correlated attribute to the one we selected.
 * the linear regression between the two attributes from the first two graphs. 
 * scatter series showing the most rescent two dimensional values of the regression we evaluated.
4. Gear control- includind a joystick to inspect the direction that the plane is heading and sliders to visualize the changes of the throttle and rudder and a knob moving horizontally and verticly with changes of elevator and aileron values. 
5. DLL controller- give an option to upload a dll anomaly detector to present anomalies

## Organization of the Project:
Our project is organized in seven folders:
1. Models- contains all of the models.
2. ViewModels- contains all of the view models.
3. Views- contains all of the views including an Images folder for the media player.
4. IO- contains all of the classes involve with parsing data from files and writing to them as well.
5. plugins- contains dynamic link libraries.
6. Communication- contains Client.cs that handels client-server communication
7. Files- contains playback_small.xml and reg_flight.csv files

## Required files:
1. reg_flight.csv - for a valid flight data.
Make sure to save it in FlightSimulator\bin\Debug folder
2. anomaly_flight.csv - for an anomaly flight data.
Make sure to save it in FlightSimulator\Files folder.
4. play_back.xml - contains names of different features.
Make sure to save it in FlightSimulator\Files folder.
6. Optional: add a dll file to FlightSimulator\plugins folder.

## Required Installations:
1. Visual Studio 2019
2. Install FlightGear 2020.3.6 on your computer
3. Recompile dll on your own native environment
4. Import oxyplot in appropriate files to view graphs.
5. Import Syncfusion package to present the dashboard and the graphs
6. Execute the solution
7. Visual Studio 2019
8. In order to run a different dll from our built in dll's, make sure to implement the following methods:
Make sure that the uploaded algorithm has a void Creat(), void Detect() and void Free() methods.
You should also make an anomaly file report name "AnomalieReport.txt" in "FlightSimulator\bin\Debug" directory.
This file needs to open with "Anomalies report:" line and shows each pair of feature as follow:
first name+second name,number of line

## Manual:
1. Download the repository.
2. Make sure the settings file, "playback_small.xml", which is in FlightSimulator/Files is located in the proper directory FlightGear/Files.
3. Open "FlightSimulator.sln" in Visual studio and build the project.
4. Open the FlightSimulator.exe file which is located at FlightSimulator/bin/Debug folder to see the home screen.
5. Make sure that the .dll file name is exactly the same as the class's name of the uploaded algorithm.
6. Upload your CSV file and press the "Next" button.
7. The simulation is on, feel free to check it out and use the different features!
Notice: in any case you want to go back one page, you can press the "Back" button on your left hand side.

## UML diagram:
![image](https://user-images.githubusercontent.com/63461543/114775428-f6135d80-9d79-11eb-817c-cee1d28668d7.png)
