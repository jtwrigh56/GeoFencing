/*Authors: James Wright
 *Program: GeoFencing
 *Details: This program will allow you to set a poly of your choicing and will compare with a single point to see if that point is in the polygon.
 * the second half of this program validates whether a point is within the radius of a set point. 
 * 
 * Date: 7/7/2018
 *
 * */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeoFencing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool inside = false;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //Validation for the polygon. 
        private void button1_Click(object sender, EventArgs e)
        {
            inside = false;

            string[] GPSarr = UserGpsTextbox.Text.Split(new[] { ',', ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            double Lat = double.Parse(GPSarr[0]);
            double Lon = double.Parse(GPSarr[1]);

            Point GPSPoint = new Point(Lat, Lon);

            MessageBox.Show("Coordinates are: " + Lat + " " + Lon);

            string[] Polyarr = polytextbox.Text.Split(new[] { ',', ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            double PLat1 = double.Parse(Polyarr[0]);
            double PLon1 = double.Parse(Polyarr[1]);
            double PLat2 = double.Parse(Polyarr[2]);
            double PLon2 = double.Parse(Polyarr[3]);
            double PLat3 = double.Parse(Polyarr[4]);
            double PLon3 = double.Parse(Polyarr[5]);
            double PLat4 = double.Parse(Polyarr[6]);
            double PLon4 = double.Parse(Polyarr[7]);
           

            var polygon = new List<Point> {
            new Point(PLat1, PLon1),
            new Point(PLat2, PLon2),
            new Point(PLat3, PLon3),
            new Point(PLat4, PLon4)
            };

            

            PolyContainsPoint(polygon, GPSPoint);

            if (inside == true)
            {
                MessageBox.Show("Point is inside the polygon");
            }
            else
            {
                MessageBox.Show("Point is not inside the polygon");
            };
        }

        public bool PolyContainsPoint(List<Point> points, Point p)
        {
            Point v1 = points[points.Count - 1];

            foreach (Point v0 in points)
            {
                double d1 = (p.Y - v0.Y) * (v1.X - v0.X);
                double d2 = (p.X - v0.X) * (v1.Y - v0.Y);

                if (p.Y < v1.Y)
                {
                    if (v0.Y <= p.Y)
                    {
                        if (d1 > d2)
                        {
                            inside = !inside;
                        }
                    }
                }
                else if (p.Y < v0.Y)
                {
                    if (d1 < d2)
                    {
                        inside = !inside;
                    }
                }

                v1 = v0;
            }

            return inside;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] GPSarr = UserGpsTextbox.Text.Split(new[] { ',', ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            double lat = double.Parse(GPSarr[0]);
            double lon = double.Parse(GPSarr[1]);

            string[] POI = PointOfInterest.Text.Split(new[] { ',', ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            double plat = double.Parse(POI[0]);
            double plon = double.Parse(POI[1]);

            double radius = double.Parse(textBox4.Text);

            double w = (radius / 5280);

            double distance = GetDistance(lat, lon, plat, plon);



            checkRadius(distance, w);
        }

        //Checks whether the points distance is greater than the radius of the point that is set. 
        public void checkRadius(double d, double r)
        {

            if (d < r)
            {
                MessageBox.Show("The Point is inside the Radius!");
            }
            else
            {
                MessageBox.Show("The Point is outside of the Radius!");
            }


        }


        //Converts the Lat, and Lon to radians so the distance can be properly calculated. 
        double GetDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 3958.756; // Radius of the earth in miles
            var dLat = ToRadians(lat2 - lat1);  // deg2rad below
            var dLon = ToRadians(lon2 - lon1);
            var a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c; // Distance in miles
            return d;
        }

        //conversion to Radians
        double ToRadians(double deg)
        {
            return deg * (Math.PI / 180);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }


    public struct Point
    {
        public double X;
        public double Y;

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }




}
