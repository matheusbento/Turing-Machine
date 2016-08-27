using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringMachine
{
    class abrirArquivo
    {
        public List<String> abrir(String lugar) {
            string filePath = @lugar;
            string line;
            List<String> arquivo = new List<String>();

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        arquivo.Add(line);
                    }
                }
            }
            return arquivo;
        }
    }
}
