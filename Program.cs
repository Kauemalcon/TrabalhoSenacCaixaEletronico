using System;
using System.Globalization;

namespace TrabalhoSenacCaixaEletronico
{
    class Program
    {
        // variaveis globais para saldo da conta, e quantidades de notas
        static decimal saldoContaCorrente, valorDoSaque;
        static int quantidadeDeNotasDeDois, quantidadeDeNotasDeCinco, quantidadeDeNotasDeDez, quantidadeDeNotasDeVinte,
            quantidadeDeNotasDeCinquenta, quantidadeDeNotasDeCem, quantidadeDeNotasDeDuzentos;
        static int tentativasDeSenha = 0;


        static void Main(string[] args)
        {
            IniciarOpcoesDaConta();

        }

        private static void IniciarOpcoesDaConta()
        {
            Console.WriteLine("Digite 1 para depositar 2 para sacar um valor ou 0 para sair");
            int opcao = int.Parse(Console.ReadLine());
            // inicia no CMD com as alternativas de digitaçao 1,2 e 0 conforme projeto do prof.
            switch (opcao)
            {
                case 1:
                    Depositar();
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Saldo disponível: R${saldoContaCorrente}");
                    Console.ResetColor();
                    Sacar();
                    break;
                case 0:
                    IniciarOpcoesDaConta();
                    break;
            }

        }
        static void Depositar()
        {
            //Aqui inicio o algoritmo para solicitaçao de deposito 
            Console.WriteLine("Digite uma nota para depositar ou 0 para voltar ao menu anterior: ");
            int valorNota = int.Parse(Console.ReadLine());

            if (valorNota == 0)
            {
                IniciarOpcoesDaConta();
            }
            else
            {// Aqui inicio o pedido da quantidade de cedulas 'notas' para depositar na conta
                SelecionarQuantidadeDeNotasSaque(valorNota);
            }
        }

        private static void SelecionarQuantidadeDeNotasSaque(int valorNota)
        {
            Console.WriteLine($"Digite uma quantidade para a nota {valorNota} ou 0 para voltar ao menu anterior");
            int quantidadeNotas = int.Parse(Console.ReadLine());

            if (quantidadeNotas == 0)
            {
                Depositar();
            }
            else
            {// Aqui elaborei um 'bloco' de extrado de conta para saber as quantidades de notas e o valor que possuo na conta
                switch (valorNota)
                {
                    case 2:
                        quantidadeDeNotasDeDois += quantidadeNotas;
                        Console.WriteLine($"Quantidade de nota de {valorNota} foi atualizada para: {quantidadeDeNotasDeDois}");
                        break;
                    case 5:
                        quantidadeDeNotasDeCinco += quantidadeNotas;
                        Console.WriteLine($"Quantidade de nota de {valorNota} foi atualizada para: {quantidadeDeNotasDeCinco}");
                        break;
                    case 10:
                        quantidadeDeNotasDeDez += quantidadeNotas;
                        Console.WriteLine($"Quantidade de nota de {valorNota} foi atualizada para: {quantidadeDeNotasDeDez}");
                        break;
                    case 20:
                        quantidadeDeNotasDeVinte += quantidadeNotas;
                        Console.WriteLine($"Quantidade de nota de {valorNota} foi atualizada para: {quantidadeDeNotasDeVinte}");
                        break;
                    case 50:
                        quantidadeDeNotasDeCinquenta += quantidadeNotas;
                        Console.WriteLine($"Quantidade de nota de {valorNota} foi atualizada para: {quantidadeDeNotasDeCinquenta}");
                        break;
                    case 100:
                        quantidadeDeNotasDeCem += quantidadeNotas;
                        Console.WriteLine($"Quantidade de nota de {valorNota} foi atualizada para: {quantidadeDeNotasDeCem}");
                        break;
                    case 200:
                        quantidadeDeNotasDeDuzentos += quantidadeNotas;
                        Console.WriteLine($"Quantidade de nota de {valorNota} foi atualizada para: {quantidadeDeNotasDeDuzentos}");
                        break;
                }
                saldoContaCorrente += quantidadeNotas * valorNota;
                //Console.WriteLine($"Saldo disponivel :R$ {saldoContaCorrente}");

                Depositar();
            }
        }

