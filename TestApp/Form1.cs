using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestUpdate;

namespace TestApp
{
    public partial class Form1 : Form, ITestUpdatable
    {
        public Form1()
        {
            InitializeComponent();

            this.label1.Text = ApplicationAssembly.GetName().Version.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        public string ApplicationName
        {
            get { return "test"; }
        }

        public string ApplicationID
        {
            get { return "1"; }
        }

        public Assembly ApplicationAssembly
        {
            get { return Assembly.GetExecutingAssembly(); }
        }

        public Uri UpdateXmlLocation
        {
            get { return new Uri(""); }
        }

        public Form Context
        {
            get { return this; }
        }

        public Icon ApplicationIcon
        {
            get { return this.Icon; }
        }
    }
}
