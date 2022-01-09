using System;
using System.Collections.Generic;

namespace Banks.ConsoleSweeps
{
    public class OutputSubtypes
    {
        public void Output(List<string> outputsElements)
        {
            foreach (string element in outputsElements)
            {
                Console.WriteLine(element);
            }
        }

        public void Output(List<int> outputsElements)
        {
            foreach (int element in outputsElements)
            {
                Console.WriteLine(element);
            }
        }

        public void Output(List<Client> outputsElements)
        {
            foreach (Client element in outputsElements)
            {
                Console.WriteLine($"Имя клиента: {element.GetName()}, Фамилия клиента: {element.GetSurName()}");
            }
        }
    }
}