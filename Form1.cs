using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace nyoba_browser
{
    public partial class Form1 : Form
    {
        public ChromiumWebBrowser chromeBrowser;

        public Form1()
        {
            InitializeComponent();
            // Start the browser after initialize global component
            InitializeChromium();
            // Register an object in javascript named "cefCustomObject" with function of the CefCustomObject class :3
           //chromiumWebBrowser1.RegisterJsObject("cefCustomObject", new CefCustomObject(chromiumWebBrowser1, this));


            


        }

        public static int yaw = 0;
        public static int pitch = 0;
        private void Form1_Load(object sender, EventArgs e)
        {



        }

        public void InitializeChromium()
        {
            CefSettings settings = new CefSettings();

            // Note that if you get an error or a white screen, you may be doing something wrong !
            // Try to load a local file that you're sure that exists and give the complete path instead to test
            // for example, replace page with a direct path instead :
            // String page = @"C:\Users\SDkCarlos\Desktop\afolder\index.html";

            String page = string.Format(@"{0}\sudah benar load.html", Application.StartupPath);
            //String page = @"C:\Users\SDkCarlos\Desktop\artyom-HOMEPAGE\index.html";

            if (!File.Exists(page))
            {
                MessageBox.Show("Error The html file doesn't exists : " + page);
            }

            // Initialize cef with the provided settings
            Cef.Initialize(settings);
            // Create a browser component
            chromiumWebBrowser1.Load(page);

            // Add it to the form and fill it to the form window.
           // this.Controls.Add(chromiumWebBrowser1);
           // chromeBrowser.Dock = DockStyle.Fill;

            // Allow the use of local resources in the browser
            BrowserSettings browserSettings = new BrowserSettings();
            browserSettings.FileAccessFromFileUrls = CefState.Enabled;
            browserSettings.UniversalAccessFromFileUrls = CefState.Enabled;
            chromiumWebBrowser1.BrowserSettings = browserSettings;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
            
        }





        private async void trackBar1_Scroll(object sender, EventArgs e)
        {
            yaw = trackBar1.Value;
            label2.Text = yaw.ToString();
            pitch = trackBar2.Value;
            label3.Text = pitch.ToString();
            string scrip = "scene.registerBeforeRender(function(){dude.rotation= new BABYLON.Vector3(("+yaw.ToString()+"*Math.PI/180), (0*Math.PI/180), ("+pitch.ToString()+"*Math.PI/180));});";
            //MessageBox.Show(scrip);
            


            //Check if the browser can execute JavaScript and the ScriptTextBox is filled
            if (chromiumWebBrowser1.CanExecuteJavascriptInMainFrame)
            {
                //Evaluate javascript and remember the evaluation result
                JavascriptResponse response = await chromiumWebBrowser1.EvaluateScriptAsync(scrip);

                if (response.Result != null)
                {
                    //Display the evaluation result if it is not empty
                    MessageBox.Show(response.Result.ToString(), "JavaScript Result");
                }
            }



        }



        

       




    }
}
