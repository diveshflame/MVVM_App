using LiveCharts.Wpf;
using LiveCharts;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using MVVM_App.Models;
using System.Data;
using NpgsqlTypes;
using System.Windows;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace MVVM_App.ViewModels
{
    public class chartViewModel : INotifyPropertyChanged
    {
        public ICommand showPieChart { get; }


        public SeriesCollection DataPoints { get; set; }

        public chartViewModel()
        {
            LoadData();
        }

        private void LoadData()
        {
            List<UserModel> dbData = GetDataFromDatabase(); // Implement this method

            try
            {
                DataPoints = new SeriesCollection();

                foreach (var dataPoint in dbData)
                {
                    var pieSeries = new PieSeries
                    {
                        Title = dataPoint.ConsultantType,
                        Values = new ChartValues<double> { dataPoint.DoctorCount },
                        DataLabels = true,
                        LabelPoint = point => $"{point.Y} ({dataPoint.ConsultantType})"
                    };

                    // Assign different colors based on the consultant type
                    pieSeries.Fill = new SolidColorBrush(GetColorForConsultantType(dataPoint.ConsultantType));

                    DataPoints.Add(pieSeries);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        // Implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<UserModel> GetDataFromDatabase()
        {
            List<UserModel> data = new List<UserModel>();

            using (NpgsqlConnection connection = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=pass;Database=postgres"))
            {
                connection.Open();

                string query = @"SELECT c.consultant_desc, COUNT(d.doctor_name) AS doctor_count " +
                    "FROM consultant_type c INNER JOIN doctor_table d ON c.consultant_id = d.consultant_id" +
                    " GROUP BY c.consultant_desc ORDER BY c.consultant_desc";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string consultantType = reader.GetString(0);
                        int doctorCount = reader.GetInt32(1);
                        data.Add(new UserModel { ConsultantType = consultantType, DoctorCount = doctorCount });
                    }
                }
            }

            return data;
        }
        private Color GetColorForConsultantType(string consultantType)
        {
            // Define a dictionary to map consultant types to colors
            Dictionary<string, Color> colorMapping = new Dictionary<string, Color>
    {
        { "Dental", Colors.Blue },
        { "ENT", Colors.Green },
        { "Type C", Colors.Red },
        // Add more consultant types and colors as needed
    };

            // Default color if not found in the dictionary
            Color defaultColor = Colors.Gray;

            // Try to get the color from the dictionary, use default if not found
            if (colorMapping.TryGetValue(consultantType, out Color color))
            {
                return color;
            }

            return defaultColor;
        }


    }


}

