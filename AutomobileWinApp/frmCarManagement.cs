using AutomobileLibrary.BussinessObject;
using AutomobileLibrary.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutomobileWinApp
{
    public partial class frmCarManagement : Form
    {
        ICarRepository carRepository = new CarRepository();
        //Create a data source
        BindingSource source;

        //----------------------------------------
        public frmCarManagement()
        {
            InitializeComponent();
        }
        //----------------------------------------
        private void frmCarManagement_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            //Register this event to open the frmCarDetais form that performs updating
            dgvCarList.CellDoubleClick += DvgCarList_CellDoubleClick;
        }

        //--------------------------------
        private void DvgCarList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmCarDetails frmCarDetails = new frmCarDetails
            {
                Text = "Update car",
                InsertOrUpdate = true,
                CarInfo = GetCarObject(),
                CarRepository = carRepository,
            };
            if(frmCarDetails.ShowDialog() == DialogResult.OK)
            {
                LoadCarList();
                //Set focus car updated
                source.Position = source.Position - 1;
            }
        }
        //Clear txt on TextBoxes
        private void ClearText()
        {
            txtCarID.Text = string.Empty;
            txtCarName.Text = string.Empty;
            txtManufacturer.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtReleaseYear.Text = string.Empty;
        }
        //-------------------------------------------
        private Car GetCarObject()
        {
            Car car = null;
            try
            {
                car = new Car
                {
                    CarID = int.Parse(txtCarID.Text),
                    CarName = txtCarName.Text,
                    Manufacturer = txtManufacturer.Text,
                    Price = decimal.Parse(txtPrice.Text),
                    ReleaseYear = int.Parse(txtReleaseYear.Text)
                };
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get car");
            }
            return car;
        }//end GetCarObject
        //----------------------------------------
        public void LoadCarList()
        {
            var cars = carRepository.GetCars();
            try
            {
                //The BindingSource component is designed to simplify
                //the process of binding controls to and underlying data source
                source = new BindingSource();
                source.DataSource = cars;

                txtCarID.DataBindings.Clear();
                txtCarName.DataBindings.Clear();
                txtManufacturer.DataBindings.Clear();
                txtPrice.DataBindings.Clear();
                txtReleaseYear.DataBindings.Clear();

                dgvCarList.DataSource = null;
                dgvCarList.DataSource = source;
                if(cars.Count() == 0)
                {
                    ClearText();
                    btnDelete.Enabled = false;
                }
                else
                {
                    btnDelete.Enabled = true;
                }

            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load car list");
            }
        }//end LoadCarList

        //--------------------------------------------------------------
        private void btnLoad_Click(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
