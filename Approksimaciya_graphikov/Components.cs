using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Approksimaciya_graphikov
{
    [Serializable]
    public class Component
    {
        public List<double> coordinatesY;
        public List<double> coordinatesX;
        public string info = "";
        public string currentModValue = "Немодифицированный";
        public bool isModified = false;
        public double startFreq;
        public double stopFreq;
        public double amplitude;
        public double sweepFreq;

        public void setCoordinates(double[] mas)
        {
            this.coordinatesY = mas.ToList();
        }
        public void setCoordinates(double[] masX, double[] masY)
        {
            this.coordinatesY = masY.ToList();
            this.coordinatesX = masX.ToList();
        }
        public void setCoordinates(List<double> list)
        {
            this.coordinatesY = list;
        }

        public void setInfo(string str)
        {
            this.info = str;
        }

        public void setModified(bool mod)
        {
            this.isModified = mod;
            if (mod)
                this.currentModValue = "Модифицированный";
        }

        public bool getMod()
        {
            return isModified;
        }

        public string getCurrentModValue()
        {
            return currentModValue;
        }

        public void setStartFreq(double startFreq)
        {
            this.startFreq = startFreq;
        }

        public void setStopFreq(double stopFreq)
        {
            this.stopFreq = stopFreq;
        }
        public void setAmplitude(double amplitude)
        {
            this.amplitude = amplitude;
        }

        public double getStartFreq()
        {
            return startFreq;
        }

        public double getStopFreq()
        {
            return stopFreq;
        }

        public double getAmplitude()
        {
            return amplitude;
        }


        public void  freqSweep (){
            double freqStep = (stopFreq - startFreq)/250;
            double maxYCoord = coordinatesY.Max();
            int i = coordinatesY.IndexOf(maxYCoord);
            this.sweepFreq = startFreq + freqStep * i;
        }

        public double getFreqSweep()
        {
            return sweepFreq;
        }
    }
}
