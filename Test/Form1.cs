using Infoware.AutoUpdate.Services;
using Microsoft.Extensions.DependencyInjection;
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

namespace Test
{
    public partial class Form1 : Form
    {
        private readonly IAutoUpdater _updater;
        private object _updateAvailable;

        public Form1()
        {
            InitializeComponent();
            _updater = Program.Services.GetRequiredService<IAutoUpdater>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Click += Button1_ClickAsync;
        }

        private async void Button1_ClickAsync(object sender, EventArgs e)
        {
            if (_updateAvailable is null)
            {
                _updateAvailable = await _updater.CheckUpdatesAsync(Assembly.GetEntryAssembly());
                if (_updateAvailable != null)
                {
                    await _updater.DownloadUpdateAsync();
                }
                else
                {
                }
            }
            else
            {
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_updateAvailable != null)
            {
                _updater.PerformUpdate();
            }
        }

    }
}
