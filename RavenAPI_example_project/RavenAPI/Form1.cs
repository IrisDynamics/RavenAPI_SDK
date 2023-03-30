/**
   @file Form1.cs
   @author Kate Colwell <kcolwell@irisdynamics.com>
   @brief  Creates an interactive GUI and manages events based on GUI interactions
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// Manages GUI interactions 
/// </summary>

namespace RavenAPI
{
    public partial class Form1 : Form
    {
        public List<TextBox> dofTxtBoxes = new List<TextBox>();
        public float[] dofFloats = new float[6];

        //sets up form
        public Form1()
        {
            InitializeComponent();
            ResponseList.formList.Add(this);
            ParseMsg.formList.Add(this);
            UDPConnection.NewConnection();
        }

        //sends a 682/mode change request 
        private void ModeChangeRequest(object sender, EventArgs e)
        {
            try
            {
                ModeStatus.selectedMode = (ModeStatus.Modes)listBox1.SelectedItem;
                byte[] sendBytes = CreateFrames.CreateNewFrame(ModeStatus.selectedMode);
                UDPConnection.udpCon.Send(UDPConnection.targetIP, sendBytes);
            }
            catch 
            {
                ResponseList.AddToResponseList("Please choose a mode before making a mode change request.\r\n");
            }
        }

        //sends a 2730/actuator forces request 
        private void ForcesRequest(object sender, EventArgs e)
        {
            byte[] sendBytes = CreateFrames.CreateNewFrame(CreateFrames.MessageID.forcesRequest);
            UDPConnection.udpCon.Send(UDPConnection.targetIP, sendBytes);
        }

        //sends a 10922/actuator temperature request 
        private void TempRequest(object sender, EventArgs e)
        {
            byte[] sendBytes = CreateFrames.CreateNewFrame(CreateFrames.MessageID.temperatureRequest);
            UDPConnection.udpCon.Send(UDPConnection.targetIP, sendBytes);
        }

        //sends a 65532/all dofs position request 
        private void DOFPosRequest(object sender, EventArgs e)
        {
            byte[] sendBytes = CreateFrames.CreateNewFrame(CreateFrames.MessageID.DOFpositionsRequest);
            UDPConnection.udpCon.Send(UDPConnection.targetIP, sendBytes);
        }
        
        //sends a 65534/all actuators target position request  
        private void TargetPosRequest(object sender, EventArgs e)
        {
            byte[] sendBytes = CreateFrames.CreateNewFrame(CreateFrames.MessageID.targetPosRequest);
            UDPConnection.udpCon.Send(UDPConnection.targetIP, sendBytes);
        }

        //sends a 65533/all actuators commanded force request 
        private void CommandedForcesRequest(object sender, EventArgs e)
        {
            byte[] sendBytes = CreateFrames.CreateNewFrame(CreateFrames.MessageID.commandedForceRequest);
            UDPConnection.udpCon.Send(UDPConnection.targetIP, sendBytes);
        }

        //send a 65532/all actuators position request 
        private void PositionRequest(object sender, EventArgs e)
        {
            byte[] sendBytes = CreateFrames.CreateNewFrame(CreateFrames.MessageID.actuatorsPosRequest);
            UDPConnection.udpCon.Send(UDPConnection.targetIP, sendBytes);
        }

        //sends a 65531/actuator initialisation request frame 
        private void ActuatorInit(object sender, EventArgs e)
        {
            byte[] sendBytes = CreateFrames.CreateNewFrame(CreateFrames.MessageID.actuatorInitialization);
            UDPConnection.udpCon.Send(UDPConnection.targetIP, sendBytes);
        }

        //sends a 170/washout targets request 
        private void WashoutRequest(object sender, EventArgs e)
        {
            foreach (Control con in this.Controls)
            {
                if (con is TextBox)
                {
                    dofTxtBoxes.Add((TextBox)con);
                }
            }

            foreach (TextBox t in dofTxtBoxes)
            {
                if (t.Name != "ResponseBox" && t.Text.Length < 1)
                {
                    t.Text = "0";
                }
            }

            byte[] sendBytes = CreateFrames.CreateNewFrame(CreateFrames.MessageID.washoutPositions, dofFloats[0], dofFloats[1], dofFloats[2], dofFloats[3], dofFloats[4], dofFloats[5]);
            UDPConnection.udpCon.Send(UDPConnection.targetIP, sendBytes);
        }

        //sends a 3780/firmware release request 
        private void FirmwareRequest(object sender, EventArgs e)
        {
            byte[] sendBytes = CreateFrames.CreateNewFrame(CreateFrames.MessageID.firmwareRequest);
            UDPConnection.udpCon.Send(UDPConnection.targetIP, sendBytes);
        }

        //used to update form elements
        delegate void SetTextCallback(string text);
        public void SetText(string text)
        {
            if (this.ResponseBox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.ResponseBox.AppendText(text);
            }
        }

        public void WriteTextSafe(string text)
        {
            if (ResponseBox.InvokeRequired)
            {
                // Call this same method but append THREAD2 to the text
                Action safeWrite = delegate { WriteTextSafe($"{text} (THREAD2)"); };
                ResponseBox.Invoke(safeWrite);
            }
            else
                ResponseBox.Text = text;
        }

        delegate void UpdateLabelsCallback(string label, string text);
        public void UpdateLabels(string label, string update)
        {
            Control[] labels = this.Controls.Find(label, true);
            if (labels[0].InvokeRequired)
            {
                UpdateLabelsCallback u = new UpdateLabelsCallback(UpdateLabels);
                this.Invoke(u, new object[] { label, update });

            }
            else
            {
                labels[0].Text = update;
            }
        }

        delegate void UpdateDOFValsCallback(string txtName, string text);
        public void UpdateDOFVals(string txtName, string value)
        {
            Control[] txtBoxes = this.Controls.Find(txtName, true);
            if (txtBoxes[0].InvokeRequired)
            {
                UpdateDOFValsCallback u = new UpdateDOFValsCallback(UpdateDOFVals);
                this.Invoke(u, new object[] { txtName, value });
            }
            else
            {
                txtBoxes[0].Text = value;
            }
        }

        delegate void UpdateFrameContentsCallback(string text);

        public void UpdateFrameContents(string text)
        {
            if (this.FrameContentBox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.FrameContentBox.AppendText(text);
            }
        }


        //updates values in the dofFloats array when a values is types in the appropriate textbox. 
        private void ParseValue(object sender, EventArgs e)
        {
            float floatValue;
            TextBox txtBox = sender as TextBox;
            string txtName = txtBox.Name;
            char index = txtName.Last();
            int i = index - '0';
            if (Single.TryParse(txtBox.Text, out floatValue))
            {
                dofFloats[i] = floatValue;
            }
            else
            {
                dofFloats[i] = 0f;
            }

        }

    }
}
