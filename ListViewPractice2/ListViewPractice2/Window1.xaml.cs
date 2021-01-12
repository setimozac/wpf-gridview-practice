using System;
using System.Collections.Generic;
using System.IO;
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

namespace ListViewPractice2
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        List<Person> people = new List<Person>();
        const string DataFileName = @"..\..\people.txt";
        public Window1()
        {
            InitializeComponent();
            LoadDataFromFile();
        }

        private void LoadDataFromFile()
        {
            if (File.Exists(DataFileName))
            {
                string[] peopleInfo = File.ReadAllLines(DataFileName);

                foreach (string personLine in peopleInfo)
                {
                    string[] person = personLine.Split(';');

                    string name = person[0];
                    int age;
                    if (!int.TryParse(person[1], out age))
                    {
                        MessageBox.Show("an error happened in reading the file");
                    }
                    Person p = new Person(name, age);
                    people.Add(p);
                }
            }
            vGrid.ItemsSource = people;
            btnDelete.IsEnabled = false;
            btnUpdate.IsEnabled = false;
        }

        private void listV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            btnDelete.IsEnabled = true;
            btnUpdate.IsEnabled = true;

            if (vGrid.SelectedIndex != -1)
            {
                Person person = (Person)vGrid.SelectedItem;
                tBoxName.Text = person.Name;
                tBoxAge.Text = person.Age.ToString();
            }


        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (vGrid.SelectedIndex == -1)
            {
                MessageBox.Show("You need to choose one Person");
            }
            else
            {
                string newName = tBoxName.Text;
                string newAge = tBoxAge.Text;

                Person personToBeUpdated = (Person)vGrid.SelectedItem;
                personToBeUpdated.Name = newName;
                personToBeUpdated.Age = int.Parse(newAge);
                vGrid.Items.Refresh();
            }

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tBoxAge.Text = "";
            tBoxName.Text = "";
            vGrid.SelectedIndex = -1;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (vGrid.SelectedIndex != -1)
            {
                Person personToDelete = (Person)vGrid.SelectedItem;
                people.Remove(personToDelete);
                vGrid.Items.Refresh();
                ResetValue();
            }
            else
            {
                MessageBox.Show("You need to choose a person");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = tBoxName.Text;
            string ageText = tBoxAge.Text;

            int age;
            if (int.TryParse(ageText, out age))
            {
                people.Add(new Person(name, age));
                vGrid.Items.Refresh();
                ResetValue();
            }
            else
            {
                MessageBox.Show("age has to be number");
            }

        }
        private void ResetValue()
        {
            tBoxAge.Text = "";
            tBoxName.Text = "";
            vGrid.SelectedIndex = -1;
            btnDelete.IsEnabled = false;
            btnUpdate.IsEnabled = false;
        }

        /*private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            using (StreamWriter writer = new StreamWriter(DataFileName))
            {
                foreach (Person p in people)
                {
                    writer.WriteLine($"{p.Name};{p.Age}");
                }
            }

        }*/

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            using (StreamWriter writer = new StreamWriter(DataFileName))
            {
                foreach (Person p in people)
                {
                    writer.WriteLine($"{p.Name};{p.Age}");
                }
            }
        }
    }

    
}
