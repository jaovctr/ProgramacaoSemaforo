using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramacaoSemaforo
{
    internal class SemaforoBinario
    {

        private int maxThreads; // O máximo de Threads que poderão executar simultaneamente
        private int execLivres; // A quantidade de threads que podem executar no momento


        /* O Semáforo funciona da seguinte maneira: 
         * O construtor recebe a quantidade de Threads que podem ser executadas pela variável vagas
         * Os métodos FecharSemaforo() e AbrirSemaforo() controlam a quantidade de execuções disponíveis
         */

        /* Inicialmente o construtor configura a quantidade de execuções totais e livres para o mesmo valor,
         * afinal não existe nenhum bloqueio no momento
         */
        public SemaforoBinario(int quantThreads)
        {
            execLivres = quantThreads;
            maxThreads = quantThreads;
        }


        /* Através da instrução lock, declara uma área crítica e permite que apenas uma thread por vez faça a execução do trecho
         * ao bloquear a classe SemaforoBinario.
         * 
         * Se não houverem execuções disponíveis, a thread será bloqueada utilizando o Monitor.Wait e, com isso, outra thread
         * poderá acessar a área. 
         * 
         * Se houver uma vaga livre, o contador será decrescido em uma unidade.
         * 
         * No teste com 3 threads e uma vaga:
         * - a 1º a chamar o método diminui o execLivres para 0
         * - a 2º a chamar fica presa no Monitor.Wait
         * - a 3º a chamar fica presa na linha de lock, aguardando a liberação para verificar se tem vaga livre
         */
        public void FecharSemaforo()
        {
            lock(this)
            {
                while (execLivres==0)
                {
                    Monitor.Wait(this);
                }

                execLivres--;
            }
        }


        /* Novamente usando lock para bloquear o recurso, a thread que chamou o método AbrirSemaforo() vai verificar
         * se as execuções livres estão em quantidade menor do que o total de execuções permitidas.
         * 
         * Com isso, libera a vaga para outra thead acrescendo execLivres em uma unidade e acorda a thread que está na fila
         * com Monitor.Pulse() avisando que o bloqueio da classe acabou.
         */
        public void AbrirSemaforo()
        {
            lock (this)
            {
                if (execLivres<maxThreads)
                {
                    execLivres++;
                    Monitor.Pulse(this);
                }
            }
        }
    }
}

