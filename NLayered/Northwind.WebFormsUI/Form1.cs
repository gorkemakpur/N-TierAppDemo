using Northwind.Business.Abstract;
using Northwind.Business.Concrete;
using Northwind.Business.DependencyResolver.Ninject;
using Northwind.DataAccess.Concrete.EntityFramework;
using Northwind.DataAccess.Concrete.NHibernate;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Northwind.WebFormsUI
{
    public partial class Ürünler : Form
    {
        IProductService _productService;
        ICategoryService _categoryService;
        public Ürünler()
        {
            InitializeComponent();
            _productService = InstanceFactory.GetInstance<IProductService>();
            _categoryService = InstanceFactory.GetInstance<ICategoryService>();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadAllProducts();
            LoadCategories();

        }

        private void LoadCategories()
        {
            cbxCategoryName.DataSource = _categoryService.GetAll();
            cbxCategoryName.DisplayMember = "CategoryName";
            cbxCategoryName.ValueMember = "CategoryId";


            cbxCategory.DataSource = _categoryService.GetAll();
            cbxCategory.DisplayMember = "CategoryName";
            cbxCategory.ValueMember = "CategoryId";

            cbxUpdateCategory.DataSource = _categoryService.GetAll();
            cbxUpdateCategory.DisplayMember = "CategoryName";
            cbxUpdateCategory.ValueMember = "CategoryId";
        }

        private void LoadAllProducts()
        {
            dgwProduct.DataSource = _productService.GetAll();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void cbxCategoryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgwProduct.DataSource = _productService.GetProductsByCategory(Convert.ToInt16(cbxCategoryName.SelectedValue));
            }
            catch
            {

            }

        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtProductName.Text))
            {
                dgwProduct.DataSource = _productService.GetProductsByName(txtProductName.Text);
            }
            else
            {
                LoadAllProducts();
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {

            try
            {
                _productService.Add(new Product
                {
                    CategoryID = Convert.ToInt32(cbxCategory.SelectedValue),
                    ProductName = tbxProductName.Text,
                    QuantityPerUnit = tbxQuantityPerUnit.Text,
                    UnitPrice = Convert.ToDecimal(tbxPrice.Text),
                    UnitsInStock = Convert.ToInt16(tbxStock.Text)

                });


                MessageBox.Show("Ürün Başarılı Bir Şekilde Eklendi");
                LoadAllProducts();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }



        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                _productService.Update(new Product
                {
                    ProductID = Convert.ToInt32(dgwProduct.CurrentRow.Cells[0].Value),
                    CategoryID = Convert.ToInt32(cbxUpdateCategory.SelectedValue),
                    ProductName = tbxUpdateProductName.Text,
                    QuantityPerUnit = tbxUpdateQuantityPerUnit.Text,
                    UnitPrice = Convert.ToDecimal(tbxUpdatePrice.Text),
                    UnitsInStock = Convert.ToInt16(tbxUpdateStock.Text)

                });



                MessageBox.Show("Ürün Başarılı Bir Şekilde Güncellendi");
                LoadAllProducts();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }


        }

        private void dgwProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgwProduct.CurrentRow;
            tbxUpdateProductName.Text = row.Cells[1].Value.ToString();
            cbxUpdateCategory.SelectedValue = row.Cells[2].Value;
            tbxUpdatePrice.Text = row.Cells[3].Value.ToString();
            tbxUpdateQuantityPerUnit.Text = row.Cells[4].Value.ToString();
            tbxUpdateStock.Text = row.Cells[5].Value.ToString();





        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                _productService.Delete(new Product
                {
                    ProductID = Convert.ToInt32(dgwProduct.CurrentRow.Cells[0].Value)
                });
                MessageBox.Show("Ürün Başarılı Bir Şekilde Silindi");
                LoadAllProducts();
            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }


        }
    }
}
