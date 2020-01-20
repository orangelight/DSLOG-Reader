using System;
using System.Collections.Generic;
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
                throw new IndexOutOfRangeException();
            }
            return PdpValues[i];
        }
    }
}
