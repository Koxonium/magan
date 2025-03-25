using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace magan
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        ServerConnection connection;

        public Window1(ServerConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            start();
        }
        void start()
        {
            asd();
        }
        async void create(object s, EventArgs e)
        {
            bool temp = await connection.createPerson(NameInput.Text, Convert.ToInt32(AgeInput.Text));
            if (temp)
            {
                MessageBox.Show("Sikeres létrehozás");
                asd();
            }
        }
        async void asd()
        {

            NameStackPanel.Children.Clear();
            AgeStackPanel.Children.Clear();
            List<string> allnames = await connection.AllNames();
            foreach (string item in allnames)
            {
                NameStackPanel.Children.Add(new TextBlock() { Text = item });
                TextBlock nameLabel = new TextBlock();
                nameLabel.Text = item;
                NameStackPanel.Children.Add(nameLabel);
                Button delbutton = new Button();
                delbutton.Content = "X";
                delbutton.Click += async (s, e) =>
                {
                    bool temp = await connection.deletePerson(nameLabel.Text);
                    if (temp)
                    {
                        asd();
                        MessageBox.Show("törölve!");
                    }
                };
                dels.Children.Add(delbutton);
                
            }
            List<string> allages = await connection.AllAges();
            foreach (string item in allages)
            {
                AgeStackPanel.Children.Add(new TextBlock() { Text = item });
            }
        }
    }
}
