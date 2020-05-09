﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuspeSys.Test.WinFrom
{
    public partial class Form1 : Form
    {
        private ToolStripContainer toolStripContainer1;
        private ToolStrip toolStrip1;
        public Form1()
        {
            InitializeComponent();
            Init();
        }
        void Init() {
            toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            toolStrip1 = new System.Windows.Forms.ToolStrip();
            // Add items to the ToolStrip.
            toolStrip1.Items.Add("One");
            toolStrip1.Items.Add("Two");
            toolStrip1.Items.Add("Three");
            // Add the ToolStrip to the top panel of the ToolStripContainer.
            toolStripContainer1.TopToolStripPanel.Controls.Add(toolStrip1);
            // Add the ToolStripContainer to the form.
            Controls.Add(toolStripContainer1);
        }
    }
}
