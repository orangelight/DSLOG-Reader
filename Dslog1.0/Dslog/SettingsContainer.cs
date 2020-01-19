using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLOG_Reader
{
    public class SettingsContainer
    {
        public int VoltageQuality { get; set; }
        public bool RobotModeDots { get; set; }
        public int PDPConfigSelected { get; set; }
        public bool DSEvents { get; set; }
        public List<PDPConfig> PDPConfigs;

        public SettingsContainer(string parse)
        {
            PDPConfigs = new List<PDPConfig>();
            string[] settingSplit = parse.Split(new string[] { "PDPConfigs\n" }, StringSplitOptions.None);
            string[] mainSettings = settingSplit[0].Split('\n');
            string[] pdpconfigs = settingSplit[1].Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for(int i = 0; i < mainSettings.Length; i++)
            {
                if (mainSettings[i].Split(':')[0] == "VoltageQuality") VoltageQuality = int.Parse(mainSettings[i].Split(':')[1]);
                if (mainSettings[i].Split(':')[0] == "PDPConfigSelected") PDPConfigSelected = int.Parse(mainSettings[i].Split(':')[1]);
                if (mainSettings[i].Split(':')[0] == "RobotModeDots") RobotModeDots = bool.Parse(mainSettings[i].Split(':')[1]);
                if (mainSettings[i].Split(':')[0] == "DSEvents") DSEvents = bool.Parse(mainSettings[i].Split(':')[1]);
            }
            for(int i = 0; i < pdpconfigs.Length; i++)
            {
                PDPConfig config = new PDPConfig(pdpconfigs[i].Split(':')[0], pdpconfigs[i].Split(':')[1].Split(','));
                
                PDPConfigs.Add(config);        
            }
        }

        public void Save()
        {
            StringBuilder builder = new StringBuilder("VoltageQuality:" + VoltageQuality + "\nRobotModeDots:" + RobotModeDots + "\nDSEvents:" + DSEvents + "\nPDPConfigSelected:" + PDPConfigSelected + "\nPDPConfigs\n");
            for(int i =0; i < PDPConfigs.Count; i++)
            {
                builder.Append(PDPConfigs[i].Name+":");
                for(int s = 0; s < 16; s++)
                {
                    builder.Append(PDPConfigs[i].PDPNames[s]);
                    if (s != 15) builder.Append(",");
                }
                builder.Append("\n");
            }
            File.WriteAllText(@"C:\Users\Public\Documents\dslogsettings.txt", builder.ToString());
        }

        public class PDPConfig
        {
            public string Name { get; set; }
            public string[] PDPNames { get; set; }
            public PDPConfig(string name, string[] pdpnames)
            {
                this.Name = name;
                this.PDPNames = pdpnames;
            }

            
        }
    }
}
