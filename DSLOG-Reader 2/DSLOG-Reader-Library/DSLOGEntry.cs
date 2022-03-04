using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSLOG_Reader_Library
{
    public class DSLOGEntry
    {
        public readonly double TripTime, LostPackets, Voltage, RoboRioCPU, CANUtil, WifidB, Bandwith, PDPResistance, PDPVoltage, PDPTemperature;
        public readonly bool[] StatusFlags;
        public readonly bool Brownout, Watchdog, DSTele, DSAuto, DSDisabled, RobotTele, RobotAuto, RobotDisabled;
        public readonly int PDPID;
        public readonly double[] PdpValues;
        public readonly DateTime Time;

        public DSLOGEntry(double trip, double packets, double vol, double rrCPU, bool[] flags, double can, double dB, double band, int pdp, double[] pdpV, double res, double volS, double temp, DateTime time)
        {
            TripTime = trip;
            LostPackets = packets;
            Voltage = vol;
            RoboRioCPU = rrCPU;
            StatusFlags = flags;
            Brownout = StatusFlags[0];
            Watchdog = StatusFlags[1];
            DSTele = StatusFlags[2];
            DSAuto = StatusFlags[3];
            DSDisabled = StatusFlags[4];
            RobotTele = StatusFlags[5];
            RobotAuto = StatusFlags[6];
            RobotDisabled = StatusFlags[7];
            CANUtil = can;
            WifidB = dB;
            Bandwith = band;
            PDPID = pdp;
            PdpValues = pdpV;
            PDPResistance = res;
            PDPVoltage = volS;
            PDPTemperature = temp;
            Time = time;
        }

        public double GetPDPChannel(int i)
        {
            if (i >= PdpValues.Length)
            {
                return 0;
            }
            return PdpValues[i];
        }

        public double GetDPDTotal()
        {
            double sum = 0;
            foreach(double value in PdpValues)
            {
                sum += value;
            }
            return sum;
        }

        public double GetGroupPDPTotal(int[] slots)
        {
            double sum = 0;
            foreach(int slot in slots)
            {
                sum += GetPDPChannel(slot);
            }
            return sum;
        }

        public double GetGroupPDPSd(int[] slots)
        {
            List<double> group = new List<double>();
            foreach(int slot in slots)
            {
                group.Add(PdpValues[slot]);
            }
            double avg = group.Average();    
            double sum = group.Sum(d => Math.Pow(d - avg, 2));    
            return Math.Sqrt((sum) / group.Count());
        }
    }
}
