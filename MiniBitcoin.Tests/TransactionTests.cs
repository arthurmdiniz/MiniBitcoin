using Xunit;
using MiniBitcoin.Console.Entities;

namespace MiniBitcoin.Tests;

public class TransactionTests
{
    [Fact]
    public void BuyBtc_WithSufficientBrl_ShouldUpdateBalances()
    {
        var wallet = new Wallet(100000m, 0.0m);
        decimal btcQuote = 500000m; // 1 BTC = 500.000 BRL
        decimal amountBtcToBuy = 0.1m; // Custo: 50.000 BRL
        
        wallet.BuyBtc(amountBtcToBuy, btcQuote);
        Assert.Equal(50000m, wallet.BalanceBrl);
        Assert.Equal(0.1m, wallet.BalanceBtc);
    }

    [Fact]
    public void BuyBtc_WithInsufficientBrl_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var wallet = new Wallet(1000m, 0.0m);
        decimal btcQuote = 500000m;
        decimal amountBtcToBuy = 0.1m; // Declarado aqui também

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => wallet.BuyBtc(amountBtcToBuy, btcQuote));
    }

    [Fact]
    public void SellBtc_WithSufficientBtc_ShouldUpdateBalances()
    {
        
        var wallet = new Wallet(0.0m, 1.0m);
        decimal btcQuote = 500000m;
        decimal amountBtcToSell = 0.5m; // Recebe: 250.000 BRL

        wallet.SellBtc(amountBtcToSell, btcQuote);

        Assert.Equal(250000m, wallet.BalanceBrl);
        Assert.Equal(0.5m, wallet.BalanceBtc);
    }

    [Fact]
    public void SellBtc_WithInsufficientBtc_ShouldThrowInvalidOperationException()
    {
        var wallet = new Wallet(0.0m, 0.1m);
        decimal btcQuote = 500000m;

        Assert.Throws<InvalidOperationException>(() => wallet.SellBtc(0.5m, btcQuote));
    }
}