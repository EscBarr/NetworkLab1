using System.Net;
using System.Web;

namespace FileProtocol
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox_Uri.Text != null)
            {
                var uri = new System.Uri(textBox_Uri.Text);
                var converted = uri.AbsoluteUri;
                textBox2.Text = converted;
                WebRequest webRequest = WebRequest.Create(converted);
                webRequest.Method = "GET";
                
                var response = webRequest.GetResponse();
                textBox_Status.Text = response.ResponseUri.ToString();
                textBox_Status.Text += "\r\n";
                textBox_Status.Text += response.Headers.ToString();
               
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                textBox2.Text = responseFromServer;
                reader.Close();
                dataStream.Close();
                response.Close();
            }
        }
    }
}