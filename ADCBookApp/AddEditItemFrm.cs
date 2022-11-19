using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ADCBookApp
{
    public partial class AddEditItemFrm : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = @"Data Source=VKDUY\SQLEXPRESS;Initial Catalog=DataADCBook;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();

        void loadData()
        {
            command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Item";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
        }

        private List<Discount> discountList;
        private Item item;
        private IViewController controller;
        public AddEditItemFrm()
        {
            InitializeComponent();
            CenterToScreen();
        }

        public AddEditItemFrm(IViewController materView, List<Discount> discounts, Item _item = null): this()
        {
            discountList = discounts;
            controller = materView;
            if (_item != null)
            {
                Text = "CẬP NHẬT THÔNG TIN sách";
                btnAddNew.Text = "Cập nhật";
                item = _item;
                ShowItemData();
            }
        }

        private void ShowItemData()
        {
            txtId.Text = $"{item.ItemId}";
            txtItemName.Text = item.ItemName;
            txtBrand.Text = item.Brand;
            numericQuantity.Value = item.Quantity;
            numericPrice.Value = item.Price;
            datePickerReleaseDate.Value = item.ReleaseDate;
            comboItemType.Text = item.ItemType;
            for (int i = 0; i < comboDiscount.Items.Count; i++)
            {
                if (item.Discount?.Name.CompareTo(comboDiscount.Items[i]) == 0)
                {
                    comboDiscount.SelectedIndex = i;
                    break;
                }
            }
            for (int i = 0; i < comboDiscount.Items.Count; i++)
            {
                if (item.Discount?.Name.CompareTo(comboDiscount.Items[i]) == 0)
                {
                    comboDiscount.SelectedIndex = i;
                    break;
                }
            }
        }

        private void btnAddNew_Click(object sender, System.EventArgs e)
        {
            var itemId = Int32.Parse(txtId.Text);
            var itemName = txtItemName.Text;
            var itemType = comboItemType.Text;
            var itemPrice = (int)numericPrice.Value;
            var quantity = (int)numericQuantity.Value;
            var brand = txtBrand.Text;
            var manufactureDate = datePickerReleaseDate.Value;
            Discount discount = null;
            if (comboDiscount.SelectedIndex > -1)
            {
                discount = discountList[comboDiscount.SelectedIndex];
            }
            try
            {
                if (string.IsNullOrEmpty(itemName))
                {
                    var msg = "Tên sách không được để trống.";
                    throw new InvalidItemNameException(msg);
                }
                if (itemType == null || itemType == "")
                {
                    var msg = "Vui lòng chọn loại sách trước.";
                    MessageBox.Show(msg, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (btnAddNew.Text.CompareTo("Cập nhật") == 0)
                {
                    item.ItemId = itemId;
                    item.ItemName = itemName;
                    item.Price = itemPrice;
                    item.Brand = brand;
                    item.Discount = discount;
                    item.ReleaseDate = manufactureDate;
                    item.Quantity = quantity;
                    item.ItemType = itemType;
                    var msg = "Bạn có chắc chắn muốn lưu lại các thay đổi?";
                    var title = "Xác nhận cập nhật";
                    var ans = ShowConfirmMessage(title, msg);
                    if (ans == DialogResult.Yes)
                    {
                        controller.UpdateItem(item);
                        Dispose();
                    }
                }
                else
                {
                    Item item = new Item(itemName, itemType, quantity, brand, manufactureDate, itemPrice, discount);
                    connection = new SqlConnection(str);
                    connection.Open();
                    command = connection.CreateCommand();
                    command.CommandText = $"INSERT INTO Item (nameItem, typeItem, quantityItem, brandItem, releaseDateItem, priceItem)" +
                        $"VALUES (N'" + txtItemName.Text + "', N'" + comboItemType.Text + "', " + numericQuantity.Value + ", N'" + txtBrand.Text + "', '" + datePickerReleaseDate.Value + "', " + numericPrice.Value + ");";
                    command.ExecuteNonQuery();
                    controller.AddNewItem(item);
                }
            }
            catch (InvalidItemNameException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            var title = "Xác nhận";
            var message = "Bạn có chắc muốn hủy bỏ?";
            var ans = ShowConfirmMessage(title, message);
            if (ans == DialogResult.Yes)
            {
                Dispose();
            }
        }

        private DialogResult ShowConfirmMessage(string title, string message)
        {
            return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}
