using Xunit;
using MiniBitcoin.Console.Entities;

namespace MiniBitcoin.Tests;

public class WalletTests
{
    [Fact]
    public void Constructor_WithValidInitialBalances_ShouldCreateWallet()
    {
        var wallet = new Wallet(1000m, 0.5m);

        Assert.Equal(1000m, wallet.BalanceBrl);
        Assert.Equal(0.5m, wallet.BalanceBtc);
    }

    [Theory]
    [InlineData(-100, 1.0)]
    [InlineData(1000, -0.1)]
    [InlineData(-50, -0.5)]
    public void Constructor_WithNegativeBalances_ShouldThrowArgumentException(decimal brl, decimal btc)
    {
        Assert.Throws<ArgumentException>(() => new Wallet(brl, btc));
    }

    [Fact]
    public void DebitBrl_WithValidAmount_ShouldDecreaseBalance()
    {
        var wallet = new Wallet(500m, 1.0m);
        wallet.DebitBrl(200m);
        Assert.Equal(300m, wallet.BalanceBrl);
    }

    [Fact]
    public void DebitBrl_WithInsufficientFunds_ShouldThrowInvalidOperationException()
    {
        var wallet = new Wallet(100m, 1.0m);
        Assert.Throws<InvalidOperationException>(() => wallet.DebitBrl(200m));
    }
}