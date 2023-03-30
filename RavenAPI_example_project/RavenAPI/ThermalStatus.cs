/**
   @file ThermalStatus.cs
   @author Kate Colwell <kcolwell@irisdynamics.com>
   @brief  Keeps track of and updates the motion platform's thermal mode
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
/// This class keeps track of the platforms thermal mode. Is updated when the thermal mode changes. Possible modes are Normal and Overheat Protection.
/// This mode is parsed from each message received from the mantis firmware.
/// </summary>

namespace RavenAPI
{
    class ThermalStatus
    {
        public static ThermalMode currentThermalMode = ThermalMode.OverheatProtection;

        public enum ThermalMode
        {
            Normal = 0, 
            OverheatProtection = 1
        }

        static public void UpdateCurrentMode(int newMode)
        {
            switch (newMode)
            {
                case 0:
                    currentThermalMode = ThermalMode.Normal;
                    break;
                case 1:
                    currentThermalMode = ThermalMode.OverheatProtection;
                    break;
                default:
                    Console.WriteLine("thermal mode not recognized");
                    break;
            }
        }
    }
}
