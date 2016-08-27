using System;
using System.Collections.Generic;
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
        List<String> fita = new List<String>();
        public bool verificarEstadosFinais(Estados aux) {
            for (int i = 0; i < finais.Count; i++) {
                if (finais[i].estado.Equals(aux.estado)) {
                    return true;
                }
            }
            return false;
        }
        
        public void inicioMaquina()
        {
            fileMaquinaTuring();
            fileEntrada();
            string atual = inicial.estado;
            int i = 0;
            int posFita = 0;
            bool parada = false;
            i = 0;
            do
            {
                Console.Write("1");
                if (transicoes[i].readSymbol.Equals(fita[posFita]) && atual.Equals(transicoes[i].From) && fita[posFita].Equals(transicoes[i].readSymbol))
                {
                    Estados aux = new Estados();
                    aux.estado = transicoes[i].To;
                    //Console.WriteLine("COMECA VERIFICACAO -------------------");
                    if (fita[posFita] == branco && verificarEstadosFinais(aux) == true)
                    {
                        Console.WriteLine("ATUAL -> " + atual);
                        Console.WriteLine("PARO BRANCO");
                        parada = true;
                    }
                    if (fita[posFita] == inicio && verificarEstadosFinais(aux) == true)
                    {
                        Console.WriteLine("PARO INICIO");
                        parada = true;
                    }
                    //Console.WriteLine("----------- ATUAL " + aux.estado);
                    //Console.WriteLine(fita[posFita] + " é igual a " + branco);
                    //Console.WriteLine(aux.estado + " contem em finais: " + finais.Contains(aux));
                    //Console.WriteLine("TERMINA ---------------------------------------");

                    Console.WriteLine("ATUAL -> " + atual);
                    Console.WriteLine("LEU FITA -> " + fita[posFita]);
                    Console.WriteLine("POSICAO FITA -> " + posFita);
                    Console.WriteLine("ESCREVEU FITA ->" + transicoes[i].writeSymbol);
                    Console.WriteLine("FOI PARA -> " + transicoes[i].To);
                    fita[posFita] = transicoes[i].writeSymbol;
                    atual = transicoes[i].To;
                    if (transicoes[i].direction == "D")
                    {
                        posFita++;

                        //Console.Write("DIREITA");
                    }
                    else if (transicoes[i].direction == "E")
                    {
                        posFita--;
                        //Console.WriteLine("Esquerda");
                    }

                }// else if (!transicoes[i].readSymbol.Equals(fita[posFita]) && !atual.Equals(transicoes[i].From) && !fita[posFita].Equals(transicoes[i].readSymbol)) {
                //    Console.WriteLine("REJEEITO");
                //    parada = true;
                //}
                
                i++;               
                if (i == transicoes.Count)
                {
                    i = 0;
                }

            } while (parada == false);
            Console.Write("\n");
            foreach (var a in fita)
            {
                Console.Write(a + "\n");
            }
        }
        public void fileEntrada()
        {
            abrirArquivo abrir = new abrirArquivo();
            List<String> arquivo = abrir.abrir("C:\\Users\\Matheus Bento\\Desktop\\Turing Machine\\Turing Machine\\Turing\\TuringMachine\\obj\\Release\\entrada\\duplobal.in");
            fita.Add(inicio);
            for (int i = 0; i < arquivo[0].Length; i++)
            {
                fita.Add(arquivo[0][i] + "");
            }
            fita.Add(branco);
            foreach (var a in fita)
            {
                Console.Write(a + "\n");
            }
        }
        public void fileMaquinaTuring()
        {
            abrirArquivo abrir = new abrirArquivo();
            List<String> arquivo = abrir.abrir("C:\\Users\\Matheus Bento\\Desktop\\Turing Machine\\Turing Machine\\Turing\\TuringMachine\\obj\\Release\\entrada\\duplobal.mt");
            Console.Write("INICIAL -> ");
            inicial.estado = arquivo[0];
            Console.Write(inicial.estado);
            string[] words = arquivo[1].Split(',');
            Console.Write("\nESTADOS FINAIS -> \n");
            for (int i = 0; i < words.Length; i++)
            {
                Estados aux = new Estados();
                aux.estado = words[i];
                Console.Write(aux.estado + " | ");
                finais.Add(aux);
            }
            branco = arquivo[2];
            Console.Write("\n" + "SIMBOLO BRANCO -> " + branco + "\n");
            inicio = arquivo[3];
            Console.Write("\n" + "SIMBOLO DE INICIO DE FITA -> " + inicio + "\n");
            Console.Write("\nTRANSICOES -> \n");
            for (int i = 4; i < arquivo.Count; i++)
            {
                Console.Write(arquivo[i] + "\n");
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
