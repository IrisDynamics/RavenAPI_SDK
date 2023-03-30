/**
   @file ResponseList.cs
   @author Kate Colwell <kcolwell@irisdynamics.com>
   @brief  Handles printing responses from the SuperEagle Motor Controller to the GUI
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
using System.Windows.Forms;

/// <summary>
/// Updates the form textbox with info from the Mantis firmware. When a response is recevied from the mantis firmware a contents is printed to the form textbox.
/// </summary>
/// 

namespace RavenAPI
{
    public static class ResponseList
    {
        public static List<Form1> formList = new List<Form1>();

        public static void AddToResponseList(string resp)
        {
            formList[0].SetText(resp);

        }

        public static void AddToFrameList(string resp)
        {
            formList[0].UpdateFrameContents(resp);
        }

        public static void AddToFrameListRequests(byte[] byteArray)
        {
            string responseList = "";
            foreach(byte b in byteArray)
            {
                responseList = responseList += (b.ToString()) + "/";    
               
            }
            responseList += "\r\n";
            formList[0].UpdateFrameContents(responseList);
        }

        public static void AddToFrameListResponses(byte[] byteArray)
        {
            string responseList = "";
            foreach (byte b in byteArray)
            {
                responseList = responseList += (b.ToString()) + "/";

            }
            responseList += "\r\n";
            formList[0].UpdateFrameContents(responseList);
        }

    }
}
