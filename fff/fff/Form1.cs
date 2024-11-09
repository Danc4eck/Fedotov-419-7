using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace fff
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Обработчик события при загрузке формы
        private void Form1_Load(object sender, EventArgs e)
        {
            // Здесь можно добавить код, если нужно выполнить действия при запуске формы.
        }

        // Обработчик кнопки Войти
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblError.Text = "Пожалуйста, заполните все поля.";
                return;
            }

            // Проверка на правильность логина и пароля
            if (AuthenticateUser(username, password))
            {
                // Получаем роль пользователя
                string role = GetUserRole(username);

                // В зависимости от роли, открываем нужную форму
                Form nextForm = null;

                if (role == "администратор")
                {
                    nextForm = new admin();
                }
                else if (role == "фармацевт")
                {
                    nextForm = new farma();
                }
                else if (role == "клиент")
                {
                    nextForm = new client();
                }

                // Если форма для роли была найдена, показываем её
                if (nextForm != null)
                {
                    nextForm.Show();
                    this.Hide(); // Скрыть текущую форму
                }
                else
                {
                    lblError.Text = "Роль не распознана.";
                }
            }
            else
            {
                lblError.Text = "Неверный логин или пароль.";
            }
        }

        // Функция для проверки авторизации пользователя (сравнивает логин и пароль)
        private bool AuthenticateUser(string username, string password)
        {
            string connString = "Server=ADCLG1;Database=FEDOTOV19;Integrated Security=True;";  // Замените на свой сервер и базу данных
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM users WHERE username = @username AND password = @password";  // Параметры пароля в открытом виде
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password); // Пароль проверяется в открытом виде
                        int result = (int)cmd.ExecuteScalar();
                        return result > 0;  // Возвращаем true, если есть хотя бы один совпадающий пользователь
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}");
                    return false;
                }
            }
        }

        // Функция для получения роли пользователя
        private string GetUserRole(string username)
        {
            string connString = "Server=ADCLG1;Database=FEDOTOV19;Integrated Security=True;";  // Замените на свой сервер и базу данных
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT role FROM users WHERE username = @username";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        return cmd.ExecuteScalar()?.ToString();  // Возвращаем роль пользователя
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при получении роли: {ex.Message}");
                    return string.Empty;
                }
            }
        }
    }
}
