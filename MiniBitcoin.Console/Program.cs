using System;
using MiniBitcoin.Console.Entities;

namespace MiniBitcoin.Console;

class Program
{
    static void Main(string[] args)
    {
        // Inicializa a carteira com 100.000 BRL e 0.5 BTC para testes
        var wallet = new Wallet(100000m, 0.5m);
        decimal currentBtcQuote = 500000m; // Cotação simulada inicial (1 BTC = 500.000 BRL)

        int option = 0;

        do
        {
            System.Console.Clear();
            System.Console.WriteLine("========================================");
            System.Console.WriteLine("       MINI BITCOIN - CARTEIRA          ");
            System.Console.WriteLine("========================================");
            System.Console.WriteLine($"Cotação Atual do BTC: R$ {currentBtcQuote:N2}");
            System.Console.WriteLine($"Saldo em BRL:         R$ {wallet.BalanceBrl:N2}");
            System.Console.WriteLine($"Saldo em Bitcoin:     {wallet.BalanceBtc:N8} BTC");
            System.Console.WriteLine($"Patrimônio Total:     R$ {wallet.GetTotalPortfolioValue(currentBtcQuote):N2}");
            System.Console.WriteLine("========================================");
            System.Console.WriteLine("1. Consultar / Atualizar Cotação do BTC");
            System.Console.WriteLine("2. Visualizar Saldos e Portfólio");
            System.Console.WriteLine("3. Comprar Bitcoin");
            System.Console.WriteLine("4. Vender Bitcoin");
            System.Console.WriteLine("5. Exibir Extrato de Transações");
            System.Console.WriteLine("0. Sair");
            System.Console.WriteLine("========================================");
            System.Console.Write("Escolha uma opção: ");

            if (!int.TryParse(System.Console.ReadLine(), out option))
            {
                System.Console.WriteLine("\nOpção inválida! Pressione qualquer tecla para continuar...");
                System.Console.ReadKey();
                continue;
            }

            try
            {
                switch (option)
                {
                    case 1:
                        System.Console.WriteLine($"\nCotação atual: R$ {currentBtcQuote:N2}");
                        System.Console.Write("Deseja alterar a cotação? (s/n): ");
                        string? resp = System.Console.ReadLine()?.Trim().ToLower();
                        if (resp == "s")
                        {
                            System.Console.Write("Informe o novo valor da cotação do BTC (BRL): ");
                            if (decimal.TryParse(System.Console.ReadLine(), out decimal newQuote) && newQuote > 0)
                            {
                                currentBtcQuote = newQuote;
                                System.Console.WriteLine("Cotação atualizada com sucesso!");
                            }
                            else
                            {
                                System.Console.WriteLine("Valor inválido para a cotação.");
                            }
                        }
                        break;

                    case 2:
                        System.Console.WriteLine($"\n--- Status do Portfólio ---");
                        System.Console.WriteLine($"BRL: R$ {wallet.BalanceBrl:N2}");
                        System.Console.WriteLine($"BTC: {wallet.BalanceBtc:N8} BTC");
                        System.Console.WriteLine($"Patrimônio Consolidado: R$ {wallet.GetTotalPortfolioValue(currentBtcQuote):N2}");
                        break;

                    case 3:
                        System.Console.WriteLine($"\n--- Comprar Bitcoin ---");
                        System.Console.Write($"Informe a quantidade de BTC que deseja comprar: ");
                        if (decimal.TryParse(System.Console.ReadLine(), out decimal amountToBuy))
                        {
                            wallet.BuyBtc(amountToBuy, currentBtcQuote);
                            System.Console.WriteLine("Compra realizada com sucesso!");
                        }
                        else
                        {
                            System.Console.WriteLine("Quantidade inválida.");
                        }
                        break;

                    case 4:
                        System.Console.WriteLine($"\n--- Vender Bitcoin ---");
                        System.Console.Write($"Informe a quantidade de BTC que deseja vender: ");
                        if (decimal.TryParse(System.Console.ReadLine(), out decimal amountToSell))
                        {
                            wallet.SellBtc(amountToSell, currentBtcQuote);
                            System.Console.WriteLine("Venda realizada com sucesso!");
                        }
                        else
                        {
                            System.Console.WriteLine("Quantidade inválida.");
                        }
                        break;

                    case 5:
                        System.Console.WriteLine($"\n--- Extrato de Transações ---");
                        if (wallet.Transactions.Count == 0)
                        {
                            System.Console.WriteLine("Nenhuma transação realizada até o momento.");
                        }
                        else
                        {
                            foreach (var tx in wallet.Transactions)
                            {
                                string tipo = tx.Type == TransactionType.Buy ? "COMPRA" : "VENDA";
                                System.Console.WriteLine($"[{tx.Date:dd/MM/yyyy HH:mm:ss}] {tipo} | {tx.AmountBtc:N8} BTC | Cotação: R$ {tx.BtcQuote:N2} | Total: R$ {tx.TotalBrl:N2}");
                            }
                        }
                        break;

                    case 0:
                        System.Console.WriteLine("\nEncerrando aplicação. Até mais!");
                        break;

                    default:
                        System.Console.WriteLine("\nOpção desconhecida.");
                        break;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"\n[Erro na Operação]: {ex.Message}");
            }

            if (option != 0)
            {
                System.Console.WriteLine("\nPressione qualquer tecla para retornar ao menu...");
                System.Console.ReadKey();
            }

        } while (option != 0);
    }
}