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
            NDarray b = np.empty();
            b = np.append(b, np.array(new int[] { 1, 2 }));
            Console.WriteLine(b.shape);
            biases = new List<NDarray>();
            weights = new List<NDarray>();
            this.sizes = sizes;
            num_layers = sizes.Length;
            for (int i = 1; i < sizes.Length; i++)
            {
                biases.Add(np.random.randn(new int[] { sizes[i], 1 }));
                Console.WriteLine(sizes[i - 1] + " " + sizes[i]);               
            }

            for (int x = 0, y = 1; y < num_layers; x++, y++){
                weights.Add(np.random.randn(sizes[y], sizes[x]));
            }
          
        }

        private NDarray feedforward(NDarray a) { // a single value NDarray?

            
            for (int i = 0; i < weights.Count; i++) {
                NDarray b = biases[i];
                NDarray w = weights[i];
                a = sigmoid(np.dot(w, a) + b);
            }

            return a;
        }

        //training_data and test_data contains tuples of [ndarray, ndarray]
        public void SGD(List<NDarray> training_data, int epochs, int mini_batch_size, double eta, List<NDarray> test_data = null)
        {
            int n_test;
            n = training_data.Count;      
            
            if (test_data != null) {
                n_test = test_data.Count;
            }

            List<List<NDarray>> mini_batches = new List<List<NDarray>>();
            for (int j = 0; j < epochs; j++) {
                np.random.shuffle(np.array(training_data));
                for (int k = 0; k < n; k += mini_batch_size) {
                    mini_batches.Add(ndarrayRange(training_data, k, k + mini_batch_size));
                }
             
            for (int k = 0; k < mini_batches.Count; k++) {
                    update_mini_batch(mini_batches[k], eta);
                }

                if (test_data != null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Epoch {0}: {1} / {2} complete", j, evaluate(test_data), test_data.Count);

                }
                else {
                    Console.WriteLine("Epoch {0} complete", j);

                }

            }

        }

        private double evaluate(List<NDarray> test_data) {
            List<NDarray> test_results = new List<NDarray>();
            
            for(int i=0; i<test_data.Count; i++) {
                List<NDarray> temp = new List<NDarray>();
                temp.Add(np.argmax(feedforward(test_data[i][0])));
                temp.Add(test_data[i][1]);
                test_results.Add(np.array(temp));             
            }



            double sum = 0;
            for (int i = 0; i < test_results.Count; i++) {
                sum += test_results[i][0] == test_results[i][1] ? 1 : 0;
            }
            return sum;
        }

        private (List<NDarray>, List<NDarray>) backprop(NDarray x, NDarray y) {

            List<NDarray> nabla_b = new List<NDarray>();
            List<NDarray> nabla_w = new List<NDarray>();

            for (int i = 0; i < biases.Count; i++) {
                nabla_b.Add(np.zeros(biases[i].shape));
            }

            for (int i = 0; i < weights.Count; i++)
            {
                nabla_w.Add(np.zeros(weights[i].shape));
            }

           
            NDarray activation = x;
            //activation = np.reshape(activation, new int[] {784, 1});
            List<NDarray> activations = new List<NDarray>();
            activations.Add(x);
            List<NDarray> zs = new List<NDarray>();
           

            //Console.WriteLine(biases[0].shape);
            Console.WriteLine(weights[0].shape);

            for (int i = 0; i < weights.Count; i++) {
                NDarray b = biases[i];
                NDarray w = weights[i];
                NDarray z = np.dot(w, activation) + b; 
                zs.Add(z);
                activation = sigmoid(z);
                activations.Add(activation);
            }

            NDarray delta = cost_derivative(activations[activations.Count-1], y) * sigmoidPrime(zs[zs.Count-1]); // this works?

            nabla_b[nabla_b.Count-1] = delta;
            nabla_w[nabla_w.Count-1] = np.dot(delta, np.transpose(activations[activations.Count-2]));

            for (int l = 2; l < num_layers; l++) {
                NDarray z = zs[zs.Count-l];
                NDarray sp = sigmoidPrime(z);
                delta = np.dot(weights[weights.Count - l + 1].transpose(), delta) * sp;
                nabla_b[nabla_b.Count-l] = delta;
                nabla_w[nabla_w.Count-l] = np.dot(delta, np.transpose(activations[activations.Count-l + 1]));
            }
            return (nabla_b, nabla_w);
        }

        private NDarray sigmoid(NDarray z) {
            return 1.0 / (1.0 + np.exp(-z));
        }

        private NDarray sigmoidPrime(NDarray z) {
            return sigmoid(z) * ( -sigmoid(z) + 1.0);
        }

        private NDarray cost_derivative(NDarray output_activations, NDarray y) {
            //y = np.reshape(y, output_activations.shape);
            return (output_activations - y);
        }

        private void update_mini_batch(List<NDarray> mini_batch, double eta) {

            //nabla_b = [np.zeros(b.shape) for b in self.biases]
            List<NDarray> nabla_b = new List<NDarray>();
           
            for (int i = 0; i < biases.Count; i++) {
                nabla_b.Add(np.zeros(biases[i].shape));
            }

            //nabla_w = [np.zeros(w.shape) for w in self.weights]
            List<NDarray> nabla_w = new List<NDarray>();
            for (int i = 0; i < weights.Count; i++) {
                nabla_w.Add(np.zeros(weights[i].shape));
            }



            for (int i = 0; i < mini_batch.Count; i++) {              
                (List<NDarray> delta_nabla_b, List<NDarray> delta_nabla_w) = backprop(mini_batch[i][0], mini_batch[i][1]);
                List<NDarray> zip_delta_b = Util.zip(nabla_b, delta_nabla_b);
                for (int j = 0; j < zip_delta_b.Count; j++) {
                    nabla_b[j] = zip_delta_b[j][0] + zip_delta_b[j][1];
                }
                List<NDarray> zip_delta_w = Util.zip(nabla_w, delta_nabla_w);
                for (int j = 0; j < zip_delta_w.Count; j++) {
                    nabla_w[j] = zip_delta_w[j][0] + zip_delta_w[j][1];
                }
            }

            //self.weights = [w-(eta/len(mini_batch))*nw
            //                    for w, nw in zip(self.weights, nabla_w)]
            List<NDarray> temp = Util.zip(weights, nabla_w);
            for (int i = 0; i < temp.Count; i++) {
                weights[i]= temp[i][0] - (eta / mini_batch.Count)*temp[i][1];
            }

            List<NDarray> temp2 = Util.zip(biases, nabla_b);

            for (int i = 0; i < temp2.Count; i++) {
                biases[i] = temp2[i][0] - (eta / mini_batch.Count) * temp2[i][1];
            }
        }

        private List<NDarray> ndarrayRange(List<NDarray> training_data, int rangeFrom, int rangeTo) {
            
            List<NDarray> mini_batch = new List<NDarray>();        
            for (int i = rangeFrom; i < rangeTo; i++) {
                mini_batch.Add(training_data[i]);  //  pass in tuple            
            }
            
            return mini_batch;
        }
    }
}
