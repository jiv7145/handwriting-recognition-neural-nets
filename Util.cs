using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Numpy;

namespace WindowsFormsApp3
{
    class Util
    {

        public static List<Tuple<NDarray, NDarray>> zip(List<NDarray> darrays, NDarray ted1)
        {

            List<Tuple<NDarray, NDarray>> data = new List<Tuple<NDarray, NDarray>>();
            for (int i = 0; i < darrays.Count; i++)
            {
                data.Add(new Tuple<NDarray, NDarray>(darrays[i], ted1[i]));
            }
            return data;
        }

        public static List<Tuple<NDarray, NDarray>> zip(List<NDarray> darrays, List<NDarray> ted1)
        {
            //Console.WriteLine(darrays.Count);
            //Console.WriteLine(ted1.Count);
            List<Tuple<NDarray, NDarray>> data = new List<Tuple<NDarray, NDarray>>();
            for (int i = 0; i < darrays.Count; i++)
            {
                data.Add(new Tuple<NDarray, NDarray>(darrays[i], ted1[i]));
            }
            return data;
        }
    }
}
