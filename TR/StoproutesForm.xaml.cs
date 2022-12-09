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
    /// Логика взаимодействия для StoproutesForm.xaml
    /// </summary>
    public partial class StoproutesForm : Window
    {
        public StoproutesForm()
        {
            InitializeComponent();
            using (var db = new transportrouteContext())
            {
                List<Stoproute> stoprouteList = db.Stoproutes.ToList();

                stoprouteGrid.ItemsSource = stoprouteList;
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
                if (textNameStop.Text != "" && textRouteid.Text != "")
                {
                    var Stoproute = new Stoproute
                    {
                        NameStop = textNameStop.Text,
                        Routeid = int.Parse(textRouteid.Text)
                    };
                    db.Stoproutes.Add(Stoproute);
                    db.SaveChanges();
                    List<Stoproute> stoprouteList = db.Stoproutes.ToList();
                    stoprouteGrid.ItemsSource = stoprouteList;
                }
            }
        }

        private void Button_Change(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var id = button.Tag.ToString();
            using (var db = new transportrouteContext())
            {
                Stoproute row = stoprouteGrid.SelectedItem as Stoproute;
                var Stoproute = db.Stoproutes.Find(int.Parse(id));
                Stoproute.NameStop = row.NameStop;
                Stoproute.Routeid = row.Routeid;
                db.Stoproutes.Update(Stoproute);
                db.SaveChanges();
                List<Stoproute> stoprouteList = db.Stoproutes.ToList();
                stoprouteGrid.ItemsSource = stoprouteList;
            }
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var id = button.Tag.ToString();
            using (var db = new transportrouteContext())
            {
                var Stoproute = db.Stoproutes.Find(int.Parse(id));
                db.Stoproutes.Remove(Stoproute);
                db.SaveChanges();
                List<Stoproute> stoprouteList = db.Stoproutes.ToList();
                stoprouteGrid.ItemsSource = stoprouteList;
            }
        }

        private void Button_Report(object sender, RoutedEventArgs e)
        {
            var StoproutesList = new List<Stoproute>();
            Document doc = new Document();
            DocumentBuilder builder = new DocumentBuilder(doc);
            builder.Writeln("Дата формирования отчета: " + DateTime.Now.ToString());
            using (var db = new transportrouteContext())
            {
                StoproutesList = db.Stoproutes.ToList();
            }
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
            doc.Save("C:\\reports\\StoprouteReport " + DateTime.Today.ToString("d") + ".docx");
            MessageBox.Show("Отчет сохранен по пути: C:\\reports\\");
        }
    }
}
