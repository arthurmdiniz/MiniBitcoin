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

    public void BuyBtc(decimal amountBtc, decimal btcQuote)
    {
        if (amountBtc <= 0) throw new ArgumentException("A quantidade de BTC deve ser maior que zero.");
        if (btcQuote <= 0) throw new ArgumentException("A cotação do Bitcoin deve ser maior que zero.");

        decimal totalCostBrl = amountBtc * btcQuote;
        if (totalCostBrl > BalanceBrl)
        {
            throw new InvalidOperationException("Saldo em BRL insuficiente para realizar a compra.");
        }

        BalanceBrl -= totalCostBrl; //Subtrai
        BalanceBtc += amountBtc;    //Adiciona
    }

    public void SellBtc(decimal btcAmount, decimal btcPrice)
    {
        if (btcAmount <= 0) throw new ArgumentException("O valor em BTC deve ser maior que zero.");
        if (btcPrice <= 0) throw new ArgumentException("O preço do BTC deve ser maior que zero.");
        if (btcAmount > BalanceBtc) throw new InvalidOperationException("Saldo em BTC insuficiente para vender.");

        decimal brlToReceive = btcAmount * btcPrice;
        DebitBtc(btcAmount);
        CreditBrl(brlToReceive);
    }

}