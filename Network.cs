﻿using System;
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

        Form1 form;

        public Network(int[] sizes, Form1 form1)
        {
            form = form1;
            biases = new List<NDarray>();
            weights = new List<NDarray>();
            this.sizes = sizes;
            num_layers = sizes.Length;
            for (int i = 1; i < sizes.Length; i++)
            {
                biases.Add(np.random.randn(new int[] { sizes[i], 1 }));              
            }

            for (int x = 0, y = 1; y < num_layers; x++, y++){
                weights.Add(np.random.randn(sizes[y], sizes[x]));
            }
         
        }

        public string evaluate(double[] rgbDoubles) {
            NDarray test_data = np.array(rgbDoubles);
            test_data = np.reshape(test_data, new int[] { 784, 1 });
            NDarray test_results = feedforward(test_data);
            var x = np.argmax(test_results);
            return x.str;
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
                    //System.Windows.Forms.MessageBox.Show($"Epoch {j}: {evaluate(test_data)} / {test_data.Count} complete");
                    string output = $"Epoch {j}: {evaluate(test_data)} / {test_data.Count}";
                    form.updateTextBox(output);

                    Console.WriteLine("Epoch {0}: {1} / {2} complete", j, evaluate(test_data), test_data.Count);
                }
                else {
                    Console.WriteLine("Epoch {0} complete", j);
                }

            }

            form.updateTextBox("Learning Complete");
            form.setLearned();

        }
        private NDarray feedforward(NDarray a)
        {
            for (int i = 0; i < weights.Count; i++)
            {
                NDarray b = biases[i];
                NDarray w = weights[i];
                a = sigmoid(np.dot(w, a) + b);
            }

            return a;
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
                if (np.array_equal(test_results[i][0], test_results[i][1])) {                   
                    sum++;
                }              
            }
            return sum;
        }

        private (List<NDarray>, List<NDarray>) backprop(NDarray x, NDarray y) {
            List<NDarray> nabla_b = new List<NDarray>();
            List<NDarray> nabla_w = new List<NDarray>();

            for (int i = 0; i < biases.Count; i++) { // bias and weight count should be the same
                nabla_b.Add(np.zeros(biases[i].shape));
                nabla_w.Add(np.zeros(weights[i].shape));
            }
                      
            NDarray activation = x;
            List<NDarray> activations = new List<NDarray>();
            activation = np.reshape(activation, new int[] { 784, 1 });
            activations.Add(x);
            List<NDarray> zs = new List<NDarray>();

            for (int i = 0; i < weights.Count; i++) {
                NDarray b = biases[i];
                NDarray w = weights[i];
                NDarray z = np.dot(w, activation) + b;        
                zs.Add(z);
                activation = sigmoid(z);
                activations.Add(activation);
            }

            y = np.reshape(y, new int[] { 10, 1 });      
            NDarray delta = cost_derivative(activations[activations.Count-1], y) * sigmoidPrime(zs[zs.Count-1]);
            nabla_b[nabla_b.Count-1] = delta;         
            nabla_w[nabla_w.Count-1] = np.dot(delta, np.transpose(activations[activations.Count-2]));

            for (int l = 2; l < num_layers; l++) {
                NDarray z = zs[zs.Count-l];
                NDarray sp = sigmoidPrime(z);
                delta = np.dot(np.transpose(weights[weights.Count - l + 1]), delta) * sp;
                nabla_b[nabla_b.Count-l] = delta;
                nabla_w[nabla_w.Count-l] = np.dot(delta, np.transpose(np.reshape(activations[activations.Count - l - 1], new int[] { 784, 1 })));
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
            return (output_activations - y);
        }

        private void update_mini_batch(List<NDarray> mini_batch, double eta) {
            List<NDarray> nabla_b = new List<NDarray>();
           
            for (int i = 0; i < biases.Count; i++) {
                nabla_b.Add(np.zeros(biases[i].shape));
            }

            List<NDarray> nabla_w = new List<NDarray>();
            for (int i = 0; i < weights.Count; i++) {
                nabla_w.Add(np.zeros(weights[i].shape));
            }

            for (int i = 0; i < mini_batch.Count; i++) {              
                (List<NDarray> delta_nabla_b, List<NDarray> delta_nabla_w) = backprop(mini_batch[i][0], mini_batch[i][1]);
                for (int j = 0; j < delta_nabla_b.Count; j++) {                   
                    nabla_b[j] = nabla_b[j] + delta_nabla_b[j];
                }
                for (int j = 0; j < delta_nabla_w.Count; j++) {
                    nabla_w[j] = nabla_w[j] + delta_nabla_w[j];
                }
            }

            for (int i = 0; i < weights.Count; i++) {
                weights[i]= weights[i] - (eta / mini_batch.Count)*nabla_w[i];
            }
            
            for (int i = 0; i < biases.Count; i++) {
                biases[i] = biases[i] - (eta / mini_batch.Count) * nabla_b[i];
            }
        }

        private List<NDarray> ndarrayRange(List<NDarray> training_data, int rangeFrom, int rangeTo) {
            
            List<NDarray> mini_batch = new List<NDarray>();        
            for (int i = rangeFrom; i < rangeTo; i++) {
                mini_batch.Add(training_data[i]);           
            }
            
            return mini_batch;
        }
    }
}
