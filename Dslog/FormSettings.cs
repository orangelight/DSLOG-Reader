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

namespace DSLOG_Reader
{
    public partial class FormSettings : Form
    {
        private SettingsContainer settings;
        private Color[] colors;
        public FormSettings(SettingsContainer settings, Color[] c)
        {
            InitializeComponent();
            this.settings = settings;
            this.colors = c;
            this.trackBar1.Value = settings.VoltageQuality;
            this.checkBox1.Checked = settings.DSEvents;
            this.radioButton1.Checked = settings.RobotModeDots;
            this.radioButton2.Checked = !settings.RobotModeDots;
            for (int i = 0; i < settings.PDPConfigs.Count; i++)
            {
                comboBox1.Items.Add(settings.PDPConfigs[i].Name);
            }
            
            comboBox1.SelectedIndex = settings.PDPConfigSelected;
            updateGroup();

        }
        //cancel
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            settings.PDPConfigs.Add(new SettingsContainer.PDPConfig("Config " + (settings.PDPConfigs.Count), (string[])settings.PDPConfigs[0].PDPNames.Clone()));
            comboBox1.Items.Add(settings.PDPConfigs[settings.PDPConfigs.Count - 1].Name);
            comboBox1.SelectedIndex = settings.PDPConfigs.Count - 1;
            
           // updateGroup();
        }
        private int lastIndex = 0;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lastIndex != comboBox1.SelectedIndex)
            {
                updateGroup();
                lastIndex = comboBox1.SelectedIndex;
            }
            settings.PDPConfigSelected = comboBox1.SelectedIndex;
        }

        private void updateGroup()
        {
            panel1.Visible = false;
            panel1.Controls.Clear();
            for (int i = 0; i < 16; i++)
            {
                PDPName pd = new PDPName(settings.PDPConfigs[comboBox1.SelectedIndex].PDPNames[i], colors[i], i);
                pd.textBox1.TextChanged += InnterTextBox_TextChanged;
                pd.Location = new Point(7, (i * 27) + 7);
                panel1.Controls.Add(pd);
            }

            panel1.Enabled = comboBox1.SelectedIndex != 0;
            buttonDelete.Enabled = comboBox1.SelectedIndex != 0;
            textBox1.Enabled = comboBox1.SelectedIndex != 0;
            textBox1.Text = settings.PDPConfigs[comboBox1.SelectedIndex].Name;
            panel1.Visible = true;
        }

        private void InnterTextBox_TextChanged(object sender, EventArgs e)
        {
            settings.PDPConfigs[comboBox1.SelectedIndex].PDPNames[((TextBox)sender).TabIndex] = ((TextBox)sender).Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            settings.PDPConfigs[comboBox1.SelectedIndex].Name = textBox1.Text;
            comboBox1.Items[comboBox1.SelectedIndex] = textBox1.Text;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            settings.PDPConfigs.RemoveAt(comboBox1.SelectedIndex);

            comboBox1.SelectedIndex = index - 1;
            comboBox1.Items.RemoveAt(index);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            settings.Save();
            this.Close();
        }

        private void FormSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            settings.VoltageQuality = trackBar1.Value;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            settings.RobotModeDots = this.radioButton1.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            settings.DSEvents = this.checkBox1.Checked;
        }

       
    }
}
