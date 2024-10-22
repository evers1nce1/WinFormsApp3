namespace WinFormsApp3
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string username = username_textBox.Text;
            if (!string.IsNullOrEmpty(username))
            {
                MenuForm gameForm = new MenuForm(username);
                gameForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Введите имя пользователя");
            }
        }
    }
}
