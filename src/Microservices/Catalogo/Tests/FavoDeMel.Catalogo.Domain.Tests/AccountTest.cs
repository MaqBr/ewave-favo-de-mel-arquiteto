using FavoDeMel.Catalogo.Domain.Models;
using FavoDeMel.Domain.Core.DomainObjects;
using Xunit;

namespace Catalogo.Unit.Tests
{
    public class AccountTest
    {
        [Fact]
        public void Account_Validar_ValidacoesDevemRetornarExceptions()
        {
            // Arrange & Act & Assert
            var ex = Assert.Throws<DomainException>(() =>
                new FavoDeMel.Catalogo.Domain.Models.Catalogo(string.Empty, 10)
            );

            Assert.Equal("O campo AccountType não pode estar vazio", ex.Message);
        }
    }
}
