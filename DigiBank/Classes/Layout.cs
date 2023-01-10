using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace DigiBank.Classes
{
    class Layout
    {
        private static List<Pessoa> pessoas = new List<Pessoa>();//Amazena conta cadastrada;

        private static int opcao = 0;
        public static void TelaPrincipal()
        {
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.White;
            
            Console.Clear();

            Console.WriteLine("                                                     ");
            Console.WriteLine("                 Digite a Opção desejada :           ");
            Console.WriteLine("                ===============================      ");
            Console.WriteLine("                 1 - Criar Conta                     ");
            Console.WriteLine("                ===============================      ");
            Console.WriteLine("                 2 - Entrar com CPF e Senha          ");
            Console.WriteLine("                ===============================      ");
            Console.WriteLine("                 3 - Sair                            ");


            opcao = int.Parse(Console.ReadLine());

            switch(opcao)
            {
                case 1:
                    TelaCriarConta();
                    break;

                case 2:
                    TelaLogin();
                        break;
                case 3:
                    Environment.Exit(0);//Fechar programa
                    break;
                default:
                    Console.WriteLine("Opção Inválida");
                    break;
            }
        }//Tela Principal
        private static void TelaCriarConta()//Criar uma conta
        {
            Console.Clear();

            Console.WriteLine("                                                     ");
            Console.WriteLine("                 Digite seu nome:                    ");
            string nome = Console.ReadLine();
            Console.WriteLine("                ===============================      ");
            Console.WriteLine("                 Digite o CPF:                       ");
            string cpf = Console.ReadLine();
            Console.WriteLine("                ===============================      ");
            Console.WriteLine("                 Digite sua senha:                   ");
            string senha = Console.ReadLine();
            Console.WriteLine("                ===============================      ");

            Console.WriteLine(nome);
            Console.WriteLine(cpf);
            Console.WriteLine(senha);

            ContaCorrente contaCorrente = new ContaCorrente();
            Pessoa pessoa = new Pessoa();

            pessoa.SetNome(nome);
            pessoa.SetCPF(cpf);
            pessoa.SetSenha(senha);
            pessoa.Conta = contaCorrente;

            pessoas.Add(pessoa);

            Console.Clear();

            Console.WriteLine("                Conta cadastrada com sucesso.        "); 
            Console.WriteLine("                ===============================      ");
            Thread.Sleep(1000); //Espera 1 segundo para ir para TelaLogada

            TelaContaLogada(pessoa);
            
        }
        private static void TelaLogin()//Logar no sistema
        {
            Console.Clear();

            Console.WriteLine("                                                     ");
            Console.WriteLine("                Digite o CPF :                       ");
            string cpf = Console.ReadLine();
            Console.WriteLine("                ===============================      ");
            Console.WriteLine("                Digite sua senha                     ");
            string senha = Console.ReadLine();
            Console.WriteLine("                ===============================      ");

            Pessoa pessoa = pessoas.FirstOrDefault(x => x.CPF == cpf && x.Senha ==senha); //Buscar pessoa pelo CPF e Senha

            if(pessoa!= null)
            {
                TelaBoasVindas(pessoa);
                TelaContaLogada(pessoa);
            }
            else{
                Console.Clear();
                
                Console.WriteLine("                Pessoa não cadastrada                ");
                Console.WriteLine("                ===============================      ");
                Console.WriteLine();
                Console.WriteLine();
            }
        }
        private static void TelaBoasVindas(Pessoa pessoa)//Tela de Boas Vindas
        {
            string msgTelaBemVindo =
                $"{pessoa.Nome} | Banco: {pessoa.Conta.GetCodigoDoBanco()} " +
                $"| Agencia: {pessoa.Conta.GetNumeroAgencia()} | Conta: {pessoa.Conta.GetNumeroDaConta()} ";

            Console.WriteLine("");
            Console.WriteLine($"          Seja Bem Vindo, {msgTelaBemVindo}");
            Console.WriteLine("");
        }
        private static void TelaContaLogada(Pessoa pessoa)//Tela depois de Logar
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine("                Digite a Opção desejada              ");
            Console.WriteLine("                ===============================      ");
            Console.WriteLine("                1 - Realizar um Depósito             ");
            Console.WriteLine("                ===============================      ");
            Console.WriteLine("                2 - Realizar um Saque                ");
            Console.WriteLine("                ===============================      ");
            Console.WriteLine("                3 - Consultar o Saldo                ");
            Console.WriteLine("                ===============================      ");
            Console.WriteLine("                4 - Extrato                          ");
            Console.WriteLine("                ===============================      ");
            Console.WriteLine("                5 - Perfil                           ");
            Console.WriteLine("                ===============================      ");
            Console.WriteLine("                6 - Sair                             ");

            opcao = int.Parse(Console.ReadLine());

            switch(opcao)
            {
                case 1:
                    TelaDeposito(pessoa);
                    break;
                case 2:
                    TelaSaque(pessoa);
                    break;
                case 3:
                    TelaSaldo(pessoa);
                    break;
                case 4:
                    TelaExtrato(pessoa);
                    break;
                case 5:
                    TelaPerfil(pessoa);
                    break;
                case 6:
                    TelaPrincipal();
                    break;
                default:
                    Console.Clear();                   
                    Console.WriteLine("                Opção Inválida!                      ");
                    Console.WriteLine("                ===============================      ");
                    break;
            }
        }
        private static void TelaDeposito(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine("               Digite o valor do depósito:            ");
            double valor = double.Parse(Console.ReadLine());

            Console.WriteLine("               ===========================            ");

            pessoa.Conta.Deposita(valor);

            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine("                                                      ");
            Console.WriteLine("                                                      ");
            Console.WriteLine("             Depósito Realizado com sucesso           ");
            Console.WriteLine("             ==============================           ");
            Console.WriteLine("                                                      ");

            OpcaoVoltarLogado(pessoa);
        }//Tela de depósito
        private static void TelaSaque(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine("               Digite o valor do saque:            ");
            double valor = double.Parse(Console.ReadLine());

            Console.WriteLine("               ===========================            ");

            bool okSaque = pessoa.Conta.Saque(valor);

            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine("                                                      ");
            Console.WriteLine("                                                      ");
            
            if(okSaque)
            {
                Console.WriteLine("             Saque realizado com sucesso              ");
                Console.WriteLine("             ==============================           ");
            }
            else
            {
                Console.WriteLine("             Saldo insuficiente!                       ");
                Console.WriteLine("             ==============================           ");
            }
            Console.WriteLine("                                                          ");

            OpcaoVoltarLogado(pessoa);
        }//Tela de Saque
        private static void TelaSaldo(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine($"         Seu saldo é: R${pessoa.Conta.ConsultaSaldo()}          ");
            Console.WriteLine("                 ==============================                ");
            Console.WriteLine("                                                               ");


            OpcaoVoltarLogado(pessoa);

        }//Tela para ver o saldo
        private static void TelaExtrato(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            if(pessoa.Conta.Extrato().Any()) //Verificar se tem algo no extrato
            {
                //Mostrar extrato
                double totalExtrato = pessoa.Conta.Extrato().Sum(x => x.Valor); //Soma os valores do extaro
                
                foreach(Extrato extrato in pessoa.Conta.Extrato())
                {
                    Console.WriteLine("                                                           ");
                    Console.WriteLine($"      Data: {extrato.Data.ToString("dd/mm/yyy HH:mm:ss")} ");//Formatação da data
                    Console.WriteLine($"      Tipo de Movimentação: {extrato.Descricao}           ");
                    Console.WriteLine($"      Valor: R${extrato.Valor}                              ");
                    Console.WriteLine("       =========================================           ");

                }

                Console.WriteLine("                                                               ");
                Console.WriteLine("                                                               ");
                Console.WriteLine($"                    SUBTOTAL: R${totalExtrato}                  ");
                Console.WriteLine("              =================================                ");
            
            }
            else
            {
                //Mostrar mensagem que nao há extrato
                Console.WriteLine("                                                        ");
                Console.WriteLine("               Não há extrato a ser exibido             ");
                Console.WriteLine("               ============================             ");
            }

            OpcaoVoltarLogado(pessoa);

        }//Tela para ver o extrato
        private static void TelaPerfil(Pessoa pessoa)

        {
            Console.Clear();

            Console.WriteLine($"               Nome: {pessoa.Nome}                  ");
            Console.WriteLine("                ===============================      ");
            Console.WriteLine($"               CPF: {pessoa.CPF}                    ");
            Console.WriteLine("                ===============================      ");
            Console.WriteLine($"               Senha:{pessoa.Senha}                 ");
            Console.WriteLine("                ===============================      ");
            Console.WriteLine($"               Agencia:{pessoa.Conta.GetNumeroAgencia()}");
            Console.WriteLine("                ===============================      ");
            Console.WriteLine("                                                      ");
            Console.WriteLine("                                                      ");


            Console.WriteLine("                Digite 1 Para alterar sua senha      ");
            Console.WriteLine("                ===============================      ");
            Console.WriteLine("                Digite 2 Para voltar                 ");
            Console.WriteLine("                ===============================      ");
            Console.WriteLine("                Digite 3 Para Excluir sua conta      ");

            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    Console.Clear();
                    TelaAlterarSenha(pessoa);
                    break;
                case 2:
                    Console.Clear();
                    TelaContaLogada(pessoa);
                    break;
                case 3:
                    Console.Clear();
                    TelaExcluirPefil(pessoa);
                    break;
            }
        }//Tela do Perfil
        private static void TelaExcluirPefil(Pessoa pessoa)
        {
            Console.WriteLine("Tem certeza que deseja excluir sua conta? Digite SIM para confirmar");
            string confirma = Console.ReadLine();

            switch (confirma)
            {
                case "SIM":
                    Console.WriteLine("                Sua conta foi excluida com sucesso                  ");
                    Console.WriteLine("                ==================================                  ");

                    pessoa.ExcluirConta();

                    Thread.Sleep(1000);
                    TelaPrincipal();
                    break;
                case "NÃO":
                    TelaPerfil(pessoa);
                    break;
                default:
                    Console.WriteLine("Comando inválido");
                    TelaExcluirPefil(pessoa);
                    break;
            }
        }//Tela de excluir perfil
        private static void TelaAlterarSenha(Pessoa pessoa)
        {
            Console.WriteLine("Digite sua senha antiga");
            string senhaantiga = Console.ReadLine();


            if (senhaantiga == pessoa.Senha)
            {
                Console.WriteLine("Digite sua nova senha");
                string senhanova = Console.ReadLine();

                Console.WriteLine("Digite sua nova senha novamente");
                string senhanova2 = Console.ReadLine();

                if (senhanova == senhanova2)
                {
                    pessoa.AlterarSenha(senhanova2);
                    Console.WriteLine("===========================");
                    Console.WriteLine("Senha alterada com sucesso");
                    Thread.Sleep(1500);
                    TelaPerfil(pessoa);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("As duas senhas não coincidem");
                    TelaAlterarSenha(pessoa);
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Senha incorreta");
                TelaAlterarSenha(pessoa);
            }

        }//Tela de alterar senha
        private static void OpcaoVoltarLogado(Pessoa pessoa)
        {
            Console.WriteLine("               Entre com uma opção abaixo             ");
            Console.WriteLine("               ==========================             ");
            Console.WriteLine("               1 - Voltar para minha conta            ");
            Console.WriteLine("               ==========================             ");
            Console.WriteLine("               2 - Sair                               ");

            opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
                TelaContaLogada(pessoa);
            else
                TelaPrincipal();
         }
        //private static void OpcaoVoltarDeslogado()
        //{
        //    Console.WriteLine("               Entre com uma opção abaixo             ");
        //    Console.WriteLine("               ===============================        ");
        //    Console.WriteLine("               1 - Voltar para o menu principal       ");
        //    Console.WriteLine("               ================================       ");
        //    Console.WriteLine("               2 - Sair                               ");

        //    opcao = int.Parse(Console.ReadLine());

        //    if (opcao == 1)
        //        TelaPrincipal();
        //    else
        //    {
        //        Console.WriteLine("Opção inválida!");
        //        Console.WriteLine("               ================================       ");

        //    }
        //}
    }
}