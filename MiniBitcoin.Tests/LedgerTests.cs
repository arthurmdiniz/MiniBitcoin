using Xunit;
using MiniBitcoin.Console.Entities;
using System.Linq;

namespace MiniBitcoin.Tests;

public class LedgerTests
{
    [Fact]
    public void BuyAndSell_ShouldRecordTransactionsInLedger()
    {
        // Arrange
        var wallet = new Wallet(100000m, 0.0m);
        decimal quote = 500000m;

        // Act
        wallet.BuyBtc(0.1m, quote);
        wallet.SellBtc(0.05m, quote);

        // Assert
        Assert.Equal(2, wallet.Transactions.Count);
        Assert.Equal(TransactionType.Buy, wallet.Transactions.ElementAt(0).Type);
        Assert.Equal(TransactionType.Sell, wallet.Transactions.ElementAt(1).Type);
    }

    [Fact]
    public void GetTotalPortfolioValue_ShouldCalculateCorrectly()
    {
        // Arrange: 10.000 BRL e 1 BTC
        var wallet = new Wallet(10000m, 1.0m);
        decimal currentQuote = 600000m;

        // Act
        decimal totalValue = wallet.GetTotalPortfolioValue(currentQuote);

        // Assert (10.000 + 1 * 600.000)
        Assert.Equal(610000m, totalValue);
    }
}