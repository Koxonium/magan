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
            loadPersons();
        }
        async void create(object s, EventArgs e)
        {
            try
            {
                bool temp = await connection.createPerson(NameInput.Text, Convert.ToInt32(AgeInput.Text));
                if (temp)
                {
                    loadPersons();
                    MessageBox.Show("Sikeres létrehozás");
                }
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message);
            }
        }

        async void deleteAll(object s, EventArgs e)
        {
            bool temp = await connection.deleteAllPerson();
            if (temp)
            {
                loadPersons();
                MessageBox.Show("Sikeres törlés");
            }
        }

        async void loadPersons()
        {
            NameStackPanel.Children.Clear();
            AgeStackPanel.Children.Clear();
            dels.Children.Clear();
            List<string> allNames = await connection.AllNames();
            List<string> allAges = await connection.AllAges();
            for (int i = 0; i < allNames.Count; i++)
            {
                StackPanel ageandedit = new StackPanel();
                

                string name = allNames[i];
                string age = allAges[i];

                TextBox nameLabel = new TextBox();
                nameLabel.Text = name;
                NameStackPanel.Children.Add(nameLabel);

                TextBox ageLabel = new TextBox();
                ageLabel.Text = age;
                ageLabel.Width = 150;

                Button delButton = new Button();
                delButton.Content = "X";
                delButton.Click += async (s, e) =>
                {
                    bool temp = await connection.deletePerson(nameLabel.Text);
                    if (temp)
                    {
                        loadPersons();
                        MessageBox.Show("Sikeres törlés!");
                    }
                };
                dels.Children.Add(delButton);

                string oldname = allNames[i];

                Button editButton = new Button();
                editButton.Content = "Edit";
                editButton.Click += async (s, e) =>
                {
                    try
                    {
                        string newName = nameLabel.Text;
                        int newAge = int.Parse(ageLabel.Text);

                        bool temp = await connection.editPerson(newName, oldname, newAge);
                        if (temp)
                        {
                            loadPersons();
                            MessageBox.Show("Sikeres módosítás!");
                        }
                    }
                    catch (Exception e2)
                    {
                        MessageBox.Show(e2.Message);
                    }
                };
                editButton.Width = 150;

                ageandedit.Orientation = Orientation.Horizontal;
                ageandedit.Children.Add(ageLabel);
                ageandedit.Children.Add(editButton);
                AgeStackPanel.Children.Add(ageandedit);
            }


        }
    }
}
