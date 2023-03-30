# RavenAPI_SDK_dev

## WARNING! 

Be aware that the Raven-6DoF motion platform will move during operation. Before operating the platform ensure that no one is standing within the platform's range of motion. Be very careful at all time. 

## Description

This SDK will demonstrate how to interact with a Raven-6DoF motion platform from a simulation or game using the RavenAPI. RavenAPI uses the UDP protocol over ethernet to communicate with the SuperEagle Motor Controller. The API is language agnostic, and just requires support for serial ports. 

This repo contains the following: 
* A user guide for using the Raven API
* An example project which demonstrates how to format RavenAPI requests, and how to parse the responses which are received from the SuperEagle Motor Controller.  

User guides and reference manuals for the Orca Series motors can be found at https://www.irisdynamics.com/downloads/ . 

## Environment Setup 
The following should be done on the computer that is running the simulation and communicating with the SuperEagle Motion Controller. This will allow the computer to communicate with the SuperEagle over ethernet. 

1. Open Windows Settings, select Network & Internet, and then select Change Adapter Options. 

![Raven_Setup1](https://user-images.githubusercontent.com/78892451/226694110-aa07990f-e4f6-42fd-ba40-aa329333e351.PNG)

2. Select Properties for your ethernet connection. 

![Raven_setup2](https://user-images.githubusercontent.com/78892451/226694599-c31522ca-fe8b-4708-96b2-8e42e061930f.PNG)

3. Open Properties for IPv4.

![Raven_setup3](https://user-images.githubusercontent.com/78892451/226695424-856f18b2-8fc6-467e-92ab-2023ba016fe9.PNG)

4. Configure the settings as follows:

![Raven_setup4](https://user-images.githubusercontent.com/78892451/226695635-ea0e98c0-8e95-46f2-bab6-31cb905221d5.PNG)

5. Connect the SuperEagle ethernet port to the computer. Power the SuperEagle, which will begin the running the RavenAPI. 

To confirm that RavenAPI is running on your simulation computer follow these steps:
1. Open a command prompt and type "ssh root@10.0.0.2". 
2. Type 'top' to confirm what is running.
3. You should see Raven listed as follows:

![image](https://user-images.githubusercontent.com/78892451/226710007-24224505-6695-49b6-889e-86bf7cd6b6a7.png)


## Good Luck!
We look forward to your feedback and hope this gets you on the path to success with our technology.
