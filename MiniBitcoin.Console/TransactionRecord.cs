namespace MiniBitcoin.Console.Entities;

public enum TransactionType
{
    Buy,
    Sell
}

public class TransactionRecord
{
    public TransactionType Type { get; }
    public decimal AmountBtc { get; }
    public decimal BtcQuote { get; }
    public decimal TotalBrl { get; }
    public DateTime Date { get; }

    public TransactionRecord(TransactionType type, decimal amountBtc, decimal btcQuote)
    {
        Type = type;
        AmountBtc = amountBtc;
        BtcQuote = btcQuote;
        TotalBrl = amountBtc * btcQuote;
        Date = DateTime.Now;
    }
}