using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace final
{
    public partial class CsHome : Form
    {
        public CsHome()
        {
            InitializeComponent();
        }

        bool menuExpand = false;
        bool sidebarExpand = true;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (menuExpand == false)
            {
                menuContainer.Height += 10;
                if (menuContainer.Height >= 198)
                {
                    menuTransition.Stop();
                    menuExpand = true;
                }
            }
            else
            {
                menuContainer.Height -= 10;
                if (menuContainer.Height <= 61)
                {
                    menuTransition.Stop();
                    menuExpand = false;
                }
            }
        }

        private void transaction_Click(object sender, EventArgs e)
        {
            menuTransition.Start();
        }

        private void sideBarTransition_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebar.Width -= 10;
                if (sidebar.Width <= 64)
                {
                    sidebarExpand = false;
                    sideBarTransition.Stop();
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width >= 193)
                {
                    sidebarExpand = true;
                    sideBarTransition.Stop();
                }
            }
        }

        private void Menu_btn_Click(object sender, EventArgs e)
        {
            sideBarTransition.Start();
        }

       
    }
}
