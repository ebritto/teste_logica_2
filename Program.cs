using System;

namespace teste_2
{
    class Program
    {
        public static DadosInterpolacao dadosInterpolacao = new DadosInterpolacao();
        public static double tempo = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("Programa para calcular tempo necessario de dissipacao de massa radioativa ate um percentual definido.");
            double massaAlvo = 0.10;
            Console.WriteLine($"Tempo aproximado necessario para atingir {massaAlvo} da massa: {CalcularTempoPerdaMassa(1, massaAlvo)} seg.");
            Console.WriteLine($"Tempo calculado por interpolacao: {Interpolar(dadosInterpolacao)} seg.");
        }
        private static double CalcularTempoPerdaMassa(double massaAtual, double massaAlvo)
        {
            //a cada 30 segundos perde-se 25% da massa.
            if (massaAtual > massaAlvo)
            {
                dadosInterpolacao.massaAnterior = massaAtual;
                dadosInterpolacao.tempoAnterior = tempo;
                massaAtual *= 0.75;
                tempo += 30;
                CalcularTempoPerdaMassa(massaAtual, massaAlvo);
            }

            dadosInterpolacao.massaPosterior = massaAtual;
            dadosInterpolacao.tempoPosterior = tempo;
            dadosInterpolacao.massaAlvo = massaAlvo;
            return tempo;
        }

        private static double Interpolar(DadosInterpolacao interpolacao)
        {
            if ((interpolacao.massaPosterior - interpolacao.massaAnterior) == 0)
            {
                return (interpolacao.tempoAnterior + interpolacao.tempoPosterior) / 2;
            }
            return interpolacao.tempoAnterior + (interpolacao.massaAlvo - interpolacao.massaAnterior) * (interpolacao.tempoPosterior - interpolacao.tempoAnterior) / (interpolacao.massaPosterior - interpolacao.massaAnterior);
        }
    }
}
