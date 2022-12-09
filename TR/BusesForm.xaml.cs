using Aspose.Words;
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

namespace TR
{
    /// <summary>
    /// Логика взаимодействия для BusesForm.xaml
    /// </summary>
    public partial class BusesForm : Window
    {
        public BusesForm()
        {
            InitializeComponent();
            using (var db = new transportrouteContext())
            {
                List<Bus> busList = db.Buses.ToList();

                busGrid.ItemsSource = busList;
            }
        }

        private void Button_MainWindow(object sender, RoutedEventArgs e)
        {
            MainWindow form = new MainWindow();
            form.Show();
            this.Close();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            using (var db = new transportrouteContext())
            {
                if (textBrand.Text != "" && textNumber.Text != "" && textDriver.Text != "" && textSeats.Text != "")
                {
                    var Bus = new Bus
                    {
                        Brand = textBrand.Text,
                        Number = textNumber.Text,
                        Driver = textDriver.Text,
                        Seats = int.Parse(textSeats.Text)
                    };
                    db.Buses.Add(Bus);
                    db.SaveChanges();
                    List<Bus> busList = db.Buses.ToList();
                    busGrid.ItemsSource = busList;
                }
            }
        }

        private void Button_Change(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var id = button.Tag.ToString();
            using (var db = new transportrouteContext())
            {
                Bus row = busGrid.SelectedItem as Bus;
                var Bus = db.Buses.Find(int.Parse(id));
                Bus.Brand = row.Brand;
                Bus.Number = row.Number;
                Bus.Driver = row.Driver;
                Bus.Seats = row.Seats;
                db.Buses.Update(Bus);
                db.SaveChanges();
                List<Bus> busList = db.Buses.ToList();
                busGrid.ItemsSource = busList;
            }
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var id = button.Tag.ToString();
            using (var db = new transportrouteContext())
            {
                var Bus = db.Buses.Find(int.Parse(id));
                db.Buses.Remove(Bus);
                db.SaveChanges();
                List<Bus> busList = db.Buses.ToList();
                busGrid.ItemsSource = busList;
            }
        }

        private void Button_Report(object sender, RoutedEventArgs e)
        {
            var BusesList = new List<Bus>();
            Document doc = new Document();
            DocumentBuilder builder = new DocumentBuilder(doc);
            builder.Writeln("Дата формирования отчета: " + DateTime.Now.ToString());
            using (var db = new transportrouteContext())
            {
                BusesList = db.Buses.ToList();
            }
            builder.Font.Bold = true;
            builder.Font.Size = 20;
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            builder.Writeln("Автобусы");
            builder.Writeln("");
            builder.Font.Size = 14;
            builder.StartTable();
            builder.InsertCell();
            builder.Write("ID");
            builder.InsertCell();
            builder.Write("Марка автобуса");
            builder.InsertCell();
            builder.Write("Номер");
            builder.InsertCell();
            builder.Write("Водитель");
            builder.InsertCell();
            builder.Write("Число мест");
            builder.EndRow();
            builder.Font.Bold = false;
            foreach (var Bus in BusesList)
            {
                builder.InsertCell();
                builder.Write(Bus.Id.ToString());
                builder.InsertCell();
                builder.Write(Bus.Brand.ToString());
                builder.InsertCell();
                builder.Write(Bus.Number.ToString());
                builder.InsertCell();
                builder.Write(Bus.Driver.ToString());
                builder.InsertCell();
                builder.Write(Bus.Seats.ToString());
                builder.EndRow();
            }
            builder.EndTable();
            doc.Save("C:\\reports\\BusReport " + DateTime.Today.ToString("d") + ".docx");
        }
    }
}
