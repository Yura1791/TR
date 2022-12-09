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
    /// Логика взаимодействия для RoutesForm.xaml
    /// </summary>
    public partial class RoutesForm : Window
    {
        public RoutesForm()
        {
            InitializeComponent();
            using (var db = new transportrouteContext())
            {
                List<Route> routeList = db.Routes.ToList();

                routeGrid.ItemsSource = routeList;
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
                if (textRouteStart.Text != "" && textRouteEnd.Text != "" && textTravelTime.Text != "")
                {
                    var Route = new Route
                    {
                        RouteStart = textRouteStart.Text,
                        RouteEnd = textRouteEnd.Text,
                        TravelTime = TimeOnly.Parse(textTravelTime.Text)
                    };
                    db.Routes.Add(Route);
                    db.SaveChanges();
                    List<Route> routeList = db.Routes.ToList();
                    routeGrid.ItemsSource = routeList;
                }
            }
        }

        private void Button_Change(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var id = button.Tag.ToString();
            using (var db = new transportrouteContext())
            {
                Route row = routeGrid.SelectedItem as Route;
                var Route = db.Routes.Find(int.Parse(id));
                Route.RouteStart = row.RouteStart;
                Route.RouteEnd = row.RouteEnd;
                Route.TravelTime = row.TravelTime;
                db.Routes.Update(Route);
                db.SaveChanges();
                List<Route> routeList = db.Routes.ToList();
                routeGrid.ItemsSource = routeList;
            }
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var id = button.Tag.ToString();
            using (var db = new transportrouteContext())
            {
                var Route = db.Routes.Find(int.Parse(id));
                db.Routes.Remove(Route);
                db.SaveChanges();
                List<Route> routeList = db.Routes.ToList();
                routeGrid.ItemsSource = routeList;
            }
        }

        private void Button_Report(object sender, RoutedEventArgs e)
        {
            var RoutesList = new List<Route>();
            Document doc = new Document();
            DocumentBuilder builder = new DocumentBuilder(doc);
            builder.Writeln("Дата формирования отчета: " + DateTime.Now.ToString());
            using (var db = new transportrouteContext())
            {
                RoutesList = db.Routes.ToList();
            }
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
            doc.Save("C:\\reports\\RouteReport " + DateTime.Today.ToString("d") + ".docx");
        }
    }
}
