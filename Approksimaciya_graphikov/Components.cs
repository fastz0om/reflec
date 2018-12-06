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
        private string info = "";

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

    }
}
