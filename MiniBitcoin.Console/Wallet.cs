namespace MiniBitcoin.Console.Entities;

public class Wallet
{
    public decimal BalanceBrl { get; private set; }
    public decimal BalanceBtc { get; private set; }

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
}