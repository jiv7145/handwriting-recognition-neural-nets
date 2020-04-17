using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Numpy;

namespace WindowsFormsApp3
{
    class Network
    {
        int num_layers;
        int[] sizes;
        List<NDarray> biases;
        List<NDarray> weights;
        int n; // learning rate?

        public Network(int[] sizes)
        {
            biases = new List<NDarray>();
            weights = new List<NDarray>();
            this.sizes = sizes;
            num_layers = sizes.Length;

            for (int i = 1; i < sizes.Length; i++)
            {
                biases.Add(np.random.randn(new int[] { sizes[i], 1 }));
                weights.Add(np.random.randn(new int[] { sizes[i - 1], sizes[i] }));
            }
            //Console.WriteLine(weights);
        }

        public void SGD(List<Tuple<NDarray, NDarray>> training_data, int v1, int v2, int v3, double v4, List<Tuple<NDarray, NDarray>> test_data)
        {
            throw new NotImplementedException();
        }
    }
}
