using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        private List<MyTask> tasks = new List<MyTask>();
        private List<string> stat = new List<string>();
        
        const string DATAFILE = @"..\..\tasks.txt";
        public Window2()
        {
            InitializeComponent();
            stat.Add("Done");
            stat.Add("Pending");
            AfterLoad();
        }// ========================================================================================> end of constructor

        private void AfterLoad()
        {
            ReadFromFile();
            lvShow.ItemsSource = tasks;
            comboStatus.ItemsSource = stat;
            
            btnDelete.IsEnabled = false;
            btnUpdate.IsEnabled = false;

        }// ========================================================================================> end of ReadFromFile()

        private void ReadFromFile()
        {
            Boolean corrupted = false;
            try
            {
                string[] file = File.ReadAllLines(DATAFILE);

                foreach (string line in file)
                {
                    string[] record = line.Split(';');

                    /*if legth of the array is not 4 then -
                    we know the line is corrupted and we don't create a MyTast object.*/
                    if (record.Length != 4) {
                        corrupted = true;
                        continue;
                    }

                    int diff;
                    var cultureInfo = new CultureInfo("en-CA");
                    DateTime date;
                    MessageBox.Show( record[2]);

                    // validation for status
                    if (stat.Contains(record[3]))
                    {

                        // Date check and convert
                        if (DateTime.TryParseExact(record[2], "yyyy-mm-dd", cultureInfo,
                                     DateTimeStyles.None, out date))
                        {
                            

                            if (int.TryParse(record[1], out diff))
                            {
                                MyTask myTask = new MyTask(record[0], diff, date, record[3]);
                                tasks.Add(myTask);
                            }
                        }
                    }

                }
            }
            catch
            {
                MessageBox.Show($"There is an issue with this file ==> {DATAFILE} ");
            }
            if (corrupted) MessageBox.Show("One or more record(s) didn't load! because they are coruppted!");
            corrupted = false;
            
            lvShow.Items.Refresh();
        }// ========================================================================================> end of ReadFromFile()


        // unselect all the items in listView ==>  lvshow
        private void CleanUp()
        {
            btnDelete.IsEnabled = false;
            btnUpdate.IsEnabled = false;
            lvShow.SelectedIndex = -1;
        }// ========================================================================================> end of CleanUp()

        private void lvShow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (MyTask)lvShow.SelectedItem;

            /*after cleanup(), this method is called aswell!
            so we need to make sure SelectedIndex is not null*/
            if (lvShow.SelectedIndex == -1) return;
            
            tBoxTask.Text = selected.Task;
            tBoxDate.Text = selected.Due.ToString("d");
            slideDiff.Value = selected.Difficulty;
            comboStatus.SelectedItem = selected.Status;
            btnDelete.IsEnabled = true;
            btnUpdate.IsEnabled = true;
        }// ========================================================================================> end of lvShow_SelectionChanged()

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string task = tBoxTask.Text;
            int diff = int.Parse(slideDiff.Value.ToString());
            string status = comboStatus.Text;
            DateTime date;
            var cultureInfo = new CultureInfo("en-CA");
            if (DateTime.TryParseExact(tBoxDate.Text, "yyyy-mm-dd", cultureInfo,
                                 DateTimeStyles.None, out date))
            {
                MyTask myTask = new MyTask(task, diff, date, status);
                bool containsItem = tasks.Any(item => item.ToString().Equals(myTask.ToString()));
                if (!containsItem) tasks.Add(myTask);

            }

            lvShow.Items.Refresh();
            CleanUp();

        }// ========================================================================================> end of btnAdd_Click()

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var itemsToDelete = lvShow.SelectedItems;
            foreach(MyTask task in itemsToDelete)
            {
                tasks.Remove(task);
            }
            lvShow.Items.Refresh();
            CleanUp();
        }// ========================================================================================> end of btnDelete_Click()

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var itemToUpdate = (MyTask)lvShow.SelectedItem;
            string task = tBoxTask.Text;
            DateTime date;
            string status = comboStatus.Text;
            int diff;
            var cultureInfo = new CultureInfo("en-CA");
            if (DateTime.TryParseExact(tBoxDate.Text, "yyyy-mm-dd", cultureInfo,
                                 DateTimeStyles.None, out date))
            {
                if (int.TryParse(slideDiff.Value.ToString(), out diff)) {
                    itemToUpdate.Task = task;
                    itemToUpdate.Difficulty = diff;
                    itemToUpdate.Due = date;
                    itemToUpdate.Status = status;
                }
            }

            lvShow.Items.Refresh();
            CleanUp();

        }// ========================================================================================> end of btnUpdate_Click()

        private void btnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt|C# file (*.cs)|*.cs";

            if (saveFileDialog.ShowDialog() == true)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    foreach (MyTask task in tasks)
                    {
                        writer.WriteLine(task.ToDataString());
                    }

                }
            }
                
        }// ========================================================================================> end of btnSaveFile_Click()

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            using (StreamWriter writer = new StreamWriter(DATAFILE))
            {
                foreach (MyTask task in tasks)
                {
                    writer.WriteLine(task.ToDataString());
                }

            }
        }// ========================================================================================> end of Window_Closing()
    }

}
