using ADCBookApp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace ADCBookApp
{
    public partial class HomeFrm : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = @"Data Source=VKDUY\SQLEXPRESS;Initial Catalog=DataADCBook;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        public static HomeFrm hform;
        private List<Company> companies;
        private List<Company> companiesSearched;
        private List<Author> authors;
        private List<Author> authorsSearched;
        public HomeFrm()
        {
            InitializeComponent();
            CenterToScreen();
            hform = this;
        }

        public static void ConvertDataTable(List<Company> companyList, DataTable table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                Company company = new Company();
                company.companyId = Convert.ToInt32(table.Rows[i]["idCompany"]);
                company.companyName = table.Rows[i]["nameCompany"].ToString();
                company.address = table.Rows[i]["addressCompany"].ToString();
                company.phoneNumber = table.Rows[i]["phoneNumber"].ToString();
                companyList.Add(company);
            }
        }

        public static void ConvertDataTableAuthor(List<Author> authorList, DataTable table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                Author author = new Author();
                author.authorId = Convert.ToInt32(table.Rows[i]["idAuthor"]);
                author.authorName = table.Rows[i]["nameAuthor"].ToString();
                author.birstYear = Convert.ToInt32(table.Rows[i]["birthYear"]);
                author.homeTown = table.Rows[i]["homeTown"].ToString();
                authorList.Add(author);
            }
        }

        public void ShowCompany()
        {
            connection = new SqlConnection(str);
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Company";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            companies = new List<Company>();
            tblCompany.Rows.Clear();
            ConvertDataTable(companies, table);
            foreach (Company i in companies)
            {
                tblCompany.Rows.Add(new object[]
                {
                        i.companyId, i.companyName, i.address, i.phoneNumber
                });
            }
            connection.Close();
        }

        public void ShowAuthor()
        {
            connection = new SqlConnection(str);
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Author";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            authors = new List<Author>();
            tblAuthor.Rows.Clear();
            ConvertDataTableAuthor(authors, table);
            foreach (Author i in authors)
            {
                tblAuthor.Rows.Add(new object[]
                {
                        i.authorId, i.authorName, i.birstYear, i.homeTown
                });
            }
        }

        public void UpdateCompany(Company updateCompany)
        {
                var newCompany = updateCompany as Company;
                connection = new SqlConnection(str);
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = "UPDATE Company SET nameCompany = N'" + newCompany.companyName + "', addressCompany = N'" + newCompany.address + "', phoneNumber = N'" + newCompany.phoneNumber + "' WHERE idCompany = " + newCompany.companyId + "";
                adapter.SelectCommand = command;
                table.Clear();
                adapter.Fill(table);
                tblCompany.Rows.Clear();
                ConvertDataTable(companies, table);
                foreach (Company i in companies)
                {
                    tblCompany.Rows.Add(new object[]
                    {
                        i.companyId, i.companyName, i.address, i.phoneNumber
                    });
                }
        }

        public void UpdateAuthor(Author updateAuthor)
        {
            var newAuthor = updateAuthor as Author;
            connection = new SqlConnection(str);
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = "UPDATE Author SET nameAuthor = N'" + newAuthor.authorName + "', birthYear = N'" + newAuthor.birstYear + "', homeTown = N'" + newAuthor.homeTown + "' WHERE idAuthor = " + newAuthor.authorId + "";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            tblAuthor.Rows.Clear();
            ConvertDataTableAuthor(authors, table);
            foreach (Author i in authors)
            {
                tblAuthor.Rows.Add(new object[]
                {
                        i.authorId, i.authorName, i.birstYear, i.homeTown
                });
            }
        }

        private void tblCompanyCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tblCompany.Columns["tblCompanyEdit"].Index)
            {
                Company company = companies[e.RowIndex];
                var updateCompanyView = new AddEditCompanyFrm(company);
                updateCompanyView.Show();
            }
            else if (e.RowIndex >= 0 && e.ColumnIndex == tblCompany.Columns["tblCompanyRemove"].Index)
            {
                Company company = companies[e.RowIndex];
                var title = "Xác nhận xóa";
                var msg = "Bạn có chắc chắn muốn xóa bản ghi này không?";
                var ans = ShowConfirmDialog(msg, title);
                if (ans == DialogResult.Yes)
                {
                    connection = new SqlConnection(str);
                    connection.Open();
                    command = connection.CreateCommand();
                    command.CommandText = "DELETE FROM Company WHERE idCompany = " + company.companyId + ";";
                    adapter.SelectCommand = command;
                    table.Clear();
                    adapter.Fill(table);
                    ShowCompany();
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void tblAuthorCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tblAuthor.Columns["tblAuthorEdit"].Index)
            {
                Author author = authors[e.RowIndex];
                var updateAuthorView = new AddEditAuthorFrm(author);
                updateAuthorView.Show();
            }
            else if (e.RowIndex >= 0 && e.ColumnIndex == tblAuthor.Columns["tblAuthorRemove"].Index)
            {
                Author author = authors[e.RowIndex];
                var title = "Xác nhận xóa";
                var msg = "Bạn có chắc chắn muốn xóa bản ghi này không?";
                var ans = ShowConfirmDialog(msg, title);
                if (ans == DialogResult.Yes)
                {
                    connection = new SqlConnection(str);
                    connection.Open();
                    command = connection.CreateCommand();
                    command.CommandText = "DELETE FROM Author WHERE idAuthor = " + author.authorId + ";";
                    adapter.SelectCommand = command;
                    table.Clear();
                    adapter.Fill(table);
                    ShowAuthor();
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private DialogResult ShowConfirmDialog(string msg, string title)
        {
            return MessageBox.Show(msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        private void BtnAddNewCompanyClick(object sender, EventArgs e)
        {
            var childView = new AddEditCompanyFrm();
            childView.Show();
        }

        private void BtnAddNewAuthorClick(object sender, EventArgs e)
        {
            var childView = new AddEditAuthorFrm();
            childView.Show();
        }

        private void HomeFrm_Load(object sender, EventArgs e)
        {
            ShowCompany();
        }

        private void tabControlCategoryClick(object sender, EventArgs e)
        {
            if (tabControlCategory.SelectedTab.Text == "NHÀ XUẤT BẢN")
            {
                ShowCompany();
            }
            else if (tabControlCategory.SelectedTab.Text == "TÁC GIẢ")
            {
                ShowAuthor();
            }
            else if (tabControlCategory.SelectedTab.Text == "THỂ LOẠI")
            {
                //ShowAuthor();
            }
            else if (tabControlCategory.SelectedTab.Text == "KHO SÁCH")
            {
                //ShowAuthor();
            }
            else if (tabControlCategory.SelectedTab.Text == "ĐỔI TRẢ")
            {
                //ShowAuthor();
            }
        }

        private void btRefeshCompany(object sender, EventArgs e)
        {
            ShowCompany();
        }

        private void btRefeshAuthor(object sender, EventArgs e)
        {
            ShowAuthor();
        }

        private void SortCompany(object sender, EventArgs e)
        {
            if (sortNameCompanyASC.Equals(sender))
            {
                connection = new SqlConnection(str);
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Company ORDER BY Company.nameCompany ASC;";
                adapter.SelectCommand = command;
                table.Clear();
                adapter.Fill(table);
                companies = new List<Company>();
                tblCompany.Rows.Clear();
                ConvertDataTable(companies, table);
                foreach (Company i in companies)
                {
                    tblCompany.Rows.Add(new object[]
                    {
                        i.companyId, i.companyName, i.address, i.phoneNumber
                    });
                }
                connection.Close();
            }
            else if (sortNameCompanyDESC.Equals(sender))
            {
                connection = new SqlConnection(str);
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Company ORDER BY Company.nameCompany DESC;";
                adapter.SelectCommand = command;
                table.Clear();
                adapter.Fill(table);
                companies = new List<Company>();
                tblCompany.Rows.Clear();
                ConvertDataTable(companies, table);
                foreach (Company i in companies)
                {
                    tblCompany.Rows.Add(new object[]
                    {
                        i.companyId, i.companyName, i.address, i.phoneNumber
                    });
                }
                connection.Close();
            }
        }

        private void SortAuthor(object sender, EventArgs e)
        {
            if (sortNameAuthorASC.Equals(sender))
            {
                connection = new SqlConnection(str);
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Author ORDER BY Author.nameAuthor ASC;";
                adapter.SelectCommand = command;
                table.Clear();
                adapter.Fill(table);
                authors = new List<Author>();
                tblAuthor.Rows.Clear();
                ConvertDataTableAuthor(authors, table);
                foreach (Author i in authors)
                {
                    tblAuthor.Rows.Add(new object[]
                    {
                        i.authorId, i.authorName, i.birstYear, i.homeTown
                    });
                }
            }
            else if (sortNameAuthorDESC.Equals(sender))
            {
                connection = new SqlConnection(str);
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Author ORDER BY Author.nameAuthor DESC;";
                adapter.SelectCommand = command;
                table.Clear();
                adapter.Fill(table);
                authors = new List<Author>();
                tblAuthor.Rows.Clear();
                ConvertDataTableAuthor(authors, table);
                foreach (Author i in authors)
                {
                    tblAuthor.Rows.Add(new object[]
                    {
                        i.authorId, i.authorName, i.birstYear, i.homeTown
                    });
                }
            }
            connection.Close();
        }

        private void ShowSearchedCompany(List<Company> companies)
        {
            foreach (Company i in companies)
            {
                tblCompany.Rows.Add(new object[]
                {
                        i.companyId, i.companyName, i.address, i.phoneNumber
                });
            }
        }

        private void ShowSearchedAuthor(List<Author> authors)
        {
            foreach (Author i in authors)
            {
                tblAuthor.Rows.Add(new object[]
                {
                        i.authorId, i.authorName, i.birstYear, i.homeTown
                });
            }
        }

        private void BtnSearchCompanyClick(object sender, EventArgs e)
        {
            var key = txtSearchCompany.Text;
            tblCompany.Rows.Clear();
            connection = new SqlConnection(str);
            connection.Open();
            command = connection.CreateCommand();
            
            if (comboSeachCompany.SelectedIndex == -1)
            {
                var msg = "Vui lòng chọn tiêu chí tìm kiếm để tiếp tục";
                var title = "Lỗi dữ liệu";
                MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (comboSeachCompany.SelectedIndex == 0)
            {
                
                command.CommandText = "SELECT * FROM Company WHERE Company.idCompany = " + key + ";";
                adapter.SelectCommand = command;
                table.Clear();
                adapter.Fill(table);
                companiesSearched = new List<Company>();
                ConvertDataTable(companiesSearched, table);
                if (companiesSearched.Count == 0)
                {
                    var msg = "Không tìm thấy kết quả nào.";
                    var title = "Kết quả tìm kiếm";
                    MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ShowSearchedCompany(companiesSearched);
                }
            }
            else if (comboSeachCompany.SelectedIndex == 1)
            {
                command.CommandText = "SELECT * FROM Company WHERE Company.nameCompany LIKE '%" + key + "%';";
                adapter.SelectCommand = command;
                table.Clear();
                adapter.Fill(table);
                companiesSearched = new List<Company>();
                ConvertDataTable(companiesSearched, table);
                if (companiesSearched.Count == 0)
                {
                    var msg = "Không tìm thấy kết quả nào.";
                    var title = "Kết quả tìm kiếm";
                    MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ShowSearchedCompany(companiesSearched);
                }
            }
            else if (comboSeachCompany.SelectedIndex == 2)
            {
                command.CommandText = "SELECT * FROM Company WHERE Company.addressCompany LIKE '%" + key + "%';";
                adapter.SelectCommand = command;
                table.Clear();
                adapter.Fill(table);
                companiesSearched = new List<Company>();
                ConvertDataTable(companiesSearched, table);
                if (companiesSearched.Count == 0)
                {
                    var msg = "Không tìm thấy kết quả nào.";
                    var title = "Kết quả tìm kiếm";
                    MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ShowSearchedCompany(companiesSearched);
                }
            }
            else if (comboSeachCompany.SelectedIndex == 3)
            {
                command.CommandText = "SELECT * FROM Company WHERE Company.phoneNumber LIKE '%" + key + "%';";
                adapter.SelectCommand = command;
                table.Clear();
                adapter.Fill(table);
                companiesSearched = new List<Company>();
                ConvertDataTable(companiesSearched, table);
                if (companiesSearched.Count == 0)
                {
                    var msg = "Không tìm thấy kết quả nào.";
                    var title = "Kết quả tìm kiếm";
                    MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ShowSearchedCompany(companiesSearched);
                }
            }
        }

        private void BtnSearchAuthorClick(object sender, EventArgs e)
        {
            var key = txtSearchAuthor.Text;
            tblAuthor.Rows.Clear();
            connection = new SqlConnection(str);
            connection.Open();
            command = connection.CreateCommand();

            if (comboSeachAuthor.SelectedIndex == -1)
            {
                var msg = "Vui lòng chọn tiêu chí tìm kiếm để tiếp tục";
                var title = "Lỗi dữ liệu";
                MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (comboSeachAuthor.SelectedIndex == 0)
            {
                command.CommandText = "SELECT * FROM Author WHERE Author.idAuthor = " + key + ";";
                adapter.SelectCommand = command;
                table.Clear();
                adapter.Fill(table);
                authorsSearched = new List<Author>();
                ConvertDataTableAuthor(authorsSearched, table);
                if (authorsSearched.Count == 0)
                {
                    var msg = "Không tìm thấy kết quả nào.";
                    var title = "Kết quả tìm kiếm";
                    MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ShowSearchedAuthor(authorsSearched);
                }
            }
            else if (comboSeachAuthor.SelectedIndex == 1)
            {
                command.CommandText = "SELECT * FROM Author WHERE Author.nameAuthor LIKE '%" + key + "%';";
                adapter.SelectCommand = command;
                table.Clear();
                adapter.Fill(table);
                authorsSearched = new List<Author>();
                ConvertDataTableAuthor(authorsSearched, table);
                if (authorsSearched.Count == 0)
                {
                    var msg = "Không tìm thấy kết quả nào.";
                    var title = "Kết quả tìm kiếm";
                    MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ShowSearchedAuthor(authorsSearched);
                }
            }
        }

    }
}
