using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Numpy;

namespace WindowsFormsApp3
{
    class Loader
    {
        public Loader() {
            
        }

        public List<List<Tuple<NDarray, NDarray>>> load_data(string file) {
            NDarray data = np.load(file, allow_pickle: true);
            NDarray tr_d = data[0];
            NDarray va_d = data[1];
            NDarray te_d = data[2];

            List<NDarray> training_inputs = new List<Numpy.NDarray>();
            for (int i = 0; i < 50000; i++) { // 39200000/784= 50000
                training_inputs.Add(np.reshape(tr_d[0][i], new int[] { 784, 1 }));
            }
            List<NDarray> training_results = new List<NDarray>();
            //Console.WriteLine(tr_d[1].size);
            for (int i = 0; i < tr_d[1].size; i++) {
                training_results.Add(vectorized_result(tr_d[1][i]));
            }
            List<Tuple<NDarray, NDarray>> training_data = zip(training_inputs, training_results);
            //Console.WriteLine(te_d[0].size);
            //Console.WriteLine(va_d[0].size);
            List<NDarray> validation_inputs = new List<NDarray>();
            for (int i = 0; i < 10000; i++) {
                validation_inputs.Add(np.reshape(va_d[0][i], new int[] {784, 1}));
            }
            List<Tuple<NDarray, NDarray>> validation_data = zip(validation_inputs, va_d[1]);

            List<NDarray> test_inputs = new List<NDarray>();
            for (int i=0; i< 10000; i++) { //78400000/784 = 10000
                test_inputs.Add(np.reshape(te_d[0][i], new int[] {784, 1}));
            }
            List<Tuple<NDarray, NDarray>> test_data = zip(test_inputs, te_d[1]);

            List<List<Tuple<NDarray, NDarray>>> output = new List<List<Tuple<NDarray, NDarray>>>();
            output.Add(training_data);
            output.Add(validation_data);
            output.Add(test_data);

            return output;
        }


        private List<Tuple<NDarray, NDarray>> zip(List<NDarray> darrays, NDarray ted1) {

            List<Tuple<NDarray, NDarray>> data = new List<Tuple<NDarray, NDarray>>();           
            for (int i = 0; i < darrays.Count; i++) {
                data.Add(new Tuple<NDarray, NDarray>(darrays[i], ted1[i]));
            }
            return data;
        }

        private List<Tuple<NDarray, NDarray>> zip(List<NDarray> darrays, List<NDarray> ted1) 
        {
            Console.WriteLine(darrays.Count);
            Console.WriteLine(ted1.Count);
            List<Tuple<NDarray, NDarray>> data = new List<Tuple<NDarray, NDarray>>();
            for (int i = 0; i < darrays.Count; i++)
            {
                data.Add(new Tuple<NDarray, NDarray>(darrays[i], ted1[i]));
            }
            return data;
        }


        private NDarray vectorized_result(NDarray darrays) {
            NDarray zz = np.zeros(new int[] {10, 1});
            zz[darrays] = np.ones(1);
            return zz;
        }
       
    }
}
