using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    class Loader
    {
        public Loader() {
            
        }

        public List<Tuple<Numpy.NDarray, Numpy.NDarray>> load_data(string file) {
            Numpy.NDarray data = Numpy.np.load(file, allow_pickle: true);
            Numpy.NDarray test_data = data[2];
             

            List<Numpy.NDarray> list = new List<Numpy.NDarray>();
            for (int i=0; i< 10000; i++) {
                list.Add(Numpy.np.reshape(test_data[0][i], new int[] {784, 1}));
            }
            Console.WriteLine(list.Count);
            Console.WriteLine(test_data[1][0]);

            List<Tuple<Numpy.NDarray, Numpy.NDarray>> formatted = zip(list, test_data[1]);

            return formatted;
        }


        private List<Tuple<Numpy.NDarray, Numpy.NDarray>> zip(List<Numpy.NDarray> darrays, Numpy.NDarray ted1) {

            List<Tuple<Numpy.NDarray, Numpy.NDarray>> data = new List<Tuple<Numpy.NDarray, Numpy.NDarray>>();           
            for (int i = 0; i < 10000; i++) {
                data.Add(new Tuple<Numpy.NDarray, Numpy.NDarray>(darrays[i], ted1[i]));
            }
            return data;
        }

       
    }
}
