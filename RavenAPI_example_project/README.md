# Raven API Example Project 

## Description 

This project will demonstrate how to send a message via the Raven API, and how to parse the responses recieved from the SuperEagle motor controller. 

The project is written in C#, but the RavenAPI is language agnostic. Any language that supports serial ports can be used to communicate with it. 

## Environment Setup 

We recomend opening the project in Visual Studio. Visual Studio can be downloaded here: https://visualstudio.microsoft.com/ . 

To build and view the project in Visual Studio open the .sln file, and select Start from the top menu. The solution explorer on the left hand of the screen can be used to browse project files. 

## Troubleshooting 

If you are unable to connect to the Raven API, ensure your firewall settings allow the connection. You can confirm this by opening Windows Defender Firewall -> Advanced Settings ->Inbound Rules and adding a rule which allows connection from the example project.
