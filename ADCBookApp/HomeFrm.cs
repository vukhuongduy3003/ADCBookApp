using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;

namespace ADCBookApp
{
    public interface IViewController
    {
        void AddNewItem<T>(T item);
        void UpdateItem<T>(T updatedItem);
    }
    public partial class HomeFrm : Form, IViewController
    {

        SqlConnection connection;
        SqlCommand command;
        string str = @"Data Source=VKDUY\SQLEXPRESS;Initial Catalog=DataADCBook;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();

        private List<Item> itemList;
        private List<Discount> discountList;
        private List<Customer> customerList;
        public HomeFrm()
        {
            InitializeComponent();
            CenterToScreen();
            discountList = new List<Discount>();
            customerList = new List<Customer>();
        }

        private static void ConvertDataTable(List<Item> itemList, DataTable table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                Item item1 = new Item();
                item1.ItemId = Convert.ToInt32(table.Rows[i]["idItem"]);
                item1.ItemName = table.Rows[i]["nameItem"].ToString();
                item1.ItemType = table.Rows[i]["typeItem"].ToString();
                item1.Quantity = Convert.ToInt32(table.Rows[i]["quantityItem"]);
                item1.Brand = table.Rows[i]["brandItem"].ToString();
                item1.ReleaseDate = (DateTime)table.Rows[i]["releaseDateItem"];
                item1.Price = Convert.ToInt32(table.Rows[i]["priceItem"]);
                itemList.Add(item1);
            }
        }

        public void AddNewItem<T>(T item)
        {
            if (typeof(T) == typeof(Item))
            {
                connection = new SqlConnection(str);
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Item";
                adapter.SelectCommand = command;
                table.Clear();
                adapter.Fill(table);
                tblItem.Rows.Clear();
                itemList = new List<Item>();

                ConvertDataTable(itemList, table);
                foreach (Item i in itemList)
                {
                    tblItem.Rows.Add(new object[]
                    {
                        i.ItemId, i.ItemName, i.ItemType, i.Quantity,
                        i.Brand, i.ReleaseDate.ToString("dd/MM/yyyy"), $"{i.Price:N0}",
                        i.Discount == null ? "-" : i.Discount.Name
                    });
                }
            }
            else if (typeof(T) == typeof(Customer))
            {

            }
        }

        public void UpdateItem<T>(T updatedItem)
        {
            if (typeof(T) == typeof(Item))
            {
                var newItem = updatedItem as Item;

                connection = new SqlConnection(str);
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = "UPDATE Item SET nameItem = N'"+ newItem.ItemName + "', typeItem = N'" + newItem.ItemType + "', quantityItem = "+ newItem.Quantity + ", brandItem = N'"+ newItem.Brand + "', releaseDateItem = '"+ newItem.ReleaseDate + "', priceItem = "+ newItem.Price +" WHERE idItem = " + newItem.ItemId + "";
                adapter.SelectCommand = command;
                table.Clear();
                adapter.Fill(table);
                tblItem.Rows.Clear();

                ConvertDataTable(itemList, table);
                foreach (Item i in itemList)
                {
                    tblItem.Rows.Add(new object[]
                    {
                        i.ItemId, i.ItemName, i.ItemType, i.Quantity,
                        i.Brand, i.ReleaseDate.ToString("dd/MM/yyyy"), $"{i.Price:N0}",
                        i.Discount == null ? "-" : i.Discount.Name
                    });
                }
            }
        }

        private void btnAddNewItem_Click(object sender, System.EventArgs e)
        {
            if(sender.Equals(btnAddNewItem))
            {
                var childView = new AddEditItemFrm(this, discountList);
                childView.Show();
            }
        }

        private void tblItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tblItem.Columns["tblItemEdit"].Index)
            {
                Item item = itemList[e.RowIndex];
                var updateItemView = new AddEditItemFrm(this, discountList, item);
                updateItemView.Show();
            }
            else if (e.RowIndex >= 0 && e.ColumnIndex == tblItem.Columns["tblItemRemove"].Index)
            {
                Item item = itemList[e.RowIndex];
                var title = "Xác nhận xóa";
                var msg = "Bạn có chắc chắn muốn xóa bản ghi này không?";
                var ans = ShowConfirmDialog(msg, title);
                if (ans == DialogResult.Yes)
                {
                    connection = new SqlConnection(str);
                    connection.Open();
                    command = connection.CreateCommand();
                    command.CommandText = "DELETE FROM Item WHERE idItem = " + item.ItemId + ";";
                    adapter.SelectCommand = command;
                    table.Clear();
                    adapter.Fill(table);
                    command.CommandText = "SELECT * FROM Item;";
                    adapter.SelectCommand = command;
                    table.Clear();
                    adapter.Fill(table);
                    tblItem.Rows.Clear();
                    itemList = new List<Item>();

                    ConvertDataTable(itemList, table);
                    foreach (Item i in itemList)
                    {
                        tblItem.Rows.Add(new object[]
                        {
                        i.ItemId, i.ItemName, i.ItemType, i.Quantity,
                        i.Brand, i.ReleaseDate.ToString("dd/MM/yyyy"), $"{i.Price:N0}",
                        i.Discount == null ? "-" : i.Discount.Name
                        });
                    }
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private DialogResult ShowConfirmDialog(string msg, string title)
        {
            return MessageBox.Show(msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

    }
}
