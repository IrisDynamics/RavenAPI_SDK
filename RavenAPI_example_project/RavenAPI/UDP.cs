/**@file UDP.cs
 * @author Author Rebecca McWilliam
 * @version Revision 1.0
 * @date Date: 2019/07/29
 */

using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System;
using System.Net;
using System.Linq;
using System.Threading;

namespace RavenAPI
{
    public class UDP
    {
        int listenPort;                     //!< Port number to listen on 
        int talkPort;                       //!< Port number to talk on

        UdpClient udp;                      //!<udp client to receive messages
        IPEndPoint e;                       //!< endpoint of received messages
        byte[] msg;                    //!< string of received message
        DateTime sendTime;                  //!< system time at send
        DateTime receiveTime;               //!< system time at receive
        long elapsedTicks;                  //!< time between last send and receive

        public bool messageSent = false;   //!< flag that is checked to determine if the message is done sending
        public string last_sent = "";
        ///Constructor
        /// \param talkPort_ set the port number to send messages.
        /// \param listenPort_ sets the port number to receive messages.
        public UDP(int talkPort_, int listenPort_)
        {
            listenPort = listenPort_;
            talkPort = talkPort_;
        }

        /// wait for a message on the listen port and restart listening when received
        public void StartListening()
        {
            CRCFast.build_table();
            //set up listening udp client
            IPEndPoint e = new IPEndPoint(IPAddress.Any, listenPort);
            udp = new UdpClient(e);
            udp.EnableBroadcast = true;
            udp.BeginReceive(Receive, null);
        }

        /**When a new message is received this asynchronous funciton is called the CRC byte of the message
         * is checked and then the message is converted to a string the round trip latency 
         * between the last send and the receive is calculated then then starts listening again
         * \param ar async Result.
         */
        public void Receive(IAsyncResult ar)
        {
            try
            {
                IPEndPoint ipe = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = udp.EndReceive(ar, ref ipe);
                msg = data;
                foreach(byte b in msg)
                {
                    Console.Write(b + "/");
                }

                ParseMsg.ParseMessageID(msg);
                receiveTime = System.DateTime.Now;
                elapsedTicks = receiveTime.Ticks - sendTime.Ticks;

                udp.BeginReceive(Receive, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e + ": udp client closed");
            }

        }

        /**Send a message using the udp pipeline. A string message get converted to an array of bytes and a CRC bytes is appended
         * Send the message using the udp client to the specified server at the talking port number. Wait until the message is fully send
         * Also note the send time.
         * \param server ip address of receiving port.
         * \param message string of message to send.
         */

        public void Send(string server, byte[] sendBytes)
        {
            UdpClient u = new UdpClient();
            byte CRCbyte_send = CRCFast.generate(sendBytes, (byte)sendBytes.Length);
            byte[] sendByteswithCRC = new byte[sendBytes.Length + 1];
            for (int i = 0; i < sendBytes.Length; i++)
            {
                sendByteswithCRC[i] = sendBytes[i];
            }
            sendByteswithCRC[sendBytes.Length] = CRCbyte_send;
            string sendbytes_string = "";

            for (int i = 0; i < sendByteswithCRC.Length; i++)
            {
                sendbytes_string += sendByteswithCRC[i] + " ";
                last_sent = sendbytes_string;
                Console.Write(sendByteswithCRC[i] + "/");
            }
            Console.WriteLine();

            u.BeginSend(sendByteswithCRC, sendByteswithCRC.Length, server, talkPort, new AsyncCallback(SendCallback), u);
            while (!messageSent)
            {
                Thread.Sleep(10);
            }
            sendTime = System.DateTime.Now; //mark send time to be used for round trip latency calculation
        }

        ///AsyncCallback that sets messageSent to true when a message is finished sending
        public void SendCallback(IAsyncResult ar)
        {
            messageSent = true;
        }

        /// Get the received message
        /// \return two element array with the last received message and time in ticks it was received.
        //public byte[] GetReceived()
        //{
        //    if (msg == null) msg = new byte[1];
        //    return msg;// new byte[] { msg, receiveTime.Ticks.ToString() };  //return the last received message
        //}

        //public string GetLastSent()
        //{
        //    return last_sent;
        //}

        ///Closes the udp receiveing client
        public void Close()
        {
            udp.Close();  //close port 
        }
    }


    //     \class CRCFast
    //* \brief Used to calculate and check CRC byte.
    //*
    //* The following CRCFast class was taken from
    //* https://barrgroup.com/Embedded-Systems/How-To/CRC-Calculation-C-Code
    //* and can be found here: 
    //* Barr, Michael. "Slow and Steady Never Lost the Race," Embedded Systems Programming, January 2000, pp. 37-46.
    //* Big thank you for this! 
    //*/
    public class CRCFast
    {
        public static byte CRC_POLYNOMIAL = 0xD5;    //!< number used to generate crc table
        public static byte[] table = new byte[256];     //!< table of numbers for CRC calculation


        /**generate the CRC byte.
         * \param message Message.
         * \param nBytes number of bytes in the message.
         */
        public static byte generate(byte[] message, byte nBytes)
        {
            byte data;
            byte remainder = 0;

            //// Divide the message by the polynomial, a byte at a time.       
            for (byte byteIndex = 0; byteIndex < nBytes; ++byteIndex)
            {
                data = (byte)(message[byteIndex] ^ remainder);
                remainder = (byte)(table[data] ^ (remainder << 8));
            }
            //// The final remainder is the CRC.
            return (remainder);
        }

        /** Build the CRC table (one time at start)
         * \return CRC table.
         */
        public static byte[] build_table()
        {
            byte remainder;

            for (int dividend = 0; dividend < 256; ++dividend)
            {
                remainder = (byte)dividend;

                ////    // Perform modulo-2 division, a bit at a time.    
                for (int bit = 8; bit > 0; --bit)
                {
                    if ((remainder & 0x80) != 0)
                        remainder = (byte)((remainder << 1) ^ CRC_POLYNOMIAL);
                    else
                        remainder = (byte)(remainder << 1);
                }

                ////    //Store the result into the table.     
                table[dividend] = remainder;
            }
            return table;
        }

    }
}

