using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace AdministrationsAPp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal List<User> normalUsers = new List<User>();
        internal List<User> adminUsers = new List<User>();

        bool userLBSelected;

        public MainWindow()
        {
            InitializeComponent();

            // Create listeners
            CreateButton.Click += CreateButton_Click;
            DeleteButton.Click += DeleteButton_Click;
            ChangeButton.Click += ChangeButton_Click;
            ToUserButton.Click += ToOtherListButton_Click;
            ToAdminButton.Click += ToOtherListButton_Click;
            AdminLB.SelectionChanged += AdminLB_SelectionChanged;
            AdminLB.GotFocus += AdminLB_GotFocus;
            UserLB.SelectionChanged += UserLB_SelectionChanged;
            UserLB.GotFocus += UserLB_GotFocus;


            // Set the source for the ListBoxes and choose display value
            UserLB.ItemsSource = normalUsers;
            AdminLB.ItemsSource = adminUsers;

            UserLB.DisplayMemberPath = "UserName";
            AdminLB.DisplayMemberPath = "UserName";

        }

        private void AdminLB_GotFocus(object sender, RoutedEventArgs e)
        {
            UserLB.SelectedItem = null;
            userLBSelected = false;
        }

        private void UserLB_GotFocus(object sender, RoutedEventArgs e)
        {
            AdminLB.SelectedItem = null;
            userLBSelected = true;
        }

        private void ToOtherListButton_Click(object sender, RoutedEventArgs e)
        {
            if (userLBSelected)
            {
                if ((UserLB.SelectedItem as User) != null)
                {
                    adminUsers.Add(UserLB.SelectedItem as User);
                    normalUsers.Remove(UserLB.SelectedItem as User);
                    ListBoxRefresh();
                    ToAdminButton.IsEnabled = false;
                }
                DeleteButton.IsEnabled = false;
                ChangeButton.IsEnabled = false;
            }
            else
            {
                if ((AdminLB.SelectedItem as User) != null)
                {
                    normalUsers.Add(AdminLB.SelectedItem as User);
                    adminUsers.Remove(AdminLB.SelectedItem as User);
                    ListBoxRefresh();
                    ToUserButton.IsEnabled = false;
                }
                DeleteButton.IsEnabled = false;
                ChangeButton.IsEnabled = false;
            }

        }
        private void ToAdminButton_Click(object sender, RoutedEventArgs e)
        {
            if ((UserLB.SelectedItem as User) != null)
            {
                adminUsers.Add(UserLB.SelectedItem as User);
                normalUsers.Remove(UserLB.SelectedItem as User);
                ListBoxRefresh();
                ToAdminButton.IsEnabled = false;
            }
            DeleteButton.IsEnabled = false;
            ChangeButton.IsEnabled = false;
        }

        private void ToUserButton_Click(object sender, RoutedEventArgs e)
        {
            if ((AdminLB.SelectedItem as User) != null)
            {
                normalUsers.Add(AdminLB.SelectedItem as User);
                adminUsers.Remove(AdminLB.SelectedItem as User);
                ListBoxRefresh();
                ToUserButton.IsEnabled = false;
            }
            DeleteButton.IsEnabled = false;
            ChangeButton.IsEnabled = false;

        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (AdminLB.SelectedIndex != -1)
            {
                (AdminLB.SelectedItem as User).UserName = NameTB.Text;
                (AdminLB.SelectedItem as User).UserEmail = EmailTB.Text;
                ListBoxRefresh();
            }
            else if (UserLB.SelectedIndex != -1)
            {
                (UserLB.SelectedItem as User).UserName = NameTB.Text;
                (UserLB.SelectedItem as User).UserEmail = EmailTB.Text;
                ListBoxRefresh();
            }

            NameTB.Text = null;
            EmailTB.Text = null;

            ChangeButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (AdminLB.SelectedIndex != -1)
            {
                adminUsers.Remove((AdminLB.SelectedItem as User));
                ListBoxRefresh();
            }
            else if (UserLB.SelectedIndex != -1)
            {
                normalUsers.Remove((UserLB.SelectedItem as User));
                ListBoxRefresh();
            }

            NameTB.Text = null;
            EmailTB.Text = null;

            ChangeButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameTB.Text != "" && EmailTB.Text != "")
            {
                normalUsers.Add(new User(NameTB.Text, EmailTB.Text));
                NameTB.Text = null;
                EmailTB.Text = null;
                ListBoxRefresh();
            }

        }

        /// <summary>
        /// Updates the List Boxes with current users.
        /// </summary>
        private void ListBoxRefresh()
        {
            AdminLB.Items.Refresh();
            UserLB.Items.Refresh();
            AdminLB.SelectedItem = null;
            UserLB.SelectedItem = null;
        }

        private void UserLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                SelectedUserLbl.Content = "Vald användare:\n" +
                    "Namn: " + (UserLB.SelectedItem as User).UserName + "\n" +
                    "Epost: " + (UserLB.SelectedItem as User).UserEmail;
                NameTB.Text = (UserLB.SelectedItem as User).UserName;
                EmailTB.Text = (UserLB.SelectedItem as User).UserEmail;

                ToAdminButton.IsEnabled = true;
                ToUserButton.IsEnabled = false;
                DeleteButton.IsEnabled = true;
                ChangeButton.IsEnabled = true;
            }
            catch
            {
                SelectedUserLbl.Content = "Vald användare: ";
                NameTB.Text = null;
                EmailTB.Text = null;
            }
        }

        private void AdminLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                SelectedUserLbl.Content = "Vald användare:\n" +
                    "Namn: " + (AdminLB.SelectedItem as User).UserName + "\n" +
                    "Epost: " + (AdminLB.SelectedItem as User).UserEmail;
                NameTB.Text = (AdminLB.SelectedItem as User).UserName;
                EmailTB.Text = (AdminLB.SelectedItem as User).UserEmail;

                ToAdminButton.IsEnabled = false;
                ToUserButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
                ChangeButton.IsEnabled = true;
            }
            catch
            {
                SelectedUserLbl.Content = "Vald användare:";
                NameTB.Text = null;
                EmailTB.Text = null;
            }
        }
    }
}
