using Aspose.Words;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;

namespace TR
{
    /// <summary>
    /// Логика взаимодействия для RunsForm.xaml
    /// </summary>
    public partial class RunsForm : Window
    {
        public RunsForm()
        {
            InitializeComponent();
            using (var db = new transportrouteContext())
            {
                List<Runs> runList = db.Runs.ToList();

                runGrid.ItemsSource = runList;
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
                if (textRunStart.Text != "" && textRunEnd.Text != "" && textRouteid.Text != "" && textBusid.Text != "" && textTicketPrice.Text != "" && textDate.Text != "")
                {
                    var Run = new Runs
                    {
                        RunStart = TimeOnly.Parse(textRunStart.Text),
                        RunEnd = TimeOnly.Parse(textRunEnd.Text),
                        Routeid = int.Parse(textRouteid.Text),
                        Busid = int.Parse(textBusid.Text),
                        TicketPrice = int.Parse(textTicketPrice.Text),
                        Date = DateOnly.Parse(textDate.Text)
                    };
                    db.Runs.Add(Run);
                    db.SaveChanges();
                    List<Runs> runList = db.Runs.ToList();
                    runGrid.ItemsSource = runList;
                }
            }
        }

        private void Button_Change(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var id = button.Tag.ToString();
            using (var db = new transportrouteContext())
            {
                Runs row = runGrid.SelectedItem as Runs;
                var Run = db.Runs.Find(int.Parse(id));
                Run.RunStart = row.RunStart;
                Run.RunEnd = row.RunEnd;
                Run.Routeid = row.Routeid;
                Run.Busid = row.Busid;
                Run.TicketPrice = row.TicketPrice;
                Run.Date = row.Date;
                db.Runs.Update(Run);
                db.SaveChanges();
                List<Runs> runList = db.Runs.ToList();
                runGrid.ItemsSource = runList;
            }
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var id = button.Tag.ToString();
            using (var db = new transportrouteContext())
            {
                var Run = db.Runs.Find(int.Parse(id));
                db.Runs.Remove(Run);
                db.SaveChanges();
                List<Runs> runList = db.Runs.ToList();
                runGrid.ItemsSource = runList;
            }
        }

        private void Button_Report(object sender, RoutedEventArgs e)
        {
            var RunsList = new List<Runs>();
            Document doc = new Document();
            DocumentBuilder builder = new DocumentBuilder(doc);
            builder.Writeln("Дата формирования отчета: " + DateTime.Now.ToString());
            using (var db = new transportrouteContext())
            {
                RunsList = db.Runs.ToList();
            }
            builder.Font.Bold = true;
            builder.Font.Size = 20;
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            builder.Writeln("Рейсы");
            builder.Writeln("");
            builder.Font.Size = 14;
            builder.StartTable();
            builder.InsertCell();
            builder.Write("ID");
            builder.InsertCell();
            builder.Write("Время отправления");
            builder.InsertCell();
            builder.Write("Время прибытия");
            builder.InsertCell();
            builder.Write("Маршрут");
            builder.InsertCell();
            builder.Write("Автобус");
            builder.InsertCell();
            builder.Write("Цена билета");
            builder.InsertCell();
            builder.Write("Дата");
            builder.EndRow();
            builder.Font.Bold = false;
            foreach (var Run in RunsList)
            {
                builder.InsertCell();
                builder.Write(Run.Id.ToString());
                builder.InsertCell();
                builder.Write(Run.RunStart.ToString());
                builder.InsertCell();
                builder.Write(Run.RunEnd.ToString());
                builder.InsertCell();
                builder.Write(Run.Routeid.ToString());
                builder.InsertCell();
                builder.Write(Run.Busid.ToString());
                builder.InsertCell();
                builder.Write(Run.TicketPrice.ToString());
                builder.InsertCell();
                builder.Write(Run.Date.ToString());
                builder.EndRow();
            }
            builder.EndTable();
            doc.Save("C:\\reports\\RunsReport " + DateTime.Today.ToString("d") + ".docx");
        }
    }
}
