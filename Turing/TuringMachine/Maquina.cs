using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringMachine
{
    class Maquina
    {
        Estados inicial = new Estados();
        String inicio = "";
        String branco = "";
        List<Estados> finais = new List<Estados>();
        List<Transicoes> transicoes = new List<Transicoes>();
        List<String> saida = new List<String>();
        Stopwatch sw = new Stopwatch();
        string artout = "";
        bool parada;
        int posFita = 0;
        public bool verificarEstadosFinais(Estados aux) {
            for (int i = 0; i < finais.Count; i++) {
                if (finais[i].estado.Equals(aux.estado)) {
                    return true;
                }
            }
            return false;
        }
        public int retornaEstados(string atual, List<String>fita) {
            int i = 0;
            while (i<transicoes.Count)
            {
                if (transicoes[i].readSymbol.Equals(fita[posFita]) && atual.Equals(transicoes[i].From))
                {
                    return i;
                }
          
                i++;
            }
            return - 1;
        }
        public void fileSaida(List<String> linha) {
            TextWriter escritor = new StreamWriter(artout, true);
            foreach (var linh in linha)
            {
                escritor.WriteLine(linh);
            }
            escritor.Close();
            sw.Stop();
            TimeSpan tempo = sw.Elapsed;
            Console.WriteLine("FEITO EM " + tempo);
        }
        public string inicioMaquina(List<String> fita)
        {
            parada = false;
            int i = -1;
            posFita = 0;
            sw.Start();
            string atual = inicial.estado;
                while (parada == false)
                {
                    i = retornaEstados(atual,fita);
                
                    if (i == -1){
                    string fitaLinha = "";
                    for (int t = 1; t < fita.Count-1;t++ )
                        {
                            fitaLinha = fitaLinha + fita[t];
                        }
                   // Console.WriteLine("FITA LINHA -> " + fitaLinha);
                    parada = true;
                    //sw.Stop();
                    //TimeSpan tempo = sw.Elapsed;
                    //Console.WriteLine("TEMPO -> " + tempo);
                        return fitaLinha + ";0";
                     }
                    else
                    {
                        Estados aux = new Estados();
                        aux.estado = transicoes[i].To;
                        if (fita[posFita] == branco && verificarEstadosFinais(aux) == true)
                        {
                        string fitaLinha = "";
                        for (int y = 1; y < fita.Count - 1; y++)
                        {
                            fitaLinha = fitaLinha + fita[y];
                        }
                       // Console.WriteLine("FITA LINHA -> " + fitaLinha);
                        //sw.Stop();
                        //TimeSpan tempo = sw.Elapsed;


                        //Console.WriteLine("TEMPO -> " + tempo);
                        parada = true;
                        return fitaLinha + ";1";
                    }
                        if (fita[posFita] == inicio && verificarEstadosFinais(aux) == true)
                        {
                        string fitaLinha = "";
                        for (int y = 1; y < fita.Count - 1; y++)
                            {
                                fitaLinha = fitaLinha + fita[y];
                            }
                       // Console.WriteLine("FITA LINHA -> " + fitaLinha);
                        //sw.Stop();
                        //TimeSpan tempo = sw.Elapsed;

                        parada = true;
                        return fitaLinha + ";1";
                    }
                        fita[posFita] = transicoes[i].writeSymbol;
                        atual = transicoes[i].To;
                        if (atual.Equals(""))
                        {
                            parada = true;
                        }
                        if (transicoes[i].direction == "D")
                        {
                            if (fita.Count() == posFita)
                            {
                                fita.Add(branco);
                            }
                            posFita++;
                        }
                        else if (transicoes[i].direction == "E")
                        {
                            if (posFita == 0)
                            {
                                parada = true;
                            }
                            posFita--;
                        }
                    }

                }
            return null;
        }
        public void fileEntrada(string lugar)
        {
            
            abrirArquivo abrir = new abrirArquivo();
            List<String> saida = new List<String>();
            List<String> fita = new List<String>();
            List<String> arquivo = abrir.abrir(lugar);
            for (int y = 0; y < arquivo.Count; y++)
            {
                fita.Clear();
                fita.Add(inicio);
                for (int i = 0; i < arquivo[y].Length; i++)
                {
                    fita.Add(arquivo[y][i] + "");
                }
                //Console.WriteLine("FITA POS " + y + " : " + arquivo[y]);
                fita.Add(branco);
                string aux = inicioMaquina(fita);
                saida.Add(aux);
            }
            fileSaida(saida);

        }
        public void start(string mtf, string inf, string outf) {
            artout = outf;
            fileMaquinaTuring(mtf);
            fileEntrada(inf);
        }
        public void fileMaquinaTuring(string lugar)
        {
            abrirArquivo abrir = new abrirArquivo();
            List<String> arquivo = abrir.abrir(lugar);
            inicial.estado = arquivo[0];
            string[] words = arquivo[1].Split(',');
            for (int i = 0; i < words.Length; i++)
            {
                Estados aux = new Estados();
                aux.estado = words[i];
                finais.Add(aux);
            }
            branco = arquivo[2];
            inicio = arquivo[3];
            for (int i = 4; i < arquivo.Count; i++)
            {
                string[] trans = arquivo[i].Split(',');
                Transicoes aux = new Transicoes();
                aux.From = trans[0];
                aux.To = trans[4];
                aux.readSymbol = trans[1];
                aux.writeSymbol = trans[2];
                aux.direction = trans[3];
                transicoes.Add(aux);
            }
        }

    }
}
