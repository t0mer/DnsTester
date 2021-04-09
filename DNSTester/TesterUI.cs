using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DNSTester
{
    public partial class TesterUI : MetroForm
    {
        private bool UseCustomDnsServers = false;
        public string DNS1, DNS2;

        public TesterUI()
        {
            InitializeComponent();
        }

        private void RunTest_Click(object sender, EventArgs e)
        {
            int i;
            string[] CleanNames = new string[500];
            int CleanNamesCount = 0;

            if (this.UseCustomDnsServers)
            {
                DNS1 = CustomDns1.Text;
                DNS2 = CustomDns2.Text;
            }
            else
            {
                DNS1 = DnsList1.SelectedValue.ToString();
                DNS2 = DnsList2.SelectedValue.ToString();

            }

			ResultView.Items.Clear();
			ResultView.Update();
			Application.DoEvents();
			
			GetWebAddresses(ref CleanNames, ref CleanNamesCount);

			for (i = 0; i < CleanNamesCount; i++)
			{
				ListViewItem listURL = new ListViewItem(CleanNames[i]);
				listURL.SubItems.Add(" ");
				listURL.SubItems.Add(" ");
				listURL.SubItems.Add(" ");
				listURL.SubItems.Add(" ");

				ResultView.Items.Add(listURL);
			}
			ResultView.EnsureVisible(ResultView.Items.Count - 1);

			ResultView.Update();
			Application.DoEvents();

			CheckDNS(CleanNames, CleanNamesCount, DNS1, DNS2);

		}


		private void TesterUI_Load(object sender, EventArgs e)
        {
            FillDnsList();
        }

        private void FillDnsList()
        {
            DnsList1.ValueMember = "ip";
            DnsList1.DisplayMember = "name";

            DnsList2.ValueMember = "ip";
            DnsList2.DisplayMember = "name";

            DataSet servers1 = new DataSet();
            DataSet servers2 = new DataSet();
            servers1.ReadXml(Path.Combine(Application.StartupPath, "Servers.xml"));
            servers2.ReadXml(Path.Combine(Application.StartupPath, "Servers.xml"));
            DataTable ns1 = servers1.Tables[0];
            DataTable ns2 = servers2.Tables[0];
            DnsList1.DataSource = ns1;
            DnsList2.DataSource = ns2;

        }


        private void UseCustomServers_CheckedChanged(object sender, EventArgs e)
        {
            this.UseCustomDnsServers = UseCustomServers.Checked;
            SetUI();
        }

        private void SetUI()
        {
            if (this.UseCustomDnsServers)
            {
                this.DnsList1.Enabled = false;
                this.DnsList2.Enabled = false;

                this.CustomDns1.Enabled = true;
                this.CustomDns2.Enabled = true;

                this.CustomDns1.Visible = true;
                this.CustomDns2.Visible = true;
                this.lblCustom1.Visible = true;
                this.lblCustom2.Visible = true;

            }
            else
            {
                this.DnsList1.Enabled = true;
                this.DnsList2.Enabled = true;
                this.CustomDns1.Enabled = false; ;
                this.CustomDns2.Enabled = false;

                this.CustomDns1.Visible = false;
                this.CustomDns2.Visible = false;
                this.lblCustom1.Visible = false;
                this.lblCustom2.Visible = false;

            }
        }



		private void GetWebAddresses(ref string[] CleanNames, ref int CleanNamesCount)
		{
			int i, j;
			byte[] random = new Byte[256];
			string Letters = "";
			// Make a 3 letter random string

			//RNGCryptoServiceProvider is an implementation of a random number generator.
			RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
			rng.GetBytes(random); // The array is now filled with cryptographically strong random bytes.
			for (i = 1; i < 256; i++)
			{
				// Just take the first three letters
				if (random[i] > 0x40 && random[i] < 0x5b) Letters = Letters + Convert.ToChar(random[i]);
				if (Letters.Length == 3) break;
			}

			// Use Google to get random addresses for DNS testing

			// Initialize the WebRequest.
		
			StatusBoxPrint("Getting Google request");
			try
			{
				WebRequest GoogleRequest = WebRequest.Create("http://www.google.com/search?num=100&q=" + Letters); //Letters);
				WebResponse GoogleResponse = GoogleRequest.GetResponse();
				Application.DoEvents();
				// Get the response stream.
				Stream GoogleStream = GoogleResponse.GetResponseStream();
				StatusBoxPrint("Google response received");
				// Use a StreamReader to read the entire response.

				StreamReader GoogleReader = new StreamReader(GoogleStream, Encoding.ASCII);
				string GoogleString = GoogleReader.ReadToEnd();
				// Close the response stream and response to free resources.
				GoogleStream.Close();
				GoogleResponse.Close();
				Application.DoEvents();

				// find all .xxx.com addresses
				MatchCollection HostNames;
				string[] Names = new string[1000];
				// Create a new Regex object and define the regular expression.
				Regex Dotcom = new Regex("[.]([A-Za-z]*)[.]com");
				// Use the Matches method to find all matches in the input string.
				HostNames = Dotcom.Matches(GoogleString);

				Application.DoEvents();
				// Loop through the match collection to retrieve all 
				// matches and delete the leading "."
				for (i = 0; i < HostNames.Count; i++)
				{
					Names[i] = HostNames[i].Value.Remove(0, 1);
				}
				// eliminate google.com and repeated entries
				for (i = 0; i < HostNames.Count - 1; i++)
				{
					if (Names[i] == "google.com") Names[i] = "";
					for (j = i + 1; j < HostNames.Count; j++)
					{
						if (Names[i] == Names[j]) Names[j] = "";
					}
				}
				// count list and compact
				j = 0;
				for (i = 0; i < HostNames.Count; i++)
				{
					if (Names[i] != "")
					{
						CleanNames[j] = Names[i];
						j++;
					}
				}
				CleanNamesCount = j;
				StatusBoxPrint(CleanNamesCount + " Random URL's found");

			}
			catch
			{
				StatusBoxPrint("Host Google.com not found - are you connected ?");
			}

		}

		private void CheckDNS(string[] URLNames, int URLNamescount, string DNSAddress1, string DNSAddress2)
		{
			int i;
			const int IPPort = 53;
			const string TransactionID1 = "Q1"; // Use transaction ID of Q1 and Q2 to identify our packet and DNS
			const string TransactionID2 = "Q2";
			const string TypeString = "\u0001" + "\u0000" + "\u0000" + "\u0001" + "\u0000" + "\u0000" + "\u0000" + "\u0000" + "\u0000" + "\u0000";
			const string TrailerString = "\u0000" + "\u0000" + "\u0001" + "\u0000" + "\u0001";
			const int DNSReceiveTimeout = 5000;
			string URLNameStart, DomainName, QueryString, ReceiveString, IPResponse, sDeltaTime;
			int URLNameStartLength, DomainNameLength, index, TransactionDNS;
			byte[] Sendbytes = new byte[256];
			long StartTime, StopTime;
			string DeltaTime;
			Socket DNSsocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			DNSsocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, DNSReceiveTimeout);
			IPEndPoint dnsEP1 = new IPEndPoint(IPAddress.Parse(DNSAddress1), IPPort);
			IPEndPoint dnsEP2 = new IPEndPoint(IPAddress.Parse(DNSAddress2), IPPort);

			// Start the clock
			StartTime = DateTime.Now.Ticks;

			// i=0; // for testing - do one
			for (i = 0; i < URLNamescount; i++)
			{
				URLNameStart = URLNames[i].Substring(0, URLNames[i].IndexOf("."));
				DomainName = URLNames[i].Substring(URLNames[i].IndexOf(".") + 1, URLNames[i].Length - URLNames[i].IndexOf(".") - 1);
				// Build the query 
				QueryString = TransactionID1 + TypeString + (char)URLNameStart.Length + URLNameStart + (char)DomainName.Length + DomainName + TrailerString;
				Sendbytes = Encoding.ASCII.GetBytes(QueryString);
				DNSsocket.SendTo(Sendbytes, Sendbytes.Length, SocketFlags.None, dnsEP1);

				// send the same message to both DNS servers except for the transaction ID
				QueryString = TransactionID2 + TypeString + (char)URLNameStart.Length + URLNameStart + (char)DomainName.Length + DomainName + TrailerString;
				Sendbytes = Encoding.ASCII.GetBytes(QueryString);
				DNSsocket.SendTo(Sendbytes, Sendbytes.Length, SocketFlags.None, dnsEP2);
			}

			byte[] Receivebytes = new byte[512];

			try
			{
			// wait for a response up to timeout
			more: DNSsocket.Receive(Receivebytes);


				// make sure the message returned is ours
				if (Receivebytes[0] == Sendbytes[0] && (Receivebytes[1] == 0x31) || (Receivebytes[1] == 0x32))
				{
					if (Receivebytes[2] == 0x81 && Receivebytes[3] == 0x80)
					{
						// Get the time now
						StopTime = DateTime.Now.Ticks;
						DeltaTime = Convert.ToString((double)(StopTime - StartTime) / 10000000);

						// Decode the answers
						// Find the URL that was returned
						TransactionDNS = Receivebytes[1];
						ReceiveString = Encoding.ASCII.GetString(Receivebytes);
						index = 12;
						URLNameStartLength = Receivebytes[index];
						index++;
						URLNameStart = ReceiveString.Substring(index, URLNameStartLength);
						index = index + URLNameStartLength;
						DomainNameLength = Receivebytes[index];
						index++;
						DomainName = ReceiveString.Substring(index, DomainNameLength);
						index = index + DomainNameLength;
						index = index + 8;

						// Get the record type
						int ResponseType = Receivebytes[index];
						index = index + 9;

						// Get the IP address if applicable
						IPResponse = "";
						switch (ResponseType)
						{
							case 1:
								IPResponse = Convert.ToString(Receivebytes[index]) + "."
									+ Convert.ToString(Receivebytes[index + 1]) + "."
									+ Convert.ToString(Receivebytes[index + 2]) + "."
									+ Convert.ToString(Receivebytes[index + 3]); break;
							case 5: IPResponse = "CNAME"; break;
							case 6: IPResponse = "SOA"; break;
						}
						StatusBoxPrint("DNS Answer: " + URLNameStart + "." + DomainName + " = " + IPResponse);

						// Find the URL entry in the list
						for (i = 0; i < ResultView.Items.Count; i++)
						{
							if (ResultView.Items[i].Text == URLNameStart + "." + DomainName)
							{

								switch (TransactionDNS)
								{
									case 0x31:
										ResultView.Items[i].SubItems[1].Text = Convert.ToString(IPResponse);
										sDeltaTime = Convert.ToString(DeltaTime);
										if (sDeltaTime.Length > 5) ResultView.Items[i].SubItems[2].Text = sDeltaTime.Substring(0, 5);
										else ResultView.Items[i].SubItems[2].Text = sDeltaTime;
										ResultView.Items[i].ForeColor = System.Drawing.Color.Red;
										ResultView.EnsureVisible(i);
										ResultView.Update();
										Application.DoEvents();
										ResultView.Items[i].ForeColor = System.Drawing.Color.Black;
										break;

									case 0x32:
										ResultView.Items[i].SubItems[3].Text = Convert.ToString(IPResponse);
										sDeltaTime = Convert.ToString(DeltaTime);
										if (sDeltaTime.Length > 5) ResultView.Items[i].SubItems[4].Text = sDeltaTime.Substring(0, 5);
										else ResultView.Items[i].SubItems[4].Text = sDeltaTime;
										ResultView.Items[i].ForeColor = System.Drawing.Color.Red;
										ResultView.EnsureVisible(i);
										ResultView.Update();
										Application.DoEvents();
										ResultView.Items[i].ForeColor = System.Drawing.Color.Black;
										break;
								}
							}
						}
					}
					goto more;
				}
			}
			catch (SocketException e)
			{
				if (Convert.ToString(e).IndexOf("time") > 0)
				{
					StatusBoxPrint("Timeout - No response received for " + Convert.ToString(DNSReceiveTimeout / 1000) + " seconds");
				}
				else
				{
					StatusBoxPrint(Convert.ToString(e)); // for testing	
				}
			}
			finally
			{
				// close the socket
				DNSsocket.Close();
			}

		}

		private void StatusBoxPrint(string LogText)
		{
			StatusBox.Items.Add(DateTime.Now + "  " + LogText);
			StatusBox.TopIndex = StatusBox.Items.Count - 1;
			if (StatusBox.Items.Count > 5000)
			{
				StatusBox.Items.RemoveAt(0);
			}
			StatusBox.Update();
		}


	}
}
