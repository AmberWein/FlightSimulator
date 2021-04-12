using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Models;

namespace FlightSimulator
{
    public class Data
    {
        private IFlightSimulatorModel model;
        public Data(IFlightSimulatorModel m)

        {
            this.model = m;
        }
            public List<Measurement> GetData()
        {
            var measurements = new List<Measurement>();

            var startDate = DateTime.Now.AddMinutes(-10);
           

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (model.DataMap == null)
                    {

                    }
                    else
                    {
                        measurements.Add(new Measurement() { DetectorId = i, DateTime = startDate.AddMinutes(j), Value = float.Parse(model.DataMap["engine_rpm"][j].ToString()) });
                    }
                }
            }
            measurements.Sort((m1, m2) => m1.DateTime.CompareTo(m2.DateTime));
            return measurements;
        }

        public List<Measurement> GetUpdateData(DateTime dateTime)
        {
            var measurements = new List<Measurement>();
            var r = new Random();
            if (model.DataMap == null)
            {

            }
            else
            {

              //  for (int i = 0; i < 5; i++)
                //{
                    measurements.Add(new Measurement() { DetectorId = 2, DateTime = dateTime.AddSeconds(1), Value = float.Parse(model.DataMap["engine_rpm"][10* (int)model.Timer].ToString()) });
                //}
            }
            return measurements;
        }
    }

    public class Measurement
    {
        public int DetectorId { get; set; }
        public float Value { get; set; }
        public DateTime DateTime { get; set; }
    }
}
