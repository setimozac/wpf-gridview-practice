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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ListViewPractice2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Person> people = new List<Person>();
        const string DataFileName = @"..\..\people.txt";
        
        public MainWindow()
        {
            InitializeComponent();
            LoadDataFromFile();
            
        }



        private void LoadDataFromFile()
        {
            if (File.Exists(DataFileName))
            {
                string[] peopleInfo = File.ReadAllLines(DataFileName);

                foreach(string personLine in peopleInfo)
                {
                    string[] person = personLine.Split(';');

                    string name = person[0];
                    int age;
                    if(!int.TryParse(person[1],out age))
                    {
                        MessageBox.Show("an error happened in reading the file");
                    }
                    Person p = new Person(name, age);
                    people.Add(p);
                }
            }
            listV.ItemsSource = people;
        }

        private void listV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            btnDelete.IsEnabled = true;
            btnUpdate.IsEnabled = true;

            if(listV.SelectedIndex != -1)
            {
                Person person = (Person)listV.SelectedItem;
                tBoxName.Text = person.Name;
                tBoxAge.Text = person.Age.ToString();
            }
            

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if(listV.SelectedIndex == -1)
            {
                MessageBox.Show("You need to choose one Person");
            }
            else
            {
                string newName = tBoxName.Text;
                string newAge = tBoxAge.Text;

                Person personToBeUpdated = (Person)listV.SelectedItem;
                personToBeUpdated.Name = newName;
                personToBeUpdated.Age = int.Parse(newAge);
                listV.Items.Refresh();
            }
            
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tBoxAge.Text = "";
            tBoxName.Text = "";
            listV.SelectedIndex = -1;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(listV.SelectedIndex != -1)
            {
                Person personToDelete = (Person)listV.SelectedItem;
                people.Remove(personToDelete);
                listV.Items.Refresh();
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
            if(int.TryParse(ageText, out age))
            {
                people.Add(new Person(name, age));
                listV.Items.Refresh();
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
            listV.SelectedIndex = -1;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            using (StreamWriter writer = new StreamWriter(DataFileName))
            {
                foreach(Person p in people)
                {
                    writer.WriteLine($"{p.Name};{p.Age}");
                }
            }
            
        }
    }
}

