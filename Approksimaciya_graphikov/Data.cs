using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Approksimaciya_graphikov
{
    [Serializable]
    class Data
    {
        public List<Component> data = new List<Component>();

        public void addComponent(Component component)
        {
            this.data.Add(component);
        }
    }
}
