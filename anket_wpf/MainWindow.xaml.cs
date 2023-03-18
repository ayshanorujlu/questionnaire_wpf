using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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

namespace anket_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
   
        public partial class MainWindow : Window
        {
            public ObservableCollection<Person> Persons
            {
                get { return (ObservableCollection<Person>)GetValue(PersonsProperty); }
                set { SetValue(PersonsProperty, value); }
            }

            public static readonly DependencyProperty PersonsProperty =
                DependencyProperty.Register("Persons", typeof(ObservableCollection<Person>), typeof(MainWindow));

            public MainWindow()
            {
                Persons = new ObservableCollection<Person>();
                InitializeComponent();
                DataContext = this;
            }

            private void Button_Click(object sender, RoutedEventArgs e)
            {
                if (sender is Button b)
                {
                    if (b.Content.ToString() == "Add")
                    {
                        if (name_text.Text != string.Empty && surname_text.Text != string.Empty && email_text.Text != string.Empty && phone_text.Text != string.Empty && birth_date.Text != string.Empty)
                        {
                            Persons.Add(new Person()
                            {
                                Name = name_text.Text,
                                Surname = surname_text.Text,
                                Email = email_text.Text,
                                Number = phone_text.Text,
                                Date = birth_date.Text
                            });
                        }
                        else
                        {
                            MessageBox.Show("Fill all sections", "Application", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else if (b.Content.ToString() == "Change")
                    {
                        if (members_list.SelectedItem is Person p)
                        {
                            p.Name = name_text.Text;
                            p.Surname = surname_text.Text;
                            p.Number = phone_text.Text;
                            p.Email = email_text.Text;
                            p.Date = birth_date.Text;
                            b.Content = "Add";
                            name_text.Text = string.Empty;
                            surname_text.Text = string.Empty;
                            phone_text.Text = string.Empty;
                            email_text.Text = string.Empty;
                            birth_date.Text = string.Empty;

                        }
                    }
                }
            }

            private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
                if (sender is ListBox l)
                {
                    if (l.SelectedItem is Person person)
                    {
                        name_text.Text = person.Name;
                        surname_text.Text = person.Surname;
                        email_text.Text = person.Email;
                        phone_text.Text = person.Number;
                        birth_date.Text = person.Date;
                        add_btn.Content = "Change";
                    }
                }
            }

            private void Button_Click_1(object sender, RoutedEventArgs e)
            {
                try
                {
                    var json = File.ReadAllText(filename_text.Text + ".json");
                    Persons = JsonSerializer.Deserialize<ObservableCollection<Person>>(json);
                    filename_text.Text = string.Empty;
                    MessageBox.Show("Loaded", "Application", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (FileNotFoundException d)
                {
                    MessageBox.Show("File not found", "Application", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            private void Button_Click_2(object sender, RoutedEventArgs e)
            {
                if (members_list.ItemsSource is ObservableCollection<Person> list)
                {
                    if (list.Count > 0)
                    {
                        if (filename_text.Text != string.Empty && filename_text.Text != "Enter file name " && !filename_text.Text.Contains('"'))
                        {
                            var json = JsonSerializer.Serialize(Persons);
                            File.WriteAllText($"{filename_text.Text}.json", json);
                            MessageBox.Show("Saved", "Application", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                            MessageBox.Show("Please enter file name correctly", "Application", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                        MessageBox.Show("Your list is empty", "Application", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            private void filename_text_MouseEnter(object sender, MouseEventArgs e)
            {
                if (filename_text.Text == "Enter file name..")
                {
                    filename_text.Text = string.Empty;
                    filename_text.Foreground = new SolidColorBrush(Colors.Black);
                }
            }

            private void filename_text_MouseLeave(object sender, MouseEventArgs e)
            {
                if (filename_text.Text == string.Empty)
                {
                    filename_text.Foreground = new SolidColorBrush(Colors.Gray);
                    filename_text.Text = "Enter file name..";
                }
            }
        }
    }

