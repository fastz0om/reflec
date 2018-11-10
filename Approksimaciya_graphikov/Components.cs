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
        public List<int> coordinates;
        private string info = "";

        public void setCoordinates(int[] mas){
            this.coordinates = mas.ToList();
        }

        public void setCoordinates(List<int> list)
        {
            this.coordinates = list;
        }

        public void setInfo(string str)
        {
            this.info = str;
        }

    }
}
