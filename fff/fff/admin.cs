using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fff
{
    public partial class admin : Form
    {
        public admin()
        {
            InitializeComponent();
        }

        private void clientsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.clientsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.fEDOTOV19DataSet);

        }

        private void admin_Load(object sender, EventArgs e)
        {
            //// TODO: данная строка кода позволяет загрузить данные в таблицу "fEDOTOV19DataSet.users". При необходимости она может быть перемещена или удалена.
            //this.usersTableAdapter.Fill(this.fEDOTOV19DataSet.users);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "fEDOTOV19DataSet.orders". При необходимости она может быть перемещена или удалена.
            this.ordersTableAdapter.Fill(this.fEDOTOV19DataSet.orders);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "fEDOTOV19DataSet.items". При необходимости она может быть перемещена или удалена.
            this.itemsTableAdapter.Fill(this.fEDOTOV19DataSet.items);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "fEDOTOV19DataSet.clients". При необходимости она может быть перемещена или удалена.
            this.clientsTableAdapter.Fill(this.fEDOTOV19DataSet.clients);

        }
    }
}
