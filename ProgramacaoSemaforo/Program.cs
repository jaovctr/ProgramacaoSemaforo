using System.Diagnostics;
using System.Net.NetworkInformation;

namespace ProgramacaoSemaforo
{
    internal class Program
    {
   
        /* É criado um novo semáforo que permite apenas uma thread acesse o recurso por vez.
         */
        public static Semaforo semaforo = new Semaforo(1);
        static void Main(string[] args)
        {
            /* Cria 3 threads que acessarão o método recurso, e as inicia. 
             */
            Thread t1 = new Thread(Recurso);
            t1.Name = "Thread 1";
            t1.Start();

            Thread t2 = new Thread(Recurso);
            t2.Name = "Thread 2";
            t2.Start();

            Thread t3 = new Thread(Recurso);
            t3.Name = "Thread 3";
            t3.Start();

            //semaforo.FecharSemaforo();

            /* Caso de uso incorreto se fechar o semáforo na linha 21.
             * Isso fará com que a thread Main até execute o semáforo, porém as 3 threads criadas
             * já começam dentro do método recurso e, com isso, não reconhecem nenhum bloqueio e concorrem pelo mesmo recurso
             * simultâneamente.
             */
        }


        /* O método recurso fechará o semáforo, informará quem está acessando o recurso no momento e
         * continuará utilizando até o usuário informar do contrário.
         * Então, ao finalizar o uso, o semáforo será aberto e uma mensagem informará que o recurso foi utilizado.
         */
        public static void Recurso()
        {
            semaforo.FecharSemaforo();
            Console.WriteLine("{0} acessando o recurso", Thread.CurrentThread.Name);
            string continuar = "n";
            do
            {
                Console.WriteLine("{0} usando o recurso, continuar utilizando? (s|n)", Thread.CurrentThread.Name);
                continuar = Console.ReadLine();

            } while (continuar=="s");
            semaforo.AbrirSemaforo();

            Console.WriteLine("{0} usou o recurso", Thread.CurrentThread.Name);

        }
    }
}