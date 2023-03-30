/**
   @file ParseMsg.cs
   @author Kate Colwell <kcolwell@irisdynamics.com>
   @brief  Parses responses from the SuperEagle Motor Controller
   @version 1.1.0
    
    @copyright Copyright 2022 Iris Dynamics Ltd 
    Licensed under the Apache License, Version 2.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License.

    For questions or feedback on this file, please email <support@irisdynamics.com>. 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// Parses responses from the Mantis firmware. 
/// When a valid request is sent to the Mantis firmware it will respond with another message containing relevent information. This class parses this message.
/// Platform mode, thermal mode and initialization status is parsed from the status word of each message. 
/// </summary>

namespace RavenAPI
{
    public static class ParseMsg
    {
        //refernece to main form - for updating GUI 
        public static List<Form1> formList = new List<Form1>();

        static int msgID;

        //status bit positions/counts 
        const int startModeCount = 0;
        const int platformModeLength = 4;
        const int startThermalCount = 4;
        const int thermalModeLength = 2;
        const int initStatusCount = 6;
        const int initModeLength = 1;

        //called when a new message is recieved 
        public static void ParseMessageID(byte[] msgBytes)
        {
            //check the mode status for each frame recieved
            byte[] statusWord = new byte[] { (msgBytes[msgBytes.Length - 2]), (msgBytes[msgBytes.Length - 3]), (msgBytes[msgBytes.Length - 4]), (msgBytes[msgBytes.Length - 5]) };
            BitArray statusBits = new BitArray(statusWord);

            //get mode statuses 
            int platformMode = ParseStatus(platformModeLength, startModeCount, statusBits);
            ModeStatus.UpdateCurrentMode(platformMode);
            int thermalMode = ParseStatus(thermalModeLength, startThermalCount, statusBits);
            ThermalStatus.UpdateCurrentMode(thermalMode);
            int initMode = ParseStatus(initModeLength, initStatusCount, statusBits);
            InitStatus.UpdateCurrentMode(initMode);
            UpdateLabels();

            //get msg id from response
            msgID = (((Int32)(msgBytes[4]) << 8) + (Int32)(msgBytes[5]));

            //parse msg appropriatly 
            switch (msgID)
            {
                case 170:
                    ParseWashouts(msgBytes);
                    break;
                case 682:
                    break;
                case 2730:
                    ParseForces(msgBytes);
                    break;
                case 10922:
                    ParseTemps(msgBytes);
                    break;
                case 65535:
                    ParseDOFPos(msgBytes);
                    break;
                case 65534:
                    ParseActuatorTargets(msgBytes);
                    break;
                case 65533:
                    ParseActuatorCommandedForces(msgBytes);
                    break;
                case 65532:
                    ParseActuatorPos(msgBytes);
                    break;
                case 65531:
                    break;
                case 3780:
                    ParseFirwareInfo(msgBytes);
                    break;
                default:
                    Console.WriteLine("msg id did not match any known requests");
                    break;
            }
        }

        //parses appropriate statuses from responses 
        public static int ParseStatus(int length, int startIdx, BitArray statusWord)
        {
            int modeStatus = 0;
            int j = startIdx;
            for (int i = 0; i < length; i++)
            {
                modeStatus += Convert.ToInt32(statusWord[j++]) << i;
            }
            return modeStatus;
        }

        public static void UpdateLabels()
        {
            formList[0].UpdateLabels("ModeLabel", " Platform Mode: " + ModeStatus.currentMode.ToString());
            formList[0].UpdateLabels("ThermalLabel", "Thermal Mode: " + ThermalStatus.currentThermalMode.ToString());
            formList[0].UpdateLabels("InitLabel", "Initialization Status: " + InitStatus.currentInitMode.ToString());
        }


        //for 2730 responses 
        public static void ParseForces(byte[] msgBytes)
        {
            //add to frame printout 
            ResponseList.AddToFrameListResponses(msgBytes);

            float[] values = ParseValues(msgBytes);
            float force0Data = values[0];
            float force1Data = values[1];
            float force2Data = values[2];
            float force3Data = values[3];
            float force4Data = values[4];
            float force5Data = values[5];

            ResponseList.AddToResponseList("Force Data " +
                            "\r\nForce 0 Data: " + force0Data +
                            "\r\nForce 1 Data: " + force1Data +
                            "\r\nForce 2 Data: " + force2Data +
                            "\r\nForce 3 Data: " + force3Data +
                            "\r\nForce 4 Data: " + force4Data +
                            "\r\nForce 5 Data: " + force5Data +
                            "\r\n\r\n"
                            );
        }

        //for 10922 responses 
        public static void ParseTemps(byte[] msgBytes)
        {
            //add to frame printout 
            ResponseList.AddToFrameListResponses(msgBytes);

            Char degree = (Char)176;
            float[] values = ParseValues(msgBytes);
            float temp0Data = values[0];
            float temp1Data = values[1];
            float temp2Data = values[2];
            float temp3Data = values[3];
            float temp4Data = values[4];
            float temp5Data = values[5];

            ResponseList.AddToResponseList("Temperature Data in Celsius " +
                            "\r\nTemp 0 Data: " + temp0Data + degree + 
                            "\r\nTemp 1 Data: " + temp1Data + degree +  
                            "\r\nTemp 2 Data: " + temp2Data + degree + 
                            "\r\nTemp 3 Data: " + temp3Data + degree + 
                            "\r\nTemp 4 Data: " + temp4Data + degree + 
                            "\r\nTemp 5 Data: " + temp5Data + degree +
                            "\r\n\r\n"
                            );
        }

        //for 65535 responses 
        public static void ParseDOFPos(byte[] msgBytes)
        {
            //add to frame printout 
            ResponseList.AddToFrameListResponses(msgBytes);

            float[] values = ParseValues(msgBytes);
            float surgePos = values[0] /1000;
            float swayPos = values[1] / 1000;
            float heavePos = values[2] / 1000;
            float rollPos = values[3] / 1000;
            float pitchPos = values[4] / 1000;
            float yawPos = values[5] / 1000;
            UInt32 timeStamp = GetTimeStamp(msgBytes[30], msgBytes[31], msgBytes[32], msgBytes[33]);

            //updates textbox values
            UpdateWashoutValues(surgePos, swayPos, heavePos, rollPos, pitchPos, yawPos);

            ResponseList.AddToResponseList("DoF Positions " +
                            "\r\nSurge Position: " + surgePos +
                            "\r\nSway Position: " + swayPos +
                            "\r\nHeave Position: " + heavePos +
                            "\r\nRoll Position: " + rollPos +
                            "\r\nPitch Position: " + pitchPos +
                            "\r\nYaw Position: " + yawPos +
                            GetTimePretty(timeStamp) + 
                            "\r\n\r\n"
                            );
        }

        //for 65534 messages 
        public static void ParseActuatorTargets(byte[] msgBytes)
        {
            //add to frame printout 
            ResponseList.AddToFrameListResponses(msgBytes);

            float[] values = ParseValues(msgBytes);
            float act0Target = values[0];
            float act1Target = values[1];
            float act2Target = values[2];
            float act3Target = values[3];
            float act4Target = values[4];
            float act5Target = values[5];

            UInt32 timeStamp = GetTimeStamp(msgBytes[30], msgBytes[31], msgBytes[32], msgBytes[33]);

            ResponseList.AddToResponseList("Actuator Target Positions " +
                            "\r\nActuator 0 Target Position: " + act0Target +
                            "\r\nActuator 1 Target Position: " + act1Target +
                            "\r\nActuator 2 Target Position: " + act2Target +
                            "\r\nActuator 3 Target Position: " + act3Target +
                            "\r\nActuator 4 Target Position: " + act4Target +
                            "\r\nActuator 5 Target Position: " + act5Target +
                            GetTimePretty(timeStamp) +
                            "\r\n\r\n"
                            );
        }
    
        //for 65533 messages 
        public static void ParseActuatorCommandedForces(byte[] msgBytes)
        {
            //add to frame printout 
            ResponseList.AddToFrameListResponses(msgBytes);

            float[] values = ParseValues(msgBytes);
            float act0Forces = values[0];
            float act1Forces = values[1];
            float act2Forces = values[2];
            float act3Forces = values[3];
            float act4Forces = values[4];
            float act5Forces = values[5];
            UInt32 timeStamp = GetTimeStamp(msgBytes[30], msgBytes[31], msgBytes[32], msgBytes[33]);

            ResponseList.AddToResponseList("Actuator Command Forces " +
                            "\r\nActuator 0 Commanded Forces: " + act0Forces +
                            "\r\nActuator 1 Commanded Forces: " + act1Forces +
                            "\r\nActuator 2 Commanded Forces: " + act2Forces +
                            "\r\nActuator 3 Commanded Forces: " + act3Forces +
                            "\r\nActuator 4 Commanded Forces: " + act4Forces +
                            "\r\nActuator 5 Commanded Forces: " + act5Forces +
                             GetTimePretty(timeStamp) +
                            "\r\n\r\n"
                            );
        }

        //for 65532 messages 
        public static void ParseActuatorPos(byte[] msgBytes)
        {
            //add to frame printout 
            ResponseList.AddToFrameListResponses(msgBytes);

            float[] values = ParseValues(msgBytes);
            float act0Pos = values[0];
            float act1Pos = values[1];
            float act2Pos = values[2];
            float act3Pos = values[3];
            float act4Pos = values[4];
            float act5Pos = values[5];
            UInt32 timeStamp = GetTimeStamp(msgBytes[30], msgBytes[31], msgBytes[32], msgBytes[33]);

            ResponseList.AddToResponseList("All Actuator Positions " +
                            "\r\nActuator 0 Position: " + act0Pos +
                            "\r\nActuator 1 Position: " + act1Pos +
                            "\r\nActuator 2 Position: " + act2Pos +
                            "\r\nActuator 3 Position: " + act3Pos +
                            "\r\nActuator 4 Position: " + act4Pos +
                            "\r\nActuator 5 Position: " + act5Pos +
                            GetTimePretty(timeStamp) +
                            "\r\n\r\n"
                            );

        }

        //for 170 messages 
        public static void ParseWashouts(byte[] msgBytes)
        {
            //add to frame printout 
            ResponseList.AddToFrameListResponses(msgBytes);

            float[] values = ParseValues(msgBytes);
            float surgePos = values[0] / 1000;
            float swayPos = values[1] / 1000;
            float heavePos = values[2] / 1000;
            float rollPos = values[3] / 1000;
            float pitchPos = values[4] / 1000;
            float yawPos = values[5] / 1000;

            UpdateWashoutValues(surgePos, swayPos, heavePos, rollPos, pitchPos, yawPos);

            ResponseList.AddToResponseList("Washout Positions " +
                            "\r\nSurge Position: " + surgePos +
                            "\r\nSway Position: " + swayPos +
                            "\r\nHeave Position: " + heavePos +
                            "\r\nRoll Orientation: " + rollPos +
                            "\r\nPitch Orientation: " + pitchPos +
                            "\r\nYaw Orientation: " + yawPos +
                            "\r\n\r\n"
                            );

        }

        //for 3780 messages
        public static void ParseFirwareInfo(byte[] msgBytes)
        {
            //add to frame printout 
            ResponseList.AddToFrameListResponses(msgBytes);

            float[] values = ParseSevenValues(msgBytes);
            float version = values[0];
            float state = values[1];
            float revNumber = values[2];
            float year = values[3];
            float month = values[4];
            float day = values[5];
            double hash = values[6];

            string hashString = Convert.ToInt64(hash).ToString("X");

            ResponseList.AddToResponseList("All Actuator Positions " +
                            "\r\nFirware Version: " + version +
                            "\r\nRelease State: " + state +
                            "\r\nRevision Number: " + revNumber +
                            "\r\nRelease Year: " + year +
                            "\r\nRelease Month: " + month +
                            "\r\nRelease Day: " + day +
                            "\r\nCommit Hash: " +  hashString +
                            "\r\n\r\n"
                            );
        }

        //parses values out of the recieved messages 
        public static float[] ParseValues(byte[] msgBytes)
        {
            float valueOne = (float)((int)(msgBytes[6] << 24) + (int)(msgBytes[7] << 16) + (int)(msgBytes[8] << 8) + (int)(msgBytes[9]));
            float valueTwo = (float)((int)(msgBytes[10] << 24) + (int)(msgBytes[11] << 16) + (int)(msgBytes[12] << 8) + (int)(msgBytes[13]));
            float valueThree = (float)((int)(msgBytes[14] << 24) + (int)(msgBytes[15] << 16) + (int)(msgBytes[16] << 8) + (int)(msgBytes[17]));
            float valueFour = (float)((int)(msgBytes[18] << 24) + (int)(msgBytes[19] << 16) + (int)(msgBytes[20] << 8) + (int)(msgBytes[21]));
            float valueFive = (float)((int)(msgBytes[22] << 24) + (int)(msgBytes[23] << 16) + (int)(msgBytes[24] << 8) + (int)(msgBytes[25]));
            float valueSix = (float)((int)(msgBytes[26] << 24) + (int)(msgBytes[27] << 16) + (int)(msgBytes[28] << 8) + (int)(msgBytes[29]));

            float[] returnValues = new float[] { valueOne, valueTwo, valueThree, valueFour, valueFive, valueSix };

            return returnValues;
        }

        public static float[] ParseSevenValues(byte[] msgBytes)
        {
            float valueOne = (float)((int)(msgBytes[6] << 24) + (int)(msgBytes[7] << 16) + (int)(msgBytes[8] << 8) + (int)(msgBytes[9]));
            float valueTwo = (float)((int)(msgBytes[10] << 24) + (int)(msgBytes[11] << 16) + (int)(msgBytes[12] << 8) + (int)(msgBytes[13]));
            float valueThree = (float)((int)(msgBytes[14] << 24) + (int)(msgBytes[15] << 16) + (int)(msgBytes[16] << 8) + (int)(msgBytes[17]));
            float valueFour = (float)((int)(msgBytes[18] << 24) + (int)(msgBytes[19] << 16) + (int)(msgBytes[20] << 8) + (int)(msgBytes[21]));
            float valueFive = (float)((int)(msgBytes[22] << 24) + (int)(msgBytes[23] << 16) + (int)(msgBytes[24] << 8) + (int)(msgBytes[25]));
            float valueSix = (float)((int)(msgBytes[26] << 24) + (int)(msgBytes[27] << 16) + (int)(msgBytes[28] << 8) + (int)(msgBytes[29]));
            float valueSeven = (float)((int)(msgBytes[30] << 24) + (int)(msgBytes[31] << 16) + (int)(msgBytes[32] << 8) + (int)(msgBytes[33]));

            float[] returnValues = new float[] { valueOne, valueTwo, valueThree, valueFour, valueFive, valueSix, valueSeven };

            return returnValues;
        }

        public static UInt32 GetTimeStamp(byte first, byte second, byte third, byte fourth)
        {
            UInt32 timeStamp = ((UInt32)(first << 24) + (UInt32)(second << 16) + (UInt32)(third << 8) + (UInt32)(fourth)) / 1000000;
            return timeStamp;
        }

        public static string GetTimePretty(float secs)
        {
            int hours = (int)secs / 3600;
            int minutes = (int)secs / 60;
            int seconds = (int)secs % 60;

            string timePretty = "\r\nTime since platform powered on: " + hours + ":" + minutes + ":" + seconds;

            return timePretty;
        }

        //updates the washout values textboxes when washout values are receieved 
        public static void UpdateWashoutValues(float surge, float sway, float heave, float roll, float pitch, float yaw)
        {
            float[] dofs = new float[6] { surge, sway, heave, roll, pitch, yaw };

            for(int i = 0; i < dofs.Length; i++)
            {
                formList[0].UpdateDOFVals("textBox" + i, dofs[i].ToString("n2"));
            }
        }
    }
}
