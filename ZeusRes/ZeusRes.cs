using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZeusRes
{
    public partial class ZeusRes : Form
    {
        private NotifyIcon trayIcon;
        private ContextMenu trayMenu;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ZeusRes());
        }

        public ZeusRes()
        {
            InitializeComponent();
            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Exit", OnExit);
            trayIcon = new NotifyIcon();
            trayIcon.Text = "ZeusRes";
            trayIcon.Icon = new Icon(Icon, 32, 32);
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;
            base.OnLoad(e);
            Zeus();
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Zeus()
        {

        }
    }
}