        // Referente ao algoritmo de saques dando o retorno em saldo disponivel
        static void Sacar()
        {
            DateTime dataAtual = DateTime.Now;
            string diaDaSemana = dataAtual.ToString("dddd", new CultureInfo("pt-BR"));//Conversao para data por extenso

            Console.WriteLine("Digite o valor que deseja sacar");
            decimal valor = decimal.Parse(Console.ReadLine());

            AplicarRegrasDosDiasParaSaques(diaDaSemana, valor);
            if (valor <= saldoContaCorrente)
            {
                Console.WriteLine($"{valor}");
                Console.WriteLine("Digite 1 para especificar as notas, 2 para prosseguir e 0 para volta");
                int acao = int.Parse(Console.ReadLine());

                switch (acao)
                {
                    case 1:
                        EspecificarNotas(valor);
                        break;
                    case 2:
                        DescontarNaConta(valor);
                        break;
                    case 0:
                        Sacar();
                        break;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Saldo indisponível!");
                Console.ResetColor();
                IniciarOpcoesDaConta();
            }
        }

        private static void AplicarRegrasDosDiasParaSaques(string diaDaSemana, decimal valor)
        {
            if (diaDaSemana == "sábado" && valor > 1000)
            {
                Console.WriteLine("Valor de saque acima do permitido (R$1.000,00) para o dia, informe outro valor");
                Sacar();
            }
            else if (diaDaSemana == "domingo" && valor > 800)
            {
                Console.WriteLine("Valor de saque acima do permitido (R$800,00) para o dia, informe outro valor");
                Sacar();
            }
        }

        // validaçao da senha por extenso sabado/domingo,  informando as quantidades de
        // tentativas conforme os dias da semana e a janela de digitacoes de senha entre 10000 á 10060

        static bool ValidarSenha()
        {
            Console.WriteLine("Informe sua senha");
            int senha = int.Parse(Console.ReadLine());


            DateTime dataAtual = DateTime.Now;
            string diaDaSemana = dataAtual.ToString("dddd", new CultureInfo("pt-BR"));//Conversao para data por extenso

            if (senha > 10000 && senha < 10060)
            {
                return true;
            }
            else
            {
                tentativasDeSenha++;

                if ((diaDaSemana == "Sabado" || diaDaSemana == "Domingo") && tentativasDeSenha <= 3)
                {
                    ValidarSenha();
                }
                if ((diaDaSemana == "Sabado" && diaDaSemana == "Domingo") && tentativasDeSenha <= 4)
                {

                    ValidarSenha();
                }
                return false;
            }


        }

        static void ImprimirQuantidadeDeNotas()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Quantidade disponível de notas de R$200:{quantidadeDeNotasDeDuzentos}");
            Console.WriteLine($"Quantidade disponível de notas de R$100:{quantidadeDeNotasDeCem }");
            Console.WriteLine($"Quantidade disponível de notas de R$50:{quantidadeDeNotasDeCinquenta}");
            Console.WriteLine($"Quantidade disponível de notas de R$20:{quantidadeDeNotasDeVinte}");
            Console.WriteLine($"Quantidade disponível de notas de R$10:{quantidadeDeNotasDeDez}");
            Console.WriteLine($"Quantidade disponível de notas de R$5:{quantidadeDeNotasDeCinco}");
            Console.WriteLine($"Quantidade disponível de notas de R$2:{quantidadeDeNotasDeDois}");
            Console.ResetColor();
        }

        static void EspecificarNotas(decimal valor)
        {
            DateTime dataAtual = DateTime.Now;
            string diaDaSemana = dataAtual.ToString("dddd", new CultureInfo("pt-BR"));//Conversao para data por extenso

            string texto = "Quantidade de notas é superior a quantidade disponível!";

            if (diaDaSemana != "quarta-feira")
            {
                Console.WriteLine("Digite a quantidade de notas de 200 reais:");
                int quantidadeDesejadaDuzentos = int.Parse(Console.ReadLine());
                if (quantidadeDesejadaDuzentos > quantidadeDeNotasDeDuzentos)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{texto}");
                    Console.ResetColor();
                    ImprimirQuantidadeDeNotas();
                    IniciarOpcoesDaConta();
                }
                else
                {
                    valorDoSaque += quantidadeDesejadaDuzentos * 200;
                    ConsultandoSaldo(valorDoSaque, valor);
                }
            }

