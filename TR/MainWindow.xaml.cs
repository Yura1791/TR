using Aspose.Words;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
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

namespace TR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnRuns_Click(object sender, RoutedEventArgs e)
        {
            RunsForm form = new RunsForm();
            form.Show();
            this.Close();
        }

        private void btnRoutes_Click(object sender, RoutedEventArgs e)
        {
            RoutesForm form = new RoutesForm();
            form.Show();
            this.Close();
        }

        private void btnStoproutes_Click(object sender, RoutedEventArgs e)
        {
            StoproutesForm form = new StoproutesForm();
            form.Show();
            this.Close();
        }

        private void btnBuses_Click(object sender, RoutedEventArgs e)
        {
            BusesForm form = new BusesForm();
            form.Show();
            this.Close();
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            var RunsList = new List<Runs>();
            var RoutesList = new List<Route>();
            var StoproutesList = new List<Stoproute>();
            var BusesList = new List<Bus>();
            Document doc = new Document();
            DocumentBuilder builder = new DocumentBuilder(doc);
            builder.Writeln("Дата формирования отчета: " + DateTime.Now.ToString());
            using (var db = new transportrouteContext())
            {
                RunsList = db.Runs.ToList();
                StoproutesList = db.Stoproutes.ToList();
                RoutesList = db.Routes.ToList();
                BusesList = db.Buses.ToList();
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
            builder.Writeln("");
            builder.Font.Bold = true;
            builder.Font.Size = 20;
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            builder.Writeln("Маршруты");
            builder.Writeln("");
            builder.Font.Size = 14;
            builder.StartTable();
            builder.InsertCell();
            builder.Write("ID");
            builder.InsertCell();
            builder.Write("Пункт отправления");
            builder.InsertCell();
            builder.Write("Пункт назначения");
            builder.InsertCell();
            builder.Write("Время в пути ч:м");
            builder.EndRow();
            builder.Font.Bold = false;
            foreach (var Route in RoutesList)
            {
                builder.InsertCell();
                builder.Write(Route.Id.ToString());
                builder.InsertCell();
                builder.Write(Route.RouteStart.ToString());
                builder.InsertCell();
                builder.Write(Route.RouteEnd.ToString());
                builder.InsertCell();
                builder.Write(Route.TravelTime.ToString());
                builder.EndRow();
            }
            builder.EndTable();
            builder.Writeln("");
            builder.Font.Bold = true;
            builder.Font.Size = 20;
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            builder.Writeln("Остановки");
            builder.Writeln("");
            builder.Font.Size = 14;
            builder.StartTable();
            builder.InsertCell();
            builder.Write("ID");
            builder.InsertCell();
            builder.Write("Назваие");
            builder.InsertCell();
            builder.Write("Маршрут");
            builder.EndRow();
            builder.Font.Bold = false;
            foreach (var Stoproute in StoproutesList)
            {
                builder.InsertCell();
                builder.Write(Stoproute.Id.ToString());
                builder.InsertCell();
                builder.Write(Stoproute.NameStop.ToString());
                builder.InsertCell();
                builder.Write(Stoproute.Routeid.ToString());
                builder.EndRow();
            }
            builder.EndTable();
            builder.Writeln("");
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

            doc.Save("C:\\reports\\TR-Report " + DateTime.Today.ToString("d") + ".docx");
        }
    }
}
