using System;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsClient
{
    public partial class Client : Form
    {
        /// <summary>
        /// Instance of the TcpClient class
        /// connection to the server
        /// </summary>
        TcpClient tcpclnt = new TcpClient();

        /// <summary>
        /// Stream for TCP connection
        /// </summary>
        Stream stm;

        /// <summary>
        /// Constructor for the Form
        /// </summary>
        public Client()
        {
            InitializeComponent();
        }


        private void Client_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// on_click handler that established conncetion to server 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Connect_Button_Click(object sender, EventArgs e)
        {
            string ServerIPAddress;

            // Checking to make sure IP address is valid before proceeding
            if (!ValidateIPv4(ServerIPBox.Text))
            {
                MessageBox.Show("Please enter a valid IPv4 address");
                return;
            }

            try
            {
                // Create a TcpClient Object
                tcpclnt = new TcpClient();

                // Set timeouts
               // tcpclnt.SendTimeout = 5000;
               // tcpclnt.ReceiveTimeout = 5000;

                // Pull Server Ip address form text box
                ServerIPAddress = ServerIPBox.Text;
                // Initialize as TCP client with server over port 6666
                tcpclnt.Connect(ServerIPAddress, 6666);

                stm = tcpclnt.GetStream();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not connect!: "+ ex.Message);

                return;
            }

            // Considering the connect button was jsut clicked, set 
            // The rest of the buttons/ text boxes to sensible enable state
            ServerIPBox.Enabled = false;
            CalcButton.Enabled = true;
            DisconnectButton.Enabled = true;
            Connect_Button.Enabled = false;
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// on_click handler for Disconnect button
        /// sends the Disconnect signal to the server 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            // The Disconnect button sends a message "<DIS> which
            // indicates to the server this client is disconnecting

            // Set stream to Tcp client stream
            try
            { 
                // set this array of bytes to equal the converted string message
                byte[] ba = System.Text.Encoding.UTF8.GetBytes("<DIS>"); // asen.GetBytes(DistancePkg);

                // Simple send the byte array containing the Disconnect signal to the server through the 
                // TCP stream
                stm.Write(ba, 0, ba.Length);   
            }
            catch (Exception ex)
            {
                // Failure to gracefully Discoonect from server connection
                MessageBox.Show("Failed to do something: " + ex.Message);
            }

            // Close the connection
            tcpclnt.GetStream().Close();
            tcpclnt.Close();

            // now that the Disconnect button has been pressed 
            // enable the Server Ip entry box and disable the other buttons
            // note the connection button will be automatically enabled, or diabled if an 
            // invalid Ip address is entered
            ServerIPBox.Enabled = true;
            Connect_Button.Enabled = true;
            DisconnectButton.Enabled = false;
            CalcButton.Enabled = false;
        }

        /// <summary>
        /// Quick validation of IP addresses
        /// </summary>
        /// <param name="ipString"></param>
        /// <returns>
        /// true for valid, and false for invalid
        /// </returns>
        public bool ValidateIPv4(string ipString)
        {
            if (String.IsNullOrWhiteSpace(ipString))
            {
                return false;
            }

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            byte tempForParsing;

            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }

        /// <summary>
        /// on_click handler for calc button
        /// packaging of the Distance info from the input boxes and sends the package
        /// to the Server over TCP stream
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalcButton_Click(object sender, EventArgs e)
        {

            // check to make sure each txt box is not empty and not a char  
            double dump;
            if (string.IsNullOrWhiteSpace(AX.Text) ||
                !double.TryParse(AX.Text,out dump))
                AX.Text = "0";

            if (string.IsNullOrWhiteSpace(AY.Text) ||
                !double.TryParse(AY.Text, out dump))
                AY.Text = "0";

            if (string.IsNullOrWhiteSpace(AZ.Text) ||
                !double.TryParse(AZ.Text, out dump))
                AZ.Text = "0";

            if (string.IsNullOrWhiteSpace(BX.Text) ||
                !double.TryParse(BX.Text, out dump))
                BX.Text = "0";

            if (string.IsNullOrWhiteSpace(BY.Text) ||
                !double.TryParse(BY.Text, out dump))
                BY.Text = "0";

            if (string.IsNullOrWhiteSpace(BZ.Text) ||
                !double.TryParse(BZ.Text, out dump))
                BZ.Text = "0";

            // disable clalc button while data is being sent
            CalcButton.Enabled = false;


            // organize the distance data into a packet seperated by ':' 
            // 
            String DistancePkg = (AX.Text + ':'
                + AY.Text + ':' + AZ.Text + ':'
                + BX.Text + ':' + BY.Text + ':'
                + BZ.Text );

            try
            {

                // Set byte array equal to packetized message 
                byte[] ba = System.Text.Encoding.UTF8.GetBytes(DistancePkg);

                // wriite message to stream
                stm.Write(ba, 0, ba.Length);


                // initalize new array to store incoming message in
                byte[] bb = new byte[100];
                int k = stm.Read(bb, 0, 100);

                DistanceTextBox.Text = System.Text.Encoding.UTF8.GetString(bb, 0, bb.Length);

                // re enable calc button now that return message has been recieved
                CalcButton.Enabled = true;

                // clear the stream buffer incase Server 
                stm.Flush();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Failed to communicate with the server: "+ex.Message);
            }

        }

        /// <summary>
        // every time the ServerIPBox is changed, update the enable state of the connect 
        // button based on the validity of the inputted "IP" address
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServerIPBox_TextChanged(object sender, EventArgs e)
        {
            // Depending on the validity of the IPaddress enable/disable the connect button
            Connect_Button.Enabled = ValidateIPv4(ServerIPBox.Text);
        }

        
    }
    }

