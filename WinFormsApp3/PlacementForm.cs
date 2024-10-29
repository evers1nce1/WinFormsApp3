using Accessibility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp3
{
    public partial class PlacementForm : Form
    {
        private PlacementManager _placementManager;
        private MenuForm _menuForm;
        public PlacementForm(string username, MenuForm menuForm)
        {
            InitializeComponent();
            _menuForm = menuForm;
            _placementManager = new PlacementManager(_menuForm, this, username, radioButton1, radioButton2, radioButton3, radioButton4, label_count1, labelcount_2, labelcount_3, labelcount_4, panel1, playButton);
            _placementManager.InitializeForm(this);
            this.FormClosed += OnFormClosed;
        }
        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            _menuForm.Show();
    }

    }
}
