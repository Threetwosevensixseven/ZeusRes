using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZeusRes
{
    public partial class ZeusRes : Form
    {
        private Settings settings;

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
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                Visible = false;
                ShowInTaskbar = false;
                base.OnLoad(e);
                settings = Settings.GetInstance();
                Launch();
                MoveControls();
                Application.Exit();
            }
            catch { }
        }

        private bool Launch(bool Force = false)
        {
            if (Process.GetProcessesByName("zeus").Length > 0)
                return true;
            bool exit;
            var file = GetZeusFile(Force, out exit);
            if (exit)
                return false;
            DateTime timeout = DateTime.Now.AddSeconds(5);
            if (File.Exists(file))
            {
                Win32.ShellExecute(IntPtr.Zero, "open", file, "", Path.GetFullPath(file), Win32.ShowCommands.Show);
                while (true)
                {
                    if (timeout < DateTime.Now)
                        break;
                    if (Process.GetProcessesByName("zeus").Length == 0)
                        continue;
                    if (MoveControls())
                        break;
                }
            }
            if (Process.GetProcessesByName("zeus").Length == 0)
            {
                if (MessageBox.Show("Could not launch Zeus-ish. Do you want to locate the application and try again?",
                    "ZeusRes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    return Launch(true);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private string GetZeusFile(bool Force, out bool Exit)
        {
            Exit = false;
            if (!Force && !string.IsNullOrWhiteSpace(settings.ZeusLocation) && File.Exists(settings.ZeusLocation))
                return settings.ZeusLocation;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                settings.ZeusLocation = ofd.FileName;
                settings.Save();
            }
            else
            {
                Exit = true;
            }
            return settings.ZeusLocation;
        }

        private bool FindTForm1()
        {
            foreach (var p in Process.GetProcessesByName("zeus"))
            {
                if (Win32.EnumerateProcessWindowHandles(p.Id).Any(h => Win32.GetClassNameOfWindow(h) == "TForm1"))
                {
                    Thread.Sleep(3000);
                    return true;
                }
            }
            return false;                   
        }

        private bool MoveControls()
        {
            bool visible = false;
            const int YOFFSET = 628 - 500;
            const int YSHRINK = 3;
            var w = new Win32();
            foreach (var p in Process.GetProcessesByName("zeus"))
            {
                foreach (var tlw in Win32.EnumerateProcessWindowHandles(p.Id).Where(h => Win32.GetClassNameOfWindow(h) == "TForm1"))
                {
                    foreach (var panel in Win32.EnumerateChildWindows(tlw).Where(h => Win32.GetClassNameOfWindow(h) == "TPanel"))
                    {
                        foreach (var ctrl in Win32.EnumerateChildWindows(panel))
                        {
                            visible = visible | Win32.IsWindowVisible(ctrl, "Simple CP/M Hardware", "TRadioButton");
                            Win32.HideWindow(ctrl, "Simple CP/M Hardware", "TRadioButton");
                            Win32.HideWindow(ctrl, "NASCOM 2", "TRadioButton");
                            Win32.HideWindow(ctrl, "Chess", "TCheckBox");
                            Win32.HideWindow(ctrl, "Nameless Hardware", "TRadioButton");
                            Win32.HideWindow(ctrl, "Membership Card", "TRadioButton");
                            Win32.HideWindow(ctrl, "Battle", "TRadioButton");
                            Win32.MoveWindow(ctrl, "Reset Z80", "TButton", 22, 628 - YOFFSET - (YSHRINK * 0));          // Was 22, 628
                            Win32.MoveWindow(ctrl, "RUN", "TButton", 22, 659 - YOFFSET - (YSHRINK * 1));                // Was 22, 659
                            Win32.MoveWindow(ctrl, "WAIT", "TButton", 22, 659 - YOFFSET - (YSHRINK * 1));               // Was 22, 659
                            Win32.MoveWindow(ctrl, "Step", "TButton", 22,  690 - YOFFSET - (YSHRINK * 2));              // Was 22, 690
                            Win32.MoveWindow(ctrl, "Follow Execution", "TCheckBox", 16, 724 - YOFFSET - (YSHRINK * 3)); // Was 16, 724
                        }
                    }
                }
            }
            return visible;
        }
    }
}
