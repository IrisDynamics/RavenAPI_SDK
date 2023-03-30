/**
   @file UDPConnection.cs
   @author Kate Colwell <kcolwell@irisdynamics.com>
   @brief  Creates an instance of the UDP object and initiates connection
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
/// Instance of the UDP class 
/// </summary>

namespace RavenAPI
{
    public static class UDPConnection
    {
        //for connecting to Raven
        public static UDP udpCon = new UDP(9200, 9201);
        public static string targetIP = "10.0.0.2";

        public static void NewConnection()
        {
            try
            {
                udpCon.StartListening();
                Console.WriteLine("udp connection success");
            }
            catch (Exception e)
            {

                Console.WriteLine("problem with udp connection: " + e);
            }
        }
    }
}
