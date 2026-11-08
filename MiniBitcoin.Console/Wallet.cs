using System;
using System.Collections.Generic;

namespace MiniBitcoin.Console.Entities;

public class Wallet
{
    public decimal BalanceBrl { get; private set; }
    public decimal BalanceBtc { get; private set; }
    
    private readonly List<TransactionRecord> _transactions = new();
    public IReadOnlyCollection<TransactionRecord> Transactions => _transactions.AsReadOnly();

    public Wallet(decimal initialBrl, decimal initialBtc)
    {
        if (initialBrl < 0 || initialBtc < 0)
        {
            throw new ArgumentException("Os saldos iniciais não podem ser negativos.");
        }

        BalanceBrl = initialBrl;
        BalanceBtc = initialBtc;
    }

    public void CreditBrl(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("O valor do crédito deve ser maior que zero.");
        BalanceBrl += amount;
    }

    public void DebitBrl(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("O valor do débito deve ser maior que zero.");
        if (amount > BalanceBrl) throw new InvalidOperationException("Saldo em BRL insuficiente.");
        BalanceBrl -= amount;
    }

    public void CreditBtc(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("O valor do crédito em BTC deve ser maior que zero.");
        BalanceBtc += amount;
    }

    public void DebitBtc(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("O valor do débito em BTC deve ser maior que zero.");
        if (amount > BalanceBtc) throw new InvalidOperationException("Saldo em Bitcoin insuficiente.");
        BalanceBtc -= amount;
    }

    public void BuyBtc(decimal amountBtc, decimal btcQuote)
    {
        if (amountBtc <= 0) throw new ArgumentException("A quantidade de BTC deve ser maior que zero.");
        if (btcQuote <= 0) throw new ArgumentException("A cotação do Bitcoin deve ser maior que zero.");

        decimal totalCostBrl = amountBtc * btcQuote;

        if (totalCostBrl > BalanceBrl)
        {
            throw new InvalidOperationException("Saldo em BRL insuficiente para realizar a compra.");
        }

        BalanceBrl -= totalCostBrl;
        BalanceBtc += amountBtc;

        _transactions.Add(new TransactionRecord(TransactionType.Buy, amountBtc, btcQuote));
    }

    public void SellBtc(decimal amountBtc, decimal btcQuote)
    {
        if (amountBtc <= 0) throw new ArgumentException("A quantidade de BTC deve ser maior que zero.");
        if (btcQuote <= 0) throw new ArgumentException("A cotação do Bitcoin deve ser maior que zero.");

        if (amountBtc > BalanceBtc)
        {
            throw new InvalidOperationException("Saldo em Bitcoin insuficiente para realizar a venda.");
        }

        decimal totalRevenueBrl = amountBtc * btcQuote;

        BalanceBtc -= amountBtc;
        BalanceBrl += totalRevenueBrl;

        _transactions.Add(new TransactionRecord(TransactionType.Sell, amountBtc, btcQuote));
    }

    public decimal GetTotalPortfolioValue(decimal currentBtcQuote)
    {
        if (currentBtcQuote < 0) throw new ArgumentException("A cotação não pode ser negativa.");
        return BalanceBrl + (BalanceBtc * currentBtcQuote);
    }
}