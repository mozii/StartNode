using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Xml.Linq;
using System.Windows.Forms;

namespace StartNode
{
    class Program
    {
        static void Main(string[] args)
        {
            string environment, nodePath, appPath, appStart;
    
            try
            {
                XElement config = XElement.Load(@"config.xml");
                environment = config.Element("mode").Value;
                nodePath = config.Element("nodepath").Value;
                appPath = config.Element("apppath").Value;
                appStart = config.Element("appstart").Value;        
                appPath += @"\" + appStart;

                Launch(environment, nodePath, appPath );  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Configuration Error", MessageBoxButtons.OK);
            }               

        }

        static void Launch(string env, string nodepath, string apppath)
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = nodepath;
            startInfo.Arguments = apppath;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = false;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;

            if(env.Length > 0)
                startInfo.EnvironmentVariables.Add("NODE_ENV", env);

            try
            {
                using (Process p = Process.Start(startInfo))
                {
                    p.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Start Error", MessageBoxButtons.OK);
            }
       
        }


    }
}
