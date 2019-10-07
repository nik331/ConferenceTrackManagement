using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ThoughtWorks.CTM
{
    public partial class UserInterface : Form
    {
        string[] _readlines = null;
        bool _inputFileSelected = false;
        List<ConferenceTrack> _conferenceTracks;
        int _numberOfTracks;

        public UserInterface()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                _numberOfTracks = int.Parse(textBox1.Text);
                _readlines = System.IO.File.ReadAllLines(openFileDialog1.FileName);
                _inputFileSelected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                _inputFileSelected = false;
            }

            if (_inputFileSelected)
            {
                var start = new Processor();
                _conferenceTracks = start.ProcessFile(_readlines, _numberOfTracks);
                richTextBox1.Text = start.CreateProgram(_conferenceTracks);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please enter number of track first", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                openFileDialog1.Reset();
                textBox1.Focus();
            }
            else
            {
                openFileDialog1.ShowDialog();
            }
        }

        
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(textBox1.Text.ToString(), @"[0-9]+$"))
            {
                MessageBox.Show("Only interger value is acceptable", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Clear();
                textBox1.Focus();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Title = "Save as a text file";
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {                
                File.WriteAllText(saveFileDialog1.FileName, richTextBox1.Text);
                MessageBox.Show("File saved successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