            Console.WriteLine("Digite a quantidade de notas de 100 reais");
            int quantidadeDesejadaCem = int.Parse(Console.ReadLine());
            if (quantidadeDesejadaCem > quantidadeDeNotasDeCem)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{texto}");
                Console.ResetColor();
                ImprimirQuantidadeDeNotas();
                IniciarOpcoesDaConta();
            }
            else
            {
                valorDoSaque += quantidadeDesejadaCem * 100;
                ConsultandoSaldo(valorDoSaque, valor);
            }

            Console.WriteLine("Digite a quantidade de notas de 50 reais");
            int quantidadeDesejadaCinquenta = int.Parse(Console.ReadLine());
            if (quantidadeDesejadaCinquenta > quantidadeDeNotasDeCinquenta)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{texto}");
                Console.ResetColor();
                ImprimirQuantidadeDeNotas();
                IniciarOpcoesDaConta();
            }
            else
            {
                valorDoSaque += quantidadeDesejadaCinquenta * 50;
                ConsultandoSaldo(valorDoSaque, valor);
            }

            Console.WriteLine("Digite a quantidade de notas de 20 reais");
            int quantidadeDesejadaVinte = int.Parse(Console.ReadLine());
            if (quantidadeDesejadaVinte > quantidadeDeNotasDeVinte)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{texto}");
                Console.ResetColor();
                ImprimirQuantidadeDeNotas();
                IniciarOpcoesDaConta();
            }
            else
            {
                valorDoSaque += quantidadeDesejadaVinte * 20;
                ConsultandoSaldo(valorDoSaque, valor);
            }

            Console.WriteLine("Digite a quantidade de notas de 10 reais");
            int quantidadeDesejadaDez = int.Parse(Console.ReadLine());
            if (quantidadeDesejadaDez > quantidadeDeNotasDeDez)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{texto}");
                Console.ResetColor();
                ImprimirQuantidadeDeNotas();
                IniciarOpcoesDaConta();
            }
            else
            {
                valorDoSaque += quantidadeDesejadaDez * 10;
                ConsultandoSaldo(valorDoSaque, valor);
            }

            Console.WriteLine("Digite a quantidade de notas de 5 reais");
            int quantidadeDesejadaCinco = int.Parse(Console.ReadLine());
            if (quantidadeDesejadaCinco > quantidadeDeNotasDeCinco)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{texto}");
                Console.ResetColor();
                ImprimirQuantidadeDeNotas();
                IniciarOpcoesDaConta();
            }
            else
            {
                valorDoSaque += quantidadeDesejadaCinco * 5;
                ConsultandoSaldo(valorDoSaque, valor);
            }

            Console.WriteLine("Digite a quantidade de notas de 2 reais");
            int quantidadeDesejadaDois = int.Parse(Console.ReadLine());
            if (quantidadeDesejadaDois > quantidadeDeNotasDeDois)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{texto}");
                Console.ResetColor();
                ImprimirQuantidadeDeNotas();
                IniciarOpcoesDaConta();
            }
            else
            {
                valorDoSaque += quantidadeDesejadaCinco * 2;
                ConsultandoSaldo(valorDoSaque, valor);
            }

            DescontarNaConta(valorDoSaque);
        }

        static void ConsultandoSaldo(decimal valorDoSaque, decimal valorDesejadoParaSaque)
        {
            if (valorDoSaque > valorDesejadoParaSaque)
            {
                ImprimirQuantidadeDeNotas();
                EspecificarNotas(valorDesejadoParaSaque);
            }

            return;
        }

        static void DescontarNaConta(decimal valor)
        {
            saldoContaCorrente -= valor;

            ValidarSenha();
            Console.WriteLine($"Saque Autorizado!");
            Console.WriteLine("\n");
            Console.WriteLine($"Saldo em conta: R${saldoContaCorrente}");
            IniciarOpcoesDaConta();
        }


    }
}