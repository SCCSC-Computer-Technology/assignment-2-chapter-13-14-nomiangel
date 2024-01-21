using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Noemi_Angeles_CPT_206_LAB_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'productDBDataSet.Product' table. You can move, or remove it, as needed.
            this.productTableAdapter.Fill(this.productDBDataSet.Product);

            //Create a data context object
            ProductsDataContext db = new ProductsDataContext();
            //Get all the Product objects from the Products collection
            var results = from product in db.Products
                          
                          select product;

            //Assign the results of the query to the datagrid control
            productDataGridView.DataSource = results;

        }

        private void productBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.productBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.productDBDataSet);

        }

        private void searchProdNumButton_Click(object sender, EventArgs e)
        {
            // Create a data context object
            ProductsDataContext db = new ProductsDataContext();

           
            //Search for Product Numbers using method syntax 
            var results = db.Products.Where(prod => prod.Product_Number == searchProdNumTextBox.Text);


            //Assign results of the query to the data grid control
            productDataGridView.DataSource = results;
        }

        private void searchDescButton_Click(object sender, EventArgs e)
        {
            //create a data context object
            ProductsDataContext db = new ProductsDataContext();

            //Search for products containing specific text
            var desc = db.Products.Where(description => description.Description.Contains(searchDescTextBox.Text));
            //assign results of the query to the datagrid control
            productDataGridView.DataSource = desc;
        }

        

        private void ascButton_Click(object sender, EventArgs e)
        {
            //Create a data context object
            ProductsDataContext db = new ProductsDataContext();
            //Get all the Product objects from the Products collection
            var results = from product in db.Products
                          orderby product.Units_On_Hand
                          select product;

            //Assign the results of the query to the datagrid control
            
            
                productDataGridView.DataSource = results;
            
        }

        private void lessThan100Button_Click(object sender, EventArgs e)
        {
            //create a data context object
            ProductsDataContext db = new ProductsDataContext();
            //Get all the products from the products collection and sort 
            var results = from products in db.Products
                          where products.Units_On_Hand < 100
                          select products;
          
            productDataGridView.DataSource = results;
        }

        private void moreThan100_Click(object sender, EventArgs e)
        {
            //create a data context object
            ProductsDataContext db = new ProductsDataContext();
            //Get all the products from the products collection and sort 
            var results = from products in db.Products
                          where products.Units_On_Hand > 100
                          select products;

            productDataGridView.DataSource = results;
        }

        private void priceASCButton_Click(object sender, EventArgs e)
        {
            //Create a data context object
            ProductsDataContext db = new ProductsDataContext();
            //Get all the Product objects from the Products collection
            
            var results = from product in db.Products
                          orderby product.Price
                          select product;

            //Assign the results of the query to the datagrid control

            productDataGridView.DataSource = results;
        }

        private void searchMinPricButton_Click(object sender, EventArgs e)
        {
            //create a data context object
            ProductsDataContext db = new ProductsDataContext();

            //Parse the numbers entered in the text boxes
            if (decimal.TryParse(minPriceTextBox.Text, out decimal minPrice) && decimal.TryParse(maxPriceTextBox.Text, out decimal maxPrice))
            {
                //search for the products within the price range entered
                var results = from product in db.Products
                              where product.Price >= minPrice && product.Price <= maxPrice
                              select product;
                //assign results of the quert to the datagrid control
                productDataGridView.DataSource = results;
            }
            else
            {
                //display error message for non numeros 
                MessageBox.Show("Please enter a valid number!!");
            }
        }
       //create a method to reassign the DataSource
        private void ResetDataGrid()
        {
            //reload the datagrid with original data
            this.productTableAdapter.Fill(this.productDBDataSet.Product);
            //set datatable as the datasource  for the data grid view
            productDataGridView.DataSource = this.productDBDataSet.Product;
        }
        private void clearButton_Click(object sender, EventArgs e)
        {


            ResetDataGrid();
            searchProdNumTextBox.Clear();
            searchDescTextBox.Clear();
            minPriceTextBox.Clear();
            maxPriceTextBox.Clear();



        }

        private void exitButton_Click(object sender, EventArgs e)
        {
          
            this.Close();
        }
    }
}
