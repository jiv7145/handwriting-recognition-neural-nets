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

        public static List<NDarray> zip(List<NDarray> darrays, NDarray ted1)
        {
           
            List<NDarray> data = new List<NDarray>();
            int min = Math.Min(darrays.Count, ted1.size);

            for (int i = 0; i < min; i++)
            {
                List<NDarray> convert = new List<NDarray>();
                convert.Add(darrays[i]);
                convert.Add(ted1[i]);
                NDarray converted = np.array(convert); // convert to ndarray of ndarrays
                data.Add(converted);
            }
            return data;
        }

        public static List<NDarray> zip(List<NDarray> darrays, List<NDarray> ted1)
        {

            List<NDarray> data = new List<NDarray>();
            int min = Math.Min(darrays.Count, ted1.Count);
            int max = Math.Max(darrays.Count, ted1.Count);
         
            
            for (int i = 0; i < min; i++)
            {
                List<NDarray> convert = new List<NDarray>();
               
                convert.Add(np.ravel(darrays[i]));
                convert.Add(np.ravel(ted1[i]));

                NDarray converted = np.array(convert); // convert to ndarray of ndarrays          
                data.Add(converted);

            }
            return data;
        }



    }

}
