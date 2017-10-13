using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace WindowsFormsServer
{  
    public partial class Form1 : Form
    {
        // Initalize all the necessary variables that will be used and accessible 
        // throughouhg the entire project

        private static List<string> ListOfIP = new List<string>();
        private static readonly Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static readonly List<Socket> clientSockets = new List<Socket>();
        private const int BUFFER_SIZE = 2048;
        private const int PORT = 6666;
        private static readonly byte[] buffer = new byte[BUFFER_SIZE];

        /// <summary>
        /// Refresh timer
        /// </summary>
        private Timer refreshTimer;
        

        /// <summary>
        /// Constructor for the Server
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            
            // Initalize the socket server
            SetupServer();

            // Setup timer
            refreshTimer = new Timer();
            refreshTimer.Interval = 100;
            refreshTimer.Tick += RefreshTimer_Tick;
            refreshTimer.Start();
        }

        /// <summary>
        /// Timer called function responsible for updating the primary window 
        /// with list of connected IP's
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            Text_Box_1.Clear();
            Text_Box_1.Text = (string.Join("\n", ListOfIP));
        }

        /// <summary>
        /// This function specifically handles the calaulations for the 
        /// incoming distance package
        /// The Output is a double converted to string
        /// </summary>
        /// <param name="DistancePkg"></param>
        /// <returns></returns>
        private static string DistCalc(String DistancePkg)
        {
            double Response = 0;

            // Distribute the packatized info across a string array 
            string[] Value = DistancePkg.Split(':');
            try
            {
                // Perform the necessary 3D Distance calculation required for the client
                 Response =
                      Math.Sqrt(
                      Math.Pow((Convert.ToDouble(Value[0]) - Convert.ToDouble(Value[3])), 2)
                    + Math.Pow((Convert.ToDouble(Value[1]) - Convert.ToDouble(Value[4])), 2)
                    + Math.Pow((Convert.ToDouble(Value[2]) - Convert.ToDouble(Value[5])), 2));
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error with package: " + ex.Message);
            }
            return Convert.ToString(Response);
        }

        /// <summary>
        /// Called from the main class this function begins listening on specified port waiting for clients
        /// </summary>
        private static void SetupServer()
        {
            try
            {
                serverSocket.Bind(new IPEndPoint(IPAddress.Any, PORT));
                serverSocket.Listen(0);
                serverSocket.BeginAccept(AcceptCallback, null);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error opening socket server: " + ex.Message);

                // If this error occures then there is no recovery
                Application.Exit();
            }
          
        }

        /// <summary>
        /// This function closes all the sockets within the clientSockets list
        /// </summary>
        private static void CloseAllSockets()
        {
            foreach (Socket socket in clientSockets)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }

            serverSocket.Close();
        }

        /// <summary>
        /// Gets called to handle intial connections for clients
        /// </summary>
        /// <param name="AR"></param>
        private static void AcceptCallback(IAsyncResult AR)
        {
            Socket socket;

            try
            {
                socket = serverSocket.EndAccept(AR);
            }
            catch (ObjectDisposedException) 
            {
                return;
            }
            
            // New connection through socket needs to be added to clientSocket list
            clientSockets.Add(socket);

            // Add new IP to the IP List 
            AppendIPList((((IPEndPoint)socket.RemoteEndPoint).Address.ToString()), true);

            // Call to the RecieveCallback function to process incoming data
            socket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, socket);

            // Continue to connect with pending clients
            serverSocket.BeginAccept(AcceptCallback, null);

        }

        /// <summary>
        /// Handles the clients claculation requests and updates IP List
        /// </summary>
        /// <param name="AR"></param>
        private static void ReceiveCallback(IAsyncResult AR)
        {
            Socket current = (Socket)AR.AsyncState;
            int received;

            try
            {
                received = current.EndReceive(AR);
            }
            catch (SocketException)
            {

                // Remove the correspondong address from the IP List
                AppendIPList((((IPEndPoint)current.RemoteEndPoint).Address.ToString()), false);

                // Since an exception was triggered close the socket
                current.Close();
                clientSockets.Remove(current);
                return;
            }
            
            // Create byte array for incoming message 
            byte[] recBuf = new byte[received];
            // Remove any null bytes by mean of copying
            Array.Copy(buffer, recBuf, received);
            // Convert the message to string
            string text = Encoding.ASCII.GetString(recBuf);


            // Client wants to exit gracefully
            if (text == "<DIS>")
            {
                // Update the IPList to remove this address from the list
                AppendIPList((((IPEndPoint)current.RemoteEndPoint).Address.ToString()), false);
                
                // Begin shuttingdown sockets and closing them
                current.Shutdown(SocketShutdown.Both);
                current.Close();
                clientSockets.Remove(current);
                
                return;
            }

            // If the client is not trying to Disconnect then process the message a distance package
            byte[] data = Encoding.ASCII.GetBytes((DistCalc(text)));
            // Send calculated data to client
            current.Send(data);
                
            // check to ensure that the client recieving the calculation is on the IPList
            AppendIPList((((IPEndPoint)current.RemoteEndPoint).Address.ToString()), true);
            current.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, current);
        }

        /// <summary>
        /// This function is used to maintain a List of the currently connected 
        /// client IP's
        /// </summary>
        /// <param name="REP"></param>
        /// <param name="Operation"></param>
        public static void AppendIPList(String REP, bool Operation)
         {
            // Bool 'operation' determines is the IP being passed through is to be added
            // or removed form list
            if (Operation == true)
            {
                if (Form1.ListOfIP.Contains(REP) == false)
                {
                    ListOfIP.Add(REP);
                }
            }
            else
            {
                ListOfIP.Remove(REP);
            }
            
        }

        /// <summary>
        /// on_click handler for the "Exit" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            CloseAllSockets();
            Application.Exit();
        }

        private void Text_Box_1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
