/**
   @file CreateFrames.cs
   @author Kate Colwell <kcolwell@irisdynamics.com>
   @brief  Creates frames which can be sent to the SuperEagle Motor Controller via the RavenAPI
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// This class creates a request frame to send to Mantis. The CreateNewFrame() method has been overloadded to handle all different types of frames.
/// The MessageID enum stores the different message ID options - a message id is sent with each frame to identify the kind of information we are requesting.
/// More information about frame contents and message IDs is availabel in the RavenAPI wiki @ https://wiki.irisdynamics.com/index.php?title=RavenAPI_Overview
/// </summary>

namespace RavenAPI
{
    public class CreateFrames
    {
        ///ID for type of command being sent/received, either 6DOF orientation or individual actuator positions.
        public enum MessageID
        {
            sixDOF = 5,
            sixDOFPlusAngles = 21,
            sixDOFAnglesGravity = 85,
            washoutPositions = 170,
            modeRequest = 682,
            forcesRequest = 2730,
            temperatureRequest = 10922,
            DOFpositionsRequest = 65535, 
            targetPosRequest = 65534, 
            commandedForceRequest = 65533,
            actuatorsPosRequest = 65532,
            actuatorInitialization = 65531,
            firmwareRequest = 3780
        }

        ///Byte used to identify the start of a new message
        public static UInt32 startbyte = 0xFFFEFFFE;

        //used for 2730, 10922, 65535, 65534, 65533, 65532, 65531 and 3780 messages
        static public byte[] CreateNewFrame(CreateFrames.MessageID requestType)
        {
            Int16 msgID = (Int16)requestType;

            byte[] startBytes = { (byte)(startbyte >> 24), (byte)(startbyte >> 16), (byte)(startbyte >> 8), (byte)(startbyte) };
            byte[] messageIDBytes = { (byte)(msgID >> 8), (byte)(msgID) };

            byte[] sendBytes = {  startBytes[0], startBytes[1], startBytes[2], startBytes[3],
                                messageIDBytes[0], messageIDBytes[1],
                                };

            return sendBytes;
        }

        //used for 682 message
        public static byte[] CreateNewFrame(ModeStatus.Modes modeRequest)
        {
            Int16 msgID = (Int16)MessageID.modeRequest;
            Int32 modeID = (Int32)modeRequest;

            byte[] startBytes = { (byte)(startbyte >> 24), (byte)(startbyte >> 16), (byte)(startbyte >> 8), (byte)(startbyte) };
            byte[] messageIDBytes = { (byte)(msgID >> 8), (byte)(msgID) };
            byte[] modeBytes = { (byte)(modeID >> 24), (byte)(modeID >> 16), (byte)(modeID >> 8), (byte)(modeID) };

            byte[] sendBytes = {  startBytes[0], startBytes[1], startBytes[2], startBytes[3],
                                messageIDBytes[0], messageIDBytes[1],
                                modeBytes[0], modeBytes[1], modeBytes[2], modeBytes[3]};

            return sendBytes;
        }

        //used for 5 message 
        static public byte[] CreateNewFrame(float Surge, float Sway, float Heave, float Roll, float Pitch, float Yaw)
        {
            Int16 msgID = (Int16)MessageID.sixDOF;
            Int32 surge = (Int32)(Surge);
            Int32 sway = (Int32)(Sway);
            Int32 heave = (Int32)(Heave);
            Int32 roll = (Int32)(Roll);
            Int32 pitch = (Int32)(Pitch);
            Int32 yaw = (Int32)(Yaw);

            byte[] startBytes = { (byte)(startbyte >> 24), (byte)(startbyte >> 16), (byte)(startbyte >> 8), (byte)(startbyte) };
            byte[] messageIDBytes = { (byte)(msgID >> 8), (byte)(msgID) };
            byte[] surgebytes = { (byte)(surge >> 24), (byte)(surge >> 16), (byte)(surge >> 8), (byte)(surge) };
            byte[] swaybytes = { (byte)(sway >> 24), (byte)(sway >> 16), (byte)(sway >> 8), (byte)(sway) };
            byte[] heavebytes = { (byte)(heave >> 24), (byte)(heave >> 16), (byte)(heave >> 8), (byte)(heave) };
            byte[] rollbytes = { (byte)(roll >> 24), (byte)(roll >> 16), (byte)(roll >> 8), (byte)(roll) };
            byte[] pitchbytes = { (byte)(pitch >> 24), (byte)(pitch >> 16), (byte)(pitch >> 8), (byte)(pitch) };
            byte[] yawbytes = { (byte)(yaw >> 24), (byte)(yaw >> 16), (byte)(yaw >> 8), (byte)(yaw) };


            byte[] sendBytes = {  startBytes[0], startBytes[1], startBytes[2], startBytes[3],
                            messageIDBytes[0], messageIDBytes[1],
                            surgebytes[0], surgebytes[1], surgebytes[2], surgebytes[3],
                            swaybytes[0], swaybytes[1], swaybytes[2], swaybytes[3],
                            rollbytes[0], rollbytes[1], rollbytes[2], rollbytes[3],
                            heavebytes[0], heavebytes[1], heavebytes[2], heavebytes[3],
                            pitchbytes[0], pitchbytes[1], pitchbytes[2], pitchbytes[3],
                            yawbytes[0], yawbytes[1], yawbytes[2], yawbytes[3]};

            return sendBytes;
        }

        //for 170 message 
        public static byte[] CreateNewFrame(CreateFrames.MessageID message, float SurgePos, float SwayPos, float HeavePos, float RollPos, float PitchPos, float YawPos)
        {
            Int16 msgID = (Int16)message;
            Int32 surgePos = (Int32)(SurgePos * 1000);
            Int32 swayPos = (Int32)(SwayPos * 1000);
            Int32 heavePos = (Int32)(HeavePos * 1000);
            Int32 rollPos = (Int32)(RollPos * 1000);
            Int32 pitchPos = (Int32)(PitchPos * 1000);
            Int32 yawPos = (Int32)(YawPos * 1000);

            byte[] startBytes = { (byte)(startbyte >> 24), (byte)(startbyte >> 16), (byte)(startbyte >> 8), (byte)(startbyte) };
            byte[] messageIDBytes = { (byte)(msgID >> 8), (byte)(msgID) };
            byte[] surgebytes = { (byte)(surgePos >> 24), (byte)(surgePos >> 16), (byte)(surgePos >> 8), (byte)(surgePos) };
            byte[] swaybytes = { (byte)(swayPos >> 24), (byte)(swayPos >> 16), (byte)(swayPos >> 8), (byte)(swayPos) };
            byte[] heavebytes = { (byte)(heavePos >> 24), (byte)(heavePos >> 16), (byte)(heavePos >> 8), (byte)(heavePos) };
            byte[] rollbytes = { (byte)(rollPos >> 24), (byte)(rollPos >> 16), (byte)(rollPos >> 8), (byte)(rollPos) };
            byte[] pitchbytes = { (byte)(pitchPos >> 24), (byte)(pitchPos >> 16), (byte)(pitchPos >> 8), (byte)(pitchPos) };
            byte[] yawbytes = { (byte)(yawPos >> 24), (byte)(yawPos >> 16), (byte)(yawPos >> 8), (byte)(yawPos) };

            byte[] sendBytes = {startBytes[0], startBytes[1], startBytes[2], startBytes[3],
                                messageIDBytes[0], messageIDBytes[1],
                                surgebytes[0], surgebytes[1], surgebytes[2], surgebytes[3],
                                swaybytes[0], swaybytes[1], swaybytes[2], swaybytes[3],
                                heavebytes[0], heavebytes[1], heavebytes[2], heavebytes[3],
                                rollbytes[0], rollbytes[1], rollbytes[2], rollbytes[3],
                                pitchbytes[0], pitchbytes[1], pitchbytes[2], pitchbytes[3],
                                yawbytes[0], yawbytes[1], yawbytes[2], yawbytes[3]
                                };

            return sendBytes;
        }
    }
}
