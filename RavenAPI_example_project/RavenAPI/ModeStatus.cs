/**
   @file ModeStatus.cs
   @author Kate Colwell <kcolwell@irisdynamics.com>
   @brief  Keeps track of the platform's current mode.
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
/// This class keeps track of the platform's current mode. Is updated when the mode changes. Possible modes are Off, Level Brake, Loading and Queueing.
/// This mode is parsed from each message received from the mantis firmware.
/// More information about platform modes is available in the Mantis Reference Manual. 
/// </summary>

namespace RavenAPI
{
    public class ModeStatus
    {
        public static Modes currentMode = Modes.Off;
        public static Modes selectedMode;

        public enum Modes
        {
            Off = 0,
            LevelBrake = 1,
            Loading = 2,
            Queuing = 3
        }

        static public void UpdateCurrentMode(int newMode)
        {
            switch (newMode)
            {
                case 0:
                    currentMode = Modes.Off;
                    break;
                case 1:
                    currentMode = Modes.LevelBrake;
                    break;
                case 2:
                    currentMode = Modes.Loading;
                    break;
                case 3:
                    currentMode = Modes.Queuing;
                    break;
                default:
                    Console.WriteLine("current mode not recognized");
                    break;
            }
        }
    }
}
