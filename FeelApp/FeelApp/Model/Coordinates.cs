using System;
using System.Collections.Generic;
using System.Text;

namespace FeelApp.Model
{
    public class Coordinates
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Date { get; set; }
    }
}
