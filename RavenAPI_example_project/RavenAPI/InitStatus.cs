/**
   @file InitStatus.cs
   @author Kate Colwell <kcolwell@irisdynamics.com>
   @brief  Keeps track of actuator initilisation status.
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
/// This class keeps track of the actuator initilisation status. Is updated when the status changes. Possible stateses are Initialized and Not Initialized.
/// This status is parsed from each message received from the mantis firmware.
/// </summary>

namespace RavenAPI
{
    class InitStatus
    {
        public static InitMode currentInitMode = InitMode.NotInitialized;
        public enum InitMode
        {
            NotInitialized = 0,
            Initialized = 1
        }

        static public void UpdateCurrentMode(int newMode)
        {
            switch (newMode)
            {
                case 0:
                    currentInitMode = InitMode.NotInitialized;
                    break;
                case 1:
                    currentInitMode = InitMode.Initialized;
                    break;
                default:
                    Console.WriteLine("Init mode not recognized");
                    break;
            }
        }
    }
}
